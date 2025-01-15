using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D7 RID: 1495
	internal abstract class UpdateExpressionVisitor<TReturn> : DbExpressionVisitor<TReturn>
	{
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060047FE RID: 18430
		protected abstract string VisitorName { get; }

		// Token: 0x060047FF RID: 18431 RVA: 0x0010010C File Offset: 0x000FE30C
		protected NotSupportedException ConstructNotSupportedException(DbExpression node)
		{
			return new NotSupportedException(Strings.Update_UnsupportedExpressionKind((node == null) ? null : node.ExpressionKind.ToString(), this.VisitorName));
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x00100143 File Offset: 0x000FE343
		public override TReturn Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			if (expression != null)
			{
				return expression.Accept<TReturn>(this);
			}
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x00100163 File Offset: 0x000FE363
		public override TReturn Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00100178 File Offset: 0x000FE378
		public override TReturn Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0010018D File Offset: 0x000FE38D
		public override TReturn Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x001001A2 File Offset: 0x000FE3A2
		public override TReturn Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x001001B7 File Offset: 0x000FE3B7
		public override TReturn Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x001001CC File Offset: 0x000FE3CC
		public override TReturn Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x001001E1 File Offset: 0x000FE3E1
		public override TReturn Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x001001F6 File Offset: 0x000FE3F6
		public override TReturn Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0010020B File Offset: 0x000FE40B
		public override TReturn Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x00100220 File Offset: 0x000FE420
		public override TReturn Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x00100235 File Offset: 0x000FE435
		public override TReturn Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0010024A File Offset: 0x000FE44A
		public override TReturn Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0010025F File Offset: 0x000FE45F
		public override TReturn Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x00100274 File Offset: 0x000FE474
		public override TReturn Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x00100289 File Offset: 0x000FE489
		public override TReturn Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x0010029E File Offset: 0x000FE49E
		public override TReturn Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x001002B3 File Offset: 0x000FE4B3
		public override TReturn Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x001002C8 File Offset: 0x000FE4C8
		public override TReturn Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x001002DD File Offset: 0x000FE4DD
		public override TReturn Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x001002F2 File Offset: 0x000FE4F2
		public override TReturn Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x00100307 File Offset: 0x000FE507
		public override TReturn Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0010031C File Offset: 0x000FE51C
		public override TReturn Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x00100331 File Offset: 0x000FE531
		public override TReturn Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x00100346 File Offset: 0x000FE546
		public override TReturn Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x0010035B File Offset: 0x000FE55B
		public override TReturn Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x00100370 File Offset: 0x000FE570
		public override TReturn Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x00100385 File Offset: 0x000FE585
		public override TReturn Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x0010039A File Offset: 0x000FE59A
		public override TReturn Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x001003AF File Offset: 0x000FE5AF
		public override TReturn Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x001003C4 File Offset: 0x000FE5C4
		public override TReturn Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x001003D9 File Offset: 0x000FE5D9
		public override TReturn Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x001003EE File Offset: 0x000FE5EE
		public override TReturn Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x00100403 File Offset: 0x000FE603
		public override TReturn Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x00100418 File Offset: 0x000FE618
		public override TReturn Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x0010042D File Offset: 0x000FE62D
		public override TReturn Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x00100442 File Offset: 0x000FE642
		public override TReturn Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x00100457 File Offset: 0x000FE657
		public override TReturn Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x0010046C File Offset: 0x000FE66C
		public override TReturn Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x00100481 File Offset: 0x000FE681
		public override TReturn Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x00100496 File Offset: 0x000FE696
		public override TReturn Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x001004AB File Offset: 0x000FE6AB
		public override TReturn Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x001004C0 File Offset: 0x000FE6C0
		public override TReturn Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x001004D5 File Offset: 0x000FE6D5
		public override TReturn Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}
	}
}
