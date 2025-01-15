using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.DSQ;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200000A RID: 10
	internal static class DataShapeBuilderExtensions
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003D74 File Offset: 0x00001F74
		internal static void WithGrouping<TParent>(this GroupBuilder<DataMemberBuilder<TParent>> group, DataShapeBuilderContext context, IReadOnlyList<QueryGroupKey> groupKeys, QueryDetailGroupIdentity detailGroupIdentity, int groupIdx, bool isPrimary, string memberId, string subtotalMemberId, SubtotalType subtotalType, bool contextOnly = false)
		{
			context.AddGrouping<TParent>(group, groupKeys, detailGroupIdentity, groupIdx, isPrimary, memberId, subtotalMemberId, subtotalType, contextOnly);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003D98 File Offset: 0x00001F98
		internal static void WithSynchronizationGrouping<TParent>(this GroupBuilder<DataMemberBuilder<TParent>> group, DataShapeBuilderContext context, IntermediateGroupSchema groupSchema, int groupIdx, bool isPrimary, string memberId, string subtotalMemberId, SubtotalType subtotalType, HashSet<int> visitedSelectIndices)
		{
			context.AddGroupingForSynchronization<TParent>(group, groupSchema, groupIdx, isPrimary, memberId, subtotalMemberId, subtotalType, visitedSelectIndices);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003DB8 File Offset: 0x00001FB8
		internal static DataMemberBuilder<TParent> WithGroupCalculation<TParent>(this DataMemberBuilder<TParent> dataMember, DataShapeBuilderContext context, QueryGroupValue groupValue, int groupIdx, bool isPrimary, bool contextOnly = false)
		{
			return context.AddGroupCalculation<TParent>(dataMember, groupValue, groupIdx, isPrimary, contextOnly);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003DC7 File Offset: 0x00001FC7
		internal static DataShapeBuilder<TParent> WithMeasureCalculation<TParent>(this DataShapeBuilder<TParent> dataShape, DataShapeBuilderContext context, ProjectedDsqExpression generatedDsqExpr)
		{
			return context.AddMeasureCalculation<TParent>(dataShape, generatedDsqExpr);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003DD1 File Offset: 0x00001FD1
		internal static DataMemberBuilder<TParent> WithMeasureCalculation<TParent>(this DataMemberBuilder<TParent> dataMember, DataShapeBuilderContext context, ProjectedDsqExpression generatedDsqExpr, bool hasHighlightFilters, int? groupIdx = null)
		{
			return context.AddMeasureCalculation<TParent>(dataMember, generatedDsqExpr, hasHighlightFilters, groupIdx);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003DDE File Offset: 0x00001FDE
		internal static DataIntersectionBuilder<TParent> WithMeasureCalculation<TParent>(this DataIntersectionBuilder<TParent> intersection, DataShapeBuilderContext context, ProjectedDsqExpression generatedDsqExpr, bool hasHighlightFilters)
		{
			return context.AddMeasureCalculation<TParent>(intersection, generatedDsqExpr, hasHighlightFilters);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003DEC File Offset: 0x00001FEC
		internal static DataMemberBuilder<TParent> WithAggregates<TParent>(this DataMemberBuilder<TParent> dataMember, DataShapeBuilderContext context, ProjectedDsqExpression item, int groupIdx, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, AggregateContextOnlyImpact aggregateContextOnlyImpact)
		{
			context.AddAggregates<DataMemberBuilder<TParent>>(dataMember, item, new int?(groupIdx), null, suppressedAggregates, aggregateContextOnlyImpact);
			return dataMember;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003E18 File Offset: 0x00002018
		internal static DataIntersectionBuilder<TParent> WithAggregates<TParent>(this DataIntersectionBuilder<TParent> intersection, DataShapeBuilderContext context, ProjectedDsqExpression item, int primaryIdx, int secondaryIdx, int maxPrimaryIdx, int maxSecondaryIdx, int maxPrimaryIdxWithoutContextOnly, int maxSecondaryIdxWithoutContextOnly, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, bool hasContextOnlyContributingStaticMember)
		{
			AggregateContextOnlyImpact aggregateContextOnlyImpact;
			int? num = DataShapeBuilderExtensions.ComputeAggregateIndexWithoutContextOnly(primaryIdx, secondaryIdx, maxPrimaryIdx, maxSecondaryIdx, maxPrimaryIdxWithoutContextOnly, maxSecondaryIdxWithoutContextOnly, hasContextOnlyContributingStaticMember, out aggregateContextOnlyImpact);
			context.AddAggregates<DataIntersectionBuilder<TParent>>(intersection, item, null, num, suppressedAggregates, aggregateContextOnlyImpact);
			return intersection;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003E50 File Offset: 0x00002050
		internal static DataShapeBuilder<TParent> WithAggregates<TParent>(this DataShapeBuilder<TParent> dataShape, DataShapeBuilderContext context, ProjectedDsqExpression item, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, AggregateContextOnlyImpact aggregateContextOnlyImpact)
		{
			context.AddAggregates<DataShapeBuilder<TParent>>(dataShape, item, null, null, suppressedAggregates, aggregateContextOnlyImpact);
			return dataShape;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003E7C File Offset: 0x0000207C
		internal static int? ComputeAggregateIndexWithoutContextOnly(int primaryIdx, int secondaryIdx, int maxPrimaryIdx, int maxSecondaryIdx, int maxPrimaryIdxWithoutContextOnly, int maxSecondaryIdxWithoutContextOnly, bool hasContextOnlyContributingStaticMember, out AggregateContextOnlyImpact aggregateContextOnlyImpact)
		{
			int? num = ((primaryIdx > maxPrimaryIdxWithoutContextOnly || secondaryIdx > maxSecondaryIdxWithoutContextOnly) ? null : new int?(DataShapeBuilderExtensions.ComputeAggregateIndex(primaryIdx, secondaryIdx, maxPrimaryIdxWithoutContextOnly, maxSecondaryIdxWithoutContextOnly)));
			bool flag = maxPrimaryIdx != maxPrimaryIdxWithoutContextOnly || maxSecondaryIdx != maxSecondaryIdxWithoutContextOnly;
			if (hasContextOnlyContributingStaticMember || num == null)
			{
				aggregateContextOnlyImpact = AggregateContextOnlyImpact.InsideAContextOnlyMember;
			}
			else
			{
				if (flag)
				{
					int? num2 = num;
					if ((num2.GetValueOrDefault() == maxPrimaryIdxWithoutContextOnly) & (num2 != null))
					{
						aggregateContextOnlyImpact = AggregateContextOnlyImpact.NewInnermostIntersection;
						return num;
					}
				}
				aggregateContextOnlyImpact = AggregateContextOnlyImpact.None;
			}
			return num;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003EFA File Offset: 0x000020FA
		internal static int ComputeAggregateIndex(int primaryIdx, int secondaryIdx, int maxPrimaryIdx, int maxSecondaryIdx)
		{
			return primaryIdx + (maxSecondaryIdx - secondaryIdx) * (maxPrimaryIdx + 1);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003F05 File Offset: 0x00002105
		internal static void WithSortKey<TParent>(this GroupBuilder<DataMemberBuilder<TParent>> group, DataShapeBuilderContext context, QueryMember queryMember, DsqSortKey sortKey, bool hasNestedGroups, bool isPrimary, bool contextOnly, int groupingIndex)
		{
			context.WithSortKey<TParent>(group, sortKey, queryMember, hasNestedGroups, isPrimary, contextOnly, groupingIndex);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003F18 File Offset: 0x00002118
		internal static DataMemberBuilderPair WithDynamicMember<TParent>(this DataShapeBuilder<TParent> parent, DataShapeBuilderContext context, QueryMember queryMember, bool isPrimary, bool suppressStatic)
		{
			SubtotalType subtotal = queryMember.Group.Subtotal;
			DataMember dataMember = null;
			if (subtotal != SubtotalType.After)
			{
				dataMember = (suppressStatic ? null : DataShapeBuilderExtensions.WithMember<TParent>(parent, isPrimary, context.CreateMemberId()).Result);
			}
			DataMember result = DataShapeBuilderExtensions.WithMember<TParent>(parent, isPrimary, context.CreateMemberId()).Result;
			if (subtotal == SubtotalType.After)
			{
				dataMember = DataShapeBuilderExtensions.WithMember<TParent>(parent, isPrimary, context.CreateMemberId()).Result;
			}
			return new DataMemberBuilderPair(dataMember, result, queryMember);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003F84 File Offset: 0x00002184
		private static DataMemberBuilder<DataShapeBuilder<TParent>> WithMember<TParent>(DataShapeBuilder<TParent> parent, bool isPrimary, string staticId)
		{
			if (isPrimary)
			{
				return parent.WithPrimaryMember(staticId, null);
			}
			return parent.WithSecondaryMember(staticId);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003FB8 File Offset: 0x000021B8
		internal static DataMemberBuilderPair WithDynamicMember(this DataMemberBuilder parent, DataShapeBuilderContext context, QueryMember queryMember, bool isPrimary, bool suppressStatic)
		{
			SubtotalType subtotal = queryMember.Group.Subtotal;
			DataMember dataMember = null;
			if (subtotal != SubtotalType.After)
			{
				dataMember = (suppressStatic ? null : DataShapeBuilderExtensions.WithMember(parent, isPrimary, context.CreateMemberId()).Result);
			}
			DataMember result = DataShapeBuilderExtensions.WithMember(parent, isPrimary, context.CreateMemberId()).Result;
			if (subtotal == SubtotalType.After)
			{
				dataMember = DataShapeBuilderExtensions.WithMember(parent, isPrimary, context.CreateMemberId()).Result;
			}
			return new DataMemberBuilderPair(dataMember, result, queryMember);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004024 File Offset: 0x00002224
		private static DataMemberBuilder<DataMemberBuilder<DataMember>> WithMember(DataMemberBuilder parent, bool isPrimary, string staticId)
		{
			if (isPrimary)
			{
				return parent.WithPrimaryMember(staticId, null);
			}
			return parent.WithSecondaryMember(staticId);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004056 File Offset: 0x00002256
		internal static ExpressionNode DsqExpression(this IntermediateQueryTransformTableColumn column)
		{
			return ExpressionNodeBuilder.DataTransformTableColumn(column.Table.Id.Value, column.Id.Value);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004078 File Offset: 0x00002278
		internal static QueryBindingDescriptorAggregateContainer CreateQueryBindingDescriptorAggregateContainer(this DataShapeBindingAggregateContainer aggregateContainer)
		{
			return new QueryBindingDescriptorAggregateContainer
			{
				Percentile = aggregateContainer.Percentile,
				Median = aggregateContainer.Median,
				Min = aggregateContainer.Min,
				Max = aggregateContainer.Max,
				Average = aggregateContainer.Average,
				Scope = aggregateContainer.Scope
			};
		}
	}
}
