using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C1 RID: 193
	internal sealed class QuerySortGenerator
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0001A640 File Offset: 0x00018840
		private QuerySortGenerator(IList<DsqSortKey> orderBy, BitArray isMeasure, QueryScopedSortAdapter scopedSortAdapter)
		{
			this._orderBy = orderBy;
			this._isMeasure = isMeasure;
			this._handledOrderBys = new BitArray(orderBy.Count);
			this._scopedSortAdapter = scopedSortAdapter;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001A66E File Offset: 0x0001886E
		internal static QuerySortGenerator CreateEmptySortGenerator()
		{
			return new QuerySortGenerator(Util.EmptyReadOnlyCollection<DsqSortKey>(), new BitArray(0), new QueryScopedSortAdapter());
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001A688 File Offset: 0x00018888
		internal static bool TryParse(QueryTranslationContext context, bool omitOrderBy, out QuerySortGenerator sort)
		{
			IReadOnlyList<ResolvedQuerySortClause> orderBy = context.QueryDefinition.OrderBy;
			if (orderBy == null || orderBy.Count == 0 || omitOrderBy)
			{
				sort = QuerySortGenerator.CreateEmptySortGenerator();
				return true;
			}
			List<DsqSortKey> list = new List<DsqSortKey>(orderBy.Count);
			BitArray bitArray = new BitArray(0);
			QueryScopedSortAdapter queryScopedSortAdapter = new QueryScopedSortAdapter();
			IList<QuerySortDirection?> list2 = null;
			if (context.DataShapeBinding != null && context.DataShapeBinding.OrderBy != null)
			{
				list2 = context.DataShapeBinding.OrderBy.Overrides;
			}
			for (int i = 0; i < orderBy.Count; i++)
			{
				ResolvedQuerySortClause resolvedQuerySortClause = orderBy[i];
				IList<ExpressionNode> list3;
				bool flag;
				bool flag2;
				if (!QuerySortGenerator.TryGetSortExpressions(context, resolvedQuerySortClause.Expression, i, out list3, out flag, out flag2))
				{
					context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedOrderByExpression(EngineMessageSeverity.Error, i));
					sort = null;
					return false;
				}
				QuerySortDirection querySortDirection = resolvedQuerySortClause.Direction;
				if (list2 != null && list2.Count > i)
				{
					QuerySortDirection? querySortDirection2 = list2[i];
					if (querySortDirection2 != null)
					{
						querySortDirection = querySortDirection2.Value;
					}
				}
				for (int j = 0; j < list3.Count; j++)
				{
					ExpressionNode expressionNode = list3[j];
					SortDirection sortDirection = QuerySortGenerator.Convert(querySortDirection);
					DsqSortKey dsqSortKey = list.FirstOrDefault(expressionNode);
					if (dsqSortKey == null)
					{
						DsqSortKeyExpression dsqSortKeyExpression = new DsqSortKeyExpression(expressionNode, sortDirection, flag, null, flag2);
						list.Add(dsqSortKeyExpression);
						BitArray bitArray2 = bitArray;
						BitArray bitArray3 = bitArray;
						int length = bitArray3.Length;
						bitArray3.Length = length + 1;
						bitArray2[length] = flag;
						queryScopedSortAdapter.AddSortKey(list.Count - 1, dsqSortKeyExpression);
					}
					else if (dsqSortKey.Direction != sortDirection)
					{
						context.SharedContext.Tracer.SanitizedTrace(TraceLevel.Warning, "DSQGeneration: Expression requires conflicting sort direction: {0}", new string[] { expressionNode.ToString().MarkAsCustomerContent() });
					}
				}
			}
			sort = new QuerySortGenerator(list, bitArray, queryScopedSortAdapter);
			return true;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001A868 File Offset: 0x00018A68
		internal static bool TryParseNonMeasureSort(QueryTranslationContext context, out QuerySortGenerator nonMeasureSorts)
		{
			QuerySortGenerator querySortGenerator;
			if (QuerySortGenerator.TryParse(context, false, out querySortGenerator))
			{
				List<DsqSortKey> list = new List<DsqSortKey>();
				List<bool> list2 = new List<bool>();
				QueryScopedSortAdapter queryScopedSortAdapter = new QueryScopedSortAdapter();
				for (int i = 0; i < querySortGenerator._orderBy.Count; i++)
				{
					if (!querySortGenerator._isMeasure[i])
					{
						list.Add(querySortGenerator._orderBy[i]);
						list2.Add(false);
						queryScopedSortAdapter.AddSortKey(i, querySortGenerator._orderBy[i]);
					}
				}
				nonMeasureSorts = new QuerySortGenerator(list, new BitArray(list2.ToArray()), queryScopedSortAdapter);
				return true;
			}
			nonMeasureSorts = null;
			return false;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001A908 File Offset: 0x00018B08
		internal bool HasMeasure
		{
			get
			{
				using (IEnumerator enumerator = this._isMeasure.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if ((bool)enumerator.Current)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001A964 File Offset: 0x00018B64
		private static bool TryGetSortExpressions(QueryTranslationContext context, ResolvedQueryExpression expression, int orderByIndex, out IList<ExpressionNode> sortExpressions, out bool hasAggregate, out bool isScoped)
		{
			DsqExpressionGenerator expressions = context.Expressions;
			IConceptualColumn conceptualColumn;
			if (expression.TryGetAsProperty(out conceptualColumn))
			{
				QuerySortGenerator.AddSortExpressions<IConceptualColumn>(expressions, conceptualColumn, QueryConceptualColumnAdapter.Instance, out sortExpressions);
				hasAggregate = false;
				isScoped = false;
				return true;
			}
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (expressions.TryGetAsTransformColumn(expression, out intermediateQueryTransformTableColumn))
			{
				if (intermediateQueryTransformTableColumn.UnderlyingConceptualColumn != null)
				{
					QuerySortGenerator.AddSortExpressions<IntermediateQueryTransformTableColumn>(expressions, intermediateQueryTransformTableColumn, QueryTransformColumnAdapter.Instance, out sortExpressions);
					hasAggregate = false;
					isScoped = false;
					return true;
				}
				if (intermediateQueryTransformTableColumn.UnderlyingExpression is ResolvedQueryTransformOutputRoleRefExpression)
				{
					context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidSortByTransformOutputRoleRef(EngineMessageSeverity.Error));
					sortExpressions = null;
					hasAggregate = false;
					isScoped = false;
					return false;
				}
			}
			ExpressionContext expressionContext = new ExpressionContext(context.QueryDefinition.Name, SemanticQueryObjectType.OrderBy, orderByIndex);
			GeneratedDsqExpression generatedDsqExpression;
			if (!ResolvedQueryExpressionValidator.Validate(expression, context.SharedContext.ErrorContext, AllowedExpressionContent.OrderBy, expressionContext) || !expressions.TryGenerate(expression, out generatedDsqExpression))
			{
				sortExpressions = null;
				hasAggregate = false;
				isScoped = false;
				return false;
			}
			hasAggregate = generatedDsqExpression.HasAggregate;
			sortExpressions = generatedDsqExpression.Expression.ArrayWrap<ExpressionNode>();
			isScoped = generatedDsqExpression.ExpressionContent.HasFlag(ExpressionContent.ScopedEval);
			return true;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001AA70 File Offset: 0x00018C70
		private static void AddSortExpressions<TColumn>(DsqExpressionGenerator expressions, TColumn column, QueryColumnAdapter<TColumn> adapter, out IList<ExpressionNode> sortExpressions)
		{
			sortExpressions = new List<ExpressionNode>();
			IConceptualColumn conceptualColumn = adapter.GetConceptualColumn(column);
			QuerySortGenerator.AddSortExpressions<TColumn>(sortExpressions, column, adapter, conceptualColumn.OrderByColumns, expressions);
			QuerySortGenerator.AddSortExpression(sortExpressions, adapter.ToDsqExpression(column));
			QuerySortGenerator.AddSortExpressions<TColumn>(sortExpressions, column, adapter, conceptualColumn.Grouping.QueryGroupColumns, expressions);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		private static void AddSortExpressions<TColumn>(IList<ExpressionNode> sortExpressions, TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, IReadOnlyList<IConceptualColumn> newColumns, DsqExpressionGenerator expressionGenerator)
		{
			if (newColumns.IsNullOrEmpty<IConceptualColumn>())
			{
				return;
			}
			for (int i = 0; i < newColumns.Count; i++)
			{
				IConceptualColumn conceptualColumn = newColumns[i];
				TColumn orCreateColumn = adapter.GetOrCreateColumn(conceptualColumn, existingColumn, expressionGenerator, false);
				ExpressionNode expressionNode = adapter.ToDsqExpression(orCreateColumn);
				QuerySortGenerator.AddSortExpression(sortExpressions, expressionNode);
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001AB0A File Offset: 0x00018D0A
		private static void AddSortExpression(IList<ExpressionNode> sortExpressions, ExpressionNode candidate)
		{
			if (!sortExpressions.Contains(candidate))
			{
				sortExpressions.Add(candidate);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001AB1C File Offset: 0x00018D1C
		private static SortDirection Convert(QuerySortDirection direction)
		{
			if (direction == QuerySortDirection.Descending)
			{
				return SortDirection.Descending;
			}
			return SortDirection.Ascending;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001AB28 File Offset: 0x00018D28
		internal void Rebind(ProjectedDsqExpression projection)
		{
			for (int i = 0; i < this._orderBy.Count; i++)
			{
				DsqSortKey dsqSortKey;
				if (this._orderBy[i].TryRebind(projection, out dsqSortKey))
				{
					this._orderBy[i] = dsqSortKey;
				}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001AB70 File Offset: 0x00018D70
		internal IReadOnlyList<DsqSortKey> DetermineGroupSortKeys(IReadOnlyList<QueryGroupKey> groupKeys, IReadOnlyList<QueryGroupValue> groupValues, IReadOnlyList<QueryGroupSortKey> sortKeys, bool allowCustomMeasureSorts, bool suppressAutomaticGroupSorts)
		{
			List<DsqSortKey> list = new List<DsqSortKey>(this._orderBy.Count + groupKeys.Count);
			for (int i = 0; i < this._orderBy.Count; i++)
			{
				DsqSortKey dsqSortKey = this._orderBy[i];
				if (this.IsUsableMeasureSort(allowCustomMeasureSorts, i) || groupKeys.Contains(dsqSortKey) || groupValues.Contains(dsqSortKey))
				{
					if (this._scopedSortAdapter.IsScopedSortKey(i))
					{
						DsqSortKey dsqSortKey2;
						if (this._scopedSortAdapter.TryGetScopedSortKeyForGroup(i, dsqSortKey, groupKeys, out dsqSortKey2))
						{
							list.Add(dsqSortKey2);
						}
					}
					else
					{
						list.Add(dsqSortKey);
					}
					this._handledOrderBys[i] = true;
				}
			}
			foreach (QueryGroupSortKey queryGroupSortKey in sortKeys)
			{
				if (!list.Contains(queryGroupSortKey.Expression))
				{
					list.Add(new DsqSortKeyExpression(queryGroupSortKey.Expression, SortDirection.Ascending, false, queryGroupSortKey.SelectIndex, false));
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001AC80 File Offset: 0x00018E80
		internal IReadOnlyList<DsqSortKey> DetermineDefaultSortKeys(IReadOnlyList<QueryGroupKey> groupKeys, IReadOnlyList<QueryGroupValue> groupValues)
		{
			List<DsqSortKey> list = new List<DsqSortKey>(groupKeys.Count);
			for (int i = 0; i < this._orderBy.Count; i++)
			{
				DsqSortKey dsqSortKey = this._orderBy[i];
				if (groupKeys.Contains(dsqSortKey) || groupValues.Contains(dsqSortKey))
				{
					list.Add(dsqSortKey);
				}
			}
			for (int j = 0; j < groupKeys.Count; j++)
			{
				ExpressionNode expression = groupKeys[j].Expression;
				if (!list.Contains(expression))
				{
					list.Add(new DsqSortKeyExpression(expression, SortDirection.Ascending, false, groupKeys[j].SelectIndex, false));
				}
			}
			return list;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001AD1B File Offset: 0x00018F1B
		private bool IsUsableMeasureSort(bool allowCustomSorts, int i)
		{
			return allowCustomSorts && this._isMeasure[i];
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001AD30 File Offset: 0x00018F30
		private static bool AreGroupKeysSubsetOfSortKeys(IReadOnlyList<DsqSortKey> orderBy, IReadOnlyList<QueryGroupKey> groupKeys)
		{
			for (int i = 0; i < groupKeys.Count; i++)
			{
				if (!orderBy.Contains(groupKeys[i].Expression))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001AD68 File Offset: 0x00018F68
		internal void RegisterUnhandledOrderBy(DataShapeGenerationErrorContext context)
		{
			for (int i = 0; i < this._handledOrderBys.Count; i++)
			{
				if (!this._handledOrderBys[i])
				{
					context.Register(DataShapeGenerationMessages.IgnoredOrderBy(EngineMessageSeverity.Warning, this._orderBy[i]));
				}
			}
		}

		// Token: 0x040003B0 RID: 944
		private readonly BitArray _handledOrderBys;

		// Token: 0x040003B1 RID: 945
		private readonly IList<DsqSortKey> _orderBy;

		// Token: 0x040003B2 RID: 946
		private readonly BitArray _isMeasure;

		// Token: 0x040003B3 RID: 947
		private readonly QueryScopedSortAdapter _scopedSortAdapter;
	}
}
