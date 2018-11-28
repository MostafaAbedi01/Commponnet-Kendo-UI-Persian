namespace Kendo.Mvc.UI.Fluent
{
    using System.Collections.Generic;
    using System.Collections;
    using System;
    using Kendo.Mvc.Extensions;

    /// <summary>
    /// Defines the fluent API for configuring the MapLayerDefaultsSettings settings.
    /// </summary>
    public class MapLayerDefaultsSettingsBuilder: IHideObjectMembers
    {
        private readonly MapLayerDefaultsSettings container;

        public MapLayerDefaultsSettingsBuilder(MapLayerDefaultsSettings settings)
        {
            container = settings;
        }

        //>> Fields
        
        /// <summary>
        /// The default configuration for marker layers.
        /// </summary>
        /// <param name="configurator">The action that configures the marker.</param>
        public MapLayerDefaultsSettingsBuilder Marker(Action<MapLayerDefaultsMarkerSettingsBuilder> configurator)
        {
            configurator(new MapLayerDefaultsMarkerSettingsBuilder(container.Marker));
            return this;
        }
        
        /// <summary>
        /// The default configuration for shape layers.
        /// </summary>
        /// <param name="configurator">The action that configures the shape.</param>
        public MapLayerDefaultsSettingsBuilder Shape(Action<MapLayerDefaultsShapeSettingsBuilder> configurator)
        {
            configurator(new MapLayerDefaultsShapeSettingsBuilder(container.Shape));
            return this;
        }
        
        /// <summary>
        /// The default configuration for bubble layers.
        /// </summary>
        /// <param name="configurator">The action that configures the bubble.</param>
        public MapLayerDefaultsSettingsBuilder Bubble(Action<MapLayerDefaultsBubbleSettingsBuilder> configurator)
        {
            configurator(new MapLayerDefaultsBubbleSettingsBuilder(container.Bubble));
            return this;
        }
        
        /// <summary>
        /// The default configuration for tile layers.
        /// </summary>
        /// <param name="configurator">The action that configures the tile.</param>
        public MapLayerDefaultsSettingsBuilder Tile(Action<MapLayerDefaultsTileSettingsBuilder> configurator)
        {
            configurator(new MapLayerDefaultsTileSettingsBuilder(container.Tile));
            return this;
        }
        
        /// <summary>
        /// The default configuration for Bing (tm) tile layers.
        /// </summary>
        /// <param name="configurator">The action that configures the bing.</param>
        public MapLayerDefaultsSettingsBuilder Bing(Action<MapLayerDefaultsBingSettingsBuilder> configurator)
        {
            configurator(new MapLayerDefaultsBingSettingsBuilder(container.Bing));
            return this;
        }
        
        //<< Fields

        
    }
}

