using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mehr.Web
{
    public class HttpContextCacheProvider : ICacheProvider
    {
        public T Get<T>(string key, Func<string, T> builder)
        {
            return HttpRuntime.Cache.Get(key, () => builder(key));
        }

        public bool Remove(string key)
        {
            return HttpRuntime.Cache.Remove(key) != null;
        }

        public T Get<T>(string key, Func<string, T> builder, DateTime absoluteExpiration, TimeSpan? slidingExpiration = null)
        {
            return HttpRuntime.Cache.Get(key, () => builder(key), absoluteExpiration, slidingExpiration);
        }
    }
}
