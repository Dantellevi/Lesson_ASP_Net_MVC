﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using System.Reflection;
using System.Security.Claims;


namespace ASP_Identity_Auth.Infrastructure
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            AppUserManage mgr = HttpContext.Current
                .GetOwinContext().GetUserManager<AppUserManage>();

            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }

        public static MvcHtmlString ClaimType(this HtmlHelper html, string claimType)
        {
            FieldInfo[] fields = typeof(ClaimTypes).GetFields();

            foreach(FieldInfo field in fields)
            {
                if(field.GetValue(null).ToString()==claimType)
                {
                    return new MvcHtmlString(field.Name);
                }
            }

            return new MvcHtmlString(string.Format("{0}",
                claimType.Split('/', '.').Last()));
        }
    }
}