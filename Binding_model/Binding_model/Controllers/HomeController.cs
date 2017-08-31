using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Binding_model.Models;
using System.Data.Entity;

namespace Binding_model.Controllers
{
    public class HomeController : Controller
    {

        Context db = new Context();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        /*
         * Выборочная привязка
            Иногда возникает возможность исключить некоторые свойства их привязки модели.
            Мы можем это сделать с помощью атрибута Bind.
            Для включения только определенных свойств мы можем использовать свойство Include данного атрибута:
         * */
         [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="Name,Author")]Book book)
        {
            if(ModelState.IsValid)
                {

                    

                }
            return View();
        }

        //Либо мы можем использовать свойство Exclude атрибута Bind, чтобы исключить свойство из привязки:
        /*
         * public ActionResult Create([Bind (Exclude="Year")] Book b)
            {
                  // ...
            }
         * */


        /*
         * При использовании параметра в методе действия привязка модели работает неявно.
         *  Но мы можем вызвать на контроллере и явную привязку модели с помощью методов UpdateModel
         *   и TryUpdateModel. Если модель не прошла валидацию, то метод UpdateModel выбрасывает исключение.
         *    Ниже показан пример использования метода UpdateModel в действии Edit вместо применения параметра:
         * 
         * */
        [HttpPost]
        public ActionResult Edit()
        {
            var book = new Book();
            try
            {
                UpdateModel(book);
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Во время редактирования возникли ошибки";
                return RedirectToAction("Index");
            }
        }


        /*
         * TryUpdateModel также вызывает привязку модели,
         *  но не выбрасывает исключение. Этот метод возвращает значение типа bool
         *   - если это значение равно true, модель прошла привязку, если false, то валидация прошла неудачно.
         * */

        [HttpPost]
        public ActionResult Editss()
        {
            var book = new Book();
            if (TryUpdateModel(book))
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Во время редактирования возникли ошибки";
                return RedirectToAction("Index");
            }
        }

    }
}