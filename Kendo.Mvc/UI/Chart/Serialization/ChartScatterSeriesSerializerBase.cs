﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.Extensions;

namespace Kendo.Mvc.UI
{
    internal abstract class ChartScatterSeriesSerializerBase : ChartSeriesSerializerBase
    {
        private readonly IScatterSeries series;

        public ChartScatterSeriesSerializerBase(IScatterSeries series)
            : base(series)
        {
            this.series = series;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("type", "scatter")
                .Add("xField", series.XMember, () => series.XMember != null)
                .Add("yField", series.YMember, () => series.YMember != null)
                .Add("noteTextField", series.NoteTextMember, () => series.NoteTextMember != null)
                .Add("data", series.Data, () => { return series.Data != null; })
                .Add("xAxis", series.XAxis, () => !string.IsNullOrEmpty(series.XAxis))
                .Add("yAxis", series.YAxis, () => !string.IsNullOrEmpty(series.YAxis));

            var labelsData = series.Labels.CreateSerializer().Serialize();
            if (labelsData.Count > 0)
            {
                result.Add("labels", labelsData);
            }

            var markers = series.Markers.CreateSerializer().Serialize();
            if (markers.Count > 0)
            {
                result.Add("markers", markers);
            }

            return result;
        }
    }
}
