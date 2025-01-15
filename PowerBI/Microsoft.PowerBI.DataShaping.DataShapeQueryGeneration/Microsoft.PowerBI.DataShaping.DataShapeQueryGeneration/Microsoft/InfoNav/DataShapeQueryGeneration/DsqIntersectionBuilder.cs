using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000064 RID: 100
	internal sealed class DsqIntersectionBuilder
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x00010D64 File Offset: 0x0000EF64
		private DsqIntersectionBuilder(DataShapeBuilderContext context, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, IReadOnlyList<ProjectedDsqExpression> measures, bool hasHighlightFilters, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregatesWithSubtotal)
		{
			this._context = context;
			this._primaryDynamics = primaryDynamics;
			this._secondaryDynamics = secondaryDynamics;
			this._primaryDynamicsWithoutContextOnlyCount = this._primaryDynamics.Count((DataMemberBuilderPair p) => !p.Dynamic.Result.ContextOnly);
			this._secondaryDynamicsWithoutContextOnlyCount = this._secondaryDynamics.Count((DataMemberBuilderPair p) => !p.Dynamic.Result.ContextOnly);
			this._measures = measures;
			this._hasHighlightFilters = hasHighlightFilters;
			this._suppressedAggregates = suppressedAggregates;
			this._suppressedAggregatesWithSubtotal = suppressedAggregatesWithSubtotal;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00010E0C File Offset: 0x0000F00C
		internal static Identifier BuildIntersections(DataShapeBuilderContext context, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, IReadOnlyList<ProjectedDsqExpression> measures, DataShapeBuilder dataShape, bool hasHighlightFilters, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregatesWithSubtotal)
		{
			DsqIntersectionBuilder dsqIntersectionBuilder = new DsqIntersectionBuilder(context, primaryDynamics, secondaryDynamics, measures, hasHighlightFilters, suppressedAggregates, suppressedAggregatesWithSubtotal);
			dsqIntersectionBuilder.BuildIntersections(dataShape);
			return dsqIntersectionBuilder._innermostScopeId;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00010E2A File Offset: 0x0000F02A
		private void BuildIntersections(DataShapeBuilder dataShape)
		{
			this._context.CreateMeasureCalculationIds(this._measures);
			this.BuildRows(dataShape);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00010E44 File Offset: 0x0000F044
		private void BuildRows(DataShapeBuilder dataShape)
		{
			Stack<global::System.ValueTuple<int, bool>> stack = null;
			for (int i = 0; i < this._primaryDynamics.Count; i++)
			{
				DataMemberBuilderPair dataMemberBuilderPair = this._primaryDynamics[i];
				DataMemberBuilder @static = dataMemberBuilderPair.Static;
				bool flag = @static != null && @static.Result.ContextOnly && !dataMemberBuilderPair.Dynamic.Result.ContextOnly;
				if (dataMemberBuilderPair.QueryMember.Group.Subtotal == SubtotalType.Before)
				{
					this.BuildIntersections(dataShape.WithDataRow(), i, false, DsqIntersectionBuilder.ShouldSuppressSubtotalOutput(dataMemberBuilderPair.QueryMember), flag);
				}
				if (dataMemberBuilderPair.Dynamic.Result.DataMembers.IsNullOrEmpty<DataMember>())
				{
					this.BuildIntersections(dataShape.WithDataRow(), i + 1, true, false, false);
				}
				if (dataMemberBuilderPair.QueryMember.Group.Subtotal == SubtotalType.After)
				{
					Util.PushToLazyStack<global::System.ValueTuple<int, bool>>(ref stack, new global::System.ValueTuple<int, bool>(i, flag));
				}
			}
			if (stack == null)
			{
				return;
			}
			foreach (global::System.ValueTuple<int, bool> valueTuple in stack)
			{
				this.BuildIntersections(dataShape.WithDataRow(), valueTuple.Item1, false, DsqIntersectionBuilder.ShouldSuppressSubtotalOutput(this._primaryDynamics[valueTuple.Item1].QueryMember), valueTuple.Item2);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00010F9C File Offset: 0x0000F19C
		private void BuildIntersections(DataRowBuilder<DataShapeBuilder<DataShape>> row, int primaryAggIdx, bool isPrimaryDynamic, bool suppressSubtotalCalculationsOnPrimary, bool hasContextOnlyContributingStaticMemberOnPrimary)
		{
			Stack<global::System.ValueTuple<int, bool>> stack = null;
			for (int i = 0; i < this._secondaryDynamics.Count; i++)
			{
				DataMemberBuilderPair dataMemberBuilderPair = this._secondaryDynamics[i];
				DataMemberBuilder @static = dataMemberBuilderPair.Static;
				bool flag = @static != null && @static.Result.ContextOnly && !dataMemberBuilderPair.Dynamic.Result.ContextOnly;
				bool flag2 = hasContextOnlyContributingStaticMemberOnPrimary || flag;
				if (dataMemberBuilderPair.QueryMember.Group.Subtotal == SubtotalType.Before)
				{
					this.BuildIntersection(row, primaryAggIdx, isPrimaryDynamic, i, false, suppressSubtotalCalculationsOnPrimary || DsqIntersectionBuilder.ShouldSuppressSubtotalOutput(dataMemberBuilderPair.QueryMember), flag2);
				}
				if (dataMemberBuilderPair.Dynamic.Result.DataMembers.IsNullOrEmpty<DataMember>())
				{
					this.BuildIntersection(row, primaryAggIdx, isPrimaryDynamic, i + 1, true, suppressSubtotalCalculationsOnPrimary, hasContextOnlyContributingStaticMemberOnPrimary);
				}
				if (dataMemberBuilderPair.QueryMember.Group.Subtotal == SubtotalType.After)
				{
					Util.PushToLazyStack<global::System.ValueTuple<int, bool>>(ref stack, new global::System.ValueTuple<int, bool>(i, flag2));
				}
			}
			if (stack == null)
			{
				return;
			}
			foreach (global::System.ValueTuple<int, bool> valueTuple in stack)
			{
				this.BuildIntersection(row, primaryAggIdx, isPrimaryDynamic, valueTuple.Item1, false, suppressSubtotalCalculationsOnPrimary || DsqIntersectionBuilder.ShouldSuppressSubtotalOutput(this._secondaryDynamics[valueTuple.Item1].QueryMember), valueTuple.Item2);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00011104 File Offset: 0x0000F304
		private void BuildIntersection(DataRowBuilder<DataShapeBuilder<DataShape>> row, int primaryAggIdx, bool isPrimaryDynamic, int secondaryAggIdx, bool isSecondaryDynamic, bool suppressSubtotals, bool hasContextOnlyContributingStaticMember)
		{
			DataIntersectionBuilder<DataRowBuilder<DataShapeBuilder<DataShape>>> dataIntersectionBuilder = row.WithIntersection(this._context.CreateIntersectionId());
			if (isPrimaryDynamic && isSecondaryDynamic)
			{
				this.AddMeasureCalculations<DataRowBuilder<DataShapeBuilder<DataShape>>>(dataIntersectionBuilder);
				this._innermostScopeId = dataIntersectionBuilder.Result.Id;
				return;
			}
			this.AddAggregates<DataRowBuilder<DataShapeBuilder<DataShape>>>(dataIntersectionBuilder, primaryAggIdx, secondaryAggIdx, suppressSubtotals, hasContextOnlyContributingStaticMember);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00011150 File Offset: 0x0000F350
		private void AddMeasureCalculations<TParent>(DataIntersectionBuilder<TParent> intersection)
		{
			foreach (ProjectedDsqExpression projectedDsqExpression in this._measures)
			{
				intersection.WithMeasureCalculation(this._context, projectedDsqExpression, this._hasHighlightFilters);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000111AC File Offset: 0x0000F3AC
		private void AddAggregates<TParent>(DataIntersectionBuilder<TParent> intersection, int primaryAggIdx, int secondaryAggIdx, bool suppressSubtotals, bool hasContextOnlyContributingStaticMember)
		{
			int count = this._primaryDynamics.Count;
			int count2 = this._secondaryDynamics.Count;
			foreach (ProjectedDsqExpression projectedDsqExpression in this._measures)
			{
				string text;
				string text2;
				this._context.PrepareAddMeasureCalculation(projectedDsqExpression, this._hasHighlightFilters, out text, out text2);
				intersection.WithAggregates(this._context, projectedDsqExpression, primaryAggIdx, secondaryAggIdx, count, count2, this._primaryDynamicsWithoutContextOnlyCount, this._secondaryDynamicsWithoutContextOnlyCount, suppressSubtotals ? this._suppressedAggregatesWithSubtotal : this._suppressedAggregates, hasContextOnlyContributingStaticMember);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00011254 File Offset: 0x0000F454
		private static bool ShouldSuppressSubtotalOutput(QueryMember member)
		{
			return member.HasExplicitSubtotal;
		}

		// Token: 0x0400027A RID: 634
		private readonly DataShapeBuilderContext _context;

		// Token: 0x0400027B RID: 635
		private readonly IReadOnlyList<DataMemberBuilderPair> _primaryDynamics;

		// Token: 0x0400027C RID: 636
		private readonly IReadOnlyList<DataMemberBuilderPair> _secondaryDynamics;

		// Token: 0x0400027D RID: 637
		private readonly int _primaryDynamicsWithoutContextOnlyCount;

		// Token: 0x0400027E RID: 638
		private readonly int _secondaryDynamicsWithoutContextOnlyCount;

		// Token: 0x0400027F RID: 639
		private readonly IReadOnlyList<ProjectedDsqExpression> _measures;

		// Token: 0x04000280 RID: 640
		private readonly bool _hasHighlightFilters;

		// Token: 0x04000281 RID: 641
		private readonly IReadOnlyList<DsqExpressionAggregateKind> _suppressedAggregates;

		// Token: 0x04000282 RID: 642
		private readonly IReadOnlyList<DsqExpressionAggregateKind> _suppressedAggregatesWithSubtotal;

		// Token: 0x04000283 RID: 643
		private Identifier _innermostScopeId;
	}
}
