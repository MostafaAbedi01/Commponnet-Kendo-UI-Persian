using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mehr.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ValidationScriptSeperatorFilterAttribute : ActionFilterAttribute
    {
        public int? Version { get; set; }
        public ValidationScriptSeperatorFilterAttribute()
        {
        }

        public ValidationScriptSeperatorFilterAttribute(int version)
        {
            Version = version;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(StopValidationScriptSeperatorAttribute), true);
            if (attrs == null || attrs.Length == 0)
                if (!(filterContext.HttpContext.Response.Filter is ValidationScriptSeperatorStream))
                    //Action override Controller
                    filterContext.HttpContext.Response.Filter = new ValidationScriptSeperatorStream()
                    {
                        Base = filterContext.HttpContext.Response.Filter,
                        HttpContext = filterContext.HttpContext,
                        ScriptFileVersion = Version,
                    };
        }


        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is FileResult)
                if (filterContext.HttpContext.Response.Filter is ValidationScriptSeperatorStream)
                    filterContext.HttpContext.Response.Filter =
                        (filterContext.HttpContext.Response.Filter as ValidationScriptSeperatorStream).Base;
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class StopValidationScriptSeperatorAttribute : ActionFilterAttribute
    {
    }

}
