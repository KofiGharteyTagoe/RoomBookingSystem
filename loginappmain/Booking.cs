using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrintView
{
    public class Booking
    {
        DateTime date;
        public TimeSpan start, end;
        bool pcBooking;
        string booker;
        string description;


        public Booking(DateTime date, TimeSpan start, TimeSpan end, string booker="", string description="")
        {
            this.pcBooking=pcBooking;
            this.date = date;
            this.start = start;
            this.end = end;
            this.booker = booker;
            this.description = description;
        }

        public string ToString()
        {
            if (booker.Length>0)
                return printTime(start) + "-" + printTime(end) + " " + booker + ": " + description;
            else
                return printTime(start) + "-" + printTime(end);
        }

        private string printTime(TimeSpan time)
        {
            return time.Hours.ToString().PadLeft(2, '0') + ":" + time.Minutes.ToString().PadRight(2, '0');
        }
    }
}