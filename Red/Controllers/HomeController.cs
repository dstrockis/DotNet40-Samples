using System.Collections.Generic;
using System.Web.Mvc;

// Added libraries
using Common;
using Microsoft.IdentityModel.Claims;
using System.Threading;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Web.Script.Serialization;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Configuration;
using System.Linq;

namespace Red.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string createUserStatus, string userCreatedStatus)
        {
            ViewBag.SamlContent = "Please login to view SAML token content.";
            ViewBag.CreateUserStatus = "";
            ViewBag.UserCreatedStatus = "";

            // The SAML token contents are automatically populated in the ClaimsIdentity.
            if (Request.IsAuthenticated)
            {
                var samlText = string.Empty;
                IClaimsIdentity ci = Thread.CurrentPrincipal.Identity as IClaimsIdentity;
                foreach (Claim claim in ci.Claims)
                    samlText += claim.ClaimType + ":   " + claim.Value + "\n\n";
                ViewBag.SamlContent = samlText;
            }

            if (createUserStatus != string.Empty)
            {
                ViewBag.CreateUserStatus = createUserStatus;
            }
            if (userCreatedStatus != string.Empty)
            {
                ViewBag.UserCreatedStatus = userCreatedStatus;
            }

            return View();
        }

        // You Could use [Authorize(Roles=)] Tags here instead of IsInRole below
        public ActionResult GetUserGroups()
        {
            var usergroups = new List<UserGroup>();

            ViewBag.GetGroupsStatus = "";

            // It is up to the application to ensure that only admins can read groups.
            if (!User.Identity.IsAuthenticated || !User.IsInRole(RocRole.Admin))
            {
                ViewBag.GetGroupsStatus = "You must sign in as an admin to view Groups.";
                return View(usergroups);
            }

            try
            {
                // Get a token for the Graph API in the context of the user's tenant.  Admins from hospital A can't see groups in hopital B.
                ClientCredential cc = new ClientCredential(ConfigurationManager.AppSettings["ida:ClientId"], ConfigurationManager.AppSettings["ida:AppKey"]);
                IClaimsIdentity ci = User.Identity as IClaimsIdentity;
                Claim tid = ci.Claims.Where(c => c.ClaimType == "http://schemas.microsoft.com/identity/claims/tenantid").First();
                AuthenticationContext authContext = new AuthenticationContext(String.Format(TenantConfig.authorityFormat, tid.Value));
                AuthenticationResult ar = authContext.AcquireToken(TenantConfig.graphResourceId, cc);

                HttpClient httpClient = new HttpClient();
                string graphRequest = TenantConfig.graphEndpoint + tid.Value + "/groups?api-version=" + TenantConfig.graphApiVersion;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, graphRequest);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ar.AccessToken);
                HttpResponseMessage response = httpClient.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string serializedJson = response.Content.ReadAsStringAsync().Result;
                    GraphGroupResult ggr = jss.Deserialize<GraphGroupResult>(serializedJson);
                    foreach (Value group in ggr.value)
                    {
                        usergroups.Add(new UserGroup
                            {
                                Id = group.objectId,
                                Name = group.displayName
                            });
                    }
                }
                else
                {
                    throw new WebException(response.Content.ReadAsStringAsync().Result);
                }

            }
            catch (Exception ex)
            {
                // A tenant admin needs to sign up for the app and grant it permission (via consent) before it can write to a tenant.
                if (ex.Message.Contains("Authorization_IdentityNotFound"))
                {
                    ViewBag.GetGroupsStatus = "Your admin needs to sign in once before you can get groups.";
                }

                ViewBag.GetGroupsStatus = "An error occurred when getting groups: " + ex.Message;
            }
            
            return View(usergroups);
        }

    }
}
