using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using System.Diagnostics.Contracts;
using System.Web.Routing;
using Mehr.Reflection;
using Mehr.Web.Mvc.JqGrid;
using Mehr.Web.Mvc.ScriptSeperation;

namespace CommonLibrary.Web.Mvc.Html
{
    public static class UiHelperExtensions
    {
        public static MvcHtmlString AsReverseDateString(this long value)
        { return MvcHtmlString.Create(Mehr.PersianDateTime.FromLong(value).ToReverseDateString()); }

        public static MvcHtmlString AsDateString(this long value)
        { return MvcHtmlString.Create(Mehr.PersianDateTime.FromLong(value).ToDateString()); }

        public static MvcHtmlString AsDateString(this long value, string fromat)
        { return MvcHtmlString.Create(Mehr.PersianDateTime.FromLong(value).ToString(fromat)); }

        public static MvcHtmlString AsReverseDateString(this long? value)
        { return value == null ? MvcHtmlString.Empty : value.Value.AsReverseDateString(); }

        public static MvcHtmlString AsDateString(this long? value)
        { return value == null ? MvcHtmlString.Empty : value.Value.AsDateString(); }

        public static MvcHtmlString TabsOptions(this HtmlHelper html, TabsViewModel model)
        { return html.JsonScript(model.Id + "_op", model); }

        public static MvcHtmlString JsonScript(this HtmlHelper html, string id, object jsonObject)
        { return html.TextScript(id, new JavaScriptSerializer().Serialize(jsonObject)); }

        public static MvcHtmlString TextScript(this HtmlHelper html, string id, string content)
        {
            TagBuilder tag = new TagBuilder("script");
            tag.Attributes.Add("Id", id);
            tag.Attributes.Add("type", "text/html");
            tag.InnerHtml = content;
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString TabsOptions(this HtmlHelper html, string link, string[] tabTiltes)
        {
            return TabsOptions(html, new TabsViewModel()
            {
                ContentLink = link,
                TabTitles = tabTiltes
            });
        }

        public static MvcHtmlString JqGridOptions(this HtmlHelper html, string id = "grid", string url = null, string detailPageUrl = null, string editPageUrl = null)
        { return html.JqGridOptions(id, new { url, detailPageUrl, editPageUrl }); }

        public static MvcHtmlString JqGridOptions(this HtmlHelper html, string id = "grid", object options = null)
        { return html.JsonScript(id + "_op", options ?? new { }); }

        const string gridScriptTemplate = @"<script filename=""{3}.{0}"">" +
                     @"$('body').live('{1}', function(){{ new m.grid('{3}',{2}).build(); }});</script>";

        public static MvcHtmlString JqGrid(this HtmlHelper html, Grid grid, string id = "grid", object options = null)
        {
            return MvcHtmlString.Create(
                string.Format(gridScriptTemplate,
                    grid.GetType().Name + grid.DefinitionVersion,
                    grid.ClientBuildEventName,
                    grid.GetClientModelAsJson(),
                    id) +
                html.JqGridOptions(id, options).ToHtmlString()
                );
        }

        const string gridDialogScriptTemplate = @"<script filename=""{2}.{0}"">" +
                   @"$(function(){{ new m.grid('{2}',{1}).build(); }});</script>" +
                   @"<script id=""" + InlineScriptSeperatorFilterAttribute.ScriptIdTagValue + @"""></script>";
        public static MvcHtmlString DialogJqGrid(this HtmlHelper html, Grid grid, string id = "grid", object options = null)
        {
            return MvcHtmlString.Create(
                html.JqGridOptions(id, options).ToHtmlString() +
                string.Format(gridDialogScriptTemplate,
                    grid.GetType().Name + grid.DefinitionVersion,
                    grid.GetClientModelAsJson(),
                    id)
                );
        }


        public static MvcHtmlString EnumAsHidden<T>(this HtmlHelper html, string emptyItemTtile = "<<همه>>", string name = null)
        {
            var factory = new EnumMetadataFactory(new HttpContextCacheProvider());
            return html.Hidden("ECS_" + (name ?? typeof(T).Name), ":" + emptyItemTtile + ";" + factory.Get<T>().GetAsString());
        }

    }
}
