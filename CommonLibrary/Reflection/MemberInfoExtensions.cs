using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Mehr.Reflection
{
    public static class MemberInfoExtensions
    {
        public static T GetAttribute<T>(this MemberInfo member)
            where T : Attribute
        {
            var attrs = member.GetCustomAttributes(typeof(T), true);
            if (attrs != null && attrs.Length > 0)
                return attrs[0] as T;
            return null;
        }
    }
}
