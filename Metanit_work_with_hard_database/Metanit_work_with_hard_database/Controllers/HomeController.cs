using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metanit_work_with_hard_database.Models;
using System.Data.Entity;


namespace Metanit_work_with_hard_database.Controllers
{
    public class HomeController : Controller
    {
        Context db;

        public HomeController()
        {
            db = new Context();

        }

        // GET: Список игроков
        //==============================================================================
        public ActionResult Index()
        {

            var pl = db.players.Include(p => p.Team);
            return View(pl.ToList());

        }
        //=========================================================================

        //=========================================================================
        //описание игроков
        public ActionResult Details(int? Id)
        {
            //if(Id==null)
            //{
            //    return HttpNotFound();
            //}


            //Player pls = db.players.Find(Id);
            //return View(pls);

            if(Id==null)
            {
                return HttpNotFound();
            }

            Player plt = db.players.Include(p => p.Team).FirstOrDefault(p => p.Id == Id);
            return View(plt);


        }
        //=======================================================================
        //Добавляем объект
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Первый метод обрабатывает GET-запрос и возвращает представление, 
        //передавая в него объект SelectList - список всех команд.

        [HttpGet]
        public ActionResult Create()
        {
            //формируем список команд для передачи в представление
            SelectList teams = new SelectList(db.teams, "Id", "Name");
            ViewBag.Teams = teams;
            return View();


        }

        //Второй метод получает введенную пользователем
        //в представлении модель и добавляет ее в БД.

        [HttpPost]
        public ActionResult Create(Player player)
        {
            //добавляем игрока в таблицу
            db.players.Add(player);
            db.SaveChanges();

            //перенаправляем на главную страницу
            return RedirectToAction("Index");

        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++=

        //Редактирование модели
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Здесь также в виде объекта SelectList создается список команд,
        //которые извлекаются из БД. И после получения запроса на редактирование 
        //определенной модели Player контроллер передает эту модель и список команд
        //в представление
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if(Id==null)
            {
                return HttpNotFound();
            }

            //Находим в бд футболиста
            Player player = db.players.Find(Id);
            if(player!=null)
            {
                //Создаем список команд для передачи в представление
                SelectList teams = new SelectList(db.teams, "Id", "Name", player.TeamId);
                ViewBag.Teams = teams;
                return View(player);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult Edit(Player player)
        {
           
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //список команд
        //======================================================================
        public ActionResult ListTeams()
        {
            var teamspls = db.teams.Include(t => t.Players);
            return View(teamspls.ToList());
        }
        //======================================================================

        //Описание команды
        //=======================================================================
        public ActionResult TeamDetails(int? Id)
        {
            if(Id==null)
            {
                return HttpNotFound();
            }

            Team team = db.teams.Include(t =>t.Players).FirstOrDefault(t => t.Id == Id);
            return View(team);

        }
        //========================================================================
            


    }
}