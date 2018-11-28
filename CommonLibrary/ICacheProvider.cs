using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public interface ICacheProvider
    {
        T Get<T>(string key, Func<string,T> builder);

        T Get<T>(string key, Func<string, T> builder, DateTime absoluteExpiration,TimeSpan? slidingExpiration=null);
        
        bool Remove(string key);
    }
}
