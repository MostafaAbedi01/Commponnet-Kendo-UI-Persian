using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CommonLibrary;

namespace Mehr
{
    public static class PersianDateTimeExtensions
    {
        public static PersianDateTime ToPersianDateTime(this DateTime value)
        { return PersianDateTime.FromGorgian(value); }

        public static PersianDateTime GetStartOfMonth(this PersianDateTime value)
        {
            value.Day = 1;
            return value;
        }

        public static PersianDateTime GetEndOfMonth(this PersianDateTime value)
        {
            value.Day = value.GetDaysInCurrentMonth();
            return value; 
        }

        public static int GetDaysInCurrentMonth(this PersianDateTime value)
        {
            return PersianDateTime.Calendar.GetDaysInMonth(value.Year, value.Month);
        }

        public static bool IsLastDayOfCurrentMonth(this PersianDateTime value)
        {
            return PersianDateTime.Calendar.GetDaysInMonth(value.Year, value.Month) == value.Day;
        }

        public static int[] GetDayOfMonthList(this PersianDateTime value)
        {
            int[] dayOfMonths = new[] { value.Day };
            if (value.IsLastDayOfCurrentMonth())
            {
                int daysInCurrentMonth = value.GetDaysInCurrentMonth();
                if (daysInCurrentMonth == 29)
                    dayOfMonths = new[] { 29, 30, 31 };
                else if (daysInCurrentMonth == 30)
                    dayOfMonths = new[] { 30, 31 };
            }
            return dayOfMonths;
        }

        public static PersianDateTime GetEndOfDay(this PersianDateTime value)
        {
            value.Hour = 23;
            value.Minute = 59;
            value.Second = 59;
            return value;
        }
    }
}
