<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Blue.Account.Login" ValidateRequest="false" %>

<%@ Register assembly="Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="Microsoft.IdentityModel.Web.Controls" TagPrefix="wif" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>

<%--This is how you would initiate federated login on the Login.aspx page if you wanted the user to click a button.--%>
<%--    <div>
        <wif:FederatedPassiveSignIn ID="AzureADAuth" runat="server"
            Issuer ="https://login.microsoftonline.com/60ffe505-3ef1-47e2-8553-2bbe12dd494f/wsfed"
            Realm="http://localhost:9654/"
            Reply="http://localhost:9654/Account/Login.aspx"
            VisibleWhenSignedIn="false"
            OnSignInError="AzureADAuth_SignInError"
            RequireHttps="false">
        </wif:FederatedPassiveSignIn>
    </div>

    <br />
    <asp:Label ID="SignInError" runat="server" Text="Label" Visible="false"></asp:Label>--%>

</asp:Content>
