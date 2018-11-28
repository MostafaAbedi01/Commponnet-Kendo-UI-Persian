﻿namespace Kendo.Mvc.UI
{
    using Kendo.Mvc.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    /// <summary>
    /// The server side wrapper for Kendo UI PivotConfigurator
    /// </summary>
    public class PivotConfigurator : WidgetBase
    {
        public PivotConfigurator(ViewContext viewContext,
                    IJavaScriptInitializer initializer,
                    IUrlGenerator urlGenerator
            )
            : base(viewContext, initializer)
        {
            UrlGenerator = urlGenerator;
            Messages = new PivotConfiguratorMessages();
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public bool Filterable
        {
            get;
            set;
        }

        public PivotConfiguratorMessages Messages
        {
            get;
            set;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            var options = this.SeriailzeBaseOptions();

            if (Filterable == true)
            {
                options["filterable"] = Filterable;
            }

            var messages = Messages.ToJson();
            if (messages.Count > 0)
            {
                options["messages"] = messages;
            }

            writer.Write(Initializer.Initialize(Selector, "PivotConfigurator", options));

            base.WriteInitializationScript(writer);
        }

        protected virtual IDictionary<string, object> SeriailzeBaseOptions()
        {
            var options = new Dictionary<string, object>(Events);

            return options;
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            PivotConfiguratorHtmlBuilder builder = new PivotConfiguratorHtmlBuilder(this);

            builder.Build().WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}
