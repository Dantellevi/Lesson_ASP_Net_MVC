using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlHelpers.Models;
namespace HtmlHelpers.Controllers
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
    }
}