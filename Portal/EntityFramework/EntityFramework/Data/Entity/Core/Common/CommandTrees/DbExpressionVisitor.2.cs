using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006BA RID: 1722
	public abstract class DbExpressionVisitor<TResultType>
	{
		// Token: 0x06005087 RID: 20615
		public abstract TResultType Visit(DbExpression expression);

		// Token: 0x06005088 RID: 20616
		public abstract TResultType Visit(DbAndExpression expression);

		// Token: 0x06005089 RID: 20617
		public abstract TResultType Visit(DbApplyExpression expression);

		// Token: 0x0600508A RID: 20618
		public abstract TResultType Visit(DbArithmeticExpression expression);

		// Token: 0x0600508B RID: 20619
		public abstract TResultType Visit(DbCaseExpression expression);

		// Token: 0x0600508C RID: 20620
		public abstract TResultType Visit(DbCastExpression expression);

		// Token: 0x0600508D RID: 20621
		public abstract TResultType Visit(DbComparisonExpression expression);

		// Token: 0x0600508E RID: 20622
		public abstract TResultType Visit(DbConstantExpression expression);

		// Token: 0x0600508F RID: 20623
		public abstract TResultType Visit(DbCrossJoinExpression expression);

		// Token: 0x06005090 RID: 20624
		public abstract TResultType Visit(DbDerefExpression expression);

		// Token: 0x06005091 RID: 20625
		public abstract TResultType Visit(DbDistinctExpression expression);

		// Token: 0x06005092 RID: 20626
		public abstract TResultType Visit(DbElementExpression expression);

		// Token: 0x06005093 RID: 20627
		public abstract TResultType Visit(DbExceptExpression expression);

		// Token: 0x06005094 RID: 20628
		public abstract TResultType Visit(DbFilterExpression expression);

		// Token: 0x06005095 RID: 20629
		public abstract TResultType Visit(DbFunctionExpression expression);

		// Token: 0x06005096 RID: 20630
		public abstract TResultType Visit(DbEntityRefExpression expression);

		// Token: 0x06005097 RID: 20631
		public abstract TResultType Visit(DbRefKeyExpression expression);

		// Token: 0x06005098 RID: 20632
		public abstract TResultType Visit(DbGroupByExpression expression);

		// Token: 0x06005099 RID: 20633
		public abstract TResultType Visit(DbIntersectExpression expression);

		// Token: 0x0600509A RID: 20634
		public abstract TResultType Visit(DbIsEmptyExpression expression);

		// Token: 0x0600509B RID: 20635
		public abstract TResultType Visit(DbIsNullExpression expression);

		// Token: 0x0600509C RID: 20636
		public abstract TResultType Visit(DbIsOfExpression expression);

		// Token: 0x0600509D RID: 20637
		public abstract TResultType Visit(DbJoinExpression expression);

		// Token: 0x0600509E RID: 20638 RVA: 0x00121D66 File Offset: 0x0011FF66
		public virtual TResultType Visit(DbLambdaExpression expression)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600509F RID: 20639
		public abstract TResultType Visit(DbLikeExpression expression);

		// Token: 0x060050A0 RID: 20640
		public abstract TResultType Visit(DbLimitExpression expression);

		// Token: 0x060050A1 RID: 20641
		public abstract TResultType Visit(DbNewInstanceExpression expression);

		// Token: 0x060050A2 RID: 20642
		public abstract TResultType Visit(DbNotExpression expression);

		// Token: 0x060050A3 RID: 20643
		public abstract TResultType Visit(DbNullExpression expression);

		// Token: 0x060050A4 RID: 20644
		public abstract TResultType Visit(DbOfTypeExpression expression);

		// Token: 0x060050A5 RID: 20645
		public abstract TResultType Visit(DbOrExpression expression);

		// Token: 0x060050A6 RID: 20646
		public abstract TResultType Visit(DbParameterReferenceExpression expression);

		// Token: 0x060050A7 RID: 20647
		public abstract TResultType Visit(DbProjectExpression expression);

		// Token: 0x060050A8 RID: 20648
		public abstract TResultType Visit(DbPropertyExpression expression);

		// Token: 0x060050A9 RID: 20649
		public abstract TResultType Visit(DbQuantifierExpression expression);

		// Token: 0x060050AA RID: 20650
		public abstract TResultType Visit(DbRefExpression expression);

		// Token: 0x060050AB RID: 20651
		public abstract TResultType Visit(DbRelationshipNavigationExpression expression);

		// Token: 0x060050AC RID: 20652
		public abstract TResultType Visit(DbScanExpression expression);

		// Token: 0x060050AD RID: 20653
		public abstract TResultType Visit(DbSortExpression expression);

		// Token: 0x060050AE RID: 20654
		public abstract TResultType Visit(DbSkipExpression expression);

		// Token: 0x060050AF RID: 20655
		public abstract TResultType Visit(DbTreatExpression expression);

		// Token: 0x060050B0 RID: 20656
		public abstract TResultType Visit(DbUnionAllExpression expression);

		// Token: 0x060050B1 RID: 20657
		public abstract TResultType Visit(DbVariableReferenceExpression expression);

		// Token: 0x060050B2 RID: 20658 RVA: 0x00121D6D File Offset: 0x0011FF6D
		public virtual TResultType Visit(DbInExpression expression)
		{
			throw new NotImplementedException(Strings.VisitDbInExpressionNotImplemented);
		}
	}
}
