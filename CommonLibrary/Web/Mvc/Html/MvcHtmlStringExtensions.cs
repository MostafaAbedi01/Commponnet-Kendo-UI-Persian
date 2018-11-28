using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace CommonLibrary.Web.Mvc.Html
{
    public static class MvcHtmlStringExtensions
    {
        public static void Render(this MvcHtmlString stringText)
        {
            HttpContext.Current.Response.Write(stringText.ToHtmlString());
        }
    }
}
