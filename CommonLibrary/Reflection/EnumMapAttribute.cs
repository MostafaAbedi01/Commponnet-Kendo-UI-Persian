using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Reflection
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = true)]
    public abstract class EnumMapAttribute : Attribute
    {
        public Type MapEnumType { get; set; }

        public string CorrelationName { get; set; }
    }
}
