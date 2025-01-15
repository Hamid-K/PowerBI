using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200067C RID: 1660
	internal sealed class DerefExpr : Node
	{
		// Token: 0x06004F14 RID: 20244 RVA: 0x0011F805 File Offset: 0x0011DA05
		internal DerefExpr(Node derefArgExpr)
		{
			this._argExpr = derefArgExpr;
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x0011F814 File Offset: 0x0011DA14
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x04001CC6 RID: 7366
		private readonly Node _argExpr;
	}
}
