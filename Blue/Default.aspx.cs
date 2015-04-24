﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using Common;

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

            txtSaml.Text = @"{SAML CONTENT}";
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var userName = txtUserName.Text;
            var client = ddlClient.SelectedValue;
            // TODO show the process of creating new user
        }

        protected void btn_getUserGroup_Click(object sender, EventArgs e)
        {
            // TODO show the process of calling your API to get extra authentication info. This is limited for Admin only
            lst_UserGroups.DataSource = new List<string> {"dummy group/role 1", "dummy group/role 2", "dummy group/role 3"};
            lst_UserGroups.DataBind();
        }
    }
}