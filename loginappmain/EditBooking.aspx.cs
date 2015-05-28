using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    /*
     * Displays a form that lets the user create bookings for either entire rooms or computers
     */
    public partial class EditBooking : System.Web.UI.Page
    {
        TimeSpan incrementer = new TimeSpan(0, 15, 0); // How much time we add a time 
        // Two lists for storing the meeting start and finish times
        List<TimeSpan> meetingsStart = new List<TimeSpan>();
        List<TimeSpan> meetingsEnd = new List<TimeSpan>();
        // Global variable to store the start time to start the booking
        private int bookingID;
        private static int DESCRIPTION_MAX = 150;
        private int accessLevel = 0;
        DateTime date = DateTime.Now;
        TextBox txtDatePicker;
        TimeSpan endTime;

        /* Called on page load. Loads the data and displays it on the screen */
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the User Id from the session
            int id = 1;

            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                id = (int)Session["UserId"];
            }

            try
            {
                bookingID = Int32.Parse(Request["BookingId"].ToString());
                accessLevel = DatabaseConnections.GetAcessLevel(id);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            if (accessLevel == 1 || accessLevel == 2) // Admin and Novus Staff
            {
                txtDatePicker5D.Visible = false;
                txtDatePicker = txtDatePicker6M;
            }

            if (accessLevel == 4) // Non Novus Staff
            {
                txtDatePicker = txtDatePicker5D;
                txtDatePicker6M.Visible = false;
            }


            if (accessLevel == 3) // Novus Trainnie
            {
                txtDatePicker = txtDatePicker5D;
                txtDatePicker6M.Visible = false;
            }




            // Run these functions the first time the page was called. This will setup the default values for the page.
            if (!Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                try
                {

                    dt = DatabaseConnections.GetBooking(bookingID);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

                DateTime startDateTime = DateTime.Parse(dt.Rows[0]["StartDateTime"].ToString());
                DateTime endDateTime = DateTime.Parse(dt.Rows[0]["EndDateTime"].ToString());
                String description = dt.Rows[0]["Description"].ToString();
                int selectedRoom = Int32.Parse(dt.Rows[0]["RoomID"].ToString());
                bool pcBooking = bool.Parse(dt.Rows[0]["PcBooking"].ToString());
                Session["UpdateUserID"]= Int32.Parse(dt.Rows[0]["UserID"].ToString());
                int pcsBooked = Int32.Parse(dt.Rows[0]["PCsBooked"].ToString());
                string roomName = "";
                
                try
                {
                    roomName = DatabaseConnections.GetRoomName(selectedRoom);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }
                

                ddlPcBookings.Enabled = pcBooking;
                if (!pcBooking)
                {                    
                    lblPcBooking.Visible = false;
                    ddlPcBookings.Visible = false;
                }


                endTime = endDateTime.TimeOfDay;

                // Get all of the rooms in the database and add them to the drop down list
                try
                {
                    dt = DatabaseConnections.GetRooms();
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

                foreach (DataRow dr in dt.Rows)
                {
                    ddlChooseRoom.Items.Add(dr["RoomName"].ToString());
                }
                // Load the data for the first room
                LoadData(roomName);


                ddlChooseRoom.Items.FindByText(roomName).Selected = true;
                txtDatePicker.Text = BookingFormHelper.FormatDate(startDateTime);
                txtDescription.Text = description;
                ddlPcBookings.Enabled = pcBooking;


                // Load the times into the time box.
                SetUpStartTimes(startDateTime);

                try
                {
                    ddlStartTime.ClearSelection();
                    ddlStartTime.Items.FindByText(BookingFormHelper.FormatTimeSpan(startDateTime.TimeOfDay)).Selected = true;


                    ddlEndTime.Items.FindByText(BookingFormHelper.FormatTimeSpan(endDateTime.TimeOfDay)).Selected = true;
                }
                catch { /* Carry on - do nothing*/}
                RefreshPCsBooked();
                try
                {
                    ddlPcBookings.Items.FindByText(pcsBooked.ToString()).Selected = true;
                }
                catch
                {
                    // continue
                }
            }

        }

        /*
         * Load the data and populate the fields on the page based upon the room name that has been passed
         */
        private void LoadData(String selectedRoomName)
        {           
            // Get the data from the room
            DataTable dtResources = new DataTable();
            try
            {
                dtResources = DatabaseConnections.GetRoom(selectedRoomName);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            bool printerColour = Convert.ToBoolean(dtResources.Rows[0][1].ToString());
            bool printerBlack = Convert.ToBoolean(dtResources.Rows[0][2].ToString());
            int pcNumbers = Convert.ToInt32(dtResources.Rows[0][3].ToString());

            // Set the text of the labels to indicate if there's a printer or not                
            lblColourPrinter2.Text = printerColour ? "Yes" : "No";                
            lblBlackPrinter2.Text = printerBlack ? "Yes" : "No";
      
            // Add the number of pcs in the room to the text box
            lblNumberPCs2.Text = pcNumbers.ToString();
            // Clear the items in the pcbooking drop down.
            ddlPcBookings.Items.Clear();

            // Store the number of pcs in the room (for later use)
            Session["pcNumbers"] = pcNumbers;        
        }

        /*
         * Populate the drop down with the times the user can book from
         */
        private void SetUpStartTimes(DateTime selectedDate)
        {
            string selectedTime = BookingFormHelper.FormatTimeSpan(selectedDate.TimeOfDay);
            // First of all - clear all data in the drop down.
            ddlStartTime.Items.Clear();
            
            TimeSpan currentTime = new TimeSpan(9, 0, 0);
            List<TimeSpan> tempTimes = new List<TimeSpan>(); // List of Time Slots
            TimeSpan timeLimit = new TimeSpan(17, 30, 0); // The time the office closes    

            if (selectedDate.Date == DateTime.Now.Date)
            {
                currentTime = BookingFormHelper.getCurrentTime();
            }

            // Get a list of all of the occupied times
            List<TimeSpan> occupiedTimes = getOccupiedTimes(ddlChooseRoom.SelectedValue, selectedDate);

            // Starting from the current time check to see if it appears in the occupied times list. If it doesn't, 
            // add it to the list. Increment the time by the set incrementor (15mins) until it reaches the end time.
            // The logic for booking a pc is different. So if the user is bookinh a pc add it anyways.
            while (currentTime < timeLimit)
            {
                // Check if the time is occupied or if the user is making a pc booking
                if (!IsTimeOccupied(occupiedTimes, currentTime) || ddlPcBookings.Enabled)
                {
                    // If it's not occupied or if the user is booking a pc add it to the list
                    tempTimes.Add(currentTime);
                }
                // Increment the current time.
                currentTime += incrementer;
            }

            // If there are no times stored then inform the user the day is fully booked, otherwise add text asking them to select a time
            if (tempTimes.Count > 0)
            {
                ddlStartTime.Items.Add("-Select A Time-");
            }
            else
            {
                ddlStartTime.Items.Add("No more slots free on this day");
            }

            // Add text to the endtime box informing them to make a selection
            ddlEndTime.Items.Add("Select A Start Time");

            // Now look through all of the temp times and format each and add them to the start time list
            for (int i = 0; i < tempTimes.Count; i++)
            {
                ddlStartTime.Items.Add(BookingFormHelper.FormatTimeSpan(tempTimes[i]));
            }
            try
            {
                ddlStartTime.ClearSelection();
                ddlStartTime.Items.FindByValue(selectedTime).Selected = true;
            }
            catch
            {  
                // Do nothing - just continue
            }
            refreshEndTimes();
        }

        /*
         * Checks to see if a given time is occupied by another booking
         */
        protected bool IsTimeOccupied(List<TimeSpan> occupiedTimes, TimeSpan times)
        {
            // Loop through and compare the hours and minutes and return true if matched
            for (int i = 0; i < occupiedTimes.Count; i++)
            {
                if (occupiedTimes[i].Hours == times.Hours && occupiedTimes[i].Minutes == times.Minutes)
                    return true;
            }
            // otherwise return true.
            return false;
        }

        /*
         * Called when the user has changed the start time. This will populate the end time dropdown
         */
        protected void ddlStartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshEndTimes();
        }


        private void refreshEndTimes()
        {         
            // Clear the end times and enable the drop down
            if (ddlEndTime.SelectedIndex < 0) return;

            string selectedEndTime = ddlEndTime.SelectedValue;
            ddlEndTime.Items.Clear();
            ddlEndTime.Enabled = true;
            string selectedTime;
            TimeSpan start;
            try
            {
                selectedTime = ddlStartTime.SelectedValue.ToString();
                start = TimeSpan.Parse(selectedTime);
            }
            catch
            {
                selectedTime = ddlStartTime.Items[1].ToString();
                start = TimeSpan.Parse(selectedTime);
            }

            List<string> times = new List<string>();
            List<TimeSpan> occupiedTimes = getOccupiedTimes(ddlChooseRoom.SelectedValue, DateTime.Parse(txtDatePicker.Text));

            // Run a linq to get all of the times that in the meetings what are after the current time selected
            var times2 = from time in meetingsStart
                            where start < time
                            select time;

            // Create a list of times and store the results of the linq statement in them.
            List<TimeSpan> listOfTimes = new List<TimeSpan>();
            foreach (TimeSpan ts in times2)
            {
                listOfTimes.Add(ts);
            }

            List<string> timescollections = new List<string>();
            foreach (ListItem li in ddlStartTime.Items)
            {
                timescollections.Add(li.Text);
            }
            // timescollections.Add("17:30");
            TimeSpan oldtime;
            TimeSpan temp = start; ;
            TimeSpan startTime = new TimeSpan(9,0,0);
            try
            {
                startTime = TimeSpan.Parse(ddlStartTime.SelectedValue);
            }
            catch {  }

            for (int i = 0; i < timescollections.Count; i++)
            {
                try
                {
                    oldtime = TimeSpan.Parse(timescollections[i]);
                    temp = TimeSpan.Parse(timescollections[i + 1]);

                    if (temp > start)
                    {

                        if ((temp - oldtime).Minutes > 15 || temp<startTime)
                            break;
                        else
                            times.Add(BookingFormHelper.FormatTimeSpan(temp));
                    }
                }
                catch
                {
                    continue;
                }
            }
            if (times.Count > 0)
            {
                TimeSpan last = TimeSpan.Parse(times[times.Count - 1]);
                times.Add(BookingFormHelper.FormatTimeSpan(last + new TimeSpan(0, 15, 0)));
            }

            foreach (string s in times)
            {
                ddlEndTime.Items.Add(s);
            }
            try
            {
                ddlEndTime.Items.FindByText(BookingFormHelper.FormatTimeSpan(endTime)).Selected = true;
            }
            catch {/*continue*/}

            try
            {
                ddlEndTime.ClearSelection();
                ddlEndTime.Items.FindByValue(selectedEndTime).Selected = true;
            }
            catch
            {
                // Do nothing, just continue
            }
        }

        /*
         * Called when the user clicks on the button to make a booking
         */
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Setup variables
            string buildBookingDate = null;
            DateTime bookingStart = new DateTime();
            DateTime bookingEnd = new DateTime();
            string selectedRoomName = null;
            string description = null;
            int pcsBooked = 0;



            // Get the date from the datefield and store it
            if (txtDatePicker.Text.Length == 10)
            {
                buildBookingDate = txtDatePicker.Text;
            }
            else
            {
                ErrorBox("Please enter a valid date.");
                return;
            }

            // Get the time from the start time field and append it to the date and convert to a DateTime. 
            if (ddlStartTime.Text.Length == 5)
            {
                bookingStart = Convert.ToDateTime(buildBookingDate + " " + ddlStartTime.SelectedItem.Text + ":00");
            }
            else
            {
                ErrorBox("Please enter a valid start time");
                return;
            }

            // Get the end time and append it to the date to create a DateTime. We now have a startTimeDate and EndTimeDate
            if (ddlEndTime.Text.Length == 5)
            {
                bookingEnd = Convert.ToDateTime(buildBookingDate + " " + ddlEndTime.SelectedItem.Text + ":00");
            }
            else
            {
                ErrorBox("Please enter a valid end time");
                return;
            }

            // Check the description
            if (txtDescription.Text.Length <= DESCRIPTION_MAX)
            {
                description = txtDescription.Text;
            }
            else
            {
                ErrorBox("Please enter a description (less than " + DESCRIPTION_MAX + " characters) for your event");
                return;
            }

            if (ddlPcBookings.Enabled)
            {
                try
                {
                    pcsBooked = int.Parse(ddlPcBookings.SelectedValue);
                }
                catch
                {
                    ErrorBox("Please select a valid amount of computers to book.");
                    return;
                }
            }

            if (bookingStart < DateTime.Now)
            {
                ErrorBox("You can not make a booking in the past. Please modify the date/time");
                return;
            }

            int userID =  Int32.Parse(Session["UpdateUserID"].ToString());

            // Store the selected room.
            selectedRoomName = ddlChooseRoom.Text;

            if (bookingEnd < bookingStart)
            {
                ErrorBox("End date/time cannot be before the start date/time");
                return;
            }

            string url = "1;HomePage.aspx?date=" + BookingFormHelper.FormatDate(bookingStart);
            try
            {
                DatabaseConnections.EditBooking(bookingID, selectedRoomName, userID, bookingStart, bookingEnd, description, ddlPcBookings.Enabled, pcsBooked);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            ErrorBox("Your booking has been made successfully. You will be redirected to the homepage.");
            Response.AddHeader("REFRESH", url);

        }

        /*
         * Updates the number of PCs that have been booked so that the user can only book computers that are free
         */
        private void RefreshPCsBooked()
        {
            // Only function if the user is making a pc booking.
            if (ddlPcBookings.Enabled)
            {
                // Create variables.
                string buildBookingDate = "";
                DateTime bookingStart;

                // Get the date and the time from the input fields so we know what date/time
                try
                {
                    buildBookingDate = txtDatePicker.Text;
                    string tempdate = buildBookingDate + ddlStartTime.SelectedItem.Text + ":00";
                    bookingStart = Convert.ToDateTime(buildBookingDate + " " + ddlStartTime.SelectedItem.Text + ":00");
                }
                catch
                {
                    ErrorBox("Invalid values detected in the date/time fields");
                    return;
                }
                int roomID = 0, pcsBooked = 0;
                try
                {
                    // Get the number of pcs booked in the selected room at the selected date/time
                    roomID = DatabaseConnections.GetRoomID(ddlChooseRoom.SelectedValue);
                    pcsBooked = DatabaseConnections.GetBookedPCs(roomID, bookingStart);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

                // Get the number of pcs avaiable in the room from the session.
                int pcNumbers = 0;
                try
                {
                    pcNumbers = int.Parse(Session["pcNumbers"].ToString());
                }
                catch
                {
                    pcNumbers = 0;
                }

                // Clear the list
                ddlPcBookings.Items.Clear();

                // Loop through for all of the unbooked pcs
                for (int i = 0; i < pcNumbers - pcsBooked; i++)
                {
                    ddlPcBookings.Items.Add((i + 1).ToString());
                }

                // If there are no pcs available display this error
                if (pcNumbers - pcsBooked < 1)
                {
                    ddlPcBookings.Items.Add("No more PCs avaiable");
                    ErrorBox("There are no more PCS avaiable in this room at this time. Please select another room, date or adjust your chosen time.");
                }
            }
        }

        /*
         * Gets a list of all of the times that are occupied by a booking
         */
        private List<TimeSpan> getOccupiedTimes(string room, DateTime date, bool srt = true)
        {
            // Get the status of the drop down to determine if we are making a room/pc booking
            bool roomBooking = ddlPcBookings.Enabled;

            // Get all of the bookings of the given room on the given date/time
            DataTable bookingInfo = new DataTable();
            try
            {
                bookingInfo = DatabaseConnections.GetBookingsOfRoomExclude(room, date, bookingID);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            // For each row store the start and end time of the booking.
            foreach (DataRow dr in bookingInfo.Rows)
            {
                try
                {
                    TimeSpan start = DateTime.Parse(dr["StartDateTime"].ToString()).TimeOfDay;
                    TimeSpan finish = DateTime.Parse(dr["EndDateTime"].ToString()).TimeOfDay;

                    meetingsStart.Add(start);
                    meetingsEnd.Add(finish);
                }
                catch { continue;  }
            }

            // This list will store of all of the times.
            // For example, if a booking is from 9-10 then we will store 9.00, 9.15, 9.30 and 9.45
            List<TimeSpan> occupiedTimes = new List<TimeSpan>();

            // Loop through all of the meetings and add 15mins until we reach the end time.
            for (int i = 0; i < meetingsStart.Count; i++)
            {
                TimeSpan tempTime;
                if (!srt)
                    tempTime = meetingsStart[i] + incrementer;
                else
                    tempTime = meetingsStart[i];
                while (tempTime < meetingsEnd[i])
                {
                    occupiedTimes.Add(tempTime);
                    tempTime += incrementer;
                }
            }
            // Return the times
            return occupiedTimes;
        }

        /*
         * Called when the user changes the selected room
         */
        protected void ddlChooseRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the new selected room and load the data
            string room = (sender as DropDownList).SelectedItem.Text;
            LoadData(room);

            // Set up the start time and booked pcs
            DateTime date = DateTime.Now;
            try
            {
                date = DateTime.Parse(txtDatePicker.Text);
            }
            catch 
            { 
                // Do nothing, just continue
            }
            SetUpStartTimes(date);
            RefreshPCsBooked();
        }

        /*
         * The user has changed the date. Update the page
         */
        protected void txtDatePicker_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Parse(txtDatePicker.Text);
                SetUpStartTimes(date);
                RefreshPCsBooked();
            }
            catch
            { return;  }
        }
              

        /*
        * If a date is passed in the url get this as the starting date.
        */
        private void ProcessRequest()
        {
            // Check to see if there is a request
            if (Request["Date"] != null)
            {
                // Setup variables
                DateTime date = DateTime.Now;
                int id = 0;

                // Get the date and time and the room id that's expected
                try
                {
                    date = DateTime.Parse(Request["Date"].ToString());
                    id = Int32.Parse(Request["RoomID"].ToString());

                    // Set the fields to that data
                    txtDatePicker.Text = date.Date.ToString().Substring(0, 11);
                }
                catch
                {
                    // If this cannot be done just return. 
                    return;
                }
            }
        }

        /*
         * Shows a JS error
         */
        private void ErrorBox(string message)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('" + message + "');", true);

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