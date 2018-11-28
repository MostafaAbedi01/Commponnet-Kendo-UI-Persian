namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the fluent API for configuring the Kendo Map for ASP.NET MVC events.
    /// </summary>
    public class MapEventBuilder: EventBuilder
    {
        public MapEventBuilder(IDictionary<string, object> events)
            : base(events)
        {
        }

        //>> Handlers
        
        /// <summary>
        /// Fired immediately before the map is reset.
		/// This event is typically used for cleanup by layer implementers.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the beforeReset event.</param>
        public MapEventBuilder BeforeReset(string handler)
        {
            Handler("beforeReset", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the user clicks on the map.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the click event.</param>
        public MapEventBuilder Click(string handler)
        {
            Handler("click", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when a marker has been displayed.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the markerActivate event.</param>
        public MapEventBuilder MarkerActivate(string handler)
        {
            Handler("markerActivate", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when a marker has been created and is about to be displayed.
		/// Cancelling the event will prevent the marker from being shown.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the markerCreated event.</param>
        public MapEventBuilder MarkerCreated(string handler)
        {
            Handler("markerCreated", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when a marker has been clicked or tapped.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the markerClick event.</param>
        public MapEventBuilder MarkerClick(string handler)
        {
            Handler("markerClick", handler);

            return this;
        }
        
        /// <summary>
        /// Fired while the map viewport is being moved.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the pan event.</param>
        public MapEventBuilder Pan(string handler)
        {
            Handler("pan", handler);

            return this;
        }
        
        /// <summary>
        /// Fires after the map viewport has been moved.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the panEnd event.</param>
        public MapEventBuilder PanEnd(string handler)
        {
            Handler("panEnd", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the map is reset.
		/// This typically occurs on initial load and after a zoom/center change.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the reset event.</param>
        public MapEventBuilder Reset(string handler)
        {
            Handler("reset", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when a shape is clicked or tapped.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the shapeClick event.</param>
        public MapEventBuilder ShapeClick(string handler)
        {
            Handler("shapeClick", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when a shape is created, but is not rendered yet.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the shapeCreated event.</param>
        public MapEventBuilder ShapeCreated(string handler)
        {
            Handler("shapeCreated", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the mouse enters a shape.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the shapeMouseEnter event.</param>
        public MapEventBuilder ShapeMouseEnter(string handler)
        {
            Handler("shapeMouseEnter", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the mouse leaves a shape.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the shapeMouseLeave event.</param>
        public MapEventBuilder ShapeMouseLeave(string handler)
        {
            Handler("shapeMouseLeave", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the map zoom level is about to change.
		/// Cancelling the event will prevent the user action.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the zoomStart event.</param>
        public MapEventBuilder ZoomStart(string handler)
        {
            Handler("zoomStart", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the map zoom level has changed.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the zoomEnd event.</param>
        public MapEventBuilder ZoomEnd(string handler)
        {
            Handler("zoomEnd", handler);

            return this;
        }
        
        //<< Handlers
    }
}

