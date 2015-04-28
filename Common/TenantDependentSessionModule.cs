using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Added Libraries
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Claims;

namespace Common
{
    // This class implements sliding sessions, so that the user remains logged in as long as they are active
    // in the application.
    public class TenantDependentSessionModule : SessionAuthenticationModule
    {
        protected override void OnSessionSecurityTokenReceived(SessionSecurityTokenReceivedEventArgs args)
        {
            base.OnSessionSecurityTokenReceived(args);

            // Implement Sliding Sessions for Inactivity Timeout
            DateTime now = DateTime.UtcNow;
            DateTime validFrom = args.SessionToken.ValidFrom;
            DateTime validTo = args.SessionToken.ValidTo;
            double timeout = (validTo - validFrom).TotalMinutes;

            // Reissue new session tokens if half of session lifetime has elapsed.
            if (validFrom.AddMinutes(timeout/2) < now && now < validTo)
            {
                args.SessionToken = this.CreateSessionSecurityToken(args.SessionToken.ClaimsPrincipal, args.SessionToken.Context, now, now.AddMinutes(timeout), args.SessionToken.IsPersistent);
                args.ReissueCookie = true;
            }
        }
    }
}
