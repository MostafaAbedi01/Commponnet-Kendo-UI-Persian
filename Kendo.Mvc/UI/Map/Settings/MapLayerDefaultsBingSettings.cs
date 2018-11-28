namespace Kendo.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Routing;
    using Kendo.Mvc.Extensions;

    public class MapLayerDefaultsBingSettings : JsonObject
    {
        public MapLayerDefaultsBingSettings()
        {
            //>> Initialization
        
        //<< Initialization
        }

        //>> Fields
        
        public string Attribution { get; set; }
        
        public double? Opacity { get; set; }
        
        public string Key { get; set; }
        
        public MapLayersImagerySet? ImagerySet { get; set; }
        
        //<< Fields

        protected override void Serialize(IDictionary<string, object> json)
        {
            //>> Serialization
        
            if (Attribution.HasValue())
            {
                json["attribution"] = Attribution;
            }
            
            if (Opacity.HasValue)
            {
                json["opacity"] = Opacity;
            }
                
            if (Key.HasValue())
            {
                json["key"] = Key;
            }
            
            if (ImagerySet.HasValue)
            {
                json["imagerySet"] = ImagerySet;
            }
                
        //<< Serialization
        }
    }
}
