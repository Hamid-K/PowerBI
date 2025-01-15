using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F7 RID: 503
	internal static class PlanOperationBuilder
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x00046FB4 File Offset: 0x000451B4
		public static PlanOperationContext DeclareIfNotDeclared(this PlanOperationContext context, string name, PlanDeclarationCollection declarations, bool canExpandToMultiTables = false, string namingContextId = null, bool useGlobalNamingContext = false)
		{
			PlanOperationDeclarationReference planOperationDeclarationReference = context.Table.DeclareIfNotDeclared(name, declarations, canExpandToMultiTables, false, namingContextId, useGlobalNamingContext);
			return context.ReplaceTable(planOperationDeclarationReference, null, null, null);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00046FE0 File Offset: 0x000451E0
		public static PlanOperationDeclarationReference DeclareIfNotDeclared(this PlanOperation table, string name, PlanDeclarationCollection declarations, bool canExpandToMultiTables = false, bool isFragmentOfExistingDeclaration = false, string namingContextId = null, bool useGlobalNamingContext = false)
		{
			PlanOperationDeclarationReference planOperationDeclarationReference = table as PlanOperationDeclarationReference;
			if (planOperationDeclarationReference != null)
			{
				return planOperationDeclarationReference;
			}
			return declarations.DeclareTable(name, table, canExpandToMultiTables, isFragmentOfExistingDeclaration, namingContextId, useGlobalNamingContext);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00047008 File Offset: 0x00045208
		public static PlanOperation AddMissingItems(this PlanOperation input, IEnumerable<PlanGroupByMember> groups, IEnumerable<PlanGroupByMember> showAllMembers, IEnumerable<PlanOperation> contextTables)
		{
			return new PlanOperationAddMissingItems(input, groups, showAllMembers, contextTables);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00047013 File Offset: 0x00045213
		public static PlanOperation AddMissingItemsCompatPattern(this IEnumerable<PlanGroupByMember> showAllMembers, IReadOnlyList<Calculation> measureJoinConstraints, IReadOnlyList<PlanOperation> contextTables, bool allowBlankRow)
		{
			return new PlanOperationAddMissingItemsCompatPattern(showAllMembers, measureJoinConstraints, contextTables, allowBlankRow);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0004701E File Offset: 0x0004521E
		public static PlanOperationContext EnsureUniqueUnqualifiedNames(this PlanOperationContext input, bool forceRename)
		{
			return input.ReplaceTable(input.Table.EnsureUniqueUnqualifiedNames(forceRename), null, null, null);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00047035 File Offset: 0x00045235
		public static PlanOperation EnsureUniqueUnqualifiedNames(this PlanOperation input, bool forceRename)
		{
			return new PlanOperationEnsureUniqueUnqualifiedNames(input, forceRename);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0004703E File Offset: 0x0004523E
		public static PlanOperationContext SortBy(this PlanOperationContext input, IEnumerable<PlanSortItem> sorts)
		{
			return input.ReplaceTable(input.Table.SortBy(sorts), null, null, null);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00047055 File Offset: 0x00045255
		public static PlanOperation SortBy(this PlanOperation input, IEnumerable<PlanSortItem> sorts)
		{
			if (sorts.Any<PlanSortItem>())
			{
				return new PlanOperationSortBy(input, sorts);
			}
			return input;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00047068 File Offset: 0x00045268
		public static PlanOperation SingleRow(IEnumerable<Calculation> calculations = null, IEnumerable<PlanOperation> contextTables = null, IEnumerable<ExistsFilterItem> existsFilters = null, IEnumerable<SingleRowAdditionalColumn> additionalColumns = null)
		{
			return new PlanOperationSingleRow(calculations, contextTables, existsFilters, additionalColumns);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00047074 File Offset: 0x00045274
		public static IEnumerable<PlanSortItem> ToSortItems(this IEnumerable<DataMember> members, DataShapeAnnotations annotations, bool includeSubtotals = true)
		{
			return members.SelectMany((DataMember m) => m.ToSortItems(annotations, includeSubtotals));
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000470A7 File Offset: 0x000452A7
		public static IEnumerable<PlanSortItem> ToSortItems(this DataMember member, DataShapeAnnotations annotations, bool includeSubtotals = true)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (includeSubtotals && annotations.TryGetBatchSubtotalAnnotation(member, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInOutput())
			{
				yield return new PlanColumnSortItem(batchSubtotalAnnotation.SubtotalIndicatorColumnName, batchSubtotalAnnotation.SortDirection);
			}
			if (member.Group.SortKeys.IsNullOrEmpty<SortKey>())
			{
				yield break;
			}
			yield return new PlanMemberSortItem(member);
			yield break;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000470C5 File Offset: 0x000452C5
		public static PlanSortItem ToAllColumnsSortItems(this PlanOperation planOp, SortDirection sortDirection = SortDirection.Ascending)
		{
			return new PlanAllColumnsSortItem(sortDirection);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000470D0 File Offset: 0x000452D0
		public static PlanOperationContext TopN(this PlanOperationContext input, PlanExpression rowCountExpr, IEnumerable<PlanSortItem> sorts, bool reverseSortOrder = false)
		{
			PlanOperation planOperation = input.Table.TopN(rowCountExpr, sorts, reverseSortOrder);
			return input.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000470F8 File Offset: 0x000452F8
		public static PlanOperation TopN(this PlanOperation input, PlanExpression rowCountExpr, IEnumerable<PlanSortItem> sorts, bool reverseSortOrder = false)
		{
			PlanOperationTopN planOperationTopN = input as PlanOperationTopN;
			int num;
			int num2;
			if (planOperationTopN != null && sorts.SequenceEqual(planOperationTopN.Sorts) && PlanOperationBuilder.TryExtractInt32Literal(rowCountExpr, out num) && PlanOperationBuilder.TryExtractInt32Literal(planOperationTopN.RowCount, out num2))
			{
				if (reverseSortOrder == planOperationTopN.ReverseSortOrder)
				{
					if (num2 < num)
					{
						return input;
					}
					input = planOperationTopN.Input;
				}
				else if (num >= num2)
				{
					return input;
				}
			}
			return new PlanOperationTopN(input, rowCountExpr, sorts, reverseSortOrder);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00047160 File Offset: 0x00045360
		private static bool TryExtractInt32Literal(PlanExpression planExpr, out int value)
		{
			LiteralExpressionNode literalExpressionNode = planExpr.Expression as LiteralExpressionNode;
			if (literalExpressionNode != null && literalExpressionNode.Value.IsOfType<int>())
			{
				value = literalExpressionNode.Value.CastValue<int>();
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000471A2 File Offset: 0x000453A2
		public static PlanOperation TopNSkip(this PlanOperation input, PlanExpression rowCount, PlanExpression skipCount, IEnumerable<PlanSortItem> sorts)
		{
			return new PlanOperationTopNSkip(input, rowCount, skipCount, sorts, false);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000471AE File Offset: 0x000453AE
		public static PlanOperation Sample(this PlanOperation input, PlanExpression rowCount, IEnumerable<PlanSortItem> sorts)
		{
			return new PlanOperationSample(input, rowCount, sorts);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000471B8 File Offset: 0x000453B8
		public static PlanOperation BinnedLineSample(this PlanOperation input, PlanBinnedLineSampleItem axis, IReadOnlyList<PlanBinnedLineSampleItem> measures, IReadOnlyList<PlanBinnedLineSampleMember> series, PlanExpression targetPointCount, PlanExpression minPointsPerSeries, PlanExpression maxPointsPerSeries, PlanExpression maxDynamicSeriesCount)
		{
			return new PlanOperationBinnedLineSample(input, axis, measures, series, targetPointCount, minPointsPerSeries, maxPointsPerSeries, maxDynamicSeriesCount);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000471CB File Offset: 0x000453CB
		public static PlanOperation TopNPerLevelSample(this PlanOperation input, PlanExpression rowCount, string restartIndicatorColumnName, IReadOnlyList<PlanMemberSortItem> sortItems, IReadOnlyList<IReadOnlyList<Expression>> levelsList, LimitWindowExpansionInstance windowExpansionInstance)
		{
			return new PlanOperationTopNPerLevelSample(input, rowCount, restartIndicatorColumnName, sortItems, levelsList, windowExpansionInstance);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000471DA File Offset: 0x000453DA
		public static PlanBinnedLineSampleMember ToBinnedLineSampleItem(this DataMember member)
		{
			return new PlanBinnedLineSampleMember(member);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000471E2 File Offset: 0x000453E2
		public static IReadOnlyList<PlanBinnedLineSampleMember> ToBinnedLineSampleItems(this IReadOnlyList<DataMember> members)
		{
			Func<DataMember, PlanBinnedLineSampleMember> func;
			if ((func = PlanOperationBuilder.<>O.<0>__ToBinnedLineSampleItem) == null)
			{
				func = (PlanOperationBuilder.<>O.<0>__ToBinnedLineSampleItem = new Func<DataMember, PlanBinnedLineSampleMember>(PlanOperationBuilder.ToBinnedLineSampleItem));
			}
			return members.Select(func).ToList<PlanBinnedLineSampleMember>();
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0004720A File Offset: 0x0004540A
		public static PlanBinnedLineSampleCalculation ToBinnedLineSampleItem(this Calculation calculation)
		{
			return new PlanBinnedLineSampleCalculation(calculation);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00047212 File Offset: 0x00045412
		public static IReadOnlyList<PlanBinnedLineSampleCalculation> ToBinnedLineSampleItems(this IReadOnlyList<Calculation> calcs)
		{
			Func<Calculation, PlanBinnedLineSampleCalculation> func;
			if ((func = PlanOperationBuilder.<>O.<1>__ToBinnedLineSampleItem) == null)
			{
				func = (PlanOperationBuilder.<>O.<1>__ToBinnedLineSampleItem = new Func<Calculation, PlanBinnedLineSampleCalculation>(PlanOperationBuilder.ToBinnedLineSampleItem));
			}
			return calcs.Select(func).ToList<PlanBinnedLineSampleCalculation>();
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0004723A File Offset: 0x0004543A
		public static PlanOperation OverlappingPointsSample(this PlanOperation input, PlanExpression x, PlanExpression y, PlanExpression targetPointCount)
		{
			return new PlanOperationOverlappingPointsSample(input, x, y, targetPointCount);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00047245 File Offset: 0x00045445
		public static PlanOperation ApplyStartPosition(this PlanOperation input, IEnumerable<DataMember> members, RestartMatchingBehavior? restartMatchingBehavior)
		{
			return new PlanOperationApplyStartPosition(input, members, restartMatchingBehavior);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0004724F File Offset: 0x0004544F
		public static PlanOperation FilterBy(this PlanOperation input, FilterCondition condition)
		{
			return new PlanOperationFilterBy(input, condition);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00047258 File Offset: 0x00045458
		public static PlanOperation FilterBy(this PlanOperation input, PlanExpression predicate)
		{
			return new PlanOperationFilterBy(input, predicate);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00047261 File Offset: 0x00045461
		public static PlanOperationContext FilterBy(this PlanOperationContext input, PlanExpression predicate)
		{
			return input.ReplaceTable(input.Table.FilterBy(predicate), null, null, null);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00047278 File Offset: 0x00045478
		public static PlanOperationContext FilterBy(this PlanOperationContext input, FilterCondition filterCondition, PlanOperationFilteringMetadata filteringMetadata = null)
		{
			return input.ReplaceTable(input.Table.FilterBy(filterCondition), null, filteringMetadata, null);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0004728F File Offset: 0x0004548F
		public static PlanOperation CreateFilterContextTable(this FilterCondition condition)
		{
			return new PlanOperationCreateFilterContextTable(condition);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00047297 File Offset: 0x00045497
		public static PlanOperation CalculateTableInFilterContext(this PlanOperation input, IEnumerable<PlanOperation> contextTables)
		{
			return new PlanOperationCalculateTableInFilterContext(input, contextTables);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000472A0 File Offset: 0x000454A0
		public static PlanOperation GroupBy(this PlanOperation input, IEnumerable<PlanGroupByItem> items)
		{
			return input.GroupBy(items, Enumerable.Empty<PlanAggregateItem>());
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000472AE File Offset: 0x000454AE
		public static PlanOperation GroupBy(this PlanOperation input, IEnumerable<PlanGroupByItem> items, IEnumerable<PlanAggregateItem> aggregates)
		{
			return new PlanOperationGroupBy(input, items, aggregates);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000472B8 File Offset: 0x000454B8
		public static IEnumerable<PlanGroupByMember> ToGroupByItems(this IEnumerable<DataMember> members, DataShapeAnnotations annotations, SubtotalUsage includedSubtotalKind, bool includeSortByMeasureKeysAtMeasureScope, bool excludeMeasureSortKeys, IFilterDeclarationCollection instanceFilterDeclarations = null)
		{
			return members.Select((DataMember m) => m.ToGroupByItem(annotations, includedSubtotalKind, includeSortByMeasureKeysAtMeasureScope, excludeMeasureSortKeys, (instanceFilterDeclarations == null) ? null : BatchDataSetPlanningFilterUtils.GetInstanceFilterTables(m, instanceFilterDeclarations)));
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00047302 File Offset: 0x00045502
		public static IEnumerable<PlanGroupByItem> ToGroupByItems(this IEnumerable<Calculation> calculations)
		{
			Func<Calculation, PlanGroupByItem> func;
			if ((func = PlanOperationBuilder.<>O.<2>__ToGroupByItem) == null)
			{
				func = (PlanOperationBuilder.<>O.<2>__ToGroupByItem = new Func<Calculation, PlanGroupByItem>(PlanOperationBuilder.ToGroupByItem));
			}
			return calculations.Select(func);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00047328 File Offset: 0x00045528
		public static PlanGroupByMember ToGroupByItem(this DataMember member, DataShapeAnnotations annotations, SubtotalUsage includedSubtotalKind, bool includeSortByMeasureKeysAtMeasureScope, bool excludeMeasureSortKeys, IReadOnlyList<PlanOperation> contextTables = null)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (includedSubtotalKind != SubtotalUsage.None && annotations.TryGetBatchSubtotalAnnotation(member, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInStage(includedSubtotalKind))
			{
				return new PlanGroupByMember(member, includeSortByMeasureKeysAtMeasureScope, batchSubtotalAnnotation.SubtotalIndicatorColumnName, excludeMeasureSortKeys, contextTables);
			}
			return new PlanGroupByMember(member, includeSortByMeasureKeysAtMeasureScope, excludeMeasureSortKeys, contextTables);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0004736E File Offset: 0x0004556E
		public static PlanGroupByItem ToGroupByItem(this Calculation calculation)
		{
			return new PlanGroupByCalculation(calculation);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00047376 File Offset: 0x00045576
		public static PlanProjectItem ToGroupProjectItemsTotalOnly(this DataMember member, DataShapeAnnotations annotations, SubtotalUsage includedSubtotalKind, out BatchSubtotalAnnotation totalAnnotation)
		{
			totalAnnotation = null;
			if (annotations.TryGetBatchSubtotalAnnotation(member, out totalAnnotation) && (totalAnnotation.Usage.IsIncludeInOutput() || includedSubtotalKind == SubtotalUsage.SortByMeasure))
			{
				return new PlanNamedColumnProjectItem(totalAnnotation.SubtotalIndicatorColumnName);
			}
			return null;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000473A8 File Offset: 0x000455A8
		public static IEnumerable<PlanProjectItem> ToGroupProjectItems(this DataMember member, DataShapeAnnotations annotations, SubtotalUsage includedSubtotalKind, bool includeSortKeysAtMeasureScope, out BatchSubtotalAnnotation totalAnnotation)
		{
			totalAnnotation = null;
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			if (includedSubtotalKind != SubtotalUsage.None)
			{
				PlanProjectItem planProjectItem = member.ToGroupProjectItemsTotalOnly(annotations, includedSubtotalKind, out totalAnnotation);
				if (planProjectItem != null)
				{
					list.Add(planProjectItem);
				}
			}
			PlanProjectItem planProjectItem2 = member.ToGroupProjectItemsGroupOnly(annotations, includeSortKeysAtMeasureScope);
			list.Add(planProjectItem2);
			return list;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000473E8 File Offset: 0x000455E8
		public static PlanProjectItem ToGroupProjectItemsGroupOnly(this DataMember member, DataShapeAnnotations annotations, bool includeSortKeysAtMeasureScope)
		{
			Group group = member.GetGroup(annotations);
			List<ExpressionId> list = group.GroupKeys.Select((GroupKey k) => k.Value.ExpressionId.Value).ToList<ExpressionId>();
			if (group.SortKeys != null)
			{
				SortByMeasureInfoCollection sortByMeasureInfos = annotations.DataMemberAnnotations.GetSortByMeasureInfos(member);
				for (int i = 0; i < group.SortKeys.Count; i++)
				{
					SortKey sortKey = group.SortKeys[i];
					if (sortByMeasureInfos == null || (includeSortKeysAtMeasureScope && sortByMeasureInfos.IsAtMeasureScope) || !sortByMeasureInfos.ContainsKey(sortKey))
					{
						list.Add(sortKey.Value.ExpressionId.Value);
						if (group.ScopeIdDefinition != null)
						{
							list.Add(group.ScopeIdDefinition.Values[i].Value.ExpressionId.Value);
						}
					}
				}
			}
			return new PlanPreserveColumnsProjectItem(list);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000474D7 File Offset: 0x000456D7
		public static PlanPreserveAllColumnsProjectItem ToAllColumnsProjectItem()
		{
			return PlanPreserveAllColumnsProjectItem.Instance;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000474DE File Offset: 0x000456DE
		public static PlanProjectItem ToSortKeyProjectItem(this SortByMeasureInfo sortByMeasureInfo)
		{
			return new PlanPreserveColumnsProjectItem(sortByMeasureInfo.GetEffectivePlanIdentities());
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000474EB File Offset: 0x000456EB
		public static PlanTransformExistingColumnProjectItem ToNewColumnFromExistingProjectItem(this Expression newColumnExpression, ExpressionContext expressionContext, IEnumerable<ExpressionId> planIdentities)
		{
			return new PlanTransformExistingColumnProjectItem(newColumnExpression, expressionContext, planIdentities);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000474F5 File Offset: 0x000456F5
		public static PlanTransformExistingColumnWithSameNameProjectItem ToNewColumnFromExistingProjectItemWithSameName(this Expression newColumnExpression, IEnumerable<ExpressionId> planIdentities)
		{
			return new PlanTransformExistingColumnWithSameNameProjectItem(newColumnExpression, planIdentities);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000474FE File Offset: 0x000456FE
		public static PlanProjectItem ToNewColumnProjectItem(this Calculation calculation)
		{
			return new PlanCalculationProjectItem(calculation);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00047508 File Offset: 0x00045708
		public static PlanProjectItem ToNewColumnProjectItem(this DataTransformTableColumn dataTransformTableColumn, ExpressionContext expressionContext)
		{
			return new PlanNewColumnProjectItem(dataTransformTableColumn.Value.ExpressionId.Value, dataTransformTableColumn.Id.Value, expressionContext, ColumnReuseKind.ByReference);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004753A File Offset: 0x0004573A
		public static PlanProjectItem ToDataMemberProjectItem(this DataMember dataMember, bool excludeMeasureSortKeys)
		{
			return new PlanDataMemberProjectItem(dataMember, excludeMeasureSortKeys);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00047543 File Offset: 0x00045743
		public static PlanProjectItem ToPreserveColumnProjectItem(this Calculation calculation)
		{
			return new PlanPreserveCalculationProjectItem(calculation);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0004754B File Offset: 0x0004574B
		public static PlanOperation Project(this PlanOperation input, PlanProjectItem item, bool enforceColumnOrder = false)
		{
			return input.Project(item.AsReadOnlyList<PlanProjectItem>(), enforceColumnOrder);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0004755A File Offset: 0x0004575A
		public static PlanOperation Project(this PlanOperation input, IReadOnlyList<PlanProjectItem> items, bool enforceColumnOrder = false)
		{
			return new PlanOperationProject(input, items, enforceColumnOrder);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00047564 File Offset: 0x00045764
		public static PlanAggregateItem ToAggregateItem(this Calculation calculation)
		{
			return new PlanAggregateCalculationItem(calculation);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004756C File Offset: 0x0004576C
		public static PlanOperationContext InnerJoin(this PlanOperationContext left, PlanOperationContext right)
		{
			PlanOperation planOperation = left.Table.InnerJoin(right.Table);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(left.Totals.Union(right.Totals).ToReadOnlyList<DataMember>().ToTotalsMetadata(), false);
			return new PlanOperationContext(planOperation, left.RowScopes, left.Calculations.Union(right.Calculations).ToReadOnlyList<Calculation>(), left.ShowAll.Intersect(right.ShowAll).ToReadOnlyList<DataMember>(), planOperationFilteringMetadata);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000475E4 File Offset: 0x000457E4
		public static PlanOperation InnerJoin(this PlanOperation left, PlanOperation right)
		{
			return new PlanOperationInnerJoin(left, right);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000475F0 File Offset: 0x000457F0
		public static PlanOperationContext LeftOuterJoin(this PlanOperationContext left, PlanOperationContext right, ScopeTree scopeTree)
		{
			PlanOperation planOperation = left.Table.LeftOuterJoin(right.Table);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(left.Totals.ToTotalsMetadata(), false);
			return new PlanOperationContext(planOperation, left.RowScopes, left.Calculations.Union(right.Calculations).ToReadOnlyList<Calculation>(), left.ShowAll, planOperationFilteringMetadata);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00047648 File Offset: 0x00045848
		public static PlanOperation LeftOuterJoin(this PlanOperation left, PlanOperation right)
		{
			return new PlanOperationLeftOuterJoin(left, right);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00047651 File Offset: 0x00045851
		public static PlanOperation CrossJoin(this PlanOperation left, PlanOperation right)
		{
			return new PlanOperationCrossJoin(new PlanOperation[] { left, right });
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00047666 File Offset: 0x00045866
		public static PlanOperation GenerateAll(IReadOnlyList<PlanOperation> inputs)
		{
			return new PlanOperationFullOuterCrossJoin(inputs);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004766E File Offset: 0x0004586E
		public static PlanOperation Union(this PlanOperation left, PlanOperation right)
		{
			return new PlanOperationUnion(new PlanOperation[] { left, right });
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00047684 File Offset: 0x00045884
		public static PlanOperationContext Union(this PlanOperationContext left, PlanOperation right)
		{
			PlanOperation planOperation = left.Table.Union(right);
			return left.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000476A8 File Offset: 0x000458A8
		public static PlanOperationContext UnionDistinct(this PlanOperationContext left, PlanOperation right)
		{
			PlanOperation planOperation = left.Table.Union(right).DistinctRows();
			return left.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000476D1 File Offset: 0x000458D1
		public static PlanOperation DistinctRows(this PlanOperation input)
		{
			return new PlanOperationDistinctRows(input);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000476DC File Offset: 0x000458DC
		public static PlanOperationContext DistinctRows(this PlanOperationContext input)
		{
			PlanOperation planOperation = input.Table.DistinctRows();
			return input.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000476FF File Offset: 0x000458FF
		public static PlanOperation TableScan(IConceptualEntity entity, IReadOnlyList<ExpressionId> expectedProjections)
		{
			return new PlanOperationTableScan(entity, expectedProjections);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00047708 File Offset: 0x00045908
		public static PlanOperation TableScan(string entityPlanName, IReadOnlyList<ExpressionId> expectedProjections)
		{
			return new PlanOperationTableScan(entityPlanName, expectedProjections);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00047714 File Offset: 0x00045914
		public static PlanOperationContext SubstituteWithIndex(this PlanOperationContext table, string indexColumnName, PlanOperationContext indexTable, IEnumerable<PlanSortItem> indexTableSorts)
		{
			PlanOperation planOperation = table.Table.SubstituteWithIndex(indexColumnName, indexTable.Table, indexTableSorts);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(table.Totals.Except(indexTable.Totals).ToReadOnlyList<DataMember>().ToTotalsMetadata(), false);
			return new PlanOperationContext(planOperation, table.RowScopes, table.Calculations.Except(indexTable.Calculations).ToReadOnlyList<Calculation>(), table.ShowAll.Except(indexTable.ShowAll).ToReadOnlyList<DataMember>(), planOperationFilteringMetadata);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004778E File Offset: 0x0004598E
		public static PlanOperation SubstituteWithIndex(this PlanOperation table, string indexColumnName, PlanOperation indexTable, IEnumerable<PlanSortItem> indexTableSorts)
		{
			return new PlanOperationSubstituteWithIndex(table, indexColumnName, indexTable, indexTableSorts);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00047799 File Offset: 0x00045999
		public static PlanOperation AddJoinIndex(this PlanOperation table, Calculation indexColumnCalculation, PlanOperation indexTable, IEnumerable<PlanSortItem> indexTableSorts, WritableExpressionTable expressionTable)
		{
			return new PlanOperationAddJoinIndex(table, indexColumnCalculation, indexTable, indexTableSorts);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000477A4 File Offset: 0x000459A4
		public static PlanOperation ReferencableFromVisualCalculation(this PlanOperation table, IReadOnlyList<ColumnWithExplicitName> explicitlyNamedColumns)
		{
			return new PlanOperationVisualCalculationReferenceableTable(table, explicitlyNamedColumns);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000477B0 File Offset: 0x000459B0
		public static PlanOperationContext DataTransform(this PlanOperationContext input, DataTransform transform)
		{
			PlanOperationDataTransform planOperationDataTransform = new PlanOperationDataTransform(input.Table, transform);
			return input.ReplaceTable(planOperationDataTransform, null, null, null);
		}

		// Token: 0x0200031C RID: 796
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B5E RID: 2910
			public static Func<DataMember, PlanBinnedLineSampleMember> <0>__ToBinnedLineSampleItem;

			// Token: 0x04000B5F RID: 2911
			public static Func<Calculation, PlanBinnedLineSampleCalculation> <1>__ToBinnedLineSampleItem;

			// Token: 0x04000B60 RID: 2912
			public static Func<Calculation, PlanGroupByItem> <2>__ToGroupByItem;
		}
	}
}
