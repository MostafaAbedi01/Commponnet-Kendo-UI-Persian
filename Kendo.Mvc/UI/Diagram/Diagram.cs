namespace Kendo.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Kendo.Mvc.Infrastructure;
    using Kendo.Mvc.Extensions;

    public class Diagram : WidgetBase
    {
        public IUrlGenerator urlGenerator;

        public Diagram(ViewContext viewContext, IJavaScriptInitializer initializer, IUrlGenerator urlGenerator)
            : base(viewContext, initializer)
        {
            this.urlGenerator = urlGenerator;

            DataSource = new DataSource();
            DataSource.ModelType(typeof(object));

            //>> Initialization
        
            ConnectionDefaults = new DiagramConnectionDefaultsSettings();
                
            Connections = new List<DiagramConnection>();
                
            Editable = new DiagramEditableSettings();
                
            Layout = new DiagramLayoutSettings();
                
            Selectable = new DiagramSelectableSettings();
                
            ShapeDefaults = new DiagramShapeDefaultsSettings();
                
            Shapes = new List<DiagramShape>();
                
        //<< Initialization
        }

        public DataSource DataSource
        {
            get;
            private set;
        }

//>> Fields
        
        public bool? AutoBind { get; set; }
        
        public double? ZoomRate { get; set; }
        
        public double? Zoom { get; set; }
        
        public double? ZoomMin { get; set; }
        
        public double? ZoomMax { get; set; }
        
        public DiagramEditableSettings Editable
        {
            get;
            set;
        }
        
        public DiagramLayoutSettings Layout
        {
            get;
            set;
        }
        
        public string Template { get; set; }

        public string TemplateId { get; set; }
        
        public DiagramConnectionDefaultsSettings ConnectionDefaults
        {
            get;
            set;
        }
        
        public List<DiagramConnection> Connections
        {
            get;
            set;
        }
        
        public DiagramSelectableSettings Selectable
        {
            get;
            set;
        }
        
        public DiagramShapeDefaultsSettings ShapeDefaults
        {
            get;
            set;
        }
        
        public List<DiagramShape> Shapes
        {
            get;
            set;
        }
        
        //<< Fields

        public override void WriteInitializationScript(TextWriter writer)
        {
            var json = new Dictionary<string, object>(Events);

            if (!string.IsNullOrEmpty(DataSource.Transport.Read.Url) || DataSource.Type == DataSourceType.Custom)
            {
                json["dataSource"] = DataSource.ToJson();
            }
            else if (DataSource.Data != null)
            {
                json["dataSource"] = DataSource.Data;
            }

//>> Serialization
        
            if (AutoBind.HasValue)
            {
                json["autoBind"] = AutoBind;
            }
                
            if (ZoomRate.HasValue)
            {
                json["zoomRate"] = ZoomRate;
            }
                
            if (Zoom.HasValue)
            {
                json["zoom"] = Zoom;
            }
                
            if (ZoomMin.HasValue)
            {
                json["zoomMin"] = ZoomMin;
            }
                
            if (ZoomMax.HasValue)
            {
                json["zoomMax"] = ZoomMax;
            }
                
            var layout = Layout.ToJson();
            if (layout.Any())
            {
                json["layout"] = layout;
            }
                
            if (!string.IsNullOrEmpty(TemplateId))
            {
                json["template"] = new ClientHandlerDescriptor {
                    HandlerName = string.Format(
                        "jQuery('#{0}').html()",
                        TemplateId
                    )
                };
            }
            else if (!string.IsNullOrEmpty(Template))
            {
                json["template"] = Template;
            }
                
            var connectionDefaults = ConnectionDefaults.ToJson();
            if (connectionDefaults.Any())
            {
                json["connectionDefaults"] = connectionDefaults;
            }
                
            var connections = Connections.ToJson();
            if (connections.Any())
            {
                json["connections"] = connections;
            }
                
            var selectable = Selectable.ToJson();
            if (selectable.Any())
            {
                json["selectable"] = selectable;
            }
                
            var shapeDefaults = ShapeDefaults.ToJson();
            if (shapeDefaults.Any())
            {
                json["shapeDefaults"] = shapeDefaults;
            }
                
            var shapes = Shapes.ToJson();
            if (shapes.Any())
            {
                json["shapes"] = shapes;
            }
                
        //<< Serialization

            if (Editable != null)
            {
                var editable = Editable.ToJson();
                if (editable.Any())
                {
                    json["editable"] = editable;
                }
            }
            else 
            {
                json["editable"] = false;
            }

            writer.Write(Initializer.Initialize(Selector, "Diagram", json));

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            var html = new DiagramHtmlBuilder(this).Build();

            html.WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}

