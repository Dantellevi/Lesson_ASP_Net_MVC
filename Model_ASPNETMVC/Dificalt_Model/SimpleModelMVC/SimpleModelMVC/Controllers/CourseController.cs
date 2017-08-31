using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleModelMVC.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace SimpleModelMVC.Controllers
{
    public class CourseController : Controller
    {
        UniversityContext db;
        public CourseController()
        {
            db = new UniversityContext();
        }
        
        //------------------------------Вывод данных на страницу----------------------------------\
        public ActionResult Index()
        {
            var courses = db.Courses.Include(t => t.Department);
            return View(courses.ToList());
        }
        //-----------------------------------------------------------------------------------------\


       //-------------------------------Добавление нового элемента--------------------------------\
        public ActionResult Create()
        {

            PopulateDepartmentsDropDownList();
            return View();

        }

        //---------------------------------------------------------------------------------------\


        //----------------------------------POST-метод добавления данных -------------------------\
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")]Course course)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);

        }

        //----------------------------------------------------------------------------------------------------

            public ActionResult Edit(int? Id)
        {
            if(Id!=null)
            {
                Course Courses = db.Courses.Find(Id);
                PopulateDepartmentsDropDownList(Courses.DepartmentID);
                return View(Courses);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseToUpdate = db.Courses.Find(id);
            if(TryUpdateModel(courseToUpdate,"",
                new string[] { "Title", "Credits", "DepartmentID" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                
            }
            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
            return View(courseToUpdate);
        }



        //---------------------------------Подгружаем данные из таблицы Departments в comboBox----------------\
        private void PopulateDepartmentsDropDownList(object selectedDepartment=null)
        {
            var departmentsQuery = from d in db.Departments
                                  orderby d.Name
                                  select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }

        //-----------------------------------------------------------------------------------------------------\

    }
}