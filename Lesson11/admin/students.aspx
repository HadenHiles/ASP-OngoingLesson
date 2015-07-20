<%@ Page Title="" Language="C#" MasterPageFile="~/Lesson.Master" AutoEventWireup="true" CodeBehind="students.aspx.cs" Inherits="Lesson11.students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Students</h1>
    <a href="student.aspx">Add Student</a>
    <asp:GridView ID="grdStudents" runat="server" CssClass="table table-striped"
        AutoGenerateColumns="false" OnRowDeleting="grdStudents_RowDeleting"
        DataKeyNames="StudentID"
        AllowSorting="true"
        OnSorting="grdStudents_Sorted">
        <Columns>
            <asp:BoundField DataField="FirstMidName" SortExpression="FirstMidName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" SortExpression="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="EnrollmentDate" SortExpression="EnrollmentDate" HeaderText="Enrollment Date (m/d/y)" DataFormatString="{0:d}" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="student.aspx" 
                 Text="Edit" DataNavigateUrlFields="StudentID"
                 DataNavigateUrlFormatString="student.aspx?StudentID={0}" />
            <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
