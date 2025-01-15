using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x0200010F RID: 271
	internal sealed class SubQueryDataSetPlan
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00028947 File Offset: 0x00026B47
		internal SubQueryDataSetPlan(IScope outerScope, IScope innerScope, IScope rollupStartScope, bool filterEmptyGroups, DataSetPlan plan)
		{
			this.m_outerScope = outerScope;
			this.m_innerScope = innerScope;
			this.m_rollupStartScope = rollupStartScope;
			this.m_filterEmptyGroups = filterEmptyGroups;
			this.m_plan = plan;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00028974 File Offset: 0x00026B74
		public DataSetPlan Plan
		{
			get
			{
				return this.m_plan;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002897C File Offset: 0x00026B7C
		public bool Matches(IScope outerScope, IScope innerScope, IScope rollupStartScope, bool filterEmptyGroups)
		{
			return this.m_outerScope == outerScope && this.m_innerScope == innerScope && this.m_rollupStartScope == rollupStartScope && this.m_filterEmptyGroups == filterEmptyGroups;
		}

		// Token: 0x04000524 RID: 1316
		private readonly IScope m_outerScope;

		// Token: 0x04000525 RID: 1317
		private readonly IScope m_innerScope;

		// Token: 0x04000526 RID: 1318
		private readonly IScope m_rollupStartScope;

		// Token: 0x04000527 RID: 1319
		private readonly bool m_filterEmptyGroups;

		// Token: 0x04000528 RID: 1320
		private readonly DataSetPlan m_plan;
	}
}
