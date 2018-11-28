using System;
using System.Text;
using System.Web.Mvc;

namespace CommonLibrary
{
    public static class NumberFormatter
    {
        const long Miliard = 1000 * 1000 * 1000;
        const long Milion = 1000 * 1000;
        const long Hezar = 1000;

        public static string AlphabeticalNumericalToman(this int value, string postfix = null)
        { return AlphabeticalNumericalToman((long)value, postfix); }

        public static string AlphabeticalNumericalToman(this long value, string postfix = null)
        { return string.Format("{0}  معادل {1}", value.NumericalMoney(postfix), value.AlphabeticalToman(postfix)); }

        public static MvcHtmlString HtmlBriefToman(this int value, string postfix = null)
        { return HtmlBriefToman((long)value, postfix); }

        public static MvcHtmlString HtmlBriefToman(this long value, string postfix = null)
        {
            return MvcHtmlString.Create(
              string.Format(
              "<span title=\"معادل" + " {1}" + "\">{0}</span>",
              value.NumericalMoney(postfix),
              value.AlphabeticalToman(postfix)));
        }

        public static string AlphabeticalToman(this int value, string postfix = null)
        { return AlphabeticalToman((long)value, postfix); }

        public static string AlphabeticalToman(this long value, string postfix = null)
        { return AlphabeticalMoney(value / 10, postfix ?? "تومان"); }

        public static string NumericalToman(this int value, string postfix = null)
        { return NumericalToman((long)value, postfix); }

        public static string NumericalToman(this long value, string postfix = null)
        { return NumericalMoney(value / 10, postfix ?? "تومان"); }

        public static string PositivePartNumericalMoney(this long value, string postfix = null)
        {
            if (value > 0)
                return string.Format("{0:N0} {1}", value, postfix ?? "ریال");
            return "0 " + postfix ?? "ریال";
        }

        public static string AlphabeticalNumericalMoney(this long value, string postfix = null)
        { return string.Format("{0}  معادل {1}", value.NumericalMoney(postfix), value.AlphabeticalMoney(postfix)); }

        public static string AlphabeticalNumericalMoney(this int value, string postfix = null)
        { return string.Format("{0}  معادل {1}", value.NumericalMoney(postfix), value.AlphabeticalMoney(postfix)); }

        public static string NumericalMoney(this string value, string postfix = null)
        { return NumericalMoney(Convert.ToInt64(value), postfix); }

        public static string NumericalMoney(this int value, string postfix = null)
        { return NumericalMoney((long)value, postfix); }

        public static string NumericalMoney(this long value, string postfix = null)
        { return string.Format("{0:N0} {1}", value, postfix ?? "ریال").LocalizeNumbers(); }

        public static string AlphabeticalMoney(this int value, string postfix = null)
        { return AlphabeticalMoney((long)value, postfix); }

        public static string AlphabeticalMoney(this string value, string postfix = null)
        { return AlphabeticalMoney(Convert.ToInt64(value), postfix); }

        public static string AlphabeticalMoney(this long value, string postfix = null)
        {
            StringBuilder resultBuilder = new StringBuilder();
            bool isNegetive = value < 0;
            if (isNegetive)
                value = Math.Abs(value);

            value = value.AppendAsTextByCheck(resultBuilder, "{0} میلیارد", Miliard);
            value = value.AppendAsTextByCheck(resultBuilder, "{0} میلیون", Milion);
            value = value.AppendAsTextByCheck(resultBuilder, "{0} هزار", Hezar);
            value = value.AppendAsTextByCheck(resultBuilder, "{0}", 1);

            if (isNegetive)
                resultBuilder.Insert(0, "منفی ");

            if (resultBuilder.Length == 0)
                resultBuilder.Append("صفر");
            resultBuilder.Append(" ");
            resultBuilder.Append(postfix ?? "ریال");

            return resultBuilder.ToString();
        }

        private static long AppendAsTextByCheck(this long value, StringBuilder buffer, string format, long baseNum)
        {
            int num = (int)(value / baseNum);
            num.AppendAsTextByCheck(buffer, format);
            return value % baseNum;
        }

        private static void AppendAsTextByCheck(this int value, StringBuilder buffer, string format)
        {
            if (value > 0)
            {
                if (buffer.Length > 0)
                    buffer.Append(" و ");
                buffer.AppendFormat(format, value.AsText());
            }
        }

        private static string AsText(this int value)
        {
            StringBuilder inetrnalBbuffer = new StringBuilder();

            if (value > 1000)
                value = value % 1000;
            if (value >= 100)
            {
                int num = value / 100;
                inetrnalBbuffer.Append(num.As100Text());
            }
            value = value % 100;

            if (value >= 20)
            {
                int num = value / 10;
                if (inetrnalBbuffer.Length > 0)
                    inetrnalBbuffer.Append(" و ");
                inetrnalBbuffer.Append(num.As10Text());
            }
            if (value >= 20)
                value = value % 10;

            if (value > 0)
            {
                if (inetrnalBbuffer.Length > 0)
                    inetrnalBbuffer.Append(" و ");
                inetrnalBbuffer.Append(value.As1Text());
            }

            return inetrnalBbuffer.ToString();
        }

        private static string As100Text(this int num)
        {
            string text;
            switch (num)
            {
                case 1: text = "صد"; break;
                case 2: text = "دویست"; break;
                case 3: text = "سی صد"; break;
                case 4: text = "چهار صد"; break;
                case 5: text = "پانصد"; break;
                default: text = As1Text(num) + "صد"; break;
            }
            return text;
        }

        private static string As10Text(this int num)
        {
            string text = "";
            switch (num)
            {
                case 2: text = "بیست"; break;
                case 3: text = "سی"; break;
                case 4: text = "چهل"; break;
                case 5: text = "پنجاه"; break;
                case 6: text = "شصت"; break;
                case 7: text = "هفتاد"; break;
                case 8: text = "هشتاد"; break;
                case 9: text = "نود"; break;
            }
            return text;
        }


        private static string As1Text(this  int num)
        {
            string text = "";
            switch (num)
            {
                case 1: text = "یک"; break;
                case 2: text = "دو"; break;
                case 3: text = "سه"; break;
                case 4: text = "چهار"; break;
                case 5: text = "پنج"; break;
                case 6: text = "شش"; break;
                case 7: text = "هفت"; break;
                case 8: text = "هشت"; break;
                case 9: text = "نه"; break;
                case 10: text = "ده"; break;
                case 11: text = "یازده"; break;
                case 12: text = "دوازده"; break;
                case 13: text = "سیزده"; break;
                case 14: text = "چهارده"; break;
                case 15: text = "پانزده"; break;
                case 16: text = "شانزده"; break;
                case 17: text = "هفده"; break;
                case 18: text = "هجده"; break;
                case 19: text = "نوزده"; break;
            }
            return text;
        }
    }
}
