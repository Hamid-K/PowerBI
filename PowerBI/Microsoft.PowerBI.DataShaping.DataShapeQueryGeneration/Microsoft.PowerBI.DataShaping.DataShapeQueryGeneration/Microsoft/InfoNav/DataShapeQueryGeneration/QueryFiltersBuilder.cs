using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A1 RID: 161
	internal sealed class QueryFiltersBuilder
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x00017074 File Offset: 0x00015274
		internal QueryFilters ToFilters()
		{
			if (this._filterConditionsByIdType.IsNullOrEmpty<KeyValuePair<QueryFiltersBuilder.FilterKey, List<FilterCondition>>>() && this._highlightFilters.IsNullOrEmptyCollection<FilterDefinition>() && this._projectionFilters.IsNullOrEmpty<KeyValuePair<int, HashSet<ResolvedQueryFilter>>>())
			{
				return QueryFilters.Empty;
			}
			return new QueryFilters(QueryFiltersBuilder.FlattenAndWrapAllByFilterKeys(this._filterConditionsByIdType), this._highlightFilters.AsReadOnlyCollection<FilterDefinition>(), this._projectionFilters);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000170D0 File Offset: 0x000152D0
		private static IList<Filter> FlattenAndWrapAllByFilterKeys(Dictionary<QueryFiltersBuilder.FilterKey, List<FilterCondition>> filterConditionsById)
		{
			if (filterConditionsById.IsNullOrEmpty<KeyValuePair<QueryFiltersBuilder.FilterKey, List<FilterCondition>>>())
			{
				return null;
			}
			List<Filter> list = new List<Filter>();
			foreach (KeyValuePair<QueryFiltersBuilder.FilterKey, List<FilterCondition>> keyValuePair in filterConditionsById)
			{
				QueryFiltersBuilder.FilterKey key = keyValuePair.Key;
				DsqFilterType? type = key.Type;
				if (type != null && QueryFiltersBuilder._uncombinableFilterType == type.Value)
				{
					QueryFiltersBuilder.AddConditions(list, keyValuePair.Value, key);
				}
				else
				{
					FilterCondition filterCondition = QueryFiltersBuilder.FlattenAndWrapAll(keyValuePair.Value);
					list.Add(new Filter
					{
						Condition = filterCondition,
						Target = key.Id.StructureReference(),
						UsageKind = key.UsageKind
					});
				}
			}
			return list;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000171A8 File Offset: 0x000153A8
		internal static FilterCondition FlattenAndWrapAll(ICollection<FilterCondition> filterConditions)
		{
			if (filterConditions.IsNullOrEmptyCollection<FilterCondition>())
			{
				return null;
			}
			if (filterConditions.Count == 1)
			{
				return filterConditions.First<FilterCondition>();
			}
			List<FilterCondition> list = new List<FilterCondition>(filterConditions.Count);
			QueryFiltersBuilder.FlattenNestedConditionsToParentConditionsList(filterConditions, list);
			return new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = list
			};
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000171FC File Offset: 0x000153FC
		private static void FlattenNestedConditionsToParentConditionsList(ICollection<FilterCondition> filterConditions, List<FilterCondition> flattenedFilterConditions)
		{
			Stack<FilterCondition> stack = new Stack<FilterCondition>(filterConditions.Reverse<FilterCondition>());
			while (stack.Count > 0)
			{
				FilterCondition filterCondition = stack.Pop();
				CompoundFilterCondition compoundFilterCondition = filterCondition as CompoundFilterCondition;
				if (compoundFilterCondition != null && compoundFilterCondition.Operator == CompoundFilterOperator.All)
				{
					QueryFiltersBuilder.PushConditionsToStack(stack, compoundFilterCondition);
				}
				else
				{
					flattenedFilterConditions.Add(filterCondition);
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00017254 File Offset: 0x00015454
		private static void PushConditionsToStack(Stack<FilterCondition> filterConditionsToVisit, CompoundFilterCondition compoundFilterCondition)
		{
			foreach (FilterCondition filterCondition in compoundFilterCondition.Conditions.Reverse<FilterCondition>())
			{
				filterConditionsToVisit.Push(filterCondition);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000172A8 File Offset: 0x000154A8
		private static void AddConditions(IList<Filter> filters, List<FilterCondition> filterConditions, QueryFiltersBuilder.FilterKey filterKey)
		{
			if (filterConditions.IsNullOrEmpty<FilterCondition>())
			{
				return;
			}
			StructureReferenceExpressionNode structureReferenceExpressionNode = filterKey.Id.StructureReference();
			foreach (FilterCondition filterCondition in filterConditions)
			{
				filters.Add(new Filter
				{
					Condition = filterCondition,
					Target = structureReferenceExpressionNode,
					UsageKind = filterKey.UsageKind
				});
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00017330 File Offset: 0x00015530
		internal void Add(GeneratedFilter filter, FilterUsageKind usageKind)
		{
			if (filter.Condition == null)
			{
				return;
			}
			if (this._filterConditionsByIdType == null)
			{
				this._filterConditionsByIdType = new Dictionary<QueryFiltersBuilder.FilterKey, List<FilterCondition>>();
			}
			QueryFiltersBuilder.FilterKey filterKey = new QueryFiltersBuilder.FilterKey(filter.TargetScope, filter.FilterType, usageKind);
			this._filterConditionsByIdType.Add(filterKey, filter.Condition);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00017383 File Offset: 0x00015583
		internal void SetProjectionFilters(IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> projectionFilters)
		{
			this._projectionFilters = projectionFilters;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001738C File Offset: 0x0001558C
		internal void SetHighlightFilters(IList<FilterDefinition> highlightFilters)
		{
			this._highlightFilters = highlightFilters;
		}

		// Token: 0x04000339 RID: 825
		private static DsqFilterType _uncombinableFilterType = DsqFilterType.Apply;

		// Token: 0x0400033A RID: 826
		private IList<FilterDefinition> _highlightFilters;

		// Token: 0x0400033B RID: 827
		private IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> _projectionFilters;

		// Token: 0x0400033C RID: 828
		private Dictionary<QueryFiltersBuilder.FilterKey, List<FilterCondition>> _filterConditionsByIdType;

		// Token: 0x02000144 RID: 324
		private struct FilterKey
		{
			// Token: 0x060009A2 RID: 2466 RVA: 0x00025BE3 File Offset: 0x00023DE3
			public FilterKey(Identifier id, DsqFilterType? type, FilterUsageKind usageKind)
			{
				this.Id = id;
				this.Type = type;
				this.UsageKind = usageKind;
			}

			// Token: 0x04000521 RID: 1313
			public Identifier Id;

			// Token: 0x04000522 RID: 1314
			public DsqFilterType? Type;

			// Token: 0x04000523 RID: 1315
			public FilterUsageKind UsageKind;
		}
	}
}
