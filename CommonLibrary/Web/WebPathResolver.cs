using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Messagging;
using System.Web;
using System.Web.Hosting;

namespace Mehr.Web
{
    public class WebPathResolver : IPathResolver
    {
        public static readonly WebPathResolver Instance = new WebPathResolver();

        public string Resolve(string filePath)
        {
            if (!filePath.StartsWith("~/"))
                filePath = "~/" + filePath;

            return HostingEnvironment.MapPath(filePath);
            //return HttpContext.Current.Server.MapPath(filePath);
        }
    }
}
