using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlandRoute.Controllers
{
    [RoutePrefix("Users")]
    public class CustomerController : Controller
    {
        
        [Route("~/Test")] //~--указываем инфраструктуре на то, что атрибут RoutePrefix не должен применяться к методу действия Index()
        public ActionResult Index()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Index";
            return View("ActionName");

        }

        public ActionResult List()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "List";
            return View("ActionName");

        }

        [Route("Add/{user}/{id:int}")]
        public string Create(string user, int id)
        {
            return string.Format("User:{0}, id:{1}", user, id);
        }


        [Route("Add/{user}/{id}/{password}")]
        public string ChangePass(string user, string password)
        {
            return string.Format("ChangePass Method-User:{0},Pass:{1}", user, password);
        }
    }
}