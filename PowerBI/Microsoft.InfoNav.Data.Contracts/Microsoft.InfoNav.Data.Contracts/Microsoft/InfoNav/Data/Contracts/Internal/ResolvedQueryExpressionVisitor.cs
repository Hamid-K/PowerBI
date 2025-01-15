using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000232 RID: 562
	public abstract class ResolvedQueryExpressionVisitor
	{
		// Token: 0x06001063 RID: 4195
		public abstract void Visit(ResolvedQuerySourceRefExpression expression);

		// Token: 0x06001064 RID: 4196
		public abstract void Visit(ResolvedQueryExpressionSourceRefExpression expression);

		// Token: 0x06001065 RID: 4197
		public abstract void Visit(ResolvedQuerySubqueryExpression expression);

		// Token: 0x06001066 RID: 4198
		public abstract void Visit(ResolvedQueryColumnExpression expression);

		// Token: 0x06001067 RID: 4199
		public abstract void Visit(ResolvedQueryMeasureExpression expression);

		// Token: 0x06001068 RID: 4200
		public abstract void Visit(ResolvedQueryHierarchyExpression expression);

		// Token: 0x06001069 RID: 4201
		public abstract void Visit(ResolvedQueryHierarchyLevelExpression expression);

		// Token: 0x0600106A RID: 4202
		public abstract void Visit(ResolvedQueryPropertyVariationSourceExpression expression);

		// Token: 0x0600106B RID: 4203
		public abstract void Visit(ResolvedQueryColumnReferenceExpression expression);

		// Token: 0x0600106C RID: 4204
		public abstract void Visit(ResolvedQueryNotExpression expression);

		// Token: 0x0600106D RID: 4205
		public abstract void Visit(ResolvedQueryAndExpression expression);

		// Token: 0x0600106E RID: 4206
		public abstract void Visit(ResolvedQueryOrExpression expression);

		// Token: 0x0600106F RID: 4207
		public abstract void Visit(ResolvedQueryAggregationExpression expression);

		// Token: 0x06001070 RID: 4208
		public abstract void Visit(ResolvedQueryArithmeticExpression expression);

		// Token: 0x06001071 RID: 4209
		public abstract void Visit(ResolvedQueryBetweenExpression expression);

		// Token: 0x06001072 RID: 4210
		public abstract void Visit(ResolvedQueryComparisonExpression expression);

		// Token: 0x06001073 RID: 4211
		public abstract void Visit(ResolvedQueryContainsExpression expression);

		// Token: 0x06001074 RID: 4212
		public abstract void Visit(ResolvedQueryDateAddExpression expression);

		// Token: 0x06001075 RID: 4213
		public abstract void Visit(ResolvedQueryDateSpanExpression expression);

		// Token: 0x06001076 RID: 4214
		public abstract void Visit(ResolvedQueryExistsExpression expression);

		// Token: 0x06001077 RID: 4215
		public abstract void Visit(ResolvedQueryFloorExpression expression);

		// Token: 0x06001078 RID: 4216
		public abstract void Visit(ResolvedQueryInExpression expression);

		// Token: 0x06001079 RID: 4217
		public abstract void Visit(ResolvedQueryScopedEvalExpression expression);

		// Token: 0x0600107A RID: 4218
		public abstract void Visit(ResolvedQueryFilteredEvalExpression expression);

		// Token: 0x0600107B RID: 4219
		public abstract void Visit(ResolvedQueryLiteralExpression expression);

		// Token: 0x0600107C RID: 4220
		public abstract void Visit(ResolvedQueryNowExpression expression);

		// Token: 0x0600107D RID: 4221
		public abstract void Visit(ResolvedQueryPercentileExpression expression);

		// Token: 0x0600107E RID: 4222
		public abstract void Visit(ResolvedQueryMinExpression expression);

		// Token: 0x0600107F RID: 4223
		public abstract void Visit(ResolvedQueryMaxExpression expression);

		// Token: 0x06001080 RID: 4224
		public abstract void Visit(ResolvedQueryStartsWithExpression expression);

		// Token: 0x06001081 RID: 4225
		public abstract void Visit(ResolvedQueryEndsWithExpression expression);

		// Token: 0x06001082 RID: 4226
		public abstract void Visit(ResolvedQueryDefaultValueExpression expression);

		// Token: 0x06001083 RID: 4227
		public abstract void Visit(ResolvedQueryAnyValueExpression expression);

		// Token: 0x06001084 RID: 4228
		public abstract void Visit(ResolvedQueryTransformOutputRoleRefExpression expression);

		// Token: 0x06001085 RID: 4229
		public abstract void Visit(ResolvedQueryTransformTableColumnExpression expression);

		// Token: 0x06001086 RID: 4230
		public abstract void Visit(ResolvedQueryDiscretizeExpression expression);

		// Token: 0x06001087 RID: 4231
		public abstract void Visit(ResolvedQueryMemberExpression expression);

		// Token: 0x06001088 RID: 4232
		public abstract void Visit(ResolvedQueryNativeFormatExpression expression);

		// Token: 0x06001089 RID: 4233
		public abstract void Visit(ResolvedQueryNativeMeasureExpression expression);

		// Token: 0x0600108A RID: 4234
		public abstract void Visit(ResolvedQueryLetRefExpression expression);

		// Token: 0x0600108B RID: 4235
		public abstract void Visit(ResolvedQueryRoleRefExpression expression);

		// Token: 0x0600108C RID: 4236
		public abstract void Visit(ResolvedSummaryValueRefExpression expression);

		// Token: 0x0600108D RID: 4237
		public abstract void Visit(ResolvedQueryTypeOfExpression expression);

		// Token: 0x0600108E RID: 4238
		public abstract void Visit(ResolvedQueryTableTypeExpression expression);

		// Token: 0x0600108F RID: 4239
		public abstract void Visit(ResolvedQueryPrimitiveTypeExpression expression);

		// Token: 0x06001090 RID: 4240
		public abstract void Visit(ResolvedQueryParameterRefExpression expression);

		// Token: 0x06001091 RID: 4241
		public abstract void Visit(ResolvedQuerySparklineDataExpression expression);

		// Token: 0x06001092 RID: 4242
		public abstract void Visit(ResolvedQueryNativeVisualCalculationExpression expression);
	}
}
