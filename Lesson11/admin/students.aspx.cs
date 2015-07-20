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
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fill the grid
            if (!IsPostBack)
            {
                GetStudents();
            }
        }

        protected void GetStudents()
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //use link to query the Students model
                    var studs = from s in conn.Students
                                select s;

                    //If there is a sort parameter then override the query
                    if (Request.QueryString.Count > 0)
                    {
                        studs = from s in conn.Students
                                orderby Request.QueryString["orderby"]
                                select s;
                    }

                    //bind to the gridview
                    grdStudents.DataSource = studs.ToList();
                    grdStudents.DataBind();
                }
            }
            catch (System.IO.IOException e)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //get the Department Id
                    Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[e.RowIndex].Values["StudentID"]);

                    var s = (from stud in conn.Students
                             where stud.StudentID == StudentID
                             select stud).FirstOrDefault();

                    //process the delete
                    conn.Students.Remove(s);
                    conn.SaveChanges();

                    //update the grid
                    GetStudents();
                }
            }
            catch (System.IO.IOException e2)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void grdStudents_Sorted(object sender, EventArgs e)
        {
            //Response.Redirect("students.aspx?orderby=");
            Response.Redirect("students.aspx");
        }
    }
}