using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200018B RID: 395
	internal sealed class FilterDeclarationCollection : IFilterDeclarationCollection
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x00038346 File Offset: 0x00036546
		internal FilterDeclarationCollection(Dictionary<FilterCondition, PlanOperationDeclarationReference> filterDeclarations)
		{
			this.m_filterDeclarations = filterDeclarations;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00038355 File Offset: 0x00036555
		public bool TryGetFilterDeclaration(FilterCondition condition, out PlanOperationDeclarationReference declaration)
		{
			return this.m_filterDeclarations.TryGetValue(condition, out declaration);
		}

		// Token: 0x040006B4 RID: 1716
		private readonly Dictionary<FilterCondition, PlanOperationDeclarationReference> m_filterDeclarations;
	}
}
