using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CommonLibrary.Web.Mvc.Html
{
    public static class KnockoutHtmlExtensions
    {
        public static MvcHtmlString KnockoutSerializeModel<TModel>(this HtmlHelper<TModel> html, TModel model,
                                                            bool includeScriptTags = false)
        {
            return KnockoutSerializeModel(html, (object)model, includeScriptTags);
        }

        public static MvcHtmlString KnockoutSerializeModel(this HtmlHelper html, object model, bool includeScriptTags = false)
        {
            if (model == null) throw new ArgumentNullException("model");
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var serialized = serializer.Serialize(model);
            string script = string.Format("var viewModelJs = {0};", serialized);
            if (includeScriptTags) script = CreateScriptBlock(script);
            return MvcHtmlString.Create(script);
        }

        public static MvcHtmlString KnockoutInitializeModel(this HtmlHelper html, bool initialize = true, bool includeScriptTags = false)
        {
            string script = string.Format("var viewModel = ko.mapping.fromJS(viewModelJs);{0}",
                                          initialize ? "ko.applyBindings(viewModel);" : string.Empty);
            if (includeScriptTags) script = CreateScriptBlock(script);
            return MvcHtmlString.Create(script);
        }

        public static MvcHtmlString KnockoutApplyBinding(this HtmlHelper html, bool includeScriptTags = false)
        {
            var script = "ko.applyBindings(viewModel);";
            if (includeScriptTags) script = CreateScriptBlock(script);
            return MvcHtmlString.Create(script);
        }

        private static string CreateScriptBlock(string script)
        {
            return string.Format("<script type=\"text/javascript\">{0}</script>", script);
        }
    }


}
