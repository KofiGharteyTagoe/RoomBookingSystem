using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class Calendar : System.Web.UI.Page
    {
        // A list of the bookings storing the start and end time of the bookings
        List<StartAndFinishTimes> bookings = new List<StartAndFinishTimes>();
        // Colours to be used to reflect the densisty of bookings. Feel free to change the colours but ensure the number of colours remains the same.
        Color[] colours = { Color.Yellow, Color.Orange, Color.OrangeRed, Color.Red, Color.Black };

        /*
         * Called on method load. Sets up the key table
         */

        protected void Page_Load(object sender, EventArgs e)
        {

            // Loads the data into the array
            bookings = GetData();

            // Create a new table and add colmuns to the key
            DataTable dt = new DataTable();
            dt.Columns.Add("Colour");
            dt.Columns.Add("Hours Free To Book (out of 24 hrs spread across 3 rooms)");


            // Create a new row, add blank into the first column
            DataRow dr = dt.NewRow();           
            dr[0] = " ";
            dr[1] = "Less than 20 hours left";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = " ";
            dr[1] = "Less than 15 hours left";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[1] = "Less than 10 hours left";
            dr[0] = " ";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = " ";
            dr[1] = "Less than 5 hours left";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = " ";
            dr[1] = "No Space Remainning";
            dt.Rows.Add(dr);
            

            // Add the source.
            GridView1.DataSource = dt;
            GridView1.DataBind();

            // Loop through the number of colours, adding a different colour based upon the row into the first column of each row
            for (int i = 0; i < colours.Length; i++)
            {
                Style s2 = new Style();
                s2.BackColor = colours[i];
                GridView1.Rows[i].Cells[0].ApplyStyle(s2);
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

        /*
        * Called when each day on the calender is rendered. Works out the number of minutes free and applies formatting accordingly
        */
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            // How many minutes have been booked so far
            double hoursBooked = 0;

            // Get a list of the bookings on the current date being rendered
            var daysBooking = from booking in bookings
                              where booking.start.Date == e.Day.Date
                              orderby booking.start
                              select booking;

            // Loop through each StartFinishTime element working out the duration of the meeting (by subtracting the start time from
            // the finish time and keeping a tally of the total minutes.
            foreach (StartAndFinishTimes sf in daysBooking)
            {
                TimeSpan ts = sf.finish.Subtract(sf.start);
                hoursBooked += ts.TotalHours;
            }

            // If the minutes is greater than 0 apply formatting
            if (hoursBooked > 0)
                e.Cell.BackColor = GetColour(hoursBooked);
        }

        /*
         * Gets data from the database and returns it as a list
         */
        private List<StartAndFinishTimes> GetData()
        {
            // Create a list of the start and finish times
            List<StartAndFinishTimes> bookings = new List<StartAndFinishTimes>();

            // Get the bookings from the database
            DataTable dt = new DataTable();
            try
            {
                DatabaseConnections.GetBookings();
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            // Parse each element as a dateTime and add it to the list
            foreach (DataRow dr in dt.Rows)
            {
                // Try to parse. If it can't parse a date, skip onto the next element.
                try
                {
                    bookings.Add(new StartAndFinishTimes(DateTime.Parse(dr["StartDateTime"].ToString()), DateTime.Parse(dr["EndDateTime"].ToString())));
                }
                catch { continue; }
            }
            return bookings;
        }

        /*
         * Returns a colour based upon the total minutes that are booked
         */
        private Color GetColour(double bookings)
        {

            // Note: From 9-5 there are 8 hours. There are 3 rooms so in total there are 24 hours worth of booking slots
            // For ease of maths, we want this in minutes. This will be 24 x 60 = 1440 
            if (bookings < 5)
                return colours[0];
            else if (bookings < 10)
                return colours[1];
            else if (bookings < 15)
                return colours[2];
            else if (bookings < 20)
                return colours[3];
            else if (bookings < 24)
                return colours[4];
            else return colours[5];
        }

        /*
         * If an element is clicked on, forward the user onto the weekview page with a reference of the date. This enables them to see
         * the events that have been booked in more detail
         */
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // Get selected date
            DateTime selectedDate = Calendar1.SelectedDate.Date;
            // Redirect the user
            Response.Redirect("AddBooking.aspx?SelectedDate=" + selectedDate.ToShortDateString());
        }



    }
}