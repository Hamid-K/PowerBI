using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000038 RID: 56
	internal sealed class MapRdlReportItemConverter : ScatterChartRdlReportItemConverter
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000793C File Offset: 0x00005B3C
		public override LayoutContext CreateLayoutContext(Chart chart)
		{
			ChartLayoutType chartLayoutType = ChartRdlReportItemConverter.ConvertChartType(chart.Type, chart.Subtype);
			return new LayoutContext
			{
				ChartLayoutType = chartLayoutType.ToString(),
				LegendPosition = chart.LegendPosition.ToString(),
				IsLegendHidden = new bool?(chart.LegendHidden),
				LabelsPosition = chart.LabelsPositions.ToString(),
				IsChartTitleHidden = new bool?(chart.ChartTitleHidden),
				ResourceId = chart.MapBackdrop.ToString(),
				AreLabelsVisible = new bool?(chart.IsLabelsVisible)
			};
		}
	}
}
