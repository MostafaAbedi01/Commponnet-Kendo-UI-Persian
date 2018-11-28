using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Reflection;
using System.CodeDom.Compiler;

namespace CommonLibrary.Web.Mvc.Security
{
    public static class T4MVCActionResultExtensions
    {
        public static readonly string TokenPassword = "@#s%e#p";
        public static readonly SortedDictionary<string, ProtectedBySecurityTokenAttribute> SecurityTokenEnabledActionFullNames =
            new SortedDictionary<string, ProtectedBySecurityTokenAttribute>();

        public static void Initial()
        {
            var controllerClasses = from t in Assembly.GetCallingAssembly().GetTypes()
                                    where t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(System.Web.Mvc.Controller)) 
                                            && t.GetCustomAttributes(typeof(GeneratedCodeAttribute),true).Length == 0
                                    select t;

            foreach (Type controllerClass in controllerClasses)
                foreach (MethodInfo methodInfo in controllerClass.GetMethods())
                {
                    var protectedBySecurityTokenAttribute = Attribute.GetCustomAttribute(methodInfo, typeof(ProtectedBySecurityTokenAttribute), true)
                        as ProtectedBySecurityTokenAttribute;
                    if (protectedBySecurityTokenAttribute != null)
                    {
                        StringBuilder uniqueName = new StringBuilder(100);
                        uniqueName.Append(controllerClass.Name.Substring(0, controllerClass.Name.Length - "Controller".Length));
                        uniqueName.Append('.');
                        uniqueName.Append(methodInfo.Name);
                        uniqueName.Append('.');
                        foreach (var param in methodInfo.GetParameters())
                        {
                            uniqueName.Append(param.Name);
                            uniqueName.Append('.');
                        }
                        SecurityTokenEnabledActionFullNames.Add(uniqueName.ToString(), protectedBySecurityTokenAttribute);
                    }
                }

        }

        public static void AddSecurityTokenToRouteValue(this RouteValueDictionary routeValueDictionary)
        {

            StringBuilder uniqueName = new StringBuilder(100);
            string controller = routeValueDictionary["controller"] as string;
            uniqueName.Append(controller);
            uniqueName.Append(".");
            string action = routeValueDictionary["action"] as string;
            uniqueName.Append(action);
            uniqueName.Append(".");
            foreach (var item in routeValueDictionary)
            {
                if (!SecurityTokenManager.KnownRouteNames.Contains(item.Key.ToLower()))
                {
                    uniqueName.Append(item.Key);
                    uniqueName.Append(".");
                }
            }

            ProtectedBySecurityTokenAttribute protectedBySecurityTokenAttribute;
            if (SecurityTokenEnabledActionFullNames.TryGetValue(uniqueName.ToString(), out protectedBySecurityTokenAttribute))
            {
                var token = SecurityTokenManager.GenerateToken(controller, action, routeValueDictionary, protectedBySecurityTokenAttribute, TokenPassword);
                if (!string.IsNullOrEmpty(token))
                    routeValueDictionary.Add(SecurityTokenManager.UrlTokenRoutKey, token);
            }
        }
    }
}
