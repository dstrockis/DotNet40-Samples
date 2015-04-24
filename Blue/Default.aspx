<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blue._Default" %>



<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <asp:Button ID="btn_User" runat="server" Text="User" />
    <asp:Button ID="btn_Manager" runat="server" Text="Manager" />
    <asp:Button ID="btn_Admin" runat="server" Text="Admin" />

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                <asp:TextBox ID="txtSaml" runat="server" Height="500px" ReadOnly="True" Width="500px" TextMode="MultiLine" style="resize:none"></asp:TextBox></td>
            <td style="vertical-align: top">
                <fieldset>
                    <legend>Create User:</legend>
                    <label class="form_label">First Name:</label><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    
                    <label class="form_label">Last Name:</label><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                    
                    <label class="form_label">Email/Username:</label><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    <label class="form_label">Client:</label><asp:DropDownList ID="ddlClient" runat="server">
                        <asp:ListItem>Hospital System A</asp:ListItem>
                        <asp:ListItem>Hospital System B</asp:ListItem>
                    </asp:DropDownList>
                    
&nbsp;<label class="form_label"></label><asp:Button ID="btn_Create" runat="server" Text="Create" OnClick="btn_Create_Click" />
                </fieldset>
                
                <table width="100%"><tr><td><label>Available Groups/Roles</label></td><td style="text-align: right"><asp:Button ID="btn_getUserGroup" runat="server" Text="Refresh" OnClick="btn_getUserGroup_Click" /></td></tr></table>
                
                <asp:ListBox ID="lst_UserGroups" runat="server" Width="100%" Height="203px"></asp:ListBox>
            </td>

        </tr>
    </table>


</asp:Content>
