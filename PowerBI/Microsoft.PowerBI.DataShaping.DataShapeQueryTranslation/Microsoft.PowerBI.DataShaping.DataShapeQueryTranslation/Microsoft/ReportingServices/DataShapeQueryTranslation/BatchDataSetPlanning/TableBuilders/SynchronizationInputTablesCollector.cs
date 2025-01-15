using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001E4 RID: 484
	internal sealed class SynchronizationInputTablesCollector
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x00045D2D File Offset: 0x00043F2D
		internal SynchronizationInputTablesCollector()
		{
			this.m_inputTablesForGroupSynchronization = new List<PlanOperationContext>();
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00045D40 File Offset: 0x00043F40
		internal IReadOnlyList<PlanOperationContext> References
		{
			get
			{
				return this.m_inputTablesForGroupSynchronization;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00045D48 File Offset: 0x00043F48
		internal void RegisterScopedLimitedTable(DataShapeContext dsContext, PlanOperationContext table)
		{
			if (!dsContext.HasSynchronizationDataShapes)
			{
				return;
			}
			this.m_inputTablesForGroupSynchronization.Add(table);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00045D60 File Offset: 0x00043F60
		internal bool TryGetTable(IReadOnlyList<IScope> scopes, out PlanOperationContext syncInputTable)
		{
			foreach (PlanOperationContext planOperationContext in this.m_inputTablesForGroupSynchronization)
			{
				if (planOperationContext.RowScopes.Equals(scopes))
				{
					syncInputTable = planOperationContext;
					return true;
				}
			}
			syncInputTable = null;
			return false;
		}

		// Token: 0x040007DA RID: 2010
		private readonly List<PlanOperationContext> m_inputTablesForGroupSynchronization;
	}
}
