using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace CommonLibrary
{
    public class Utils
    {
        public static void CreateThumbnail(string originalFileFullPath, int? width = null, int? height = null,
            string imageUploadPath = null)
        {
            if (imageUploadPath == null) imageUploadPath = "/Uploads/";

            if (File.Exists(originalFileFullPath))
            {
                var img = Image.FromFile(originalFileFullPath);
                var bmp = new Bitmap(img);
                if (width == null) width = 150;
                if (height == null) height = 150;
                bmp = BitmapManipulator.ThumbnailBitmap(bmp, (int) width, (int) height);

                var thumbfilename = "Thumb_" + Path.GetFileNameWithoutExtension(originalFileFullPath) +
                                    Path.GetExtension(originalFileFullPath);

                var thumbFileRelativePath = imageUploadPath + thumbfilename;

                bmp.Save(HttpContext.Current.Server.MapPath(thumbFileRelativePath), ImageFormat.Jpeg);
                img.Dispose();
                bmp.Dispose();
            }
        }
    }

    public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public MyHttpControllerHandler(RouteData routeData)
            : base(routeData)
        {
        }
    }

    public class MyHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyHttpControllerHandler(requestContext.RouteData);
        }
    }
}