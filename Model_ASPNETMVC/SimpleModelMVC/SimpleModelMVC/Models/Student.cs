using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SimpleModelMVC.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Display(Name = "Имя")]
        public string LastName { get; set; }
        [Display(Name = "Фамилия")]
        public string FirstMidName { get; set; }

        [Display(Name = "Время регистрации")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}