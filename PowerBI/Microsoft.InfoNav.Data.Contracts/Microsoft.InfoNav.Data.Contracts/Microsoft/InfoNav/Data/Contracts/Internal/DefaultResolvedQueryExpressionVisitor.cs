using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000234 RID: 564
	public class DefaultResolvedQueryExpressionVisitor : ResolvedQueryExpressionVisitor
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
		public override void Visit(ResolvedQuerySourceRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0001EFBD File Offset: 0x0001D1BD
		public override void Visit(ResolvedQueryExpressionSourceRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0001EFC6 File Offset: 0x0001D1C6
		public override void Visit(ResolvedQuerySubqueryExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0001EFCF File Offset: 0x0001D1CF
		public override void Visit(ResolvedQueryColumnExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0001EFDD File Offset: 0x0001D1DD
		public override void Visit(ResolvedQueryMeasureExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0001EFEB File Offset: 0x0001D1EB
		public override void Visit(ResolvedQueryHierarchyExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0001EFF9 File Offset: 0x0001D1F9
		public override void Visit(ResolvedQueryHierarchyLevelExpression expression)
		{
			this.VisitExpression(expression.HierarchyExpression);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0001F007 File Offset: 0x0001D207
		public override void Visit(ResolvedQueryPropertyVariationSourceExpression expression)
		{
			this.VisitExpression(expression.SourceRefExpression);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0001F015 File Offset: 0x0001D215
		public override void Visit(ResolvedQueryColumnReferenceExpression expression)
		{
			this.VisitExpression(expression.Source);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0001F023 File Offset: 0x0001D223
		public override void Visit(ResolvedQueryNotExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0001F02C File Offset: 0x0001D22C
		public override void Visit(ResolvedQueryAndExpression expression)
		{
			this.VisitBinary(expression);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0001F035 File Offset: 0x0001D235
		public override void Visit(ResolvedQueryOrExpression expression)
		{
			this.VisitBinary(expression);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0001F03E File Offset: 0x0001D23E
		public override void Visit(ResolvedQueryAggregationExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0001F047 File Offset: 0x0001D247
		public override void Visit(ResolvedQueryArithmeticExpression expression)
		{
			this.VisitBinary(expression);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0001F050 File Offset: 0x0001D250
		public override void Visit(ResolvedQueryBetweenExpression expression)
		{
			this.VisitExpression(expression.LowerBound);
			this.VisitExpression(expression.Expression);
			this.VisitExpression(expression.UpperBound);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0001F076 File Offset: 0x0001D276
		public override void Visit(ResolvedQueryComparisonExpression expression)
		{
			this.VisitBinary(expression);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0001F07F File Offset: 0x0001D27F
		public override void Visit(ResolvedQueryContainsExpression expression)
		{
			this.VisitBinary(expression);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0001F088 File Offset: 0x0001D288
		public override void Visit(ResolvedQueryDateAddExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0001F091 File Offset: 0x0001D291
		public override void Visit(ResolvedQueryDateSpanExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0001F09A File Offset: 0x0001D29A
		public override void Visit(ResolvedQueryExistsExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0001F0A3 File Offset: 0x0001D2A3
		public override void Visit(ResolvedQueryFloorExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		public override void Visit(ResolvedQueryInExpression expression)
		{
			foreach (ResolvedQueryExpression resolvedQueryExpression in expression.Expressions)
			{
				this.VisitExpression(resolvedQueryExpression);
			}
			if (expression.HasValues)
			{
				using (IEnumerator<ResolvedQueryExpression> enumerator = expression.Values.SelectMany<ResolvedQueryExpression>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ResolvedQueryExpression resolvedQueryExpression2 = enumerator.Current;
						this.VisitExpression(resolvedQueryExpression2);
					}
					return;
				}
			}
			this.VisitExpression(expression.Table);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0001F158 File Offset: 0x0001D358
		public override void Visit(ResolvedQueryScopedEvalExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0001F161 File Offset: 0x0001D361
		public override void Visit(ResolvedQueryFilteredEvalExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0001F16A File Offset: 0x0001D36A
		public override void Visit(ResolvedQueryLiteralExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0001F173 File Offset: 0x0001D373
		public override void Visit(ResolvedQueryNowExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0001F17C File Offset: 0x0001D37C
		public override void Visit(ResolvedQueryPercentileExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0001F185 File Offset: 0x0001D385
		public override void Visit(ResolvedQueryMinExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0001F18E File Offset: 0x0001D38E
		public override void Visit(ResolvedQueryMaxExpression expression)
		{
			this.VisitUnary(expression);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0001F197 File Offset: 0x0001D397
		public override void Visit(ResolvedQueryStartsWithExpression expression)
		{
			this.VisitBinary(expression);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		public override void Visit(ResolvedQueryEndsWithExpression expression)
		{
			this.VisitBinary(expression);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0001F1A9 File Offset: 0x0001D3A9
		public override void Visit(ResolvedQueryDefaultValueExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0001F1B2 File Offset: 0x0001D3B2
		public override void Visit(ResolvedQueryAnyValueExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0001F1BB File Offset: 0x0001D3BB
		protected virtual void VisitUnhandledExpression(ResolvedQueryExpression expression)
		{
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0001F1BD File Offset: 0x0001D3BD
		protected virtual void VisitExpression(ResolvedQueryExpression expression)
		{
			expression.Accept(this);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0001F1C6 File Offset: 0x0001D3C6
		private void VisitBinary(ResolvedQueryBinaryExpression expression)
		{
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		private void VisitUnary(ResolvedQueryUnaryExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0001F1EE File Offset: 0x0001D3EE
		public override void Visit(ResolvedQueryTransformOutputRoleRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0001F1F7 File Offset: 0x0001D3F7
		public override void Visit(ResolvedQueryTransformTableColumnExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0001F200 File Offset: 0x0001D400
		public override void Visit(ResolvedQueryDiscretizeExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0001F20E File Offset: 0x0001D40E
		public override void Visit(ResolvedQueryMemberExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0001F21C File Offset: 0x0001D41C
		public override void Visit(ResolvedQueryNativeFormatExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0001F225 File Offset: 0x0001D425
		public override void Visit(ResolvedQueryNativeMeasureExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0001F22E File Offset: 0x0001D42E
		public override void Visit(ResolvedQueryLetRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0001F237 File Offset: 0x0001D437
		public override void Visit(ResolvedQueryRoleRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0001F240 File Offset: 0x0001D440
		public override void Visit(ResolvedSummaryValueRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0001F249 File Offset: 0x0001D449
		public override void Visit(ResolvedQueryPrimitiveTypeExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0001F254 File Offset: 0x0001D454
		public override void Visit(ResolvedQueryTableTypeExpression expression)
		{
			foreach (ResolvedQueryTableTypeColumn resolvedQueryTableTypeColumn in expression.Columns)
			{
				this.VisitExpression(resolvedQueryTableTypeColumn.Expression);
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		public override void Visit(ResolvedQueryTypeOfExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0001F2B6 File Offset: 0x0001D4B6
		public override void Visit(ResolvedQueryParameterRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0001F2BF File Offset: 0x0001D4BF
		public override void Visit(ResolvedQuerySparklineDataExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public override void Visit(ResolvedQueryNativeVisualCalculationExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}
	}
}
