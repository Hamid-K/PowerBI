using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x0200002E RID: 46
	public sealed class MetricConfigurationForMeasurement : MetricConfiguration
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00009353 File Offset: 0x00007553
		public MetricConfigurationForMeasurement(int seriesCountLimit, int valuesPerDimensionLimit, MetricSeriesConfigurationForMeasurement seriesConfig)
			: base(seriesCountLimit, valuesPerDimensionLimit, seriesConfig)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000935E File Offset: 0x0000755E
		public MetricConfigurationForMeasurement(int seriesCountLimit, IEnumerable<int> valuesPerDimensionLimits, MetricSeriesConfigurationForMeasurement seriesConfig)
			: base(seriesCountLimit, valuesPerDimensionLimits, seriesConfig)
		{
		}
	}
}
