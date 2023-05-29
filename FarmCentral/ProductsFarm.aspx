<%@ Page Title="Add Product" Language="C#" MasterPageFile="~/Farmer.Master" AutoEventWireup="true" CodeBehind="ProductsFarm.aspx.cs" Inherits="FarmCentral.ProductsFarm" %>

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
        <h2>Add a new product</h2>
        <div class="input-box">
            <asp:TextBox ID="txtProductName" runat="server" placeholder="Enter the product name" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtProductPrice" runat="server" TextMode="Number" placeholder="Enter the product price" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtProductType" runat="server" placeholder="Enter the product type" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box">
            <asp:TextBox ID="txtStockAmount" runat="server" TextMode="Number" placeholder="Enter the stock amount" required="true"></asp:TextBox>
        </div>
        <br />
        <div class="input-box button">
            <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
        </div>
    </div>
</asp:Content>

