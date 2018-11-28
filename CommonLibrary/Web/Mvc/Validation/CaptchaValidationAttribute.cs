/** 
 * Copyright (C) 2007-2008 Nicholas Berardi, Managed Fusion, LLC (nick@managedfusion.com)
 * 
 * <author>Nicholas Berardi</author>
 * <author_email>nick@managedfusion.com</author_email>
 * <company>Managed Fusion, LLC</company>
 * <product>Url Rewriter and Reverse Proxy</product>
 * <license>Microsoft Public License (Ms-PL)</license>
 * <agreement>
 * This software, as defined above in <product />, is copyrighted by the <author /> and the <company /> 
 * and is licensed for use under <license />, all defined above.
 * 
 * This copyright notice may not be removed and if this <product /> or any parts of it are used any other
 * packaged software, attribution needs to be given to the author, <author />.  This can be in the form of a textual
 * message at program startup or in documentation (online or textual) provided with the packaged software.
 * </agreement>
 * <product_url>http://www.managedfusion.com/products/url-rewriter/</product_url>
 * <license_url>http://www.managedfusion.com/products/url-rewriter/license.aspx</license_url>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using CommonLibrary.Web.Mvc;
using TryInfo = System.Tuple<int, System.DateTime>;
using Mehr.Setting;
namespace CommonLibrary.Web.Mvc.Validation
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class CaptchaValidationAttribute : ActionFilterAttribute
    {
        string defaultValidationMessage = "مقدار وارد شده صحیح نیست!";
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaCheckAttribute"/> class.
        /// </summary>
        public CaptchaValidationAttribute()
            : this("captcha") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaCheckAttribute"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        public CaptchaValidationAttribute(string field)
        {
            Field = field;
            ValidationMessage = defaultValidationMessage;
        }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public string Field { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Try())
                return;

            var modelState = filterContext.Controller.ViewData.ModelState;

            // make sure the captcha valid key is not contained in the route data   
            //if (filterContext.RouteData.Values.ContainsKey("captchaValid"))
            //    filterContext.RouteData.Values.Remove("captchaValid");
            string guid = filterContext.RequestContext.HttpContext.Request.Form["captcha-guid"];
            if (String.IsNullOrEmpty(guid))
            {
                modelState.AddModelError(Field, ValidationMessage);
                //filterContext.RouteData.Values.Add("captchaValid", false);
                return;
            }

            CaptchaImage image = CaptchaImage.GetCachedCaptcha(guid);
            string actualValue = filterContext.RequestContext.HttpContext.Request.Form[Field];
            string expectedValue = image == null ? String.Empty : image.Text;
            filterContext.RequestContext.HttpContext.Cache.Remove(guid);

            if (String.IsNullOrEmpty(actualValue) || String.IsNullOrEmpty(expectedValue) ||
                !String.Equals(actualValue, expectedValue, StringComparison.OrdinalIgnoreCase))
                modelState.AddModelError(Field, ValidationMessage);
            else
                modelState.Remove(Field);
        }

        static readonly SortedDictionary<string, TryInfo> captchaTry = new SortedDictionary<string, TryInfo>();

        public static int ReachCount
        {
            get { return SettingReader.Get(SettingsKey.ReachCount, 4); }
        }

        public static bool DisableReachCountControl
        {
            get { return SettingReader.Get(SettingsKey.DisableReachCountControl, false); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Reached or not.</returns>
        /// <remarks>If return true captha should be displayed on response and wil be controlled in next request.</remarks>
        public static bool IsTryCountReached()
        {
            if (DisableReachCountControl)
                return true;
            string ip = HttpContext.Current.Request.UserHostAddress;
            TryInfo lastTry;
            return captchaTry.TryGetValue(ip, out lastTry) && lastTry.Item1 >= ReachCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Whether succeed or not.</returns>
        /// <remarks>If return false captha shoul be provided in request params.</remarks>
        static bool Try()
        {
            if (DisableReachCountControl)
                return false;

            string ip = HttpContext.Current.Request.UserHostAddress;
            TryInfo lastTry;
            if (!captchaTry.TryGetValue(ip, out lastTry))
            {
                if (captchaTry.Count > 1000)
                    captchaTry.Clear();
                captchaTry[ip] = new TryInfo(1, DateTime.Now);
                return true;
            }

            if (DateTime.Now.Subtract(lastTry.Item2).TotalMinutes < 1)
            {
                bool notReached = lastTry.Item1 < ReachCount + 1;
                captchaTry[ip] = new TryInfo(notReached ? lastTry.Item1 + 1 : ReachCount + 1, DateTime.Now);
                return notReached;
            }
            else
            {
                captchaTry[ip] = new TryInfo(1, DateTime.Now);
                return true;
            }

        }

        public static class SettingsKey
        {
            public const string Prefix = "CaptchaValidation";
            public const string ReachCount = Prefix + ".ReachCount";
            public const string DisableReachCountControl = Prefix + ".DisableReachCountControl";

        }
    }
}
