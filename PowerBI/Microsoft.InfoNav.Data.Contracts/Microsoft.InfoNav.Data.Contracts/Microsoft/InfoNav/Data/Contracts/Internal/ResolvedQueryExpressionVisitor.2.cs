using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000233 RID: 563
	public abstract class ResolvedQueryExpressionVisitor<T>
	{
		// Token: 0x06001094 RID: 4244
		public abstract T Visit(ResolvedQuerySourceRefExpression expression);

		// Token: 0x06001095 RID: 4245
		public abstract T Visit(ResolvedQueryExpressionSourceRefExpression expression);

		// Token: 0x06001096 RID: 4246
		public abstract T Visit(ResolvedQuerySubqueryExpression expression);

		// Token: 0x06001097 RID: 4247
		public abstract T Visit(ResolvedQueryColumnExpression expression);

		// Token: 0x06001098 RID: 4248
		public abstract T Visit(ResolvedQueryMeasureExpression expression);

		// Token: 0x06001099 RID: 4249
		public abstract T Visit(ResolvedQueryHierarchyExpression expression);

		// Token: 0x0600109A RID: 4250
		public abstract T Visit(ResolvedQueryHierarchyLevelExpression expression);

		// Token: 0x0600109B RID: 4251
		public abstract T Visit(ResolvedQueryPropertyVariationSourceExpression expression);

		// Token: 0x0600109C RID: 4252
		public abstract T Visit(ResolvedQueryColumnReferenceExpression expression);

		// Token: 0x0600109D RID: 4253
		public abstract T Visit(ResolvedQueryNotExpression expression);

		// Token: 0x0600109E RID: 4254
		public abstract T Visit(ResolvedQueryAndExpression expression);

		// Token: 0x0600109F RID: 4255
		public abstract T Visit(ResolvedQueryOrExpression expression);

		// Token: 0x060010A0 RID: 4256
		public abstract T Visit(ResolvedQueryAggregationExpression expression);

		// Token: 0x060010A1 RID: 4257
		public abstract T Visit(ResolvedQueryArithmeticExpression expression);

		// Token: 0x060010A2 RID: 4258
		public abstract T Visit(ResolvedQueryBetweenExpression expression);

		// Token: 0x060010A3 RID: 4259
		public abstract T Visit(ResolvedQueryComparisonExpression expression);

		// Token: 0x060010A4 RID: 4260
		public abstract T Visit(ResolvedQueryContainsExpression expression);

		// Token: 0x060010A5 RID: 4261
		public abstract T Visit(ResolvedQueryDateAddExpression expression);

		// Token: 0x060010A6 RID: 4262
		public abstract T Visit(ResolvedQueryDateSpanExpression expression);

		// Token: 0x060010A7 RID: 4263
		public abstract T Visit(ResolvedQueryExistsExpression expression);

		// Token: 0x060010A8 RID: 4264
		public abstract T Visit(ResolvedQueryFloorExpression expression);

		// Token: 0x060010A9 RID: 4265
		public abstract T Visit(ResolvedQueryInExpression expression);

		// Token: 0x060010AA RID: 4266
		public abstract T Visit(ResolvedQueryScopedEvalExpression expression);

		// Token: 0x060010AB RID: 4267
		public abstract T Visit(ResolvedQueryFilteredEvalExpression expression);

		// Token: 0x060010AC RID: 4268
		public abstract T Visit(ResolvedQueryLiteralExpression expression);

		// Token: 0x060010AD RID: 4269
		public abstract T Visit(ResolvedQueryNowExpression expression);

		// Token: 0x060010AE RID: 4270
		public abstract T Visit(ResolvedQueryPercentileExpression expression);

		// Token: 0x060010AF RID: 4271
		public abstract T Visit(ResolvedQueryMinExpression expression);

		// Token: 0x060010B0 RID: 4272
		public abstract T Visit(ResolvedQueryMaxExpression expression);

		// Token: 0x060010B1 RID: 4273
		public abstract T Visit(ResolvedQueryStartsWithExpression expression);

		// Token: 0x060010B2 RID: 4274
		public abstract T Visit(ResolvedQueryEndsWithExpression expression);

		// Token: 0x060010B3 RID: 4275
		public abstract T Visit(ResolvedQueryDefaultValueExpression expression);

		// Token: 0x060010B4 RID: 4276
		public abstract T Visit(ResolvedQueryAnyValueExpression expression);

		// Token: 0x060010B5 RID: 4277
		public abstract T Visit(ResolvedQueryTransformOutputRoleRefExpression expression);

		// Token: 0x060010B6 RID: 4278
		public abstract T Visit(ResolvedQueryTransformTableColumnExpression expression);

		// Token: 0x060010B7 RID: 4279
		public abstract T Visit(ResolvedQueryDiscretizeExpression expression);

		// Token: 0x060010B8 RID: 4280
		public abstract T Visit(ResolvedQueryMemberExpression expression);

		// Token: 0x060010B9 RID: 4281
		public abstract T Visit(ResolvedQueryNativeFormatExpression expression);

		// Token: 0x060010BA RID: 4282
		public abstract T Visit(ResolvedQueryNativeMeasureExpression expression);

		// Token: 0x060010BB RID: 4283
		public abstract T Visit(ResolvedQueryLetRefExpression expression);

		// Token: 0x060010BC RID: 4284
		public abstract T Visit(ResolvedQueryRoleRefExpression expression);

		// Token: 0x060010BD RID: 4285
		public abstract T Visit(ResolvedSummaryValueRefExpression expression);

		// Token: 0x060010BE RID: 4286
		public abstract T Visit(ResolvedQueryTypeOfExpression expression);

		// Token: 0x060010BF RID: 4287
		public abstract T Visit(ResolvedQueryTableTypeExpression expression);

		// Token: 0x060010C0 RID: 4288
		public abstract T Visit(ResolvedQueryPrimitiveTypeExpression expression);

		// Token: 0x060010C1 RID: 4289
		public abstract T Visit(ResolvedQueryParameterRefExpression expression);

		// Token: 0x060010C2 RID: 4290
		public abstract T Visit(ResolvedQuerySparklineDataExpression expression);

		// Token: 0x060010C3 RID: 4291
		public abstract T Visit(ResolvedQueryNativeVisualCalculationExpression expression);
	}
}
