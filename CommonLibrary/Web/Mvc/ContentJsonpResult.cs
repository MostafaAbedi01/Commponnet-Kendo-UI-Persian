using System;
using System.Web.Mvc;

namespace CommonLibrary.Web.Mvc
{
    public class ContentJsonpResult : ActionResult
    {
        public string Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            string jsoncallback = (context.RouteData.Values["jsoncallback"] as string) ?? request["jsoncallback"];
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.ContentType = "application/x-javascript";
                response.Write(string.Format("{0}('", jsoncallback));
            }
            response.Write(Data);
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.Write("')");
            }
        }
    }
}
