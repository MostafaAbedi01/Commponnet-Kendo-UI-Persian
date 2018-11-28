using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;

namespace CommonLibrary.Web.Mvc.Security
{
    /// <summary>
    /// Search google: ExtendedHtmlActionLinkHelper
    /// </summary>
    public static class SecurityTokenManager
    {
        public const string UrlTokenRoutKey = "urltoken";
        public static string[] KnownRouteNames = new[] { "area", "controller", "action", UrlTokenRoutKey };
        public static string GenerateToken(string controllerName, string actionName, RouteValueDictionary argumentParams, ISecurityTokenSetting securityTokenSetting, string password)
        {
            if (!securityTokenSetting.IsEnabled) return "";
            ////The URL hash is dynamic by assign a dynamic key in each session. So
            ////eventhough your URL is stolen, it will not work in other session
            //if (HttpContext.Current.Session["url_dynamickey"] == null)
            //    HttpContext.Current.Session["url_dynamickey"] = RandomString();

            string token = "";
            //The salt include the dynamic session key and valid for an hour.
            // string salt = HttpContext.Current.Session["url_dynamickey"].ToString() + DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour; ;
            string salt = securityTokenSetting.ActionUniqueIdentifier;
            //generating the partial url
            StringBuilder stringToTokenBuilde = new StringBuilder(30);
            stringToTokenBuilde.Append(controllerName);
            stringToTokenBuilde.Append('/');
            stringToTokenBuilde.Append(actionName);
            foreach (KeyValuePair<string, object> item in argumentParams)
                if (!KnownRouteNames.Contains(item.Key.ToLower()))
                    if (item.Value != null)
                        if (securityTokenSetting.ExcludeNames == null || !securityTokenSetting.ExcludeNames.Contains(item.Key.ToLower()))
                        {
                            stringToTokenBuilde.Append('/');
                            stringToTokenBuilde.Append(item.Value);
                        }
            var stringToToken = stringToTokenBuilde.ToString().ToLower();

            //Converting the salt in to a byte array
            byte[] saltValueBytes = System.Text.Encoding.ASCII.GetBytes(salt);
            //Encrypt the salt bytes with the password
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, saltValueBytes);
            //get the key bytes from the above process
            byte[] secretKey = key.GetBytes(16);
            //generate the hash
            HMACMD5 tokenHash = new HMACMD5(secretKey);
            var BytesToToken = System.Text.Encoding.ASCII.GetBytes(stringToToken);
            tokenHash.ComputeHash(BytesToToken);
            //convert the hash to a base64string
            //token = Convert.ToBase64String(tokenHash.Hash).Replace("/", "_");

            token = HttpServerUtility.UrlTokenEncode(tokenHash.Hash.Skip(5).Take(8).ToArray());
            return token;
        }
        //This validates the token
        public static bool ValidateToken(string token, string controllerName, string actionName, RouteValueDictionary argumentParams, ISecurityTokenSetting securityTokenSetting, string password)
        {
            //compute the token for the currrent parameter
            string computedToken = GenerateToken(controllerName, actionName, argumentParams, securityTokenSetting, password);
            return computedToken == token;
        }

        //It validates the token, where all the parameters passed as a RouteValueDictionary
        public static bool ValidateToken(RouteValueDictionary requestUrlParts, ISecurityTokenSetting securityTokenSetting, string password)
        {
            //get the parameters
            string controllerName;
            try
            {
                controllerName = Convert.ToString(requestUrlParts["controller"]);
            }
            catch { controllerName = ""; }
            string actionName = Convert.ToString(requestUrlParts["action"]);
            string token = Convert.ToString(requestUrlParts[UrlTokenRoutKey]);
            return ValidateToken(token, controllerName, actionName, requestUrlParts, securityTokenSetting, password);
        }

        ////This method create the random dynamic key for the session
        //private static string RandomString()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    Random random = new Random();
        //    char ch;
        //    for (int i = 0; i < 8; i++)
        //    {
        //        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        //        builder.Append(ch);
        //    }
        //    return builder.ToString();
        //}

    }
}
