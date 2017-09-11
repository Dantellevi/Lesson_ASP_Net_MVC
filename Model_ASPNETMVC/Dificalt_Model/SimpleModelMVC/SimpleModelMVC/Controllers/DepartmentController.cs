using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SimpleModelMVC.Models;
using SimpleModelMVC.ViewModels;
using System.Data.Entity;
using System.Net;
using System.Collections;

namespace SimpleModelMVC.Controllers
{
    public class DepartmentController : Controller
    {
        UniversityContext db;
        public DepartmentController()
        {
            db = new UniversityContext();

        }

        /*
         * Метод отмечен asyncключевым словом, которое сообщает компилятору,
         *  чтобы он генерировал обратные вызовы для частей тела метода и автоматически
         *   создавал Task<ActionResult>возвращаемый объект
         *   --------------------------------------------------------------------------------
         *   awaitКлючевое слово было применено к вызова веб - службы. Когда компилятор видит
         *   это ключевое слово, за кулисами он разбивает метод на две части.
         *   Первая часть заканчивается операцией, которая запускается асинхронно.
         *   Вторая часть помещается в метод обратного вызова, который вызывается,
         *   когда операция завершается.
         *  --------------------------------------------------------------------------------
         *   
         * */
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.Administrator);
            return View(await departments.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {

            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if(department==null)
            {
                return HttpNotFound();
            }

            return View(department);

        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

    }
}