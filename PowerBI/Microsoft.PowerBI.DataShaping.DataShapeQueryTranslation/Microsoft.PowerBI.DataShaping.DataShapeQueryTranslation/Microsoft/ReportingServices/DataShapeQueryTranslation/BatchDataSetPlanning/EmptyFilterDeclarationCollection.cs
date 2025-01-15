using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200018A RID: 394
	internal sealed class EmptyFilterDeclarationCollection : IFilterDeclarationCollection
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003832C File Offset: 0x0003652C
		public bool TryGetFilterDeclaration(FilterCondition condition, out PlanOperationDeclarationReference declaration)
		{
			declaration = null;
			return false;
		}

		// Token: 0x040006B3 RID: 1715
		internal static readonly EmptyFilterDeclarationCollection Instance = new EmptyFilterDeclarationCollection();
	}
}
