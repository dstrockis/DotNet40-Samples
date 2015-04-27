using System.Web.Mvc;
using Red.Models;

// Added Libraries
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Protocols.WSFederation;
using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Common;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Configuration;

namespace Red.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [ValidateInput(false)]
        public ActionResult Login()
        {
            if (!Request.IsAuthenticated)
            {
                WSFederationAuthenticationModule WsFam = FederatedAuthentication.WSFederationAuthenticationModule;
                SignInRequestMessage signIn = new SignInRequestMessage(new Uri(WsFam.Issuer), WsFam.Realm, WsFam.Reply);
                return new RedirectResult(signIn.WriteQueryString());
            }

            return new RedirectResult("/");
        }
         
        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                // Remove the application cookies, etc.
                WSFederationAuthenticationModule WsFam = FederatedAuthentication.WSFederationAuthenticationModule;
                WsFam.SignOut(false);

                // Issue a sign out request to remove the STS session, etc.
                SignOutRequestMessage signOutRequestMessage = new SignOutRequestMessage(new Uri(WsFam.Issuer), WsFam.Reply);
                String signOutRequest = signOutRequestMessage.WriteQueryString() + "&wtrealm=" + WsFam.Realm;
                return new RedirectResult(signOutRequest);
            }

            return new RedirectResult("/");

        }

        [HttpPost]
        public ActionResult CreateUser(CreateUserModel user)
        {
            ViewBag.CreateUserStatus = "";
            if (!User.Identity.IsAuthenticated || !User.IsInRole(RocRole.Admin))
            {
                return new RedirectResult("/?createUserStatus=Please sign in as an Admin to create users.");
            }

            var firstName = user.FirstName;
            var lastName = user.LastName;
            var userName = user.UserName;
            var client = user.Client;

            if (firstName == string.Empty || lastName == string.Empty || userName == string.Empty || client == string.Empty)
            {
                return new RedirectResult("/?createUserStatus=lease enter values for all user fields.");
            }

            string newUpn = userName + '@' + TenantConfig.ClientToTenantMapping[client];
            string newPw = "P@ssw0rd123";

            try
            {
                ClientCredential cc = new ClientCredential(ConfigurationManager.AppSettings["ida:ClientId"], ConfigurationManager.AppSettings["ida:AppKey"]);
                AuthenticationContext authContext = new AuthenticationContext(String.Format(TenantConfig.authorityFormat, TenantConfig.ClientToTenantMapping[client]));
                AuthenticationResult ar = authContext.AcquireToken(TenantConfig.graphResourceId, cc);

                object pp = new
                {
                    password = newPw,
                    forceChangePasswordNextLogin = true,
                };

                object newUser = new
                {
                    accountEnabled = true,
                    displayName = firstName + ' ' + lastName,
                    mailNickName = userName,
                    passwordProfile = pp,
                    userPrincipalName = newUpn,
                };

                JavaScriptSerializer jss = new JavaScriptSerializer();

                HttpClient httpClient = new HttpClient();
                string graphRequest = TenantConfig.graphEndpoint + TenantConfig.ClientToTenantMapping[client] + "/users?api-version=" + TenantConfig.graphApiVersion;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, graphRequest);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ar.AccessToken);
                request.Content = new StringContent(jss.Serialize(newUser), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new WebException(response.Content.ReadAsStringAsync().Result);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Authorization_IdentityNotFound"))
                {
                    return new RedirectResult("/?createUserStatus=An admin of client " + client + " needs to sign in once before you can create users.");
                }

                return new RedirectResult("/?createUserStatus=An error occurred when creating the user: " + ex.Message);

            }
            
            return new RedirectResult("/?createUserStatus=" + "User " + newUpn + " created with temporary password " + newPw + ".");

        }
    }
}
