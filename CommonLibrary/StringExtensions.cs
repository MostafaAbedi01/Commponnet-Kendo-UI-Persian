using System;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int len, string appendValue = null)
        {
            appendValue = appendValue ?? string.Empty;
            if (value != null && value.Length > len)
                return value.Substring(0, len - appendValue.Length) + appendValue;
            return value;
        }

        public static string ReplaceToPersian(this string value)
        {
            value = value.Replace('ي', 'ی');
            value = value.Replace('ك', 'ک');
            return value;
        }

        public static string TrimSignleWord(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return string.Join("", value.Split(' ').Select(v => v.Trim()));
        }

        public static string TrimMultiWord(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return string.Join(" ", value.Split(' ').Select(v => v.Trim()));
        }

        const int zeroCharCode = (int)'0';
        const int localizedZeroCharCode = (int)'۰';

        public static string LocalizeNumbers<T>(this T value) { return LocalizeNumbers(value, string.Empty); }

        public static string LocalizeNumbers<T>(this T value, string nullString = "")
        {
            if (value == null) return nullString;
            return value.ToString().LocalizeNumbers();
        }

        public static string FormatLocalizeNumbers<T>(this T value, string formatstring, string nullString = "")
            where T : IFormattable
        {
            if (value == null) return nullString;
            return value.ToString(formatstring, null).LocalizeNumbers();
        }

        public static string FormatLocalizeNumbers<T>(this Nullable<T> value, string formatstring, string nullString = "")
           where T : struct, IFormattable
        {
            if (value == null) return nullString;
            return value.Value.ToString(formatstring, null).LocalizeNumbers();
        }


        public static string LocalizeNumbers(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int num = ((int)chars[i]) - zeroCharCode;
                if (0 <= num && num < 10)
                    chars[i] = (char)(localizedZeroCharCode + num);
            }
            return new string(chars);
        }

        public static string UnLocalizeNumbers(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int num = ((int)chars[i]) - localizedZeroCharCode;
                if (0 <= num && num < 10)
                    chars[i] = (char)(zeroCharCode + num);
            }
            return new string(chars);
        }

        public static StringBuilder LocalizeNumbers(this StringBuilder value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                int num = ((int)value[i]) - zeroCharCode;
                if (0 <= num && num < 10)
                    value[i] = (char)(localizedZeroCharCode + num);
            }
            return value;
        }

        public static string TrimStart(this string value, string trimingValue)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (string.IsNullOrEmpty(trimingValue)) return value;

            if (value.StartsWith(trimingValue))
                return value.Substring(trimingValue.Length);

            return value;
        }
    }
}
