﻿namespace Kendo.Mvc.UI
{
    using Kendo.Mvc.Infrastructure;
    using Kendo.Mvc.UI.Html;
    using System.Collections;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public class MultiSelect : ListBase
    {
        //Escape meta characters: http://api.jquery.com/category/selectors/
        private static readonly Regex EscapeRegex = new Regex(@"([;&,\.\+\*~'\:\""\!\^\$\[\]\(\)\|\/])", RegexOptions.Compiled);

        public MultiSelect(ViewContext viewContext, IJavaScriptInitializer initializer, ViewDataDictionary viewData, IUrlGenerator urlGenerator)
            : base(viewContext, initializer, viewData, urlGenerator)
        {
        }

        public bool? AutoBind
        {
            get;
            set;
        }

        public bool? AutoClose
        {
            get;
            set;
        }

        public string DataValueField
        {
            get;
            set;
        }

        public bool? HighlightFirst
        {
            get;
            set;
        }

        public int? MaxSelectedItems
        {
            get;
            set;
        }

        public string Placeholder
        {
            get;
            set;
        }

        public string ItemTemplate
        {
            get;
            set;
        }

        public string ItemTemplateId
        {
            get;
            set;
        }

        public string TagTemplate
        {
            get;
            set;
        }

        public string TagTemplateId
        {
            get;
            set;
        }

        public IEnumerable Value
        {
            get;
            set;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            if (DataSource.ServerFiltering && !DataSource.Transport.Read.Data.HasValue())
            {
                DataSource.Transport.Read.Data = new ClientHandlerDescriptor
                {
                    HandlerName = "function() { return kendo.ui.MultiSelect.requestData(jQuery(\"" + EscapeRegex.Replace(Selector, @"\\$1") + "\")); }"
                };
            }

            var idPrefix = "#";
            if (IsInClientTemplate)
            {
                idPrefix = "\\" + idPrefix;
            }

            var options = this.SeriailzeBaseOptions();

            if (!string.IsNullOrEmpty(ItemTemplateId))
            {
                options["itemTemplate"] = new ClientHandlerDescriptor { HandlerName = string.Format("jQuery(\"{0}{1}\").html()", idPrefix, ItemTemplateId) };
            }
            else if (!string.IsNullOrEmpty(ItemTemplate))
            {
                options["itemTemplate"] = ItemTemplate;
            }

            if (!string.IsNullOrEmpty(TagTemplateId))
            {
                options["tagTemplate"] = new ClientHandlerDescriptor { HandlerName = string.Format("jQuery(\"{0}{1}\").html()", idPrefix, TagTemplateId) };
            }
            else if (!string.IsNullOrEmpty(TagTemplate))
            {
                options["tagTemplate"] = TagTemplate;
            }

            if (AutoBind != null)
            {
                options["autoBind"] = AutoBind;
            }

            if (AutoClose != null)
            {
                options["autoClose"] = AutoClose;
            }

            if (!string.IsNullOrEmpty(DataValueField))
            {
                options["dataValueField"] = DataValueField;
            }

            if (HighlightFirst != null)
            {
                options["highlightFirst"] = HighlightFirst;
            }

            if (MaxSelectedItems != null)
            {
                options["maxSelectedItems"] = MaxSelectedItems;
            }

            if (!string.IsNullOrEmpty(Placeholder))
            {
                options["placeholder"] = Placeholder;
            }

            var value = GetValue();

            if (value != null)
            {
                options["value"] = value;
            }

            writer.Write(Initializer.Initialize(Selector, "MultiSelect", options));

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            new MultiSelectHtmlBuilder(this).Build().WriteTo(writer);

            base.WriteHtml(writer);
        }

        private IEnumerable GetValue()
        {
            ModelState state;
            if (ViewData.ModelState.TryGetValue(Name, out state) && (state.Value != null))
            {
                //if (ViewData.ModelState.IsValidField(Name)) TODO: Do I need this ?
                return state.Value.ConvertTo(typeof(string[]), null) as IEnumerable;
            }
            else if (Value == null)
            {
                return ViewData.Eval(Name) as IEnumerable;
            }

            return Value;
        }
    }
}
