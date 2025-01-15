using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200019B RID: 411
	internal sealed class PlanOperationTreeGeneratorResult
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x0003B60F File Offset: 0x0003980F
		internal PlanOperationTreeGeneratorResult(OutputTableMapping outputTables)
			: this(outputTables, null, null, null, null, null)
		{
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003B620 File Offset: 0x00039820
		internal PlanOperationTreeGeneratorResult(OutputTableMapping outputTables, PlanLimitInfo limitInfo, BatchRestartIndicator restartIndicator, PlanColumnSortItem columnIndexSort, IntermediateMemberDiscardConditions discardConditions, CalculationsWithSharedValues calculationsWithSharedValues)
		{
			this.OutputTables = outputTables;
			this.LimitInfo = limitInfo;
			this.RestartIndicator = restartIndicator;
			this.ColumnIndexSort = columnIndexSort;
			this.DiscardConditions = (discardConditions.IsNullOrEmpty<KeyValuePair<DataMember, IntermediateDiscardCondition>>() ? null : discardConditions);
			this.CalculationsWithSharedValues = calculationsWithSharedValues;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0003B66C File Offset: 0x0003986C
		internal OutputTableMapping OutputTables { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003B674 File Offset: 0x00039874
		internal PlanLimitInfo LimitInfo { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0003B67C File Offset: 0x0003987C
		internal BatchRestartIndicator RestartIndicator { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0003B684 File Offset: 0x00039884
		internal PlanColumnSortItem ColumnIndexSort { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0003B68C File Offset: 0x0003988C
		internal IntermediateMemberDiscardConditions DiscardConditions { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0003B694 File Offset: 0x00039894
		internal CalculationsWithSharedValues CalculationsWithSharedValues { get; }
	}
}
