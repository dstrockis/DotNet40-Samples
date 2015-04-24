using System;

// Added libraries
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Protocols.WSFederation;

namespace Blue.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Remove the application cookies, etc.
            WSFederationAuthenticationModule WsFam = FederatedAuthentication.WSFederationAuthenticationModule;
            WsFam.SignOut(false);

            // Issue a sign out request to remove the STS session, etc.
            SignOutRequestMessage signOutRequestMessage = new SignOutRequestMessage(new Uri(WsFam.Issuer), WsFam.Realm);
            String signOutRequest = signOutRequestMessage.WriteQueryString() + "&wtrealm=" + fedAuthenticationModule.Realm;
            Response.Redirect(signOutRequest);
        }
    }
}