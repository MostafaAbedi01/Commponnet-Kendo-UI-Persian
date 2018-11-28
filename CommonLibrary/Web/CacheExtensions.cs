using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace Mehr.Web
{
    public static class CacheExtensions
    {
        public static T Get<T>(this Cache cache, string key, Func<T> builder, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            object value = cache[key];
            if (value == null)
            {
                value = builder();
                cache.Remove(key);
                cache.Add(
                    key, 
                    value, 
                    null, 
                    absoluteExpiration ?? Cache.NoAbsoluteExpiration, 
                    slidingExpiration?? Cache.NoSlidingExpiration, 
                    CacheItemPriority.Normal, 
                    null);
            }
            return (T)(value ?? default(T));
        }
    }
}
