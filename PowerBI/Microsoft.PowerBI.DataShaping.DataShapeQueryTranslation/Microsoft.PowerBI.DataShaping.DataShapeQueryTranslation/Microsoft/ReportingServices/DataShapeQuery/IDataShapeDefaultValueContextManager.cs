using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x0200000B RID: 11
	internal interface IDataShapeDefaultValueContextManager
	{
		// Token: 0x06000035 RID: 53
		PlanOperationClearDefaultContext ToPlanOperation(DataShape dataShape);

		// Token: 0x06000036 RID: 54
		void AddGrouping(DataMember dataMember);

		// Token: 0x06000037 RID: 55
		void AddFilter(DataShape dataShape, FilterCondition filterCondition);

		// Token: 0x06000038 RID: 56
		void AddDefaultValueFilters(DataShape dataShape);
	}
}
