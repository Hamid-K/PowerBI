using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000685 RID: 1669
	internal sealed class GroupPartitionExpr : GroupAggregateExpr
	{
		// Token: 0x06004F2A RID: 20266 RVA: 0x0011FA0D File Offset: 0x0011DC0D
		internal GroupPartitionExpr(DistinctKind distinctKind, Node refArgExpr)
			: base(distinctKind)
		{
			this._argExpr = refArgExpr;
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06004F2B RID: 20267 RVA: 0x0011FA1D File Offset: 0x0011DC1D
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x04001CDE RID: 7390
		private readonly Node _argExpr;
	}
}
