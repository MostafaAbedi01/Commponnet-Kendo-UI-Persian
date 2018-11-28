using System.Collections.Generic;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.Extensions;

namespace Kendo.Mvc.UI
{
    internal class ChartFunnelSeriesSerializer : ChartSeriesSerializerBase
    {
        private readonly IChartFunnelSeries series;

        public ChartFunnelSeriesSerializer(IChartFunnelSeries series)
            : base(series)
        {
            this.series = series;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("type", series.Type)
                .Add("dynamicHeight", series.DynamicHeight, () => { return series.DynamicHeight != true; })
                .Add("dynamicSlope", series.DynamicSlope, () => { return series.DynamicSlope != false; })
                .Add("neckRatio", series.NeckRatio, () => { return series.NeckRatio != 0.3; })
                .Add("segmentSpacing", series.SegmentSpacing, () => { return series.SegmentSpacing != 0; })
                .Add("field", series.Member, () => series.Member != null)
                .Add("categoryField", series.CategoryMember, () => series.CategoryMember != null)
                .Add("colorField", series.ColorMember, () => series.ColorMember != null)
                .Add("visibleInLegendField", series.VisibleInLegendMember, () => series.VisibleInLegendMember != null)
                .Add("data", series.Data, () => { return series.Data != null; })
                .Add("border", series.Border.CreateSerializer().Serialize(), ShouldSerializeBorder);


            var labelsData = series.Labels.CreateSerializer().Serialize();
            if (labelsData.Count > 0)
            {
                result.Add("labels", labelsData);
            }

            return result;
        }

        private bool ShouldSerializeBorder()
        {
            return series.Border.Color.HasValue() ||
                   series.Border.Width.HasValue ||
                   series.Border.DashType.HasValue;
        }
    }
}