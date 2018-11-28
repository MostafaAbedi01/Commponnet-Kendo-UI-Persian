namespace Kendo.Mvc.UI.Fluent
{
    using System.Collections.Generic;
    using System.Collections;
    using System;
    using Kendo.Mvc.Extensions;

    /// <summary>
    /// Defines the fluent API for configuring the DiagramConnectionSelectionHandlesStrokeSettings settings.
    /// </summary>
    public class DiagramConnectionSelectionHandlesStrokeSettingsBuilder: IHideObjectMembers
    {
        private readonly DiagramConnectionSelectionHandlesStrokeSettings container;

        public DiagramConnectionSelectionHandlesStrokeSettingsBuilder(DiagramConnectionSelectionHandlesStrokeSettings settings)
        {
            container = settings;
        }

        //>> Fields
        
        /// <summary>
        /// Defines the handles stroke color.
        /// </summary>
        /// <param name="value">The value that configures the color.</param>
        public DiagramConnectionSelectionHandlesStrokeSettingsBuilder Color(string value)
        {
            container.Color = value;

            return this;
        }
        
        //<< Fields
    }
}

