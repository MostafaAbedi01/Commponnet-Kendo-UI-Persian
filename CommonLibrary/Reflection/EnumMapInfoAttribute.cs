using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Reflection
{
    [AttributeUsage(AttributeTargets.Enum,AllowMultiple=true)]
    public class AttributeEnumMapAttribute : EnumMapAttribute
    {
        public Type MapperAttrbuiteType { get; set; }
    }
}
