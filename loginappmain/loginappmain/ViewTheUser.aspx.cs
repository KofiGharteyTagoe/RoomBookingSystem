using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class ViewTheUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            

            string search = (string)Session["searchedUserID"];
            int searchedUserID = Convert.ToInt32(search);
            getSearchedUsers getInfo = new getSearchedUsers();


            DataTable DisplayUserInfo = getInfo.getInfoOnUser(searchedUserID); // Get User Personal Information and Display in GridView

            foreach (DataRow row in DisplayUserInfo.Rows)
            {
                string UserID = Convert.ToString(row.Field<int>("UserID"));
                string UserName = row["Username"].ToString();
                string FullName = row.Field<string>("Title") + " " + row.Field<string>("ForeName") + " " + row.Field<string>("SurName");
                string Email = row.Field<string>("Email");
                string CreateDate = Convert.ToString(row.Field<DateTime>("CreateDate"));
                string LastLoginDate = Convert.ToString(row.Field<DateTime>("LastLoginDate"));
                int IsLockedOut = Convert.ToInt32(row["IsLockedOut"]);
                int AccessLvl = row.Field<int>("AccessLvl");
                string ContactNumber = row.Field<string>("ContactNumber");
                int IsApproved = Convert.ToInt32(row["IsApproved"]);
                string expiryDate = row["ExpiryDate"].ToString();

                string Lockedout=  Convert.ToString(IsLockedOut);
                string AccessLevel = Convert.ToString(AccessLvl);
                string isApproved = Convert.ToString(IsApproved);
                string AccessCompare = null;
                string ApprovedCompare = null;
                lbl_userID.Text = UserID;
                lbl_UserName.Text = UserName;
                lbl_Fullnamelbl.Text = FullName;
                lbl_email.Text = Email;
                lbl_createDate.Text = CreateDate;
                lbl_lastlogin.Text = LastLoginDate;
                lbl_expiryDate.Text = expiryDate;

                if (AccessLevel.Equals("1"))
                {
                    AccessCompare = "Admin";

                }
                else if (AccessLevel.Equals("2"))
                {
                    AccessCompare = "Novus Team";
                }

                else if (AccessLevel.Equals("3"))
                {
                    AccessCompare = "Trainee";
                }
                else if (AccessLevel.Equals("4"))
                {
                    AccessCompare = "Non Novus Staff";
                }

                lbl_AccessLevel.Text = AccessCompare;
                lbl_contactnumber.Text = ContactNumber;

                if (isApproved.Equals("0"))
                {
                    ApprovedCompare = "No";
                }
                else if (isApproved.Equals("1"))
                {
                    ApprovedCompare = "Yes";
                }

                lbl_isApproved.Text = ApprovedCompare;
                lbl_lockedout.Text = Lockedout;
            }

            DataTable DisplayUserBookings = getInfo.getBookingInfo(searchedUserID); // Get User Booking Information and Display in GridView
            DataTable tempDisplayBook = new DataTable();

            tempDisplayBook.Columns.AddRange(new DataColumn[9]
                {
                
                new DataColumn("EventID", typeof(string)),
                new DataColumn("Room", typeof(string)),
                new DataColumn("Date", typeof(string)),
                new DataColumn("StartTime", typeof(string)),
                new DataColumn("EndTime", typeof(string)),
                new DataColumn("PcBooking", typeof(bool)),
                new DataColumn("PcsBooked", typeof(string)),
                new DataColumn("Description", typeof(string)),
                new DataColumn("deleteButton",typeof(Button))
                
                });

            foreach (DataRow row in DisplayUserBookings.Rows) // For each row in the main table
            {
                DataRow tempRow = tempDisplayBook.NewRow(); // Make a new row

                try
                {
                    DateTime startTime = DateTime.Parse(row["StartDateTime"].ToString());
                    DateTime endTime = DateTime.Parse(row["EndDateTime"].ToString());
                    int id = Int32.Parse(row["RoomID"].ToString());

                    tempRow[0] = row["EventID"].ToString();
                   
                    tempRow[2] = startTime.ToShortDateString();
                    tempRow[3] = startTime.ToShortTimeString();
                    tempRow[4] = endTime.ToShortTimeString();
                    tempRow[5] = row["PcBooking"];
                    tempRow[6] = row["PcsBooked"];
                    tempRow[7] = row["Description"].ToString();
                    try
                    {
                        tempRow[1] = DatabaseConnections.GetRoomName(id);
                    }
                    catch
                    {
                        Session["Disconnected"] = true;
                        Response.Redirect("LoginPage.aspx");
                    }
                }
                catch
                {

                    continue;
                }
                tempDisplayBook.Rows.Add(tempRow);

            }
            grd_getEventGridView.DataSource = tempDisplayBook; // Select the datasource for the gridview
            grd_getEventGridView.DataBind(); //Bind the data to the gridview
        }

        protected void grd_getEventGridView_RowCommand(Object sender, GridViewCommandEventArgs e)  //Function that runs when a button is clicked
        {

            if (e.CommandName == "buttonDelete")    //If Delete is clicked
            {
                Session["rowIndex"] = e.CommandArgument;
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>ShowPopup();</script>");
            }
        }

        protected void yesButton(object sender, EventArgs e)
        {
            deleteEntry();
        }

        protected void noButton(object sender, EventArgs e)
        {

        }

        private void RecreateTable()
        {
            string search = (string)Session["searchedUserID"];
            int searchedUserID = Convert.ToInt32(search);
            getSearchedUsers getInfo = new getSearchedUsers();

            DataTable DisplayUserBookings = getInfo.getBookingInfo(searchedUserID); // Get User Booking Information and Display in GridView
            DataTable tempDisplayBook = new DataTable();

            tempDisplayBook.Columns.AddRange(new DataColumn[7]
                {
                
                new DataColumn("EventID", typeof(string)),
                new DataColumn("RoomID", typeof(string)),
                new DataColumn("StartDateTime", typeof(string)),
                new DataColumn("EndDateTime", typeof(string)),
                new DataColumn("PcBooking", typeof(bool)),
                new DataColumn("Description", typeof(string)),
                new DataColumn("deleteButton",typeof(Button))
                
                });

            foreach (DataRow row in DisplayUserBookings.Rows) // For each row in the main table
            {
                DataRow tempRow = tempDisplayBook.NewRow(); // Make a new row

                try
                {
                    tempRow[0] = row["EventID"].ToString();
                    tempRow[1] = row["RoomID"].ToString();
                    tempRow[2] = row["StartDateTime"].ToString();
                    tempRow[3] = row["EndDateTime"].ToString();
                    tempRow[4] = row["PcBooking"];
                    tempRow[5] = row["Description"].ToString();

                }
                catch (Exception)
                {

                    throw;
                }
                tempDisplayBook.Rows.Add(tempRow);

            }
            grd_getEventGridView.DataSource = tempDisplayBook; // Select the datasource for the gridview
            grd_getEventGridView.DataBind(); //Bind the data to the gridview
        }

        protected void deleteEntry()
        {
            int indexOfRow = Convert.ToInt32(Session["rowIndex"]);    //Gets the argument and convert in to an int (row index) 
            string eventIDString = grd_getEventGridView.Rows[indexOfRow].Cells[1].Text;    //Store the EventID from the GridView to a string
            int eventIDNumber = Convert.ToInt32(eventIDString);    //Convert the string to an int
            try
            {
                DatabaseConnections.DeleteBooking(eventIDNumber);    //Delete the booking matching the EventId number
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            RecreateTable();//Reload the table after booking is deleted
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

