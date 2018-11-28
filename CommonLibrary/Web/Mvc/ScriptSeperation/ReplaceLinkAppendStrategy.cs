using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ScriptSeperation
{
    public class ReplaceLinkAppendStrategy : ILinkAppendStrategy
    {
        public string ReplaceOldValue { get; set; }
        public string ReplaceNewValueFormat { get; set; }

        public ReplaceLinkAppendStrategy()
        {
            ReplaceOldValue = "</head>";
            ReplaceNewValueFormat = @"<script src=""{0}""></script></head>";
        }

        public string Append(string pageHtmlContent, string[] seperatedScriptUrls)
        {
            foreach (var url in seperatedScriptUrls)
            {
                pageHtmlContent = pageHtmlContent.Replace(ReplaceOldValue, string.Format(ReplaceNewValueFormat, url));
            }
            return pageHtmlContent;
        }
    }
}
