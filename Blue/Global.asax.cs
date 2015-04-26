using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using Blue;

// Added libraries
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Common;
using Microsoft.IdentityModel.Tokens;

namespace Blue
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenReceived += new EventHandler<SessionSecurityTokenReceivedEventArgs>(SessionAuthenticationModule_SessionSecurityTokenReceived);
            FederatedAuthentication.ServiceConfigurationCreated += FederatedAuthentication_ServiceConfigurationCreated;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        private void FederatedAuthentication_ServiceConfigurationCreated(object sender, Microsoft.IdentityModel.Web.Configuration.ServiceConfigurationCreatedEventArgs e)
        {
            e.ServiceConfiguration.SecurityTokenHandlers.AddOrReplace(new TenantDependentSessionHandler());
        }

        private void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            if (e.SessionToken.ValidTo < DateTime.UtcNow)
            {
                // Remove the application cookies, etc.
                WSFederationAuthenticationModule WsFam = FederatedAuthentication.WSFederationAuthenticationModule;
                WsFam.SignOut(false);

                // Issue a sign out request to remove the STS session, etc.
                SignOutRequestMessage signOutRequestMessage = new SignOutRequestMessage(new Uri(WsFam.Issuer), WsFam.Reply + "Account/Logout");
                String signOutRequest = signOutRequestMessage.WriteQueryString() + "&wtrealm=" + WsFam.Realm;
                Response.Redirect(signOutRequest);
            }
        }
    }
}
