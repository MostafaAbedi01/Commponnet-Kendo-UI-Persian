﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kendo.Mvc.UI.Fluent
{
    public abstract class ChartLineSeriesBuilderBase<TSeries, TSeriesBuilder> : ChartSeriesBuilderBase<TSeries, TSeriesBuilder>
        where TSeriesBuilder : ChartLineSeriesBuilderBase<TSeries, TSeriesBuilder>
        where TSeries : ILineSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartLineSeriesBuilderBase{TSeries, TSeriesBuilder}"/> class.
        /// </summary>
        /// <param name="series">The series.</param>
        public ChartLineSeriesBuilderBase(TSeries series)
            : base(series)
        {
        }

        /// <summary>
        /// Sets a value indicating if the lines should be stacked.
        /// </summary>
        /// <param name="stacked">A value indicating if the lines should be stacked.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart(Model)
        ///            .Name("Chart")
        ///            .Series(series => series.Line(s => s.Sales).Stack(true))
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Stack(bool stacked)
        {
            Series.Stacked = stacked;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets a value indicating the type of stacking to use.
        /// </summary>
        /// <param name="type">A value indicating the stacking type.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Kendo().Chart(Model)
        ///             .Name("Chart")
        ///             .Series(series => series.Line(s => s.Sales).Stack(ChartStackType.Stack100))
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Stack(ChartStackType type)
        {
            Series.StackType = type;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the aggregate function for date series.
        /// This function is used when a category (an year, month, etc.) contains two or more points.
        /// </summary>
        /// <param name="aggregate">Aggregate function name.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Kendo().Chart(Model)
        ///             .Name("Chart")
        ///             .Series(series => series.Line(s => s.Sales).Aggregate())
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Aggregate(ChartSeriesAggregate aggregate)
        {
            Series.Aggregate = aggregate;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Configures the line chart labels.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Labels(labels => labels
        ///                    .Position(ChartBarLabelsPosition.Above)
        ///                    .Visible(true)
        ///                );
        ///            )
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Labels(Action<ChartPointLabelsBuilder> configurator)
        {

            configurator(new ChartPointLabelsBuilder(Series.Labels));

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the visibility of line chart labels.
        /// </summary>
        /// <param name="visible">The visibility. The default value is false.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Labels(true);
        ///            )
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Labels(bool visible)
        {
            Series.Labels.Visible = visible;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the line chart line width.
        /// </summary>
        /// <param name="width">The line width.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series.Line(s => s.Sales).Width(2))
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TSeriesBuilder Width(double width)
        {
            Series.Width = width;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the line chart line dash type.
        /// </summary>
        /// <param name="dashType">The line dash type.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series.Line(s => s.Sales).DashType(ChartDashType.Dot))
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TSeriesBuilder DashType(ChartDashType dashType)
        {
            Series.DashType = dashType;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Configures the line chart markers.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Markers(markers => markers
        ///                    .Type(ChartMarkerShape.Triangle)
        ///                );
        ///            )
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Markers(Action<ChartMarkersBuilder> configurator)
        {

            configurator(new ChartMarkersBuilder(Series.Markers));

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the visibility of line chart markers.
        /// </summary>
        /// <param name="visible">The visibility. The default value is true.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Markers(true);
        ///            )
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Markers(bool visible)
        {
            Series.Markers.Visible = visible;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Configures the behavior for handling missing values in line series.
        /// </summary>
        /// <param name="missingValues">The missing values behavior. The default is to leave gaps.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .MissingValues(ChartLineMissingValues.Interpolate);
        ///            )
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder MissingValues(ChartLineMissingValues missingValues)
        {
            Series.MissingValues = missingValues;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the value field for the series
        /// </summary>
        /// <param name="field">The value field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.Line(Model.Records).Field("Value"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TSeriesBuilder Field(string field)
        {
            Series.Member = field;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the category field for the series
        /// </summary>
        /// <param name="categoryField">The category field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.Line(Model.Records).Field("Value").CategoryField("Category"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TSeriesBuilder CategoryField(string categoryField)
        {
            Series.CategoryMember = categoryField;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the color field for the series
        /// </summary>
        /// <param name="colorField">The color field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.Line(Model.Records).Field("Value").ColorField("Color"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TSeriesBuilder ColorField(string colorField)
        {
            Series.ColorMember = colorField;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Sets the note text field for the series
        /// </summary>
        /// <param name="noteTextField">The note text field for the series</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.Line(Model.Records).Field("Value").NoteTextField("NoteText"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TSeriesBuilder NoteTextField(string noteTextField)
        {
            Series.NoteTextMember = noteTextField;

            return (TSeriesBuilder)this;
        }

        /// <summary>
        /// Configures the series highlight
        /// </summary>
        /// <param name="configurator">The configuration action.</param>        
        public TSeriesBuilder Highlight(Action<ChartLineSeriesHighlightBuilder> configurator)
        {
            configurator(new ChartLineSeriesHighlightBuilder(Series.Highlight));
            return this as TSeriesBuilder;
        }
    }
}
