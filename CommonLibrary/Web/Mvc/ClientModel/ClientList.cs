using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    public class ClientList<T> : List<T>, IJsonSerializable
    {
        public ClientList() : base() { }

        public ClientList(IEnumerable<T> collection) : base(collection) { }

        public virtual string GetClientModelAsJson()
        {
            return '[' + string.Join(",", this.Select(SerializeItem)) + ']';
        }

        protected virtual string SerializeItem(T item)
        {
            return (item is IJsonSerializable) ?
                        (item as IJsonSerializable).GetClientModelAsJson() :
                        item.ToString();
        }
    }
}
