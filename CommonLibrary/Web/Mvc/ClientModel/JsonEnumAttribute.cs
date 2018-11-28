using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class JsonEnumAttribute : Attribute
    {
        public bool ToLower { get; set; }

        public JsonEnumAttribute()
        {
        }
    }
}
