<%@ Page Title="Contoso University - Main Menu" Language="C#" MasterPageFile="~/lesson.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Lesson11.main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Main Menu</h1>
    <div class="container">
        <div class="well">
            <h3>Departments</h3>
            <ul class="list-group">
                <li class="list-group-item"><a href="/departments.aspx">List Departments</a></li>
                <li class="list-group-item"><a href="/department.aspx">Add Department</a></li>
            </ul>
        </div>
        <div class="well">
            <h3>Courses</h3>
            <ul class="list-group">
                <li class="list-group-item"><a href="/courses.aspx">List Courses</a></li>
                <li class="list-group-item"><a href="/course.aspx">Add Course</a></li>
            </ul>
        </div>
        <div class="well">
            <h3>Students</h3>
            <ul class="list-group">
                <li class="list-group-item"><a href="/students.aspx">List Students</a></li>
                <li class="list-group-item"><a href="/student.aspx">Add Student</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
