using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A2 RID: 162
	public sealed class ResolvedDataReductionOverlappingPointsSampleLimit : ResolvedDataReductionLimit
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0000B5B3 File Offset: 0x000097B3
		public ResolvedDataReductionOverlappingPointsSampleLimit(int? count, ResolvedDataReductionPlotAxisBinding x, ResolvedDataReductionPlotAxisBinding y)
		{
			this.Count = count;
			this.X = x;
			this.Y = y;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000B5D0 File Offset: 0x000097D0
		public int? Count { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000B5D8 File Offset: 0x000097D8
		public ResolvedDataReductionPlotAxisBinding X { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public ResolvedDataReductionPlotAxisBinding Y { get; }
	}
}
