using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Reflection;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    public class EnumValue<T> : IJsonSerializable
        where T : struct
    {
        public T Value { get; set; }

        public EnumValue(T value)
        {
            this.Value = value;
        }

        //public static EnumValue<T> Create<T>(T value) where T : struct
        //{ return new EnumValue<T>(value); }

        public string GetClientModelAsJson()
        {
            var enumType = typeof(T);
            var jsonEnumValueAttribute = enumType.GetField(Value.ToString()).GetAttribute<JsonEnumValueAttribute>();
            if (jsonEnumValueAttribute != null)
                return "'" + jsonEnumValueAttribute.JsonValue + "'";

            var jsonEnumAttribute = typeof(T).GetAttribute<JsonEnumAttribute>();
            if (jsonEnumAttribute != null)
                if (jsonEnumAttribute.ToLower)
                    return "'" + Value.ToString().ToLower() + "'";

            return "'" + Value.ToString() + "'";
        }
    }
}
