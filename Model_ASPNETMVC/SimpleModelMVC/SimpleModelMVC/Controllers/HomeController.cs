using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleModelMVC.Models;
using SimpleModelMVC.ViewModel;


namespace SimpleModelMVC.Controllers
{
    public class HomeController : Controller
    {
        UniversityContext db;

        public HomeController()
        {

            db = new UniversityContext();

        }

        public ActionResult Index()
        {


            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StuedentCount = dateGroup.Count()
                                                   };

            return View(data.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}