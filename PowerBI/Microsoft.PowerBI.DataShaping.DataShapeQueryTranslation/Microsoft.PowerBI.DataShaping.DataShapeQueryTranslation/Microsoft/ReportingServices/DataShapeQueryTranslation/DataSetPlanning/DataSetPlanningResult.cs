using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000FA RID: 250
	internal sealed class DataSetPlanningResult
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x000260DC File Offset: 0x000242DC
		internal DataSetPlanningResult(IList<DataSetPlan> dataSetPlans, DataBindingMapping dataBindings, OutputPlanMapping itemToOutputPlanIndex, ReadOnlyExpressionTable expressionTable, List<DataSetPlan> subQueryPlans, DataShape dataShape)
		{
			this.m_dataSetPlans = dataSetPlans.ToReadOnlyCollection<DataSetPlan>();
			this.m_dataBindings = dataBindings;
			this.m_itemToOutputPlanIndex = itemToOutputPlanIndex;
			this.m_expressionTable = expressionTable;
			this.m_subQueryPlans = subQueryPlans.AsReadOnly();
			this.m_dataShape = dataShape;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002611B File Offset: 0x0002431B
		public ReadOnlyCollection<DataSetPlan> DataSetPlans
		{
			get
			{
				return this.m_dataSetPlans;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00026123 File Offset: 0x00024323
		public ReadOnlyExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionTable;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002612B File Offset: 0x0002432B
		public ReadOnlyCollection<DataSetPlan> SubQueryPlans
		{
			get
			{
				return this.m_subQueryPlans;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00026133 File Offset: 0x00024333
		public DataShape DataShape
		{
			get
			{
				return this.m_dataShape;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002613B File Offset: 0x0002433B
		internal IEnumerable<KeyValuePair<IDataBoundItem, DataBinding>> ItemBindings
		{
			get
			{
				return this.m_dataBindings.ItemBindings;
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00026148 File Offset: 0x00024348
		public DataBinding GetDataBindingForItem(IDataBoundItem item)
		{
			DataBinding dataBinding;
			if (this.m_dataBindings.TryGetValue(item, out dataBinding))
			{
				return dataBinding;
			}
			return null;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00026168 File Offset: 0x00024368
		public DataBinding GetDataBindingForPlan(int planIndex)
		{
			DataBinding dataBinding;
			if (this.m_dataBindings.TryGetValue(planIndex, out dataBinding))
			{
				return dataBinding;
			}
			return null;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00026188 File Offset: 0x00024388
		public DataSetPlan GetOutputDataSetPlanForItem(IContextItem item)
		{
			int num;
			if (this.m_itemToOutputPlanIndex.TryGetValue(item, out num))
			{
				return this.GetDataSetPlan(num);
			}
			return null;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000261AE File Offset: 0x000243AE
		public DataSetPlan GetDataSetPlan(int planIndex)
		{
			if (planIndex >= 0 && planIndex < this.m_dataSetPlans.Count)
			{
				return this.m_dataSetPlans[planIndex];
			}
			return null;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000261D0 File Offset: 0x000243D0
		public bool IsReusableBinding(int index)
		{
			return this.m_dataBindings.IsReusableBinding(index);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000261DE File Offset: 0x000243DE
		public bool HasReusableBinding()
		{
			return this.m_dataBindings.HasReusableBinding();
		}

		// Token: 0x040004BE RID: 1214
		private readonly ReadOnlyCollection<DataSetPlan> m_dataSetPlans;

		// Token: 0x040004BF RID: 1215
		private readonly DataBindingMapping m_dataBindings;

		// Token: 0x040004C0 RID: 1216
		private readonly OutputPlanMapping m_itemToOutputPlanIndex;

		// Token: 0x040004C1 RID: 1217
		private readonly ReadOnlyExpressionTable m_expressionTable;

		// Token: 0x040004C2 RID: 1218
		private readonly ReadOnlyCollection<DataSetPlan> m_subQueryPlans;

		// Token: 0x040004C3 RID: 1219
		private readonly DataShape m_dataShape;
	}
}
