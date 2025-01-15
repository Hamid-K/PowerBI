using System;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Metrics
{
	// Token: 0x0200008E RID: 142
	internal interface ISpecificAutocollectedMetricsExtractor
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000472 RID: 1138
		string ExtractorName { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000473 RID: 1139
		string ExtractorVersion { get; }

		// Token: 0x06000474 RID: 1140
		void InitializeExtractor(TelemetryClient metricTelemetryClient);

		// Token: 0x06000475 RID: 1141
		void ExtractMetrics(ITelemetry fromItem, out bool isItemProcessed);
	}
}
