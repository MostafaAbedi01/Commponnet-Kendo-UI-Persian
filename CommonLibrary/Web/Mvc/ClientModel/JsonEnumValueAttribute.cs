using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    [AttributeUsage(AttributeTargets.Field)]
    public class JsonEnumValueAttribute : Attribute
    {
        public string JsonValue { get; set; }

        public JsonEnumValueAttribute(string jsonValue)
        {
            JsonValue = jsonValue;
        }
    }
}
