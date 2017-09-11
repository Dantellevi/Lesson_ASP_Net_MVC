using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ASP_Identity_Auth.Models;
using ASP_Identity_Auth.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace ASP_Identity_Auth.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {


        //=============================================================================

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        //=============================================================================
        /*
         * С помощью параметра ReturnUrl мы сможем перенаправить пользователя,
         *  успешно прошедшего аутентификацию, на страницу, с которой он пришел.
         *   Это обеспечивает простой и понятный процесс навигации пользователя
         *    в приложении при аутентификации.

            Далее обратите внимание на атрибуты, которые я применил к контроллеру и его
            методам действий. Функции контроллера Account 
            (такие как смена пароля, например) по умолчанию должны быть доступны только
            авторизованным пользователям. Для этого мы применили атрибут Authorize 
            к контроллеру Account и добавили атрибут AllowAnonymous к некоторым методам
            действий. Это позволяет ограничить методы действий для авторизованных
            пользователей по умолчанию, но открыть доступ неавторизованным пользователям
            для входа в приложение.

            Наконец, мы добавили атрибут ValidateAntiForgeryToken который работает в
            связке с классом HtmlHelper, используемом в Razor в представлениях cshtml.
            Вспомогательный метод AntiForgeryToken защищает от межсайтовой подделки
            запросов CSRF, благодаря тому, что генерирует скрытое поле формы с токеном,
            который проверяется при отправке формы.
         * 
         * */



        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "В доступе отказано" });
            }

            ViewBag.returnUrl = returnUrl;
            return View();
        }


        /*
         * Самая простая часть - это проверка учетных данных, которую мы делаем с помощью
         *  метода FindAsync класса AppUserManager:
         * 
         * В дальнейшем мы будем многократно обращаться к экземпляру класса AppUserManager,
         *  поэтому мы создали отдельное свойство UserManager,
         *   который возвращает экземпляр класса AppUserManager с помощью метода 
         *   расширения GetOwinContext() класса HttpContext.

    Метод FindAsync принимает в качестве параметров имя и пароль, введенные пользователем,
    и возвращает экземпляр класса пользователя (AppUser в примере) если учетная запись
    с такими данными существует. Если нет учетной записи с таким именем или пароль не совпадает,
    метод FindAsync возвращает значение null. В этом случае мы добавляем ошибку в метаданные модели, которая будет отображена пользователю.<,/

        Если метод FindAsync возвращает объект AppUser, нам нужно создать файл cookie,
        который будет отправляться браузером в ответ на последующие запросы, благодаря чему
        пользователь будет автоматически аутентифицироваться в системе:
         * */

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel details, string returnUrl)
        {
            AppUser user = await UserManager.FindAsync(details.Name, details.Password);

            if(user==null)
            {
                ModelState.AddModelError("", "Некорректное имя и пароль.");

            }
            else
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                //-----------------------------------------------------------
                ident.AddClaims(LocationClaimsProvide.GetClaims(ident));
                //-----------------------------------------------------------

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent=false
                },ident);
                return Redirect(returnUrl);
            }

            return View(details);
        }

        //=============================================================================

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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