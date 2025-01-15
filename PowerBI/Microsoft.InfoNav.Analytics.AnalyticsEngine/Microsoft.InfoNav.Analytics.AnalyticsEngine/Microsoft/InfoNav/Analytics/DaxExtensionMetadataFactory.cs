using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000013 RID: 19
	[ImmutableObject(true)]
	public sealed class DaxExtensionMetadataFactory
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000028B8 File Offset: 0x00000AB8
		public IDaxDataTransformMetadata CreateSpatialClusteringExtensionMetadata()
		{
			return new DaxDataTransformMetadata("SpatialClustering", new List<IDaxDataTransformParameterMetadata>());
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028C9 File Offset: 0x00000AC9
		public IDaxDataTransformMetadata CreateKMeansClusteringExtensionMetadata()
		{
			return new DaxDataTransformMetadata("KMeansClustering", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("NumberOfClusters")
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028EC File Offset: 0x00000AEC
		public IDaxDataTransformMetadata CreateSampleTimeSeriesDataExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.SampleTimeSeriesData", new List<IDaxDataTransformParameterMetadata>(3)
			{
				new DaxDataTransformParameterMetadata("SamplingMode"),
				new DaxDataTransformParameterMetadata("TotalSampleSize"),
				new DaxDataTransformParameterMetadata("SampleSizePerSeries")
			});
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002939 File Offset: 0x00000B39
		public IDaxDataTransformMetadata CreateDetectAnomalyExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.DetectAnomaly", new List<IDaxDataTransformParameterMetadata>(2)
			{
				new DaxDataTransformParameterMetadata("Sensitivity"),
				new DaxDataTransformParameterMetadata("SeasonalityPeriod")
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000296C File Offset: 0x00000B6C
		public IDaxDataTransformMetadata CreateSampleAndDetectAnomalyExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.SampleAndDetectAnomaly", new List<IDaxDataTransformParameterMetadata>(5)
			{
				new DaxDataTransformParameterMetadata("Sensitivity"),
				new DaxDataTransformParameterMetadata("SeasonalityPeriod"),
				new DaxDataTransformParameterMetadata("SamplingMode"),
				new DaxDataTransformParameterMetadata("TotalSampleSize"),
				new DaxDataTransformParameterMetadata("SampleSizePerSeries")
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029D9 File Offset: 0x00000BD9
		public IDaxDataTransformMetadata CreateRegionWithMostPointsSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.RegionWithMostPointsSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId")
			});
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029FA File Offset: 0x00000BFA
		public IDaxDataTransformMetadata CreateChangeSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.ChangeSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("changeType")
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A2C File Offset: 0x00000C2C
		public IDaxDataTransformMetadata CreateAggregateSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.AggregateSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("aggregateType"),
				new DaxDataTransformParameterMetadata("timeGranularity")
			});
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A78 File Offset: 0x00000C78
		public IDaxDataTransformMetadata CreateTopRightHandCornerSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.TopRightHandCornerSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId")
			});
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A99 File Offset: 0x00000C99
		public IDaxDataTransformMetadata CreateLargestDifferenceSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.LargestDifferenceSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId")
			});
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002ABA File Offset: 0x00000CBA
		public IDaxDataTransformMetadata CreateLargestCategorySummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.LargestCategorySummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId")
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002ADB File Offset: 0x00000CDB
		public IDaxDataTransformMetadata CreateUnpivotExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.Unpivot", new List<IDaxDataTransformParameterMetadata>());
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AEC File Offset: 0x00000CEC
		public IDaxDataTransformMetadata CreateHighPointLowPointSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.HighPointLowPointSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId")
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B10 File Offset: 0x00000D10
		public IDaxDataTransformMetadata CreateTrendSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.TrendSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("trendType"),
				new DaxDataTransformParameterMetadata("timeGranularity")
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B5C File Offset: 0x00000D5C
		public IDaxDataTransformMetadata CreateCorrelationSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.CorrelationSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("significanceThreshold")
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B8D File Offset: 0x00000D8D
		public IDaxDataTransformMetadata CreateLargestDivergenceSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.LargestDivergenceSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId")
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BAE File Offset: 0x00000DAE
		public IDaxDataTransformMetadata CreateExtractTrendExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.ExtractTrend", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("trendType")
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public IDaxDataTransformMetadata CreateAnomalySummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.AnomalySummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("anomalySummaryType"),
				new DaxDataTransformParameterMetadata("timeGranularity")
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C1C File Offset: 0x00000E1C
		public IDaxDataTransformMetadata CreateKeyDriversSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.KeyDriversSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("targetValue"),
				new DaxDataTransformParameterMetadata("keyDriversSummaryType")
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C68 File Offset: 0x00000E68
		public IDaxDataTransformMetadata CreateKPISummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.KPISummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("minValue"),
				new DaxDataTransformParameterMetadata("targetValue"),
				new DaxDataTransformParameterMetadata("maxValue")
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public IDaxDataTransformMetadata CreateKPIInsightsExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.KPIInsights", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("dimensionDisplayName"),
				new DaxDataTransformParameterMetadata("targetValue")
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public IDaxDataTransformMetadata CreateKPIInsightsSummaryExtensionMetadata()
		{
			return new DaxDataTransformMetadata("AI.KPIInsightsSummary", new List<IDaxDataTransformParameterMetadata>
			{
				new DaxDataTransformParameterMetadata("initialTemplateId"),
				new DaxDataTransformParameterMetadata("summaryType"),
				new DaxDataTransformParameterMetadata("targetValue")
			});
		}
	}
}
