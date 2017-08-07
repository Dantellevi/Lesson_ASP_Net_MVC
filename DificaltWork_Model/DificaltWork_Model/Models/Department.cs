using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DificaltWork_Model.Models
{
    public class Department
    {
        public Instructor Administranor { get; set; }
        public decimal? Budget { get; set; }
        public ICollection<Course> Courses { get; set; }
        public int DepartmentID { get; set; }
        public int? InstructorID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

    }
}