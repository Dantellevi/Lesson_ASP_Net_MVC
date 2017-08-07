using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DificaltWork_Model.Models
{
    public class Instructor
    {
       public int InstructorID { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime? HideDate { get; set; }
        public ICollection<Course> Courses { get; set; }
        public OfficeAssigment OfficeAssigment { get; set; }

    }
}