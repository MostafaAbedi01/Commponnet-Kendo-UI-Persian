using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CommonLibrary.Web.Mvc.Security
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class ProtectedBySecurityTokenAttribute : AuthorizeAttribute, ISecurityTokenSetting
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // basic athorization check 
            base.OnAuthorization(filterContext);

            if (!IsEnabled) return;

            //Commented cause: Enetring a url in address bar cause to url refferrer beign null.
            //if (filterContext.HttpContext != null)
            //    if (filterContext.HttpContext.Request.UrlReferrer == null)
            //        filterContext.Result = new HttpUnauthorizedResult();

            /*Add code here to check the domain name the request come from*/

            // The call for validation of URL hash and do the redirection if error occurs
            //It uses a controller named ErrorViewController and action named DisplayURLError
            //These controller and action need to be present in your project in the project name space
            if (!SecurityTokenManager.ValidateToken(filterContext.RequestContext.RouteData.Values, this, T4MVCActionResultExtensions.TokenPassword))
                filterContext.Result = new HttpUnauthorizedResult();
        }

        public abstract string ActionUniqueIdentifier { get; }
        public abstract bool IsEnabled { get; }

        public int OverloadDitinct { get; set; }

        public string[] ExcludeNames { get; set; }
    }

    public interface ISecurityTokenSetting
    {
        string ActionUniqueIdentifier { get; }
        bool IsEnabled { get; }
        string[] ExcludeNames { get; }
    }
}
