using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DificaltWork_Model.Models
{
    public class OfficeAssigment
    {
        public int InstructorID{get;set;}
        public Instructor Instructor { get; set; }
        public string Location { get; set; }

    }
}