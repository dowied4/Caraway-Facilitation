using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;


namespace _395project.App_Code
{
    public class GetWeekOfMonth
    {
        //Gets the number of the week in the current month
        public static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }

        //Gets the week of the year (1-52)
        public static int GetWeekOfYear(DateTime dt)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt,
                 CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }


        //Returns the week of the year that the first monday occurs on. For stats page to find stats by each week (The week of year
        //for each week in the month
        public static int FirstMonday(int month)
        {
            DateTime dt = new DateTime(DateTime.Now.Year, month, 1);
            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = new DateTime(DateTime.Now.Year, month, dt.Day + 1);
            }
            return GetWeekOfYear(dt);
        }

        //Gets the number of weeks in a month (Used to calculate the number of hours a family needs to volunteer that month
        public static Double MondaysInMonth(DateTime thisMonth)
        {
            Double mondays = 0;
            int month = thisMonth.Month;
            int year = thisMonth.Year;
            int daysThisMonth = DateTime.DaysInMonth(year, month);
            DateTime beginingOfThisMonth = new DateTime(year, month, 1);
            for (int i = 0; i < daysThisMonth; i++)
                if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                    mondays++;
            return mondays;
        }
    }
}