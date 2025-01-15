using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x0200009E RID: 158
	public sealed class ResolvedDataReductionBinnedLineSampleLimit : ResolvedDataReductionLimit
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x0000B50A File Offset: 0x0000970A
		public ResolvedDataReductionBinnedLineSampleLimit(int? count, int? minPointsPerSeries, int? maxDynamicSeriesCount, int? primaryScalarKey, int? warningCount)
		{
			this.Count = count;
			this.MinPointsPerSeries = minPointsPerSeries;
			this.MaxDynamicSeriesCount = maxDynamicSeriesCount;
			this.PrimaryScalarKey = primaryScalarKey;
			this.WarningCount = warningCount;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000B537 File Offset: 0x00009737
		public int? Count { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000B53F File Offset: 0x0000973F
		public int? MinPointsPerSeries { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000B547 File Offset: 0x00009747
		public int? MaxDynamicSeriesCount { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000B54F File Offset: 0x0000974F
		public int? PrimaryScalarKey { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000B557 File Offset: 0x00009757
		public int? WarningCount { get; }
	}
}
