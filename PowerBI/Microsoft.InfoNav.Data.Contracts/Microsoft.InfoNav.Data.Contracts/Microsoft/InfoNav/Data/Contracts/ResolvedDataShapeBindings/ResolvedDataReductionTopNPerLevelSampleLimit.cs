using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A7 RID: 167
	public sealed class ResolvedDataReductionTopNPerLevelSampleLimit : ResolvedDataReductionLimit
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0000B662 File Offset: 0x00009862
		public ResolvedDataReductionTopNPerLevelSampleLimit(int? count, ResolvedDataReductionWindowExpansionState windowExpansion)
		{
			this.Count = count;
			this.WindowExpansion = windowExpansion;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000B678 File Offset: 0x00009878
		public int? Count { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000B680 File Offset: 0x00009880
		public ResolvedDataReductionWindowExpansionState WindowExpansion { get; }
	}
}
