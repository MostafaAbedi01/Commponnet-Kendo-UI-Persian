using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;

namespace Mehr.Web
{
    public static class HttpSessionStateExtensions
    {
        public static T Get<T>(this HttpSessionState sessionState, string key, Func<T> builder, bool forceReload = false)
        {
            object value = forceReload ? null : sessionState[key];
            if (value == null)
                sessionState[key] = value = builder();
            return (T)(value ?? default(T));
        }
    }
}
