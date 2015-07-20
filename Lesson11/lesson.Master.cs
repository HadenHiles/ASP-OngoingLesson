using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Identity/Owin references
using Microsoft.Owin.Security;

namespace Lesson11
{
    public partial class Lesson11 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var authenticatedUser = HttpContext.Current.User.Identity.IsAuthenticated;
            if (authenticatedUser)
            {
                plhPrivate.Visible = true;
                plhPublic.Visible = false;
            }
            else
            {
                plhPrivate.Visible = false;
                plhPublic.Visible = true;
            }
        }
    }
}