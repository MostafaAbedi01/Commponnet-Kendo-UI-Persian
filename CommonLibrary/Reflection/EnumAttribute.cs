using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Reflection
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumAttribute  :Attribute
    {
        public string Caption { get; set; }

        public EnumAttribute() : this(null)
        {
        }

        public EnumAttribute(string caption)
        {
            this.Caption = caption;
        }
    }
}
