using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001B2 RID: 434
	internal class ContextTableManager
	{
		// Token: 0x06000F55 RID: 3925 RVA: 0x0003E2F8 File Offset: 0x0003C4F8
		internal ContextTableManager()
		{
			this.m_contextTablesPerDataShape = new Dictionary<DataShape, ContextTableManager.ContextTablesAndFilters>();
			this.m_planOpsPerFilter = new Dictionary<FilterCondition, PlanOperation>();
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003E318 File Offset: 0x0003C518
		public bool TryGetContextTables(DataShape dataShape, out IList<PlanOperation> contextTables)
		{
			ContextTableManager.ContextTablesAndFilters contextTablesAndFilters;
			if (this.m_contextTablesPerDataShape.TryGetValue(dataShape, out contextTablesAndFilters))
			{
				contextTables = contextTablesAndFilters.ContextTables;
				return true;
			}
			contextTables = null;
			return false;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003E344 File Offset: 0x0003C544
		public bool TryGetFilterConditions(DataShape dataShape, out IList<FilterCondition> filters)
		{
			ContextTableManager.ContextTablesAndFilters contextTablesAndFilters;
			if (this.m_contextTablesPerDataShape.TryGetValue(dataShape, out contextTablesAndFilters))
			{
				filters = contextTablesAndFilters.Filters;
				return true;
			}
			filters = null;
			return false;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003E370 File Offset: 0x0003C570
		public ReadOnlyCollection<PlanOperation> GetContextTables(DataShape dataShape)
		{
			IList<PlanOperation> list;
			if (!this.TryGetContextTables(dataShape, out list))
			{
				list = new List<PlanOperation>();
			}
			return list.ToReadOnlyCollection<PlanOperation>();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003E394 File Offset: 0x0003C594
		public IList<FilterCondition> GetFilterConditions(DataShape dataShape)
		{
			IList<FilterCondition> list;
			if (!this.TryGetFilterConditions(dataShape, out list))
			{
				return new List<FilterCondition>();
			}
			return list;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003E3B3 File Offset: 0x0003C5B3
		public bool TryGetContextTableForFilterCondition(FilterCondition filter, out PlanOperation planOperation)
		{
			return this.m_planOpsPerFilter.TryGetValue(filter, out planOperation);
		}

		// Token: 0x04000735 RID: 1845
		protected readonly Dictionary<DataShape, ContextTableManager.ContextTablesAndFilters> m_contextTablesPerDataShape;

		// Token: 0x04000736 RID: 1846
		protected readonly Dictionary<FilterCondition, PlanOperation> m_planOpsPerFilter;

		// Token: 0x02000303 RID: 771
		protected struct ContextTablesAndFilters
		{
			// Token: 0x06001710 RID: 5904 RVA: 0x00052570 File Offset: 0x00050770
			public static ContextTableManager.ContextTablesAndFilters Create()
			{
				return new ContextTableManager.ContextTablesAndFilters
				{
					ContextTables = new List<PlanOperation>(),
					Filters = new List<FilterCondition>()
				};
			}

			// Token: 0x04000B1F RID: 2847
			public IList<PlanOperation> ContextTables;

			// Token: 0x04000B20 RID: 2848
			public IList<FilterCondition> Filters;
		}
	}
}
