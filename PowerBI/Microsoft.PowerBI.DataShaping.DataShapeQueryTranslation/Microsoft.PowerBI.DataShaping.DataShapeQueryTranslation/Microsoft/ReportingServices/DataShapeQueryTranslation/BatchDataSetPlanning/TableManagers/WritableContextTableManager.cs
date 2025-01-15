using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001B3 RID: 435
	internal sealed class WritableContextTableManager : ContextTableManager
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x0003E3C2 File Offset: 0x0003C5C2
		internal WritableContextTableManager()
		{
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003E3CC File Offset: 0x0003C5CC
		public void RegisterContextTable(DataShape dataShape, PlanOperation contextTable, FilterCondition filter)
		{
			if (!this.m_planOpsPerFilter.ContainsKey(filter))
			{
				this.m_planOpsPerFilter.Add(filter, contextTable);
			}
			ContextTableManager.ContextTablesAndFilters contextTablesAndFilters;
			if (!this.m_contextTablesPerDataShape.TryGetValue(dataShape, out contextTablesAndFilters))
			{
				contextTablesAndFilters = ContextTableManager.ContextTablesAndFilters.Create();
				this.m_contextTablesPerDataShape.Add(dataShape, contextTablesAndFilters);
			}
			if (!contextTablesAndFilters.ContextTables.Contains(contextTable))
			{
				contextTablesAndFilters.ContextTables.Add(contextTable);
				contextTablesAndFilters.Filters.Add(filter);
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003E440 File Offset: 0x0003C640
		public void RegisterContextTables(DataShape dataShape, IList<PlanOperation> contextTables, IList<FilterCondition> filters)
		{
			for (int i = 0; i < contextTables.Count; i++)
			{
				this.RegisterContextTable(dataShape, contextTables[i], filters[i]);
			}
		}
	}
}
