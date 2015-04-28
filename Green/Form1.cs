using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

// Added Libraries
using Common;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Runtime.InteropServices;

namespace Green
{
    public partial class Form1 : Form
    {
        private AuthenticationContext authContext;
        private const string clientId = "90191fd0-d7a7-4db2-b00c-597853dab931";
        private readonly Uri redirectUri = new Uri("http://medassetsgreenapp/"); 

        public Form1()
        {
            InitializeComponent();

            // Use the common endpoint since users from any tenant can sign into the app.
            authContext = new AuthenticationContext(String.Format(TenantConfig.authorityFormat, "common"));
            label6.Visible = false;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            AuthenticationResult ar;
            try {

                // Always prompt for sign in when the user clicks "Sign In"
                ar = authContext.AcquireToken(TenantConfig.graphResourceId, clientId, redirectUri, PromptBehavior.Always);
                label2.Text = ar.UserInfo.UserId;
            }
            catch (ActiveDirectoryAuthenticationException ex) {
                label6.Text = "Error Signing In: " + ex.Message;
                label6.Visible = true;
                label6.ForeColor = Color.Red;
                return;
            }

            ShowOAuth(ar);
            UpdateUI();
        }

        private void ShowOAuth(AuthenticationResult ar)
        {
            // The ADAL library provides some information about the user for UI display.
            string tokenContent = "";
            tokenContent += "Given Name: " + ar.UserInfo.GivenName + Environment.NewLine;
            tokenContent += "Family Name: " + ar.UserInfo.FamilyName + Environment.NewLine;
            tokenContent += "Identity Provider: " + ar.UserInfo.IdentityProvider + Environment.NewLine;
            tokenContent += "User Id: " + ar.UserInfo.UserId + Environment.NewLine;
            txt_Saml.Text = tokenContent;
        }

        private void UpdateUI()
        {
            // You shouldn't do authorization on the client.  You could technically open up
            // the id_token and grab role/group claims out of it, but it would be subject to
            // manipulation since it is running on the desktop.  Instead, you would simply send
            // the token to the resource application (web API), and enforce authorization there.
            // The resource will integrate with ClaimsPrincipal, where you can do IsInRole &
            // [Authorize(Roles="admin")], etc.

            btn_Admin.Enabled = false;
            btn_Manager.Enabled = false;
            btn_User.Enabled = false;
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            var firstName = txt_FirstName.Text;
            var lastName = txt_LastName.Text;
            var userName = txt_UserName.Text;
            var client = cbClient.SelectedItem == null ? "" : cbClient.SelectedItem.ToString();

            label6.Visible = false;
            AuthenticationResult ar;
            try
            {
                // Check if the user is signed in by using PromptBehavior.Never
                ar = authContext.AcquireToken(TenantConfig.graphResourceId, clientId, redirectUri, PromptBehavior.Never);
            }
            catch (ActiveDirectoryAuthenticationException ex)
            {
                // If the user wasn't signed in.
                if (ex.Message.Contains("user interaction was required"))
                {
                    label6.Text = "Please sign in as a tenant admin to create users.";
                    label6.Visible = true;
                    label6.ForeColor = Color.Red;
                    return;
                }

                label6.Text = "An error occurred signing you in: " + ex.Message;
                label6.Visible = true;
                label6.ForeColor = Color.Red;
                return;
            }
            
            // Validate input
            if (firstName == string.Empty || lastName == string.Empty || userName == string.Empty || client == string.Empty)
            {
                label6.Text = "Please enter values for all fields.";
                label6.Visible = true;
                label6.ForeColor = Color.Red;
                return;
            }

            string newUpn = userName + '@' + TenantConfig.ClientToTenantMapping[client];
            string newPw = "P@ssw0rd123";

            // Call the Graph API to create a user.
            try
            {
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
                // Only tenant admins can write to the directory, and it must be their own directory.
                if (ex.Message.Contains("Insufficient privileges") || ex.Message.Contains("Invalid domain name"))
                {
                    label6.Text = "You must be a tenant admin of " + client + " to create users.";
                    label6.Visible = true;
                    label6.ForeColor = Color.Red;
                    return;
                }

                label6.Text = "An error occurred when creating the user: " + ex.Message;
                label6.Visible = true;
                label6.ForeColor = Color.Red;
                return;
            }

            // Success!
            label6.Text = "User " + newUpn + " created with temporary password " + newPw + ".";
            label6.Visible = true;
            label6.ForeColor = Color.Green;
        }



        private void lnk_Logout_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // This will only sign the user out of the GREEN app.  In native 
            // applications, the identity is often closely tied to the device.
            // If the user clicks "sign out" in one application, it does not 
            // necessarily mean remove the account from the device.  It will not
            // affect the RED or BLUE app's session.

            label6.Visible = false;
            ClearCookies();
            authContext.TokenCacheStore.Clear();
            txt_Saml.Text = "";
            label2.Text = "(user_name)";
            UpdateUI();
        }

        private void lnk_RedWebClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // AAD can't do SSO between desktop apps and the browser.
            System.Diagnostics.Process.Start("https://localhost:44330/");
        }

        private void lnk_BlueWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // AAD can't do SSO between desktop apps and the browser.
            System.Diagnostics.Process.Start("https://localhost:44331/Default.aspx/");
        }

        // The below is necessary to clear the web browser control cookies during logout.
        private void ClearCookies()
        {
            const int INTERNET_OPTION_END_BROWSER_SESSION = 42;
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
    }
}
