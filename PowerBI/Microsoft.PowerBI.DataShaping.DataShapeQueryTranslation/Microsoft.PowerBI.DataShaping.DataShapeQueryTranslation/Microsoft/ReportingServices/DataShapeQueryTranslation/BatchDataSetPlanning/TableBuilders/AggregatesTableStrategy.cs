using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001B7 RID: 439
	internal abstract class AggregatesTableStrategy
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x0003E792 File Offset: 0x0003C992
		protected AggregatesTableStrategy(IAggregatesPlanningContext plannerContext, DataShapeContext dsContext, IReadOnlyList<PlanOperation> contextTables)
		{
			this.m_plannerContext = plannerContext;
			this.m_dsContext = dsContext;
			this.m_contextTables = contextTables;
		}

		// Token: 0x06000F6E RID: 3950
		internal abstract PlanOperationContext ToTableContext();

		// Token: 0x0400073E RID: 1854
		protected readonly IAggregatesPlanningContext m_plannerContext;

		// Token: 0x0400073F RID: 1855
		protected readonly DataShapeContext m_dsContext;

		// Token: 0x04000740 RID: 1856
		protected readonly IReadOnlyList<PlanOperation> m_contextTables;
	}
}
