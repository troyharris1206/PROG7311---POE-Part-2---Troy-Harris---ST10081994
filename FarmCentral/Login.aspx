<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FarmCentral.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="StyleSheet1.css">
      <style type="text/css">
          .auto-style1 {
              width: 43%;
          }
      </style>
  <div class="wrapper">
    <h2>Log into your Farm Central account</h2>
      <div class="input-box">
        <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter your username" required="true"></asp:TextBox>
      </div>       
      <br />
      <div class="input-box">
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter your password" required="true"></asp:TextBox>
      </div>
      <br />
      <div class="input-box button">       
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
      </div>
      <br />
      <div class="text">
        <asp:Label ID="lblLoginError" runat="server" Text="Incorrect login credentials. Please try again." ForeColor="Red"></asp:Label>
      </div>
      <div class="text">
        <h3>Don't have an account? <a href="Register">Register now</a></h3>
      </div>
      <div class="text">
        <h3>Forgot your password? <a href="ForgotPassword">Reset password now</a></h3>
      </div>
  </div>
</asp:Content>
