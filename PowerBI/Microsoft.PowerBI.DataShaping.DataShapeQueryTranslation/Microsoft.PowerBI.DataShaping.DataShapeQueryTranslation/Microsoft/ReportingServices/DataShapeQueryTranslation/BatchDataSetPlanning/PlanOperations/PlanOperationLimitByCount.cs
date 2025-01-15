using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000214 RID: 532
	internal abstract class PlanOperationLimitByCount : PlanOperation
	{
		// Token: 0x06001272 RID: 4722 RVA: 0x00048EB5 File Offset: 0x000470B5
		protected PlanOperationLimitByCount(PlanOperation input, PlanExpression rowCount, IEnumerable<PlanSortItem> sorts)
		{
			this.m_input = input;
			this.m_rowCount = rowCount;
			this.m_sorts = sorts.ToReadOnlyCollection<PlanSortItem>();
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00048ED7 File Offset: 0x000470D7
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00048EDF File Offset: 0x000470DF
		public PlanExpression RowCount
		{
			get
			{
				return this.m_rowCount;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00048EE7 File Offset: 0x000470E7
		public ReadOnlyCollection<PlanSortItem> Sorts
		{
			get
			{
				return this.m_sorts;
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00048EEF File Offset: 0x000470EF
		protected bool CommonEquals(PlanOperationLimitByCount otherTyped)
		{
			return this.Input.Equals(otherTyped.Input) && this.RowCount.Equals(otherTyped.RowCount) && this.Sorts.SequenceEqual(otherTyped.Sorts);
		}

		// Token: 0x04000842 RID: 2114
		private readonly PlanOperation m_input;

		// Token: 0x04000843 RID: 2115
		private readonly ReadOnlyCollection<PlanSortItem> m_sorts;

		// Token: 0x04000844 RID: 2116
		private readonly PlanExpression m_rowCount;
	}
}
