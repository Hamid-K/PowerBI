using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001FE RID: 510
	internal sealed class PlanOperationDeclarationEqualityComparer : IEqualityComparer<PlanOperation>
	{
		// Token: 0x060011D0 RID: 4560 RVA: 0x00047D4E File Offset: 0x00045F4E
		internal PlanOperationDeclarationEqualityComparer(IEqualityComparer<FilterCondition> filterConditionComparer)
		{
			this.m_filterConditionComparer = filterConditionComparer;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00047D5D File Offset: 0x00045F5D
		public bool Equals(PlanOperation left, PlanOperation right)
		{
			if (left is PlanOperationCreateFilterContextTable && right is PlanOperationCreateFilterContextTable)
			{
				return this.Equals((PlanOperationCreateFilterContextTable)left, (PlanOperationCreateFilterContextTable)right);
			}
			return left.Equals(right);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00047D8C File Offset: 0x00045F8C
		public int GetHashCode(PlanOperation obj)
		{
			PlanOperationCreateFilterContextTable planOperationCreateFilterContextTable = obj as PlanOperationCreateFilterContextTable;
			if (planOperationCreateFilterContextTable != null)
			{
				return this.m_filterConditionComparer.GetHashCode(planOperationCreateFilterContextTable.Condition);
			}
			return obj.GetHashCode();
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00047DBB File Offset: 0x00045FBB
		public bool Equals(PlanOperationCreateFilterContextTable left, PlanOperationCreateFilterContextTable right)
		{
			return this.m_filterConditionComparer.Equals(left.Condition, right.Condition);
		}

		// Token: 0x04000812 RID: 2066
		private readonly IEqualityComparer<FilterCondition> m_filterConditionComparer;
	}
}
