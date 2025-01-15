using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000619 RID: 1561
	internal abstract class NormalFormNode<T_Identifier>
	{
		// Token: 0x06004BA7 RID: 19367 RVA: 0x0010AE5D File Offset: 0x0010905D
		protected NormalFormNode(BoolExpr<T_Identifier> expr)
		{
			this._expr = expr.Simplify();
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x0010AE71 File Offset: 0x00109071
		internal BoolExpr<T_Identifier> Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0010AE79 File Offset: 0x00109079
		protected static BoolExpr<T_Identifier> ExprSelector<T_NormalFormNode>(T_NormalFormNode node) where T_NormalFormNode : NormalFormNode<T_Identifier>
		{
			return node._expr;
		}

		// Token: 0x04001A75 RID: 6773
		private readonly BoolExpr<T_Identifier> _expr;
	}
}
