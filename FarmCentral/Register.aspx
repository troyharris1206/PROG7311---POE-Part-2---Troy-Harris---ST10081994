<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FarmCentral.Register" %>

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
        <h2>Register an employee account for Farm Central</h2>
        <div class="input-box">
            <asp:TextBox ID="txtFirstName" runat="server" placeholder="Enter your first name" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtSurname" runat="server" placeholder="Enter your surname" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Create a username" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Create a password" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm password" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box button">
            <asp:Button ID="btnSubmit" runat="server" Text="Register Employee Account" OnClick="btnSubmit_Click" />
        </div>
        <div class="text">
            <h3>Already have an account? <a href="Login">Login now</a></h3>
        </div>
    </div>
</asp:Content>
