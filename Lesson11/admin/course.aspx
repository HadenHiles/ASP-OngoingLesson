<%@ Page Title="" Language="C#" MasterPageFile="~/Lesson.Master" AutoEventWireup="true" CodeBehind="course.aspx.cs" Inherits="Lesson11.course" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Course Details</h1>
    <h5>All fields are required</h5>
    <div class="form-group">
        <label for="txtFirstMidName" class="col-sm-3">Title:</label>
        <asp:TextBox ID="txtTitle" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Please Enter a title!"
            CssClass="label label-danger"
            ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <label for="txtCredits" class="col-sm-3">Credits:</label>
        <asp:TextBox ID="txtCredits" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="Please Enter the number of credits!"
            CssClass="label label-danger"
            ControlToValidate="txtCredits"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <label for="ddlDepartments" class="col-sm-3">Department:</label>
        <asp:DropDownList ID="ddlDepartments" runat="server" DataTextField="Name" DataValueField="DepartmentID"></asp:DropDownList>
    </div>
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    <br />
</asp:Content>
