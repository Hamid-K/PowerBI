using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A6 RID: 166
	internal sealed class SampleLimitOperator : LimitOperator
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000075C2 File Offset: 0x000057C2
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000075CA File Offset: 0x000057CA
		public Candidate<bool> PreserveKeyPoints { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000075D3 File Offset: 0x000057D3
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.SampleLimitOperator;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000075D7 File Offset: 0x000057D7
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
