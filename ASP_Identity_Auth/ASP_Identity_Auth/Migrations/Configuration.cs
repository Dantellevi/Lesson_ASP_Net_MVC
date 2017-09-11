namespace ASP_Identity_Auth.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ASP_Identity_Auth.Infrastructure;
    using ASP_Identity_Auth.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<ASP_Identity_Auth.Models.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Users.Infrastructure.AppIdentityDbContext";

        }

        protected override void Seed(ASP_Identity_Auth.Models.AppIdentityDbContext context)
        {
            AppUserManage userMgr = new AppUserManage(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string password = "mypassword";
            string email = "admin@gmail.ru";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userMgr.FindByName(userName);

            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            foreach(AppUser dbUser in userMgr.Users)
            {
                if(dbUser.Country==Countries.NONE)
                {
                    dbUser.SetCountryFromCity(dbUser.City);
                }
                //dbUser.City = Cities.MOSCOW;
            }
            context.SaveChanges();


        }
    }
}
