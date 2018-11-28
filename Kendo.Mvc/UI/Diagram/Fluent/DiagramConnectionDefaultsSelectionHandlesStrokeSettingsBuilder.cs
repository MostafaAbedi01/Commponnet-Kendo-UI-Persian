namespace Kendo.Mvc.UI.Fluent
{
    using System.Collections.Generic;
    using System.Collections;
    using System;
    using Kendo.Mvc.Extensions;

    /// <summary>
    /// Defines the fluent API for configuring the DiagramConnectionDefaultsSelectionHandlesStrokeSettings settings.
    /// </summary>
    public class DiagramConnectionDefaultsSelectionHandlesStrokeSettingsBuilder: IHideObjectMembers
    {
        private readonly DiagramConnectionDefaultsSelectionHandlesStrokeSettings container;

        public DiagramConnectionDefaultsSelectionHandlesStrokeSettingsBuilder(DiagramConnectionDefaultsSelectionHandlesStrokeSettings settings)
        {
            container = settings;
        }

        //>> Fields
        
        /// <summary>
        /// Defines the handles stroke color.
        /// </summary>
        /// <param name="value">The value that configures the color.</param>
        public DiagramConnectionDefaultsSelectionHandlesStrokeSettingsBuilder Color(string value)
        {
            container.Color = value;

            return this;
        }
        
        //<< Fields
    }
}

