<%@ Page Title="Register" Language="C#" MasterPageFile="~/lesson.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Lesson11.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Register</h1>
    <h6>All fields are required.</h6>

    <div class="form-group-lg">
        <asp:Label ID="lblStatus" runat="server" CssClass="label label-info"></asp:Label>
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
        <label for="txtConfirm" class="col-sm-2">Confirm Password:</label>
        <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password"></asp:TextBox>
        <asp:CompareValidator runat="server" 
            ControlToValidate="txtPassword"
            ControlToCompare="txtConfirm"
            Operator="Equal"
            ErrorMessage="Passwords must match!"
            CssClass="label label-danger"
        ></asp:CompareValidator>
    </div>
    <div class="form-group">
        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
    </div>
</asp:Content>
