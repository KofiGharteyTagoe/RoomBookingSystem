using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Stores the details of a booking slot. This includes the name of the booker and the start and finish times
/// </summary>
public class BookingData
{
    // The start and finish times of the office day. Time slots can be booked within these times
    public static int START_HOUR = 9, START_MINUTE = 00;
    public static int FINISH_HOUR = 17, FINISH_MINUTE = 30;

    // ID value of the booking from the data base. We need this to get more information from the booking.
    public int id { get; private set; }
    public int userId { get; private set; }
    // Name of the person who has booked the room.
    public string name { get; private set; }
    // The start time
    public DateTime startTime{ get; private set; }
    // The end time
    public DateTime endTime { get; private set; }
    // The room that is booked (1-3)
    public int roomNumber {get; private set;}

    public int pcBooking { get; private set; }

    // Information about the booking
    public string info { get; private set; }

    #region constructors

    /*
     * Various constructors based upon the values inputted. Creating a new booking without specify the date will use the current date
     * The data is passed to the 'SEtup' method to set the values up
     */
    
    // No date provided - Use todays date
    public BookingData(int bookingId, int room, string name,  int startHour, int startMinute, int finishHour, int finishMinute, string info, int userID, int pcBooking)
    {
        Setup(bookingId, room, name, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHour, startMinute, finishHour, finishMinute, info, userID, pcBooking);
    }

    // Year, month and day provided
    public BookingData(int bookingId, int room, string name, int year, int month, int day, int startHour, int startMinute, int finishHour, int finishMinute, string info, int userID, int pcBooking)
    {
        Setup(bookingId, room, name, year, month, day, startHour, startMinute, finishHour, finishMinute, info, userID, pcBooking);
    }

    // No year provided - use the current year
    public BookingData(int bookingId, int room, string name, int month, int day, int startHour, int startMinute, int finishHour, int finishMinute, string info, int userID, int pcBooking)
    {
        Setup(bookingId, room, name, DateTime.Now.Year, month, day, startHour, startMinute, finishHour, finishMinute, info, userID, pcBooking);
    }

    // No month or year provided - use current values
    public BookingData(int bookingId, int room, string name, int day, int startHour, int startMinute, int finishHour, int finishMinute, string info, int userID, int pcBooking)
    {
        Setup(bookingId, room, name, DateTime.Now.Year, DateTime.Now.Month, day, startHour, startMinute, finishHour, finishMinute, info, userID, pcBooking);
    }


    // No month or year provided - use current values
    public BookingData(int bookingId, int room, string name, DateTime startDate, int finishHour, int finishMinute, string info, int userID, int pcBooking)
    {
        Setup(bookingId, room, name, startDate, finishHour, finishMinute, info, userID, pcBooking);
    }

    // Pass a date
    public BookingData(int bookingId, int room, string name, DateTime startDate, int endHour, int endMinute)
    {
        id = bookingId;
        roomNumber = room;
        this.name = name;
        startTime = startDate;
        endTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, endHour, endMinute, 0);
    }

    #endregion

    /*
     * Sets the variables based upon the given values
     */
    private void Setup(int bookingId, int room, string name, int year, int month, int day, int startHour, int startMinute, int finishHour, int finishMinute, string info, int userID, int pcBooking)
    {
        this.name = name;
        roomNumber = room;
        id = bookingId;
        this.startTime =   SetDate(year, month, day, startHour, startMinute);
        this.endTime =     SetDate(year, month, day, finishHour, finishMinute);
        this.info = info;
        this.userId = userID;
        this.pcBooking = pcBooking;
    }

    /*
 * Sets the variables based upon the given values
 */
    private void Setup(int bookingId, int room, string name, DateTime startDate, int finishHour, int finishMinute, string info, int userID, int pcBooking)
    {
        this.name = name;
        roomNumber = room;
        id = bookingId;
        this.startTime = startDate;
        this.endTime = SetDate(startDate.Year, startDate.Month, startDate.Day, finishHour, finishMinute);
        this.info = info;
        this.userId = userID;
        this.pcBooking = pcBooking;
    }

    /*
     * Based upon the passed year, month, day, hour and minute values create and return a new DateTime
     */
    private DateTime SetDate(int year, int month, int day, int hour, int minute)
    {
        return new DateTime(year, month, day, hour, minute, 0);
    }
    
    /*
     * Returns the current DateTime
     */
    public DateTime GetDate()
    {
        return startTime;
    }
}
