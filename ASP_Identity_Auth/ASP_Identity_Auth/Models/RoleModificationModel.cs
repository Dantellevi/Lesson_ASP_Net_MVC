using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Identity_Auth.Infrastructure;
using ASP_Identity_Auth.Models;
using System.ComponentModel.DataAnnotations;
namespace ASP_Identity_Auth.Models
{
    public class RoleModificationModel
    {
        /*
         * Класс RoleModificationModel будет получать данные от
         *  системы привязки модели во время редактирования данных
         *   пользователя. Он содержит массив идентификаторов
         *    пользователей, а не объектов AppUser, для замены
         *     ролей.
         * */


        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }

    }
}