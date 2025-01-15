using System;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001CB RID: 459
	internal sealed class KeyPointsTable
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x00042E5D File Offset: 0x0004105D
		internal KeyPointsTable(PlanOperationContext keyPointsPlanOperation, PlanDeclarationCollection declarations)
		{
			this.m_keyPointsPlanOperation = keyPointsPlanOperation;
			this.m_declarations = declarations;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00042E73 File Offset: 0x00041073
		internal PlanOperationContext ApplyAndDeclare(PlanOperationContext input, string declarationName)
		{
			if (this.m_keyPointsPlanOperation == null)
			{
				return input;
			}
			input = input.UnionDistinct(this.m_keyPointsPlanOperation.Table);
			return input.DeclareIfNotDeclared(declarationName, this.m_declarations, false, null, false);
		}

		// Token: 0x0400078A RID: 1930
		private readonly PlanOperationContext m_keyPointsPlanOperation;

		// Token: 0x0400078B RID: 1931
		private readonly PlanDeclarationCollection m_declarations;
	}
}
