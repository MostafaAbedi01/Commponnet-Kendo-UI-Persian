using System;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class PersianDateAttribute : ValidationAttribute
    {
        public string MaxOffsetToNowString { set { MaxOffsetToNow = TimeSpan.Parse(value); } get { return MaxOffsetToNow.Value.ToString(); } }
        public string MinOffsetToNowString { set { MinOffsetToNow = TimeSpan.Parse(value); } get { return MinOffsetToNow.Value.ToString(); } }

        public TimeSpan? MaxOffsetToNow { get; set; }
        public TimeSpan? MinOffsetToNow { get; set; }

        public PersianDateTime MaxDateTime
        { get { return MaxOffsetToNow.HasValue ? PersianDateTime.Now.Add(MaxOffsetToNow.Value) : null; } }

        public PersianDateTime MinDateTime
        { get { return MinOffsetToNow.HasValue ? PersianDateTime.Now.Add(MinOffsetToNow.Value) : null; } }

        public PersianDateAttribute() : base("تاریخ وارد شده معتبر نیست.") { }

        protected bool PropertyValid(object value)
        {
            try
            {
                //bypass required checking.
                if (value == null ||
                    (value is string && string.IsNullOrEmpty(value as string))) return true;

                PersianDateTime dt = Parse(value);
                return IsDateValid(dt);
            }
            catch { return false; }
        }

        public PersianDateTime Parse(object value)
        {
            if (value is string)
            {
                string stringValue = (string)value;
                PersianDateTime dt;
                if (!PersianDateTime.TryParse(stringValue, out dt)) return null;
                return dt;
            }
            else if (value is long)
            {
                long longValue = (long)value;
                if (longValue == 0) return null;
                return PersianDateTime.FromLong(longValue);
            }
            return null;
        }



        protected virtual bool IsDateValid(PersianDateTime dt)
        {
            if (dt == null) return false;
            if (MaxDateTime != null && dt.ToGorgian() > MaxDateTime.ToGorgian().Date) return false;
            if (MinDateTime != null && dt.ToGorgian() < MinDateTime.ToGorgian().Date) return false;
            return true;
        }

        public override bool IsValid(object value)
        {
            return PropertyValid(value);
        }
    }

}
