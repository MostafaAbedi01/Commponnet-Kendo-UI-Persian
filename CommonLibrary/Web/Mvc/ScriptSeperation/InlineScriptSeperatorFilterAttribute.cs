using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ScriptSeperation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class InlineScriptSeperatorFilterAttribute : ScriptSeperatorFilterAttribute
    {
        public const string ScriptIdTagValue = "seperated";
        public static readonly ReplaceLinkAppendStrategy linkAppendStrategy = new ReplaceLinkAppendStrategy()
            {
                ReplaceOldValue = "<script id=\"" + ScriptIdTagValue + "\"></script>",
                ReplaceNewValueFormat = "<script id=\"" + ScriptIdTagValue + "\" src=\"{0}\"></script>",
            };

        public InlineScriptSeperatorFilterAttribute()
        {
            NotAddLoadedScript = true;
            LinkAppendStrategy = linkAppendStrategy;
        }
    }
}
