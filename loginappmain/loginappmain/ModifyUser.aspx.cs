using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class ModifyUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                int userId = (int)Session["UserId"];
              
            }
        }

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

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            int accessLevel = Convert.ToInt32(ddl_accessLvl.Text);
            try
            {
                DatabaseConnections.CreateUser(txt_userName.Text, ddl_title.Text, txt_firstName.Text, txt_lastName.Text, txt_email.Text, txt_contactNumber.Text, accessLevel, Convert.ToDateTime(txtDatePicker));
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            txt_firstName.Text = "";
            txt_lastName.Text = "";
            txt_userName.Text = "";
            txt_email.Text = "";
            txt_contactNumber.Text = "";
        }

        protected void Search(object sender, EventArgs e)
        {
            userDataGrid.Visible = false;
            searchDataGrid.Visible = true;

            if (txt_searchUserName.Text.Equals(""))
            {
                userDataGrid.Visible = true;
                searchDataGrid.Visible = false;
            }
        }

    }
}