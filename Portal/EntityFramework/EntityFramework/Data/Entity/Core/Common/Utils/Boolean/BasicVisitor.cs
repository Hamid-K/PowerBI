using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000603 RID: 1539
	internal abstract class BasicVisitor<T_Identifier> : Visitor<T_Identifier, BoolExpr<T_Identifier>>
	{
		// Token: 0x06004B35 RID: 19253 RVA: 0x0010A061 File Offset: 0x00108261
		internal override BoolExpr<T_Identifier> VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x0010A064 File Offset: 0x00108264
		internal override BoolExpr<T_Identifier> VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x0010A067 File Offset: 0x00108267
		internal override BoolExpr<T_Identifier> VisitTerm(TermExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x0010A06A File Offset: 0x0010826A
		internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
		{
			return new NotExpr<T_Identifier>(expression.Child.Accept<BoolExpr<T_Identifier>>(this));
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x0010A07D File Offset: 0x0010827D
		internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
		{
			return new AndExpr<T_Identifier>(this.AcceptChildren(expression.Children));
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x0010A090 File Offset: 0x00108290
		internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
		{
			return new OrExpr<T_Identifier>(this.AcceptChildren(expression.Children));
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x0010A0A3 File Offset: 0x001082A3
		private IEnumerable<BoolExpr<T_Identifier>> AcceptChildren(IEnumerable<BoolExpr<T_Identifier>> children)
		{
			foreach (BoolExpr<T_Identifier> boolExpr in children)
			{
				yield return boolExpr.Accept<BoolExpr<T_Identifier>>(this);
			}
			IEnumerator<BoolExpr<T_Identifier>> enumerator = null;
			yield break;
			yield break;
		}
	}
}
