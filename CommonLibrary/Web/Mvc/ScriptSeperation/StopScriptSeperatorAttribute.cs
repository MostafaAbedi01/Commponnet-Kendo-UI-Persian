using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CommonLibrary.Web.Mvc.ScriptSeperation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class StopScriptSeperatorAttribute : ActionFilterAttribute
    {
    }
}
