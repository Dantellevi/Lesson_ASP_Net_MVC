using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Identity_Auth.Models;
using ASP_Identity_Auth.Infrastructure;

using System.ComponentModel.DataAnnotations;
namespace ASP_Identity_Auth.Models
{

    /*
     * Для авторизации пользователей недостаточно просто создавать и удалять роли.
     *  Мы также должны уметь управлять ролями, назначать и удалять пользователей из роли.
     *   Это не сложный процесс, но для его реализации нам необходимо загружать данные о ролях 
     *   с помощью класса AppRoleManager, а затем вызывать методы, определенные в классе 
     *   AppUserManager на объектах, связанных с определенной ролью.
     *   -------------------------------------------------------------------------------------------
     *  Класс RoleEditModel содержит информацию о роли и
     *   определяет список пользователей в роли в виде коллекции
     *    объектов AppUser.
     *     Благодаря этому, мы сможем извлечь ID и имя каждого
     *      пользователя в роли 
     * */
    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }

    }
}