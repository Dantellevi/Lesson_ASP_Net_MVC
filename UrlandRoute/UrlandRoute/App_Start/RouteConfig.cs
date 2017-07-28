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

            //включение маршрутизации с помощью атрибутов

            //----------------------------------------------------

            routes.MapMvcAttributeRoutes();//инспектирует калссы контроллеров в приложения
            //в поисках атрибутов,конфигурирующие маршруты.

            //----------------------------------------------------


            //использование регулярнного выражения для ограничения маршрута
            //========================================
            routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new { controller="^H.*"},//соответствует только тем URL которые имеют значение переменной  controller  начинается с буквы Н
               new[] { "UrlandRoute.Controllers" });


            //========================================


            //ограничение маршрута заданным набором значений переменных сегментов 
            //========================================
            routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new { controller = "^H.*",//соответствует только тем URL которые имеют значение переменной  controller  начинается с буквы Н
               action="^Index$|^About$"},//соответствует маршруту со значением action== либо Index, либо About
               new[] { "UrlandRoute.Controllers" });


            //========================================

            //Ограничение  маршрута на основе метода HTTP
            //========================================
            routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new
               {
                   controller = "^H.*",//соответствует только тем URL которые имеют значение переменной  controller  начинается с буквы Н
                   action = "Index|About",
                   httpMethod=new HttpMethodConstraint("GET")

               },//соответствует маршруту со значением action== либо Index, либо About
               
               new[] { "UrlandRoute.Controllers" });


            //========================================

            //Использование  встроенных ограничений на основе типов(using System.Web.Mvc.Routing.Constraints;)

            //========================================
            routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new
               {
                   controller = "^H.*",//соответствует только тем URL которые имеют значение переменной  controller  начинается с буквы Н
                   action = "Index|About",
                   httpMethod = new HttpMethodConstraint("GET"),
                   id=new CompoundRouteConstraint(new IRouteConstraint[] {
                       new AlphaRouteConstraint(),
                       new MinLengthRouteConstraint(6)
                   })
               },//соответствует маршруту со значением action== либо Index, либо About

               new[] { "UrlandRoute.Controllers" });


            //========================================




            //использование  нескольких маршрутов для управления распознаванием простаранств имен

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlandRoute.AdditionalControllers" });

            routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlandRoute.Controllers" });


            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //определение дополнитеньных переменных внутри шаблона URL
            //-----------------------------------------------------------
            //определение необязательного сегмента URL| указание порядка распознавания пространств имен(Additional-раньше всех)
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional  }, new[] { "UrlandRoute.AdditionalControllers" });


            //-----------------------------------------------------------
            //Назначение переменной общего захвата (определение маршрутов переменной длины)

            //-----------------------------------------------------------
            //определение необязательного сегмента URL
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            //-----------------------------------------------------------


            //URL  который имеют сегмента содержащий как статические , так и переменные элементы
            //вроде шаблона
            //---------------------------------------------
            routes.MapRoute("", "X{controller}/{action}");

            //---------------------------------------------

            //смешивание статических и динамических URL
            //---------------------------------------

            routes.MapRoute("ShopSchema", "Shop/{action}",
                new { controller = "Home" });


            //-------------------------------------

            //создание псевдонима для контроллера и действия
            //-------------------------------------------------------------------

            routes.MapRoute("ShopSchema2", "Shop/OldAction",
                new { controller = "Home", action = "Index" });


            //------------------------------------------------------------------

            /*
             * В этом случае можно запрашивать URL вида http^//mydomain.com/Home
             * и тем самым вызывать метод действия Index() контроллера Home
             * */
             //------------------------------------------------------------------------------
            routes.MapRoute("MyRoute", "{controller}/{action}", new { action = "Index" });
            //-------------------------------------------------------------------------------

            //Вариант маршрута без элементов в URL
            //-----------------------------------------
            routes.MapRoute("MyRoute", "{controller}/{action}",
                new { Controller = "Home", action = "Index" });
            //-----------------------------------------
            /*этот шаблон будет соотсветствововать только URL содержащим тир сегмента: Public
             * а остальные два сегмента могут содержать любые  значения, и они будут использоваться для 
             * переменных controller и action
             * 
             * */
            routes.MapRoute("", "Public/{controller}/{action}", new { controller = "Home", action = "Index" });

            //-------------------------------------------------------------------------------
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            //первый способ регистрации маршрута
            ////---------------------------------------------------------------------------
            //Route myRote = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("MyRoute", myRote);
            //---------------------------------------------------------------------------
            //Второй способ регистрации маршрутов
            //-------------------------------------------------------
            //routes.MapRoute("MyRoute", "{controller}/{action}");
            //----------------------------------------------------------

        }
    }
}
