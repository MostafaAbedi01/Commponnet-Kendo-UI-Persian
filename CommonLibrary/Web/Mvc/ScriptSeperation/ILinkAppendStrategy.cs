using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ScriptSeperation
{
    public interface ILinkAppendStrategy
    {
        string Append(string pageHtmlContent, string[] seperatedScriptUrls);

    }
}
