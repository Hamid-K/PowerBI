using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A5 RID: 165
	internal sealed class TopLimitOperator : LimitOperator
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000758B File Offset: 0x0000578B
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00007593 File Offset: 0x00005793
		public long? Skip { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000759C File Offset: 0x0000579C
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x000075A4 File Offset: 0x000057A4
		public Candidate<bool> IsStrict { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000075AD File Offset: 0x000057AD
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.TopLimitOperator;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000075B1 File Offset: 0x000057B1
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
