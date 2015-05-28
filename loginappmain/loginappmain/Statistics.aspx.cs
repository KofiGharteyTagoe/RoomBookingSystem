using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.DataVisualization.Charting;
using LineGraphStatistics;

namespace LoginAppmain
{
    public partial class Statistics : System.Web.UI.Page
    {

        // Used only when creating charts to identify which type of chart to create.
        enum ChartType { Line, Column };

        // This allows access to the correct data for the charts to display.
        DataAccessor da = DataAccessor.Instance;

        protected void Page_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;

            if (!Page.IsPostBack)
            {
                for (int i = 0; i < 5; i++)
                {
                    YearDropDown.Items.Add((year - i).ToString());
                }
            }
        }

        protected void AmountOfUsersChart_Load(object sender, EventArgs e)
        {

            createChart(AmountOfUsersChart, ChartType.Column, "Amount of users", da.xUserGroups, da.getAmountOfUsers(), "User groups", "Amount");
        }
        protected void AverageUsageChart_Load(object sender, EventArgs e)
        {
            createChart(AverageUsageChart, ChartType.Column, "Average usage amongst user groups", da.xUserGroups, da.getAverageUsage(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1)), "User groups", "Minutes");
        }
        protected void BookingsChart_Load(object sender, EventArgs e)
        {
            createChart(BookingsChart, ChartType.Column, "Bookings attempted", da.xBookingTypes, da.getBookings(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1)), "Bookings type", "Value");
        }
        protected void RoomBookingsChart_Load(object sender, EventArgs e)
        {
            createChart(RoomBookingsChart, ChartType.Column, "Amount of room bookings", da.xRoomNumbers, da.getRoomBookings(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1)), "Rooms", "Times booked");
        }
        protected void PCBookingsChart_Load(object sender, EventArgs e)
        {
            createChart(PCBookingsChart, ChartType.Column, "Amount of PC bookings", da.xRoomNumbers, da.getPCBookings(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1)), "Rooms", "PCs booked");
        }
        protected void UserActivityChart_Load(object sender, EventArgs e)
        {
            createChart(UserActivityChart, ChartType.Column, "User activity", da.xUserGroups, da.getUserActivity(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1)), "Users", "Minutes");
        }

        private void createChart(Chart chart1, ChartType type, String title, String[] xName, int[] yVal, String xTitle, String yTitle)
        {
            chart1.Series.Add(new Series());
            loadChartData(chart1, xName, yVal);

            if (type == ChartType.Column)
            {
                chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            }
            else if (type == ChartType.Line)
            {
                chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
            }

            chart1.Series[0].IsValueShownAsLabel = true;

            chart1.ChartAreas.Add(new ChartArea());
            chart1.ChartAreas[0].AxisX.Title = xTitle;
            chart1.ChartAreas[0].AxisY.Title = yTitle;

            chart1.ChartAreas[0].AxisX.Interval = 1;

            chart1.Titles.Add(title);
        }

        private void loadChartData(Chart chart1, String[] xName, int[] yVal)
        {
            chart1.Series[0].Points.DataBindXY(xName, yVal);
        }

        protected void ChooseChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ChooseChartDropDown.SelectedIndex)
            {
                case 1:
                    MultiView1.ActiveViewIndex = 0;
                    break;
                case 2:
                    MultiView1.ActiveViewIndex = 1;
                    break;
                case 3:
                    MultiView1.ActiveViewIndex = 2;
                    break;
                case 4:
                    MultiView1.ActiveViewIndex = 3;
                    break;
                case 5:
                    MultiView1.ActiveViewIndex = 4;
                    break;
                case 6:
                    MultiView1.ActiveViewIndex = 5;
                    break;
                default:
                    break;
            }
        }

        protected void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            DateTime range1 = calendar1.SelectedDates[0];
            DateTime range2 = calendar1.SelectedDates[calendar1.SelectedDates.Count - 1];

            if (range2.Equals(range1))
            {
                range2 = range1.AddDays(1).AddSeconds(-1);
            }

            chooseCorrectChart(range1, range2);
        }

        protected void YearDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Int32.Parse(YearDropDown.SelectedValue);
            DateTime range1 = new DateTime(year, 01, 01, 00, 00, 00);
            DateTime range2 = new DateTime(year, 12, 31, 23, 59, 59);

            switch (YearDropDown.SelectedIndex)
            {
                case 0:
                    chooseCorrectChart(range1, range2);
                    break;
                case 1:
                    chooseCorrectChart(range1, range2);
                    break;
                case 2:
                    chooseCorrectChart(range1, range2);
                    break;
                case 3:
                    chooseCorrectChart(range1, range2);
                    break;
                case 4:
                    chooseCorrectChart(range1, range2);
                    break;
                default:
                    break;
            }
        }

        private void chooseCorrectChart(DateTime range1, DateTime range2)
        {
            switch (ChooseChartDropDown.SelectedIndex)
            {
                case 1:
                    loadChartData(AmountOfUsersChart, da.xUserGroups, da.getAmountOfUsers());
                    break;
                case 2:
                    loadChartData(AverageUsageChart, da.xUserGroups, da.getAverageUsage(range1, range2));
                    break;
                case 3:
                    loadChartData(BookingsChart, da.xBookingTypes, da.getBookings(range1, range2));
                    break;
                case 4:
                    loadChartData(RoomBookingsChart, da.xRoomNumbers, da.getRoomBookings(range1, range2));
                    break;
                case 5:
                    loadChartData(PCBookingsChart, da.xRoomNumbers, da.getPCBookings(range1, range2));
                    break;
                case 6:
                    loadChartData(UserActivityChart, da.xUserGroups, da.getUserActivity(range1, range2));
                    break;
                default:
                    break;
            }
        }

        protected void okayButton(object sender, EventArgs e)
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
    }
}