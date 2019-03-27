using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace TimeReg.Tools
{
    public class CalendarHelper
    {
        public static int GetWeekNr()
        {
            //Sets up a calendar to find the current date.
            var culture = CultureInfo.CurrentCulture;
            int weekNo = culture.Calendar.GetWeekOfYear(
            DateTime.Now,
            culture.DateTimeFormat.CalendarWeekRule,
            culture.DateTimeFormat.FirstDayOfWeek);

            return weekNo;
        }

        public int GetYear()
        {
            return DateTime.Now.Year;

        }

    
    }
}