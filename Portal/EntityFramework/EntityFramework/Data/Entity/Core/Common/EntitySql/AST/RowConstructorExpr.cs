using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200069A RID: 1690
	internal sealed class RowConstructorExpr : Node
	{
		// Token: 0x06004F84 RID: 20356 RVA: 0x0012090D File Offset: 0x0011EB0D
		internal RowConstructorExpr(NodeList<AliasedExpr> exprList)
		{
			this._exprList = exprList;
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x0012091C File Offset: 0x0011EB1C
		internal NodeList<AliasedExpr> AliasedExprList
		{
			get
			{
				return this._exprList;
			}
		}

		// Token: 0x04001D24 RID: 7460
		private readonly NodeList<AliasedExpr> _exprList;
	}
}
