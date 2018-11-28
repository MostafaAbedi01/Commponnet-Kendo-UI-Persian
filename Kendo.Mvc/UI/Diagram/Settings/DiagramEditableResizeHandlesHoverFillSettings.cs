namespace Kendo.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Routing;
    using Kendo.Mvc.Extensions;

    public class DiagramEditableResizeHandlesHoverFillSettings : JsonObject
    {
        public DiagramEditableResizeHandlesHoverFillSettings()
        {
            //>> Initialization
        
        //<< Initialization
        }

        //>> Fields
        
        public string Color { get; set; }
        
        public double? Opacity { get; set; }
        
        //<< Fields

        protected override void Serialize(IDictionary<string, object> json)
        {
            //>> Serialization
        
            if (Color.HasValue())
            {
                json["color"] = Color;
            }
            
            if (Opacity.HasValue)
            {
                json["opacity"] = Opacity;
            }
                
        //<< Serialization
        }
    }
}
