using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Mehr.Web
{
    public class WebServicePathResolver : IPathResolver
    {
        public static readonly WebPathResolver Instance = new WebPathResolver();

        public string Resolve(string filePath)
        {
            if (!filePath.StartsWith("~/"))
                filePath = "~/" + filePath;

            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(filePath);
            return HostingEnvironment.MapPath(filePath);
        }
    }
}
