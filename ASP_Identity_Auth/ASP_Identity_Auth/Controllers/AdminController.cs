using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Identity_Auth.Infrastructure;
using ASP_Identity_Auth.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ASP_Identity_Auth.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(UserViewModels model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }

            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        //===============================================================================
        /*
         * Метод действия Delete принимает уникальный идентификатор пользователя 
         * в качестве строкового параметра, после чего мы используем метод FindByIdAsync(),
         *  чтобы найти объект пользователя с таким идентификатором. 
         *  Метод DeleteAsync() возвращает объект типа IdentityResult,
         *   который я проверяю также, как и в предыдущих примерах на наличие ошибок.
         *    Вы можете протестировать новую функциональность — создайте нового пользователя,
         *     а затем нажмите «Удалить» рядом с именем пользователя в представлении Index.
         * */

        [HttpPost]
            public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Пользователь не найден" });
            }
        }

        //======================================================================================
        //---------------------------Редактирование-------------------------------------

        /*
         * Первый метод действия Edit обрабатывает GET-запросы с идентификатором пользователя,
         *  выполняет поиск с помощью метода FindByIdAsync() и возвращает представление Edit.cshtml
         *   с возможностью редактирования если пользователь найден, иначе перенаправляет обратно
         *    на представление Index.

            Второй метод обрабатывает POST-запросы с данными пользователя,
            которые передаются в качестве параметров: имя, адрес почты и пароль.
            В этом методе мы должны решить несколько задач, связанных с редактированием пользователей.
            ---------------------------------------------------------
            Первая задача — выполнить валидацию значений,
            которые мы получаем из формы.
            В данном примере мы работаем с простым объектом данных пользователя,
            однако, позже я покажу вам, как взаимодействовать с более сложными объектами.
            Итак, нам необходимо выполнить проверки данных пользователя и пароля,
            которые мы добавили в предыдущей статье в класс AddUserManager.
            Начинаем с проверки адреса электронной почты:
            ---------------------------------------------------------
            Следующий шаг - сменить пароль, если таковой был предоставлен.
            ASP.NET Identity не хранит пароли в чистом виде, а использует их хэш.
            Поэтому, мы сначала проверяем пароль, затем генерируем его хэш и сохраняем в свойстве PasswordHash.


            Пароли преобразуются в хэш через реализацию интерфейса IPasswordHasher 
            (в данном случае используется свойство AppUserManager.PasswordHasher).
            Интерфейс IPasswordHasher определяет метод HashPassword(),
            который принимает строковый аргумент и возвращает его хэшированное значение:
            ---------------------------------------------------------

            Изменения пользовательских данных не сохраняются в базе данных вплоть до вызова метода UpdateAsync():

         * */

        [HttpGet]
                public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult>Edit(string id, string email, string password)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                user.Email = email;
                IdentityResult validEmail = await UserManager
                    .UserValidator.ValidateAsync(user);
                if(!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);

                }
                IdentityResult Validpass = null;
                if(password!=string.Empty)
                {
                    Validpass = await UserManager.PasswordValidator
                        .ValidateAsync(password);

                    if (Validpass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher
                            .HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(Validpass);
                    }
                }

                if ((validEmail.Succeeded && Validpass == null) ||
                        (validEmail.Succeeded && password != string.Empty && Validpass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View(user);

            
        }
        private AppUserManage UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManage>();
            }
        }
    }
}