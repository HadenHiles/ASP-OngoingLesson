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
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fill the grid
            if (!IsPostBack)
            {
                GetDepartments();
            }
        }

        protected void GetDepartments()
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //use link to query the Departments model
                    var deps = from d in conn.Departments
                               select d;

                    //bind to the gridview
                    grdDepartments.DataSource = deps.ToList();
                    grdDepartments.DataBind();
                }
            }
            catch (System.IO.IOException e)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //get the Department Id
                    Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[e.RowIndex].Values["DepartmentID"]);

                    var d = (from dep in conn.Departments
                             where dep.DepartmentID == DepartmentID
                             select dep).FirstOrDefault();

                    //process the delete
                    conn.Departments.Remove(d);
                    conn.SaveChanges();

                    //update the grid
                    GetDepartments();
                }
            }
            catch (System.IO.IOException e2)
            {
                Server.Transfer("/error.aspx", true);
            }
        }
    }
}