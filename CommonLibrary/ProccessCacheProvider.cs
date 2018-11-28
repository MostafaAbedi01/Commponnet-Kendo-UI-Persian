using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public class ProccessCacheProvider : ICacheProvider
    {
        public static readonly DateTime NoAbsoluteExpiration;

        static ProccessCacheProvider()
        {
            NoAbsoluteExpiration = System.Web.Caching.Cache.NoAbsoluteExpiration;
        }

        public static SortedDictionary<string, Tuple<object, DateTime>> cache = new SortedDictionary<string, Tuple<object, DateTime>>();

        public T Get<T>(string key, Func<string, T> builder)
        {
            return Get(key, builder, NoAbsoluteExpiration);
        }

        public bool Remove(string key)
        {
            return cache.Remove(key);
        }

        public T Get<T>(string key, Func<string, T> builder, DateTime absoluteExpiration, TimeSpan? slidingExpiration = null)
        {
            var tuple = cache.Get<string, Tuple<object, DateTime>>(key, k => new Tuple<object, DateTime>(builder(k), absoluteExpiration));
            if (tuple == null || 
                tuple.Item1 == null || 
                tuple.Item1.GetType() != typeof(T))
                return default(T);
            if (tuple.Item2 != NoAbsoluteExpiration &&
                tuple.Item2 < DateTime.Now)
            {
                cache.Remove(key);
                return default(T);
            }
            return (T)tuple.Item1;
        }
    }
}
