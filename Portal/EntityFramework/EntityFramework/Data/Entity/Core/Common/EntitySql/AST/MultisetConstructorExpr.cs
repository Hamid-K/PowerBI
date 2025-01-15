using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200068E RID: 1678
	internal sealed class MultisetConstructorExpr : Node
	{
		// Token: 0x06004F5B RID: 20315 RVA: 0x00120632 File Offset: 0x0011E832
		internal MultisetConstructorExpr(NodeList<Node> exprList)
		{
			this._exprList = exprList;
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x00120641 File Offset: 0x0011E841
		internal NodeList<Node> ExprList
		{
			get
			{
				return this._exprList;
			}
		}

		// Token: 0x04001D06 RID: 7430
		private readonly NodeList<Node> _exprList;
	}
}
