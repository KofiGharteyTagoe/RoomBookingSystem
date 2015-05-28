using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrintView
{
    public partial class PrintCalendar : System.Web.UI.Page
    {
        string roomName = "Room 1";
        DateTime date;
        GridView[] grids;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            grids = new GridView[] { GridView1, GridView2, GridView3, GridView4, GridView5 };
            date = DateTime.Now;

            try
            {
                date = DateTime.Parse(Request["Date"].ToString());
            }
            catch { }
            string[] rooms = new string[1];
            try
            {
                rooms = DatabaseConnections.getRoomNames();
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            if(!IsPostBack)
            {
                foreach (string s in rooms)
                {
              
                        DropDownList1.Items.Add(s);
                }
            }

            refreshBookings(date);
        }

        private void Populate(GridView dg, Booking[] bookings, DateTime date)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < bookings.Length + 1; i++)
            {
                dt.Columns.Add(i + "");
            }

            DataRow dr = dt.NewRow();
            dr[0] = date.ToShortDateString()+ " " +  date.DayOfWeek.ToString();

            for (int i = 0; i < bookings.Length; i++)
            {
                dr[i + 1] = bookings[i].ToString();
            }
            dt.Rows.Add(dr);
            dg.DataSource = dt;
            dg.DataBind();

            
            Style[] styles = new Style[2];
            styles[0] = new Style();
            styles[0].BackColor = Color.LightCyan;
            styles[1] = new Style();
            styles[1].BackColor = Color.LightSteelBlue;

            int i2 = 0;
            foreach (TableCell cell in dg.Rows[0].Cells)
            {
                if (cell.Text.ToString().Length > 11)
                {
                    i2++;
                    if (i2 > 1) i2 = 0;
                    cell.ApplyStyle(styles[i2]);
                }
            }

            Style s1 = new Style();
            s1.BackColor = Color.Purple;
            s1.Font.Bold = true;
            s1.ForeColor = Color.White;

            dg.Rows[0].Cells[0].ApplyStyle(s1);
            dg.Rows[0].Cells[0].Width = 100;
            dg.Rows[0].Height = 100;

        }

        private Booking[] GetBookings(DateTime date)
        {
            List<Booking> bookings = new List<Booking>();

            DataTable dt = DatabaseConnections.GetBookingsOfRoom(DropDownList1.SelectedValue, date, false);
            foreach (DataRow dr in dt.Rows)
            {
                DateTime startDate = DateTime.Parse(dr["StartDateTime"].ToString());
                DateTime endDate = DateTime.Parse(dr["EndDateTime"].ToString());
                string description = dr["Description"].ToString();
                string userName = dr["UserName"].ToString();
                TimeSpan endTime = endDate.TimeOfDay;
                TimeSpan startTime = startDate.TimeOfDay;
                Booking book = new Booking(startDate.Date, startTime, endTime, userName, description);
                
                bookings.Add(book);

            }
            GetPCBookings(ref bookings, date);

            bookings = bookings.OrderBy(x => x.start).ToList();

            List<TimeSpan> bookingSlots = new List<TimeSpan>();
            TimeSpan time = new TimeSpan(9, 0, 0);
            do
            {
                bookingSlots.Add(time);
                time = time += new TimeSpan(0, 15, 0);

            } while (time <= new TimeSpan(17, 30, 0));


            foreach (Booking booking in bookings)
            {
                for (int i = 0; i < bookingSlots.Count; i++)
                {
                    if (bookingSlots[i] >= booking.start && bookingSlots[i] < booking.end)
                    {
                        bookingSlots[i] = TimeSpan.Zero;
                    }       
                }
            }

            TimeSpan start = TimeSpan.Zero, end = TimeSpan.Zero;
            int index = 0;
            while (index < bookingSlots.Count)
            {
                while (index < bookingSlots.Count)
                {
                    if (bookingSlots[index] != TimeSpan.Zero)
                    {
                        start = bookingSlots[index];
                        break;
                    }
                    index++;
                }

                while (index < bookingSlots.Count)
                {
                    if (bookingSlots[index] == TimeSpan.Zero )
                    {
                        end = bookingSlots[index - 1] + new TimeSpan(0, 15, 0);
                        if (!start.Equals(end))
                        bookings.Add(new Booking(date, start, end));
                        break;
                    }
                    if (bookingSlots[index] >= new TimeSpan(17,30,0))
                    {
                         
                        if (!start.Equals(bookingSlots[index]))
                            bookings.Add(new Booking(date, start, new TimeSpan(17, 30, 0)));
                        break;
                    }
                    index++;
                }
                index++;
            }
            bookings = bookings.OrderBy(x => x.start).ToList();
            for (int i = 0; i < bookings.Count; i++)
            {
                
            }

            return bookings.OrderBy(x => x.start).ToArray();
        }

        private Booking[] GenerateBookings(DateTime date)
        {
            TimeSpan[] increments = { new TimeSpan(0, 15, 0), new TimeSpan(0, 30, 0), new TimeSpan(0, 45, 0), new TimeSpan(1, 0, 0), new TimeSpan(1, 15, 0), new TimeSpan(1, 30, 0), new TimeSpan(1, 45, 0), new TimeSpan(2, 0, 0) };
            string[] names = {"", "", "Tom", "Peter", "George", "Mark", "Zoe", "Mary", "Matt", "Charlotte", "Amy", "Mark", ""};
            string[] descriptions = {"Pc Booking", "Meeting", "Work", "Presentation", "Interview", "Meeting"};

            List<Booking> bookings = new List<Booking>();
            Random rad = new Random((int)date.Ticks);
            TimeSpan time = new TimeSpan(9,0,0);
            TimeSpan end = new TimeSpan(17,30,0);
            int count = (int)DateTime.Now.Ticks + (int)date.Ticks;
            bool last = false;
            do {
                Random rand = new Random(count++);
                int rt = rand.Next(0, increments.Length);
               
                string name="", description="";

                do
                {
                    rand = new Random(count++);
                    int f = rand.Next(names.Length);
                    name = names[f];
                } while (name.Length < 1 && last);
                
                if (name.Length > 0)
                {
                    rand = new Random(count++);
                    int i = rand.Next(descriptions.Length);
                    description = descriptions[i];
                    last = false;
                    
                }
                else
                {
                    last = true;
                }

                TimeSpan endt = time.Add(increments[0]);
                if (endt > end) endt = end;
                Booking b1 = new Booking
                (date, time, endt, name, description
                );
                bookings.Add(b1);
                time = endt;
                }while (time<end);

            return bookings.ToArray();
        }

        private void GetPCBookings(ref List<Booking> bookings, DateTime date)
        {
            List<TimeSpan> times = new List<TimeSpan>();
            TimeSpan incrementer = new TimeSpan(0, 15, 0);
            DataTable dt = DatabaseConnections.GetBookingsOfRoom(roomName, date, true);
            foreach (DataRow dr in dt.Rows)
            {
                DateTime startDate = DateTime.Parse(dr["StartDateTime"].ToString());
                DateTime endDate = DateTime.Parse(dr["EndDateTime"].ToString());
                TimeSpan endTime = endDate.TimeOfDay;
                TimeSpan startTime = startDate.TimeOfDay;

                do
                {
                    times.Add(startTime);
                    startTime += incrementer;

                } while (startTime < endTime);

            }
            if (times.Count>0)
            {
                times = times.Select(x=>x).Distinct().OrderBy(x => x).ToList<TimeSpan>();
                TimeSpan start = times[0], end = times[1];
                for (int i = 1; i < times.Count; i++)
                {
                    if (times[i] - start > incrementer)
                    {
                        bookings.Add(new Booking(date, start, times[i-1]+incrementer, "System", "PC Bookings"));
                    }
                }
            }

        }

        private void refreshBookings(DateTime date)
        {
            date = date.AddDays(-1);
            for (int i = 0; i < 5; i++)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    i--;
                    continue;
                }
                Populate(grids[i], GetBookings(date), date);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshBookings(date);
        }
    }
}