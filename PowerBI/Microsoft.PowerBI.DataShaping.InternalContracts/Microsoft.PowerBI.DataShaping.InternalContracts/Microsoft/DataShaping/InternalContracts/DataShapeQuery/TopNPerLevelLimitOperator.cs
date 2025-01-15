using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000AC RID: 172
	internal sealed class TopNPerLevelLimitOperator : LimitOperator
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000076C7 File Offset: 0x000058C7
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000076CF File Offset: 0x000058CF
		public List<List<Expression>> Levels { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000076D8 File Offset: 0x000058D8
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x000076E0 File Offset: 0x000058E0
		public LimitWindowExpansionInstance WindowExpansionInstance { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000076E9 File Offset: 0x000058E9
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.TopNPerLevelLimitOperator;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000076ED File Offset: 0x000058ED
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
