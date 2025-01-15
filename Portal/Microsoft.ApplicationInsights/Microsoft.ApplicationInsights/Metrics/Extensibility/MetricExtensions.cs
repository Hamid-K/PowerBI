using System;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000047 RID: 71
	public static class MetricExtensions
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000C74A File Offset: 0x0000A94A
		public static MetricConfiguration GetConfiguration(this Metric metric)
		{
			return metric.configuration;
		}
	}
}
