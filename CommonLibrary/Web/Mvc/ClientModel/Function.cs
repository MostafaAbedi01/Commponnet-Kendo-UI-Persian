using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    public class Function : IJsonSerializable
    {
        public string Name { get; set; }

        public Function(string name)
        {
            this.Name = name;
        }

        public string GetClientModelAsJson()
        {
            return Name;
        }
    }
}
