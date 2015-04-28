using System;
using System.Web.UI;

namespace Blue.Account
{
    public partial class Login : Page
    {
        // The redirect to login is configured in web.config for this app.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sign in to this page is controlled in web.config
            if (Page.User.Identity.IsAuthenticated)
                Response.Redirect("/");
        }
    }
}