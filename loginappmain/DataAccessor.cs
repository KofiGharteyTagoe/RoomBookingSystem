using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LineGraphStatistics
{

    // This class is responsible for accessing the data to display in the charts. It is using an unsafe singelton
    // pattern when used with multiple threads.

    public class DataAccessor
    {
        private static DataAccessor da = null;

        private DataAccessor() { }

        public static DataAccessor Instance
        {
            get
            {
                if (da == null)
                {
                    da = new DataAccessor();
                }

                return da;
            }
        }

        public String[] xUserGroups
        {
            get { return new String[] { "Admin", "Novus Team", "Novus Trainees", "Non-Novus Capita Staff" }; }
        }
        public String[] xBookingTypes
        {
            get { return new String[] { "Successful", "Cancelled" }; }
        }
        public String[] xRoomNumbers
        {
            get { return new String[] { "1", "2", "3" }; }
        }

        public int[] getAmountOfUsers()
        {
            int admins = DatabaseConnections.GetNumbersPerGroup(1);
            int novus = DatabaseConnections.GetNumbersPerGroup(2);
            int trainees = DatabaseConnections.GetNumbersPerGroup(3);
            int nonNovus = DatabaseConnections.GetNumbersPerGroup(4);

            return new int[] { admins, novus, trainees, nonNovus };
        }

        public int[] getAverageUsage(DateTime range1, DateTime range2)
        {
            int admins = getGroupAverageUsage(1, range1, range2);
            int novus = getGroupAverageUsage(2, range1, range2);
            int trainees = getGroupAverageUsage(3, range1, range2);
            int nonNovus = getGroupAverageUsage(4, range1, range2);

            return new int[] { admins, novus, trainees, nonNovus };
        }

        private int getGroupAverageUsage(int group, DateTime range1, DateTime range2)
        {

            return DatabaseConnections.GetGroupActivity(group, range1, range2);

            //try
            //{
            //    return Convert.ToInt32(dt.Rows[0][0]);

            //}
            //catch (InvalidCastException ex)
            //{
            //    ex.ToString();
            //    return 0;
            //}

        }

        public int[] getBookings(DateTime range1, DateTime range2)
        {
            DataTable dt = DatabaseConnections.GetBookings(range1, range2);
            int successfulBookings = dt.Rows.Count;

            DataTable dt2 = DatabaseConnections.GetCancelledBookings(range1, range2);
            int cancelledBookings = dt2.Rows.Count;

            return new int[] { successfulBookings, cancelledBookings }; ;
        }

        public int[] getRoomBookings(DateTime range1, DateTime range2)
        {
            int room1 = DatabaseConnections.getRoomBookings(1, range1, range2);
            int room2 = DatabaseConnections.getRoomBookings(2, range1, range2);
            int room3 = DatabaseConnections.getRoomBookings(3, range1, range2);

            return new int[] { room1, room2, room3 };
        }

        public int[] getPCBookings(DateTime range1, DateTime range2)
        {
            int room1 = DatabaseConnections.getPCBookings(1, range1, range2);
            int room2 = DatabaseConnections.getPCBookings(2, range1, range2);
            int room3 = DatabaseConnections.getPCBookings(3, range1, range2);

            return new int[] { room1, room2, room3 };
        }

        public int[] getUserActivity(DateTime range1, DateTime range2)
        {
            int admins = DatabaseConnections.GetActivityPerGroup(1, range1, range2);
            int novus = DatabaseConnections.GetActivityPerGroup(2, range1, range2);
            int trainees = DatabaseConnections.GetActivityPerGroup(3, range1, range2);
            int nonNovus = DatabaseConnections.GetActivityPerGroup(4, range1, range2);

            return new int[] { admins, novus, trainees, nonNovus };
        }
    }
}