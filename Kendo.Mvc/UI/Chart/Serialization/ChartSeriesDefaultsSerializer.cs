namespace Kendo.Mvc.UI
{
    using System.Collections.Generic;
    using Kendo.Mvc.Infrastructure;

    internal class ChartSeriesDefaultsSerializer : IChartSerializer
    {
        private readonly IChartSeriesDefaults seriesDefaults;

        public ChartSeriesDefaultsSerializer(IChartSeriesDefaults seriesDefaults)
        {
            this.seriesDefaults = seriesDefaults;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var barData = seriesDefaults.Bar.CreateSerializer().Serialize();
            barData.Remove("type");

            var columnData = seriesDefaults.Column.CreateSerializer().Serialize();
            columnData.Remove("type");

            var rangeBarData = seriesDefaults.RangeBar.CreateSerializer().Serialize();
            rangeBarData.Remove("type");

            var rangeColumnData = seriesDefaults.RangeColumn.CreateSerializer().Serialize();
            rangeColumnData.Remove("type");

            var lineData = seriesDefaults.Line.CreateSerializer().Serialize();
            lineData.Remove("type");

            var verticalLineData = seriesDefaults.VerticalLine.CreateSerializer().Serialize();
            verticalLineData.Remove("type");

            var areaData = seriesDefaults.Area.CreateSerializer().Serialize();
            areaData.Remove("type");

            var verticalAreaData = seriesDefaults.VerticalArea.CreateSerializer().Serialize();
            verticalAreaData.Remove("type");

            var pieData = seriesDefaults.Pie.CreateSerializer().Serialize();
            pieData.Remove("type");

            var donutData = seriesDefaults.Donut.CreateSerializer().Serialize();
            donutData.Remove("type");

            var funnelData = seriesDefaults.Funnel.CreateSerializer().Serialize();
            funnelData.Remove("type");

            var scatterData = seriesDefaults.Scatter.CreateSerializer().Serialize();
            scatterData.Remove("type");

            var scatterLineData = seriesDefaults.ScatterLine.CreateSerializer().Serialize();
            scatterLineData.Remove("type");

            var ohlcData = seriesDefaults.OHLC.CreateSerializer().Serialize();
            ohlcData.Remove("type");

            var bulletData = seriesDefaults.Bullet.CreateSerializer().Serialize();
            bulletData.Remove("type");

            var verticalBulletData = seriesDefaults.VerticalBullet.CreateSerializer().Serialize();
            verticalBulletData.Remove("type");

            var radarAreaData = seriesDefaults.RadarArea.CreateSerializer().Serialize();
            radarAreaData.Remove("type");

            var radarColumnData = seriesDefaults.RadarColumn.CreateSerializer().Serialize();
            radarColumnData.Remove("type");

            var radarLineData = seriesDefaults.RadarLine.CreateSerializer().Serialize();
            radarLineData.Remove("type");

            var polarAreaData = seriesDefaults.PolarArea.CreateSerializer().Serialize();
            polarAreaData.Remove("type");

            var polarLineData = seriesDefaults.PolarLine.CreateSerializer().Serialize();
            polarLineData.Remove("type");

            var polarScatterData = seriesDefaults.PolarScatter.CreateSerializer().Serialize();
            polarScatterData.Remove("type");

            var result = new Dictionary<string, object>();
            FluentDictionary.For(result)
                  .Add("bar", barData, () => barData.Count > 0)
                  .Add("column", columnData, () => columnData.Count > 0)
                  .Add("rangeBar", rangeBarData, () => rangeBarData.Count > 0)
                  .Add("rangeColumn", rangeColumnData, () => rangeColumnData.Count > 0)
                  .Add("line", lineData, () => lineData.Count > 0)
                  .Add("verticalLine", verticalLineData, () => verticalLineData.Count > 0)
                  .Add("area", areaData, () => areaData.Count > 0)
                  .Add("verticalArea", verticalAreaData, () => verticalAreaData.Count > 0)
                  .Add("pie", pieData, () => pieData.Count > 0)
                  .Add("donut", donutData, () => donutData.Count > 0)
                  .Add("funnel", funnelData, () => funnelData.Count > 0)
                  .Add("scatter", scatterData, () => scatterData.Count > 0)
                  .Add("scatterLine", scatterLineData, () => scatterLineData.Count > 0)
                  .Add("ohlc", ohlcData, () => ohlcData.Count > 0)
                  .Add("bullet", bulletData, () => bulletData.Count > 0)
                  .Add("verticalBullet", verticalBulletData, () => verticalBulletData.Count > 0)
                  .Add("radarArea", radarAreaData, () => radarAreaData.Count > 0)
                  .Add("radarColumn", radarColumnData, () => radarColumnData.Count > 0)
                  .Add("radarLine", radarLineData, () => radarLineData.Count > 0)
                  .Add("polarArea", polarAreaData, () => polarAreaData.Count > 0)
                  .Add("polarLine", polarLineData, () => polarLineData.Count > 0)
                  .Add("polarScatter", polarScatterData, () => polarScatterData.Count > 0);

            return result;
        }
    }
}