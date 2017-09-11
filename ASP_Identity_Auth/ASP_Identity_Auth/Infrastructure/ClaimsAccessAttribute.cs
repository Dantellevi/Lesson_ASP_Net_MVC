using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace ASP_Identity_Auth.Infrastructure
{
    public class ClaimsAccessAttribute:AuthorizeAttribute
    {
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }


        protected override bool AuthorizeCore(HttpContextBase Context)
        {
            return Context.User.Identity.IsAuthenticated
                && Context.User.Identity is ClaimsIdentity
                && ((ClaimsIdentity)Context.User.Identity).HasClaim(x =>
                 x.Issuer == Issuer && x.Type == ClaimType && x.ValueType==Value);
        }
    }
}