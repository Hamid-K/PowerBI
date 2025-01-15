using System;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200018C RID: 396
	internal interface ICommonPlanningContext
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000DB6 RID: 3510
		ScopeTree ScopeTree { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000DB7 RID: 3511
		DataShapeAnnotations Annotations { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000DB8 RID: 3512
		WritableExpressionTable OutputExpressionTable { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000DB9 RID: 3513
		TranslationErrorContext ErrorContext { get; }
	}
}
