using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000235 RID: 565
	public class DefaultResolvedQueryExpressionVisitor<T> : ResolvedQueryExpressionVisitor<T>
	{
		// Token: 0x060010FA RID: 4346 RVA: 0x0001F2D9 File Offset: 0x0001D4D9
		public override T Visit(ResolvedQuerySourceRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0001F2E2 File Offset: 0x0001D4E2
		public override T Visit(ResolvedQueryExpressionSourceRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0001F2EB File Offset: 0x0001D4EB
		public override T Visit(ResolvedQuerySubqueryExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
		public override T Visit(ResolvedQueryColumnExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0001F2FD File Offset: 0x0001D4FD
		public override T Visit(ResolvedQueryMeasureExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0001F306 File Offset: 0x0001D506
		public override T Visit(ResolvedQueryHierarchyExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0001F30F File Offset: 0x0001D50F
		public override T Visit(ResolvedQueryHierarchyLevelExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0001F318 File Offset: 0x0001D518
		public override T Visit(ResolvedQueryPropertyVariationSourceExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0001F321 File Offset: 0x0001D521
		public override T Visit(ResolvedQueryColumnReferenceExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0001F32A File Offset: 0x0001D52A
		public override T Visit(ResolvedQueryNotExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0001F333 File Offset: 0x0001D533
		public override T Visit(ResolvedQueryAndExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0001F33C File Offset: 0x0001D53C
		public override T Visit(ResolvedQueryOrExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0001F345 File Offset: 0x0001D545
		public override T Visit(ResolvedQueryAggregationExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0001F34E File Offset: 0x0001D54E
		public override T Visit(ResolvedQueryArithmeticExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0001F357 File Offset: 0x0001D557
		public override T Visit(ResolvedQueryBetweenExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0001F360 File Offset: 0x0001D560
		public override T Visit(ResolvedQueryComparisonExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0001F369 File Offset: 0x0001D569
		public override T Visit(ResolvedQueryContainsExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0001F372 File Offset: 0x0001D572
		public override T Visit(ResolvedQueryDateAddExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0001F37B File Offset: 0x0001D57B
		public override T Visit(ResolvedQueryDateSpanExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0001F384 File Offset: 0x0001D584
		public override T Visit(ResolvedQueryExistsExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0001F38D File Offset: 0x0001D58D
		public override T Visit(ResolvedQueryFloorExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0001F396 File Offset: 0x0001D596
		public override T Visit(ResolvedQueryInExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0001F39F File Offset: 0x0001D59F
		public override T Visit(ResolvedQueryScopedEvalExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
		public override T Visit(ResolvedQueryFilteredEvalExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0001F3B1 File Offset: 0x0001D5B1
		public override T Visit(ResolvedQueryLiteralExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0001F3BA File Offset: 0x0001D5BA
		public override T Visit(ResolvedQueryNowExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0001F3C3 File Offset: 0x0001D5C3
		public override T Visit(ResolvedQueryPercentileExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0001F3CC File Offset: 0x0001D5CC
		public override T Visit(ResolvedQueryMinExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0001F3D5 File Offset: 0x0001D5D5
		public override T Visit(ResolvedQueryMaxExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0001F3DE File Offset: 0x0001D5DE
		public override T Visit(ResolvedQueryStartsWithExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0001F3E7 File Offset: 0x0001D5E7
		public override T Visit(ResolvedQueryEndsWithExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		public override T Visit(ResolvedQueryDefaultValueExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0001F3F9 File Offset: 0x0001D5F9
		public override T Visit(ResolvedQueryAnyValueExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0001F404 File Offset: 0x0001D604
		protected virtual T VisitUnhandledExpression(ResolvedQueryExpression expression)
		{
			return default(T);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0001F41A File Offset: 0x0001D61A
		protected virtual T VisitExpression(ResolvedQueryExpression expression)
		{
			return expression.Accept<T>(this);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0001F423 File Offset: 0x0001D623
		public override T Visit(ResolvedQueryTransformOutputRoleRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0001F42C File Offset: 0x0001D62C
		public override T Visit(ResolvedQueryTransformTableColumnExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0001F435 File Offset: 0x0001D635
		public override T Visit(ResolvedQueryDiscretizeExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0001F43E File Offset: 0x0001D63E
		public override T Visit(ResolvedQueryMemberExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0001F447 File Offset: 0x0001D647
		public override T Visit(ResolvedQueryNativeFormatExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0001F450 File Offset: 0x0001D650
		public override T Visit(ResolvedQueryNativeMeasureExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0001F459 File Offset: 0x0001D659
		public override T Visit(ResolvedQueryLetRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0001F462 File Offset: 0x0001D662
		public override T Visit(ResolvedQueryRoleRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0001F46B File Offset: 0x0001D66B
		public override T Visit(ResolvedSummaryValueRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0001F474 File Offset: 0x0001D674
		public override T Visit(ResolvedQueryPrimitiveTypeExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0001F47D File Offset: 0x0001D67D
		public override T Visit(ResolvedQueryTableTypeExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0001F486 File Offset: 0x0001D686
		public override T Visit(ResolvedQueryTypeOfExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0001F48F File Offset: 0x0001D68F
		public override T Visit(ResolvedQueryParameterRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0001F498 File Offset: 0x0001D698
		public override T Visit(ResolvedQuerySparklineDataExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0001F4A1 File Offset: 0x0001D6A1
		public override T Visit(ResolvedQueryNativeVisualCalculationExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}
	}
}
