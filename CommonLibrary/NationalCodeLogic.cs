using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public static class NationalCodeLogic
    {
        const long fix10One = 1111111111;
        static long[] SameNums = new long[]{
            fix10One * 0,
            fix10One * 1,
            fix10One * 2,
            fix10One * 3,
            fix10One * 4,
            fix10One * 5,
            fix10One * 6,
            fix10One * 7,
            fix10One * 8,
            fix10One * 9,
        };

        const char ZeroChar = '0';

        public static bool IsValid(string nationalCode)
        {
            if (string.IsNullOrEmpty(nationalCode))
                return false;

            long longValue;
            if (nationalCode.Length != 10 ||
                !long.TryParse(nationalCode, out longValue) ||
                SameNums.Contains(longValue))
                return false;

            long sum = (int)(nationalCode[0] - ZeroChar) * 10 +
                (int)(nationalCode[1] - ZeroChar) * 9 +
                (int)(nationalCode[2] - ZeroChar) * 8 +
                (int)(nationalCode[3] - ZeroChar) * 7 +
                (int)(nationalCode[4] - ZeroChar) * 6 +
                (int)(nationalCode[5] - ZeroChar) * 5 +
                (int)(nationalCode[6] - ZeroChar) * 4 +
                (int)(nationalCode[7] - ZeroChar) * 3 +
                (int)(nationalCode[8] - ZeroChar) * 2;

            long c = (int)(nationalCode[9] - ZeroChar);
            long r = sum % 11;

            if (r == 0 || r == 1)
                return c == r;
            else
                return c + r == 11;
        }
    }
}
