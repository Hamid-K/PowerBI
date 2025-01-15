using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B9 RID: 1721
	public abstract class DbExpressionVisitor
	{
		// Token: 0x0600505A RID: 20570
		public abstract void Visit(DbExpression expression);

		// Token: 0x0600505B RID: 20571
		public abstract void Visit(DbAndExpression expression);

		// Token: 0x0600505C RID: 20572
		public abstract void Visit(DbApplyExpression expression);

		// Token: 0x0600505D RID: 20573
		public abstract void Visit(DbArithmeticExpression expression);

		// Token: 0x0600505E RID: 20574
		public abstract void Visit(DbCaseExpression expression);

		// Token: 0x0600505F RID: 20575
		public abstract void Visit(DbCastExpression expression);

		// Token: 0x06005060 RID: 20576
		public abstract void Visit(DbComparisonExpression expression);

		// Token: 0x06005061 RID: 20577
		public abstract void Visit(DbConstantExpression expression);

		// Token: 0x06005062 RID: 20578
		public abstract void Visit(DbCrossJoinExpression expression);

		// Token: 0x06005063 RID: 20579
		public abstract void Visit(DbDerefExpression expression);

		// Token: 0x06005064 RID: 20580
		public abstract void Visit(DbDistinctExpression expression);

		// Token: 0x06005065 RID: 20581
		public abstract void Visit(DbElementExpression expression);

		// Token: 0x06005066 RID: 20582
		public abstract void Visit(DbExceptExpression expression);

		// Token: 0x06005067 RID: 20583
		public abstract void Visit(DbFilterExpression expression);

		// Token: 0x06005068 RID: 20584
		public abstract void Visit(DbFunctionExpression expression);

		// Token: 0x06005069 RID: 20585
		public abstract void Visit(DbEntityRefExpression expression);

		// Token: 0x0600506A RID: 20586
		public abstract void Visit(DbRefKeyExpression expression);

		// Token: 0x0600506B RID: 20587
		public abstract void Visit(DbGroupByExpression expression);

		// Token: 0x0600506C RID: 20588
		public abstract void Visit(DbIntersectExpression expression);

		// Token: 0x0600506D RID: 20589
		public abstract void Visit(DbIsEmptyExpression expression);

		// Token: 0x0600506E RID: 20590
		public abstract void Visit(DbIsNullExpression expression);

		// Token: 0x0600506F RID: 20591
		public abstract void Visit(DbIsOfExpression expression);

		// Token: 0x06005070 RID: 20592
		public abstract void Visit(DbJoinExpression expression);

		// Token: 0x06005071 RID: 20593 RVA: 0x00121D4B File Offset: 0x0011FF4B
		public virtual void Visit(DbLambdaExpression expression)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005072 RID: 20594
		public abstract void Visit(DbLikeExpression expression);

		// Token: 0x06005073 RID: 20595
		public abstract void Visit(DbLimitExpression expression);

		// Token: 0x06005074 RID: 20596
		public abstract void Visit(DbNewInstanceExpression expression);

		// Token: 0x06005075 RID: 20597
		public abstract void Visit(DbNotExpression expression);

		// Token: 0x06005076 RID: 20598
		public abstract void Visit(DbNullExpression expression);

		// Token: 0x06005077 RID: 20599
		public abstract void Visit(DbOfTypeExpression expression);

		// Token: 0x06005078 RID: 20600
		public abstract void Visit(DbOrExpression expression);

		// Token: 0x06005079 RID: 20601
		public abstract void Visit(DbParameterReferenceExpression expression);

		// Token: 0x0600507A RID: 20602
		public abstract void Visit(DbProjectExpression expression);

		// Token: 0x0600507B RID: 20603
		public abstract void Visit(DbPropertyExpression expression);

		// Token: 0x0600507C RID: 20604
		public abstract void Visit(DbQuantifierExpression expression);

		// Token: 0x0600507D RID: 20605
		public abstract void Visit(DbRefExpression expression);

		// Token: 0x0600507E RID: 20606
		public abstract void Visit(DbRelationshipNavigationExpression expression);

		// Token: 0x0600507F RID: 20607
		public abstract void Visit(DbScanExpression expression);

		// Token: 0x06005080 RID: 20608
		public abstract void Visit(DbSkipExpression expression);

		// Token: 0x06005081 RID: 20609
		public abstract void Visit(DbSortExpression expression);

		// Token: 0x06005082 RID: 20610
		public abstract void Visit(DbTreatExpression expression);

		// Token: 0x06005083 RID: 20611
		public abstract void Visit(DbUnionAllExpression expression);

		// Token: 0x06005084 RID: 20612
		public abstract void Visit(DbVariableReferenceExpression expression);

		// Token: 0x06005085 RID: 20613 RVA: 0x00121D52 File Offset: 0x0011FF52
		public virtual void Visit(DbInExpression expression)
		{
			throw new NotImplementedException(Strings.VisitDbInExpressionNotImplemented);
		}
	}
}
