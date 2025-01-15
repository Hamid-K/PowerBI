using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200016B RID: 363
	internal sealed class BatchDataSetPlanningResult
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x00036514 File Offset: 0x00034714
		internal BatchDataSetPlanningResult(List<BatchDataSetPlan> dataSetPlans, ExpressionTable expressionTable, BatchDataBindingMapping dataBindings, IntersectionCorrelations intersectionCorrelations, BatchMemberMatchConditions memberMatchConditions, BatchMemberDiscardConditions memberDiscardConditions, BatchSortByMeasureExpressionMappings sortByMeasureExpressions, GroupDetailMap groupDetailMapping, CalculationExpressionMap calculationExpressionMapping, CalculationsWithSharedValues calculationsWithSharedValues, PlanLimitInfo limitInfo, BatchRestartIndicator restartIndicator)
		{
			this.DataSetPlans = dataSetPlans.AsReadOnly();
			this.ExpressionTable = expressionTable;
			this.m_dataBindings = dataBindings;
			this.IntersectionCorrelations = intersectionCorrelations;
			this.MemberMatchConditions = memberMatchConditions;
			this.MemberDiscardConditions = memberDiscardConditions;
			this.SortByMeasureExpressions = sortByMeasureExpressions;
			this.GroupDetailMapping = groupDetailMapping;
			this.CalculationExpressionMapping = calculationExpressionMapping;
			this.CalculationsWithSharedValues = calculationsWithSharedValues;
			this.LimitInfo = limitInfo;
			this.RestartIndicator = restartIndicator;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00036589 File Offset: 0x00034789
		public ReadOnlyCollection<BatchDataSetPlan> DataSetPlans { get; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00036591 File Offset: 0x00034791
		public ExpressionTable ExpressionTable { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00036599 File Offset: 0x00034799
		public IntersectionCorrelations IntersectionCorrelations { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x000365A1 File Offset: 0x000347A1
		public BatchMemberMatchConditions MemberMatchConditions { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x000365A9 File Offset: 0x000347A9
		public BatchMemberDiscardConditions MemberDiscardConditions { get; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x000365B1 File Offset: 0x000347B1
		public BatchSortByMeasureExpressionMappings SortByMeasureExpressions { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000365B9 File Offset: 0x000347B9
		public IEnumerable<KeyValuePair<IDataBoundItem, BatchDataBinding>> DataBindings
		{
			get
			{
				return this.m_dataBindings;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x000365C1 File Offset: 0x000347C1
		public GroupDetailMap GroupDetailMapping { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000365C9 File Offset: 0x000347C9
		public CalculationExpressionMap CalculationExpressionMapping { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x000365D1 File Offset: 0x000347D1
		public CalculationsWithSharedValues CalculationsWithSharedValues { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x000365D9 File Offset: 0x000347D9
		public PlanLimitInfo LimitInfo { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000365E1 File Offset: 0x000347E1
		public BatchRestartIndicator RestartIndicator { get; }

		// Token: 0x06000D30 RID: 3376 RVA: 0x000365EC File Offset: 0x000347EC
		public BatchDataBinding GetDataBindingForItem(IDataBoundItem item)
		{
			BatchDataBinding batchDataBinding;
			this.m_dataBindings.TryGetValue(item, out batchDataBinding);
			return batchDataBinding;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003660C File Offset: 0x0003480C
		public BatchColumnReference GetCorrelationForItem(IContextItem item)
		{
			if (this.IntersectionCorrelations == null || this.IntersectionCorrelations.Count == 0)
			{
				return null;
			}
			BatchColumnReference batchColumnReference;
			this.IntersectionCorrelations.TryGetValue(item.Id, out batchColumnReference);
			return batchColumnReference;
		}

		// Token: 0x04000679 RID: 1657
		private readonly BatchDataBindingMapping m_dataBindings;
	}
}
