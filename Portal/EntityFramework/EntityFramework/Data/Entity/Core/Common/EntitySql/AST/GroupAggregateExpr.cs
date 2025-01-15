using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000683 RID: 1667
	internal abstract class GroupAggregateExpr : Node
	{
		// Token: 0x06004F27 RID: 20263 RVA: 0x0011F9E7 File Offset: 0x0011DBE7
		internal GroupAggregateExpr(DistinctKind distinctKind)
		{
			this.DistinctKind = distinctKind;
		}

		// Token: 0x04001CDB RID: 7387
		internal readonly DistinctKind DistinctKind;

		// Token: 0x04001CDC RID: 7388
		internal GroupAggregateInfo AggregateInfo;
	}
}
