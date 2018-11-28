namespace Kendo.Mvc.UI.Fluent
{
    using System.Collections.Generic;
    using System.Collections;
    using System;
    using Kendo.Mvc.Extensions;

    /// <summary>
    /// Defines the fluent API for configuring the MapLayerDefaultsBubbleStyleFillSettings settings.
    /// </summary>
    public class MapLayerDefaultsBubbleStyleFillSettingsBuilder: IHideObjectMembers
    {
        private readonly MapLayerDefaultsBubbleStyleFillSettings container;

        public MapLayerDefaultsBubbleStyleFillSettingsBuilder(MapLayerDefaultsBubbleStyleFillSettings settings)
        {
            container = settings;
        }

        //>> Fields
        
        /// <summary>
        /// The default fill color for bubble layer symbols.
		/// Accepts a valid CSS color string, including hex and rgb.
        /// </summary>
        /// <param name="value">The value that configures the color.</param>
        public MapLayerDefaultsBubbleStyleFillSettingsBuilder Color(string value)
        {
            container.Color = value;

            return this;
        }
        
        /// <summary>
        /// The default fill opacity (0 to 1) for layer symbols.
        /// </summary>
        /// <param name="value">The value that configures the opacity.</param>
        public MapLayerDefaultsBubbleStyleFillSettingsBuilder Opacity(double value)
        {
            container.Opacity = value;

            return this;
        }
        
        //<< Fields
    }
}

