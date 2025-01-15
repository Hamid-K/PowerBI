using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200013E RID: 318
	internal static class ChartMapperFactory
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x0003B07C File Offset: 0x0003927C
		internal static IChartMapper CreateChartMapperInstance(Chart chart, string defaultFontFamily)
		{
			return new ChartMapper(chart, defaultFontFamily);
		}
	}
}
