using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Reflection
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumItemMappingAttribute : Attribute
    {
        public EnumItemMappingAttribute(params Enum[] mappedValues)
        {
            this.MappedValues = mappedValues;
        }

        public virtual Enum[] MappedValues { get;protected set; }
    }
}
