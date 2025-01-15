using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000230 RID: 560
	public class ResolvedQueryExpressionRewriter : ResolvedQueryExpressionVisitor<ResolvedQueryExpression>
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x0001E727 File Offset: 0x0001C927
		public override ResolvedQueryExpression Visit(ResolvedQuerySourceRefExpression expression)
		{
			return expression;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0001E72A File Offset: 0x0001C92A
		public override ResolvedQueryExpression Visit(ResolvedQueryExpressionSourceRefExpression expression)
		{
			return expression;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0001E72D File Offset: 0x0001C92D
		public override ResolvedQueryExpression Visit(ResolvedQuerySubqueryExpression expression)
		{
			return expression;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0001E730 File Offset: 0x0001C930
		public override ResolvedQueryExpression Visit(ResolvedQueryColumnExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression == resolvedQueryExpression)
			{
				return expression;
			}
			return resolvedQueryExpression.Column(expression.Column);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0001E764 File Offset: 0x0001C964
		public override ResolvedQueryExpression Visit(ResolvedQueryMeasureExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression == resolvedQueryExpression)
			{
				return expression;
			}
			return resolvedQueryExpression.Measure(expression.Measure);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0001E798 File Offset: 0x0001C998
		public override ResolvedQueryExpression Visit(ResolvedQueryHierarchyExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression == resolvedQueryExpression)
			{
				return expression;
			}
			return resolvedQueryExpression.Hierarchy(expression.Hierarchy);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public override ResolvedQueryExpression Visit(ResolvedQueryHierarchyLevelExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.HierarchyExpression);
			if (expression.HierarchyExpression == resolvedQueryExpression)
			{
				return expression;
			}
			return ((ResolvedQueryHierarchyExpression)resolvedQueryExpression).HierarchyLevel(expression.Level);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0001E804 File Offset: 0x0001CA04
		public override ResolvedQueryExpression Visit(ResolvedQueryPropertyVariationSourceExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.SourceRefExpression);
			if (expression.SourceRefExpression == resolvedQueryExpression)
			{
				return expression;
			}
			return ((ResolvedQuerySourceRefExpression)resolvedQueryExpression).VariationSource(expression.VariationSource, expression.Property);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0001E840 File Offset: 0x0001CA40
		public override ResolvedQueryExpression Visit(ResolvedQueryColumnReferenceExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Source);
			if (expression.Source == resolvedQueryExpression)
			{
				return expression;
			}
			return resolvedQueryExpression.ColumnReference(expression.SelectName);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0001E871 File Offset: 0x0001CA71
		public override ResolvedQueryExpression Visit(ResolvedQueryNotExpression expression)
		{
			return this.VisitUnary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.Not));
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0001E886 File Offset: 0x0001CA86
		public override ResolvedQueryExpression Visit(ResolvedQueryAndExpression expression)
		{
			return this.VisitBinary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.And));
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0001E89B File Offset: 0x0001CA9B
		public override ResolvedQueryExpression Visit(ResolvedQueryOrExpression expression)
		{
			return this.VisitBinary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.Or));
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0001E8B0 File Offset: 0x0001CAB0
		public override ResolvedQueryExpression Visit(ResolvedQueryAggregationExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression x) => x.Aggregate(expression.Function));
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0001E8E4 File Offset: 0x0001CAE4
		public override ResolvedQueryExpression Visit(ResolvedQueryArithmeticExpression expression)
		{
			return this.VisitBinary(expression, (ResolvedQueryExpression l, ResolvedQueryExpression r) => l.Arithmetic(r, expression.Operator));
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0001E918 File Offset: 0x0001CB18
		public override ResolvedQueryExpression Visit(ResolvedQueryBetweenExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			ResolvedQueryExpression resolvedQueryExpression2 = this.VisitExpression(expression.LowerBound);
			ResolvedQueryExpression resolvedQueryExpression3 = this.VisitExpression(expression.UpperBound);
			if (expression.Expression == resolvedQueryExpression && expression.LowerBound == resolvedQueryExpression2 && expression.UpperBound == resolvedQueryExpression3)
			{
				return expression;
			}
			return resolvedQueryExpression.Between(resolvedQueryExpression2, resolvedQueryExpression3);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0001E974 File Offset: 0x0001CB74
		public override ResolvedQueryExpression Visit(ResolvedQueryComparisonExpression expression)
		{
			return this.VisitBinary(expression, (ResolvedQueryExpression l, ResolvedQueryExpression r) => l.Comparison(r, expression.ComparisonKind));
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0001E9A6 File Offset: 0x0001CBA6
		public override ResolvedQueryExpression Visit(ResolvedQueryContainsExpression expression)
		{
			return this.VisitBinary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.Contains));
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		public override ResolvedQueryExpression Visit(ResolvedQueryDateAddExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression x) => x.DateAdd(expression.Amount, expression.TimeUnit));
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		public override ResolvedQueryExpression Visit(ResolvedQueryDateSpanExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression x) => x.DateSpan(expression.TimeUnit));
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0001EA22 File Offset: 0x0001CC22
		public override ResolvedQueryExpression Visit(ResolvedQueryExistsExpression expression)
		{
			return this.VisitUnary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.Exists));
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0001EA38 File Offset: 0x0001CC38
		public override ResolvedQueryExpression Visit(ResolvedQueryFloorExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression x) => x.Floor(expression.Size, expression.TimeUnit));
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0001EA6C File Offset: 0x0001CC6C
		public override ResolvedQueryExpression Visit(ResolvedQueryDiscretizeExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression x) => x.Discretize(expression.Count));
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public override ResolvedQueryExpression Visit(ResolvedQuerySparklineDataExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Measure);
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = this.RewriteList(expression.Groupings);
			ResolvedQueryExpression resolvedQueryExpression2 = ((expression.ScalarKey != null) ? this.VisitExpression(expression.ScalarKey) : null);
			if (expression.Measure == resolvedQueryExpression && expression.Groupings == readOnlyList && expression.ScalarKey == resolvedQueryExpression2)
			{
				return expression;
			}
			return resolvedQueryExpression.SparklineData(readOnlyList, expression.PointsPerSparkline, resolvedQueryExpression2, expression.IncludeMinGroupingInterval);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0001EB18 File Offset: 0x0001CD18
		public override ResolvedQueryExpression Visit(ResolvedQueryMemberExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression x) => x.Member(expression.Member));
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		public override ResolvedQueryExpression Visit(ResolvedQueryInExpression expression)
		{
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = this.RewriteList(expression.Expressions);
			if (expression.HasValues)
			{
				IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values = expression.Values;
				List<IReadOnlyList<ResolvedQueryExpression>> list = null;
				for (int i = 0; i < values.Count; i++)
				{
					IReadOnlyList<ResolvedQueryExpression> readOnlyList2 = values[i];
					IReadOnlyList<ResolvedQueryExpression> readOnlyList3 = this.RewriteList(readOnlyList2);
					if (readOnlyList2 != readOnlyList3)
					{
						if (list == null)
						{
							list = new List<IReadOnlyList<ResolvedQueryExpression>>(values);
						}
						list[i] = readOnlyList3;
					}
				}
				if (readOnlyList == expression.Expressions && list == null)
				{
					return expression;
				}
				IReadOnlyList<ResolvedQueryExpression> readOnlyList4 = readOnlyList;
				IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> readOnlyList5 = list;
				return readOnlyList4.In(readOnlyList5 ?? values, expression.EqualityKind);
			}
			else
			{
				ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Table);
				if (readOnlyList == expression.Expressions && resolvedQueryExpression == expression.Table)
				{
					return expression;
				}
				return readOnlyList.In(resolvedQueryExpression);
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0001EC04 File Offset: 0x0001CE04
		public override ResolvedQueryExpression Visit(ResolvedQueryScopedEvalExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = this.RewriteList(expression.Scope);
			if (resolvedQueryExpression == expression.Expression && readOnlyList == expression.Scope)
			{
				return expression;
			}
			return resolvedQueryExpression.ScopedEval(readOnlyList);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0001EC48 File Offset: 0x0001CE48
		public override ResolvedQueryExpression Visit(ResolvedQueryFilteredEvalExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			IReadOnlyList<ResolvedQueryFilter> readOnlyList = expression.Filters.Rewrite(new Func<ResolvedQueryFilter, ResolvedQueryFilter>(this.RewriteFilter));
			if (resolvedQueryExpression == expression.Expression && readOnlyList == expression.Filters)
			{
				return expression;
			}
			return resolvedQueryExpression.FilteredEval(readOnlyList);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0001EC95 File Offset: 0x0001CE95
		public override ResolvedQueryExpression Visit(ResolvedQueryLiteralExpression expression)
		{
			return expression;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0001EC98 File Offset: 0x0001CE98
		public override ResolvedQueryExpression Visit(ResolvedQueryNowExpression expression)
		{
			return expression;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		public override ResolvedQueryExpression Visit(ResolvedQueryPercentileExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression expr) => expr.Percentile(expression.Exclusive, expression.K));
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		public override ResolvedQueryExpression Visit(ResolvedQueryMinExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression expr) => expr.Min(expression.IncludeAllTypes));
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0001ED04 File Offset: 0x0001CF04
		public override ResolvedQueryExpression Visit(ResolvedQueryMaxExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression expr) => expr.Max(expression.IncludeAllTypes));
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0001ED36 File Offset: 0x0001CF36
		public override ResolvedQueryExpression Visit(ResolvedQueryStartsWithExpression expression)
		{
			return this.VisitBinary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.StartsWith));
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0001ED4B File Offset: 0x0001CF4B
		public override ResolvedQueryExpression Visit(ResolvedQueryEndsWithExpression expression)
		{
			return this.VisitBinary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.EndsWith));
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0001ED60 File Offset: 0x0001CF60
		public override ResolvedQueryExpression Visit(ResolvedQueryDefaultValueExpression expression)
		{
			return expression;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0001ED63 File Offset: 0x0001CF63
		public override ResolvedQueryExpression Visit(ResolvedQueryAnyValueExpression expression)
		{
			return expression;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0001ED66 File Offset: 0x0001CF66
		public ResolvedQueryExpression VisitExpression(ResolvedQueryExpression expression)
		{
			return expression.Accept<ResolvedQueryExpression>(this);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0001ED6F File Offset: 0x0001CF6F
		public override ResolvedQueryExpression Visit(ResolvedQueryTransformOutputRoleRefExpression expression)
		{
			return expression;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0001ED72 File Offset: 0x0001CF72
		public override ResolvedQueryExpression Visit(ResolvedQueryTransformTableColumnExpression expression)
		{
			return expression;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0001ED78 File Offset: 0x0001CF78
		public override ResolvedQueryExpression Visit(ResolvedQueryNativeFormatExpression expression)
		{
			return this.VisitUnary(expression, (ResolvedQueryExpression x) => x.NativeFormat(expression.FormatString));
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0001EDAA File Offset: 0x0001CFAA
		public override ResolvedQueryExpression Visit(ResolvedQueryNativeMeasureExpression expression)
		{
			return expression;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0001EDAD File Offset: 0x0001CFAD
		public override ResolvedQueryExpression Visit(ResolvedQueryLetRefExpression expression)
		{
			return expression;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		public override ResolvedQueryExpression Visit(ResolvedQueryRoleRefExpression expression)
		{
			return expression;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0001EDB3 File Offset: 0x0001CFB3
		public override ResolvedQueryExpression Visit(ResolvedSummaryValueRefExpression expression)
		{
			return expression;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0001EDB6 File Offset: 0x0001CFB6
		public override ResolvedQueryExpression Visit(ResolvedQueryParameterRefExpression expression)
		{
			return expression;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0001EDB9 File Offset: 0x0001CFB9
		public override ResolvedQueryExpression Visit(ResolvedQueryPrimitiveTypeExpression expression)
		{
			return expression;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		public override ResolvedQueryExpression Visit(ResolvedQueryNativeVisualCalculationExpression expression)
		{
			return expression;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		public override ResolvedQueryExpression Visit(ResolvedQueryTableTypeExpression expression)
		{
			IReadOnlyList<ResolvedQueryTableTypeColumn> readOnlyList = expression.Columns.Rewrite(new Func<ResolvedQueryTableTypeColumn, ResolvedQueryTableTypeColumn>(this.Visit));
			if (expression.Columns == readOnlyList)
			{
				return expression;
			}
			return readOnlyList.TableType();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0001EDF8 File Offset: 0x0001CFF8
		private ResolvedQueryTableTypeColumn Visit(ResolvedQueryTableTypeColumn column)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(column.Expression);
			if (column.Expression == resolvedQueryExpression)
			{
				return column;
			}
			return resolvedQueryExpression.TableTypeColumn(column.Name);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0001EE29 File Offset: 0x0001D029
		public override ResolvedQueryExpression Visit(ResolvedQueryTypeOfExpression expression)
		{
			return this.VisitUnary(expression, new Func<ResolvedQueryExpression, ResolvedQueryExpression>(ResolvedQueryExpressionBuilder.TypeOf));
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0001EE40 File Offset: 0x0001D040
		private ResolvedQueryExpression VisitBinary(ResolvedQueryBinaryExpression expression, Func<ResolvedQueryExpression, ResolvedQueryExpression, ResolvedQueryExpression> createExpr)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Left);
			ResolvedQueryExpression resolvedQueryExpression2 = this.VisitExpression(expression.Right);
			if (expression.Left == resolvedQueryExpression && expression.Right == resolvedQueryExpression2)
			{
				return expression;
			}
			return createExpr(resolvedQueryExpression, resolvedQueryExpression2);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0001EE84 File Offset: 0x0001D084
		private ResolvedQueryExpression VisitUnary(ResolvedQueryUnaryExpression expression, Func<ResolvedQueryExpression, ResolvedQueryExpression> createExpr)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression == resolvedQueryExpression)
			{
				return expression;
			}
			return createExpr(resolvedQueryExpression);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
		private IReadOnlyList<ResolvedQueryExpression> RewriteList(IReadOnlyList<ResolvedQueryExpression> original)
		{
			if (original == null)
			{
				return null;
			}
			List<ResolvedQueryExpression> list = null;
			for (int i = 0; i < original.Count; i++)
			{
				ResolvedQueryExpression resolvedQueryExpression = original[i];
				ResolvedQueryExpression resolvedQueryExpression2 = resolvedQueryExpression.Accept<ResolvedQueryExpression>(this);
				if (resolvedQueryExpression != resolvedQueryExpression2)
				{
					if (list == null)
					{
						list = new List<ResolvedQueryExpression>(original);
					}
					list[i] = resolvedQueryExpression2;
				}
			}
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = list;
			return readOnlyList ?? original;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0001EF04 File Offset: 0x0001D104
		private ResolvedQueryFilter RewriteFilter(ResolvedQueryFilter filter)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(filter.Condition);
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = this.RewriteList(filter.Target);
			if (resolvedQueryExpression == filter.Condition && readOnlyList == filter.Target)
			{
				return filter;
			}
			return resolvedQueryExpression.Filter(readOnlyList, filter.Annotations);
		}
	}
}
