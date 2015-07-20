<%@ Page Title="" Language="C#" MasterPageFile="~/Lesson.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="Lesson11.student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Student Details</h1>
    <h5>All fields are required</h5>
    <div class="form-group">
        <label for="txtFirstMidName" class="col-sm-3">First Name:</label>
        <asp:TextBox ID="txtFirstMidName" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Please Enter your first name!"
            CssClass="label label-danger"
            ControlToValidate="txtFirstMidName"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <label for="txtLastName" class="col-sm-3">Last Name:</label>
        <asp:TextBox ID="txtLastName" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="Please Enter your last name!"
            CssClass="label label-danger"
            ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <label for="txtEnrollmentDate" class="col-sm-3">Enrollment Date:</label>
        <asp:TextBox ID="txtEnrollmentDate" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="Please Enter an enrollment date!"
            CssClass="label label-danger"
            ControlToValidate="txtEnrollmentDate"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" ErrorMessage="Please enter a date in the following format: YYYY-MM-DD" 
                ControlToValidate="txtEnrollmentDate" ValidationExpression="^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$" CssClass="label label-danger"></asp:RegularExpressionValidator>
    </div>
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    <br />
    <asp:Panel ID="pnlCourses" runat="server" Visible="false">
        <h2>Courses</h2>
        <asp:GridView ID="grdCourses" runat="server" CssClass="table table-striped"
            AutoGenerateColumns="false"
            OnRowDeleting="grdCourses_RowDeleting"
            DataKeyNames="EnrollmentID">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Department" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderText="Delete" />
            </Columns>
        </asp:GridView>
        <h2>Add Enrollment</h2>
        <table class="table table-striped table-hover">
            <thead>
                <th>Department</th>
                <th>Title</th>
                <th>Add</th>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlDepartments" runat="server"
                            DataTextField="Name" 
                            DataValueField="DepartmentID"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RangeValidator runat="server"
                            ControlToValidate="ddlDepartments"
                            Type="Integer"
                            MinimumValue="1"
                            MaximumValue="9999999"
                            ErrorMessage="Required"
                            CssClass="label label-danger"></asp:RangeValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCourse" runat="server"
                            DataTextField="Title" 
                            DataValueField="CourseID">
                        </asp:DropDownList>
                        <asp:RangeValidator runat="server"
                            ControlToValidate="ddlCourse"
                            Type="Integer"
                            MinimumValue="1"
                            MaximumValue="9999999"
                            ErrorMessage="Required"
                            CssClass="label label-danger"></asp:RangeValidator>
                    </td>
                    <td><asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" /></td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
</asp:Content>
