using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A7 RID: 167
	internal sealed class FirstLimitOperator : LimitOperator
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000075E8 File Offset: 0x000057E8
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.FirstLimitOperator;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000075EC File Offset: 0x000057EC
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
