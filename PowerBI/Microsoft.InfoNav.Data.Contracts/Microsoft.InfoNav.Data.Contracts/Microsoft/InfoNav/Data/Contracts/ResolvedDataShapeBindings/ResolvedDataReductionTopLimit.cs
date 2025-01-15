using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A6 RID: 166
	public sealed class ResolvedDataReductionTopLimit : ResolvedDataReductionLimit
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0000B64B File Offset: 0x0000984B
		public ResolvedDataReductionTopLimit(int? count)
		{
			this.Count = count;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000B65A File Offset: 0x0000985A
		public int? Count { get; }
	}
}
