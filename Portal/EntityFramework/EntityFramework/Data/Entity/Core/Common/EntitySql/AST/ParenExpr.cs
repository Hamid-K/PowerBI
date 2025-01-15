using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000694 RID: 1684
	internal sealed class ParenExpr : Node
	{
		// Token: 0x06004F71 RID: 20337 RVA: 0x0012079A File Offset: 0x0011E99A
		internal ParenExpr(Node expr)
		{
			this._expr = expr;
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x001207A9 File Offset: 0x0011E9A9
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x04001D18 RID: 7448
		private readonly Node _expr;
	}
}
