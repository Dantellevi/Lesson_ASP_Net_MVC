﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
namespace ASP_Identity_Auth.Models
{
    public class AppRole:IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name)
            :base(name)
        { }

    }
}