using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DificaltWork_Model.Models
{
    public class Student
    {
        public int studentID { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FullMidName { get; set; }

        [Display(Name = "Регистрация")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public DateTime EnrollmentDate { get; set; }

    }
}