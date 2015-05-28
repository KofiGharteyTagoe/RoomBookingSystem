using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace LoginAppmain
{
    public partial class getNewPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_resetPassword_Click(object sender, EventArgs e)
        {
            string UserName = txt_username.Text;
            string emailId = txt_emailAdd.Text;

            if (UserName.Length>0)
            {
                try
                {
                    DatabaseConnections.ResetPasswordbyUserName(UserName);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }
                
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Your details have been changed');", true);
                Response.Redirect("LoginPage.aspx");
            }
            else if (emailId.Length>0)
            {
                try
                {
                    DatabaseConnections.ResetPassword(emailId);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Your details have been changed');", true);
                Response.Redirect("LoginPage.aspx");
            }
        }


    }
}