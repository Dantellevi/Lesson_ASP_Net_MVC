using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Project_Validation.Models
{
    public class Context:DbContext
    {
        public Context() : base("ValidatorContext") { }
        public DbSet<Book> books { get; set; }

    }
}