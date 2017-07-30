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
        public ActionResult Index()
        {

            var pl = db.players.Include(p => p.Team);
            return View(pl.ToList());

        }



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

        //список команд
        public ActionResult ListTeams()
        {
            var teamspls = db.teams.Include(t => t.Players);
            return View(teamspls.ToList());
        }

        //Описание команды
        public ActionResult TeamDetails(int? Id)
        {
            if(Id==null)
            {
                return HttpNotFound();
            }

            Team team = db.teams.Include(t =>t.Players).FirstOrDefault(t => t.Id == Id);
            return View(team);

        }
            


    }
}