using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mehr.Web.Mvc
{
    public class TabsViewModel
    {
        public const string IndexParamsPlaceHolder = "#####";

        public string ContentLink { get; set; }

        public string[] TabTitles { get; set; }

        public string Id { get; set; }

        public TabsViewModel()
        {
            Id = "tabs";
        }
    }
}
