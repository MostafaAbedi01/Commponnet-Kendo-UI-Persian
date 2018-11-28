using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Reflection
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumItemAttribute  :Attribute
    {
        public string Caption { get; set; }

        public byte Order { get; set; }

        public EnumItemAttribute() : this(null)
        {
        }

        public EnumItemAttribute(string caption)
        {
            this.Caption = caption;
        }
    }
}
