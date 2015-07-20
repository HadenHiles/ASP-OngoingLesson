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
    public partial class course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDepartments();
                if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    GetCourse();
                }
            }
        }

        protected void GetDepartments()
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    var deps = (from d in conn.Departments
                                orderby d.Name
                                select d);

                    //bind to the gridview
                    ddlDepartments.DataSource = deps.ToList();
                    ddlDepartments.DataBind();
                }
            }
            catch (System.IO.IOException e)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void GetCourse()
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                    //use link to query the Courses model
                    Courses objC = (from c in conn.Courses
                                    where c.CourseID == CourseID
                                    select c).FirstOrDefault();

                    //bind to the gridview
                    txtTitle.Text = objC.Title;
                    txtCredits.Text = objC.Credits.ToString();
                }
            }
            catch (System.IO.IOException e2)
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
                    Courses objC = new Courses();

                    if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                    {
                        Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
                        objC = (from c in conn.Courses
                                where c.CourseID == CourseID
                                select c).FirstOrDefault();
                    }

                    //Populate the course from the input form
                    objC.Title = txtTitle.Text;
                    objC.Credits = Convert.ToInt32(txtCredits.Text);
                    objC.DepartmentID = Convert.ToInt32(ddlDepartments.SelectedValue);

                    if (String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                    {
                        //add
                        conn.Courses.Add(objC);
                    }

                    conn.SaveChanges();
                    Response.Redirect("courses.aspx");
                }
            }
            catch (System.IO.IOException e3)
            {
                Server.Transfer("/error.aspx", true);
            }
        }
    }
}