using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Metanit_work_with_hard_database.Models
{
    public class Context:DbContext
    {
        public DbSet<Player> players { get; set; }
        public DbSet<Team> teams { get; set; }

    }
}