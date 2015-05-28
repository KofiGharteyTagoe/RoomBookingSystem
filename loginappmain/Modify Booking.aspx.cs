using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class Modify_Booking : System.Web.UI.Page
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
                if (!this.IsPostBack)
                {
                    CreateTable();
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

        protected void BookingsTable_RowCommand(Object sender, GridViewCommandEventArgs e)  //Function that runs when a button is clicked
        {
            if (e.CommandName == "buttonEdit")    //If Edit it clicked
            {
                Response.Write("Edit Clicked");
                int index = 0, id=0;
                try
                {
                    index = Int32.Parse(e.CommandArgument.ToString());
                }
                catch { return; }
                try
                {
                    string val = BookingsTable.Rows[index].Cells[5].Text.ToString();
                    id = Int32.Parse(val);
                }
                catch { return;  }
                DateTime date = DateTime.Now;
                try
                {
                    string dates = BookingsTable.Rows[index].Cells[1].Text.ToString();
                    string times = BookingsTable.Rows[index].Cells[2].Text.ToString();

                    date = new DateTime(DateTime.Parse(dates).Ticks + DateTime.Parse(times).Ticks);
                    if (date < DateTime.Now)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>alert('You cannot modify a booking in the past')</script>");
                        return;
                    }
                }
                catch { }
                Response.Redirect("EditBooking.aspx?BookingID=" + id);

            }
            if (e.CommandName == "buttonDelete")    //If Delete is clicked
            {
                Session["rowIndex"] = e.CommandArgument;
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>ShowPopup();</script>");
            }
        }

        protected void deleteEntry()
        {
            int indexOfRow = Convert.ToInt32(Session["rowIndex"]);    //Gets the argument and convert in to an int (row index) 
            string eventIDString = BookingsTable.Rows[indexOfRow].Cells[5].Text;    //Store the EventID from the GridView to a string
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
            CreateTable();    //Reload the table after booking is deleted
        }

        protected void yesButton(object sender, EventArgs e)
        {
            deleteEntry();
        }

        protected void noButton(object sender, EventArgs e)
        {

        }

        protected void CreateTable()
        {
            int userId = (int)Session["UserId"];
            DataTable currentUserTable = new DataTable();
            try
            {
                currentUserTable = DatabaseConnections.GetBookingOfUser(userId, false);    //Get bookings from UserID (1 = ADMIN)
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            DataTable tempDataTable = new DataTable();    //New datatable 
            tempDataTable.Columns.AddRange(new DataColumn[8]    //Add 7 columns to the datatable
                {
                    new DataColumn("roomID", typeof(string)),    //Each new column
                    new DataColumn("date", typeof(string)),
                    new DataColumn("startTime", typeof(string)),
                    new DataColumn("endTime", typeof(string)),
                    new DataColumn("information", typeof(string)),
                    new DataColumn("eventID", typeof(string)),
                    new DataColumn("editButton", typeof(Button)),
                    new DataColumn("deleteButton", typeof(Button))
                });

            foreach (DataRow row in currentUserTable.Rows)    //For each row in the datatable
            {
                DataRow tempRow = tempDataTable.NewRow();    //Make a new row
                try
                {
                    tempRow[0] = row[0].ToString();    //Copy each column to the empty datatable
                    tempRow[1] = DateTime.Parse(row[1].ToString()).ToShortDateString();
                    tempRow[2] = DateTime.Parse(row[1].ToString()).ToShortTimeString();
                    tempRow[3] = DateTime.Parse(row[2].ToString()).ToShortTimeString();
                    tempRow[4] = row[3].ToString();
                    tempRow[5] = row[4].ToString();
                }
                catch { }
                tempDataTable.Rows.Add(tempRow);    //Add the row to the table
            }

            BookingsTable.DataSource = tempDataTable;    //Select the data source for the GridView
            BookingsTable.DataBind();    //Bind the data to the GridView
        }

    }
}