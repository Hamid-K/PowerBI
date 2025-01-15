using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x0200009F RID: 159
	public sealed class ResolvedDataReductionBottomLimit : ResolvedDataReductionLimit
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0000B55F File Offset: 0x0000975F
		public ResolvedDataReductionBottomLimit(int? count)
		{
			this.Count = count;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000B56E File Offset: 0x0000976E
		public int? Count { get; }
	}
}
