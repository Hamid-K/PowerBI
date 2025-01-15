using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000678 RID: 1656
	internal sealed class CaseExpr : Node
	{
		// Token: 0x06004F06 RID: 20230 RVA: 0x0011F758 File Offset: 0x0011D958
		internal CaseExpr(NodeList<WhenThenExpr> whenThenExpr)
			: this(whenThenExpr, null)
		{
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0011F762 File Offset: 0x0011D962
		internal CaseExpr(NodeList<WhenThenExpr> whenThenExpr, Node elseExpr)
		{
			this._whenThenExpr = whenThenExpr;
			this._elseExpr = elseExpr;
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06004F08 RID: 20232 RVA: 0x0011F778 File Offset: 0x0011D978
		internal NodeList<WhenThenExpr> WhenThenExprList
		{
			get
			{
				return this._whenThenExpr;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x0011F780 File Offset: 0x0011D980
		internal Node ElseExpr
		{
			get
			{
				return this._elseExpr;
			}
		}

		// Token: 0x04001CBE RID: 7358
		private readonly NodeList<WhenThenExpr> _whenThenExpr;

		// Token: 0x04001CBF RID: 7359
		private readonly Node _elseExpr;
	}
}
