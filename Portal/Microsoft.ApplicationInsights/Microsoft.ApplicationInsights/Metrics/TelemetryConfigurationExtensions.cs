using System;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000034 RID: 52
	public static class TelemetryConfigurationExtensions
	{
		// Token: 0x060001EA RID: 490 RVA: 0x0000A95F File Offset: 0x00008B5F
		public static MetricManager GetMetricManager(this TelemetryConfiguration telemetryPipeline)
		{
			if (telemetryPipeline == null)
			{
				return null;
			}
			return telemetryPipeline.GetMetricManager(true);
		}
	}
}
