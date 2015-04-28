namespace Green
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lnk_RedWeb = new System.Windows.Forms.LinkLabel();
            this.lnk_BlueWeb = new System.Windows.Forms.LinkLabel();
            this.lnk_Logout = new System.Windows.Forms.LinkLabel();
            this.btn_Login = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_User = new System.Windows.Forms.Button();
            this.btn_Manager = new System.Windows.Forms.Button();
            this.btn_Admin = new System.Windows.Forms.Button();
            this.txt_Saml = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbClient = new System.Windows.Forms.ComboBox();
            this.btn_Create = new System.Windows.Forms.Button();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.txt_LastName = new System.Windows.Forms.TextBox();
            this.txt_FirstName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StatusBar = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Green Desktop App";
            // 
            // lnk_RedWeb
            // 
            this.lnk_RedWeb.AutoSize = true;
            this.lnk_RedWeb.Location = new System.Drawing.Point(667, 56);
            this.lnk_RedWeb.Name = "lnk_RedWeb";
            this.lnk_RedWeb.Size = new System.Drawing.Size(78, 13);
            this.lnk_RedWeb.TabIndex = 1;
            this.lnk_RedWeb.TabStop = true;
            this.lnk_RedWeb.Text = "RED Web App";
            this.lnk_RedWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_RedWebClicked);
            // 
            // lnk_BlueWeb
            // 
            this.lnk_BlueWeb.AutoSize = true;
            this.lnk_BlueWeb.Location = new System.Drawing.Point(766, 56);
            this.lnk_BlueWeb.Name = "lnk_BlueWeb";
            this.lnk_BlueWeb.Size = new System.Drawing.Size(76, 13);
            this.lnk_BlueWeb.TabIndex = 1;
            this.lnk_BlueWeb.TabStop = true;
            this.lnk_BlueWeb.Text = "Blue Web App";
            this.lnk_BlueWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_BlueWeb_LinkClicked);
            // 
            // lnk_Logout
            // 
            this.lnk_Logout.AutoSize = true;
            this.lnk_Logout.Location = new System.Drawing.Point(868, 56);
            this.lnk_Logout.Name = "lnk_Logout";
            this.lnk_Logout.Size = new System.Drawing.Size(40, 13);
            this.lnk_Logout.TabIndex = 1;
            this.lnk_Logout.TabStop = true;
            this.lnk_Logout.Text = "Logout";
            this.lnk_Logout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_Logout_Clicked);
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(826, 16);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "(user name)";
            // 
            // btn_User
            // 
            this.btn_User.Enabled = false;
            this.btn_User.Location = new System.Drawing.Point(18, 94);
            this.btn_User.Name = "btn_User";
            this.btn_User.Size = new System.Drawing.Size(75, 23);
            this.btn_User.TabIndex = 4;
            this.btn_User.Text = "User";
            this.btn_User.UseVisualStyleBackColor = true;
            // 
            // btn_Manager
            // 
            this.btn_Manager.Enabled = false;
            this.btn_Manager.Location = new System.Drawing.Point(119, 94);
            this.btn_Manager.Name = "btn_Manager";
            this.btn_Manager.Size = new System.Drawing.Size(75, 23);
            this.btn_Manager.TabIndex = 4;
            this.btn_Manager.Text = "Manager";
            this.btn_Manager.UseVisualStyleBackColor = true;
            // 
            // btn_Admin
            // 
            this.btn_Admin.Enabled = false;
            this.btn_Admin.Location = new System.Drawing.Point(221, 94);
            this.btn_Admin.Name = "btn_Admin";
            this.btn_Admin.Size = new System.Drawing.Size(75, 23);
            this.btn_Admin.TabIndex = 4;
            this.btn_Admin.Text = "Admin";
            this.btn_Admin.UseVisualStyleBackColor = true;
            // 
            // txt_Saml
            // 
            this.txt_Saml.Location = new System.Drawing.Point(18, 138);
            this.txt_Saml.Multiline = true;
            this.txt_Saml.Name = "txt_Saml";
            this.txt_Saml.Size = new System.Drawing.Size(486, 507);
            this.txt_Saml.TabIndex = 5;
            this.txt_Saml.Text = "Please login to view token contents.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbClient);
            this.groupBox1.Controls.Add(this.btn_Create);
            this.groupBox1.Controls.Add(this.txt_UserName);
            this.groupBox1.Controls.Add(this.txt_LastName);
            this.groupBox1.Controls.Add(this.txt_FirstName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(532, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 188);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create User";
            // 
            // cbClient
            // 
            this.cbClient.FormattingEnabled = true;
            this.cbClient.Items.AddRange(new object[] {
            "Hospital System A",
            "Hospital System B",
            "Default Tenant"});
            this.cbClient.Location = new System.Drawing.Point(117, 122);
            this.cbClient.Name = "cbClient";
            this.cbClient.Size = new System.Drawing.Size(235, 21);
            this.cbClient.TabIndex = 3;
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(105, 159);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(75, 23);
            this.btn_Create.TabIndex = 2;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(117, 86);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(235, 20);
            this.txt_UserName.TabIndex = 1;
            // 
            // txt_LastName
            // 
            this.txt_LastName.Location = new System.Drawing.Point(117, 58);
            this.txt_LastName.Name = "txt_LastName";
            this.txt_LastName.Size = new System.Drawing.Size(235, 20);
            this.txt_LastName.TabIndex = 1;
            // 
            // txt_FirstName
            // 
            this.txt_FirstName.Location = new System.Drawing.Point(117, 30);
            this.txt_FirstName.Name = "txt_FirstName";
            this.txt_FirstName.Size = new System.Drawing.Size(235, 20);
            this.txt_FirstName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Client:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "User Name/email:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Last Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "First Name:";
            // 
            // StatusBar
            // 
            this.StatusBar.AutoSize = true;
            this.StatusBar.Location = new System.Drawing.Point(18, 55);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(0, 13);
            this.StatusBar.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "label6";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 659);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_Saml);
            this.Controls.Add(this.btn_Admin);
            this.Controls.Add(this.btn_Manager);
            this.Controls.Add(this.btn_User);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.lnk_Logout);
            this.Controls.Add(this.lnk_BlueWeb);
            this.Controls.Add(this.lnk_RedWeb);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Green App";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnk_RedWeb;
        private System.Windows.Forms.LinkLabel lnk_BlueWeb;
        private System.Windows.Forms.LinkLabel lnk_Logout;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_User;
        private System.Windows.Forms.Button btn_Manager;
        private System.Windows.Forms.Button btn_Admin;
        private System.Windows.Forms.TextBox txt_Saml;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.TextBox txt_LastName;
        private System.Windows.Forms.TextBox txt_FirstName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbClient;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label StatusBar;
        private System.Windows.Forms.Label label6;
    }
}

