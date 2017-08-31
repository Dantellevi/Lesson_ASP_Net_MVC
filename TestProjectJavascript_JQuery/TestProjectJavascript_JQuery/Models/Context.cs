using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace TestProjectJavascript_JQuery.Models
{
    public class Context:DbContext
    {
        public Context() : base("AJAXbook") { }
        public DbSet<book> Books { get; set; }

    }
}