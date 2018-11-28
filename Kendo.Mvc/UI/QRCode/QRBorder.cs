﻿namespace Kendo.Mvc.UI
{
    using System.Collections.Generic;
    using System;

    public class QRBorder : JsonObject
    {
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        public int? Width { get; set; }

        public bool ShouldSerialize()
        {
            return !string.IsNullOrEmpty(this.Color)
                    || this.Width.HasValue;
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (this.Width.HasValue)
            {
                json["width"] = this.Width;
            }

            if (!String.IsNullOrEmpty(this.Color))
            {
                json["color"] = this.Color;
            }
        }
    }
}
