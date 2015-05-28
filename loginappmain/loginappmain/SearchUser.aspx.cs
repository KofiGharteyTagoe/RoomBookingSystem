using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class SearchUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
   
        }

        protected void grd_getUserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "buttonView")
            {
                Response.Write("Button View Clicked");
                Session["rowIndex"] = e.CommandArgument;
                int indexOfRow = Convert.ToInt32(Session["rowIndex"]);    //Gets the argument and convert in to an int (row index) 
                string searchUserID = grd_getUserGridView.Rows[indexOfRow].Cells[1].Text;    //Store the EventID from the GridView to a string
                Session["searchedUserID"] = searchUserID;
                Response.Redirect("ViewTheUser.aspx");
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            string searchItem = ddl_search.Text;
            string searchValue = txt_searchItem.Text;
            searchValue.ToLower();


            getSearchedUsers search = new getSearchedUsers();
            DataTable getUsersData = search.getUsersInfo(searchItem, searchValue);

            DataTable tempGetUser = new DataTable();

            tempGetUser.Columns.AddRange(new DataColumn[8]
                {
                    
                
                new DataColumn("UserID", typeof(string)),
                new DataColumn("UserName", typeof(string)),
                new DataColumn("FullName", typeof(string)),
                new DataColumn("Email", typeof(string)),
                new DataColumn("IsLockedOut", typeof(string)),
                new DataColumn("AccessLvl", typeof(string)),
                new DataColumn("ContactNumber", typeof(string)),
                new DataColumn("viewButton",typeof(Button))
                });

            foreach (DataRow row in getUsersData.Rows) // For each row in the main table
            {
                DataRow tempRow = tempGetUser.NewRow(); // Make a new row

                try
                {

                    string AccessType = null;
                    tempRow[0] = row["UserID"].ToString();
                    tempRow[1] = row["UserName"].ToString();
                    tempRow[2] = row["Title"].ToString() + " " + row["ForeName"].ToString() + " " + row["SurName"].ToString();
                    tempRow[3] = row["Email"].ToString();
                    tempRow[4] = row["IsLockedOut"].ToString();


                    if (row["AccessLvl"].ToString().Equals("1"))
                    {
                        AccessType = "Admin";
                    }

                    else if (row["AccessLvl"].ToString().Equals("2"))
                    {
                        AccessType = "Novus Team";
                    }

                    else if (row["AccessLvl"].ToString().Equals("3"))
                    {
                        AccessType = "Trainee";
                    }
                    else if (row["AccessLvl"].ToString().Equals("4"))
                    {
                        AccessType = "Non Novus Staff";
                    }
                    tempRow[5] = AccessType;
                    tempRow[6] = row["ContactNumber"].ToString();

                }
                catch (Exception)
                {
                    
                    throw;
                }

                tempGetUser.Rows.Add(tempRow);
            }
            grd_getUserGridView.DataSource = tempGetUser; // Select the datasource for the gridview
            grd_getUserGridView.DataBind(); //Bind the data to the gridview
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
    }
}