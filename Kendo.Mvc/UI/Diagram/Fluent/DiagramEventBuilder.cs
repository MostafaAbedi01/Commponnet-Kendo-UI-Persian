namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the fluent API for configuring the Kendo Diagram for ASP.NET MVC events.
    /// </summary>
    public class DiagramEventBuilder: EventBuilder
    {
        public DiagramEventBuilder(IDictionary<string, object> events)
            : base(events)
        {
        }

        //>> Handlers
        
        /// <summary>
        /// Fired when an item is added or removed to/from the diagram.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the change event.</param>
        public DiagramEventBuilder Change(string handler)
        {
            Handler("change", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the location or size of an item are changed.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the itemBoundsChange event.</param>
        public DiagramEventBuilder ItemBoundsChange(string handler)
        {
            Handler("itemBoundsChange", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when an item is rotated.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the itemRotate event.</param>
        public DiagramEventBuilder ItemRotate(string handler)
        {
            Handler("itemRotate", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the user pans the diagram.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the pan event.</param>
        public DiagramEventBuilder Pan(string handler)
        {
            Handler("pan", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the user selects one or more items.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the select event.</param>
        public DiagramEventBuilder Select(string handler)
        {
            Handler("select", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the user starts changing the diagram zoom level.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the zoomStart event.</param>
        public DiagramEventBuilder ZoomStart(string handler)
        {
            Handler("zoomStart", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the user changes the diagram zoom level.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the zoomEnd event.</param>
        public DiagramEventBuilder ZoomEnd(string handler)
        {
            Handler("zoomEnd", handler);

            return this;
        }
        
        /// <summary>
        /// Fired when the user clicks on a shape or a connection.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the click event.</param>
        public DiagramEventBuilder Click(string handler)
        {
            Handler("click", handler);

            return this;
        }
        
        //<< Handlers
    }
}

