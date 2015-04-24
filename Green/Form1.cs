using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common;

namespace Green
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            // TODO show login and update the Thread.CurrentPrincipal 

            ShowSAML();
            UpdateUI();
        }

        private void ShowSAML()
        {
            // TODO show us the SAML or other detailed authrization info
            txt_Saml.Text = @"(SAML CONTENT)";
        }

        private void UpdateUI()
        {
            var principal = Thread.CurrentPrincipal;
            bool isAuthorizedUser = principal.IsInRole(RocRole.User) || principal.IsInRole(RocRole.Manager) || principal.IsInRole(RocRole.Admin);
            bool isPowerUser = principal.IsInRole(RocRole.Manager) || principal.IsInRole(RocRole.Admin);
            bool isSuperUser = principal.IsInRole(RocRole.Admin);

            btn_Admin.Enabled = isSuperUser;
            btn_Manager.Enabled = isPowerUser;
            btn_User.Enabled = isAuthorizedUser;
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            var firstName = txt_FirstName.Text;
            var lastName = txt_LastName.Text;
            var userName = txt_UserName;
            var client = cbClient.SelectedValue;

            // TODO show how to create user
            MessageBox.Show(@"show the result of creating user");
        }



        private void lnk_Logout_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO show us the out process and affection to other apps
            UpdateUI();
        }

        private void lnk_RedWebClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO you may change the url to suite your environment
            System.Diagnostics.Process.Start("http://localhost:53618/");
        }

        private void lnk_BlueWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO you may change the url to suite your environment
            System.Diagnostics.Process.Start("http://localhost:9654/Default.aspx/");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lst_UserGroups_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
