using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200014D RID: 333
	[DataContract(Name = "ConceptualCapabilities", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualCapabilities
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00011BC6 File Offset: 0x0000FDC6
		internal ClientConceptualCapabilities()
		{
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		internal ClientConceptualCapabilities(bool discourageQueryAggregateUsage, bool supportsMedian, bool supportsPercentile, bool normalizedFiveStateKpiRange, bool supportsScopedEval, bool supportsStringMinMax, bool supportsMultiTableTupleFilters, bool isExtendable, bool supportsTimeIntelligenceQuickMeasures, bool supportsBinnedLineSample, bool supportsOverlappingPointsSample, bool limitMultiColumnFiltersToQueryGroupColumns, bool supportsCalculatedColumns, bool supportsGrouping, bool supportsInsights, bool supportsQna, bool supportsInstanceFilters, bool supportsDataSourceVariables, bool supportsSubqueryRegrouping, bool supportsTopNPerLevel, bool supportsFastRefresh, bool supportsScopedAggregates, bool supportsScopedDataReduction, bool supportsExtensionColumns, bool canEditChangeDetectionMeasure, bool supportChangeDetectionMeasureRefresh, bool isQnaEnabled, bool supportsGroupSynchronization, bool supportsSparklineData, bool supportsVisualCalculations, InsightsCapabilities insightsCapabilities = null, ClientTransformCapabilities clientTransformCapabilities = null)
		{
			this._discourageQueryAggregateUsage = discourageQueryAggregateUsage;
			this._supportsMedian = supportsMedian;
			this._supportsPercentile = supportsPercentile;
			this._normalizedFiveStateKpiRange = normalizedFiveStateKpiRange;
			this._supportsScopedEval = supportsScopedEval;
			this._supportsStringMinMax = supportsStringMinMax;
			this._supportsMultiTableTupleFilters = supportsMultiTableTupleFilters;
			this._isExtendable = isExtendable;
			this._supportsTimeIntelligenceQuickMeasures = supportsTimeIntelligenceQuickMeasures;
			this._supportsBinnedLineSample = supportsBinnedLineSample;
			this._supportsOverlappingPointsSample = supportsOverlappingPointsSample;
			this._limitMultiColumnFiltersToQueryGroupColumns = limitMultiColumnFiltersToQueryGroupColumns;
			this._supportsCalculatedColumns = ClientConceptualSchemaFactory.ConvertTrueToNull(supportsCalculatedColumns);
			this._supportsGrouping = ClientConceptualSchemaFactory.ConvertTrueToNull(supportsGrouping);
			this._supportsInsights = supportsInsights;
			this._supportsQna = supportsQna;
			this._supportsInstanceFilters = supportsInstanceFilters;
			this._supportsDataSourceVariables = supportsDataSourceVariables;
			this._supportsSubqueryRegrouping = supportsSubqueryRegrouping;
			this._supportsTopNPerLevel = supportsTopNPerLevel;
			this._supportsFastRefresh = supportsFastRefresh;
			this._supportsScopedAggregates = supportsScopedAggregates;
			this._supportsScopedDataReduction = supportsScopedDataReduction;
			this._supportsExtensionColumns = supportsExtensionColumns;
			this._canEditChangeDetectionMeasure = canEditChangeDetectionMeasure;
			this._supportChangeDetectionMeasureRefresh = supportChangeDetectionMeasureRefresh;
			this._isQnaEnabled = isQnaEnabled;
			this._supportsGroupSynchronization = supportsGroupSynchronization;
			this._supportsSparklineData = supportsSparklineData;
			this._supportsVisualCalculations = supportsVisualCalculations;
			this._insights = insightsCapabilities;
			this._transformCapabilities = clientTransformCapabilities;
		}

		// Token: 0x040003F2 RID: 1010
		[DataMember(Name = "DiscourageQueryAggregateUsage", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private bool _discourageQueryAggregateUsage;

		// Token: 0x040003F3 RID: 1011
		[DataMember(Name = "SupportsMedian", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private bool _supportsMedian;

		// Token: 0x040003F4 RID: 1012
		[DataMember(Name = "SupportsPercentile", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private bool _supportsPercentile;

		// Token: 0x040003F5 RID: 1013
		[DataMember(Name = "NormalizedFiveStateKpiRange", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private bool _normalizedFiveStateKpiRange;

		// Token: 0x040003F6 RID: 1014
		[DataMember(Name = "SupportsScopedEval", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private bool _supportsScopedEval;

		// Token: 0x040003F7 RID: 1015
		[DataMember(Name = "SupportsStringMinMax", IsRequired = false, EmitDefaultValue = false, Order = 7)]
		private bool _supportsStringMinMax;

		// Token: 0x040003F8 RID: 1016
		[DataMember(Name = "SupportsMultiTableTupleFilters", IsRequired = false, EmitDefaultValue = false, Order = 8)]
		private bool _supportsMultiTableTupleFilters;

		// Token: 0x040003F9 RID: 1017
		[DataMember(Name = "IsExtendable", IsRequired = false, EmitDefaultValue = false, Order = 9)]
		private bool _isExtendable;

		// Token: 0x040003FA RID: 1018
		[DataMember(Name = "SupportsTimeIntelligenceQuickMeasures", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		private bool _supportsTimeIntelligenceQuickMeasures;

		// Token: 0x040003FB RID: 1019
		[DataMember(Name = "SupportsBinnedLineSample", IsRequired = false, EmitDefaultValue = false, Order = 13)]
		private bool _supportsBinnedLineSample;

		// Token: 0x040003FC RID: 1020
		[DataMember(Name = "SupportsOverlappingPointsSample", IsRequired = false, EmitDefaultValue = false, Order = 14)]
		private bool _supportsOverlappingPointsSample;

		// Token: 0x040003FD RID: 1021
		[DataMember(Name = "LimitMultiColumnFiltersToQueryGroupColumns", IsRequired = false, EmitDefaultValue = false, Order = 15)]
		private bool _limitMultiColumnFiltersToQueryGroupColumns;

		// Token: 0x040003FE RID: 1022
		[DataMember(Name = "SupportsCalculatedColumns", IsRequired = false, EmitDefaultValue = false, Order = 16)]
		private bool? _supportsCalculatedColumns;

		// Token: 0x040003FF RID: 1023
		[DataMember(Name = "SupportsGrouping", IsRequired = false, EmitDefaultValue = false, Order = 17)]
		private bool? _supportsGrouping;

		// Token: 0x04000400 RID: 1024
		[DataMember(Name = "SupportsInsights", IsRequired = false, EmitDefaultValue = false, Order = 18)]
		private bool _supportsInsights;

		// Token: 0x04000401 RID: 1025
		[DataMember(Name = "SupportsQna", IsRequired = false, EmitDefaultValue = false, Order = 19)]
		private bool _supportsQna;

		// Token: 0x04000402 RID: 1026
		[DataMember(Name = "SupportsInstanceFilters", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		private bool _supportsInstanceFilters;

		// Token: 0x04000403 RID: 1027
		[DataMember(Name = "SupportsDataSourceVariables", IsRequired = false, EmitDefaultValue = false, Order = 21)]
		private bool _supportsDataSourceVariables;

		// Token: 0x04000404 RID: 1028
		[DataMember(Name = "Insights", IsRequired = false, EmitDefaultValue = false, Order = 22)]
		private InsightsCapabilities _insights;

		// Token: 0x04000405 RID: 1029
		[DataMember(Name = "SupportsSubqueryRegrouping", IsRequired = false, EmitDefaultValue = false, Order = 23)]
		private bool _supportsSubqueryRegrouping;

		// Token: 0x04000406 RID: 1030
		[DataMember(Name = "SupportsTopNPerLevel", IsRequired = false, EmitDefaultValue = false, Order = 24)]
		private bool _supportsTopNPerLevel;

		// Token: 0x04000407 RID: 1031
		[DataMember(Name = "SupportsFastRefresh", IsRequired = false, EmitDefaultValue = false, Order = 25)]
		private bool _supportsFastRefresh;

		// Token: 0x04000408 RID: 1032
		[DataMember(Name = "SupportsScopedAggregates", IsRequired = false, EmitDefaultValue = false, Order = 26)]
		private bool _supportsScopedAggregates;

		// Token: 0x04000409 RID: 1033
		[DataMember(Name = "SupportsExtensionColumns", IsRequired = false, EmitDefaultValue = false, Order = 27)]
		private bool _supportsExtensionColumns;

		// Token: 0x0400040A RID: 1034
		[DataMember(Name = "CanEditChangeDetectionMeasure", IsRequired = false, EmitDefaultValue = false, Order = 28)]
		private bool _canEditChangeDetectionMeasure;

		// Token: 0x0400040B RID: 1035
		[DataMember(Name = "IsQnaEnabled", IsRequired = false, EmitDefaultValue = false, Order = 29)]
		private bool _isQnaEnabled;

		// Token: 0x0400040C RID: 1036
		[DataMember(Name = "SupportsScopedDataReduction", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		private bool _supportsScopedDataReduction;

		// Token: 0x0400040D RID: 1037
		[DataMember(Name = "TransformCapabilities", IsRequired = false, EmitDefaultValue = false, Order = 31)]
		private ClientTransformCapabilities _transformCapabilities;

		// Token: 0x0400040E RID: 1038
		[DataMember(Name = "SupportsGroupSynchronization", IsRequired = false, EmitDefaultValue = false, Order = 32)]
		private bool _supportsGroupSynchronization;

		// Token: 0x0400040F RID: 1039
		[DataMember(Name = "SupportChangeDetectionMeasureRefresh", IsRequired = false, EmitDefaultValue = false, Order = 33)]
		private bool _supportChangeDetectionMeasureRefresh;

		// Token: 0x04000410 RID: 1040
		[DataMember(Name = "SupportsSparklineData", IsRequired = false, EmitDefaultValue = false, Order = 34)]
		private bool _supportsSparklineData;

		// Token: 0x04000411 RID: 1041
		[DataMember(Name = "SupportsVisualCalculations", IsRequired = false, EmitDefaultValue = false, Order = 35)]
		private bool _supportsVisualCalculations;
	}
}
