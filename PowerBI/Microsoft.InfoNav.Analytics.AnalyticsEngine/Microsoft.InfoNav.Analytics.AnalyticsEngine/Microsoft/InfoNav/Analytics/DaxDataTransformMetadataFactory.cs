using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000011 RID: 17
	[ImmutableObject(true)]
	public sealed class DaxDataTransformMetadataFactory : IDaxDataTransformMetadataFactory
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002693 File Offset: 0x00000893
		public DaxDataTransformMetadataFactory(DaxExtensionMetadataFactory daxExtensionFacotry, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			this._registeredExtensions = DaxDataTransformMetadataFactory.RegisterExtensions(daxExtensionFacotry, analyticsFeatureSwitchProvider);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026A8 File Offset: 0x000008A8
		public IDaxDataTransformMetadata Create(string name)
		{
			IDaxDataTransformMetadata daxDataTransformMetadata;
			if (this._registeredExtensions.TryGetValue(name, out daxDataTransformMetadata))
			{
				return daxDataTransformMetadata;
			}
			return null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026C8 File Offset: 0x000008C8
		public bool HasTransform(string name)
		{
			return !string.IsNullOrWhiteSpace(name) && this._registeredExtensions.ContainsKey(name);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026E0 File Offset: 0x000008E0
		private static Dictionary<string, IDaxDataTransformMetadata> RegisterExtensions(DaxExtensionMetadataFactory daxExtensionFactory, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			Dictionary<string, IDaxDataTransformMetadata> dictionary = new Dictionary<string, IDaxDataTransformMetadata>(16, TransformNameComparer.Instance);
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.SpatialClusteringInQuery))
			{
				dictionary.Add("SpatialClustering", daxExtensionFactory.CreateSpatialClusteringExtensionMetadata());
			}
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.KMeansClustering))
			{
				dictionary.Add("KMeansClustering", daxExtensionFactory.CreateKMeansClusteringExtensionMetadata());
			}
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.SamplingTransform))
			{
				dictionary.Add("SampleTimeSeriesData", daxExtensionFactory.CreateSampleTimeSeriesDataExtensionMetadata());
			}
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.AnomalyDetectionTransform))
			{
				dictionary.Add("DetectAnomaly", daxExtensionFactory.CreateDetectAnomalyExtensionMetadata());
				dictionary.Add("SampleAndDetectAnomaly", daxExtensionFactory.CreateSampleAndDetectAnomalyExtensionMetadata());
			}
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.NLGTransforms))
			{
				dictionary.Add("RegionWithMostPointsSummary", daxExtensionFactory.CreateRegionWithMostPointsSummaryExtensionMetadata());
				dictionary.Add("ChangeSummary", daxExtensionFactory.CreateChangeSummaryExtensionMetadata());
				dictionary.Add("AggregateSummary", daxExtensionFactory.CreateAggregateSummaryExtensionMetadata());
				dictionary.Add("TopRightHandCornerSummary", daxExtensionFactory.CreateTopRightHandCornerSummaryExtensionMetadata());
				dictionary.Add("LargestDifferenceSummary", daxExtensionFactory.CreateLargestDifferenceSummaryExtensionMetadata());
				dictionary.Add("LargestCategorySummary", daxExtensionFactory.CreateLargestCategorySummaryExtensionMetadata());
				dictionary.Add("Unpivot", daxExtensionFactory.CreateUnpivotExtensionMetadata());
				dictionary.Add("HighPointLowPointSummary", daxExtensionFactory.CreateHighPointLowPointSummaryExtensionMetadata());
				dictionary.Add("TrendSummary", daxExtensionFactory.CreateTrendSummaryExtensionMetadata());
				dictionary.Add("CorrelationSummary", daxExtensionFactory.CreateCorrelationSummaryExtensionMetadata());
				dictionary.Add("LargestDivergenceSummary", daxExtensionFactory.CreateLargestDivergenceSummaryExtensionMetadata());
				dictionary.Add("ExtractTrend", daxExtensionFactory.CreateExtractTrendExtensionMetadata());
				dictionary.Add("AnomalySummary", daxExtensionFactory.CreateAnomalySummaryExtensionMetadata());
				dictionary.Add("KeyDriversSummary", daxExtensionFactory.CreateKeyDriversSummaryExtensionMetadata());
				dictionary.Add("KPISummary", daxExtensionFactory.CreateKPISummaryExtensionMetadata());
				dictionary.Add("KPIInsights", daxExtensionFactory.CreateKPIInsightsExtensionMetadata());
				dictionary.Add("KPIInsightsSummary", daxExtensionFactory.CreateKPIInsightsSummaryExtensionMetadata());
			}
			return dictionary;
		}

		// Token: 0x04000055 RID: 85
		private readonly Dictionary<string, IDaxDataTransformMetadata> _registeredExtensions;
	}
}
