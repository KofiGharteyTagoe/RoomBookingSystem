using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // if (Session["UserID"] != null)
          //      Response.Redirect("HomePage.aspx");
          //  else
                Response.Redirect("LoginPage.aspx");
        }
    }
}