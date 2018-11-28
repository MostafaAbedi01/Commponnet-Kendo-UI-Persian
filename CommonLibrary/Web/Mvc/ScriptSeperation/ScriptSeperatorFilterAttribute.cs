using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CommonLibrary.Web.Mvc.ScriptSeperation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ScriptSeperatorFilterAttribute : ActionFilterAttribute
    {
        public ScriptSeperatorFilterAttribute() { }
        public bool NotAddLoadedScript { get; set; }
        public bool AbsolutePath { get; set; }
        public ILinkAppendStrategy LinkAppendStrategy { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(StopScriptSeperatorAttribute), true);
            if (attrs == null || attrs.Length == 0)
            {
                var filter = new ScriptSeperatorStream()
                {
                    Base = filterContext.HttpContext.Response.Filter,
                    HttpContext = filterContext.HttpContext,
                    NotAddLoadedScript = NotAddLoadedScript,
                    AbsolutePath = AbsolutePath,
                };
                if (LinkAppendStrategy != null)
                    filter.ServiceLocator.Set<ILinkAppendStrategy>(LinkAppendStrategy);
                filterContext.HttpContext.Response.Filter =filter;
            }
        }


        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Remove filter for FileResult result
            if (filterContext.Result is FileResult)
                if (filterContext.HttpContext.Response.Filter is ScriptSeperatorStream)
                    filterContext.HttpContext.Response.Filter =
                        (filterContext.HttpContext.Response.Filter as ScriptSeperatorStream).Base;
        }
    }


}
