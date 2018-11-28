using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Mehr;

namespace CommonLibrary
{
    public class PersianDateTime : IFormattable, ICloneable
    {
        public const long One6Zero = 1000000;
        public static readonly PersianCalendar Calendar = new PersianCalendar();
        public static readonly string[] AbbreviatedDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
        public static readonly string[] DayNames = new string[] { "یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
        public static readonly string[] MonthNames = new string[] { "فروردین", "ارديبهشت", "خرداد", "تير", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        DateTime _dateTime;
        private DateTime DateTime
        {
            set
            {
                ValidateDate(value);
                _dateTime = value;
            }
            get { return _dateTime; }
        }

        public PersianDateTime(int year = 1000, int month = 1, int day = 1, int hour = 0, int minute = 0, int seconds = 0, int milisecond = 0)
            : this(Calendar.ToDateTime(year, month, day, hour, minute, seconds, milisecond)) { }

        private PersianDateTime(DateTime dateTime)
        {
            ValidateDate(dateTime);
            _dateTime = dateTime;
        }

        public static PersianDateTime Now { get { return FromGorgian(DateTime.Now); } }

        private static void ValidateDate(DateTime value)
        {
            if (value < Calendar.MinSupportedDateTime)
                throw new NotSupportedException("Date time coud not be bellow the minimum!");
            if (value > Calendar.MaxSupportedDateTime)
                throw new NotSupportedException("Date time coud not be above the maximum!");
        }

        private static void ValidatePersianDate(int year, int month, int day)
        {
            int maxAccceptedYear = PersianDateTime.Calendar.MaxSupportedDateTime.ToPersianDateTime().Year;
            if (year < 1 || year > maxAccceptedYear)
                throw new NotSupportedException(string.Format("Year value should be between 1 and {0}.", maxAccceptedYear));

            if (month < 1 || month > 12)
                throw new NotSupportedException("Month value should be between 1 and 12.");

            int maxDaysInMonth = PersianDateTime.Calendar.GetDaysInMonth(year, month);
            if (day < 1 || day > maxDaysInMonth)
                throw new NotSupportedException(string.Format("Day value for month {0} should be between 1 and {1}.", month, maxDaysInMonth));
        }


        public int Year
        {
            get { return Calendar.GetYear(DateTime); }
            set { this.SetParts(year: value); }
        }

        public int Month
        {
            get { return Calendar.GetMonth(DateTime); }
            set { this.SetParts(month: value); }
        }

        public int Day
        {
            get { return Calendar.GetDayOfMonth(DateTime); }
            set { this.SetParts(day: value); }
        }

        public int Hour
        {
            get { return Calendar.GetHour(DateTime); }
            set { this.SetParts(hour: value); }
        }

        public int Minute
        {
            get { return Calendar.GetMinute(DateTime); }
            set { this.SetParts(minute: value); }
        }

        public int Second
        {
            get { return Calendar.GetSecond(DateTime); }
            set { this.SetParts(seconds: value); }
        }

        public double Milliseconds { get { return Calendar.GetMilliseconds(DateTime); } }

        public DayOfWeek DayOfWeek { get { return Calendar.GetDayOfWeek(DateTime); } }


        public void SetParts(int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? seconds = null)
        {
            int y = year ?? this.Year;
            int m = month ?? this.Month;
            int d = day ?? this.Day;
            int h = hour ?? this.Hour;
            int m2 = minute ?? this.Minute;
            int s = seconds ?? this.Second;

            ValidatePersianDate(y, m, d);

            this.DateTime = Calendar.ToDateTime(y, m, d, h, m2, s, 0);
        }


        public PersianDateTime AddYears(int years) { return PersianDateTime.FromGorgian(Calendar.AddYears(DateTime, years)); }

        public PersianDateTime AddMonths(int monthes) { return PersianDateTime.FromGorgian(Calendar.AddMonths(DateTime, monthes)); }

        public PersianDateTime AddDays(int days) { return PersianDateTime.FromGorgian(Calendar.AddDays(DateTime, days)); }

        public PersianDateTime AddHours(int hours) { return PersianDateTime.FromGorgian(Calendar.AddHours(DateTime, hours)); }

        public PersianDateTime AddMinutes(int minutes) { return PersianDateTime.FromGorgian(Calendar.AddMinutes(DateTime, minutes)); }

        public PersianDateTime AddSeconds(int seconds) { return PersianDateTime.FromGorgian(Calendar.AddSeconds(DateTime, seconds)); }


        public static PersianDateTime Parse(string value)
        {
            PersianDateTime dt = new PersianDateTime();
            if (TryParse(value, out dt))
                return dt;
            return new PersianDateTime();
        }

        private static readonly Regex ParsingRegex = new Regex(@"(?<year>\d{2,4})/(?<month>\d{1,2})/(?<day>\d{1,2})", RegexOptions.Compiled);

        public static bool TryParse(string value, out PersianDateTime date)
        {
            date = new PersianDateTime();

            string datePart = value;
            string timePart = string.Empty;
            bool hasTimePart = value.Contains(',');
            if (hasTimePart)
            {
                var parts = value.Split(',');
                datePart = parts[0];
                timePart = parts[1];
            }

            //Match m = Regex.Match(datePart, @"(?<year>\d{2,4})/(?<month>\d{1,2})/(?<day>\d{1,2})");
            Match m = ParsingRegex.Match(datePart);
            if (!m.Success) return false;
            string yearText = m.Result("${year}");
            string monthText = m.Result("${month}");
            string dayText = m.Result("${day}");

            int year, month, day;
            if (!(int.TryParse(yearText, out year) && int.TryParse(monthText, out month) && int.TryParse(dayText, out day))) return false;
            if (year < 100) year += 1300;
            if (year < 1300 || year > 1400) return false;

            int hour = 0, minute = 0, second = 0;
            if (hasTimePart)
            {
                Match timeMatch = Regex.Match(timePart, @"(?<hour>\d{1,2}):(?<min>\d{1,2}):(?<sec>\d{1,2})");
                if (timeMatch.Success)
                {
                    string hourText = timeMatch.Result("${hour}");
                    string minText = timeMatch.Result("${min}");
                    string secText = timeMatch.Result("${sec}");
                    if (!int.TryParse(hourText, out hour) || !int.TryParse(minText, out minute) || !int.TryParse(secText, out second))
                        return false;

                    if (hour < 0 || hour > 24) return false;
                    if (minute < 0 || minute > 59) return false;
                    if (second < 0 || second > 59) return false;
                }
            }
            try
            {
                date = new PersianDateTime(year, month, day, hour, minute, second);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Return 16 (4 for year, 2 for month , 2 for day, 2 for hour , 2 for minute , 2 for second) digits number 
        /// </remarks>
        public long ToLong()
        {
            long val = (long)Year * 100 * 100 * 100 * 100 * 100
                + (long)Month * 100 * 100 * 100 * 100
                + (long)Day * 100 * 100 * 100
                + Hour * 100 * 100
                + Minute * 100
                + Second;
            return val;
        }

        public int ToDateInt() { return (int)(Date.ToLong() / One6Zero); }

        public PersianDateTime Date { get { return this.Add(-this.DateTime.TimeOfDay); } }

        [Obsolete("Use Date.ToLong() instead.")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long ToDateLong() { return Date.ToLong(); }

        public DateTime ToGorgian() { return DateTime; }

        public DateTime Gorgian { get { return DateTime; } }


        public static PersianDateTime FromLong(long? value)
        {
            bool isEmpty = value == null || value == 0;
            if (isEmpty)
                return new PersianDateTime();
            try
            {
                long v = value.Value;
                int year = (int)(v / (100L * 100 * 100 * 100 * 100));
                v -= (year * 100L * 100 * 100 * 100 * 100);
                int month = (int)(v / (100L * 100 * 100 * 100));
                v -= (month * 100L * 100 * 100 * 100);
                int day = (int)(v / (100L * 100 * 100));
                v -= (day * 100L * 100 * 100);
                int hour = (int)(v / (100L * 100));
                v -= (hour * 100L * 100);
                int minute = (int)(v / (100));
                v -= (minute * 100);
                int second = (int)v;
                return new PersianDateTime(year, month, day, hour, minute, second);

            }
            catch
            {
                return new PersianDateTime();
            }
        }

        public static PersianDateTime FromDateInt(int? value)
        {
            if (!value.HasValue)
                return new PersianDateTime();

            try
            {
                long v = value.Value;
                int year = (int)(v / (100L * 100));
                v -= (year * 100L * 100);
                int month = (int)(v / (100L));
                v -= (month * 100L);
                int day = (int)v;
                return new PersianDateTime(year, month, day, 0, 0, 0);
            }
            catch
            {
                return new PersianDateTime();
            }
        }

        public static bool IsValid(long value)
        {
            string stringValue = string.Format("{0:0000'/'00'/'00','00':'00':'00}", value);
            PersianDateTime persianDateTime;
            return PersianDateTime.TryParse(stringValue, out persianDateTime);
        }

        public static PersianDateTime FromGorgian(DateTime dateTime)
        {
            return new PersianDateTime(dateTime);
        }


        public string ToDateString() { return ToString("d"); }

        public string ToReverseDateString() { return ToString("w"); }

        public override string ToString() { return ToString("G"); }

        public string ToStringNonLocalized(string format, IFormatProvider formatProvider = null)
        {
            StringBuilder builder = ToStringBuilder(format);
            return builder.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            StringBuilder builder = ToStringBuilder(format);
            return builder.LocalizeNumbers().ToString();
        }

        private StringBuilder ToStringBuilder(string format)
        {
            if (string.IsNullOrWhiteSpace(format)) format = "G";

            if (format.Length == 1)
                format = GetFormat(format[0]);

            StringBuilder builder = new StringBuilder(50);

            string temp = string.Empty;
            for (int i = 0; i < format.Length; )
            {
                char c = format[i];
                i++;
                int count = 0;
                switch (c)
                {
                    case 'y':
                        count = GetRepeatCount(format, c, i) + 1;
                        if (count == 4)
                            builder.Append(Year.ToString("0000"));
                        else if (count == 2)
                            builder.Append((Year % 100).ToString("00"));
                        else
                            count = 0;
                        break;
                    case 'M':
                        count = GetRepeatCount(format, c, i) + 1;
                        if (count == 4)
                            builder.Append(MonthNames[this.Month - 1]);
                        else if (count == 2)
                            builder.Append(To2Digit(Month));
                        else if (count == 1)
                            builder.Append(this.Month.ToString());
                        else
                            count = 0;
                        break;
                    case 'd':
                        count = GetRepeatCount(format, c, i) + 1;
                        if (count == 4)
                            builder.Append(DayNames[(int)this.DayOfWeek]);
                        else if (count == 2)
                            builder.Append(To2Digit(Day));
                        else if (count == 1)
                            builder.Append(this.Day.ToString());
                        else
                            count = 0;
                        break;
                    case 'h':
                        count = GetRepeatCount(format, c, i) + 1;
                        if (count == 2)
                            builder.Append(To2Digit(Hour));
                        else
                            count = 0;
                        break;
                    case 'm':
                        count = GetRepeatCount(format, c, i) + 1;
                        if (count == 2)
                            builder.Append(To2Digit(Minute));
                        else
                            count = 0;
                        break;
                    case 's':
                        count = GetRepeatCount(format, c, i) + 1;
                        if (count == 2)
                            builder.Append(To2Digit(Second));
                        else
                            count = 0;
                        break;
                    default:
                        break;
                }
                if (count == 0)
                    builder.Append(c);
                else
                    i = i + count - 1;
            }
            return builder;
        }


        private static int GetRepeatCount(string value, char c, int pos)
        {
            int j = pos;
            while (j < value.Length && value[j] == c)
                j++;
            return j - pos;
        }

        private static string GetFormat(char c)
        {
            switch (c)
            {
                case 'G': return "yyyy/MM/dd,hh:mm:ss";
                case 'a': return "hh:mm yy/MM/dd";
                case 'l': return "d MMMM yyyy";
                case 'd': return "yyyy/MM/dd";
                case 'w': return "dd/MM/yyyy";
                case 's': return "dddd d MMMM yyyy";
                case 'k': return "dddd d MMMM yyyy , hh:mm:ss ";
                case 'c': return "yyyy MMMM d , hh:mm:ss";
                case 't': return "hh:mm:ss";
                default: return c.ToString();
            }
        }

        private static string To2Digit(int num2)
        {
            byte num = (byte)num2;
            char[] chars = new char[2];
            if (num < 10)
            {
                chars[0] = '0';
                chars[1] = (Char)(48 + num);
            }
            else
            {
                chars[0] = (Char)(48 + (num / 10));
                chars[1] = (Char)(48 + (num % 10));
            }
            return new string(chars);
        }


        public string ToRelativeString(PersianDateTime baseDate = null, string baseFormat = "s", TimeSpan? exclusiveRelativeTimeSpan = null)
        {
            PersianDateRange persianDateRange = new PersianDateRange(this, baseDate ?? PersianDateTime.Now);
            TimeSpan passedTimeSpan = persianDateRange.TimeSpan;

            if (exclusiveRelativeTimeSpan == null) exclusiveRelativeTimeSpan = new TimeSpan(7, 0, 0, 0);
            if (passedTimeSpan >= exclusiveRelativeTimeSpan)
                return this.ToString(baseFormat);


            if ((int)passedTimeSpan.TotalMinutes == 0)
                return "چند ثانیه قبل";
            else if (passedTimeSpan.TotalMinutes < 60)
                return string.Format("{0} دقیقه قبل", passedTimeSpan.Minutes.LocalizeNumbers());
            else if (passedTimeSpan.TotalHours < 24)
                return string.Format("{0} ساعت قبل", passedTimeSpan.Hours.LocalizeNumbers());
            else if (passedTimeSpan.TotalDays == 1)
                return string.Format("دیروز", passedTimeSpan.Days.LocalizeNumbers());
            else if (passedTimeSpan.TotalDays < 7)
                return string.Format("{0} روز قبل", passedTimeSpan.Days.LocalizeNumbers());
            return this.ToString(baseFormat);
        }

        public PersianDateTime Add(TimeSpan offsetValue)
        {
            return PersianDateTime.FromGorgian(DateTime.AddTicks(offsetValue.Ticks));
        }

        public object Clone()
        {
            return new PersianDateTime(this._dateTime);
        }

        public static bool operator <(PersianDateTime x, PersianDateTime y)
        {
            return x.DateTime < y.DateTime;
        }

        public static bool operator >(PersianDateTime x, PersianDateTime y)
        {
            return x.DateTime > y.DateTime;
        }
    }
}
