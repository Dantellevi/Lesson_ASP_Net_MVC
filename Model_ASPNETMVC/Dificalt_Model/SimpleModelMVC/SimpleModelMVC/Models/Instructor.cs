using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleModelMVC.Models
{
    public class Instructor:Person
    {
        //public int ID { get; set; }

        //[Required]
        //[Display(Name ="Имя")]
        //[StringLength(55)]
        //public string LastName { get; set; }

        //[Required]
        //[Column("FirstName")]
        //[Display(Name ="Фамилия")]
        //[StringLength(55)]
        //public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        [Display(Name ="Дата приема на работу")]
        public DateTime HireDate { get; set; }

        [Display(Name ="Полное имя")]
        //public string FullName
        //{
        //    get
        //    {
        //        return LastName + " " + FirstMidName;
        //    }
        //}


        public virtual ICollection<Course> Courses { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }


    }
}