using System;
using Microsoft.BusinessIntelligence;
using Microsoft.InfoNav.Analytics;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000027 RID: 39
	public sealed class AnalyticsFeatureSwitchProvider : IAnalyticsFeatureSwitchProvider
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00003992 File Offset: 0x00001B92
		private AnalyticsFeatureSwitchProvider(FeatureSwitches featureSwitches)
		{
			this._featureSwitches = featureSwitches;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000039A1 File Offset: 0x00001BA1
		public static AnalyticsFeatureSwitchProvider Create(FeatureSwitches featureSwitches)
		{
			return new AnalyticsFeatureSwitchProvider(featureSwitches);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000039AC File Offset: 0x00001BAC
		public bool IsEnabled(AnalyticsFeatureSwitchKind kind)
		{
			switch (kind)
			{
			case AnalyticsFeatureSwitchKind.SamplingTransform:
				return this._featureSwitches.AnalyticsSamplingTransformEnabled;
			case AnalyticsFeatureSwitchKind.NLGTransforms:
				return this._featureSwitches.AnalyticsNLGTransformsEnabled;
			case AnalyticsFeatureSwitchKind.AnomalyDetectionTransform:
				return this._featureSwitches.AnalyticsAnomalyDetectionTransformEnabled;
			case AnalyticsFeatureSwitchKind.Forecast:
				return true;
			case AnalyticsFeatureSwitchKind.NoOpForecast:
				return false;
			case AnalyticsFeatureSwitchKind.OutlierDetection:
				return true;
			case AnalyticsFeatureSwitchKind.SpatialClusteringInQuery:
				return true;
			case AnalyticsFeatureSwitchKind.SpatialClusteringInProcessing:
				return true;
			case AnalyticsFeatureSwitchKind.KMeansClustering:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x040000C2 RID: 194
		private readonly FeatureSwitches _featureSwitches;
	}
}
