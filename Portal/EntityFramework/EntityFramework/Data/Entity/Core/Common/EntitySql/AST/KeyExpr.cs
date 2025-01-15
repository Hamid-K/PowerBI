using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200068A RID: 1674
	internal class KeyExpr : Node
	{
		// Token: 0x06004F39 RID: 20281 RVA: 0x0011FB1A File Offset: 0x0011DD1A
		internal KeyExpr(Node argExpr)
		{
			this._argExpr = argExpr;
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x0011FB29 File Offset: 0x0011DD29
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x04001CED RID: 7405
		private readonly Node _argExpr;
	}
}
