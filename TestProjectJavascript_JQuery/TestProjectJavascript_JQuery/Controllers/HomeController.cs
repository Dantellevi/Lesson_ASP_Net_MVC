using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProjectJavascript_JQuery.Models;
using System.Data.Entity;

namespace TestProjectJavascript_JQuery.Controllers
{
    public class HomeController : Controller
    {
        private Context db;

        public HomeController()
        {
            db = new Context();
        }

        // GET: Home
        public ActionResult Index()
        {
            var books = db.Books.ToList();

            return View(books);
        }


        [HttpPost]
        public ActionResult BookSearch(string name)
        {
            var allbooks = db.Books.Where(a => a.Author.Contains(name)).ToList();
            if (allbooks.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(allbooks);
        }

         public JsonResult JsonSearch(string name)
        {
            /*Новое действие теперь возвращает объект JsonResult,
             * который принимает объект с результатами запроса (в данном случае объект jsondata).
             * Второй необязательный параметр представляет значение перечисления
             *  JsonRequestBehavior и может принимать два значения:
             *   AllowGet (разрешить Get-запросы) и DenyGet (запретить Get-запросы).
             *   В данном случае мы разрешаем действию посылать результаты в JSON-формате
             *   в ответ на запросы Get.
             * */

            var jsondata = db.Books.Where(a => a.Author.Contains(name)).ToList<book>();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BestBook()
        {
            book book = db.Books.First();
            return PartialView(book);
        }
    }
}