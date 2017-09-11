using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Identity_Auth.Models;

namespace ASP_Identity_Auth.Infrastructure
{
    public class AppUserManage : UserManager<AppUser>
    {
        public AppUserManage(IUserStore<AppUser> store) : base(store)
        {
        }

        public static AppUserManage Create(IdentityFactoryOptions<AppUserManage> options,IOwinContext context)
        {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
            AppUserManage manager = new AppUserManage(new UserStore<AppUser>(db));
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6, //Задает минимально допустимую длину пароля
                RequireNonLetterOrDigit = false,    //Если установлено значение true, пароль должен содержать хотя бы один символ, который не является ни буквой ни цифрой
                RequireDigit = false,   //Если установлено значение true, пароль должен содержать цифры
                RequireLowercase = true,    //Если установлено значение true, пароль должен содержать строчные символы
                RequireUppercase = true //Если установлено значение true, пароль должен содержать прописные символы
            };

            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,//Если задано true, имена пользователей могут содержать только буквы и цифр
                RequireUniqueEmail = true   //Требует наличие уникального адреса электронно почты
            };
            manager.UserValidator = new CustomUserValidator();


            return manager;
        }
    }
}