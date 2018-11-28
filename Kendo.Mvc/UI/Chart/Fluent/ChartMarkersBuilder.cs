namespace Kendo.Mvc.UI.Fluent
{
    using System;

    /// <summary>
    /// Defines the fluent interface for configuring the chart data labels.
    /// </summary>
    public class ChartMarkersBuilder
    {
        private readonly ChartMarkers lineMarkers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartMarkersBuilder" /> class.
        /// </summary>
        /// <param name="chartLineMarkers">The line chart markers configuration.</param>
        public ChartMarkersBuilder(ChartMarkers chartLineMarkers)
        {
            lineMarkers = chartLineMarkers;
        }

        /// <summary>
        /// Sets the markers shape type.
        /// </summary>
        /// <param name="type">The markers shape type.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Markers(markers => markers
        ///                   .Type(ChartMarkerShape.Triangle)
        ///               );
        ///            )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartMarkersBuilder Type(ChartMarkerShape type)
        {
            lineMarkers.Type = type;
            return this;
        }

        /// <summary>
        /// Sets the markers size.
        /// </summary>
        /// <param name="size">The markers size.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Markers(markers => markers
        ///                   .Size(10)
        ///               );
        ///            )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartMarkersBuilder Size(int size)
        {
            lineMarkers.Size = size;
            return this;
        }

        /// <summary>
        /// Sets the markers visibility
        /// </summary>
        /// <param name="visible">The markers visibility.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Markers(markers => markers
        ///                   .Visible(true)
        ///               );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartMarkersBuilder Visible(bool visible)
        {
            lineMarkers.Visible = visible;
            return this;
        }

        /// <summary>
        /// Sets the markers border
        /// </summary>
        /// <param name="width">The markers border width.</param>
        /// <param name="color">The markers border color (CSS syntax).</param>
        /// <param name="dashType">The markers border dash type.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Markers(markers => markers
        ///                    .Border(1, "Red", ChartDashType.Dot)
        ///                );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartMarkersBuilder Border(int width, string color, ChartDashType dashType)
        {
            lineMarkers.Border = new ChartElementBorder(width, color, dashType);
            return this;
        }

        /// <summary>
        /// Configures the markers border
        /// </summary>
        /// <param name="configurator">The border configuration action</param>
        public ChartMarkersBuilder Border(Action<ChartBorderBuilder> configurator)
        {
            configurator(new ChartBorderBuilder(lineMarkers.Border));
            return this;
        }

        /// <summary>
        /// The background color of the current series markers.
        /// </summary>
        /// <param name="backgorund">The background color of the current series markers. The background color is series color.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Markers(markers => markers
        ///                    .Background("Red");
        ///                );
        ///             )
        ///             .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public ChartMarkersBuilder Background(string backgorund)
        {
            lineMarkers.Background = backgorund;

            return this;
        }

        /// <summary>
        /// Sets the function used to retrieve marker background.
        /// </summary>
        /// <param name="colorFunction">
        ///     The JavaScript function that will be executed
        ///     to retrieve the background of each marker.
        /// </param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Markers(m =>
        ///                     m.BackgroundHandler(
        ///                         @&lt;text&gt;
        ///                         function(point) {
        ///                             return point.value > 5 ? "red" : "green";
        ///                         }
        ///                         &lt;/text&gt;
        ///                    )
        ///                )
        ///             )
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public ChartMarkersBuilder BackgroundHandler(Func<object, object> backgroundFunction)
        {
            lineMarkers.BackgroundHandler = new ClientHandlerDescriptor { TemplateDelegate = backgroundFunction };

            return this;
        }

        /// <summary>
        /// Sets the function used to retrieve marker background.
        /// </summary>
        /// <param name="colorFunction">
        ///     The JavaScript function that will be executed
        ///     to retrieve the background of each marker.
        /// </param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Markers(m => m.BackgroundHandler("backgroundFn"))
        ///             )
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public ChartMarkersBuilder BackgroundHandler(string backgroundFunction)
        {
            lineMarkers.BackgroundHandler = new ClientHandlerDescriptor { HandlerName = backgroundFunction };

            return this;
        }

        /// <summary>
        /// Sets the markers rotation angle.
        /// </summary>
        /// <param name="size">The markers rotation angle.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Markers(markers => markers
        ///                   .Type(ChartMarkerShape.Triangle)
        ///                   .Rotation(10)
        ///               );
        ///            )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartMarkersBuilder Rotation(int rotation)
        {
            lineMarkers.Rotation = rotation;
            return this;
        }
    }
}