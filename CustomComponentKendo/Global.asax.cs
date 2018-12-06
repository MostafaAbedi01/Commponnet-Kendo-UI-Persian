using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CommonLibrary;

namespace CustomComponentKendo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture
            {
                DateTimeFormat =
                {
                    ShortDatePattern = "yyyy/MM/dd",
                    LongDatePattern = "dddd d MMMM yyyy",
                    AMDesignator = "صبح",
                    PMDesignator = "عصر"
                }
            };
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }
    }
}
