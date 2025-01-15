using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x0200023A RID: 570
	internal sealed class IntermediateScopedLimitState
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x0004B714 File Offset: 0x00049914
		public IntermediateScopedLimitState(Limit limit, IReadOnlyList<DataMember> groupScopesFromTargets, ExpressionNode dbCount, PlanOperationContext targetScopeTable, PlanOperationContext parentAndTargetScopeTable)
		{
			this.Limit = limit;
			this.GroupScopesFromTargets = groupScopesFromTargets;
			this.DbCount = dbCount;
			this.TargetScopeTable = targetScopeTable;
			this.ParentAndTargetScopeTable = parentAndTargetScopeTable;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0004B741 File Offset: 0x00049941
		public Limit Limit { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0004B749 File Offset: 0x00049949
		public IReadOnlyList<DataMember> GroupScopesFromTargets { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0004B751 File Offset: 0x00049951
		public ExpressionNode DbCount { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0004B759 File Offset: 0x00049959
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x0004B761 File Offset: 0x00049961
		public ExpressionNode TargetCountOverride { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0004B76A File Offset: 0x0004996A
		public PlanOperationContext TargetScopeTable { get; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0004B772 File Offset: 0x00049972
		public PlanOperationContext ParentAndTargetScopeTable { get; }
	}
}
