using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000140 RID: 320
	internal static class GaugeMapperFactory
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003B08E File Offset: 0x0003928E
		internal static IGaugeMapper CreateGaugeMapperInstance(GaugePanel gaugePanel, string defaultFontFamily)
		{
			return new GaugeMapper(gaugePanel, defaultFontFamily);
		}
	}
}
