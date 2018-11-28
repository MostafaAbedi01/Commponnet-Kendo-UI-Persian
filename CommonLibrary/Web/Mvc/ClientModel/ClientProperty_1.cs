using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    public class ClientProperty<ValueT> : ClientProperty
    {
        public ClientProperty(string name, ValueT value)
            : base(name, value) { }

        protected ClientProperty() { }

        new public ValueT Value
        {
            get { return (ValueT)base.Value; }
            set { base.Value = value; }
        }
    }

}
