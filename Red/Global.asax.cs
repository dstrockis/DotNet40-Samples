
using Common;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Red
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenReceived += new EventHandler<SessionSecurityTokenReceivedEventArgs>(SessionAuthenticationModule_SessionSecurityTokenReceived);
            FederatedAuthentication.ServiceConfigurationCreated += FederatedAuthentication_ServiceConfigurationCreated;
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