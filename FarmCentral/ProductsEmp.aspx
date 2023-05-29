<%@ Page Title="Products" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="ProductsEmp.aspx.cs" Inherits="FarmCentral.ProductsEmp" %>

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
        <h2>View Farmer products</h2>
        <h3>View all products added</h3>
        <div class="input-box button">
            <asp:Button ID="btnViewAll" runat="server" Text="View All Products" OnClick="btnViewAll_Click" />
        </div> 
        <h3>Filter products by Farmer</h3>
        <div class="input-box">
            <asp:TextBox ID="txtFarmer" runat="server" placeholder="Enter a Farmer's UserID"></asp:TextBox>
        </div>
        <br />
        <div class="input-box button">
            <asp:Button ID="btnFarmerFilter" runat="server" Text="Filter By Farmer" OnClick="btnFarmerFilter_Click" />
        </div>
        <h3>Filter products by dates added by the Farmer above</h3>
        <div class="input-box">
            <asp:TextBox ID="txtFirstDate" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtSecondDate" runat="server" TextMode="Date"></asp:TextBox>
        </div>
        <br />
        <div class="input-box button">
            <asp:Button ID="btnDateFilter" runat="server" Text="Filter By Date" OnClick="btnDateFilter_Click" />
        </div>    
        <h3>Filter products by product type added by the Farmer above</h3>
        <div class="input-box">
            <asp:TextBox ID="txtProductType" runat="server" placeholder="Enter a product type"></asp:TextBox>
        </div>
        <br />
        <div class="input-box button">
            <asp:Button ID="btnProductTypeFilter" runat="server" Text="Filter By Product Type" OnClick="btnProductTypeFilter_Click" />
        </div>
    </div>
    <h2>Results</h2>
        <div>
            <asp:GridView ID="ProductsView" runat="server" CellPadding="10" Width="100%" ViewStateMode="Enabled" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#000000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
</asp:Content>

