using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ASP_Identity_Auth.Models;


namespace ASP_Identity_Auth.Infrastructure
{
    public class AppRoleManager : RoleManager<AppRole>, IDisposable
    {
        public AppRoleManager(IRoleStore<AppRole, string> store) : base(store)
        {
        }

        /*
         * Этот класс определяет статический метод Create(), который позволит OWIN создавать
         *  экземпляры класса AppRoleManager для всех запросов,
         *   где требуются данные Identity, не раскрывая информации о том,
         *    как данные о ролях хранятся в приложении.
         *     Чтобы зарегистрировать класс управления ролями в OWIN,
         *      необходимо отредактировать файл IdentityConfig.cs
         * */

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager>options,
            IOwinContext context)
        {
            return new AppRoleManager(new
                RoleStore<AppRole>(context.Get < AppIdentityDbContext>()));
        }
    }
}