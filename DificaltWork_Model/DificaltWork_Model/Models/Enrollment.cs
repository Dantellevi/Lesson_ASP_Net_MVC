using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DificaltWork_Model.Models
{
    public class Enrollment
    {

        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        public decimal? Grade { get; set; }
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

    }
}