using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A simple class to store a given time noting the hour and the minute. These time variables can be compared. This makes things easier when we are working
/// with a time than the DateTime class.
/// </summary>
public class Time
{
    // The current hour and minute.
    private int hour, minute;

    /*
     * Constructor to set the hour and minute based upon the given input. First, validate the passed variables to 
     * ensure they conform to the hour/minute standard
     */
	public Time(int hour, int minute)
	{
        this.hour = CheckHour(hour);
        this.minute = CheckMinute(minute);
	}

    /*
     * Set the hour and minutebased upon the values obtainned from a DateTime variable. We are trusting that the 
     * hour and minute variables provided in the class conform to the hour/minute standard
     */
    public Time(DateTime time)
    {
        this.hour = time.Hour;
        this.minute = time.Minute;
    }

    /*
     * Checks a given time against the current to see if it is the same as this objcet
     */
    public bool Same(Time time2)
    {
        if (time2 != null)
        {
            if (hour == time2.GetHour() && minute == time2.GetMinute())
                return true;
        }
        return false;
    }

    /*
     * Returns the hour value
     */
    public int GetHour()
    {
        return hour;
    }

    /*
     * Returns the minute value
     */
    public int GetMinute()
    {
        return minute;
    }

    /*
     * Checks a given minute value to ensure that it is within 0-59 format
     */
    private static int CheckMinute(int minute)
    {
        if (minute > 59) minute = 59;
        else if (minute < 0) minute = 0;

        return minute;
    }

    /*
     * Checks a given hour value to ensure it's within 0-24 format
     */
    private static int CheckHour(int hour)
    {
        if (hour > 24) hour = 24;
        else if (hour < 0) hour = 0;

        return hour;
    }
}