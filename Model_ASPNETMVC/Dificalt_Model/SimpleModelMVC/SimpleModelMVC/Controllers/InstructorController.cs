using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleModelMVC.Models;
using System.Data.Entity;
using SimpleModelMVC.ViewModels;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace SimpleModelMVC.Controllers
{
    

    public class InstructorController : Controller
    {
        UniversityContext db;
        public InstructorController()
        {
            db = new UniversityContext();
        }   
        public ActionResult Index(int? id, int? courseID)
        {
            /*
             * Код начинается с создания экземпляра модели представления 
             * и размещения в нем списка инструкторов. 
             * Код указывает на загрузку для свойства навигации Instructor.OfficeAssignment и Instructor.
             * Courses навигации.
             * ------------------------------------------------------
             * Второй Includeметод загружает курсы,
             *  и для каждого загружаемого курса он загружает загрузку для Course.
             *  Departmentсвойства навигации.
             *  ---------------------------------------------------
             *  Как упоминалось ранее, желаемая загрузка не требуется,
             *   а делается для повышения производительности. Поскольку для представления всегда требуется
             *    OfficeAssignmentобъект, более эффективно извлекать его в том же запросе.
             *     CourseОбъекты требуются, когда инструктор выбран на веб-странице, 
             *     поэтому интенсивная загрузка лучше, чем ленивая загрузка, только если
             *      страница отображается чаще с выбранным курсом, чем без. 1
                Если был выбран идентификатор инструктора,
                выбранный преподаватель извлекается из списка инструкторов в модели представления.
                Затем Courses свойство модели представления загружается Course сущностями из
                Courses навигационного свойства этого инструктора .
             * */

            var viewModel = new InstructorIndexData();

            viewModel.Instructors = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses.Select(c => c.Department)
                ).OrderBy(i => i.LastName);
            
            if(id!=null)
            {
                /*
                 * WhereМетод возвращает коллекцию, но в этом случае критерий ,
                 *  принятый к этому результату метода только одного Instructor 
                 *  объекта возвращается. Single Метод преобразует коллекцию в единое 
                 *   Instructor образование, которое дает вам доступ к этому лицу Courses собственности.
                 * */


                ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel
                    .Instructors.Where(i => i.ID == id.Value)
                    .Single().Courses;

            }

            if(courseID!=null)
            {
                ViewBag.CourseID = courseID.Value;
                //Ленивая загрузка
                viewModel.Enrollments = viewModel.Courses.Where(
                    x => x.CourseID == courseID).Single().Enrollments;
                //явная загрузка
                //var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                //db.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                //foreach (Enrollment enrollment in selectedCourse.Enrollments)
                //{
                //    db.Entry(enrollment).Reference(x => x.Student).Load();
                //}

                //viewModel.Enrollments = selectedCourse.Enrollments;

            }

            return View(viewModel);
        }

        //-------------------------------Редактирование модели----------------------------------------------
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               
            }

            Instructor instructor = db.Instructors
                   .Include(i => i.OfficeAssignment)
                   .Include(i => i.Courses)
                   .Where(i => i.ID == id)
                   .Single();

            PopulateAssignedCourseData(instructor);

            if(instructor==null)
            {
                return HttpNotFound();
            }

            return View(instructor);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var instructorUpdate = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.ID == id)
                .Single();
            //обновляем указанные поля в базе данных
            if(TryUpdateModel(instructorUpdate,"",
                new string[] { "LastName", "FirstMidName", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                   
                    if(string.IsNullOrWhiteSpace(instructorUpdate.OfficeAssignment.Location))
                    {
                        instructorUpdate.OfficeAssignment = null;
                    }

                    UpdateInstructorCourses(selectedCourses, instructorUpdate);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                }
                

            }

            PopulateAssignedCourseData(instructorUpdate);
            return View(instructorUpdate);
        }

        //----------------------------------------------------------------------------------------

        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorUpdate)
        {
            //проверка
            if (selectedCourses == null)
            {
                instructorUpdate.Courses = new List<Course>();
                return;
            }
            //------------------------копирование элементов--------------------------
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorUpdate.Courses.Select(c => c.CourseID));
            //-----------------------------------------------------------------------

            foreach (var course in db.Courses)
            {
                //-------------проверяем содержится ли указанный элемент в списке-----------------------
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    //--------------проверяем отсутствует ли указанный элемент в списке-----------------
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorUpdate.Courses.Add(course);
                    }
                    //----------------------------------------------------------------------------------
                }
                else
                {
                    //--------------------------проверяем содержится ли указанный элемент в списке--------
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        //удаление
                        instructorUpdate.Courses.Remove(course);
                    }
                    //-------------------------------------------------------------------------------------
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = db.Courses;
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();

            foreach(var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title=course.Title,
                    Assigned=instructorCourses.Contains(course.CourseID)
                });

            }
            ViewBag.Courses = viewModel;
        }
    }
}