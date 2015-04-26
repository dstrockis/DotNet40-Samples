using System;
using System.Collections.Generic;
using System.Web.UI;
using Common;

// Libraries Added
using System.Threading;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Common;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Net;
using System.Drawing;

namespace Blue
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAuthorizedUser = User.IsInRole(RocRole.User) || User.IsInRole(RocRole.Manager) || User.IsInRole(RocRole.Admin);
            bool isPowerUser = User.IsInRole(RocRole.Manager) || User.IsInRole(RocRole.Admin);
            bool isSuperUser = User.IsInRole(RocRole.Admin);

            btn_Admin.Enabled = isSuperUser;
            btn_Manager.Enabled = isPowerUser;
            btn_User.Enabled = isAuthorizedUser;

            txtSaml.Text = @"Please login to view SAML token content.";

            if (User.Identity.IsAuthenticated) 
            {
                var samlText = string.Empty;
                IClaimsIdentity ci = Thread.CurrentPrincipal.Identity as IClaimsIdentity;
                foreach (Claim claim in ci.Claims)
                    samlText += claim.ClaimType + ":   " + claim.Value + "\n\n";
                txtSaml.Text = samlText;   
            }
        }

        protected void btn_getUserGroup_Click(object sender, EventArgs e)
        {
            // TODO show the process of calling your API to get extra authentication info. This is limited for Admin only
            lst_UserGroups.DataSource = new List<string> {"dummy group/role 1", "dummy group/role 2", "dummy group/role 3"};
            lst_UserGroups.DataBind();
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(RocRole.Admin))
            {
                CreateUserStatus.Text = "Please sign in as an Admin to create users.";
                CreateUserStatus.ForeColor = Color.Red;
                CreateUserStatus.Visible = true;
                return;
            }

            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var userName = txtUserName.Text;
            var client = ddlClient.SelectedValue;

            CreateUserStatus.Visible = false;
            if (firstName == string.Empty || lastName == string.Empty || userName == string.Empty || client == string.Empty)
            {
                CreateUserStatus.Text = "Please enter values for all user fields";
                CreateUserStatus.ForeColor = Color.Red;
                CreateUserStatus.Visible = true;
                return;
            }

            try
            {
                ClientCredential cc = new ClientCredential(ConfigurationManager.AppSettings["ida:ClientId"], ConfigurationManager.AppSettings["ida:AppKey"]);
                AuthenticationContext authContext = new AuthenticationContext(String.Format(TenantConfig.authorityFormat, TenantConfig.ClientToTenantMapping[client]));
                AuthenticationResult ar = authContext.AcquireToken(TenantConfig.graphResourceId, cc);

                string newUpn = userName + '@' + TenantConfig.ClientToTenantMapping[client];
                string newPw = "P@ssw0rd123";

                object pp = new
                {
                    password = newPw,
                    forceChangePasswordNextLogin = true,
                };

                object user = new
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
                request.Content = new StringContent(jss.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                { 
                    CreateUserStatus.Text = "User " + newUpn + " created with temporary password " + newPw + ".";
                    CreateUserStatus.Visible = true;
                    CreateUserStatus.ForeColor = Color.Green;
                }
                else
                {
                    throw new WebException(response.Content.ReadAsStringAsync().Result);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Authorization_IdentityNotFound"))
                {
                    CreateUserStatus.Text = "An admin of client " + client + " needs to sign in once before you can create users.";
                    CreateUserStatus.Visible = true;
                    CreateUserStatus.ForeColor = Color.Red;
                    return;
                }

                CreateUserStatus.Text = "An error occurred when creating the user: " + ex.Message;
                CreateUserStatus.Visible = true;
                CreateUserStatus.ForeColor = Color.Red;
                return;
            }
        }
    }
}