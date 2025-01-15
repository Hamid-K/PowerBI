using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A9 RID: 169
	internal sealed class BottomLimitOperator : LimitOperator
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00007612 File Offset: 0x00005812
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.BottomLimitOperator;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00007616 File Offset: 0x00005816
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
