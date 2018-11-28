namespace Kendo.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Routing;
    using Kendo.Mvc.Extensions;
    using System.Web.Util;
    using System.Web;

    public class ToolBarItemMenuButton : JsonObject
    {
        public ToolBarItemMenuButton()
        {
            //>> Initialization
        
        //<< Initialization

            HtmlAttributes = new RouteValueDictionary();
        }

        //>> Fields
        
        public bool? Enable { get; set; }
        
        public string Icon { get; set; }
        
        public string Id { get; set; }
        
        public string ImageUrl { get; set; }
        
        public string SpriteCssClass { get; set; }
        
        public string Text { get; set; }
        
        public string Url { get; set; }
        
        //<< Fields

        public IDictionary<string, object> HtmlAttributes { get; set; }

        protected override void Serialize(IDictionary<string, object> json)
        {
            //>> Serialization
                
            if (Enable.HasValue)
            {
                json["enable"] = Enable;
            }
                
            if (Icon.HasValue())
            {
                json["icon"] = Icon;
            }
            
            if (Id.HasValue())
            {
                json["id"] = Id;
            }
            
            if (ImageUrl.HasValue())
            {
                json["imageUrl"] = ImageUrl;
            }
            
            if (SpriteCssClass.HasValue())
            {
                json["spriteCssClass"] = SpriteCssClass;
            }
            
            if (Text.HasValue())
            {
                json["text"] = Text;
            }
            
            if (Url.HasValue())
            {
                json["url"] = Url;
            }
            
        //<< Serialization

            if (HtmlAttributes.Any())
            {
                var attributes = new Dictionary<string, object>();

                var hasAntiXss = HttpEncoder.Current != null && HttpEncoder.Current.GetType().ToString().Contains("AntiXssEncoder");

                HtmlAttributes.Each(attr =>
                {
                    var value = HttpUtility.HtmlAttributeEncode(attr.Value.ToString());
                    if (hasAntiXss)
                    {
                        value = value.Replace("&#32;", " ");
                    }
                    attributes[HttpUtility.HtmlAttributeEncode(attr.Key)] = value;
                });

                json["attributes"] = attributes;
            }

        }
    }
}
