using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference our entity framework models
using Lesson11.Models;
using System.Web.ModelBinding;

using System.Linq.Dynamic;

namespace Lesson11
{
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fill the grid
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "CourseID";
                GetCourses();
            }
        }

        protected void GetCourses()
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //use link to query the Courses model
                    var course = from c in conn.Courses
                                 select new { c.CourseID, c.Title, c.Credits, c.Department.Name };

                    //append the current direction of the sort collumn
                    String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                    grdCourses.DataSource = course.AsQueryable().OrderBy(Sort).ToList();
                    grdCourses.DataBind();
                }
            }
            catch (System.IO.IOException e)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //get the Department Id
                    Int32 CourseID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["CourseID"]);

                    var c = (from course in conn.Courses
                             where course.CourseID == CourseID
                             select course).FirstOrDefault();

                    //process the delete
                    conn.Courses.Remove(c);
                    conn.SaveChanges();

                    //update the grid
                    GetCourses();
                }
            }
            catch (System.IO.IOException e2)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void grdCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdCourses.PageIndex = e.NewPageIndex;
            GetCourses();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh the grid
            grdCourses.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetCourses();
        }

        protected void grdCourses_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Set the global sort collumn to the collumn that was clicked
            Session["SortColumn"] = e.SortExpression;
            GetCourses();

            //Toggle the sort direction
            if (Session["SortDirection"].ToString() == "ASC")
            {
                Session["SortDirection"] = "DESC";
            }
            else
            {
                Session["SortDirection"] = "ASC";
            }
        }

        protected void grdCourses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdCourses.Columns.Count - 1; i++)
                    {
                        if (grdCourses.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "/images/desc.jpg";
                                SortImage.AlternateText = "Sort Descending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "/images/asc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }

                            e.Row.Cells[i].Controls.Add(SortImage);

                        }
                    }
                }

            }
        }
    }
}