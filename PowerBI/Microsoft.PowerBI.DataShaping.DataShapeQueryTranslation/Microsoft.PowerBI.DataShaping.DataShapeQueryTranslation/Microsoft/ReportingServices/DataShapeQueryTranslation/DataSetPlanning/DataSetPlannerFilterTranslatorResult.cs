using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F8 RID: 248
	internal sealed class DataSetPlannerFilterTranslatorResult
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x00025EB5 File Offset: 0x000240B5
		internal DataSetPlannerFilterTranslatorResult(IList<DataSetPlan> dataSetPlans)
		{
			this.m_dataSetPlans = dataSetPlans.ToReadOnlyCollection<DataSetPlan>();
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00025EC9 File Offset: 0x000240C9
		public ReadOnlyCollection<DataSetPlan> DataSetPlans
		{
			get
			{
				return this.m_dataSetPlans;
			}
		}

		// Token: 0x040004BB RID: 1211
		private readonly ReadOnlyCollection<DataSetPlan> m_dataSetPlans;
	}
}
