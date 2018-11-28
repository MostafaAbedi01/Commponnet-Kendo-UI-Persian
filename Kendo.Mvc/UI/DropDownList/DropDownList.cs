namespace Kendo.Mvc.UI
{
    using Kendo.Mvc.Infrastructure;
    using Kendo.Mvc.UI.Html;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public class DropDownList : DropDownListBase
    {
        //Escape meta characters: http://api.jquery.com/category/selectors/
        private static readonly Regex EscapeRegex = new Regex(@"([;&,\.\+\*~'\:\""\!\^\$\[\]\(\)\|\/])", RegexOptions.Compiled);

        public DropDownList(ViewContext viewContext, IJavaScriptInitializer initializer, ViewDataDictionary viewData, IUrlGenerator urlGenerator)
            : base(viewContext, initializer, viewData, urlGenerator)
        {
        }

        public bool? AutoBind
        {
            get;
            set;
        }

        public string CascadeFrom
        {
            get;
            set;
        }

        public string CascadeFromField
        {
            get;
            set;
        }

        public string DataValueField
        {
            get;
            set;
        }

        public object OptionLabel
        {
            get;
            set;
        }

        public int? SelectedIndex
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public string ValueTemplate
        {
            get;
            set;
        }

        public string ValueTemplateId
        {
            get;
            set;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            if (DataSource.ServerFiltering && !DataSource.Transport.Read.Data.HasValue() && DataSource.Type != DataSourceType.Custom)
            {
                DataSource.Transport.Read.Data = new ClientHandlerDescriptor
                {
                    HandlerName = "function() { return kendo.ui.DropDownList.requestData(jQuery(\"" + EscapeRegex.Replace(Selector, @"\\$1") + "\")); }"
                };
            }

            var options = this.SeriailzeBaseOptions();

            var idPrefix = "#";
            if (IsInClientTemplate)
            {
                idPrefix = "\\" + idPrefix;
            }

            if (!string.IsNullOrEmpty(ValueTemplateId))
            {
                options["valueTemplate"] = new ClientHandlerDescriptor { HandlerName = string.Format("jQuery(\"{0}{1}\").html()", idPrefix, ValueTemplateId) };
            }
            else if (!string.IsNullOrEmpty(ValueTemplate))
            {
                options["valueTemplate"] = ValueTemplate;
            }

            if (AutoBind != null)
            {
                options["autoBind"] = AutoBind;
            }

            if (!string.IsNullOrEmpty(DataValueField))
            {
                options["dataValueField"] = DataValueField;
            }

            if (SelectedIndex != null && SelectedIndex > -1)
            {
                options["index"] = SelectedIndex;
            }

            if (OptionLabel != null)
            {
                options["optionLabel"] = OptionLabel;
            }

            if (!string.IsNullOrEmpty(CascadeFrom))
            {
                options["cascadeFrom"] = CascadeFrom;
            }

            if (!string.IsNullOrEmpty(CascadeFromField))
            {
                options["cascadeFromField"] = CascadeFromField;
            }

            if (!string.IsNullOrEmpty(Text))
            {
                options["text"] = Text;
            }

            writer.Write(Initializer.Initialize(Selector, "DropDownList", options));

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            new DropDownListHtmlBuilderBase(this).Build().WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}
