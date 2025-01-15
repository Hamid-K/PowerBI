using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A8 RID: 168
	internal sealed class LastLimitOperator : LimitOperator
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000075FD File Offset: 0x000057FD
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.LastLimitOperator;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00007601 File Offset: 0x00005801
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
