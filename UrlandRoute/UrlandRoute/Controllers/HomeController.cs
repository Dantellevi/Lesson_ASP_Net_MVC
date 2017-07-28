using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlandRoute.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");

        }

        public ActionResult CustomVariable(string id="")
        {
            /*Этот метод получает значение специальной переменной в шаблоне маршрута и передает
             * его прдставлению с помощью объекта ViewBag.
             * 
             * */


            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable =id;

            //проверка необязательной переменной сегмента
            ViewBag.CustomVariable = id ;
            return View();

        }

       
    }
}