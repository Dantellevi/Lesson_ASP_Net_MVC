using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace UrlandRoute
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //изменение конфигурации маршрутизации 
            //-------------------------------------------

            routes.MapMvcAttributeRoutes();
            routes.MapRoute("MyOtherRoute", "App/{action}", new { controller = "Home" }, new[] {"UrlsAndRoutes.Controllers" });


            //------------------------------------------------------------

            routes.MapRoute("MyRoute", "{controller}/{action}", null, new[] { "UrlsAndRoutes.Controllers" });


            routes.MapRoute("NewRoute", "App/Do{action}",
                new { controller = "Home" });


            routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });




        }
    }
}
