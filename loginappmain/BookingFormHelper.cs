using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BookingFormHelper
/// </summary>
public class BookingFormHelper
{
    /*
     * Gets the current date and returns it. This will either be the current date or a value stored in a request
     */
	public static string GetDate()
    {
        DateTime currentDate;
        TimeSpan startTime;
        var request = HttpContext.Current.Request;

        // If theres a request query get it...
        if (request.QueryString.Count > 0)
        {
            // If it is a valid date format, set it as the selected date, otherwise keep the default date.
            try
            {
                currentDate = DateTime.Parse(request.QueryString[0]).Date;
                startTime = DateTime.Parse(request.QueryString[0]).TimeOfDay;
            }
            catch
            {
                // If it can't be parsed. Use todays date
                currentDate = DateTime.Today.Date;
            }
        }
        else
        {
            // If there isn't a request, use todays date
            currentDate = DateTime.Today.Date;               
        }
        // Return it to the caller.
        return FormatDate(currentDate);
    }

    public static string FormatDate(DateTime date)
    {
        return date.Date.ToShortDateString();
    }

    public static TimeSpan getCurrentTime()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        // Get the current time and round it up to the nearest 15mins block.
        while (currentTime.Minutes % 15 != 0)
        {
            currentTime += new TimeSpan(0, 1, 0);
        }
        currentTime -= new TimeSpan(0, 0, 0, currentTime.Seconds, currentTime.Milliseconds);
        return currentTime;
    }

    public static string FormatTimeSpan(TimeSpan ts)
    {
        string time = ts.Hours + ":" + ts.Minutes;
        if (ts.Hours < 10)
        {
            time = time.PadRight(4, '0');
            return time.PadLeft(5, '0');
        }
        return time.PadRight(5, '0');
    }
}