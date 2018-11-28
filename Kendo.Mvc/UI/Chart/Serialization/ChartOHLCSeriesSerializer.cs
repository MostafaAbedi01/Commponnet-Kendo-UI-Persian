namespace Kendo.Mvc.UI
{
    using System.Collections.Generic;
    using Kendo.Mvc.Infrastructure;
    using Kendo.Mvc.Extensions;

    internal class ChartOHLCSeriesSerializer : ChartSeriesSerializerBase
    {
        private readonly IChartOHLCSeries series;

        public ChartOHLCSeriesSerializer(IChartOHLCSeries series)
            : base(series)
        {
            this.series = series;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("type", series.Type)
                .Add("gap", series.Gap, () => series.Gap.HasValue)
                .Add("spacing", series.Spacing, () => series.Spacing.HasValue)
                .Add("axis", series.Axis, () => series.Axis.HasValue())
                .Add("data", series.Data, () => { return series.Data != null; })
                .Add("border", series.Border.CreateSerializer().Serialize(), ShouldSerializeBorder)
                .Add("colorField", series.ColorMember, () => series.ColorMember.HasValue())
                .Add("openField", series.OpenMember, () => series.OpenMember.HasValue())
                .Add("highField", series.HighMember, () => series.HighMember.HasValue())
                .Add("lowField", series.LowMember, () => series.LowMember.HasValue())
                .Add("closeField", series.CloseMember, () => series.CloseMember.HasValue())
                .Add("categoryField", series.CategoryMember, () => series.CategoryMember.HasValue())
                .Add("noteTextField", series.NoteTextMember, () => series.NoteTextMember.HasValue());

            var line = series.Line.CreateSerializer().Serialize();
            if (line.Count > 0)
            {
                result.Add("line", line);
            }

            if (series.AggregateHandler != null)
            {
                result.Add("aggregate", series.AggregateHandler);
            } else {
                var aggregates = series.Aggregates.CreateSerializer().Serialize();
                if (aggregates.Count > 0) {
                    result.Add("aggregate", aggregates);
                }
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