using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000604 RID: 1540
	internal class BooleanExpressionTermRewriter<T_From, T_To> : Visitor<T_From, BoolExpr<T_To>>
	{
		// Token: 0x06004B3D RID: 19261 RVA: 0x0010A0C2 File Offset: 0x001082C2
		internal BooleanExpressionTermRewriter(Func<TermExpr<T_From>, BoolExpr<T_To>> translator)
		{
			this._translator = translator;
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x0010A0D1 File Offset: 0x001082D1
		internal override BoolExpr<T_To> VisitFalse(FalseExpr<T_From> expression)
		{
			return FalseExpr<T_To>.Value;
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x0010A0D8 File Offset: 0x001082D8
		internal override BoolExpr<T_To> VisitTrue(TrueExpr<T_From> expression)
		{
			return TrueExpr<T_To>.Value;
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x0010A0DF File Offset: 0x001082DF
		internal override BoolExpr<T_To> VisitNot(NotExpr<T_From> expression)
		{
			return new NotExpr<T_To>(expression.Child.Accept<BoolExpr<T_To>>(this));
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x0010A0F2 File Offset: 0x001082F2
		internal override BoolExpr<T_To> VisitTerm(TermExpr<T_From> expression)
		{
			return this._translator(expression);
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x0010A100 File Offset: 0x00108300
		internal override BoolExpr<T_To> VisitAnd(AndExpr<T_From> expression)
		{
			return new AndExpr<T_To>(this.VisitChildren(expression));
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x0010A10E File Offset: 0x0010830E
		internal override BoolExpr<T_To> VisitOr(OrExpr<T_From> expression)
		{
			return new OrExpr<T_To>(this.VisitChildren(expression));
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x0010A11C File Offset: 0x0010831C
		private IEnumerable<BoolExpr<T_To>> VisitChildren(TreeExpr<T_From> expression)
		{
			foreach (BoolExpr<T_From> boolExpr in expression.Children)
			{
				yield return boolExpr.Accept<BoolExpr<T_To>>(this);
			}
			HashSet<BoolExpr<T_From>>.Enumerator enumerator = default(HashSet<BoolExpr<T_From>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04001A51 RID: 6737
		private readonly Func<TermExpr<T_From>, BoolExpr<T_To>> _translator;
	}
}
