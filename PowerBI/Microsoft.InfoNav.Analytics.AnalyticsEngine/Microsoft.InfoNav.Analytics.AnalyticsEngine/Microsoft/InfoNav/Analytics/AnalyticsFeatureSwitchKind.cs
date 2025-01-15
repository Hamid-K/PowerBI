using System;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200001A RID: 26
	public enum AnalyticsFeatureSwitchKind
	{
		// Token: 0x0400007B RID: 123
		SamplingTransform,
		// Token: 0x0400007C RID: 124
		NLGTransforms,
		// Token: 0x0400007D RID: 125
		AnomalyDetectionTransform,
		// Token: 0x0400007E RID: 126
		Forecast,
		// Token: 0x0400007F RID: 127
		NoOpForecast,
		// Token: 0x04000080 RID: 128
		OutlierDetection,
		// Token: 0x04000081 RID: 129
		SpatialClusteringInQuery,
		// Token: 0x04000082 RID: 130
		SpatialClusteringInProcessing,
		// Token: 0x04000083 RID: 131
		KMeansClustering
	}
}
