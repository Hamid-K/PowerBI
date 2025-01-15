using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000686 RID: 1670
	internal sealed class HavingClause : Node
	{
		// Token: 0x06004F2C RID: 20268 RVA: 0x0011FA25 File Offset: 0x0011DC25
		internal HavingClause(Node havingExpr, uint methodCallCounter)
		{
			this._havingExpr = havingExpr;
			this._methodCallCount = methodCallCounter;
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06004F2D RID: 20269 RVA: 0x0011FA3B File Offset: 0x0011DC3B
		internal Node HavingPredicate
		{
			get
			{
				return this._havingExpr;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x0011FA43 File Offset: 0x0011DC43
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x04001CDF RID: 7391
		private readonly Node _havingExpr;

		// Token: 0x04001CE0 RID: 7392
		private readonly uint _methodCallCount;
	}
}
