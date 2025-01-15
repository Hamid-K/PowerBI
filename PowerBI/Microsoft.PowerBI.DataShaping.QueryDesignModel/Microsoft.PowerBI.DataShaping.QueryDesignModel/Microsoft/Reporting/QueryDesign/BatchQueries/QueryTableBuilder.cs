using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000270 RID: 624
	internal static class QueryTableBuilder
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x0004B13E File Offset: 0x0004933E
		public static QueryTableDefinition Sort(this QueryTable input, QuerySortClause first, params QuerySortClause[] rest)
		{
			return input.Sort(rest.Prepend(first));
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0004B150 File Offset: 0x00049350
		public static QueryTableDefinition Sort(this QueryTable input, IEnumerable<QuerySortClause> sortOrder)
		{
			QueryExpressionBinding queryExpressionBinding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			QuerySortExpression querySortExpression = queryExpressionBinding.Sort(QueryTableBuilder.RewriteSortClauses(input, queryExpressionBinding, sortOrder));
			return new QueryTableDefinition(input.Columns, querySortExpression, QdmNames.Sorted(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0004B198 File Offset: 0x00049398
		internal static IEnumerable<QuerySortClause> RewriteSortClauses(QueryTable input, QueryExpressionBinding binding, IEnumerable<QuerySortClause> sortOrder)
		{
			return sortOrder.Select((QuerySortClause c) => c.RewriteColumnReferences(input.Columns, binding.Variable));
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0004B1CC File Offset: 0x000493CC
		public static QueryTableDefinition Filter(this QueryTable input, QueryExpression predicate)
		{
			QueryFilterExpression queryFilterExpression = input.Expression as QueryFilterExpression;
			if (queryFilterExpression != null && QueryAlgorithms.CanMergePredicateWithPreviousFilter(predicate))
			{
				QueryExpressionBinding input2 = queryFilterExpression.Input;
				predicate = predicate.RewriteColumnReferences(input.Columns, input2.Variable);
				QueryFilterExpression queryFilterExpression2 = input2.Filter(queryFilterExpression.Predicate.And(predicate));
				return new QueryTableDefinition(input.Columns, queryFilterExpression2, input.BindingVariableNameSuggestion);
			}
			QueryExpressionBinding queryExpressionBinding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			predicate = predicate.RewriteColumnReferences(input.Columns, queryExpressionBinding.Variable);
			QueryFilterExpression queryFilterExpression3 = queryExpressionBinding.Filter(predicate);
			return new QueryTableDefinition(input.Columns, queryFilterExpression3, QdmNames.Filtered(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0004B278 File Offset: 0x00049478
		public static QueryTableDefinition TopN(this QueryTable input, int count, QuerySortClause first, params QuerySortClause[] rest)
		{
			QueryLiteralExpression queryLiteralExpression = QueryExpressionBuilder.Literal(count);
			return input.TopNInternal(queryLiteralExpression, null, rest.Prepend(first));
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0004B2A0 File Offset: 0x000494A0
		public static QueryTableDefinition TopN(this QueryTable input, QueryExpression count, QuerySortClause first, params QuerySortClause[] rest)
		{
			return input.TopNInternal(count, null, rest.Prepend(first));
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0004B2B4 File Offset: 0x000494B4
		public static QueryTableDefinition TopN(this QueryTable input, int count, IEnumerable<QuerySortClause> sortOrder)
		{
			QueryLiteralExpression queryLiteralExpression = QueryExpressionBuilder.Literal(count);
			return input.TopNInternal(queryLiteralExpression, null, sortOrder);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0004B2D6 File Offset: 0x000494D6
		public static QueryTableDefinition TopN(this QueryTable input, QueryExpression count, IEnumerable<QuerySortClause> sortOrder)
		{
			return input.TopNInternal(count, null, sortOrder);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0004B2E4 File Offset: 0x000494E4
		public static QueryTableDefinition TopNSkip(this QueryTable input, int count, int skipCount, IEnumerable<QuerySortClause> sortOrder)
		{
			QueryLiteralExpression queryLiteralExpression = QueryExpressionBuilder.Literal(count);
			QueryLiteralExpression queryLiteralExpression2 = QueryExpressionBuilder.Literal(skipCount);
			return input.TopNInternal(queryLiteralExpression, queryLiteralExpression2, sortOrder);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0004B312 File Offset: 0x00049512
		public static QueryTableDefinition TopNSkip(this QueryTable input, QueryExpression count, QueryExpression skipCount, IEnumerable<QuerySortClause> sortOrder)
		{
			return input.TopNInternal(count, skipCount, sortOrder);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0004B320 File Offset: 0x00049520
		private static QueryTableDefinition TopNInternal(this QueryTable input, QueryExpression count, QueryExpression skipCount, IEnumerable<QuerySortClause> sortOrder)
		{
			QueryExpressionBinding queryExpressionBinding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			QueryLimitExpression queryLimitExpression;
			if (skipCount != null)
			{
				queryLimitExpression = queryExpressionBinding.TopNSkip(count, skipCount, QueryTableBuilder.RewriteSortClauses(input, queryExpressionBinding, sortOrder));
			}
			else
			{
				queryLimitExpression = queryExpressionBinding.TopN(count, QueryTableBuilder.RewriteSortClauses(input, queryExpressionBinding, sortOrder));
			}
			return new QueryTableDefinition(input.Columns, queryLimitExpression, QdmNames.Limit(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0004B37C File Offset: 0x0004957C
		public static QueryTableDefinition Sample(this QueryTable input, int count, QuerySortClause first, params QuerySortClause[] rest)
		{
			return input.Sample(QueryExpressionBuilder.Literal(count), rest.Prepend(first));
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0004B398 File Offset: 0x00049598
		public static QueryTableDefinition SampleAxisWithLocalMinMax(this QueryTable input, QueryExpression maxTargetPointCount, QueryExpression axis, IReadOnlyList<QueryExpression> measures, QueryExpression minPointsResolution, IReadOnlyList<QueryExpression> series, DynamicSeriesSelectionCriteria dynamicSeriesSelectionCriteria, SortDirection dynamicSeriesSelectionCriteriaOrder, QueryExpression maxPointsResolution, QueryExpression maxDynamicSeriesCount)
		{
			QueryExpressionBinding queryExpressionBinding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			QueryExpression queryExpression = axis.RewriteColumnReferences(input.Columns, queryExpressionBinding.Variable);
			IReadOnlyList<QueryExpression> readOnlyList = QueryTableBuilder.RewriteExpressionCollectionColumnReferences(measures, input, queryExpressionBinding);
			IReadOnlyList<QueryExpression> readOnlyList2 = QueryTableBuilder.RewriteExpressionCollectionColumnReferences(series, input, queryExpressionBinding);
			QuerySampleAxisWithLocalMinMaxExpression querySampleAxisWithLocalMinMaxExpression = QueryExpressionBuilder.SampleAxisWithLocalMinMax(maxTargetPointCount, queryExpressionBinding, queryExpression, readOnlyList, minPointsResolution, readOnlyList2, dynamicSeriesSelectionCriteria, dynamicSeriesSelectionCriteriaOrder, maxPointsResolution, maxDynamicSeriesCount);
			return new QueryTableDefinition(input.Columns, querySampleAxisWithLocalMinMaxExpression, QdmNames.Limit(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0004B40C File Offset: 0x0004960C
		public static QueryTableDefinition SampleCartesianPointsByCover(this QueryTable input, QueryExpression maxTargetPointCount, QueryExpression x, QueryExpression y, QueryExpression radius, QueryExpression maxMinRatio, QueryExpression maxBlankRatio)
		{
			QueryExpressionBinding queryExpressionBinding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			QueryExpression queryExpression = ((x != null) ? x.RewriteColumnReferences(input.Columns, queryExpressionBinding.Variable) : null);
			QueryExpression queryExpression2 = ((y != null) ? y.RewriteColumnReferences(input.Columns, queryExpressionBinding.Variable) : null);
			QueryExpression queryExpression3 = ((radius != null) ? radius.RewriteColumnReferences(input.Columns, queryExpressionBinding.Variable) : null);
			QuerySampleCartesianPointsByCoverExpression querySampleCartesianPointsByCoverExpression = QueryExpressionBuilder.SampleCartesianPointsByCover(maxTargetPointCount, queryExpressionBinding, queryExpression, queryExpression2, queryExpression3, maxMinRatio, maxBlankRatio);
			return new QueryTableDefinition(input.Columns, querySampleCartesianPointsByCoverExpression, QdmNames.Limit(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0004B4A0 File Offset: 0x000496A0
		private static IReadOnlyList<QueryExpression> RewriteExpressionCollectionColumnReferences(IReadOnlyList<QueryExpression> expressions, QueryTable input, QueryExpressionBinding binding)
		{
			if (expressions == null)
			{
				return null;
			}
			return expressions.Select((QueryExpression e) => e.RewriteColumnReferences(input.Columns, binding.Variable)).ToReadOnlyCollection<QueryExpression>();
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0004B4DD File Offset: 0x000496DD
		public static QueryTableDefinition Sample(this QueryTable input, int count, IEnumerable<QuerySortClause> sortOrder)
		{
			return input.Sample(QueryExpressionBuilder.Literal(count), sortOrder);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0004B4F1 File Offset: 0x000496F1
		public static QueryTableDefinition Sample(this QueryTable input, QueryExpression count, QuerySortClause first, params QuerySortClause[] rest)
		{
			return input.Sample(count, rest.Prepend(first));
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0004B504 File Offset: 0x00049704
		public static QueryTableDefinition Sample(this QueryTable input, QueryExpression count, IEnumerable<QuerySortClause> sortOrder)
		{
			QueryExpressionBinding queryExpressionBinding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			QueryLimitExpression queryLimitExpression = queryExpressionBinding.Sample(count, QueryTableBuilder.RewriteSortClauses(input, queryExpressionBinding, sortOrder));
			return new QueryTableDefinition(input.Columns, queryLimitExpression, QdmNames.Limit(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0004B54C File Offset: 0x0004974C
		public static QueryTableDefinition Sample(this QueryTable input, QueryExpression count, string sortColumnName, SortDirection sortDirection)
		{
			QueryExpressionBinding queryExpressionBinding = input.ToBinding();
			QuerySortClause querySortClause = queryExpressionBinding.Variable.Field(sortColumnName).ToSortClause(sortDirection);
			QueryLimitExpression queryLimitExpression = queryExpressionBinding.Sample(count, QueryTableBuilder.RewriteSortClauses(input, queryExpressionBinding, querySortClause.AsEnumerable<QuerySortClause>()));
			return new QueryTableDefinition(input.Columns, queryLimitExpression, QdmNames.Limit(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0004B59F File Offset: 0x0004979F
		public static QueryTableDefinition Row(QueryTableColumn first, params QueryTableColumn[] rest)
		{
			return QueryTableBuilder.Row(rest.Prepend(first));
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0004B5B0 File Offset: 0x000497B0
		public static QueryTableDefinition Row(IEnumerable<QueryTableColumn> columns)
		{
			ReadOnlyCollection<QueryTableColumn> readOnlyCollection = columns.ToReadOnlyCollection<QueryTableColumn>();
			QueryNewTableExpression queryNewTableExpression = QueryExpressionBuilder.NewTable(readOnlyCollection.ToKeyValuePairs());
			return new QueryTableDefinition(readOnlyCollection, queryNewTableExpression, "Row");
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0004B5DA File Offset: 0x000497DA
		public static QueryTableDefinition TableScan(this IConceptualEntity targetEntity)
		{
			return QueryTableBuilder.TableScan((EntitySet)null, targetEntity);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0004B5E4 File Offset: 0x000497E4
		public static QueryTableDefinition TableScan(this EntitySet targetEntitySet, IConceptualEntity targetEntity = null)
		{
			QueryExpression queryExpression;
			if (targetEntity != null)
			{
				queryExpression = targetEntity.Scan(true);
			}
			else
			{
				queryExpression = targetEntitySet.Scan(true);
			}
			return new QueryTableDefinition(QueryTableBuilder.CreateTableColumnsFromResultType(queryExpression), queryExpression, "TableScan");
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0004B618 File Offset: 0x00049818
		public static QueryTableDefinition TableScan(this EntitySet targetEntitySet, IReadOnlyList<QueryTableColumn> expectedProjections, IConceptualEntity targetEntity = null)
		{
			List<QueryTableColumn> list = new List<QueryTableColumn>();
			QueryScanExpression queryScanExpression;
			if (targetEntity != null)
			{
				queryScanExpression = targetEntity.Scan(true);
				foreach (IConceptualProperty conceptualProperty in targetEntity.Properties)
				{
					IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
					if (conceptualColumn != null && conceptualColumn.IsStable)
					{
						QueryFieldExpression expression2 = conceptualColumn.QdmReference();
						if (!expectedProjections.Where((QueryTableColumn p) => p.Expression.Equals(expression2)).Any<QueryTableColumn>())
						{
							QueryTableColumn queryTableColumn = new QueryTableColumn(conceptualColumn.EdmName, expression2);
							list.Add(queryTableColumn);
						}
					}
				}
				list.AddRange(expectedProjections);
			}
			else
			{
				queryScanExpression = targetEntitySet.Scan(true);
				foreach (EdmField edmField in targetEntitySet.ElementType.Fields)
				{
					if (edmField.IsStable())
					{
						EdmFieldInstance edmFieldInstance = new EdmFieldInstance(targetEntitySet, edmField);
						QueryFieldExpression expression = edmFieldInstance.QdmReference(null);
						if (!expectedProjections.Where((QueryTableColumn p) => p.Expression.Equals(expression)).Any<QueryTableColumn>())
						{
							QueryTableColumn queryTableColumn2 = new QueryTableColumn(edmFieldInstance.QualifiedName.Name, expression);
							list.Add(queryTableColumn2);
						}
					}
				}
				list.AddRange(expectedProjections);
			}
			return new QueryTableDefinition(list, queryScanExpression, "TableScan");
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0004B7A4 File Offset: 0x000499A4
		public static QueryTableDefinition AsTableDefinition(this QueryExpression expression)
		{
			QueryFilterExpression queryFilterExpression = expression as QueryFilterExpression;
			if (queryFilterExpression != null)
			{
				return expression.AsTableDefinition(QdmNames.Filtered(queryFilterExpression.Input.Variable.VariableName));
			}
			if (expression is QueryTreatAsExpression)
			{
				return expression.AsTableDefinition(QdmNames.TreatAs(null));
			}
			QueryGroupByExpression queryGroupByExpression = expression as QueryGroupByExpression;
			QueryExpression queryExpression = ((queryGroupByExpression != null) ? queryGroupByExpression.Input.Expression : null) ?? expression;
			if (queryExpression is QueryAllExpression)
			{
				return expression.AsTableDefinition(QdmNames.All(null));
			}
			if (queryExpression is QueryScanExpression)
			{
				return expression.AsTableDefinition(QdmNames.Scan(null));
			}
			if (queryExpression is QueryProjectExpression)
			{
				return expression.AsTableDefinition(QdmNames.Projected(null));
			}
			if (expression is QueryCalculateExpression)
			{
				return expression.AsTableDefinition(QdmNames.CalculateTable(null));
			}
			if (expression is QueryGroupAndJoinExpression)
			{
				return expression.AsTableDefinition(QdmNames.GroupedAndJoined(null));
			}
			throw new InvalidOperationException("Unexpected expression type");
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0004B87C File Offset: 0x00049A7C
		public static QueryTableDefinition AsTableDefinition(this QueryExpression expression, string bindingVariableName)
		{
			return new QueryTableDefinition(Microsoft.Reporting.Util.EmptyReadOnlyCollection<QueryTableColumn>(), expression, bindingVariableName);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0004B88A File Offset: 0x00049A8A
		public static QueryTableDefinition Project(this QueryTable input, params QueryTableColumn[] projectedColumns)
		{
			return input.Project(projectedColumns, ProjectSubsetStrategy.Default);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0004B894 File Offset: 0x00049A94
		public static QueryTableDefinition AddProjection(this QueryTable input, IEnumerable<QueryTableColumn> newNamedColumns)
		{
			input = input.BackfillTableColumnsIfNeeded();
			List<QueryTableColumn> list = new List<QueryTableColumn>();
			list.AddRange(input.Columns.Select((QueryTableColumn c) => c.ToReferenceColumn()));
			list.AddRange(newNamedColumns);
			return input.Project(list, ProjectSubsetStrategy.Default);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0004B8EE File Offset: 0x00049AEE
		public static QueryTable BackfillTableColumnsIfNeeded(this QueryTable input)
		{
			if (input.Columns.Count > 0)
			{
				return input;
			}
			return new QueryTableDefinition(QueryTableBuilder.CreateTableColumnsFromResultType(input.Expression), input.Expression, input.BindingVariableNameSuggestion);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0004B91C File Offset: 0x00049B1C
		private static IReadOnlyList<QueryTableColumn> CreateTableColumnsFromResultType(QueryExpression expression)
		{
			ConceptualRowType rowType = ((ConceptualTableType)expression.ConceptualResultType).RowType;
			List<QueryTableColumn> list = new List<QueryTableColumn>(rowType.Columns.Count);
			foreach (ConceptualTypeColumn conceptualTypeColumn in rowType.Columns)
			{
				QueryTableColumn queryTableColumn = new QueryTableColumn(conceptualTypeColumn.Name, new QueryNonComposableExpression(conceptualTypeColumn.PrimitiveType));
				list.Add(queryTableColumn);
			}
			return list;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0004B9A4 File Offset: 0x00049BA4
		public static QueryTableDefinition Project(this QueryTable input, IEnumerable<QueryTableColumn> projectedColumns, ProjectSubsetStrategy projectSubsetStrategy = ProjectSubsetStrategy.Default)
		{
			QueryExpressionBinding binding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			projectedColumns = projectedColumns.Select((QueryTableColumn c) => c.RewriteProjectedColumnEntityPlaceholder(binding));
			ReadOnlyCollection<QueryTableColumn> readOnlyCollection = projectedColumns.Select((QueryTableColumn c) => c.RewriteColumnReferences(input.Columns, binding.Variable)).ToReadOnlyCollection<QueryTableColumn>();
			QueryNewInstanceExpression queryNewInstanceExpression = QueryExpressionBuilder.NewRow(readOnlyCollection.ToKeyValuePairs());
			QueryProjectExpression queryProjectExpression = binding.Project(queryNewInstanceExpression, projectSubsetStrategy);
			return new QueryTableDefinition(readOnlyCollection, queryProjectExpression, QdmNames.Projected(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0004BA3C File Offset: 0x00049C3C
		private static QueryTableColumn RewriteProjectedColumnEntityPlaceholder(this QueryTableColumn projectedColumn, QueryExpressionBinding binding)
		{
			QueryExpression queryExpression = projectedColumn.Expression.RewriteEntityPlaceholders(binding.Variable);
			if (queryExpression != projectedColumn.Expression)
			{
				return new QueryTableColumn(projectedColumn.Name, queryExpression);
			}
			return projectedColumn;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0004BA74 File Offset: 0x00049C74
		public static QueryTableDefinition EnsureUniqueUnqualifiedNames(this QueryTable input, bool forceRename)
		{
			QueryEnsureUniqueUnqualifiedNamesExpression queryEnsureUniqueUnqualifiedNamesExpression = input.Expression.EnsureUniqueUnqualifiedNames(forceRename);
			return new QueryTableDefinition(input.Columns, queryEnsureUniqueUnqualifiedNamesExpression, QdmNames.UniqueUnqualifiedNames(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0004BAA5 File Offset: 0x00049CA5
		public static QueryTableDefinition NaturalInnerJoin(this QueryTable left, QueryTable right)
		{
			return QueryTableBuilder.NaturalJoin(NaturalJoinKind.Inner, left, right);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0004BAAF File Offset: 0x00049CAF
		public static QueryTableDefinition NaturalLeftOuterJoin(this QueryTable left, QueryTable right)
		{
			return QueryTableBuilder.NaturalJoin(NaturalJoinKind.LeftOuter, left, right);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0004BABC File Offset: 0x00049CBC
		private static QueryTableDefinition NaturalJoin(NaturalJoinKind joinKind, QueryTable left, QueryTable right)
		{
			QueryNaturalJoinExpression queryNaturalJoinExpression = QueryExpressionBuilder.NaturalJoin(joinKind, left.ToBinding(), right.ToBinding());
			return new QueryTableDefinition(DaxFunctions.DetermineNaturalJoinResultColumns<QueryTableColumn>(left.Columns, right.Columns, (QueryTableColumn c) => c.Name).ToReadOnlyCollection<QueryTableColumn>(), queryNaturalJoinExpression, QdmNames.Join(left.BindingVariableNameSuggestion));
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0004BB22 File Offset: 0x00049D22
		public static QueryTableDefinition UnionAll(this QueryTable left, QueryTable right)
		{
			return QueryTableBuilder.UnionAll(new QueryTable[] { left, right });
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0004BB38 File Offset: 0x00049D38
		public static QueryTableDefinition UnionAll(IEnumerable<QueryTable> tables)
		{
			IList<QueryTable> list = tables.Evaluate<QueryTable>();
			IReadOnlyList<QueryTableColumn> columns = list[0].Columns;
			QueryUnionAllExpression queryUnionAllExpression = QueryExpressionBuilder.UnionAll(QueryTableBuilder.PrepareUnionInput(list, columns), TypeUnionBehavior.Upcast);
			return new QueryTableDefinition(columns, queryUnionAllExpression, QdmNames.Union(list[0].BindingVariableNameSuggestion));
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0004BB80 File Offset: 0x00049D80
		private static List<QueryExpression> PrepareUnionInput(IList<QueryTable> evaluatedTables, IReadOnlyList<QueryTableColumn> columns)
		{
			List<QueryExpression> list = new List<QueryExpression>();
			foreach (QueryTable queryTable in evaluatedTables)
			{
				QueryUnionAllExpression queryUnionAllExpression = queryTable.Expression as QueryUnionAllExpression;
				if (queryUnionAllExpression != null)
				{
					list.AddRange(queryUnionAllExpression.Tables);
				}
				else
				{
					list.Add(queryTable.Expression);
				}
			}
			return list;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0004BBF4 File Offset: 0x00049DF4
		public static QueryTableDefinition SubstituteWithIndex(this QueryTable table, string suggestedIndexColumnName, QueryTable indexTable, IEnumerable<QuerySortClause> indexTableSortOrder, out QueryTableColumn indexColumn, bool treatSuggestedIndexColumnNameAsUnique = false)
		{
			string text = suggestedIndexColumnName;
			if (!treatSuggestedIndexColumnNameAsUnique)
			{
				text = QueryNamingContext.CreateUniqueName(table.Columns.Select((QueryTableColumn c) => c.Name), suggestedIndexColumnName, null);
			}
			QueryExpressionBinding queryExpressionBinding = table.ToBinding();
			QueryExpressionBinding queryExpressionBinding2 = indexTable.ToBinding();
			IEnumerable<QuerySortClause> enumerable = QueryTableBuilder.RewriteSortClauses(indexTable, queryExpressionBinding2, indexTableSortOrder);
			QuerySubstituteWithIndexExpression querySubstituteWithIndexExpression = queryExpressionBinding.SubstituteWithIndex(text, queryExpressionBinding2, enumerable);
			indexColumn = BatchQdmExpressionBuilder.CreateDerivedColumn<long>(text);
			return new QueryTableDefinition(DaxFunctions.DetermineSubstituteWithIndexResultColumns<QueryTableColumn>(table.Columns, indexColumn, indexTable.Columns, (QueryTableColumn c) => c.Name).ToReadOnlyCollection<QueryTableColumn>(), querySubstituteWithIndexExpression, QdmNames.SubstituteWithIndex(table.BindingVariableNameSuggestion));
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0004BCAA File Offset: 0x00049EAA
		public static QueryTableDefinition GroupBy(this QueryTable input, params QueryTableColumn[] groupKeyColumns)
		{
			return input.GroupBy(groupKeyColumns, null);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0004BCB4 File Offset: 0x00049EB4
		public static QueryTableDefinition GroupBy(this QueryTable input, IEnumerable<QueryTableColumn> groupKeyColumns, IEnumerable<QueryTableColumn> aggregateColumns = null)
		{
			groupKeyColumns = groupKeyColumns.Evaluate<QueryTableColumn>();
			if (aggregateColumns == null)
			{
				aggregateColumns = Microsoft.Reporting.Util.EmptyArray<QueryTableColumn>();
			}
			else
			{
				aggregateColumns = aggregateColumns.Evaluate<QueryTableColumn>();
			}
			QueryGroupExpressionBinding inputBinding = input.Expression.GroupBindAs(input.BindingVariableNameSuggestion);
			IEnumerable<KeyValuePair<string, QueryExpression>> enumerable = groupKeyColumns.Select((QueryTableColumn c) => c.Expression.RewriteColumnReferences(input.Columns, inputBinding.Variable).As(c.Name));
			IEnumerable<KeyValuePair<string, QueryExpression>> enumerable2 = aggregateColumns.Select((QueryTableColumn c) => c.Expression.RewriteCurrentGroupPlaceholders(input, inputBinding).As(c.Name));
			QueryGroupByExpression queryGroupByExpression = inputBinding.GroupBy(enumerable, enumerable2);
			return new QueryTableDefinition(groupKeyColumns.Concat(aggregateColumns).ToReadOnlyCollection<QueryTableColumn>(), queryGroupByExpression, QdmNames.Grouped(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0004BD64 File Offset: 0x00049F64
		public static QueryTableDefinition CurrentGroup(this QueryTable input)
		{
			QdmCurrentGroupPlaceholderExpression qdmCurrentGroupPlaceholderExpression = new QdmCurrentGroupPlaceholderExpression(input);
			return new QueryTableDefinition(input.Columns, qdmCurrentGroupPlaceholderExpression, QdmNames.CurrentGroup(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0004BD90 File Offset: 0x00049F90
		public static QueryTableDefinition Calculate(this QueryTable input, IEnumerable<QueryExpression> filters)
		{
			QueryCalculateExpression queryCalculateExpression = input.Expression.Calculate(filters);
			return new QueryTableDefinition(input.Columns, queryCalculateExpression, QdmNames.Calculate(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0004BDC1 File Offset: 0x00049FC1
		private static IEnumerable<KeyValuePair<string, QueryExpression>> ToKeyValuePairs(this IEnumerable<QueryTableColumn> columns)
		{
			return columns.Select((QueryTableColumn c) => Microsoft.DataShaping.Util.ToKeyValuePair<string, QueryExpression>(c.Name, c.Expression));
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0004BDE8 File Offset: 0x00049FE8
		public static QueryTableDefinition GenerateAll(IEnumerable<QueryTable> tables)
		{
			Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> func;
			if ((func = QueryTableBuilder.<>O.<0>__GenerateAll) == null)
			{
				func = (QueryTableBuilder.<>O.<0>__GenerateAll = new Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression>(QueryExpressionBuilder.GenerateAll));
			}
			Func<string, string> func2;
			if ((func2 = QueryTableBuilder.<>O.<1>__FullOuterCrossJoin) == null)
			{
				func2 = (QueryTableBuilder.<>O.<1>__FullOuterCrossJoin = new Func<string, string>(QdmNames.FullOuterCrossJoin));
			}
			return QueryTableBuilder.ApplyJoin(tables, func, func2);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0004BE26 File Offset: 0x0004A026
		public static QueryTableDefinition GenerateAll(this QueryTable left, QueryTable right)
		{
			return QueryTableBuilder.GenerateAll(new QueryTable[] { left, right });
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0004BE3B File Offset: 0x0004A03B
		public static QueryTableDefinition CrossJoin(IEnumerable<QueryTable> tables)
		{
			Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> func;
			if ((func = QueryTableBuilder.<>O.<2>__CrossJoin) == null)
			{
				func = (QueryTableBuilder.<>O.<2>__CrossJoin = new Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression>(QueryExpressionBuilder.CrossJoin));
			}
			Func<string, string> func2;
			if ((func2 = QueryTableBuilder.<>O.<3>__CrossJoin) == null)
			{
				func2 = (QueryTableBuilder.<>O.<3>__CrossJoin = new Func<string, string>(QdmNames.CrossJoin));
			}
			return QueryTableBuilder.ApplyJoin(tables, func, func2);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0004BE79 File Offset: 0x0004A079
		public static QueryTableDefinition CrossJoin(this QueryTable left, QueryTable right)
		{
			return QueryTableBuilder.CrossJoin(new QueryTable[] { left, right });
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0004BE90 File Offset: 0x0004A090
		private static QueryTableDefinition ApplyJoin(IEnumerable<QueryTable> tables, Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> generateFunction, Func<string, string> namingFunction)
		{
			IList<QueryTable> list = tables.Evaluate<QueryTable>();
			List<QueryTableColumn> list3;
			List<QueryExpressionBinding> list2 = QueryTableBuilder.PrepareCrossJoinInput(list, out list3);
			QueryExpression queryExpression = generateFunction(list2);
			return new QueryTableDefinition(list3, queryExpression, namingFunction(list[0].BindingVariableNameSuggestion));
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0004BED0 File Offset: 0x0004A0D0
		private static List<QueryExpressionBinding> PrepareCrossJoinInput(IList<QueryTable> evaluatedTables, out List<QueryTableColumn> columns)
		{
			List<QueryExpressionBinding> list = new List<QueryExpressionBinding>();
			columns = new List<QueryTableColumn>();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (QueryTable queryTable in evaluatedTables)
			{
				columns.AddRange(queryTable.Columns);
				QueryCrossJoinExpression queryCrossJoinExpression = queryTable.Expression as QueryCrossJoinExpression;
				if (queryCrossJoinExpression != null)
				{
					list.AddRange(queryCrossJoinExpression.Inputs);
					hashSet.UnionWith(queryCrossJoinExpression.Inputs.Select((QueryExpressionBinding binding) => binding.Variable.VariableName));
				}
				else
				{
					QueryGenerateExpression queryGenerateExpression = queryTable.Expression as QueryGenerateExpression;
					if (queryGenerateExpression != null)
					{
						list.AddRange(queryGenerateExpression.Inputs);
						hashSet.UnionWith(queryGenerateExpression.Inputs.Select((QueryExpressionBinding binding) => binding.Variable.VariableName));
					}
					else
					{
						string text = QueryNamingContext.CreateUniqueName(hashSet, queryTable.BindingVariableNameSuggestion, null);
						list.Add(queryTable.Expression.BindAs(text));
						hashSet.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0004C00C File Offset: 0x0004A20C
		public static QueryTableDefinition Distinct(this QueryTable input)
		{
			QueryDistinctExpression queryDistinctExpression = input.Expression.Distinct();
			return new QueryTableDefinition(input.Columns, queryDistinctExpression, QdmNames.Distinct(input.BindingVariableNameSuggestion));
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0004C03C File Offset: 0x0004A23C
		public static QueryTableDefinition InvokeExtensionFunction(IReadOnlyList<QueryTableColumn> resultColumns, string functionName, IReadOnlyList<QueryExpression> arguments, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })] IReadOnlyList<global::System.ValueTuple<string, int>> resultColumnSourceArgumentIndices)
		{
			QueryExtensionFunctionExpression queryExtensionFunctionExpression = QueryExpressionBuilder.InvokeExtensionFunction(resultColumns.Select((QueryTableColumn c) => c.Column).ToList<ConceptualTypeColumn>().Row()
				.Table(), functionName, arguments, resultColumnSourceArgumentIndices);
			return new QueryTableDefinition(resultColumns, queryExtensionFunctionExpression, "ExtensionFunction");
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0004C094 File Offset: 0x0004A294
		public static QueryTableDefinition TreatAs(this QueryTable input, IReadOnlyList<QueryTableColumn> columns)
		{
			QueryExpressionBinding binding = input.Expression.BindAs(input.BindingVariableNameSuggestion);
			ReadOnlyCollection<QueryTableColumn> readOnlyCollection = columns.Select((QueryTableColumn c) => c.RewriteColumnReferences(input.Columns, binding.Variable)).ToReadOnlyCollection<QueryTableColumn>();
			QueryTreatAsExpression queryTreatAsExpression = input.Expression.TreatAs(readOnlyCollection.ToKeyValuePairs().ToReadOnlyCollection<KeyValuePair<string, QueryExpression>>());
			return new QueryTableDefinition(readOnlyCollection, queryTreatAsExpression, QdmNames.TreatAs(input.BindingVariableNameSuggestion));
		}

		// Token: 0x020003FF RID: 1023
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001445 RID: 5189
			public static Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> <0>__GenerateAll;

			// Token: 0x04001446 RID: 5190
			public static Func<string, string> <1>__FullOuterCrossJoin;

			// Token: 0x04001447 RID: 5191
			public static Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> <2>__CrossJoin;

			// Token: 0x04001448 RID: 5192
			public static Func<string, string> <3>__CrossJoin;
		}
	}
}
