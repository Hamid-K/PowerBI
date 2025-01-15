using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002AD RID: 685
	public abstract class QueryExpressionRewriter : QueryExpressionVisitor<QueryExpression>
	{
		// Token: 0x0600157F RID: 5503 RVA: 0x00026DD2 File Offset: 0x00024FD2
		public QueryExpression Rewrite(QueryExpression expression)
		{
			return expression.Accept<QueryExpression>(this);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00026DDC File Offset: 0x00024FDC
		public QueryExpressionContainer RewriteContainer(QueryExpressionContainer expressionContainer)
		{
			QueryExpression expression = expressionContainer.Expression;
			QueryExpression queryExpression = expression.Accept<QueryExpression>(this);
			if (expression == queryExpression)
			{
				return expressionContainer;
			}
			return new QueryExpressionContainer(queryExpression, expressionContainer.Name, expressionContainer.NativeReferenceName);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00026E0E File Offset: 0x0002500E
		protected internal override QueryExpression Visit(QuerySourceRefExpression expression)
		{
			return expression;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00026E14 File Offset: 0x00025014
		protected internal override QueryExpression Visit(QueryColumnExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Column(expression.Property));
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00026E54 File Offset: 0x00025054
		protected internal override QueryExpression Visit(QueryMeasureExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Measure(expression.Property));
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00026E94 File Offset: 0x00025094
		protected internal override QueryExpression Visit(QueryHierarchyExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Hierarchy(expression.Hierarchy));
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00026ED4 File Offset: 0x000250D4
		protected internal override QueryExpression Visit(QueryHierarchyLevelExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.HierarchyLevel(expression.Level));
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00026F14 File Offset: 0x00025114
		protected internal override QueryExpression Visit(QueryPropertyVariationSourceExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.VariationSource(expression.Name, expression.Property));
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00026F51 File Offset: 0x00025151
		protected internal override QueryExpression Visit(QueryNotExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Not());
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00026F7F File Offset: 0x0002517F
		protected internal override QueryExpression Visit(QueryAndExpression expression)
		{
			return this.RewriteBinary(expression, new Func<QueryExpression, QueryExpression, QueryExpression>(SemanticQueryExpressionBuilder.And));
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00026F94 File Offset: 0x00025194
		protected internal override QueryExpression Visit(QueryOrExpression expression)
		{
			return this.RewriteBinary(expression, new Func<QueryExpression, QueryExpression, QueryExpression>(SemanticQueryExpressionBuilder.Or));
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00026FAC File Offset: 0x000251AC
		protected internal override QueryExpression Visit(QueryAggregationExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Aggregate(expression.Function));
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00026FEC File Offset: 0x000251EC
		protected internal override QueryExpression Visit(QueryArithmeticExpression expression)
		{
			return this.RewriteBinary(expression, (QueryExpression left, QueryExpression right) => left.Arithmetic(right, expression.Operator));
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00027020 File Offset: 0x00025220
		protected internal override QueryExpression Visit(QueryBetweenExpression expression)
		{
			QueryExpression expression2 = expression.Expression.Expression;
			QueryExpression queryExpression = this.Rewrite(expression2);
			QueryExpression expression3 = expression.LowerBound.Expression;
			QueryExpression queryExpression2 = this.Rewrite(expression3);
			QueryExpression expression4 = expression.UpperBound.Expression;
			QueryExpression queryExpression3 = this.Rewrite(expression4);
			if (expression2 == queryExpression && expression3 == queryExpression2 && expression4 == queryExpression3)
			{
				return expression;
			}
			return queryExpression.Between(queryExpression2, queryExpression3);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00027088 File Offset: 0x00025288
		protected internal override QueryExpression Visit(QueryComparisonExpression expression)
		{
			return this.RewriteBinary(expression, (QueryExpression left, QueryExpression right) => left.Comparison(right, expression.ComparisonKind));
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000270BA File Offset: 0x000252BA
		protected internal override QueryExpression Visit(QueryContainsExpression expression)
		{
			return this.RewriteBinary(expression, new Func<QueryExpression, QueryExpression, QueryExpression>(SemanticQueryExpressionBuilder.Contains));
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000270D0 File Offset: 0x000252D0
		protected internal override QueryExpression Visit(QueryDateAddExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.DateAdd(expression.Amount, expression.TimeUnit));
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00027110 File Offset: 0x00025310
		protected internal override QueryExpression Visit(QueryDateSpanExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.DateSpan(expression.TimeUnit));
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0002714D File Offset: 0x0002534D
		protected internal override QueryExpression Visit(QueryExistsExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, new Func<QueryExpression, QueryExpression>(SemanticQueryExpressionBuilder.Exists));
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00027168 File Offset: 0x00025368
		protected internal override QueryExpression Visit(QueryFloorExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Floor(expression.Size, expression.TimeUnit));
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x000271A8 File Offset: 0x000253A8
		protected internal override QueryExpression Visit(QueryDiscretizeExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Discretize(expression.Count));
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x000271E8 File Offset: 0x000253E8
		protected internal override QueryExpression Visit(QuerySparklineDataExpression expression)
		{
			QueryExpression expression2 = expression.Measure.Expression;
			QueryExpression queryExpression = this.Rewrite(expression2);
			List<QueryExpressionContainer> groupings = expression.Groupings;
			List<QueryExpressionContainer> list = expression.Groupings.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteContainer));
			QueryExpression queryExpression2 = null;
			QueryExpression queryExpression3 = null;
			if (expression.ScalarKey != null)
			{
				queryExpression2 = expression.ScalarKey.Expression;
				queryExpression3 = this.Rewrite(queryExpression2);
			}
			if (expression2 == queryExpression && groupings == list && queryExpression2 == queryExpression3)
			{
				return expression;
			}
			return queryExpression.SparklineData(list, expression.PointsPerSparkline, expression.IncludeMinGroupingInterval, queryExpression3);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00027278 File Offset: 0x00025478
		protected internal override QueryExpression Visit(QueryMemberExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Member(expression.Member));
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x000272B8 File Offset: 0x000254B8
		protected internal override QueryExpression Visit(QueryNativeFormatExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.NativeFormat(expression.FormatString));
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000272F5 File Offset: 0x000254F5
		protected internal override QueryExpression Visit(QueryNativeMeasureExpression expression)
		{
			return expression;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000272F8 File Offset: 0x000254F8
		protected internal override QueryExpression Visit(QueryPercentileExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Percentile(expression.Exclusive, expression.K));
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00027338 File Offset: 0x00025538
		protected internal override QueryExpression Visit(QueryMinExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Min(expression.IncludeAllTypes));
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00027378 File Offset: 0x00025578
		protected internal override QueryExpression Visit(QueryMaxExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.Max(expression.IncludeAllTypes));
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x000273B5 File Offset: 0x000255B5
		protected internal override QueryExpression Visit(QueryStartsWithExpression expression)
		{
			return this.RewriteBinary(expression, new Func<QueryExpression, QueryExpression, QueryExpression>(SemanticQueryExpressionBuilder.StartsWith));
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x000273CA File Offset: 0x000255CA
		protected internal override QueryExpression Visit(QueryEndsWithExpression expression)
		{
			return this.RewriteBinary(expression, new Func<QueryExpression, QueryExpression, QueryExpression>(SemanticQueryExpressionBuilder.EndsWith));
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x000273E0 File Offset: 0x000255E0
		protected internal override QueryExpression Visit(QueryInExpression expression)
		{
			List<QueryExpressionContainer> list = expression.Expressions.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteContainer));
			if (expression.HasValues)
			{
				List<List<QueryExpressionContainer>> values = expression.Values;
				List<List<QueryExpressionContainer>> list2 = null;
				for (int i = 0; i < values.Count; i++)
				{
					List<QueryExpressionContainer> list3 = values[i];
					List<QueryExpressionContainer> list4 = list3.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteContainer));
					if (list3 != list4)
					{
						if (list2 == null)
						{
							list2 = new List<List<QueryExpressionContainer>>(values);
						}
						list2[i] = list4;
					}
				}
				if (list == expression.Expressions && list2 == null)
				{
					return expression;
				}
				return list.In(list2 ?? values, expression.EqualityKind);
			}
			else
			{
				QueryExpression queryExpression = this.Rewrite(expression.Table.Expression);
				if (list == expression.Expressions && queryExpression == expression.Table)
				{
					return expression;
				}
				return list.In(queryExpression);
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x000274AC File Offset: 0x000256AC
		protected internal override QueryExpression Visit(QueryScopedEvalExpression expression)
		{
			QueryExpression expression2 = expression.Expression.Expression;
			QueryExpression queryExpression = this.Rewrite(expression2);
			List<QueryExpressionContainer> list = expression.Scope.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteContainer));
			if (queryExpression == expression2 && list == expression.Scope)
			{
				return expression;
			}
			return queryExpression.ScopedEval(list);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000274FC File Offset: 0x000256FC
		protected internal override QueryExpression Visit(QueryFilteredEvalExpression expression)
		{
			QueryExpression expression2 = expression.Expression.Expression;
			QueryExpression queryExpression = this.Rewrite(expression2);
			List<QueryFilter> list = expression.Filters.Rewrite((QueryFilter f) => QueryDefinitionRewriter.RewriteFilter(f, this));
			if (queryExpression == expression2 && list == expression.Filters)
			{
				return expression;
			}
			return queryExpression.FilteredEval(list);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0002754B File Offset: 0x0002574B
		protected internal override QueryExpression Visit(QueryLiteralExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0002754E File Offset: 0x0002574E
		protected internal override QueryExpression Visit(QueryNowExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00027551 File Offset: 0x00025751
		protected internal override QueryExpression Visit(QueryDefaultValueExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00027554 File Offset: 0x00025754
		protected internal override QueryExpression Visit(QueryAnyValueExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00027557 File Offset: 0x00025757
		protected internal override QueryExpression Visit(QueryTransformOutputRoleRefExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0002755A File Offset: 0x0002575A
		protected internal override QueryExpression Visit(QueryTransformTableRefExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0002755D File Offset: 0x0002575D
		protected internal override QueryExpression Visit(QueryLetRefExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00027560 File Offset: 0x00025760
		protected internal override QueryExpression Visit(QueryRoleRefExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00027563 File Offset: 0x00025763
		protected internal override QueryExpression Visit(QuerySummaryValueRefExpression expression)
		{
			return expression;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00027566 File Offset: 0x00025766
		protected internal override QueryExpression Visit(QueryParameterRefExpression expression)
		{
			return expression;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00027569 File Offset: 0x00025769
		protected internal override QueryExpression Visit(QueryPrimitiveTypeExpression expression)
		{
			return expression;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0002756C File Offset: 0x0002576C
		protected internal override QueryExpression Visit(QueryTypeOfExpression expression)
		{
			return this.RewriteUnary(expression, expression.Expression, (QueryExpression inner) => inner.TypeOf());
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0002759C File Offset: 0x0002579C
		protected internal override QueryExpression Visit(QueryTableTypeExpression expression)
		{
			List<QueryExpressionContainer> list = expression.Columns.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteContainer));
			if (list == expression.Columns)
			{
				return expression;
			}
			return list.TableType();
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000275D2 File Offset: 0x000257D2
		protected internal override QueryExpression Visit(QueryNativeVisualCalculationExpression expression)
		{
			return expression;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x000275D5 File Offset: 0x000257D5
		protected internal override QueryExpression Visit(QueryPropertyExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000275DD File Offset: 0x000257DD
		protected internal override QueryExpression Visit(QueryDatePartExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000275E5 File Offset: 0x000257E5
		protected internal override QueryExpression Visit(QueryBooleanConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000275ED File Offset: 0x000257ED
		protected internal override QueryExpression Visit(QueryDateConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000275F5 File Offset: 0x000257F5
		protected internal override QueryExpression Visit(QueryDateTimeConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000275FD File Offset: 0x000257FD
		protected internal override QueryExpression Visit(QueryDateTimeSecondConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00027605 File Offset: 0x00025805
		protected internal override QueryExpression Visit(QueryDecadeConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0002760D File Offset: 0x0002580D
		protected internal override QueryExpression Visit(QueryDecimalConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00027615 File Offset: 0x00025815
		protected internal override QueryExpression Visit(QueryIntegerConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0002761D File Offset: 0x0002581D
		protected internal override QueryExpression Visit(QueryNullConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00027625 File Offset: 0x00025825
		protected internal override QueryExpression Visit(QueryStringConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0002762D File Offset: 0x0002582D
		protected internal override QueryExpression Visit(QueryNumberConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00027635 File Offset: 0x00025835
		protected internal override QueryExpression Visit(QueryYearAndMonthConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0002763D File Offset: 0x0002583D
		protected internal override QueryExpression Visit(QueryYearAndWeekConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00027645 File Offset: 0x00025845
		protected internal override QueryExpression Visit(QueryYearConstantExpression expression)
		{
			throw QueryExpressionRewriter.CreateDeprecatedExpressionException(expression);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00027650 File Offset: 0x00025850
		private QueryExpression RewriteUnary(QueryExpression outerExpression, QueryExpressionContainer innerExpressionContainer, Func<QueryExpression, QueryExpression> createOuterExpression)
		{
			QueryExpression expression = innerExpressionContainer.Expression;
			QueryExpression queryExpression = this.Rewrite(expression);
			if (expression == queryExpression)
			{
				return outerExpression;
			}
			return createOuterExpression(queryExpression);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0002767C File Offset: 0x0002587C
		private QueryExpression RewriteBinary(QueryBinaryExpression expression, Func<QueryExpression, QueryExpression, QueryExpression> createBinaryExpression)
		{
			QueryExpression expression2 = expression.Left.Expression;
			QueryExpression queryExpression = this.Rewrite(expression2);
			QueryExpression expression3 = expression.Right.Expression;
			QueryExpression queryExpression2 = this.Rewrite(expression3);
			if (expression2 == queryExpression && expression3 == queryExpression2)
			{
				return expression;
			}
			return createBinaryExpression(queryExpression, queryExpression2);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000276C3 File Offset: 0x000258C3
		private static Exception CreateDeprecatedExpressionException(QueryExpression expression)
		{
			return new NotSupportedException(StringUtil.FormatInvariant("Expression of type {0} encountered during rewrite. This type is deprecated", expression.GetType().Name));
		}
	}
}
