using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000189 RID: 393
	internal interface IFilterDeclarationCollection
	{
		// Token: 0x06000DB0 RID: 3504
		bool TryGetFilterDeclaration(FilterCondition condition, out PlanOperationDeclarationReference declaration);
	}
}
