using System;
using System.Collections.Generic;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class NonZeroRequiredAttribute : RequiredAttribute
    {
        public NonZeroRequiredAttribute() : base()
        {
            ErrorMessage = "مورد نیاز است.";
        }

        private static readonly HashSet<Type> _numericTypes = new HashSet<Type>(new Type[] {
            typeof(byte), typeof(sbyte),
            typeof(short), typeof(ushort),
            typeof(int), typeof(uint),
            typeof(long), typeof(ulong),
            typeof(float), typeof(double), typeof(decimal)
        });

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                Type type = value.GetType();
                type = Nullable.GetUnderlyingType(type) ?? type; // strip off the Nullable<>
                if (_numericTypes.Contains(type))
                    if (value.ToString()[0] == '0')
                        return false;
            }
            return base.IsValid(value);
        }
    }
}
