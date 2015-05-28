using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BookingsOnDate
/// </summary>
public class BookingsOnDate
{
    public DateTime date;
    public int bookings;


	public BookingsOnDate(DateTime date, int bookings)
	{
        this.date = date;
        this.bookings = bookings;
	}



}