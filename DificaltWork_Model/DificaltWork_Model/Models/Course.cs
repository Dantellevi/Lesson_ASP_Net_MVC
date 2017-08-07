using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DificaltWork_Model.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int Credits { get; set; }
        public Department Department { get; set; }
        public int DepartmentID { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Instructor> Instrucors { get; set; }
        public string Title { get; set; }

    }
}