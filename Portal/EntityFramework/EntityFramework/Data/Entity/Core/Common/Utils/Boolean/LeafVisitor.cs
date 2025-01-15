using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000615 RID: 1557
	internal class LeafVisitor<T_Identifier> : Visitor<T_Identifier, bool>
	{
		// Token: 0x06004B93 RID: 19347 RVA: 0x0010AC9C File Offset: 0x00108E9C
		private LeafVisitor()
		{
			this._terms = new List<TermExpr<T_Identifier>>();
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0010ACB0 File Offset: 0x00108EB0
		internal static List<TermExpr<T_Identifier>> GetTerms(BoolExpr<T_Identifier> expression)
		{
			LeafVisitor<T_Identifier> leafVisitor = new LeafVisitor<T_Identifier>();
			expression.Accept<bool>(leafVisitor);
			return leafVisitor._terms;
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0010ACD1 File Offset: 0x00108ED1
		internal static IEnumerable<T_Identifier> GetLeaves(BoolExpr<T_Identifier> expression)
		{
			return from term in LeafVisitor<T_Identifier>.GetTerms(expression)
				select term.Identifier;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0010ACFD File Offset: 0x00108EFD
		internal override bool VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return true;
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x0010AD00 File Offset: 0x00108F00
		internal override bool VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return true;
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x0010AD03 File Offset: 0x00108F03
		internal override bool VisitTerm(TermExpr<T_Identifier> expression)
		{
			this._terms.Add(expression);
			return true;
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0010AD12 File Offset: 0x00108F12
		internal override bool VisitNot(NotExpr<T_Identifier> expression)
		{
			return expression.Child.Accept<bool>(this);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0010AD20 File Offset: 0x00108F20
		internal override bool VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0010AD29 File Offset: 0x00108F29
		internal override bool VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0010AD34 File Offset: 0x00108F34
		private bool VisitTree(TreeExpr<T_Identifier> expression)
		{
			foreach (BoolExpr<T_Identifier> boolExpr in expression.Children)
			{
				boolExpr.Accept<bool>(this);
			}
			return true;
		}

		// Token: 0x04001A70 RID: 6768
		private readonly List<TermExpr<T_Identifier>> _terms;
	}
}
