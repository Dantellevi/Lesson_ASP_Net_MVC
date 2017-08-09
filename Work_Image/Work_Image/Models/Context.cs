using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Work_Image.Models
{
    public class Context:DbContext
    {
        public Context() : base("ImageContext") { }

        public DbSet<Phone> phones { get; set; }

    }
}