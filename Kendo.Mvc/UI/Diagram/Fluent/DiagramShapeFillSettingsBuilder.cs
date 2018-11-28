namespace Kendo.Mvc.UI.Fluent
{
    using System.Collections.Generic;
    using System.Collections;
    using System;
    using Kendo.Mvc.Extensions;

    /// <summary>
    /// Defines the fluent API for configuring the DiagramShapeFillSettings settings.
    /// </summary>
    public class DiagramShapeFillSettingsBuilder: IHideObjectMembers
    {
        private readonly DiagramShapeFillSettings container;

        public DiagramShapeFillSettingsBuilder(DiagramShapeFillSettings settings)
        {
            container = settings;
        }

        //>> Fields
        
        /// <summary>
        /// Defines the fill color of the shape.
        /// </summary>
        /// <param name="value">The value that configures the color.</param>
        public DiagramShapeFillSettingsBuilder Color(string value)
        {
            container.Color = value;

            return this;
        }
        
        /// <summary>
        /// Defines the fill opacity of the shape.
        /// </summary>
        /// <param name="value">The value that configures the opacity.</param>
        public DiagramShapeFillSettingsBuilder Opacity(double value)
        {
            container.Opacity = value;

            return this;
        }
        
        //<< Fields
    }
}

