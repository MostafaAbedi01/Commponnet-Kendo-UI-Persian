using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary;

namespace Mehr
{
    public class PersianDateRange : ICloneable
    {
        public PersianDateTime Start { get; set; }
        public PersianDateTime End { get; set; }

        public TimeSpan TimeSpan { get { return End.ToGorgian().Subtract(Start.ToGorgian()); } }

        public PersianDateRange(PersianDateTime start, PersianDateTime end)
        {
            Start = (PersianDateTime)start.Clone();
            End = (PersianDateTime)end.Clone();
            if (!IsValid(start, end))
                throw new NotSupportedException("Start date shoulb be less than end date.");
        }

        public bool IsInRangeExclusive(PersianDateTime value)
        {
            return Start < value && value < End;
        }

        public static bool IsValid(PersianDateTime start, PersianDateTime end)
        { return start.ToGorgian() <= end.ToGorgian(); }


        [Obsolete]
        public static PersianDateRange GetFromStartOfYear(PersianDateTime end = null)
        {
            return GetYear(end);
        }

        public static PersianDateRange GetYear(PersianDateTime end = null)
        {
            var endDate = end ?? PersianDateTime.Now;
            var startDate = new PersianDateTime(endDate.Year, 1, 1, 0, 0, 0, 0);
            return new PersianDateRange(startDate, endDate);
        }

        public static PersianDateRange GetMonth(PersianDateTime end = null)
        {
            var endDate = end ?? PersianDateTime.Now;
            var startDate = new PersianDateTime(endDate.Year, endDate.Month, 1, 0, 0, 0, 0);
            return new PersianDateRange(startDate, endDate);
        }

        public static PersianDateRange GetDay(PersianDateTime end = null)
        {
            var endDate = end ?? PersianDateTime.Now;
            var startDate = new PersianDateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0, 0);
            return new PersianDateRange(startDate, endDate);
        }


        public virtual object Clone()
        {
          return  new PersianDateRange((PersianDateTime)this.Start.Clone(), (PersianDateTime)this.End.Clone());
        }
    }
}
