namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the fluent API for configuring the Kendo MobileButton for ASP.NET MVC events.
    /// </summary>
    public class MobileButtonEventBuilder: EventBuilder
    {
        public MobileButtonEventBuilder(IDictionary<string, object> events)
            : base(events)
        {
        }

        //>> Handlers
        
        /// <summary>
        /// Fires when the user taps the button.
        /// </summary>
        /// <param name="handler">The name of the JavaScript function that will handle the click event.</param>
        public MobileButtonEventBuilder Click(string handler)
        {
            Handler("click", handler);

            return this;
        }
        
        //<< Handlers
    }
}

