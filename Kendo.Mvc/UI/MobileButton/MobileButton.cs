namespace Kendo.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Kendo.Mvc.Infrastructure;

    public class MobileButton : WidgetBase
    {
        private readonly IUrlGenerator urlGenerator;

        public MobileButton(ViewContext viewContext, IJavaScriptInitializer initializer, IUrlGenerator urlGenerator)
            : base(viewContext, initializer)
        {
            this.urlGenerator = urlGenerator;
//>> Initialization
        
        //<< Initialization
        }

//>> Fields
        
        public bool Enable { get; set; }
        
        public string Icon { get; set; }
        
        public string Url { get; set; }
        
        public string Text { get; set; }
        
        public string Transition { get; set; }
        
        public string Target { get; set; }
        
        public string ActionsheetContext { get; set; }
        
        public MobileButtonAlign Align { get; set; }
        
        public MobileButtonRel Rel { get; set; }
        
        public string Badge { get; set; }
        
        //<< Fields

        public IUrlGenerator UrlGenerator
        {
            get
            {
                return urlGenerator;
            }
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            //no initializtion scripts for mobile widgets
        }
        
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            
            var html = new MobileButtonHtmlBuilder(this).Build();

            html.WriteTo(writer);

            //prevent rendering empty script tag
            //base.WriteHtml(writer);
        }
    }
}

