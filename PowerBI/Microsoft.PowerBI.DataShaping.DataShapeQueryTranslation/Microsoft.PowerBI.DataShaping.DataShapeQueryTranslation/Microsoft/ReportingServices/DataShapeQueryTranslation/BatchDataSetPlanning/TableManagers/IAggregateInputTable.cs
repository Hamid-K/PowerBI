using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001AE RID: 430
	internal interface IAggregateInputTable
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000F2D RID: 3885
		IScope OutputRowScope { get; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000F2E RID: 3886
		string TableName { get; }

		// Token: 0x06000F2F RID: 3887
		bool HasRequiredShowAll(IReadOnlyList<DataMember> requiredState);

		// Token: 0x06000F30 RID: 3888
		PlanOperation ToPlanOperation(DataShapeAnnotations annotations, ScopeTree scopeTree);
	}
}
