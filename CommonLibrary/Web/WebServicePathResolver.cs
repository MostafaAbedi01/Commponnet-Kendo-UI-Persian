using System.Web;
using System.Web.Hosting;
using Mehr;

namespace CommonLibrary.Web
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
