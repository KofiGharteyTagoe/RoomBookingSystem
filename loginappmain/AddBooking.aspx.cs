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
    public partial class AddBooking : System.Web.UI.Page
    {

        TimeSpan incrementer = new TimeSpan(0, 15, 0); // How much time we add a time 
        // Two lists for storing the meeting start and finish times
        List<TimeSpan> meetingsStart = new List<TimeSpan>();
        List<TimeSpan> meetingsEnd = new List<TimeSpan>();
        // Global variable to store the start time to start the booking
        private int id = 0;
        public bool months;
        private static int DESCRIPTION_MAX = 150;
        TextBox txtDatePicker;
        int accessLevel = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                accessLevel = DatabaseConnections.GetAcessLevel(id);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            ddlPcBookings.Visible = false;
            lblPcBooking.Visible = false;

            if (accessLevel != 1)
            {
                txtRecurring.Visible = false;
                chkRecurring.Visible = false;
                lblRecurring.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "List", "<script language=javascript>hideListItem();</script>");
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
                ddlPcBookings.Enabled = false;
                lblPcBooking.Enabled = false;

                lblHeader.Text = "Book A Room";
                btnPc.Visible = false;
                btnRoom.Visible = false;
            }


            if (accessLevel == 3) // Novus Trainnie
            {
                txtDatePicker = txtDatePicker5D;
                txtDatePicker6M.Visible = false; 
                ddlPcBookings.Enabled = true;
                btnPc.Visible = false;
                btnRoom.Visible = false;
                lblPcBooking.Visible = true;
                ddlPcBookings.Visible = true;
                lblHeader.Text = "Book A PC";
            }
  

                DataTable userInfo = new DataTable();
                lblPcBooking.Visible = pcBooking();
                ddlPcBookings.Visible = pcBooking();
                ddlPcBookings.Enabled = pcBooking();
                try
                {
                    userInfo = DatabaseConnections.GetUser(id);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

         
            // Set the text into the text box
            if (txtDatePicker.Text.Length < 1)
                txtDatePicker.Text = BookingFormHelper.GetDate();


            // Run these functions the first time the page was called. This will setup the default values for the page.
            if (!Page.IsPostBack)
            {
                // Get all of the rooms in the database and add them to the drop down list
                DataTable dt = new DataTable();
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
                    ddlChooseRoom.Items.Add(dr[0].ToString());
                }
                RoomRequest();
                // Load the times into the time box.
                SetUpStartTimes();

                // Load anymore requests
                DateRequest();
                // Load the data for the first room
                LoadData(ddlChooseRoom.SelectedValue.ToString());
                // By default you book a room so disable the book pc section
                ddlPcBookings.Enabled = false;
                setPcBooking(false);
               
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

            bool printerColour = Convert.ToBoolean(dtResources.Rows[0]["PrinterColour"].ToString());  
            lblColourPrinter2.Text = printerColour ?  "Yes": "No";
            
            bool printerBlack = Convert.ToBoolean(dtResources.Rows[0]["PrinterBlack"].ToString());
            lblBlackPrinter2.Text = printerBlack ? "Yes" : "No";
            
            int pcNumbers = Convert.ToInt32(dtResources.Rows[0]["PCNumbers"].ToString());
            // Add the number of pcs in the room to the text box
            lblNumberPCs2.Text = pcNumbers.ToString();


            // Clear the items in the pcbooking drop down.
            ddlPcBookings.Items.Clear();

            // Store the number of pcs in the room (for later use)
            Session["pcNumbers"] = pcNumbers;


            RefreshPCsBooked();

        }
        /*
         * Populate the drop down with the times the user can book from
         */
        private void SetUpStartTimes()
        {
             string selected="";
             if (ddlStartTime.SelectedIndex>-1)
                selected = ddlStartTime.SelectedValue;
 
            // First of all - clear all data in the drop down.
            ddlStartTime.Items.Clear();

            // To store the starting time the user can make a booking. As default it is 9am.
            TimeSpan currentTime = new TimeSpan(9, 0, 0);
            DateTime selectedDate = DateTime.Now;

            // Get the selected date
            try
            {
                selectedDate = Convert.ToDateTime(txtDatePicker.Text.TrimEnd()).Date;
            }
            catch
            {
                ErrorBox("Invalid entry in the selected date field. Please use the format DD/MM/YYYY");
                return;
            }

            // If the selected date is today. Get the current time. This will stop the user booking for times in the past
            // ie if the time now is 12am the earliest they can book for is 12.15
            if (selectedDate == DateTime.Now.Date)
            {
                currentTime = BookingFormHelper.getCurrentTime();
            }

            List<TimeSpan> tempTimes = new List<TimeSpan>(); // List of Time Slots
            TimeSpan timeLimit = new TimeSpan(17, 30, 0); // The time the office closes            

            // Get a list of all of the occupied times
            List<TimeSpan> occupiedTimes = getOccupiedTimes(ddlChooseRoom.SelectedValue, selectedDate);

            // Starting from the current time check to see if it appears in the occupied times list. If it doesn't, 
            // add it to the list. Increment the time by the set incrementor (15mins) until it reaches the end time.
            // The logic for booking a pc is different. So if the user is bookinh a pc add it anyways.
            while (currentTime < timeLimit)
            {
                // Check if the time is occupied or if the user is making a pc booking
                if (!IsTimeOccupied(occupiedTimes, currentTime) || pcBooking())
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
            if (selected.Length > 0)
            {
                try
                {
                    ddlStartTime.ClearSelection();
                    ddlStartTime.Items.FindByValue(selected).Selected = true;
                }
                catch { return; }
            }
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
            UpdateEndTimeList();
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

            DateTime enteredDate = DateTime.Parse(txtDatePicker.Text);
            if (accessLevel == 1 || accessLevel == 2)
            {
                if (enteredDate > DateTime.Now.AddMonths(6))
                {
                    ErrorBox("You can only make a booking up to 6 months in advance");
                    return;
                }
            }
            else
            {
                if (enteredDate > DateTime.Now.AddDays(7))
                {
                    ErrorBox("You can only make a booking up to 7 days in advance");
                    return;
                }
            }

            if (enteredDate.DayOfWeek == DayOfWeek.Saturday || enteredDate.DayOfWeek == DayOfWeek.Sunday)
            {
                ErrorBox("You cannot make bookings for the weekend. Please select another date.");
                return;
            }


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
                try
                {
                    bookingStart = Convert.ToDateTime(buildBookingDate + " " + ddlStartTime.SelectedItem.Text + ":00");
                }
                catch { return; }
            }
            else
            {
                ErrorBox("Please enter a valid start time");
                return;
            }

            // Get the end time and append it to the date to create a DateTime. We now have a startTimeDate and EndTimeDate
            if (ddlEndTime.Text.Length == 5)
            {
                try{
                bookingEnd = Convert.ToDateTime(buildBookingDate + " " + ddlEndTime.SelectedItem.Text + ":00");
                }
                catch{return;}
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

            if (pcBooking())
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

            // Store the selected room.
            selectedRoomName = ddlChooseRoom.Text;

            ///uncomment line below for final code
            int userID = (int)Session["UserId"];

            ///delete line below for final code
            //int userID = 1;

            DateTime until = bookingStart;
            if (chkRecurring.Checked)
            {
                try
                {
                    until = DateTime.Parse(txtRecurring.Text.ToString());
                }
                catch
                {
                    until = bookingStart;
                }
            }

            if (bookingEnd < bookingStart)
            {
                ErrorBox("End date/time cannot be before the start date/time");
                return;
            }

            //Make Booking in a loop to account for the recurring box.
            string url = "1;HomePage.aspx?date=" + BookingFormHelper.FormatDate(bookingStart);
            do
            {
                // We don't make bookings on weekends. So progress onto the next day and continue the loop.
                if (bookingStart.DayOfWeek == DayOfWeek.Saturday || bookingStart.DayOfWeek == DayOfWeek.Sunday)
                {
                    bookingStart = bookingStart.AddDays(1);
                    bookingEnd = bookingEnd.AddDays(1);
                    continue;
                }

                // If we are not making a pc booking, add the entry to the database
                try
                {
                    DatabaseConnections.AddBooking(selectedRoomName, userID, bookingStart, bookingEnd, description, pcBooking(), pcsBooked);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

                // Increment the booking days
                bookingStart = bookingStart.AddDays(1);
                bookingEnd = bookingEnd.AddDays(1);
            } while (bookingStart.Date != until.AddDays(1).Date); // Continue the loop until we exceed the end date they have entered

            ErrorBox("Your booking has been made successfully. You will be redirected to the homepage");
            Response.AddHeader("REFRESH", url);
            //Response.Redirect("HomePage.aspx?date=" + BookingFormHelper.FormatDate(bookingStart));

        }

        

        /*
         * Updates the number of PCs that have been booked so that the user can only book computers that are free
         */
        private void RefreshPCsBooked()
        {
            // Only function if the user is making a pc booking.
            if (pcBooking())
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
                    return;
                }
                int roomID = 0, pcsBooked = 0;
                // Get the number of pcs booked in the selected room at the selected date/time
                try
                {
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
            bool roomBooking = pcBooking();
            

            // Get all of the bookings of the given room on the given date/time
            DataTable bookingInfo = new DataTable();
            try
            {
                bookingInfo = DatabaseConnections.GetBookingsOfRoom(room, date);
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
                catch { continue; }
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
            SetUpStartTimes();
            RefreshPCsBooked();
        }

        /*
 * The user has changed the date. Update the page
 */
        protected void txtDatePicker_TextChanged(object sender, EventArgs e)
        {
            SetUpStartTimes();
            RefreshPCsBooked();
        }

        /*
     * The user wants to book a pc
     */
        protected void btnPc_Click(object sender, EventArgs e)
        {
            lblHeader.Text = "Book A PC";
            ddlPcBookings.Visible = true;
            lblPcBooking.Visible = true;
            ddlPcBookings.Enabled = true;
            setPcBooking(true);
            LoadData(ddlChooseRoom.SelectedValue.ToString());
            SetUpStartTimes();
        }

        /*
    * The user wants to book a room
    */
        protected void btnRoom_Click(object sender, EventArgs e)
        {
            lblHeader.Text = "Book A Room";
            ddlPcBookings.Visible = false;
            lblPcBooking.Visible = false;
            ddlPcBookings.Enabled = false;
            setPcBooking(false);
            LoadData(ddlChooseRoom.SelectedValue.ToString());
            SetUpStartTimes();
        }

    /*
    * Toggles the recurring text box from enabled to disabled based upon the recurring checkbox
    */
        protected void chkRecurring_CheckedChanged(object sender, EventArgs e)
        {
            txtRecurring.Enabled = chkRecurring.Enabled;
        }

    /*
    * If a date is passed in the url get this as the starting date.
    */
        private void DateRequest()
        {
            // Check to see if there is a request
            if (Request["Date"] != null)
            {
                // Setup variables
                DateTime date = DateTime.Now;
                // Get the date and time and the room id that's expected
                try
                {
                    date = DateTime.Parse(Request["Date"].ToString());
                    if (date > DateTime.Now)
                    {
                        // Set the fields to that data
                        txtDatePicker.Text = date.Date.ToString().Substring(0, 11).TrimEnd();
                        ddlStartTime.ClearSelection();
                        ddlStartTime.Items.FindByText(BookingFormHelper.FormatTimeSpan(date.TimeOfDay)).Selected = true;
                        UpdateEndTimeList();
                        ddlEndTime.ClearSelection();
                        ddlEndTime.Items.FindByText(BookingFormHelper.FormatTimeSpan(date.AddMinutes(15).TimeOfDay)).Selected = true;
                    }
                    else
                    {
                        ErrorBox("You cannot make a booking in the past. Please select a future date/time");
                    }
                }
                catch
                {
                    // If this cannot be done just return. 
                    return;
                }
            }
        }

        private void RoomRequest()
        {
            // Check to see if there is a request
            if (Request["RoomID"] != null)
            {
                // Setup variables
                int id = 0;
                // Get the date and time and the room id that's expected
                try
                {
                    id = Int32.Parse(Request["RoomID"].ToString());
                    // Set the fields to that data                    
                    string roomName = "";
                    try
                    {
                        roomName = DatabaseConnections.GetRoomName(id);
                    }
                    catch
                    {
                        Session["Disconnected"] = true;
                        Response.Redirect("LoginPage.aspx");
                    }
                    
                    ddlChooseRoom.ClearSelection();
                    ddlChooseRoom.Items.FindByText(roomName).Selected = true;
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
            confirmation.Text = message;
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>ShowPopup();</script>");
           
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


        protected void UpdateEndTimeList()
        {
            // Clear the end times and enable the drop down
            ddlEndTime.Items.Clear();
            ddlEndTime.Enabled = true;

            // Get the selected current time and store it as a Timespan
            TimeSpan currentTime;
            try
            {
                currentTime = TimeSpan.Parse(ddlStartTime.SelectedValue.ToString());
            }
            catch
            {
                return;
            }
            List<TimeSpan> tempTimes = new List<TimeSpan>(); // List of Time Slots
            TimeSpan timeLimit = new TimeSpan(17, 30, 0); // The time the office closes        

            // Run a linq to get all of the times that in the meetings what are after the current time selected
            var times = from time in meetingsStart
                        where currentTime < time
                        select time;

            // Create a list of times and store the results of the linq statement in them.
            List<TimeSpan> listOfTimes = new List<TimeSpan>();
            foreach (TimeSpan ts in times)
            {
                listOfTimes.Add(ts);
            }

            // The times are sorted. So if there's elements in the list, take the first one - that's the time of the earliest meeting
            // and therefore, that becomes our new timeLimit
            if (listOfTimes.Count > 0) timeLimit = listOfTimes[0];

            // While the current time doesn't exceed the finish time, add it to the list and increment it by one.
            while (currentTime < timeLimit)
            {
                currentTime += incrementer;
                tempTimes.Add(currentTime);
            }

            // Get the date from the date picker
            DateTime date = DateTime.Now;
            try
            {
                date = DateTime.Parse(txtDatePicker.Text);
            }
            catch
            {
                date = DateTime.Now;
            }

            // Check to see the occupied times of the room. 
            List<TimeSpan> occupiedTimes = getOccupiedTimes(ddlChooseRoom.SelectedValue, date, false);


            // Get the number of pcs in the room. Will be used if we are booking rooms
            int pcNumbers = 0;
            try
            {
                pcNumbers = int.Parse(Session["pcNumbers"].ToString());
            }
            catch
            {
                pcNumbers = 0;
            }

            // If there's time slots avaiable that be booked, start to add them in 
            if (tempTimes.Count > 0)
            {
                for (int i = 0; i < tempTimes.Count; i++)
                {
                    // Test whether the pcs booking list is enabled. This will tell us if the user is booking a pc or a room               
                    if (!pcBooking())
                    {
                        // If disabled the user wants a room. Check the time, is it occupied? If it's not then add it to the drop down list
                        if (!IsTimeOccupied(occupiedTimes, tempTimes[i]))
                            ddlEndTime.Items.Add(BookingFormHelper.FormatTimeSpan(tempTimes[i]));
                        else break; // As soon as we find one that is added - exit the loop.
                    }
                    else
                    {
                        // The user wants to book a pc. Therefore, we can check to see if there is a spare pc in the given time slot

                        // Get the number of booked pcs at the given time slot.
                        int booked = occupiedTimes.FindAll(x => x == tempTimes[i]).Count;

                        // Subract the total number of pcs in the room against what is booked. If there are pcs free then add the time
                        // otherwise exit the loop
                        if (pcNumbers - booked < 1) break;
                        else ddlEndTime.Items.Add(BookingFormHelper.FormatTimeSpan(tempTimes[i]));
                    }
                }
            }
            else
            {
                // If the is no time slots in the list then display a message there is no time free.
                ddlEndTime.Items.Add("No Times Avaiable For This Date");
            }

            // Refresh the number of booked pcs
            RefreshPCsBooked();
        }

        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>ShowPopup();</script>");
            confirmation.Text = message;
        }

        protected void okayButton(object sender, EventArgs e)
        {
            
        }

        private bool pcBooking()
        {
            try
            {
                return Convert.ToBoolean(Session["pcBooking"].ToString());
            }
            catch
            {
                return false;
            }
        }

        private void setPcBooking(bool pcBooking)
        {
            Session["pcBooking"] = pcBooking;
        }


    }
}