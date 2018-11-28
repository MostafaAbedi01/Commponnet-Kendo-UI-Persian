namespace Kendo.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Routing;
    using Kendo.Mvc.Extensions;

    public class DiagramConnection : JsonObject
    {
        public DiagramConnection()
        {
            //>> Initialization
        
            Hover = new DiagramConnectionHoverSettings();
                
            Points = new List<DiagramConnectionPoint>();
                
            Selection = new DiagramConnectionSelectionSettings();
                
            Stroke = new DiagramConnectionStrokeSettings();
                
        //<< Initialization
        }

        //>> Fields
        
        public DiagramConnectionStrokeSettings Stroke
        {
            get;
            set;
        }
        
        public DiagramConnectionHoverSettings Hover
        {
            get;
            set;
        }
        
        public string StartCap { get; set; }
        
        public string EndCap { get; set; }
        
        public List<DiagramConnectionPoint> Points
        {
            get;
            set;
        }
        
        public DiagramConnectionSelectionSettings Selection
        {
            get;
            set;
        }
        
        //<< Fields

        protected override void Serialize(IDictionary<string, object> json)
        {
            //>> Serialization
        
            var stroke = Stroke.ToJson();
            if (stroke.Any())
            {
                json["stroke"] = stroke;
            }
                
            var hover = Hover.ToJson();
            if (hover.Any())
            {
                json["hover"] = hover;
            }
                
            if (StartCap.HasValue())
            {
                json["startCap"] = StartCap;
            }
            
            if (EndCap.HasValue())
            {
                json["endCap"] = EndCap;
            }
            
            var points = Points.ToJson();
            if (points.Any())
            {
                json["points"] = points;
            }
                
            var selection = Selection.ToJson();
            if (selection.Any())
            {
                json["selection"] = selection;
            }
                
        //<< Serialization
        }
    }
}
