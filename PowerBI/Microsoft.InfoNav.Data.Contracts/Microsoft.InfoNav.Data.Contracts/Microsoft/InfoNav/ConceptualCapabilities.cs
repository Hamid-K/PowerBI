using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000015 RID: 21
	public class ConceptualCapabilities
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000022C7 File Offset: 0x000004C7
		internal ConceptualCapabilities()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000022D0 File Offset: 0x000004D0
		public ConceptualCapabilities(bool discourageQueryAggregateUsage, bool discourageCompositeModels, bool supportsMedian, bool supportsPercentile, bool normalizedFiveStateKpiRange, bool supportsScopedEval, bool supportsStringMinMax, bool supportsMultiTableTupleFilters, bool supportsBinnedLineSample, bool supportsOverlappingPointsSample, bool limitMultiColumnFiltersToQueryGroupColumns, bool supportsInstanceFilters, bool supportsDataSourceVariables, bool supportsScopedDataReduction, bool supportsSubqueryRegrouping, bool supportsTopNPerLevel, bool supportsScopedAggregates, bool supportsExtensionColumns, bool supportsGroupSynchronization, bool supportsSparklineData, bool supportsVisualCalculations, TransformCapabilities transformCapabilities)
		{
			this.DiscourageQueryAggregateUsage = discourageQueryAggregateUsage;
			this.DiscourageCompositeModels = discourageCompositeModels;
			this.SupportsMedian = supportsMedian;
			this.SupportsPercentile = supportsPercentile;
			this.NormalizedFiveStateKpiRange = normalizedFiveStateKpiRange;
			this.SupportsScopedEval = supportsScopedEval;
			this.SupportsStringMinMax = supportsStringMinMax;
			this.SupportsMultiTableTupleFilters = supportsMultiTableTupleFilters;
			this.SupportsBinnedLineSample = supportsBinnedLineSample;
			this.SupportsOverlappingPointsSample = supportsOverlappingPointsSample;
			this.LimitMultiColumnFiltersToQueryGroupColumns = limitMultiColumnFiltersToQueryGroupColumns;
			this.SupportsInstanceFilters = supportsInstanceFilters;
			this.SupportsDataSourceVariables = supportsDataSourceVariables;
			this.SupportsScopedDataReduction = supportsScopedDataReduction;
			this.SupportsSubqueryRegrouping = supportsSubqueryRegrouping;
			this.SupportsTopNPerLevel = supportsTopNPerLevel;
			this.SupportsScopedAggregates = supportsScopedAggregates;
			this.SupportsExtensionColumns = supportsExtensionColumns;
			this.SupportsGroupSynchronization = supportsGroupSynchronization;
			this.SupportsSparklineData = supportsSparklineData;
			this.SupportsVisualCalculations = supportsVisualCalculations;
			this.TransformCapabilities = transformCapabilities;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002390 File Offset: 0x00000590
		public bool DiscourageQueryAggregateUsage { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002398 File Offset: 0x00000598
		public bool DiscourageCompositeModels { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000023A0 File Offset: 0x000005A0
		public bool SupportsMedian { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000023A8 File Offset: 0x000005A8
		public bool SupportsPercentile { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023B0 File Offset: 0x000005B0
		public bool NormalizedFiveStateKpiRange { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023B8 File Offset: 0x000005B8
		public bool SupportsScopedEval { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000023C0 File Offset: 0x000005C0
		public bool SupportsStringMinMax { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023C8 File Offset: 0x000005C8
		public bool SupportsMultiTableTupleFilters { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000023D0 File Offset: 0x000005D0
		public bool SupportsBinnedLineSample { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000023D8 File Offset: 0x000005D8
		public bool SupportsOverlappingPointsSample { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000023E0 File Offset: 0x000005E0
		public bool LimitMultiColumnFiltersToQueryGroupColumns { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000023E8 File Offset: 0x000005E8
		public bool SupportsInstanceFilters { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000023F0 File Offset: 0x000005F0
		public bool SupportsDataSourceVariables { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000023F8 File Offset: 0x000005F8
		public bool SupportsScopedDataReduction { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002400 File Offset: 0x00000600
		public bool SupportsSubqueryRegrouping { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002408 File Offset: 0x00000608
		public bool SupportsTopNPerLevel { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002410 File Offset: 0x00000610
		public bool SupportsScopedAggregates { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002418 File Offset: 0x00000618
		public bool SupportsExtensionColumns { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002420 File Offset: 0x00000620
		public bool SupportsGroupSynchronization { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002428 File Offset: 0x00000628
		public bool SupportsSparklineData { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002430 File Offset: 0x00000630
		public bool SupportsVisualCalculations { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002438 File Offset: 0x00000638
		public TransformCapabilities TransformCapabilities { get; }
	}
}
