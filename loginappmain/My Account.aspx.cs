using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class My_Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
    
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = txt_emailbox.Text;
            string contactNumber = txt_contactbox.Text;
            string vpassword = txt_verifybox.Text;
            MD5hashcreate compare = new MD5hashcreate();
            int userId = (int)Session["UserId"];
            string dbPass = "";

            try
            {
                dbPass = DatabaseConnections.GetPassword(userId);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            if(!compare.compareHash(vpassword, dbPass))
            {
                ShowError("Invalid password");
                return;
            }

            if (email.Length>0)
            {
               try
               {
                    DatabaseConnections.EditEmail(userId, email);
               }
               catch
               {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
               }    
            }      

            else if (contactNumber.Length>0)
            {
                try
                {
                    DatabaseConnections.EditContactNumber(userId, contactNumber);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }                  
            }
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

            if (oldPassword.Equals(newPassword))
            {
                MD5hashcreate createMD5 = new MD5hashcreate();
                string hashPassword = createMD5.createMd5hash(newPassword);

                try
                {
                    DatabaseConnections.EditPassword(userId, hashPassword); 
                    ShowError("Your password has been changed");
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }
               
            }
            if (oldPassword == null || newPassword== null || vpassword == null)
            {
                return;
            }
                
        }

        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>ShowPopup();</script>");
            confirmation.Text = message;
        }

        protected void okayButton(object sender, EventArgs e)
        {
        }

    }
}