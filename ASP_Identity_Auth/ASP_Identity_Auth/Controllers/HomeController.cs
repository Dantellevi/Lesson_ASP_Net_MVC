using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Identity_Auth.Models;
using ASP_Identity_Auth.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ASP_Identity_Auth.Controllers
{
    public class HomeController : Controller
    {

        private AppUserManage UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManage>();
            }
        }

        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }


        [Authorize]
        public ActionResult UserProps()
        {
            return View(CurrentUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult>UserProps(Cities city)
        {
            AppUser user = CurrentUser;
            user.City = city;
            user.SetCountryFromCity(city);
            await UserManager.UpdateAsync(user);
            return View(user);
        }


        [NonAction]
        public static string GetCityName<TEnum>(TEnum item)
            where TEnum:struct,IConvertible
        {
            if(!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Тип TEnum должен быть перечислением!!!");

            }
            else
            {
                return item.GetType()
                    .GetMember(item.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .Name;
            }
        }


        /*
         * В этом примере мы оставили атрибут Authorize для метода действия Index без изменений,
         *  но добавили этот атрибут к методу OtherAction,
         *   задав при этом свойство Roles, ограничивающее доступ к этому методу только
         *    для пользователей, являющихся членами роли Users. Мы также добавили метод GetData(),
         *     который добавляет некоторую базовую информацию о пользователе, используя свойства,
         *      доступные через объект HttpContext.
         * 
         * */





        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Users?", HttpContext.User.IsInRole("Users"));

            return dict;
        }
    }
}