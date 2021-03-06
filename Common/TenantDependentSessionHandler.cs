﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Added libraries
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Claims;


namespace Common
{
    // This custom session handler sets app session lifetimes according to the security rules found in TenantConfig.
    // It allows you to enforce different timeouts for different organizations.
    public class TenantDependentSessionHandler : SessionSecurityTokenHandler
    {
        public override SessionSecurityToken CreateSessionSecurityToken(Microsoft.IdentityModel.Claims.IClaimsPrincipal principal, string context, string endpointId, DateTime validFrom, DateTime validTo)
        {
            // Get the session token lifetime depending on the tenant of the signed in user.
            IClaimsIdentity ci = principal.Identity as IClaimsIdentity;
            Claim tid = ci.Claims.Where(c => c.ClaimType == "http://schemas.microsoft.com/identity/claims/tenantid").First();
            double timeout = TenantConfig.GetTenantInactivityPeriod(tid.Value, (validTo - validFrom).TotalMinutes);

            return base.CreateSessionSecurityToken(principal, context, endpointId, validFrom, validFrom.AddMinutes(timeout));
        }
    }
}
