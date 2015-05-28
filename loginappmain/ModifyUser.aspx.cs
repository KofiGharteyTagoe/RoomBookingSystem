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

            if (txt_userName.Text.Length < 1) { ShowError("Please enter a username"); return;}
            if (txt_firstName.Text.Length < 1) { ShowError("Please enter the users first name"); return; }
            if (txt_email.Text.Length < 1) { ShowError("Please enter the users email"); return; }
            if (txt_contactNumber.Text.Length < 1) { ShowError("Please enter the users contact number"); return; }
            if (txtDatePicker.Text.Length < 1) { ShowError("Please enter the users expiry date in DD/MM/YYYY"); return; }

            DateTime expiryDate = DateTime.Now;
            try
            {
                string date = txtDatePicker.Text + "  00:00:00";
                expiryDate = DateTime.Parse(date);
            }
            catch
            {
                ShowError("Please enter a valid expiry date in DD/MM/YYYY format");
               return;
            }
            try
            {
                
                DatabaseConnections.CreateUser(txt_userName.Text, ddl_title.Text, txt_firstName.Text, txt_lastName.Text, txt_email.Text, txt_contactNumber.Text, accessLevel, expiryDate);
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

        private void ShowError(string message)
        {
            confirmation.Text = message;
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>ShowPopup();</script>");
        }

    }
}