using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleModelMVC.Models;
using System.Net;
using System.Data;
using PagedList;


namespace SimpleModelMVC.Controllers
{
    public class StudentController : Controller
    {

         UniversityContext db ;
        public StudentController()
        {
            db = new UniversityContext();
        }

        //-------------------------------------Создание новой модели----------------------

            [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]Student st)
        {
            if(ModelState.IsValid)
            {
                db.Students.Add(st);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }




       //----------------------------------------------------------------------------------------

        
        //-------------------------Вывод на экран данных-----------------------------------------|
        public ActionResult ListStudent(string sortOrder,string searchString, string currentFilter,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            //если мы получаем пустую строку то динамический элемент  ViewBag.NameSortParm=name_des
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //если sortOrder равен Date ViewBag.DateSortParm=date_desc иначе ViewBag.DateSortParm=Date
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //------------------------Пагинация--------------------------
            if(searchString!=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            //----------------------------------------------

            //производим выборку из базы данных
            var students = from s in db.Students
                           select s;

            //если строка поиска не равна  нулю то производим выборку из базы данных по введенным ключам
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":   //если sortOrder==name_desc сортировка по имени в порядке убывания
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":        //если sortOrder==Date сортировка по дате по возрастанию
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":   //если sortOrder==date_desc сортировка по дате по убыванию
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:            //по умолчанию по имени по возрастанию
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            //----------------------------Пагинация----------------------
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
            //-----------------------------------------------------------

            //return View(students.ToList());
        }

        //-------------------------------------------------------------------------------------------

            //---------------Показать результаты по выбранной модели---------------------\
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

            if (id==null)
            {
                return HttpNotFound();
            }
            return View(student);

        }

        //------------------------Редактирование существующей модели----------------------------------\


        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            Student student = db.Students.Find(Id);
            return View(student);

        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if(ModelState.IsValid)
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListStudent");
            }
            else
            {
                return View();
            }
            
            

        }

        //----------------------------Удаление модели--------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Удаление не удалось. Поробуйте еще раз или свяжитесь с администратором!!";
            }

            Student student = db.Students.Find(id);

            if (id == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("ListStudent");
        }
        //-------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}