using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A4 RID: 164
	public sealed class ResolvedDataReductionSampleLimit : ResolvedDataReductionLimit
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x0000B60E File Offset: 0x0000980E
		public ResolvedDataReductionSampleLimit(int? count)
		{
			this.Count = count;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000B61D File Offset: 0x0000981D
		public int? Count { get; }
	}
}
