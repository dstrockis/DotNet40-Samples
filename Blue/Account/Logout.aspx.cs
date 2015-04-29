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
            if (User.Identity.IsAuthenticated) {
                // Remove the application cookies, etc.
                WSFederationAuthenticationModule WsFam = FederatedAuthentication.WSFederationAuthenticationModule;
                WsFam.SignOut(false);

                // Issue a sign out request to remove the STS session, etc.  This will trigger an SSOut.
                SignOutRequestMessage signOutRequestMessage = new SignOutRequestMessage(new Uri(WsFam.Issuer), WsFam.Reply);
                String signOutRequest = signOutRequestMessage.WriteQueryString() + "&wtrealm=" + WsFam.Realm;
                Response.Redirect(signOutRequest);
                return;
            }

            Response.Redirect("/");
        }
    }
}