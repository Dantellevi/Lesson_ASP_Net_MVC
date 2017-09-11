using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleModelMVC.Models
{
    public abstract class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Фамилия")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="Имя не должно дыть длинее 50 символов")]
        [Column("FirstName")]
        [Display(Name ="Имя")]
        public string FirstMidName { get; set; }

        [Display(Name ="Полное имя")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstMidName;
            }
        }
    }
}