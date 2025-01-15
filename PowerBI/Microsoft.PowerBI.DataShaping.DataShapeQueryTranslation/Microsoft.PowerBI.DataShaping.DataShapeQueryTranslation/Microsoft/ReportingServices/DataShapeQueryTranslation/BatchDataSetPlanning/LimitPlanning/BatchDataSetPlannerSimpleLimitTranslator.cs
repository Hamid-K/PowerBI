using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x02000238 RID: 568
	internal sealed class BatchDataSetPlannerSimpleLimitTranslator : LimitVisitor<PlanOperation>
	{
		// Token: 0x0600136A RID: 4970 RVA: 0x0004B180 File Offset: 0x00049380
		private BatchDataSetPlannerSimpleLimitTranslator(PlanOperation input, Limit limit, IEnumerable<PlanSortItem> sorts, IEnumerable<DataMember> spannedMembers, TranslationErrorContext errorContext, BatchSubtotalAnnotations subtotalAnnotations, Func<PlanOperation, PlanOperation> windowRestartTableVisitor, bool usePadding, ExpressionNode overrideCount)
		{
			this.m_input = input;
			this.m_limit = limit;
			this.m_sorts = sorts;
			this.m_spannedMembers = spannedMembers;
			this.m_errorContext = errorContext;
			this.m_subtotalAnnotations = subtotalAnnotations;
			this.m_windowRestartTableVisitor = windowRestartTableVisitor;
			this.m_overrideCount = overrideCount;
			this.m_usePadding = usePadding;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0004B1D8 File Offset: 0x000493D8
		public static PlanOperation Translate(Limit limit, PlanOperation input, IEnumerable<PlanSortItem> sorts, IEnumerable<DataMember> spannedMembers, TranslationErrorContext errorContext, BatchSubtotalAnnotations subtotalAnnotations, bool usePadding, Func<PlanOperation, PlanOperation> windowRestartTableVisitor = null, ExpressionNode overrideCount = null)
		{
			return new BatchDataSetPlannerSimpleLimitTranslator(input, limit, sorts, spannedMembers, errorContext, subtotalAnnotations, windowRestartTableVisitor, usePadding, overrideCount).Visit(limit.Operator);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0004B204 File Offset: 0x00049404
		internal override PlanOperation Visit(TopLimitOperator limitOperator)
		{
			int num = (this.m_usePadding ? limitOperator.GetPaddedCount() : limitOperator.Count.Value);
			PlanExpression planExpression = this.CreateCountExpression(num);
			if (limitOperator.Skip == null)
			{
				return this.m_input.TopN(planExpression, this.m_sorts, false);
			}
			PlanExpression planExpression2 = this.CreateSkipExpression(limitOperator.Skip.Value);
			return this.m_input.TopNSkip(planExpression, planExpression2, this.m_sorts);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004B284 File Offset: 0x00049484
		internal override PlanOperation Visit(SampleLimitOperator limitOperator)
		{
			int num = (this.m_usePadding ? (limitOperator.Count.Value + 2) : limitOperator.Count.Value);
			PlanExpression planExpression = this.CreateCountExpression(num);
			return this.m_input.Sample(planExpression, this.m_sorts);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004B2D0 File Offset: 0x000494D0
		internal override PlanOperation Visit(FirstLimitOperator limitOperator)
		{
			PlanExpression planExpression = this.CreateCountExpression(limitOperator.Count.Value);
			return this.m_input.TopN(planExpression, this.m_sorts, false);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004B304 File Offset: 0x00049504
		internal override PlanOperation Visit(LastLimitOperator limitOperator)
		{
			PlanExpression planExpression = this.CreateCountExpression(limitOperator.Count.Value);
			return this.m_input.TopN(planExpression, this.m_sorts, true);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004B338 File Offset: 0x00049538
		internal override PlanOperation Visit(BottomLimitOperator limitOperator)
		{
			int num = (this.m_usePadding ? (limitOperator.Count.Value + 2) : limitOperator.Count.Value);
			PlanExpression planExpression = this.CreateCountExpression(num);
			return this.m_input.TopN(planExpression, this.m_sorts, true);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004B383 File Offset: 0x00049583
		internal override PlanOperation Visit(BinnedLineSampleLimitOperator limitOperator)
		{
			throw new InvalidOperationException("BinnedLineSampleLimit should not be handled here.");
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004B38F File Offset: 0x0004958F
		internal override PlanOperation Visit(OverlappingPointsSampleLimitOperator limitOperator)
		{
			throw new InvalidOperationException("OverlappingPointsSampleLimit should not be handled here.");
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004B39B File Offset: 0x0004959B
		internal override PlanOperation Visit(TopNPerLevelLimitOperator limitOperator)
		{
			throw new InvalidOperationException("TopNPerLevelLimit should not be handled here.");
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004B3A8 File Offset: 0x000495A8
		internal override PlanOperation Visit(WindowLimitOperator limitOperator)
		{
			bool flag;
			PlanOperation planOperation = WindowTableBuilder.ApplyStartPosition(this.m_input, this.m_spannedMembers, limitOperator.RestartMatchingBehavior, this.m_subtotalAnnotations, out flag);
			if (flag && this.m_windowRestartTableVisitor != null)
			{
				planOperation = this.m_windowRestartTableVisitor(planOperation);
			}
			int num = (this.m_usePadding ? (limitOperator.Count.Value + 2) : limitOperator.Count.Value);
			PlanExpression planExpression = this.CreateCountExpression(num);
			return planOperation.TopN(planExpression, this.m_sorts, false);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004B426 File Offset: 0x00049626
		private PlanExpression CreateCountExpression(int count)
		{
			if (this.m_overrideCount != null)
			{
				return BatchDataSetPlanningUtils.CreateLimitCountExpression(this.m_overrideCount, this.m_limit.Id, this.m_errorContext);
			}
			return BatchDataSetPlanningUtils.CreateLimitCountExpression(count, this.m_limit.Id, this.m_errorContext);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004B464 File Offset: 0x00049664
		private PlanExpression CreateSkipExpression(long skip)
		{
			return BatchDataSetPlanningUtils.CreateLimitSkipExpression(skip, this.m_limit.Id, this.m_errorContext);
		}

		// Token: 0x04000893 RID: 2195
		private readonly PlanOperation m_input;

		// Token: 0x04000894 RID: 2196
		private readonly Limit m_limit;

		// Token: 0x04000895 RID: 2197
		private readonly IEnumerable<PlanSortItem> m_sorts;

		// Token: 0x04000896 RID: 2198
		private readonly IEnumerable<DataMember> m_spannedMembers;

		// Token: 0x04000897 RID: 2199
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000898 RID: 2200
		private readonly BatchSubtotalAnnotations m_subtotalAnnotations;

		// Token: 0x04000899 RID: 2201
		private readonly Func<PlanOperation, PlanOperation> m_windowRestartTableVisitor;

		// Token: 0x0400089A RID: 2202
		private readonly ExpressionNode m_overrideCount;

		// Token: 0x0400089B RID: 2203
		private readonly bool m_usePadding;
	}
}
