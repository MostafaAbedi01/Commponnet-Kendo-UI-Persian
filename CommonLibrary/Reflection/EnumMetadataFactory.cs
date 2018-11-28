using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Mehr.Reflection;
using Mehr.Web;
using System.Web.Mvc;

namespace Mehr.Reflection
{
    public class EnumMetadataFactory : IEnumMetadataFactory
    {
        public ICacheProvider CacheProvider { get; private set; }
        public EnumMetadataFactory(ICacheProvider cache) { this.CacheProvider = cache; }

        public EnumMetadata<T> Get<T>()
        {
            return CacheProvider.Get(typeof(T).ToString(), (k) => new EnumMetadata<T>());
        }

        public string GetCaption<T>(T item)
        {
            var enumMetadata = Get<T>();

            try
            { return enumMetadata.Items[item]; }
            catch (Exception ex)
            { throw new NotSupportedException("Value = " + item, ex); }
        }

        public string GetCombinedCaption<T>(T item, string seperator = ",")
        {
            var enumMetadata = Get<T>();
            List<string> items = new List<string>();
            Type enumType = typeof(T);
            foreach (var val in enumMetadata.EnumValues)
            {
                long numVal = Convert.ToInt64(val);
                if (numVal != 0)
                {
                    if ((numVal & Convert.ToInt64(item)) == numVal)
                    {
                        var tVal = (T)Enum.ToObject(enumType, numVal);
                        items.Add(enumMetadata.Items[tVal]);
                    }
                }
            }
            return string.Join(seperator, items);
        }
    }
}