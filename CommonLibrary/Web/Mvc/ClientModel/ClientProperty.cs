using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    public class ClientProperty : IJsonSerializable
    {
        public ClientProperty(string name, object value)
        {
            Name = name;
            Value = value;
        }

        protected ClientProperty()
        {

        }

        public string Name { get; protected set; }

        public object Value { get; set; }

        public virtual string GetClientModelAsJson()
        {
            return Name + ":" + GetValueAsJson();
        }

        protected virtual string GetValueAsJson()
        {
            if (Value == null)
                return "''";

            if (Value is string)
                return "'" + Value.ToString() + "'";

            if (Value.GetType() == typeof(bool))
                return Value.ToString().ToLower();

            if (Value is IJsonSerializable)
                return (Value as IJsonSerializable).GetClientModelAsJson();

            return Value.ToString();
        }
    }

}
