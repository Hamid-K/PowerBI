using System;
using System.ComponentModel;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000008 RID: 8
	[ImmutableObject(true)]
	internal sealed class DataVolumeLevel
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002548 File Offset: 0x00000748
		internal DataVolumeLevel(int maxIntersectionCount, int maxPotentialIntersectionCount, int primaryCount, int secondaryCount, int primaryCountNoStats, int secondaryCountNoStats, int windowSize, int minPointsPerSeries, int maxPointsPerSeries, int maxDynamicSeriesCount, int outerScopedReductionCount, int innerScopedReductionCount)
		{
			this.MaxIntersectionCount = maxIntersectionCount;
			this.MaxPotentialIntersectionCount = maxPotentialIntersectionCount;
			this.PrimaryCount = primaryCount;
			this.SecondaryCount = secondaryCount;
			this.PrimaryCountNoStats = primaryCountNoStats;
			this.SecondaryCountNoStats = secondaryCountNoStats;
			this.WindowSize = windowSize;
			this.MinPointsPerSeries = minPointsPerSeries;
			this.MaxPointsPerSeries = maxPointsPerSeries;
			this.MaxDynamicSeriesCount = maxDynamicSeriesCount;
			this.OuterScopedReductionCount = outerScopedReductionCount;
			this.InnerScopedReductionCount = innerScopedReductionCount;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000025B8 File Offset: 0x000007B8
		internal int MaxIntersectionCount { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025C0 File Offset: 0x000007C0
		internal int MaxPotentialIntersectionCount { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025C8 File Offset: 0x000007C8
		internal int PrimaryCount { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025D0 File Offset: 0x000007D0
		internal int SecondaryCount { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025D8 File Offset: 0x000007D8
		internal int PrimaryCountNoStats { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025E0 File Offset: 0x000007E0
		internal int SecondaryCountNoStats { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025E8 File Offset: 0x000007E8
		internal int WindowSize { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025F0 File Offset: 0x000007F0
		internal int MinPointsPerSeries { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025F8 File Offset: 0x000007F8
		internal int MaxPointsPerSeries { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002600 File Offset: 0x00000800
		internal int MaxDynamicSeriesCount { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002608 File Offset: 0x00000808
		internal int OuterScopedReductionCount { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002610 File Offset: 0x00000810
		internal int InnerScopedReductionCount { get; }

		// Token: 0x0600002A RID: 42 RVA: 0x00002618 File Offset: 0x00000818
		internal int GetMaxEffectiveIntersectionCount(bool intersectionLimitSupported)
		{
			if (!intersectionLimitSupported)
			{
				return this.MaxIntersectionCount;
			}
			return this.MaxPotentialIntersectionCount;
		}
	}
}
