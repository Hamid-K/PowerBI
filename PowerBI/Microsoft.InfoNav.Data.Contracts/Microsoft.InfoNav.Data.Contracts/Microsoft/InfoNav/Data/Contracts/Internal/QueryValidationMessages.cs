using System;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E7 RID: 743
	internal static class QueryValidationMessages
	{
		// Token: 0x060018BE RID: 6334 RVA: 0x0002C49E File Offset: 0x0002A69E
		internal static string TooManyInValuesForMappedParameterFilter(int maxNumberOfValues)
		{
			return StringUtil.FormatInvariant("Found an IN filter on a mapped column with too many values. In filters on mapped column support up to {0} values.", maxNumberOfValues);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0002C4B0 File Offset: 0x0002A6B0
		internal static string InvalidExpression(QueryExpressionContainer expression)
		{
			return StringUtil.FormatInvariant("Encountered invalid expression of type '{0}'", QueryValidationMessages.GetExpressionType(expression));
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0002C4C2 File Offset: 0x0002A6C2
		internal static string IncompatibleEntityAndSource(string entity, string source)
		{
			return StringUtil.FormatInvariant("Both an entity '{0}' and entity source '{1}' were specified", entity.MarkAsModelInfo(), source.MarkAsModelInfo());
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		internal static string InvalidEntitySource(EntitySource entitySource)
		{
			return StringUtil.FormatInvariant("Encountered invalid EntitySource of type '{0}'. Note: only Subquery and LetRef expressions are supported", (entitySource != null) ? entitySource.Type.ToString() : "null");
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0002C517 File Offset: 0x0002A717
		internal static string InvalidEntitySourceMissingName()
		{
			return StringUtil.FormatInvariant("Encountered invalid EntitySource. {0}", "Missing Name");
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0002C528 File Offset: 0x0002A728
		internal static string InvalidQueryParameterDeclarationMissingExpression(string parameterName)
		{
			return StringUtil.FormatInvariant("The query parameter declaration '{0}' is invalid. The Expression must be specified.", parameterName.MarkAsModelInfo());
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0002C53A File Offset: 0x0002A73A
		internal static string InvalidQueryParameterDeclarationMissingName()
		{
			return StringUtil.FormatInvariant("Encountered invalid parameter declaration. {0}", "Missing Name");
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0002C54B File Offset: 0x0002A74B
		internal static string InvalidQueryParameterDeclarationInvalidExpressionType(string parameterName, QueryExpressionContainer expression)
		{
			return StringUtil.FormatInvariant("The query parameter declaration '{0}' has an invalid Expression of type '{1}'. Parameter declaration expressions must be type expressions.", parameterName.MarkAsModelInfo(), QueryValidationMessages.GetExpressionType(expression));
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0002C563 File Offset: 0x0002A763
		internal static string InvalidQueryTableTypeColumnInvalidExpressionType(string typeColumnName, QueryExpressionContainer expression)
		{
			return StringUtil.FormatInvariant("Encountered invalid QueryTableTypeExpression. The table type column '{0}' has an invalid Expression of type '{1}'. Table type column expressions must be type expressions and must not be table type expressions.", typeColumnName.MarkAsModelInfo(), QueryValidationMessages.GetExpressionType(expression));
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0002C57B File Offset: 0x0002A77B
		internal static string InvalidQueryTableTypeColumnDuplicateName(string typeColumnName)
		{
			return StringUtil.FormatInvariant("Encountered invalid QueryTableTypeExpression. The table type has multiple columns with name '{0}'. All columns in a table type must have a unique name.", typeColumnName.MarkAsModelInfo());
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0002C58D File Offset: 0x0002A78D
		internal static string InvalidQueryTypeOfExpressionType(QueryExpressionContainer expression)
		{
			return StringUtil.FormatInvariant("Encountered invalid QueryTypeOfExpression. The inner expression is of type '{0}'. TypeOf expressions contain a Column, Measure, or HierarchyLevel expression.", QueryValidationMessages.GetExpressionType(expression));
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0002C59F File Offset: 0x0002A79F
		internal static string InvalidLetBindingMissingName()
		{
			return StringUtil.FormatInvariant("Encountered invalid let binding. {0}", "Missing Name");
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
		internal static string InvalidTransformMissingName()
		{
			return StringUtil.FormatInvariant("Encountered invalid Transform. {0}", "Missing Name");
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0002C5C1 File Offset: 0x0002A7C1
		internal static string InvalidTransformInputParameterMissingName()
		{
			return StringUtil.FormatInvariant("Encountered invalid QueryTransformInput Parameter. {0}", "Missing Name");
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0002C5D2 File Offset: 0x0002A7D2
		internal static string InvalidTransformTableMissingName()
		{
			return StringUtil.FormatInvariant("Encountered invalid QueryTransformTable. {0}", "Missing Name");
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0002C5E3 File Offset: 0x0002A7E3
		internal static string InvalidTransformTableColumnExpressionMissingName()
		{
			return StringUtil.FormatInvariant("Encountered invalid QueryTransformTableColumn.Expression. {0}", "Missing Name");
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0002C5F4 File Offset: 0x0002A7F4
		private static string GetExpressionType(QueryExpressionContainer expression)
		{
			if (!(expression != null) || !(expression.Expression != null))
			{
				return "null";
			}
			return expression.Expression.GetType().Name;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0002C623 File Offset: 0x0002A823
		internal static string InvalidComparisonKind(QueryComparisonKind comparisonKind)
		{
			return StringUtil.FormatInvariant("Encountered invalid ComparisonKind. '{0}' is invalid", comparisonKind);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0002C635 File Offset: 0x0002A835
		internal static string NonStringOperand(string type)
		{
			return StringUtil.FormatInvariant("Encountered invalid {0}. Left and Right operands should be strings", type);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0002C642 File Offset: 0x0002A842
		internal static string NullOrEmptyProperty(object parent, string property)
		{
			return StringUtil.FormatInvariant("The property {0} is null or empty on {1} ", property, parent.GetType().Name);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0002C65A File Offset: 0x0002A85A
		internal static string InvalidChildExpression(string parent)
		{
			return StringUtil.FormatInvariant("Specific type(s) of expression on the ExpressionContainer of {0} should not be null", parent);
		}

		// Token: 0x040008B4 RID: 2228
		internal static readonly string InvalidIncludeAllTypesBehavior = "Invalid or unsupported IncludeAllTypes behavior.";

		// Token: 0x040008B5 RID: 2229
		internal static readonly string InvalidQueryDefinition = "Invalid or malformed QueryDefinition";

		// Token: 0x040008B6 RID: 2230
		internal static readonly string InvalidSkipValue = "Invalid Skip value. Only values >= 0 are allowed";

		// Token: 0x040008B7 RID: 2231
		internal static readonly string ExpressionMissingSubquery = "QueryDefinition is missing in SubqueryExpression";

		// Token: 0x040008B8 RID: 2232
		internal static readonly string EntityMissingInStandaloneExpression = "Entity is missing in standalone QueryExpression";

		// Token: 0x040008B9 RID: 2233
		internal static readonly string EntitySourceMissing = "Entity source is missing in QueryExpression";

		// Token: 0x040008BA RID: 2234
		internal static readonly string SkipWithoutTop = "Skip is present but Top is not. Skip requires an explicit Top";

		// Token: 0x040008BB RID: 2235
		internal static readonly string FilterDefinitionMissingEntitySources = "FilterDefinition is missing required entity sources";

		// Token: 0x040008BC RID: 2236
		internal static readonly string GroupByInSubquery = "The query contains a GroupBy inside a subquery. GroupBy is not allowed inside a subquery, it is only allowed on top level queries";

		// Token: 0x040008BD RID: 2237
		internal static readonly string MissingFrom = "The QueryDefinition From is missing or empty. A semantic query from must contain at least one source";

		// Token: 0x040008BE RID: 2238
		internal static readonly string MissingSelect = "The QueryDefinition Select is missing or empty. A semantic query must contain at least one Select";

		// Token: 0x040008BF RID: 2239
		internal static readonly string InvalidTopValue = "Invalid Top value. Only values > 0 are allowed";

		// Token: 0x040008C0 RID: 2240
		internal static readonly string NullLetBindingExpression = "Encountered invalid LetBinding. LetBinding.Expression is null";

		// Token: 0x040008C1 RID: 2241
		internal static readonly string NullSortClause = "QuerySortClause is null";

		// Token: 0x040008C2 RID: 2242
		internal static readonly string NullSortClauseExpression = "Encountered invalid QuerySortClause. SortClauseExpression is null";

		// Token: 0x040008C3 RID: 2243
		internal static readonly string InvalidSortClauseDirection = "Encountered invalid QuerySortClause. SortClauseDirection is not valid";

		// Token: 0x040008C4 RID: 2244
		internal static readonly string NullParameterDeclaration = "Parameter declaration is null";

		// Token: 0x040008C5 RID: 2245
		internal static readonly string NullLetBinding = "LetBinding is null";

		// Token: 0x040008C6 RID: 2246
		internal static readonly string NullFilter = "Filter is null";

		// Token: 0x040008C7 RID: 2247
		internal static readonly string NullAxis = "Axis is null";

		// Token: 0x040008C8 RID: 2248
		internal static readonly string InvalidAxisName = "Encountered invalid Axis. The Name is null or otherwise not valid";

		// Token: 0x040008C9 RID: 2249
		internal static readonly string DuplicatedAxisName = "Encountered multiple Axis with the same Name \"{0}\". Axes must have unique names.";

		// Token: 0x040008CA RID: 2250
		internal static readonly string InvalidAxisGroups = "Encountered invalid Axis. Groups are null or empty";

		// Token: 0x040008CB RID: 2251
		internal static readonly string NullAxisGroup = "Axis Group is null";

		// Token: 0x040008CC RID: 2252
		internal static readonly string InvalidAxisGroupKeys = "Encountered invalid Axis. Groups are null or empty";

		// Token: 0x040008CD RID: 2253
		internal static readonly string InvalidFilterCondition = "Encountered invalid Filter. The Condition is null or is not valid";

		// Token: 0x040008CE RID: 2254
		internal static readonly string InvalidFilterTarget = "Encountered invalid Filter. The Target is null or is not valid. Expressions with SourceRef, or with Property, or with HierarchyLevel are supported";

		// Token: 0x040008CF RID: 2255
		internal static readonly string NullTransform = "Transform is null";

		// Token: 0x040008D0 RID: 2256
		internal static readonly string InvalidTransformMissingAlgorithm = "Encountered invalid Transform. Missing Algorithm";

		// Token: 0x040008D1 RID: 2257
		internal static readonly string NullTransformInput = "TransformInput is null";

		// Token: 0x040008D2 RID: 2258
		internal static readonly string NullTransformInputParameter = "Encountered invalid Transform. The TransformParameter in TransformInput is null";

		// Token: 0x040008D3 RID: 2259
		internal static readonly string NullTransformInputParameterExpression = "Encountered invalid Transform. The TransformParameter Expression in TransformInput is null";

		// Token: 0x040008D4 RID: 2260
		internal static readonly string NullTransformInputTable = "Encountered invalid Transform. QueryTransformInput.Table is null";

		// Token: 0x040008D5 RID: 2261
		internal static readonly string NullTransformOutput = "Encountered invalid Transform. TransformOutput is null";

		// Token: 0x040008D6 RID: 2262
		internal static readonly string NullTransformOutputTable = "Encountered invalid Transform. QueryTransformOutput.Table is null";

		// Token: 0x040008D7 RID: 2263
		internal static readonly string InvalidTransformTableColumns = "Encountered invalid Transform. QueryTransformTableColumn.Expression is null or expty";

		// Token: 0x040008D8 RID: 2264
		internal static readonly string NullTransformTableColumn = "Encountered invalid Transform. QueryTransformTableColumn is null";

		// Token: 0x040008D9 RID: 2265
		internal static readonly string NullTransformTableColumnExpression = "Encountered invalid Transform. QueryTransformTableColumn.Expression is null";

		// Token: 0x040008DA RID: 2266
		internal static readonly string InvalidAggregation = "Encountered invalid QueryAggregationExpression. Function is not valid";

		// Token: 0x040008DB RID: 2267
		internal static readonly string InvalidFloor = "Encountered invalid QueryFloorExpression. Size should be bigger than 0";

		// Token: 0x040008DC RID: 2268
		internal static readonly string InvalidDatePart = "Encountered invalid QueryDatePartExpression. Function is not valid";

		// Token: 0x040008DD RID: 2269
		internal static readonly string InvalidPercentile = "Encountered invalid QueryPercentileExpression. When Exclusive is true K should be in (0, 1) and when Exclusive is false K should be in [0, 1]";

		// Token: 0x040008DE RID: 2270
		internal static readonly string InvalidDiscretize = "Encountered invalid QueryDiscretizeExpression. Count should be bigger than 0";

		// Token: 0x040008DF RID: 2271
		internal static readonly string InvalidSparklineDataPointsPerSparkline = "Encountered invalid QuerySparklineDataExpression. PointsPerSparkline should be bigger than 0";

		// Token: 0x040008E0 RID: 2272
		internal static readonly string InvalidArithmeticExpression = "Encountered invalid QueryArithmeticExpression. Operator is not valid";

		// Token: 0x040008E1 RID: 2273
		internal static readonly string InvalidQueryInExpressionExpressions = "Encountered invalid QueryInExpression. Expressions is null or empty";

		// Token: 0x040008E2 RID: 2274
		internal static readonly string InvalidQueryInExpressionValuesWithTable = "Encountered invalid QueryInExpression. Values and Table cannot both be present";

		// Token: 0x040008E3 RID: 2275
		internal static readonly string InvalidQueryInExpressionValues = "Encountered invalid QueryInExpression. Values is null or empty or the expression count in one list of values does not match the expression count in Expressions";

		// Token: 0x040008E4 RID: 2276
		internal static readonly string InvalidQueryInExpressionTable = "Encountered invalid QueryInExpression. Table.SourceRef is null";

		// Token: 0x040008E5 RID: 2277
		internal static readonly string InvalidQueryInExpressionEqualityKind = "Encountered invalid QueryInExpression. Equalitykind cannot be respected when a table is given as input.";

		// Token: 0x040008E6 RID: 2278
		internal static readonly string InvalidQueryInExpressionNoValuesNoTable = "Encountered invalid QueryInExpression. Either Values or Table must be present.";

		// Token: 0x040008E7 RID: 2279
		internal static readonly string InvalidQueryTableTypeColumnMissingName = "Encountered invalid QueryTableTypeExpression. Each table type column must have a name.";

		// Token: 0x040008E8 RID: 2280
		internal static readonly string InvalidScopedEvalExpression = "Encountered invalid QueryScopedEvalExpression. Scope is null or for expression in Scope Column and HierarchyLevel are both null";

		// Token: 0x040008E9 RID: 2281
		internal static readonly string InvalidFilteredEvalExpression = "Encountered invalid QueryFilteredEvalExpression. Filters is null or empty";

		// Token: 0x040008EA RID: 2282
		internal static readonly string InvalidDecadeConstant = "Encountered invalid QueryDecadeConstantExpression. Value % 10 should be 0";

		// Token: 0x040008EB RID: 2283
		internal static readonly string InvalidYearAndMonthConstant = "Encountered invalid QueryYearAndMonthConstantExpression. Year is less than 0 or Month is less than or equal to 0 or Month is bigger than or equal to 13";

		// Token: 0x040008EC RID: 2284
		internal static readonly string InvalidYearConstant = "Encountered invalid QueryYearConstantExpression. Value should be bigger than or equal to 0";

		// Token: 0x040008ED RID: 2285
		internal static readonly string InvalidLiteralExpression = "Encountered invalid QueryLiteralExpression. Failed to parse type encoded string";

		// Token: 0x040008EE RID: 2286
		internal static readonly string InvalidDateSpan = "Encountered invalid QueryDateSpanExpression. TimeUnit is not valid";

		// Token: 0x040008EF RID: 2287
		internal static readonly string InvalidPrimitiveTypeInvalidType = "Encountered invalid QueryPrimitiveTypeExpression. Type is invalid. Type must be a valid value of ConceptualPrimitiveType, and Type must not be Null or None.";

		// Token: 0x040008F0 RID: 2288
		internal static readonly string NullExpressionContainer = "Encountered null QueryExpressionContainer";

		// Token: 0x040008F1 RID: 2289
		internal static readonly string EmptyExpressionContainer = "Encountered a QueryExpressionContainer that does not contain an expression. A QueryExpressionContainer must assign an expression to a single property of the correct type.";

		// Token: 0x040008F2 RID: 2290
		internal static readonly string MultipleParameterMappingsUnsupported = "Found multiple parameters mapped to a specified column. Only a 1:1 Column:Parameter mapping is currently supported.";

		// Token: 0x040008F3 RID: 2291
		internal static readonly string FoundParameterMappingOnUnsupportedFilter = "A column with a parameter mapping was used in an unsupported filter.";

		// Token: 0x040008F4 RID: 2292
		internal static readonly string CannotUseMultiColumnFilteringWithMappedColumns = "Cannot use mapped parameters with multiple columns in an IN expression.";

		// Token: 0x040008F5 RID: 2293
		internal static readonly string InTableConditionNotSupportedWithMappedParameters = "The query contains a filter with an In expression where the Expressions refers to a column with a parameter mapping and a Table expression. In expressions must not contain a Table expression and a reference to a parameter column.";

		// Token: 0x040008F6 RID: 2294
		internal static readonly string InvalidMappedParameterValueExpression = "Found a non-literal expression as a value for a mapped parameter. Columns with mapped parameters only support literal expressions.";

		// Token: 0x040008F7 RID: 2295
		internal static readonly string FoundMultipleTargetsWithAtLeastOneMapping = "Found multiple targets, one of which had a parameter mapping. If a mapping exists within the targets, there can only be one target.";

		// Token: 0x040008F8 RID: 2296
		internal static readonly string MappedTargetMustMatchMappedCondition = "Found a target with a column mapping that does not match the condition column mapping. If a target exists and has a mapped parameter, the condition must have the same mapped parameter.";

		// Token: 0x040008F9 RID: 2297
		internal static readonly string DifferentMappingsWithinSubqueries = "Found multiple subqueries with different parameter mappings. When parameter mappings are used within subqueries, they must all match.";

		// Token: 0x040008FA RID: 2298
		internal static readonly string ParameterMappingsFoundWithinFilteredEvalExpressionFilter = "Found parameter mappings within a filter on a filtered eval expression. No parameter mappings are allowed on filters within filtered eval expressions.";

		// Token: 0x040008FB RID: 2299
		internal static readonly string MultipleParameterAssignmentsWithinORUnsupported = "Found multiple parameters referenced within an OR expression. Only one parameter may be used per OR expression.";

		// Token: 0x040008FC RID: 2300
		internal static readonly string MixOfAssignedAndUnassignedColumnsWithinORExpression = "Found a mix of assigned and unassigned columns within an OR expression.";
	}
}
