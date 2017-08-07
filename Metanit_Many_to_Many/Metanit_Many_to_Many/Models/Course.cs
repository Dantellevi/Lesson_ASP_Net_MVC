using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metanit_Many_to_Many.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Course()
        {
            Students = new List<Student>();
        }
    }
}