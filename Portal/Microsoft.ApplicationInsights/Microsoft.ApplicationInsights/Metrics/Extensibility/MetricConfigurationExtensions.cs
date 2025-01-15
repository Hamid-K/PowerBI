using System;
using System.ComponentModel;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000046 RID: 70
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class MetricConfigurationExtensions
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000C73C File Offset: 0x0000A93C
		public static MetricSeriesConfigurationForMeasurement.AggregateKindConstants Constants(this MetricSeriesConfigurationForMeasurement measurementConfig)
		{
			return MetricSeriesConfigurationForMeasurement.AggregateKindConstants.Instance;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C743 File Offset: 0x0000A943
		public static MetricSeriesConfigurationForMeasurement.AggregateKindConstants Constants(this MetricConfigurationForMeasurement measurementConfig)
		{
			return MetricSeriesConfigurationForMeasurement.AggregateKindConstants.Instance;
		}
	}
}
