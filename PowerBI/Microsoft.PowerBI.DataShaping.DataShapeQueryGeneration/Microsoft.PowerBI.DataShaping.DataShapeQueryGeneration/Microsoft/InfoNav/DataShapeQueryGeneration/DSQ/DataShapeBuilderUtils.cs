using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x02000106 RID: 262
	internal static class DataShapeBuilderUtils
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x00022490 File Offset: 0x00020690
		internal static void AddGroupSorting(DataShapeBuilderContext context, IReadOnlyList<DataMemberBuilderPair> dynamics, IReadOnlyList<QueryMember> queryMembers, bool isPrimary, bool contextOnly, bool hasNestedHierarchyGroups = false)
		{
			for (int i = 0; i < queryMembers.Count; i++)
			{
				GroupBuilder<DataMemberBuilder<DataMember>> groupBuilder = dynamics[i].GroupBuilder;
				QueryMember queryMember = queryMembers[i];
				QueryGroup group = queryMember.Group;
				bool flag = hasNestedHierarchyGroups || (queryMembers.Count > 1 && i < queryMembers.Count - 1);
				foreach (DsqSortKey dsqSortKey in group.SortKeys)
				{
					groupBuilder.WithSortKey(context, queryMember, dsqSortKey, flag, isPrimary, contextOnly, i);
				}
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00022538 File Offset: 0x00020738
		internal static bool TryBuildDynamicMembers(DataShapeBuilderContext context, QueryTranslationContext queryTranslationContext, DataShapeBuilder dataShape, IReadOnlyList<QueryMember> queryMembers, DsqScopeLookup dsqScopeLookup, bool hasHighlightFilters, bool isPrimary, bool needStaticsForAggregates, bool contextOnlyDataShape, out IReadOnlyList<DataMemberBuilderPair> readOnlyDynamicMembers)
		{
			readOnlyDynamicMembers = null;
			List<DataMemberBuilderPair> list = new List<DataMemberBuilderPair>(queryMembers.Count);
			bool flag = false;
			for (int i = 0; i < queryMembers.Count; i++)
			{
				QueryMember queryMember = queryMembers[i];
				QueryGroup group = queryMember.Group;
				bool flag2 = !needStaticsForAggregates && group.Subtotal == SubtotalType.None;
				DataMemberBuilderPair dataMemberBuilderPair;
				if (i == 0)
				{
					dataMemberBuilderPair = dataShape.WithDynamicMember(context, queryMember, isPrimary, flag2);
				}
				else
				{
					dataMemberBuilderPair = list[i - 1].Dynamic.WithDynamicMember(context, queryMember, isPrimary, flag2);
				}
				string text = ((group.Subtotal != SubtotalType.None && !group.IsSubtotalContextOnly) ? dataMemberBuilderPair.Static.Result.Id.Value : null);
				string value = dataMemberBuilderPair.Dynamic.Result.Id.Value;
				bool flag3 = contextOnlyDataShape || dataMemberBuilderPair.QueryMember.IsContextOnly;
				dataMemberBuilderPair.GroupBuilder.WithGrouping(context, group.Keys, group.DetailGroupIdentity, i, isPrimary, value, text, group.Subtotal, flag3);
				foreach (QueryGroupValue queryGroupValue in queryMember.Values)
				{
					dataMemberBuilderPair.Dynamic.WithGroupCalculation(context, queryGroupValue, i, isPrimary, flag3);
				}
				foreach (ProjectedDsqExpression projectedDsqExpression in queryMember.MeasureCalculations)
				{
					dataMemberBuilderPair.Dynamic.WithMeasureCalculation(context, projectedDsqExpression, hasHighlightFilters, new int?(i));
				}
				if (!queryMember.InstanceFilters.IsNullOrEmpty<FilterDefinition>())
				{
					List<FilterCondition> list2;
					if (!QueryFilters.TryResolveAndCreateFilterConditions(queryTranslationContext, queryMember.InstanceFilters, DataShapeGenerationErrorCode.CouldNotResolveInstanceFilter, SemanticQueryObjectType.InstanceFilter, FilterRestrictions.InstanceFilters, DsqFilterConditionGenerationOptions.ForInstanceFilters, out list2))
					{
						return false;
					}
					dataMemberBuilderPair.Dynamic.WithInstanceFilters(list2);
				}
				if (dataMemberBuilderPair.QueryMember.HasExplicitSubtotal)
				{
					DataMemberBuilder @static = dataMemberBuilderPair.Static;
					if (@static != null)
					{
						@static.WithExplicitSubtotal();
					}
				}
				bool flag4 = flag || dataMemberBuilderPair.QueryMember.IsContextOnly;
				if (dataMemberBuilderPair.QueryMember.Group.IsSubtotalContextOnly || flag4)
				{
					DataMemberBuilder static2 = dataMemberBuilderPair.Static;
					if (static2 != null)
					{
						static2.WithContextOnlyTrue();
					}
					if (flag4)
					{
						dataMemberBuilderPair.Dynamic.WithContextOnlyTrue();
						flag = true;
					}
				}
				list.Add(dataMemberBuilderPair);
				dsqScopeLookup.AddQueryMemberToDsqMemberMapping(queryMember, dataMemberBuilderPair.Dynamic, isPrimary);
			}
			readOnlyDynamicMembers = list;
			return true;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000227B8 File Offset: 0x000209B8
		internal static void CreatePrimaryHierarchyMeasuresOnly(DataShapeBuilderContext context, QueryProjections projections, DataShapeBuilder dataShape, bool hasHighlightFilters)
		{
			DataMemberBuilder<DataShapeBuilder<DataShape>> dataMemberBuilder = dataShape.WithPrimaryMember(context.CreateMemberId(), null);
			foreach (ProjectedDsqExpression projectedDsqExpression in projections.Measures)
			{
				dataMemberBuilder.WithMeasureCalculation(context, projectedDsqExpression, hasHighlightFilters, null);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00022830 File Offset: 0x00020A30
		internal static List<DataMemberBuilderPair> GetLeafDynamics(IReadOnlyList<DataMemberBuilderPair> dynamicMembers)
		{
			List<DataMemberBuilderPair> list = new List<DataMemberBuilderPair>();
			foreach (DataMemberBuilderPair dataMemberBuilderPair in dynamicMembers)
			{
				if (dataMemberBuilderPair.Dynamic.Result.DataMembers == null)
				{
					list.Add(dataMemberBuilderPair);
				}
			}
			return list;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00022894 File Offset: 0x00020A94
		internal static bool TryBuildStrictLimit(QueryTranslationContext queryTranslationContext, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics)
		{
			int? top = queryTranslationContext.QueryDefinition.Top;
			if (top != null)
			{
				List<Expression> list;
				if (!DataShapeBuilderUtils.ValidateStrictLimit(queryTranslationContext, primaryDynamics, secondaryDynamics, out list))
				{
					return false;
				}
				dataShapeBuilder.WithLimit(dataShapeBuilderContext.CreateLimitId(), list, dataShapeBuilder.Parent().Id.StructureReference()).WithTop(top.Value, queryTranslationContext.QueryDefinition.Skip, new bool?(true));
			}
			return true;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002290C File Offset: 0x00020B0C
		internal static bool ValidateStrictLimit(QueryTranslationContext queryTranslationContext, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, out List<Expression> targets)
		{
			targets = null;
			if (!secondaryDynamics.IsNullOrEmpty<DataMemberBuilderPair>())
			{
				queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.QueryTopWithSecondary(EngineMessageSeverity.Error));
				return false;
			}
			if (primaryDynamics.IsNullOrEmpty<DataMemberBuilderPair>())
			{
				queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.QueryTopWithoutPrimary(EngineMessageSeverity.Error));
				return false;
			}
			targets = new List<Expression>();
			foreach (DataMemberBuilderPair dataMemberBuilderPair in primaryDynamics)
			{
				targets.Add(dataMemberBuilderPair.Dynamic.Id.StructureReference());
			}
			return true;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000229B4 File Offset: 0x00020BB4
		internal static bool TryBuildFilters(QueryTranslationContext queryTranslationContext, DataShapeBuilderContext dataShapeBuilderContext, QueryFilters queryFilters, DataShapeBuilder dataShape)
		{
			if (!queryFilters.Filters.IsNullOrEmptyCollection<Filter>())
			{
				foreach (Filter filter in queryFilters.Filters)
				{
					dataShape.WithFilter(filter);
				}
			}
			IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> projectionFilters = queryFilters.ProjectionFilters;
			Dictionary<string, FilterCondition> dictionary = new Dictionary<string, FilterCondition>();
			if (!projectionFilters.IsNullOrEmpty<KeyValuePair<int, HashSet<ResolvedQueryFilter>>>())
			{
				foreach (KeyValuePair<int, HashSet<ResolvedQueryFilter>> keyValuePair in projectionFilters)
				{
					FilterCondition filterCondition;
					if (!queryFilters.TryCreateFilterCondition(queryTranslationContext, keyValuePair.Value.AsReadOnlyList<ResolvedQueryFilter>(), new ExpressionContext(queryTranslationContext.QueryDefinition.Name, SemanticQueryObjectType.ExpressionFilter, keyValuePair.Key), FilterRestrictions.Expressions, out filterCondition))
					{
						return false;
					}
					Expression selectIndexExpression = dataShapeBuilderContext.GetSelectIndexExpression(new int?(keyValuePair.Key));
					dataShape.WithFilter(selectIndexExpression, FilterUsageKind.Default).WithFilterCondition(filterCondition);
					SelectBinding selectBindingForIndex = dataShapeBuilderContext.GetSelectBindingForIndex(keyValuePair.Key);
					if (selectBindingForIndex != null)
					{
						DataShapeBuilderUtils.AddDynamicFormatAndHighlightFilters(selectBindingForIndex, dataShape, dictionary, filterCondition);
					}
				}
			}
			if (queryFilters.HasHighlightFilter)
			{
				ReadOnlyCollection<Identifier> highlightCalculations = dataShapeBuilderContext.GetHighlightCalculations();
				if (highlightCalculations != null)
				{
					for (int i = 0; i < highlightCalculations.Count; i++)
					{
						Identifier identifier = highlightCalculations[i];
						FilterCondition filterCondition2;
						if (!dictionary.TryGetValue(identifier.Value, out filterCondition2))
						{
							filterCondition2 = null;
						}
						FilterCondition filterCondition3;
						if (!queryFilters.TryCreateHighlightFilters(queryTranslationContext, filterCondition2, out filterCondition3))
						{
							return false;
						}
						dataShape.WithFilter(identifier.StructureReference(), FilterUsageKind.Default).WithFilterCondition(filterCondition3);
					}
				}
			}
			return true;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00022B60 File Offset: 0x00020D60
		private static void AddDynamicFormatAndHighlightFilters(SelectBinding selectBinding, DataShapeBuilder dataShape, Dictionary<string, FilterCondition> projectionFiltersForHighlight, FilterCondition projectionFilter)
		{
			if (selectBinding.DynamicFormat != null)
			{
				if (selectBinding.DynamicFormat.Format != null)
				{
					dataShape.WithFilter(selectBinding.DynamicFormat.Format.StructureReference(), FilterUsageKind.Default).WithFilterCondition(projectionFilter);
				}
				if (selectBinding.DynamicFormat.Culture != null)
				{
					dataShape.WithFilter(selectBinding.DynamicFormat.Culture.StructureReference(), FilterUsageKind.Default).WithFilterCondition(projectionFilter);
				}
			}
			if (selectBinding.Highlight != null)
			{
				projectionFiltersForHighlight.Add(selectBinding.Highlight.Value, projectionFilter);
				if (selectBinding.Highlight.DynamicFormat != null)
				{
					if (selectBinding.Highlight.DynamicFormat.Format != null)
					{
						projectionFiltersForHighlight.Add(selectBinding.Highlight.DynamicFormat.Format, projectionFilter);
					}
					if (selectBinding.Highlight.DynamicFormat.Culture != null)
					{
						projectionFiltersForHighlight.Add(selectBinding.Highlight.DynamicFormat.Culture, projectionFilter);
					}
				}
			}
		}
	}
}
