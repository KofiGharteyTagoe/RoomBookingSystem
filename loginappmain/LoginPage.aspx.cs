using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace LoginAppmain
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Disconnected"] != null)
                lblSql.Text = "Sorry, we are having problems with connecting you to the system. Please check your connection or try again at a later time";

            string JQueryVer = "1.7.1";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-" + JQueryVer + ".min.js",
                DebugPath = "~/Scripts/jquery-" + JQueryVer + ".js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTb.Text;
            string Password = PasswordTb.Text;
            int result = 0, count = 0;

            try
            {
                result = DatabaseConnections.Login(username, Password);
            }
            catch
            {
                lblSql.Text = "Sorry, we are having problems with connecting you to the system";
            }

            if (result > 0) // If the result is above 0 then the log in was successful
            {
                Session["UserId"] = result;
                Response.Redirect("HomePage.aspx");
            }

            // If not display error      
            else if (result == DatabaseConnections.EXPIRED) // Account has expired
            {
                lblSql.Text = "Your account has expired.";
            }


            else if (result == DatabaseConnections.UNSUCCESSFUL) // User details does not match
            {
                lblSql.Text = "Username and Password not recognised!";

                // Get the count from the session
                try
                {
                    count = Convert.ToInt32(Session["count"].ToString());
                }
                catch
                {
                    // If count doesn't exist, use 0
                    count = 0;
                }
                // Store and increment the count
                Session["count"] = ++count;

                if (count > 4) // Lockout after too many attempts
                {
                    DatabaseConnections.Lockout(username);
                    lblSql.Text = "Your account has been locked. Contact Admin!";
                }
            }

            else // Other Errors
            {
                lblSql.Text = "Sorry, we are having problems with connecting you to the system";
            }

        }
    }
}