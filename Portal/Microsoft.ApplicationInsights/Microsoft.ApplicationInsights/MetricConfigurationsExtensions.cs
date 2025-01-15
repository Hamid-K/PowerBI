using System;
using System.ComponentModel;
using Microsoft.ApplicationInsights.Metrics;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x02000020 RID: 32
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class MetricConfigurationsExtensions
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00006DEB File Offset: 0x00004FEB
		public static MetricConfigurationForMeasurement Measurement(this MetricConfigurations metricConfigPresets)
		{
			return MetricConfigurationsExtensions.defaultConfigForMeasurement;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006DF2 File Offset: 0x00004FF2
		public static void SetDefaultForMeasurement(this MetricConfigurations metricConfigPresets, MetricConfigurationForMeasurement defaultConfigurationForMeasurement)
		{
			Util.ValidateNotNull(defaultConfigurationForMeasurement, "defaultConfigurationForMeasurement");
			Util.ValidateNotNull(defaultConfigurationForMeasurement.SeriesConfig, "defaultConfigurationForMeasurement.SeriesConfig");
			MetricConfigurationsExtensions.defaultConfigForMeasurement = defaultConfigurationForMeasurement;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006E15 File Offset: 0x00005015
		internal static MetricConfiguration Default(this MetricConfigurations metricConfigPresets)
		{
			return metricConfigPresets.Measurement();
		}

		// Token: 0x04000090 RID: 144
		private const int DefaultSeriesCountLimit = 1000;

		// Token: 0x04000091 RID: 145
		private const int DefaultValuesPerDimensionLimit = 100;

		// Token: 0x04000092 RID: 146
		private static MetricConfigurationForMeasurement defaultConfigForMeasurement = new MetricConfigurationForMeasurement(1000, 100, new MetricSeriesConfigurationForMeasurement(false));
	}
}
