﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.Extensions;

namespace Kendo.Mvc.UI
{
    internal class ChartAreaSeriesSerializerBase : ChartSeriesSerializerBase
    {
        private readonly IAreaSeries series;

        public ChartAreaSeriesSerializerBase(IAreaSeries series)
            : base(series)
        {
            this.series = series;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("type", series.Orientation == ChartSeriesOrientation.Horizontal ? "area" : "verticalArea")
                .Add("field", series.Member, () => series.Member.HasValue())
                .Add("categoryField", series.CategoryMember, () => series.CategoryMember.HasValue())
                .Add("colorField", series.ColorMember, () => series.ColorMember.HasValue() )
                .Add("noteTextField", series.NoteTextMember, () => series.NoteTextMember.HasValue())
                .Add("data", series.Data, () => { return series.Data != null; })
                .Add("missingValues", series.MissingValues.ToString().ToLowerInvariant(),
                                      () => series.MissingValues.HasValue);

            if (series.StackType.HasValue) {
                var type = series.StackType == ChartStackType.Stack100 ? "100%" : series.StackType.ToString().ToLowerInvariant();
                result.Add("stack", new Dictionary<string, object> { { "type", type } });
            } else if (series.Stacked.HasValue) {
                result.Add("stack", series.Stacked);
            }

            var labelsData = series.Labels.CreateSerializer().Serialize();
            if (labelsData.Count > 0)
            {
                result.Add("labels", labelsData);
            }
                ;
            var markers = series.Markers.CreateSerializer().Serialize();
            if (markers.Count > 0)
            {
                result.Add("markers", markers);
            }

            if (series.AggregateHandler != null)
            {
                result.Add("aggregate", series.AggregateHandler);
            }
            else if (series.Aggregate.HasValue)
            {
                result.Add("aggregate", series.Aggregate.ToString().ToLowerInvariant());
            }

            return result;
        }
    }
}
