using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Binding_model.Models
{
    public class Context:DbContext
    {
        public Context() : base("BindingModel") { }
        public DbSet<Book> books { get; set; }

    }
}