<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="FarmCentral.ForgotPassword" %>

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
        <h2>Reset your password for your Farm Central account</h2>
        <div class="input-box">
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter your username" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtRecoveryKey" runat="server" placeholder="Enter your recovery key" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Create a new password" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm new password" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box button">
            <asp:Button ID="btnSubmit" runat="server" Text="Reset password" OnClick="btnSubmit_Click" />
        </div>
        <div class="text">
            <h3>Want to go back to login? <a href="Login">Login now</a></h3>
        </div>
    </div>
</asp:Content>
