using System.Web.Hosting;
using Mehr;

namespace CommonLibrary.Web
{
    public class WebPathResolver : IPathResolver
    {
        public static readonly WebPathResolver Instance = new WebPathResolver();

        public string Resolve(string filePath)
        {
            if (!filePath.StartsWith("~/"))
                filePath = "~/" + filePath;

            return HostingEnvironment.MapPath(filePath);
        }
    }
}
