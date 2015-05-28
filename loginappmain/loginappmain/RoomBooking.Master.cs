using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class RoomBooking : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    int userId = (int)Session["UserId"];
                    DataTable userInfo = new DataTable();

                    try
                    {
                        userInfo = DatabaseConnections.GetUser(userId);
                    }
                    catch
                    {
                        Session["Disconnected"] = true;
                        Response.Redirect("LoginPage.aspx");
                    }
                    foreach (DataRow row in userInfo.Rows)
                    {
                        string firstname = row.Field<string>("ForeName");
                        string lastname = row.Field<string>("SurName");
                        string username = row.Field<string>("UserName");
                        string email = row.Field<string>("Email");
                        string ContactNo = row.Field<string>("ContactNumber");
                        string lastlogindate = Convert.ToString(row.Field<DateTime>("LastLoginDate"));


                        lastlogindate.ToString();
                        string fullname = firstname + " " + lastname;
                        //Email_label.Text = email;
                        Session["User"] = fullname;
                        lbl_fullname.Text = fullname;
                        //  LastOn_label.Text = lastlogindate;
                        //Contact_label.Text = ContactNo;

                        int accessLevel = (row.Field<int>("AccessLvl"));

                        if (accessLevel != 1)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "List", "<script language=javascript>hideListItem();</script>");
                        }
                    }
                }
            }
        }


        /* Logout Function*/

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserId"];
            try
            {
                DatabaseConnections.Logout(userId);
            }
            catch { /* Normally we'd redirect the user to the LoginPage if we can't connect to the DB but they're logging our anyways..."*/ }

            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Session["UserId"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        protected void okayButton(object sender, EventArgs e)
        { 
        }


    }
}