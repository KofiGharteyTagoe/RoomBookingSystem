using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class AdminUser : System.Web.UI.Page
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

                    string fullname = firstname + " " + lastname;
                    Session["User"] = fullname;     

                    int accessLevel = (row.Field<int>("AccessLvl"));

                    if (accessLevel != 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "List", "<script language=javascript>hideListItem();</script>");
                    }
                }
            }
      }

        private bool CheckPassword(string password, int userID)
        {
           MD5hashcreate compare = new MD5hashcreate();
           string dbPass = DatabaseConnections.GetPassword(userID);
           return compare.compareHash(password, dbPass);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = txt_emailbox.Text;
            string contactNumber = txt_contactbox.Text;
            string password = txt_verifybox.Text;
            int userId = (int)Session["UserId"];

            if (!CheckPassword(password, userId))
            {
                ShowError("Invalid password");
                return;
            }

            if (email.Length>0)
            {    
               DatabaseConnections.EditEmail(userId, email);
               ShowError("Your details have been changed");               
            }
            else
                ShowError("Please enter an email address");
           
            if (contactNumber.Length>0)
            {               
                DatabaseConnections.EditContactNumber(userId, contactNumber);
                ShowError("Your contact number has been changed");

            }
            else
               ShowError("Please enter a new number");
              
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

        protected void btn_changepassword_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserId"];
            Page.Validate("changepass");

            if (!Page.IsValid)
            {
                return;
            }

            string oldPassword = txt_oldpass.Text;
            string newPassword = txt_newpass.Text;
            string vpassword = txt_verifybox.Text;
            
            MD5hashcreate createMD5 = new MD5hashcreate();
            string hashPassword = createMD5.createMd5hash(newPassword);

            DatabaseConnections.EditPassword(userId, hashPassword);
            ShowError("Your password has been changed");
        }

        private void ShowError(string message)
        {
            confirmation.Text = message;
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>ShowPopup();</script>");            
        }

        protected void okayButton(object sender, EventArgs e)
        {
        }
    }
}

