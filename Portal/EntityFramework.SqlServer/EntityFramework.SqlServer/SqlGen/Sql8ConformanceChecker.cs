using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000033 RID: 51
	internal class Sql8ConformanceChecker : DbExpressionVisitor<bool>
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x00011814 File Offset: 0x0000FA14
		internal static bool NeedsRewrite(DbExpression expr)
		{
			Sql8ConformanceChecker sql8ConformanceChecker = new Sql8ConformanceChecker();
			return expr.Accept<bool>(sql8ConformanceChecker);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001182E File Offset: 0x0000FA2E
		private Sql8ConformanceChecker()
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00011836 File Offset: 0x0000FA36
		private bool VisitUnaryExpression(DbUnaryExpression expr)
		{
			return this.VisitExpression(expr.Argument);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00011844 File Offset: 0x0000FA44
		private bool VisitBinaryExpression(DbBinaryExpression expr)
		{
			bool flag = this.VisitExpression(expr.Left);
			bool flag2 = this.VisitExpression(expr.Right);
			return flag || flag2;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001186C File Offset: 0x0000FA6C
		private bool VisitAggregate(DbAggregate aggregate)
		{
			return this.VisitExpressionList(aggregate.Arguments);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001187A File Offset: 0x0000FA7A
		private bool VisitExpressionBinding(DbExpressionBinding expressionBinding)
		{
			return this.VisitExpression(expressionBinding.Expression);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00011888 File Offset: 0x0000FA88
		private bool VisitExpression(DbExpression expression)
		{
			return expression != null && expression.Accept<bool>(this);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00011896 File Offset: 0x0000FA96
		private bool VisitSortClause(DbSortClause sortClause)
		{
			return this.VisitExpression(sortClause.Expression);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000118A4 File Offset: 0x0000FAA4
		private static bool VisitList<TElementType>(Sql8ConformanceChecker.ListElementHandler<TElementType> handler, IList<TElementType> list)
		{
			bool flag = false;
			foreach (TElementType telementType in list)
			{
				bool flag2 = handler(telementType);
				flag = flag || flag2;
			}
			return flag;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000118F4 File Offset: 0x0000FAF4
		private bool VisitAggregateList(IList<DbAggregate> list)
		{
			return Sql8ConformanceChecker.VisitList<DbAggregate>(new Sql8ConformanceChecker.ListElementHandler<DbAggregate>(this.VisitAggregate), list);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00011908 File Offset: 0x0000FB08
		private bool VisitExpressionBindingList(IList<DbExpressionBinding> list)
		{
			return Sql8ConformanceChecker.VisitList<DbExpressionBinding>(new Sql8ConformanceChecker.ListElementHandler<DbExpressionBinding>(this.VisitExpressionBinding), list);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001191C File Offset: 0x0000FB1C
		private bool VisitExpressionList(IList<DbExpression> list)
		{
			return Sql8ConformanceChecker.VisitList<DbExpression>(new Sql8ConformanceChecker.ListElementHandler<DbExpression>(this.VisitExpression), list);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011930 File Offset: 0x0000FB30
		private bool VisitSortClauseList(IList<DbSortClause> list)
		{
			return Sql8ConformanceChecker.VisitList<DbSortClause>(new Sql8ConformanceChecker.ListElementHandler<DbSortClause>(this.VisitSortClause), list);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00011944 File Offset: 0x0000FB44
		public override bool Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00011967 File Offset: 0x0000FB67
		public override bool Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001197C File Offset: 0x0000FB7C
		public override bool Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			throw new NotSupportedException(Strings.SqlGen_ApplyNotSupportedOnSql8);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00011994 File Offset: 0x0000FB94
		public override bool Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public override bool Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			bool flag = this.VisitExpressionList(expression.When);
			bool flag2 = this.VisitExpressionList(expression.Then);
			bool flag3 = this.VisitExpression(expression.Else);
			return flag || flag2 || flag3;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000119F3 File Offset: 0x0000FBF3
		public override bool Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00011A08 File Offset: 0x0000FC08
		public override bool Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00011A1D File Offset: 0x0000FC1D
		public override bool Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
			return false;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public override bool Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			return this.VisitExpressionBindingList(expression.Inputs);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011A46 File Offset: 0x0000FC46
		public override bool Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00011A5B File Offset: 0x0000FC5B
		public override bool Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00011A70 File Offset: 0x0000FC70
		public override bool Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00011A85 File Offset: 0x0000FC85
		public override bool Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011A9A File Offset: 0x0000FC9A
		public override bool Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
			return true;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		public override bool Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Predicate);
			return flag || flag2;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00011AF8 File Offset: 0x0000FCF8
		public override bool Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00011B14 File Offset: 0x0000FD14
		public override bool Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			bool flag = this.VisitExpressionList(expression.Arguments);
			bool flag2 = this.VisitExpression(expression.Lambda.Body);
			return flag || flag2;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00011B50 File Offset: 0x0000FD50
		public override bool Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
			bool flag = this.VisitExpression(expression.Input.Expression);
			bool flag2 = this.VisitExpressionList(expression.Keys);
			bool flag3 = this.VisitAggregateList(expression.Aggregates);
			return flag || flag2 || flag3;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00011B98 File Offset: 0x0000FD98
		public override bool Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
			return true;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00011BC1 File Offset: 0x0000FDC1
		public override bool Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00011BD6 File Offset: 0x0000FDD6
		public override bool Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00011BEB File Offset: 0x0000FDEB
		public override bool Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011C00 File Offset: 0x0000FE00
		public override bool Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Left);
			bool flag2 = this.VisitExpressionBinding(expression.Right);
			bool flag3 = this.VisitExpression(expression.JoinCondition);
			return flag || flag2 || flag3;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011C44 File Offset: 0x0000FE44
		public override bool Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			bool flag = this.VisitExpression(expression.Argument);
			bool flag2 = this.VisitExpression(expression.Pattern);
			bool flag3 = this.VisitExpression(expression.Escape);
			return flag || flag2 || flag3;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011C87 File Offset: 0x0000FE87
		public override bool Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			if (expression.Limit is DbParameterReferenceExpression)
			{
				throw new NotSupportedException(Strings.SqlGen_ParameterForLimitNotSupportedOnSql8);
			}
			return this.VisitExpression(expression.Argument);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011CB9 File Offset: 0x0000FEB9
		public override bool Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011CD3 File Offset: 0x0000FED3
		public override bool Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		public override bool Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
			return false;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		public override bool Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00011D0C File Offset: 0x0000FF0C
		public override bool Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00011D21 File Offset: 0x0000FF21
		public override bool Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			return this.VisitExpression(expression.Item) || this.VisitExpressionList(expression.List);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011D4B File Offset: 0x0000FF4B
		public override bool Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			return false;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public override bool Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Projection);
			return flag || flag2;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00011D90 File Offset: 0x0000FF90
		public override bool Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			return this.VisitExpression(expression.Instance);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00011DAC File Offset: 0x0000FFAC
		public override bool Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Predicate);
			return flag || flag2;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public override bool Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00011DF5 File Offset: 0x0000FFF5
		public override bool Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00011E0A File Offset: 0x0001000A
		public override bool Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			return this.VisitExpression(expression.NavigationSource);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00011E24 File Offset: 0x00010024
		public override bool Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
			return false;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00011E34 File Offset: 0x00010034
		public override bool Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			if (expression.Count is DbParameterReferenceExpression)
			{
				throw new NotSupportedException(Strings.SqlGen_ParameterForSkipNotSupportedOnSql8);
			}
			this.VisitExpressionBinding(expression.Input);
			this.VisitSortClauseList(expression.SortOrder);
			this.VisitExpression(expression.Count);
			return true;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00011E90 File Offset: 0x00010090
		public override bool Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitSortClauseList(expression.SortOrder);
			return flag || flag2;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00011EC4 File Offset: 0x000100C4
		public override bool Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00011ED9 File Offset: 0x000100D9
		public override bool Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00011EEE File Offset: 0x000100EE
		public override bool Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
			return false;
		}

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x060006FD RID: 1789
		private delegate bool ListElementHandler<TElementType>(TElementType element);
	}
}
