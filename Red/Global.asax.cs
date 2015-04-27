
using Common;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;
using System;
using System.Web;
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
            FederatedAuthentication.WSFederationAuthenticationModule.SigningOut +=WSFederationAuthenticationModule_SigningOut;
        }

        private void WSFederationAuthenticationModule_SigningOut(object sender, SigningOutEventArgs e)
        {
            if (e.IsIPInitiated)
            {
                // Remove the application cookies, etc.
                WSFederationAuthenticationModule WsFam = FederatedAuthentication.WSFederationAuthenticationModule;
                HttpContext.Current.Session.Abandon();
                Response.Cookies.Clear();
            }
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

                // Uncomment the following to end the session with AAD on inactivity timeout.  But this will result in a single sign out from all apps.
                //SignOutRequestMessage signOutRequestMessage = new SignOutRequestMessage(new Uri(WsFam.Issuer), WsFam.Reply + "Account/Logout");
                //String signOutRequest = signOutRequestMessage.WriteQueryString() + "&wtrealm=" + WsFam.Realm;
                //Response.Redirect(signOutRequest);
            }
        }
    }
}