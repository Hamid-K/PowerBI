using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000698 RID: 1688
	internal sealed class RefExpr : Node
	{
		// Token: 0x06004F80 RID: 20352 RVA: 0x001208DF File Offset: 0x0011EADF
		internal RefExpr(Node refArgExpr)
		{
			this._argExpr = refArgExpr;
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x001208EE File Offset: 0x0011EAEE
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x04001D22 RID: 7458
		private readonly Node _argExpr;
	}
}
