using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace Mehr
{
    public static class DictionaryExtension
    {
        public static ValueT Get<KeyT, ValueT>(this IDictionary<KeyT, ValueT> cache, KeyT key, Func<KeyT, ValueT> builder)
        {
            ValueT value = default(ValueT);
            if (!cache.TryGetValue(key, out value))
                cache[key] = value = builder(key);
            return value;
        }
    }
}
