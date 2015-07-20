using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference our entity framework models
using Lesson11.Models;
using System.Web.ModelBinding;

namespace Lesson11
{
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page isn't posted back, check the url for an id to see know add or edit
            if (!IsPostBack)
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    //we have a url parameter if the count is > 0 so populate the form
                    GetStudent();
                }
            }
        }

        protected void GetStudent()
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //get id from url parameter and store in a variable
                    Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    var s = (from stud in conn.Students
                             where stud.StudentID == StudentID
                             select stud).FirstOrDefault();

                    //populate the form from our Student object
                    txtFirstMidName.Text = s.FirstMidName;
                    txtLastName.Text = s.LastName;
                    txtEnrollmentDate.Text = s.EnrollmentDate.ToString("yyyy-MM-dd");

                    var studentId = Convert.ToInt32(Request.QueryString["StudentID"]);

                    var objE = (from en in conn.Enrollments
                                join c in conn.Courses on en.CourseID equals c.CourseID
                                join d in conn.Departments on c.DepartmentID equals d.DepartmentID
                                where en.StudentID == studentId
                                select new { en.EnrollmentID, en.Grade, c.Title, d.Name });

                    grdCourses.DataSource = objE.ToList();
                    grdCourses.DataBind();

                    //Clear the dropdown
                    ddlDepartments.ClearSelection();
                    ddlCourse.ClearSelection();

                    var deps = (from d in conn.Departments
                                orderby d.Name
                                select d);

                    //bind to the ddl
                    ddlDepartments.DataSource = deps.ToList();
                    ddlDepartments.DataBind();

                    //Create a default option
                    ListItem defaultItem = new ListItem("--Select--", "0");
                    ddlDepartments.Items.Insert(0, defaultItem);
                    ddlCourse.Items.Insert(0, defaultItem);

                    pnlCourses.Visible = true;
                }
            }
            catch (System.IO.IOException e)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //instantiate a new deparment object in memory
                    Student s = new Student();

                    //decide if updating and then re-query the updated Students
                    if (Request.QueryString.Count > 0)
                    {
                        Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                        s = (from stud in conn.Students
                             where stud.StudentID == StudentID
                             select stud).FirstOrDefault();
                    }

                    //fill in the values
                    s.FirstMidName = txtFirstMidName.Text;
                    s.LastName = txtLastName.Text;
                    s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

                    //Only add a new record if it's not updating an existing one
                    if (Request.QueryString.Count == 0)
                    {
                        conn.Students.Add(s);
                    }

                    conn.SaveChanges();

                    //redirect to the Students page
                    Response.Redirect("students.aspx");
                }
            }
            catch (System.IO.IOException e2)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Get the desired enrollment ID
            Int32 EnrollmentID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["EnrollmentID"]);

            try
            {
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    Enrollment objE = (from en in conn.Enrollments
                                       where en.EnrollmentID == EnrollmentID
                                       select en).FirstOrDefault();

                    conn.Enrollments.Remove(objE);
                    conn.SaveChanges();
                    GetStudent();
                }
            }
            catch (System.IO.IOException e3)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //store the selected department ID
                    Int32 DepartmentID = Convert.ToInt32(ddlDepartments.SelectedValue);

                    var course = (from c in conn.Courses
                                  where c.DepartmentID == DepartmentID
                                  orderby c.Title
                                  select c);

                    //bind to the ddl
                    ddlCourse.DataSource = course.ToList();
                    ddlCourse.DataBind();

                    //Create a default option
                    ListItem defaultItem = new ListItem("--Select--", "0");
                    ddlCourse.Items.Insert(0, defaultItem);
                }
            }
            catch (System.IO.IOException e4)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //Get the values
                    Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);
                    Int32 CourseID = Convert.ToInt32(ddlCourse.SelectedValue);

                    //Instantiate the enrollment object
                    Enrollment objE = new Enrollment();

                    //Populate, save, and refresh
                    objE.StudentID = StudentID;
                    objE.CourseID = CourseID;
                    conn.Enrollments.Add(objE);
                    conn.SaveChanges();
                    GetStudent();
                }
            }
            catch (System.IO.IOException e5)
            {
                Server.Transfer("/error.aspx", true);
            }
        }
    }
}