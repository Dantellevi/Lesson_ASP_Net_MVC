using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_Image.Models;
namespace Work_Image.Controllers
{
    public class HomeController : Controller
    {
        Context db;
        public HomeController()
        {
            db = new Context();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Phone ph, HttpPostedFileBase uploadImage)
        {
            if(ModelState.IsValid && uploadImage!=null)
            {
                byte[] imageData = null;
                //считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                //установка массива байтов
                ph.Image = imageData;

                db.phones.Add(ph);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(ph);
        }



        public ActionResult Index()
        {
            return View(db.phones.ToList());
        }
    }
}