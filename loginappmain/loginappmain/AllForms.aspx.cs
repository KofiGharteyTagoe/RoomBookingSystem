using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class AllForms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
               // Response.Redirect("LoginPage.aspx");
            }
            else
            {
                int userId = (int)Session["UserId"];
                DataTable userInfo = new DataTable();
                
                userInfo = DatabaseConnections.GetUser(userId);
                foreach (DataRow row in userInfo.Rows)
                {
                    string fullname = row.Field<string>("FullName");
                    string username = row.Field<string>("UserName");
                    string email = row.Field<string>("Email");
                    string ContactNo = row.Field<string>("ContactNumber");
                    string lastlogindate = Convert.ToString(row.Field<DateTime>("LastLoginDate"));


                    lastlogindate.ToString();
                    
                    //Email_label.Text = email;
                    Session["User"] = fullname;
                    lbl_fullname.Text = fullname;
                  //  LastOn_label.Text = lastlogindate;
                    //Contact_label.Text = ContactNo;

                    int accessLevel = (row.Field<int>("AccessLvl"));

                    if (accessLevel != 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "List", "<script language=javascript>hideListItem();</script>");
                    }
                }

            }

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserId"]; 
            DatabaseConnections.Logout(userId);

            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Session["UserId"] = null;
            Response.Redirect("LoginPage.aspx");
        }
    }
}