using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EnumFilterAttribute : Attribute
    {
        public bool IsFiltered<T>(T value)
        {
            return IsNotFilteredList ^ filterValues.Contains(Convert.ToInt64(value));
        }

        public bool IsNotFilteredList { get; set; }

        long[] filterValues;
        public EnumFilterAttribute(params long[] filterValues)
        {
            this.filterValues = filterValues;
        }
    }
}
