using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleModelMVC.Models
{
    public class Student:Person
    {
        //public int ID { get; set; }
        //[Required]
        //[Display(Name = "Имя")]
        //[StringLength(50, MinimumLength = 1)]
        //public string LastName { get; set; }
        //[Required]
        //[Display(Name = "Фамилия")]
        //[StringLength(70, ErrorMessage = "Фамилия не может быть длиннее 70 символов!!!")]
        //[Column("FirstName")]
        //public string FirstMidName { get; set; }

        [Display(Name = "Время регистрации")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        //public string FullName
        //{
        //    get
        //    {
        //        return FirstMidName + " " + LastName;
        //    }
        //}
    }
}