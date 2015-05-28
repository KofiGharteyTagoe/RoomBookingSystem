using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class HomePage : System.Web.UI.Page
    {

        // Setup two DateTime variables to keep track of the time. It doesn't matter about the date variables we're only interested in the Hour & Minute
        private DateTime time = new DateTime(2014, 01, 01, BookingData.START_HOUR, BookingData.START_MINUTE, 0);
        private DateTime finishTime = new DateTime(2014, 01, 01, BookingData.FINISH_HOUR, BookingData.FINISH_MINUTE, 0);

        // We have 7 tables on the screen. Keep this value at 7 or add more tables.
        private int daysAheadToShow = 5;

        // Store a disctonary of the timeslots we have. For example, a time slot: 15.00, 15.15 etc. WE will use the key to know what row the time slot is on in the grid. EG: 9.00 will be stored have key 0 and thus will be at row 0 in the table
        private Dictionary<int, Time> timeSlots = new Dictionary<int, Time>();
        // Keep a list of ColourCells that we add to as we populate the grid. Colour formatting is added at the end of the processing .
        private List<ColourCell> cells = new List<ColourCell>();

        // Store a list of the tables. This includes a reference to a table and its header.
        private DayViewer[] tables;

        // Lets us apply colours to the coloumns. Index 0 relates to the room 1, 2 = room 2, 3=room3
        private static Color[] colours = { Color.DarkTurquoise, Color.IndianRed, Color.IndianRed, Color.DarkRed, Color.Cyan };

        // Constants to determine the visual properties of the grid/header
        private const int TABLE_WIDTH = 150, FONT_SIZE = 11;
        private const bool BOLD = false;
        private const float TIME_INTERVAL = 15f;

        // A list of the bookings
        private BookingData[] bookings;
        DateTime selectedDate = DateTime.Now;
        private List<int> rooms = new List<int>();

        // An array for the DataTables. Data will be stored in here until they're bound to the GridView
        DataTable[] days = new DataTable[7];

        public const string EDITBOOKING = "EditBooking.aspx";

        public const string NEWBOOKINGPAGE = "AddBooking.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }


            // The date to display as first date in the table.
            selectedDate = DateTime.Now;

            // If there is a request in the URL, retrieve it
            if (Request.QueryString.Count > 0)
            {
                // If it is a valid date format, set it as the selected date, otherwise keep the default date.
                try
                {
                    selectedDate = DateTime.Parse(Request.QueryString[0]);
                    while (selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday)
                        selectedDate = selectedDate.AddDays(1);
                }
                catch
                {
                    selectedDate = DateTime.Now;
                }
            }
            
            // Setup the table for storing the time slots
            DataTable dt = new DataTable();
            dt.Columns.Add("Time" + Environment.NewLine + "slot");
            grdTimeSlots.HeaderStyle.Font.Size = FONT_SIZE;
            grdTimeSlots.HeaderStyle.Font.Bold = BOLD;

            // Keep track of the current row we are on.     
            time = new DateTime(2014, 01, 01, BookingData.START_HOUR, BookingData.START_MINUTE, 0);
            // We need to add the time from the start time to the finish time. Each time we add the specificed interval (default 15mins). This loop will add rows: 9.00, 9.15, 9.30, 9.45 etc
            // Add the interval to 15mins. Normally the loop will stop when it reaches 5.30 (so the last row will be 5.15)  so add an extra 15 so it runs for one more iteration.
            while (time < finishTime.AddMinutes(TIME_INTERVAL))
            {
                DataRow dr = dt.NewRow();
                dr[0] = time.ToShortTimeString();
                dt.Rows.Add(dr);

                if (!timeSlots.ContainsKey(timeSlots.Count))
                    timeSlots.Add(timeSlots.Count, new Time(time));
                // Add 15mins and loop again
                time = time.AddMinutes(TIME_INTERVAL);
            }
            // Bind this data
            grdTimeSlots.DataSource = dt;
            grdTimeSlots.DataBind();


            // Add the visual properties to the tables
            SetupTables();
            LoadData(selectedDate);
            Session["Date"] = selectedDate.ToShortDateString();

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
 * Load the data and put it into the tables.
 */
        private void LoadData(DateTime date)
        {
            Dictionary<int, int> roomIds = new Dictionary<int, int>();
            DataTable dt = DatabaseConnections.GetRooms();
            int index = 1;
            foreach (DataRow dr in dt.Rows)
            {
                int id = 0;
                try
                {
                    id = DatabaseConnections.GetRoomID(dr["RoomName"].ToString());
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

                try 
                { 
                    roomIds.Add(id, index); 
                }
                catch { continue; }
                index++;
            }

            // Load the data into the array
            bookings = GetData(date, date.AddDays(daysAheadToShow));

            foreach (DayViewer dv in tables)
            {
                dv.Clear();
                dv.DataSource(null);
            }
            for (int i = 0; i < days.Length; i++)
            {
                days[i] = new DataTable();
                days[i] = Setup(days[i], timeSlots.Count);
            }

            // Now sort through the bookings and add them to the datatables

            DateTime searchDate = date;
            for (int k = 0; k < daysAheadToShow; k++)
            {
                // Each iterations we want to look for the next day. We start on day 0, then the next day and then the next.

                if (searchDate.DayOfWeek == DayOfWeek.Saturday || searchDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    searchDate = searchDate.AddDays(1);
                    k--;
                    continue;
                }

                tables[k].header.Text = searchDate.ToShortDateString();
                tables[k].day.Text = searchDate.DayOfWeek.ToString();


                // A linq statement. Search through the list getting the bookings where the dates match the search date
                var daysBooking = from booking in bookings
                                  where booking.GetDate().Date == searchDate.Date
                                  orderby booking.GetDate()
                                  select booking;
                int swap = 0;
                // Loop through the bookings for the current search date.
                foreach (BookingData bd in daysBooking)
                {
                    // Now match the start time of the booking against the timeslots we saved earlier (the time slots are dictionary of index,value)
                    for (int j = 0; j < timeSlots.Count; j++)
                    {
                        // Check the start time against the time slots we have. If a match is found, add it to the table.
                        if (bd.GetDate().Hour == timeSlots[j].GetHour() && bd.GetDate().Minute == timeSlots[j].GetMinute())
                        {

                            // Ontain the row that the time slot is on (for example, 9.15 will return value 1)
                            int row = (int)(timeSlots.FirstOrDefault(x => x.Value == timeSlots[j]).Key);

                            // How long is their booking, for each 15min time slot their booking occupies we want to add that to our colour cell list
                            // Store the satart time of the booking
                            DateTime tempTime = bd.GetDate();
                            // Loop through until we reach the end time of their booking (storing each cell as we go)
                            while (!tempTime.Equals(bd.endTime))
                            {

                                int pcbooking = 0;
                                if (bd.pcBooking > 0)
                                    pcbooking = 1;

                                // Get the row of the time slot is on
                                Time temp = new Time(tempTime);
                                row = (int)(timeSlots.FirstOrDefault(x => x.Value.Same(temp)).Key);
                                int id = 0;
                                roomIds.TryGetValue(bd.roomNumber, out id);
                                // Store the properties into our list of 'ColourCell'. 
                                cells.Add(new ColourCell(bd.id,
                                                            row, // The row the booking is on
                                                            k,  // The day we are on. (determines what table to go in)
                                                            colours[pcbooking], // Get the colour the cell will be
                                                            bd.roomNumber, // Store the room number of the booking
                                                            id));

                                // Increment the time
                                tempTime = tempTime.AddMinutes(15);
                                TimeSpan difference = tempTime.Subtract(bd.endTime);
                                if (difference.TotalMinutes > 0)
                                    break;
                            }
                            swap++;
                            if (swap > 1) swap = 0;
                        }
                    }

                }
                // Bind the data to the table
                tables[k].DataSource(days[k]);
                for (int j = 0; j < tables[k].table.Columns.Count; j++)
                {
                    for (int i = 0; i < tables[k].table.Rows.Count; i++)
                    {
                        tables[k].table.Rows[i].Cells[j].ID = rooms[j].ToString();
                    }
                }
                searchDate = searchDate.AddDays(1);
            }

            // Add the colours to the cells.
            Style s2 = new Style();
            for (int i = 0; i < cells.Count; i++)
            {
                try
                {
                    // Get the colour of the current cell
                    s2.BackColor = cells[i].colour;

                    // Using the 'day' select the appropitae table. Then using the 'row' and 'room' select the right row and column. Then apply the style
                    tables[cells[i].day].table.Rows[cells[i].row].Cells[cells[i].column - 1].MergeStyle(s2);
                    tables[cells[i].day].table.Rows[cells[i].row].Cells[cells[i].column - 1].ID = cells[i].id.ToString();
                }
                catch { continue; }
            }
        }


        /* 
         * Get the data from the sql and store it in an array of BookingData
         */
        private BookingData[] GetData(DateTime date1, DateTime date2)
        {
            // Get the data and pass in two dates. This saves memory and processing time than retriveing all events at once.
            // This way, we can just retrieve the data we need, 7 days at a time.
            DataTable dt = new DataTable();
            try
            {
                dt = DatabaseConnections.GetBookings(date1, date2);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            // List to temporary store bookings data
            List<BookingData> data = new List<BookingData>();
            // Temporary start and end times
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            // Temp id numbers.
            int bookingID = 0, roomID = 0, userID = 0, pcsBooked = 0;

            // Go through each row in the and parse the data. Store it into the variables.
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    startDate = DateTime.Parse(dr["StartDateTime"].ToString());
                    endDate = DateTime.Parse(dr["EndDateTime"].ToString());
                    bookingID = Int32.Parse(dr["EventID"].ToString());
                    roomID = Int32.Parse(dr["RoomID"].ToString());
                    userID = Int32.Parse(dr["UserID"].ToString());
                    pcsBooked = Int32.Parse(dr["PcsBooked"].ToString());
                }
                catch { continue; } // Should there be a value that can't be parsed. Skip onto the next row.

                // Add the data into the list
                string fullname = dr["ForeName"].ToString() + " " + dr["SurName"].ToString();
                data.Add(new BookingData(bookingID, roomID, fullname, startDate, endDate.Hour, endDate.Minute, dr["Description"].ToString(), userID, pcsBooked));
            }
            // Return the  list as an array.
            return data.ToArray();
        }

        /*
         * Setup the tables, adding the required amount of rows
         */
        private DataTable Setup(DataTable dt, int rowCount)
        {
            DataTable room = new DataTable();
            try
            {
               room =  DatabaseConnections.GetRooms();
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            int roomCount = room.Rows.Count;
            foreach (DataRow dr in room.Rows)
            {
                int id = Convert.ToInt32(dr["RoomID"].ToString());
                string name = dr["RoomName"].ToString();
                dt.Columns.Add(name);
                rooms.Add(id);
            }

            // Add the rows required.
            for (int i = 0; i < rowCount; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /*
         * Assigns each DayViewer in tables with their corrsonding grid, header and day label. It's easier to treat each as a group
         * since they are all related
         */
        private void SetupTables()
        {
            tables = new DayViewer[5];
            tables[0] = new DayViewer(grdDay1, lblDay1, lblMonday);
            tables[1] = new DayViewer(grdDay2, lblDay2, lblTuesday);
            tables[2] = new DayViewer(grdDay3, lblDay3, lblWednesday);
            tables[3] = new DayViewer(grdDay4, lblDay4, lblThursday);
            tables[4] = new DayViewer(grdDay5, lblDay5, lblFriday);

            // Apply formatting to the tables
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i] = FormatTables(tables[i]);
            }
        }

        /*
         * Formats the table to the same formatting
         */
        private DayViewer FormatTables(DayViewer day)
        {
            day.SetWidth(TABLE_WIDTH);
            day.table.HeaderStyle.Font.Size = FONT_SIZE;
            day.table.HeaderStyle.Font.Bold = BOLD;
            return day;
        }

        /*
         * Renders the data on the screen. Adds the mouseover functionality to each table
         */
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (DayViewer dy in tables)
            {
                AddMouseOver(dy.table);
            }
            base.Render(writer);
        }

        /*
         * Called when a cell is clicked. Displays a popup dialogue showing data about the booking
         */
        protected void RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Setup default row and index variables
            int rowIndex = 0;
            int cellIndex = 0;

            if (e.CommandName == "CellSelect")
            {
                // Unpack the arguments.
                String[] arguments = ((String)e.CommandArgument).Split(new char[] { ',' });

                // More safe coding: Don't assume there are at least 2 arguments.
                // (And ignore when there are more.)
                if (arguments.Length >= 2)
                {
                    // And even more safe coding: Don't assume the arguments are proper int values.               
                    int.TryParse(arguments[0], out rowIndex);
                    int.TryParse(arguments[1], out cellIndex);
                }

                // Get the ID assigned to the grid
                string value = (sender as GridView).Rows[rowIndex].Cells[cellIndex].ID;

                // Pass the id and save it as an int.
                int id = 0;
                int.TryParse(value, out id);



                // If the values greater than 0 it's a booking so continue.
                if (id > 0)
                {
                    // Linq through the list of booking and pull the booking data.
                    var daysBooking = from booking in bookings
                                      where booking.id == id
                                      select booking;

                    // Loop through the bookings for the current search date.
                    foreach (BookingData bd in daysBooking)
                    {
                        // Store the BookingID so we know what one the user is looking at.
                        Session["BookingID"] = bd.id;

                        // Render the JavaScript
                        DisplayPopUp(bd, timeSlots[rowIndex]);
                    }
                    // Execute the lightbox script on the webpage passing the javascript we made before.
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>lightbox_open(\"" + true + "\", \"" + "\");</script>");

                }
                else
                {
                    id = rooms[cellIndex];
                    DateTime date = new DateTime();
                    string[] argument = e.CommandArgument.ToString().Split(',');
                    int row = Convert.ToInt32(argument[0]);
                    Time time = timeSlots[row];
                    string table = e.CommandSource.ToString();
                    var daysBooking = from t in tables
                                      where t.table == e.CommandSource
                                      select t.header;

                    foreach (Label s in daysBooking)
                    {
                        try
                        {
                            date = DateTime.Parse(s.Text);
                        }
                        catch { }
                    }
                    date = date.AddHours(time.GetHour()).AddMinutes(time.GetMinute());
                    Response.Redirect(NEWBOOKINGPAGE + "?date=" + date + "&RoomID=" + id);
                }
            }
        }
        /*
         * Adds a hand house over and event to a grid where dta exists.
         * If a booking does't exist, don't add the mouse over or event.
         */
        private void AddMouseOver(GridView gv)
        {
            // Loop through each row
            foreach (GridViewRow row in gv.Rows)
            {
                // Loop through each cell
                foreach (TableCell cell in row.Cells)
                {
                    // Get and parse the id. If the id is greater than 0 then we know a booking exists. Otherqise, don't add any data to it.
                    //   int id = 0;
                    //   int.TryParse(cell.ID, out id);
                    // if (id > 0)
                    //   {
                    // Add a mouseover and the onclick
                    cell.Attributes["onmouseover"] = "this.style.cursor='pointer';";
                    cell.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv,
                                             String.Format("CellSelect${0},{1}", row.RowIndex, row.Cells.GetCellIndex(cell)));

                    // Register for event validation: This will keep ASP from giving nasty errors from
                    // getting events from controls that shouldn't be sending any.
                    Page.ClientScript.RegisterForEventValidation(gv.UniqueID, String.Format("CellSelect${0},{1}", row.RowIndex, row.Cells.GetCellIndex(cell)));
                    //   }                
                }
            }
        }

        /*
         * Takes a booking data and creates a JavaScript that will be displayed in the window. Add any JS code here (such as buttons)
         * Another note, double \\ are used so C# will ignore it. This way the \n are treated by the webpage
         */
        private void DisplayPopUp(BookingData bd, Time time)
        {
            DateTime selectedTime;
            try
            {
                TimeSpan times = new TimeSpan(time.GetHour(), time.GetMinute(), 0);
                selectedTime = new DateTime(bd.startTime.Date.Ticks + times.Ticks);
            }
            catch { return; }

            int userid = 0;
            try
            {
                userid = Int32.Parse(Session["UserId"].ToString());
            }
            catch { userid = 0; }

            bool canEdit = (userid == bd.userId);
            int accessLevel = 0;
            try
            {
                accessLevel = DatabaseConnections.GetAcessLevel(userid);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            if ( accessLevel== 1)
                canEdit = true;

            btnEdit.Visible = canEdit;
            
            lblRoom2.Text = bd.roomNumber.ToString();
            lblID2.Text = bd.id.ToString();
            lblDate2.Text = bd.startTime.ToShortDateString();
            lblSTime2.Text = bd.startTime.ToShortTimeString();
            lblETime2.Text = bd.endTime.ToShortTimeString();
            lblBooked2.Text = bd.name;

            int id = bd.roomNumber;
            int pcsBooked=0, pcsRoom=0;

            try

            {
                pcsBooked = DatabaseConnections.GetPCSBooked(selectedTime, id);
                pcsRoom = DatabaseConnections.GetPCsInRoom(bd.roomNumber);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }

            if (bd.pcBooking > 0)
            {
                lblType2.Text = "PC Booking. There are " + pcsBooked + "/" + pcsRoom + " PCs booked";
                if (pcsBooked >= pcsRoom)
                    btnMakeBooking.Visible = false;
                else
                    btnMakeBooking.Visible = true;

                lblDescription1.Visible = false;
                lblDescription2.Visible = false;
                lblBooked1.Visible = false;
                lblBooked2.Visible = false;

            }
            else
            {
                lblType2.Text = "Room booking";
                btnMakeBooking.Visible = false;
                lblDescription1.Visible = true;
                lblDescription2.Visible = true;
                lblBooked1.Visible = true;
                lblBooked2.Visible = true;
            }
            if (selectedTime < DateTime.Now)
            {
                btnEdit.Visible = false;
                btnMakeBooking.Visible = false;
            }
            lblDescription2.Text = bd.info;

        }


        protected void EditButtonClick(object sender, EventArgs e)
        {
            Response.Redirect(EDITBOOKING + "?BookingID=" + Session["BookingID"]);
        }

        protected void btnNextPrevious_Click(object sender, EventArgs e)
        {
            // Clears the cells.
            cells.Clear();

            // Setup temporary values 
            string date;
            DateTime time = DateTime.Now;

            // Retrieve the date stored in the SEssion and parse it. If it's unsuccessful use todays date.
            try
            {
                date = Session["Date"].ToString();
                time = DateTime.Parse(date);
            }
            catch
            {
                time = DateTime.Now;
            }

            // Check the ID of the button. Either we add days or subtract days depending on the button.
            if ((sender as Button).ID.Equals("btnNextWeek"))
                time = time.AddDays(7);
            else if ((sender as Button).ID.Equals("btnPreviousWeek"))
                time = time.AddDays(-7);
            else if ((sender as Button).ID.Equals("btnPreviousDay"))
            {
                time = time.AddDays(-1);
                while (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
                    time = time.AddDays(-1);
            }
            else if ((sender as Button).ID.Equals("btnNextDay"))
            {
                time = time.AddDays(1);
                while (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
                    time = time.AddDays(1);
            }

            // Load data
            // LoadData(time);
            Response.Redirect("HomePage.aspx?SelectedDate=" + time.ToShortDateString());

            // Save the current datetime as a session
            Session["Date"] = time.ToShortDateString();
        }

        protected void btnMakeBookingClick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            int id = 0;
            try
            {
                date = DateTime.Parse(lblDate2.Text + " " + lblSTime2.Text);
                id = Int32.Parse(lblRoom2.Text);
            }
            catch
            {
                return;
            }
            Response.Redirect(NEWBOOKINGPAGE + "?date=" + date + "&RoomID=" + id);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("PrintCalendar.aspx?date=" + selectedDate);
        }




    }
}