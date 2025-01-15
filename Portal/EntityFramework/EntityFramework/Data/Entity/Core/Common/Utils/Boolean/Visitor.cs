using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000626 RID: 1574
	internal abstract class Visitor<T_Identifier, T_Return>
	{
		// Token: 0x06004C08 RID: 19464
		internal abstract T_Return VisitTrue(TrueExpr<T_Identifier> expression);

		// Token: 0x06004C09 RID: 19465
		internal abstract T_Return VisitFalse(FalseExpr<T_Identifier> expression);

		// Token: 0x06004C0A RID: 19466
		internal abstract T_Return VisitTerm(TermExpr<T_Identifier> expression);

		// Token: 0x06004C0B RID: 19467
		internal abstract T_Return VisitNot(NotExpr<T_Identifier> expression);

		// Token: 0x06004C0C RID: 19468
		internal abstract T_Return VisitAnd(AndExpr<T_Identifier> expression);

		// Token: 0x06004C0D RID: 19469
		internal abstract T_Return VisitOr(OrExpr<T_Identifier> expression);
	}
}
