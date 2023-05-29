<%@ Page Title="Register Farmer" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="AddNewFarmer.aspx.cs" Inherits="FarmCentral.AddNewFarmer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="StyleSheet1.css">
      <style type="text/css">
          .auto-style1 {
              width: 43%;
          }
      </style>
    <div class="wrapper">
        <h2>Register a Farmer account for Farm Central</h2>
        <div class="input-box">
            <asp:TextBox ID="txtFirstName" runat="server" placeholder="Enter the Farmer's first name" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtSurname" runat="server" placeholder="Enter the Farmer's surname" required="true"></asp:TextBox>
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
            <asp:Button ID="btnSubmit" runat="server" Text="Register Farmer Account" OnClick="btnSubmit_Click" />
        </div>
    </div>
</asp:Content>

