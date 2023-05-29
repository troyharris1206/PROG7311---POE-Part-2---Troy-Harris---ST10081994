<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FarmCentral._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Farm Central Web Application</h1>
        <p class="lead">Farm Central is a stock management web application that allows farmers to keep track of their stock.</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting to know your role</h2>
            <p>
                As a farmer user on the website, you will be able to add products that you are selling.
            </p>
            <p>
                As an employee user on the website, you will be able to add new farmers and view the list of products being sold.</p>
            <p>
                &nbsp;</p>
        </div>
        <div class="col-md-4">
            <h2>How to get started</h2>
            <p>
                If you have already registered an account, please navigate to the Login option and enter your credentials.</p>
            <p>
                Should you not yet have an account, as an employee, please register an account. As a farmer, please ask one of your logged-in employees to register you an account</p>
            <p>
                &nbsp;</p>
        </div>
    </div>

</asp:Content>
