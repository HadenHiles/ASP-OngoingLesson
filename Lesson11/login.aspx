<%@ Page Title="Login" Language="C#" MasterPageFile="~/lesson.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Lesson11.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Login</h1>
    <div class="form-group-lg">
        <asp:Label ID="lblStatus" runat="server" CssClass="label label-danger"></asp:Label>
    </div>
    <div class="form-group">
        <label for="txtUsername" class="col-sm-2">Username:</label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtPassword" class="col-sm-2">Password:</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
    </div>
</asp:Content>
