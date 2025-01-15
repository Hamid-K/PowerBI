using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C0 RID: 448
	internal sealed class CoreTableArtifacts
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003FBEF File Offset: 0x0003DDEF
		internal CoreTableArtifacts(IReadOnlyList<PlanOperation> contextTables, IReadOnlyList<PlanOperationContext> coreTableFragments, BatchDataSetPlannerJoinPredicates joinPredicates, IReadOnlyList<FilterCondition> attributeFilters)
		{
			this.ContextTables = contextTables;
			this.CoreTableFragments = coreTableFragments;
			this.JoinPredicates = joinPredicates;
			this.AttributeFilters = attributeFilters;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0003FC14 File Offset: 0x0003DE14
		internal IReadOnlyList<PlanOperation> ContextTables { get; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0003FC1C File Offset: 0x0003DE1C
		internal IReadOnlyList<PlanOperationContext> CoreTableFragments { get; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0003FC24 File Offset: 0x0003DE24
		internal BatchDataSetPlannerJoinPredicates JoinPredicates { get; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0003FC2C File Offset: 0x0003DE2C
		internal IReadOnlyList<FilterCondition> AttributeFilters { get; }

		// Token: 0x0400076F RID: 1903
		internal static CoreTableArtifacts Empty = new CoreTableArtifacts(Util.EmptyReadOnlyList<PlanOperation>(), Util.EmptyReadOnlyList<PlanOperationContext>(), BatchDataSetPlannerJoinPredicates.Empty, Util.EmptyReadOnlyList<FilterCondition>());
	}
}
