namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using Kendo.Mvc.UI;

    /// <summary>
    /// Defines the fluent interface for configuring bar series.
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public class ChartOHLCSeriesBuilder<T> : ChartSeriesBuilderBase<IChartOHLCSeries, ChartOHLCSeriesBuilder<T>>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartOHLCSeriesBuilder{T}"/> class.
        /// </summary>
        /// <param name="series">The series.</param>
        public ChartOHLCSeriesBuilder(IChartOHLCSeries series)
            : base(series)
        {
        }

        /// <summary>
        /// Sets the aggregate function for date series.
        /// This function is used when a category (an year, month, etc.) contains two or more points.
        /// </summary>
        /// <param name="open">Open aggregate name.</param>
        /// <param name="high">High aggregate name.</param>
        /// <param name="low">Low aggregate name.</param>
        /// <param name="close">Close aggregate name.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Kendo().Chart(Model)
        ///             .Name("Chart")
        ///             .Series(series => series.OHLC(s => s.Sales).Aggregate(ChartSeriesAggregate.Avg))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartOHLCSeriesBuilder<T> Aggregate(
            ChartSeriesAggregate? open = null,
            ChartSeriesAggregate? high = null,
            ChartSeriesAggregate? low = null,
            ChartSeriesAggregate? close = null
            )
        {
            Series.Aggregates.Open = open;
            Series.Aggregates.High = high;
            Series.Aggregates.Low = low;
            Series.Aggregates.Close = close;

            return this;
        }

        /// <summary>
        /// Set distance between category clusters. 
        /// <param name="gap">
        /// A value of 1 means that there is a total of 1 point width between categories. 
        /// The distance is distributed evenly on each side.
        /// The default value is 1
        /// </param>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart(Model)
        ///     .Name("Chart")
        ///     .Series(series => series.OHLC(s => s.Sales).Gap(1.5))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartOHLCSeriesBuilder<T> Gap(double gap)
        {
            Series.Gap = gap;

            return this;
        }

        /// <summary>
        /// Sets a value indicating the distance between points in the same category.
        /// </summary>
        /// <param name="spacing">
        /// Value of 1 means that the distance between points in the same category.
        /// The default value is 0.3
        /// </param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart(Model)
        ///     .Name("Chart")
        ///     .Series(series => series.Spacing(s => s.Sales).Spacing(1))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartOHLCSeriesBuilder<T> Spacing(double spacing)
        {
            Series.Spacing = spacing;

            return this;
        }

        /// <summary>
        /// Sets the points border
        /// </summary>
        /// <param name="width">The points border width.</param>
        /// <param name="color">The points border color (CSS syntax).</param>
        /// <param name="dashType">The points border dash type.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(s => s.Sales).Border("1", "#000", ChartDashType.Dot))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> Border(int width, string color, ChartDashType dashType)
        {
            Series.Border = new ChartElementBorder(width, color, dashType);

            return this;
        }

        /// <summary>
        /// Configures the ohlc chart lines.
        /// </summary>
        /// <param name="width">The lines width.</param>
        /// <param name="color">The lines color.</param>
        /// <param name="dashType">The lines dashType.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .OHLC(s => s.Sales)        
        ///               .Line(2, "red", ChartDashType.Dot)
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> Line(int width, string color, ChartDashType dashType)
        {
            Series.Line.Width = width;
            Series.Line.Color = color;
            Series.Line.DashType = dashType;

            return this;
        }

        /// <summary>
        /// Configures the ohlc line width.
        /// </summary>
        /// <param name="width">The lines width.</param>      
        public ChartOHLCSeriesBuilder<T> Line(int width)
        {
            return Line(width, null);
        }

        /// <summary>
        /// Configures the ohlc lines.
        /// </summary>
        /// <param name="width">The lines width.</param>
        /// <param name="color">The lines color.</param>    
        public ChartOHLCSeriesBuilder<T> Line(int width, string color)
        {
            Series.Line.Width = width;
            Series.Line.Color = color;

            return this;
        }

        /// <summary>
        /// Configures the ohlc chart lines.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Area(s => s.Sales)        
        ///               .Line(line => line.Opacity(0.2))
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> Line(Action<ChartLineBuilder> configurator)
        {
            configurator(new ChartLineBuilder(Series.Line));

            return this;
        }

        /// <summary>
        /// Configures the series highlight
        /// </summary>
        /// <param name="configurator">The configuration action.</param>        
        public ChartOHLCSeriesBuilder<T> Highlight(Action<ChartOHLCSeriesHighlightBuilder> configurator)
        {
            configurator(new ChartOHLCSeriesHighlightBuilder(Series.Highlight));
            return this;
        }

        /// <summary>
        /// Sets the open field for the series
        /// </summary>
        /// <param name="openField">The open field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(Model.Records).OpenField("Open").HighField("High").LowField("Low").CloseField("Close"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> OpenField(string openField)
        {
            Series.OpenMember = openField;

            return this;
        }

        /// <summary>
        /// Sets the close field for the series
        /// </summary>
        /// <param name="closeField">The close field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(Model.Records).OpenField("Open").HighField("High").LowField("Low").CloseField("Close"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> CloseField(string closeField)
        {
            Series.CloseMember = closeField;

            return this;
        }

        /// <summary>
        /// Sets the high field for the series
        /// </summary>
        /// <param name="highField">The high field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(Model.Records).OpenField("Open").HighField("High").LowField("Low").CloseField("Close"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> HighField(string highField)
        {
            Series.HighMember = highField;

            return this;
        }

        /// <summary>
        /// Sets the low field for the series
        /// </summary>
        /// <param name="lowField">The low field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(Model.Records).OpenField("Open").HighField("High").LowField("Low").CloseField("Close"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> LowField(string lowField)
        {
            Series.LowMember = lowField;

            return this;
        }

        /// <summary>
        /// Sets the value fields for the series
        /// </summary>
        /// <param name="openField">The open field for the series</param>
        /// <param name="highField">The value fields for the series</param>
        /// <param name="lowField">The low field for the series</param>
        /// <param name="closeField">The close field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(Model.Records).Fields("Open", "High", "Low", "Close"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> Fields(string openField, string highField, string lowField, string closeField)
        {
            return OpenField(openField).HighField(highField).LowField(lowField).CloseField(closeField);
        }

        /// <summary>
        /// Sets the color field for the series
        /// </summary>
        /// <param name="colorField">The color field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(Model.Records).OpenField("Open").HighField("High").LowField("Low").CloseField("Close").ColorField("Color"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> ColorField(string colorField)
        {
            Series.ColorMember = colorField;

            return this;
        }

        /// <summary>
        /// Sets the note text field for the series
        /// </summary>
        /// <param name="noteTextField">The note text field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.OHLC(Model.Records).OpenField("Open").HighField("High").LowField("Low").CloseField("Close").NoteTextField("NoteText"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartOHLCSeriesBuilder<T> NoteTextField(string noteTextField)
        {
            Series.NoteTextMember = noteTextField;

            return this;
        }
    }
}