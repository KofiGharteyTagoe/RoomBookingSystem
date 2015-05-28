using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StartAndFinishTimes
/// </summary>
public class StartAndFinishTimes
{
    public DateTime start, finish;
	public StartAndFinishTimes(DateTime startTime, DateTime finishTime)
	{
        this.start = startTime;
        this.finish = finishTime;
	}
}