using System;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A7 RID: 423
	internal interface ISubqueryPlanOperationGeneratorContext : ICommonPlanningContext
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000EE6 RID: 3814
		CalculationExpressionMap CalculationMap { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000EE7 RID: 3815
		DataTransformReferenceMap TransformReferenceMap { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000EE8 RID: 3816
		bool ApplyTransformsInQuery { get; }
	}
}
