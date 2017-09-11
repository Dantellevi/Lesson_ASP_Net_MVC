using System;
using System.Data.Entity;
using ASP_Identity_Auth.Models;
using ASP_Identity_Auth.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ASP_Identity_Auth.Models
{
    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
        //protected override void Seed(AppIdentityDbContext context)
        //{
        //    PerformInitialSetup(context);
        //    base.Seed(context);
        //}

        ///*
        // * В этом примере нам необходимо создавать экземпляры AppUserManager и AppRoleManager напрямую,
        // *  поскольку метод PerformInitialSetup() вызывается до запуска конфигурации OWIN.
        // *   Мы использовали классы RoleManger и AppManager для создания роли администратора
        // *    и добавления пользователя в эту роль.

        //    После внесения этих изменений, можно использовать атрибут Authorize для защиты 
        //    контроллеров Admin и RoleAdmin, как показано в примерах ниже:
        // * 
        // * */
        //public void PerformInitialSetup(AppIdentityDbContext context)
        //{
        //    AppUserManage userMgr = new AppUserManage(new UserStore<AppUser>(context));
        //    AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

        //    string roleName = "Administrators";
        //    string userName = "Admin";
        //    string password = "mypassword";
        //    string email = "admin@gmail.ru";

        //    if(!roleMgr.RoleExists(roleName))
        //    {
        //        roleMgr.Create(new AppRole(roleName));
        //    }

        //    AppUser user = userMgr.FindByName(userName);
        //    if(user==null)
        //    {
        //        userMgr.Create(new AppUser { UserName = userName, Email = email }, password);
        //        user = userMgr.FindByName(userName);

        //    }

        //    if(!userMgr.IsInRole(user.Id,roleName))
        //    {
        //        userMgr.AddToRole(user.Id, roleName);
        //    }
        //}
    }
}