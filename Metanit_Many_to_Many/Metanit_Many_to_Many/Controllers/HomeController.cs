using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Metanit_Many_to_Many.Models;
namespace Metanit_Many_to_Many.Controllers
{
    public class HomeController : Controller
    {
        StudentsContext db;
         
        public HomeController()
        {
            db = new StudentsContext();
        }
        // GET: Home
        public ActionResult Index()
        {
            
            return View(db.Students.Include(p=>p.Courses).ToList());
        }

        public ActionResult Details(int? Id)
        {
            if(Id==null)
            {
                return HttpNotFound();
            }
            var Student = db.Students.Find(Id);

            return View(Student);
        }


        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if(Id==null)
            {
                return HttpNotFound();
            }
            Student student = db.Students.Find(Id);
            ViewBag.Courses = db.Courses.ToList();
            return View(student);

        }

        [HttpPost]
        public ActionResult Edit(Student student,int[] selectedCourses)
        {
            Student st = db.Students.Find(student.Id);
            st.Name = student.Name;
            st.Surname = student.Surname;

            st.Courses.Clear();
            if(selectedCourses!=null)
            {

                //получаем выбранные курсы
                foreach (var c in db.Courses.Where(co => selectedCourses.Contains(co.Id)))
                {
                    st.Courses.Add(c);
                }
            }

            db.Entry(st).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Create()
        {   

            ViewBag.Courses = db.Courses.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student, int[] selectedCourses)
        {
            db.Students.Add(student);
            student.Courses.Clear();
            if (selectedCourses != null)
            {

                //получаем выбранные курсы
                foreach (var c in db.Courses.Where(co => selectedCourses.Contains(co.Id)))
                {
                    student.Courses.Add(c);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}