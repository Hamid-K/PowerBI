using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000620 RID: 1568
	internal class TermCounter<T_Identifier> : Visitor<T_Identifier, int>
	{
		// Token: 0x06004BD1 RID: 19409 RVA: 0x0010B627 File Offset: 0x00109827
		internal static int CountTerms(BoolExpr<T_Identifier> expression)
		{
			return expression.Accept<int>(TermCounter<T_Identifier>._instance);
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0010B634 File Offset: 0x00109834
		internal override int VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return 0;
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0010B637 File Offset: 0x00109837
		internal override int VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return 0;
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0010B63A File Offset: 0x0010983A
		internal override int VisitTerm(TermExpr<T_Identifier> expression)
		{
			return 1;
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0010B63D File Offset: 0x0010983D
		internal override int VisitNot(NotExpr<T_Identifier> expression)
		{
			return expression.Child.Accept<int>(this);
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0010B64B File Offset: 0x0010984B
		internal override int VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0010B654 File Offset: 0x00109854
		internal override int VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0010B660 File Offset: 0x00109860
		private int VisitTree(TreeExpr<T_Identifier> expression)
		{
			int num = 0;
			foreach (BoolExpr<T_Identifier> boolExpr in expression.Children)
			{
				num += boolExpr.Accept<int>(this);
			}
			return num;
		}

		// Token: 0x04001A7F RID: 6783
		private static readonly TermCounter<T_Identifier> _instance = new TermCounter<T_Identifier>();
	}
}
