using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Project_Validation.Models;
namespace Project_Validation.Controllers
{
    public class HomeController : Controller
    {

        Context db;
        public HomeController()
        {
            db = new Context();
        }

        // GET: Home
        public ActionResult Index()
        {
            var books = db.books;

            return View(books.ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Message = "Проверка данных прошла успешна!!!";
                db.books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Ошибка при проверке данных!!!Попробуйте еще раз!!!";
            return View(book);

        }



    }
}