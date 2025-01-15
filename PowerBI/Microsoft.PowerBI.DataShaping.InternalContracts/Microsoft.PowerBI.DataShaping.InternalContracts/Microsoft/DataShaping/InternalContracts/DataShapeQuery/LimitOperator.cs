using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A4 RID: 164
	internal abstract class LimitOperator
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00007561 File Offset: 0x00005761
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00007569 File Offset: 0x00005769
		public Candidate<int> Count { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003DD RID: 989
		public abstract ObjectType ObjectType { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00007572 File Offset: 0x00005772
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000757A File Offset: 0x0000577A
		public int? WarningCount { get; set; }

		// Token: 0x060003E0 RID: 992
		public abstract TResult Accept<TResult>(LimitVisitor<TResult> visitor);
	}
}
