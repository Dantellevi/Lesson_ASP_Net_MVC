﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;


namespace ASP_Identity_Auth.Infrastructure
{
    public class ClaimsRoles
    {

        public static IEnumerable<Claim> CreateRolesFromClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();

            if (user.HasClaim(x => x.Type == ClaimTypes.StateOrProvince
            && x.Issuer=="RemoteClaims" && x.Value=="DC")
            && user.HasClaim(x=>x.Type==ClaimTypes.Role
            && x.Value=="Employees"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "DCtaff"));
            }

            return claims;
        }

    }
}