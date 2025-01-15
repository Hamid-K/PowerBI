using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder
{
	// Token: 0x0200009C RID: 156
	public static class ResolvedQueryExpressionBuilder
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0000B0C6 File Offset: 0x000092C6
		public static ResolvedEntitySource EntitySource(this IConceptualEntity entity, string name, string schema = null)
		{
			return new ResolvedEntitySource(name, entity, schema);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public static ResolvedExpressionSource SubquerySource(this ResolvedQueryDefinition subquery, string name)
		{
			return subquery.Subquery().ExpressionSource(name);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000B0DE File Offset: 0x000092DE
		public static ResolvedExpressionSource ExpressionSource(this ResolvedQueryExpression expression, string name)
		{
			return new ResolvedExpressionSource(name, expression);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000B0E7 File Offset: 0x000092E7
		public static ResolvedQueryLetBinding LetBinding(this ResolvedQueryExpression expression, string name)
		{
			return new ResolvedQueryLetBinding(name, expression);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000B0F0 File Offset: 0x000092F0
		public static ResolvedQueryFilter Filter(this ResolvedQueryExpression condition, IReadOnlyList<ResolvedQueryExpression> target = null, FilterAnnotations annotations = null)
		{
			return new ResolvedQueryFilter(target, condition, annotations);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000B0FA File Offset: 0x000092FA
		public static ResolvedQuerySortClause Sort(this ResolvedQueryExpression expression, QuerySortDirection direction)
		{
			return new ResolvedQuerySortClause(expression, direction);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000B103 File Offset: 0x00009303
		public static ResolvedQuerySortClause SortAscending(this ResolvedQueryExpression expression)
		{
			return expression.Sort(QuerySortDirection.Ascending);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000B10C File Offset: 0x0000930C
		public static ResolvedQuerySortClause SortDescending(this ResolvedQueryExpression expression)
		{
			return expression.Sort(QuerySortDirection.Descending);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000B115 File Offset: 0x00009315
		public static ResolvedQuerySourceRefExpression SourceRef(this IConceptualEntity entity, string sourceName)
		{
			return new ResolvedQuerySourceRefExpression(sourceName, entity);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000B11E File Offset: 0x0000931E
		public static ResolvedQuerySourceRefExpression SourceRef(this ResolvedEntitySource entitySource)
		{
			return new ResolvedQuerySourceRefExpression(entitySource.Name, entitySource.Entity);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000B131 File Offset: 0x00009331
		public static ResolvedQueryExpressionSourceRefExpression ExpressionSourceRef(this ResolvedExpressionSource expressionSource)
		{
			return new ResolvedQueryExpressionSourceRefExpression(expressionSource.Name, expressionSource.Expression);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000B144 File Offset: 0x00009344
		public static ResolvedQueryExpressionSourceRefExpression ExpressionSourceRef(this string sourceName, ResolvedQueryExpression expression)
		{
			return new ResolvedQueryExpressionSourceRefExpression(sourceName, expression);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000B14D File Offset: 0x0000934D
		public static ResolvedQuerySubqueryExpression Subquery(this ResolvedQueryDefinition subquery)
		{
			return new ResolvedQuerySubqueryExpression(subquery);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000B158 File Offset: 0x00009358
		public static ResolvedQueryPropertyExpression Property(this ResolvedQueryExpression sourceExpr, IConceptualProperty property)
		{
			IConceptualColumn conceptualColumn = property as IConceptualColumn;
			if (conceptualColumn != null)
			{
				return sourceExpr.Column(conceptualColumn);
			}
			IConceptualMeasure conceptualMeasure = property as IConceptualMeasure;
			if (conceptualMeasure != null)
			{
				return sourceExpr.Measure(conceptualMeasure);
			}
			throw new ArgumentException(StringUtil.FormatInvariant("Unexpected property {0} in expression {1}.", property.GetType(), sourceExpr.ToString()));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000B1A4 File Offset: 0x000093A4
		public static ResolvedQueryColumnExpression Column(this ResolvedQueryExpression sourceExpr, IConceptualColumn column)
		{
			return new ResolvedQueryColumnExpression(sourceExpr, column);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000B1AD File Offset: 0x000093AD
		public static ResolvedQueryColumnExpression Column(this IConceptualColumn column, string sourceName)
		{
			return column.Entity.SourceRef(sourceName).Column(column);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000B1C1 File Offset: 0x000093C1
		public static ResolvedQueryColumnExpression ColumnStandalone(this IConceptualColumn column)
		{
			return new ResolvedQuerySourceRefExpression(column.Entity).Column(column);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000B1D4 File Offset: 0x000093D4
		public static ResolvedQueryMeasureExpression Measure(this ResolvedQueryExpression sourceExpr, IConceptualMeasure measure)
		{
			return new ResolvedQueryMeasureExpression(sourceExpr, measure);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000B1DD File Offset: 0x000093DD
		public static ResolvedQueryMeasureExpression Measure(this IConceptualMeasure measure, string sourceName)
		{
			return measure.Entity.SourceRef(sourceName).Measure(measure);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000B1F1 File Offset: 0x000093F1
		public static ResolvedQueryMeasureExpression MeasureStandalone(this IConceptualMeasure measure)
		{
			return new ResolvedQuerySourceRefExpression(measure.Entity).Measure(measure);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000B204 File Offset: 0x00009404
		public static ResolvedQueryHierarchyExpression Hierarchy(this ResolvedQueryExpression sourceExpr, IConceptualHierarchy hierarchy)
		{
			return new ResolvedQueryHierarchyExpression(sourceExpr, hierarchy);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000B20D File Offset: 0x0000940D
		public static ResolvedQueryHierarchyLevelExpression HierarchyLevel(this ResolvedQueryHierarchyExpression hierarchy, IConceptualHierarchyLevel level)
		{
			return new ResolvedQueryHierarchyLevelExpression(hierarchy, level);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000B216 File Offset: 0x00009416
		public static ResolvedQueryHierarchyLevelExpression HierarchyLevelStandalone(this IConceptualHierarchyLevel level)
		{
			return new ResolvedQuerySourceRefExpression(level.Source.Entity).Hierarchy(level.Hierarchy).HierarchyLevel(level);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000B239 File Offset: 0x00009439
		public static ResolvedQueryPropertyVariationSourceExpression VariationSource(this ResolvedQuerySourceRefExpression sourceRef, IConceptualVariationSource variationSource, IConceptualProperty sourceProperty)
		{
			return new ResolvedQueryPropertyVariationSourceExpression(sourceRef, variationSource, sourceProperty);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000B243 File Offset: 0x00009443
		public static ResolvedQueryColumnReferenceExpression ColumnReference(this ResolvedQueryExpression sourceExpr, string selectName)
		{
			return new ResolvedQueryColumnReferenceExpression(sourceExpr, selectName);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000B24C File Offset: 0x0000944C
		public static ResolvedQueryNotExpression Not(this ResolvedQueryExpression expression)
		{
			return new ResolvedQueryNotExpression(expression);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000B254 File Offset: 0x00009454
		public static ResolvedQueryAndExpression And(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return new ResolvedQueryAndExpression(left, right);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000B25D File Offset: 0x0000945D
		public static ResolvedQueryOrExpression Or(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return new ResolvedQueryOrExpression(left, right);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000B266 File Offset: 0x00009466
		public static ResolvedQueryInExpression In(this IReadOnlyList<ResolvedQueryExpression> args, IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values, QueryEqualitySemanticsKind? equalityKind = null)
		{
			return new ResolvedQueryInExpression(args, values, equalityKind);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000B270 File Offset: 0x00009470
		public static ResolvedQueryInExpression In(this IReadOnlyList<ResolvedQueryExpression> args, ResolvedQueryExpression table)
		{
			return new ResolvedQueryInExpression(args, table);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000B279 File Offset: 0x00009479
		public static ResolvedQueryScopedEvalExpression ScopedEval(this ResolvedQueryExpression expression, IReadOnlyList<ResolvedQueryExpression> scope)
		{
			return new ResolvedQueryScopedEvalExpression(expression, scope);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000B282 File Offset: 0x00009482
		public static ResolvedQueryFilteredEvalExpression FilteredEval(this ResolvedQueryExpression expression, IReadOnlyList<ResolvedQueryFilter> filters)
		{
			return new ResolvedQueryFilteredEvalExpression(expression, filters);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000B28B File Offset: 0x0000948B
		public static ResolvedQueryAggregationExpression Aggregate(this ResolvedQueryExpression expression, QueryAggregateFunction function)
		{
			return new ResolvedQueryAggregationExpression(expression, function);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000B294 File Offset: 0x00009494
		public static ResolvedQueryAggregationExpression Sum(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Sum);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000B29D File Offset: 0x0000949D
		public static ResolvedQueryAggregationExpression Average(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Avg);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000B2A6 File Offset: 0x000094A6
		public static ResolvedQueryAggregationExpression Median(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Median);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000B2AF File Offset: 0x000094AF
		public static ResolvedQueryAggregationExpression Variance(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Variance);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000B2B8 File Offset: 0x000094B8
		public static ResolvedQueryAggregationExpression StandardDeviation(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.StandardDeviation);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000B2C1 File Offset: 0x000094C1
		public static ResolvedQueryAggregationExpression Count(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Count);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000B2CA File Offset: 0x000094CA
		public static ResolvedQueryAggregationExpression CountNonNull(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.CountNonNull);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000B2D3 File Offset: 0x000094D3
		public static ResolvedQueryAggregationExpression Min(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Min);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000B2DC File Offset: 0x000094DC
		public static ResolvedQueryAggregationExpression Max(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Max);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000B2E5 File Offset: 0x000094E5
		public static ResolvedQueryAggregationExpression SingleValue(this ResolvedQueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.SingleValue);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000B2EF File Offset: 0x000094EF
		public static ResolvedQueryArithmeticExpression Arithmetic(this ResolvedQueryExpression left, ResolvedQueryExpression right, QueryArithmeticOperatorKind arithmeticOperatorKind)
		{
			return new ResolvedQueryArithmeticExpression(left, right, arithmeticOperatorKind);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000B2F9 File Offset: 0x000094F9
		public static ResolvedQueryArithmeticExpression Add(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Arithmetic(right, QueryArithmeticOperatorKind.Add);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000B303 File Offset: 0x00009503
		public static ResolvedQueryArithmeticExpression Subtract(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Arithmetic(right, QueryArithmeticOperatorKind.Subtract);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000B30D File Offset: 0x0000950D
		public static ResolvedQueryArithmeticExpression Multiply(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Arithmetic(right, QueryArithmeticOperatorKind.Multiply);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000B317 File Offset: 0x00009517
		public static ResolvedQueryArithmeticExpression Divide(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Arithmetic(right, QueryArithmeticOperatorKind.Divide);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000B321 File Offset: 0x00009521
		public static ResolvedQueryBetweenExpression Between(this ResolvedQueryExpression expression, ResolvedQueryExpression lowerBound, ResolvedQueryExpression upperBound)
		{
			return new ResolvedQueryBetweenExpression(expression, lowerBound, upperBound);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000B32B File Offset: 0x0000952B
		public static ResolvedQueryComparisonExpression Comparison(this ResolvedQueryExpression left, ResolvedQueryExpression right, QueryComparisonKind comparisonKind)
		{
			return new ResolvedQueryComparisonExpression(left, right, comparisonKind);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000B335 File Offset: 0x00009535
		public static ResolvedQueryComparisonExpression Equal(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Comparison(right, QueryComparisonKind.Equal);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000B33F File Offset: 0x0000953F
		public static ResolvedQueryComparisonExpression GreaterThan(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Comparison(right, QueryComparisonKind.GreaterThan);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000B349 File Offset: 0x00009549
		public static ResolvedQueryComparisonExpression GreaterThanOrEqual(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Comparison(right, QueryComparisonKind.GreaterThanOrEqual);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000B353 File Offset: 0x00009553
		public static ResolvedQueryComparisonExpression LessThan(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Comparison(right, QueryComparisonKind.LessThan);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000B35D File Offset: 0x0000955D
		public static ResolvedQueryComparisonExpression LessThanOrEqual(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return left.Comparison(right, QueryComparisonKind.LessThanOrEqual);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000B367 File Offset: 0x00009567
		public static ResolvedQueryContainsExpression Contains(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return new ResolvedQueryContainsExpression(left, right);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000B370 File Offset: 0x00009570
		public static ResolvedQueryDateAddExpression DateAdd(this ResolvedQueryExpression expression, int amount, TimeUnit timeUnit)
		{
			return new ResolvedQueryDateAddExpression(amount, timeUnit, expression);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B37A File Offset: 0x0000957A
		public static ResolvedQueryDateSpanExpression DateSpan(this ResolvedQueryExpression expression, TimeUnit timeUnit)
		{
			return new ResolvedQueryDateSpanExpression(timeUnit, expression);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000B383 File Offset: 0x00009583
		public static ResolvedQueryExistsExpression Exists(this ResolvedQueryExpression expression)
		{
			return new ResolvedQueryExistsExpression(expression);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000B38B File Offset: 0x0000958B
		public static ResolvedQueryFloorExpression Floor(this ResolvedQueryExpression value, double size, TimeUnit? timeUnit)
		{
			return new ResolvedQueryFloorExpression(value, size, timeUnit);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000B395 File Offset: 0x00009595
		public static ResolvedQueryDiscretizeExpression Discretize(this ResolvedQueryExpression value, int count)
		{
			return new ResolvedQueryDiscretizeExpression(value, count);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000B39E File Offset: 0x0000959E
		public static ResolvedQuerySparklineDataExpression SparklineData(this ResolvedQueryExpression measure, IReadOnlyList<ResolvedQueryExpression> groupings, int pointsPerSparkline, ResolvedQueryExpression scalarKey, bool includeMinGroupingInterval)
		{
			return new ResolvedQuerySparklineDataExpression(measure, groupings, pointsPerSparkline, scalarKey, includeMinGroupingInterval);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000B3AB File Offset: 0x000095AB
		public static ResolvedQueryMemberExpression Member(this ResolvedQueryExpression value, string member)
		{
			return new ResolvedQueryMemberExpression(value, member);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000B3B4 File Offset: 0x000095B4
		public static ResolvedQueryNativeFormatExpression NativeFormat(this ResolvedQueryExpression value, string formatString)
		{
			return new ResolvedQueryNativeFormatExpression(value, formatString);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000B3BD File Offset: 0x000095BD
		public static ResolvedQueryNativeMeasureExpression NativeMeasure(string language, string expression)
		{
			return new ResolvedQueryNativeMeasureExpression(language, expression);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000B3C6 File Offset: 0x000095C6
		public static ResolvedQueryLiteralExpression Literal(this PrimitiveValue value)
		{
			return new ResolvedQueryLiteralExpression(value);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000B3CE File Offset: 0x000095CE
		public static ResolvedQueryNowExpression Now()
		{
			return ResolvedQueryNowExpression.Instance;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000B3D5 File Offset: 0x000095D5
		public static ResolvedQueryPercentileExpression Percentile(this ResolvedQueryExpression arg, bool exclusive, double value)
		{
			return new ResolvedQueryPercentileExpression(exclusive, value, arg);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000B3DF File Offset: 0x000095DF
		public static ResolvedQueryMinExpression Min(this ResolvedQueryExpression arg, IncludeAllTypes includeAllTypesBehavior)
		{
			return new ResolvedQueryMinExpression(arg, includeAllTypesBehavior);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000B3E8 File Offset: 0x000095E8
		public static ResolvedQueryMaxExpression Max(this ResolvedQueryExpression arg, IncludeAllTypes includeAllTypesBehavior)
		{
			return new ResolvedQueryMaxExpression(arg, includeAllTypesBehavior);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000B3F1 File Offset: 0x000095F1
		public static ResolvedQueryStartsWithExpression StartsWith(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return new ResolvedQueryStartsWithExpression(left, right);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000B3FA File Offset: 0x000095FA
		public static ResolvedQueryEndsWithExpression EndsWith(this ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return new ResolvedQueryEndsWithExpression(left, right);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000B403 File Offset: 0x00009603
		public static ResolvedQueryDefaultValueExpression DefaultValue()
		{
			return ResolvedQueryDefaultValueExpression.Instance;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000B40A File Offset: 0x0000960A
		public static ResolvedQueryAnyValueExpression AnyValue(bool defaultValueOverridesAncestors)
		{
			if (defaultValueOverridesAncestors)
			{
				return ResolvedQueryAnyValueExpression.DefaultValueOverridesAncestorsInstance;
			}
			return ResolvedQueryAnyValueExpression.Instance;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000B41A File Offset: 0x0000961A
		public static ResolvedQueryTransformOutputRoleRefExpression TransformOutputRoleRef(string role)
		{
			return new ResolvedQueryTransformOutputRoleRefExpression(role);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000B422 File Offset: 0x00009622
		public static ResolvedQueryTransformTableColumnExpression TransformTableColumn(this ResolvedQueryTransformTable table, ResolvedQueryTransformTableColumn column)
		{
			return new ResolvedQueryTransformTableColumnExpression(table, column);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000B42B File Offset: 0x0000962B
		public static ResolvedQueryLetRefExpression LetRef(this ResolvedQueryLetBinding binding)
		{
			return new ResolvedQueryLetRefExpression(binding);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000B433 File Offset: 0x00009633
		public static ResolvedQueryRoleRefExpression RoleRef(string role)
		{
			return new ResolvedQueryRoleRefExpression(role);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000B43B File Offset: 0x0000963B
		public static ResolvedSummaryValueRefExpression SummaryValueRef(string name)
		{
			return new ResolvedSummaryValueRefExpression(name);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000B443 File Offset: 0x00009643
		public static ResolvedQueryParameterRefExpression ParameterRef(this ResolvedQueryParameterDeclaration declaration)
		{
			return new ResolvedQueryParameterRefExpression(declaration);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000B44B File Offset: 0x0000964B
		public static ResolvedQueryTypeOfExpression TypeOf(this ResolvedQueryExpression expression)
		{
			return new ResolvedQueryTypeOfExpression(expression);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000B453 File Offset: 0x00009653
		public static ResolvedQueryPrimitiveTypeExpression PrimitiveType(this ConceptualPrimitiveType type)
		{
			return new ResolvedQueryPrimitiveTypeExpression(type);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000B45B File Offset: 0x0000965B
		public static ResolvedQueryTableTypeColumn TableTypeColumn(this ResolvedQueryExpression expression, string name)
		{
			return new ResolvedQueryTableTypeColumn(name, expression);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000B464 File Offset: 0x00009664
		public static ResolvedQueryTableTypeExpression TableType(this IReadOnlyList<ResolvedQueryTableTypeColumn> columns)
		{
			return new ResolvedQueryTableTypeExpression(columns);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000B46C File Offset: 0x0000966C
		public static ResolvedQueryNativeVisualCalculationExpression NativeVisualCalculation(string language, string expression)
		{
			return new ResolvedQueryNativeVisualCalculationExpression(language, expression);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000B475 File Offset: 0x00009675
		public static ResolvedQuerySelect Select(this ResolvedQueryExpression expression, string name = null, string nativeReferenceName = null)
		{
			return new ResolvedQuerySelect(expression, name, nativeReferenceName);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000B47F File Offset: 0x0000967F
		public static ResolvedQueryAxisGroup AxisGroup(this ResolvedQueryExpression expression, bool subtotal = false)
		{
			return new ResolvedQueryExpression[] { expression }.AxisGroup(subtotal);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000B491 File Offset: 0x00009691
		public static ResolvedQueryAxisGroup AxisGroup(this IReadOnlyList<ResolvedQueryExpression> expressions, bool subtotal = false)
		{
			return new ResolvedQueryAxisGroup(expressions, subtotal);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000B49A File Offset: 0x0000969A
		public static ResolvedQueryAxis Axis(this ResolvedQueryAxisGroup axisGroup, string name)
		{
			return new ResolvedQueryAxisGroup[] { axisGroup }.Axis(name);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000B4AC File Offset: 0x000096AC
		public static ResolvedQueryAxis Axis(this IReadOnlyList<ResolvedQueryAxisGroup> axisGroups, string name)
		{
			return new ResolvedQueryAxis(name, axisGroups);
		}
	}
}
