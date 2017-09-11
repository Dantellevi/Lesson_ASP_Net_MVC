using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using ASP_Identity_Auth.Infrastructure;

namespace ASP_Identity_Auth.Controllers
{
    public class ClaimsController : Controller
    {

        [Authorize(Roles ="DCStaff")]
        public string OtherAction()
        {
            return "Это защищенный метод действия";
        }


        [ClaimsAccess(Issuer ="RemoteClaims",ClaimType =ClaimTypes.PostalCode, Value ="DC 20500")]
        public string OtherActions()
        {
            return "Это защищенный метод действия";
        }



        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;

            if(ident==null)
            {
                return View("Error", new string[] { "Нет доступных соединений" });
            }
            else
            {
                return View(ident.Claims);
            }
            
        }
    }
}