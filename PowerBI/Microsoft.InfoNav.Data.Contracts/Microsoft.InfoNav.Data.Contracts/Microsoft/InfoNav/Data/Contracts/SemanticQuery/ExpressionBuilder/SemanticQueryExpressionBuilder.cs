using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder
{
	// Token: 0x02000097 RID: 151
	public static class SemanticQueryExpressionBuilder
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00009AF1 File Offset: 0x00007CF1
		public static QuerySourceRefExpression SourceRef(this IConceptualEntity sourceEntity)
		{
			return new QuerySourceRefExpression
			{
				Schema = ConceptualSchemaNames.NormalizeSchemaNameForSerialization(sourceEntity),
				Entity = sourceEntity.Name
			};
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00009B10 File Offset: 0x00007D10
		public static QuerySourceRefExpression SourceRef(this string sourceName)
		{
			return new QuerySourceRefExpression
			{
				Source = sourceName
			};
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009B1E File Offset: 0x00007D1E
		public static QuerySourceRefExpression SourceRef(this string schemaName, string entityName)
		{
			return new QuerySourceRefExpression
			{
				Schema = schemaName,
				Entity = entityName
			};
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00009B33 File Offset: 0x00007D33
		public static QueryColumnExpression Column(this QueryExpression sourceRef, IConceptualColumn column)
		{
			return sourceRef.Column(column.Name);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00009B41 File Offset: 0x00007D41
		public static QueryColumnExpression Column(this QueryExpression sourceRef, string columnName)
		{
			return new QueryColumnExpression
			{
				Property = columnName,
				Expression = sourceRef
			};
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009B5B File Offset: 0x00007D5B
		public static QueryColumnExpression ColumnInSource(this IConceptualColumn column, string sourceName)
		{
			return sourceName.SourceRef().Column(column);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009B69 File Offset: 0x00007D69
		public static QueryColumnExpression ColumnStandalone(this IConceptualColumn column)
		{
			return column.Entity.SourceRef().Column(column);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00009B7C File Offset: 0x00007D7C
		public static QueryMeasureExpression Measure(this QueryExpression sourceRef, IConceptualMeasure measure)
		{
			return sourceRef.Measure(measure.Name);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00009B8A File Offset: 0x00007D8A
		public static QueryMeasureExpression Measure(this QueryExpression sourceRef, string measureName)
		{
			return new QueryMeasureExpression
			{
				Property = measureName,
				Expression = sourceRef
			};
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009BA4 File Offset: 0x00007DA4
		public static QueryMeasureExpression MeasureInSource(this IConceptualMeasure measure, string sourceName)
		{
			return sourceName.SourceRef().Measure(measure);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009BB2 File Offset: 0x00007DB2
		public static QueryHierarchyExpression Hierarchy(this QueryExpression sourceRef, IConceptualHierarchy hierarchy)
		{
			return sourceRef.Hierarchy(hierarchy.Name);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009BC0 File Offset: 0x00007DC0
		public static QueryHierarchyExpression Hierarchy(this QueryExpression sourceRef, string hierarchyName)
		{
			return new QueryHierarchyExpression
			{
				Hierarchy = hierarchyName,
				Expression = sourceRef
			};
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00009BDA File Offset: 0x00007DDA
		public static QueryHierarchyExpression HierarchyInSource(this IConceptualHierarchy hierarchy, string sourceName)
		{
			return sourceName.SourceRef().Hierarchy(hierarchy);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public static QueryHierarchyLevelExpression HierarchyLevel(this QueryExpression sourceRef, IConceptualHierarchyLevel hierarchyLevel)
		{
			return sourceRef.HierarchyLevel(hierarchyLevel.Name);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00009BF6 File Offset: 0x00007DF6
		public static QueryHierarchyLevelExpression HierarchyLevel(this QueryExpression sourceRef, string hierarchyLevelName)
		{
			return new QueryHierarchyLevelExpression
			{
				Level = hierarchyLevelName,
				Expression = sourceRef
			};
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00009C10 File Offset: 0x00007E10
		public static QueryHierarchyLevelExpression HierarchyLevelInSource(this IConceptualHierarchyLevel level, string sourceName)
		{
			return level.Hierarchy.HierarchyInSource(sourceName).HierarchyLevel(level);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00009C24 File Offset: 0x00007E24
		public static QueryPropertyVariationSourceExpression VariationSource(this QueryExpression sourceRef, IConceptualVariationSource variationSource, IConceptualProperty sourceProperty)
		{
			return sourceRef.VariationSource(variationSource.Name, sourceProperty.Name);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00009C38 File Offset: 0x00007E38
		public static QueryPropertyVariationSourceExpression VariationSource(this QueryExpression sourceRef, string variationSourceName, string sourcePropertyName)
		{
			return new QueryPropertyVariationSourceExpression
			{
				Property = sourcePropertyName,
				Name = variationSourceName,
				Expression = sourceRef
			};
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009C59 File Offset: 0x00007E59
		public static QueryNotExpression Not(this QueryExpression sourceRef)
		{
			return new QueryNotExpression
			{
				Expression = sourceRef
			};
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009C6C File Offset: 0x00007E6C
		public static QueryAndExpression And(this QueryExpression left, QueryExpression right)
		{
			return new QueryAndExpression
			{
				Left = left,
				Right = right
			};
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00009C8B File Offset: 0x00007E8B
		public static QueryOrExpression Or(this QueryExpression left, QueryExpression right)
		{
			return new QueryOrExpression
			{
				Left = left,
				Right = right
			};
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009CAA File Offset: 0x00007EAA
		public static QueryAggregationExpression Aggregate(this QueryExpression sourceRef, QueryAggregateFunction function)
		{
			return new QueryAggregationExpression
			{
				Function = function,
				Expression = sourceRef
			};
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00009CC4 File Offset: 0x00007EC4
		public static QueryAggregationExpression Sum(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Sum);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00009CCD File Offset: 0x00007ECD
		public static QueryAggregationExpression Average(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Avg);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00009CD6 File Offset: 0x00007ED6
		public static QueryAggregationExpression Count(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Count);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00009CDF File Offset: 0x00007EDF
		public static QueryAggregationExpression Min(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Min);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public static QueryAggregationExpression Max(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Max);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00009CF1 File Offset: 0x00007EF1
		public static QueryAggregationExpression SingleValue(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.SingleValue);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00009CFB File Offset: 0x00007EFB
		public static QueryAggregationExpression CountNonNull(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.CountNonNull);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00009D04 File Offset: 0x00007F04
		public static QueryAggregationExpression Median(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Median);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00009D0D File Offset: 0x00007F0D
		public static QueryAggregationExpression StandardDeviation(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.StandardDeviation);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00009D16 File Offset: 0x00007F16
		public static QueryAggregationExpression Variance(this QueryExpression expression)
		{
			return expression.Aggregate(QueryAggregateFunction.Variance);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009D1F File Offset: 0x00007F1F
		public static QueryArithmeticExpression Arithmetic(this QueryExpression left, QueryExpression right, QueryArithmeticOperatorKind operation)
		{
			return new QueryArithmeticExpression
			{
				Operator = operation,
				Left = left,
				Right = right
			};
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00009D45 File Offset: 0x00007F45
		public static QueryBetweenExpression Between(this QueryExpression sourceRef, QueryExpression lowerBound, QueryExpression upperBound)
		{
			return new QueryBetweenExpression
			{
				Expression = sourceRef,
				LowerBound = lowerBound,
				UpperBound = upperBound
			};
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00009D70 File Offset: 0x00007F70
		public static QueryComparisonExpression Comparison(this QueryExpression left, QueryExpression right, QueryComparisonKind comparisonKind)
		{
			return new QueryComparisonExpression
			{
				ComparisonKind = comparisonKind,
				Left = left,
				Right = right
			};
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00009D96 File Offset: 0x00007F96
		public static QueryComparisonExpression Equal(this QueryExpression left, QueryExpression right)
		{
			return new QueryComparisonExpression
			{
				ComparisonKind = QueryComparisonKind.Equal,
				Left = left,
				Right = right
			};
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00009DBC File Offset: 0x00007FBC
		public static QueryComparisonExpression GreaterThan(this QueryExpression left, QueryExpression right)
		{
			return new QueryComparisonExpression
			{
				ComparisonKind = QueryComparisonKind.GreaterThan,
				Left = left,
				Right = right
			};
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00009DE2 File Offset: 0x00007FE2
		public static QueryComparisonExpression GreaterThanOrEqual(this QueryExpression left, QueryExpression right)
		{
			return new QueryComparisonExpression
			{
				ComparisonKind = QueryComparisonKind.GreaterThanOrEqual,
				Left = left,
				Right = right
			};
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00009E08 File Offset: 0x00008008
		public static QueryComparisonExpression LessThan(this QueryExpression left, QueryExpression right)
		{
			return new QueryComparisonExpression
			{
				ComparisonKind = QueryComparisonKind.LessThan,
				Left = left,
				Right = right
			};
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00009E2E File Offset: 0x0000802E
		public static QueryComparisonExpression LessThanOrEqual(this QueryExpression left, QueryExpression right)
		{
			return new QueryComparisonExpression
			{
				ComparisonKind = QueryComparisonKind.LessThanOrEqual,
				Left = left,
				Right = right
			};
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00009E54 File Offset: 0x00008054
		public static QueryContainsExpression Contains(this QueryExpression left, QueryExpression right)
		{
			return new QueryContainsExpression
			{
				Left = left,
				Right = right
			};
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00009E73 File Offset: 0x00008073
		public static QueryDateAddExpression DateAdd(this QueryExpression sourceRef, int amount, TimeUnit timeUnit)
		{
			return new QueryDateAddExpression
			{
				Expression = sourceRef,
				Amount = amount,
				TimeUnit = timeUnit
			};
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00009E94 File Offset: 0x00008094
		public static QueryDateSpanExpression DateSpan(this QueryExpression sourceRef, TimeUnit timeunit)
		{
			return new QueryDateSpanExpression
			{
				TimeUnit = timeunit,
				Expression = sourceRef
			};
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009EAE File Offset: 0x000080AE
		public static QueryExistsExpression Exists(this QueryExpression sourceRef)
		{
			return new QueryExistsExpression
			{
				Expression = sourceRef
			};
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00009EC1 File Offset: 0x000080C1
		public static QueryFloorExpression Floor(this QueryExpression value, double size, TimeUnit? timeUnit)
		{
			return new QueryFloorExpression
			{
				Expression = value,
				Size = size,
				TimeUnit = timeUnit
			};
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00009EE2 File Offset: 0x000080E2
		public static QueryDiscretizeExpression Discretize(this QueryExpression value, int count)
		{
			return new QueryDiscretizeExpression
			{
				Expression = value,
				Count = count
			};
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00009EFC File Offset: 0x000080FC
		public static QuerySparklineDataExpression SparklineData(this QueryExpression measure, List<QueryExpressionContainer> groupings, int pointsPerSparkline, bool includeMinGroupingInterval, QueryExpression scalarKey = null)
		{
			return new QuerySparklineDataExpression
			{
				Measure = measure,
				Groupings = groupings,
				PointsPerSparkline = pointsPerSparkline,
				ScalarKey = scalarKey,
				IncludeMinGroupingInterval = includeMinGroupingInterval
			};
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009F31 File Offset: 0x00008131
		public static QueryMemberExpression Member(this QueryExpression value, string member)
		{
			return new QueryMemberExpression
			{
				Expression = value,
				Member = member
			};
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00009F4B File Offset: 0x0000814B
		public static QueryNativeFormatExpression NativeFormat(this QueryExpression value, string format)
		{
			return new QueryNativeFormatExpression
			{
				Expression = value,
				FormatString = format
			};
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00009F65 File Offset: 0x00008165
		public static QueryNativeMeasureExpression NativeMeasure(string language, string expression)
		{
			return new QueryNativeMeasureExpression
			{
				Language = language,
				Expression = expression
			};
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00009F7A File Offset: 0x0000817A
		public static QueryNativeVisualCalculationExpression NativeVisualCalculation(string language, string expression)
		{
			return new QueryNativeVisualCalculationExpression
			{
				Language = language,
				Expression = expression
			};
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009F90 File Offset: 0x00008190
		public static QueryInExpression In(this List<QueryExpressionContainer> expressions, List<QueryExpressionContainer> values)
		{
			return expressions.In(new List<List<QueryExpressionContainer>>(1) { values }, null);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00009FBC File Offset: 0x000081BC
		public static QueryInExpression In(this List<QueryExpressionContainer> expressions, List<List<QueryExpressionContainer>> values)
		{
			return expressions.In(values, null);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00009FD9 File Offset: 0x000081D9
		public static QueryInExpression In(this List<QueryExpressionContainer> expressions, List<List<QueryExpressionContainer>> values, QueryEqualitySemanticsKind? equalityKind)
		{
			return new QueryInExpression
			{
				Expressions = expressions,
				Values = values,
				EqualityKind = equalityKind
			};
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009FF5 File Offset: 0x000081F5
		public static QueryInExpression In(this QueryExpression expression, params QueryExpression[] values)
		{
			return expression.Container().ListWrap<QueryExpressionContainer>().In(values.Select((QueryExpression v) => new List<QueryExpressionContainer> { v }).ToList<List<QueryExpressionContainer>>());
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000A034 File Offset: 0x00008234
		public static QueryInExpression In(this QueryExpression expression, QueryEqualitySemanticsKind equalityKind, params QueryExpression[] values)
		{
			return expression.Container().ListWrap<QueryExpressionContainer>().In(values.Select((QueryExpression v) => new List<QueryExpressionContainer> { v }).ToList<List<QueryExpressionContainer>>(), new QueryEqualitySemanticsKind?(equalityKind));
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000A081 File Offset: 0x00008281
		public static QueryInExpression In(this List<QueryExpressionContainer> expressions, QueryExpression table)
		{
			return new QueryInExpression
			{
				Expressions = expressions,
				Table = table
			};
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000A09B File Offset: 0x0000829B
		public static QueryScopedEvalExpression ScopedEval(this QueryExpression expression, List<QueryExpressionContainer> scope)
		{
			return new QueryScopedEvalExpression
			{
				Expression = expression,
				Scope = scope
			};
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000A0B5 File Offset: 0x000082B5
		public static QueryFilteredEvalExpression FilteredEval(this QueryExpression expression, List<QueryFilter> filters)
		{
			return new QueryFilteredEvalExpression
			{
				Expression = expression,
				Filters = filters
			};
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000A0CF File Offset: 0x000082CF
		public static QueryLiteralExpression Literal(this string primitiveValue)
		{
			return primitiveValue.Literal();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000A0DC File Offset: 0x000082DC
		public static QueryLiteralExpression Literal(this PrimitiveValue primitiveValue)
		{
			return new QueryLiteralExpression
			{
				Value = PrimitiveValueEncoding.ToTypeEncodedString(primitiveValue)
			};
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A0EF File Offset: 0x000082EF
		public static QueryLiteralExpression LiteralFromEncodedString(this string encodedString)
		{
			return new QueryLiteralExpression
			{
				Value = encodedString
			};
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A0FD File Offset: 0x000082FD
		public static QueryNowExpression Now()
		{
			return new QueryNowExpression();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000A104 File Offset: 0x00008304
		public static QueryPercentileExpression Percentile(this QueryExpression sourceRef, bool exclusive, double percentile)
		{
			return new QueryPercentileExpression
			{
				Exclusive = exclusive,
				K = percentile,
				Expression = sourceRef
			};
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000A125 File Offset: 0x00008325
		public static QueryMinExpression Min(this QueryExpression sourceRef, IncludeAllTypes includeAllTypes)
		{
			return new QueryMinExpression
			{
				IncludeAllTypes = includeAllTypes,
				Expression = sourceRef
			};
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000A13F File Offset: 0x0000833F
		public static QueryMaxExpression Max(this QueryExpression sourceRef, IncludeAllTypes includeAllTypes)
		{
			return new QueryMaxExpression
			{
				IncludeAllTypes = includeAllTypes,
				Expression = sourceRef
			};
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000A159 File Offset: 0x00008359
		public static QueryStartsWithExpression StartsWith(this QueryExpression left, QueryExpression right)
		{
			return new QueryStartsWithExpression
			{
				Left = left,
				Right = right
			};
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000A178 File Offset: 0x00008378
		public static QueryEndsWithExpression EndsWith(this QueryExpression left, QueryExpression right)
		{
			return new QueryEndsWithExpression
			{
				Left = left,
				Right = right
			};
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000A197 File Offset: 0x00008397
		public static QueryDefaultValueExpression DefaultValue()
		{
			return new QueryDefaultValueExpression();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000A19E File Offset: 0x0000839E
		public static QueryAnyValueExpression AnyValue(bool defaultValueOverridesAncestors)
		{
			return new QueryAnyValueExpression
			{
				DefaultValueOverridesAncestors = defaultValueOverridesAncestors
			};
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000A1AC File Offset: 0x000083AC
		public static QueryTransformOutputRoleRefExpression TransformOutputRoleRef(this string role)
		{
			return new QueryTransformOutputRoleRefExpression
			{
				Role = role
			};
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A1BA File Offset: 0x000083BA
		public static QueryTransformTableRefExpression TransformTableRef(this string name)
		{
			return new QueryTransformTableRefExpression
			{
				Source = name
			};
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A1C8 File Offset: 0x000083C8
		public static QuerySubqueryExpression Subquery(this QueryDefinition query)
		{
			return new QuerySubqueryExpression
			{
				Query = query
			};
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A1D6 File Offset: 0x000083D6
		public static QueryLetRefExpression LetRef(this string name)
		{
			return new QueryLetRefExpression
			{
				Name = name
			};
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A1E4 File Offset: 0x000083E4
		public static QueryRoleRefExpression RoleRef(this string role)
		{
			return new QueryRoleRefExpression
			{
				Role = role
			};
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000A1F2 File Offset: 0x000083F2
		public static QuerySummaryValueRefExpression SummaryValueRef(this string name)
		{
			return new QuerySummaryValueRefExpression
			{
				Name = name
			};
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000A200 File Offset: 0x00008400
		public static QueryParameterRefExpression ParameterRef(this string name)
		{
			return new QueryParameterRefExpression
			{
				Name = name
			};
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000A20E File Offset: 0x0000840E
		public static QueryTypeOfExpression TypeOf(this QueryExpression expression)
		{
			return new QueryTypeOfExpression
			{
				Expression = expression
			};
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A221 File Offset: 0x00008421
		public static QueryPrimitiveTypeExpression PrimitiveType(this ConceptualPrimitiveType type)
		{
			return new QueryPrimitiveTypeExpression
			{
				Type = type
			};
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000A22F File Offset: 0x0000842F
		public static QueryTableTypeExpression TableType(this List<QueryExpressionContainer> columns)
		{
			return new QueryTableTypeExpression
			{
				Columns = columns
			};
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000A23D File Offset: 0x0000843D
		public static QueryExpressionContainer Container(this QueryExpression expression, string name)
		{
			return new QueryExpressionContainer(expression, name, null);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000A247 File Offset: 0x00008447
		public static QueryExpressionContainer Container(this QueryExpression expression)
		{
			return new QueryExpressionContainer(expression, null, null);
		}
	}
}
