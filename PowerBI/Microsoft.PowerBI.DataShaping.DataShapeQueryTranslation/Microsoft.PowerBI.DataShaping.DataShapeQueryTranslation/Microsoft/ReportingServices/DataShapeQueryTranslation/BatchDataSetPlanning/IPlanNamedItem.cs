using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200019D RID: 413
	internal interface IPlanNamedItem : IStructuredToString
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000E96 RID: 3734
		string Name { get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000E97 RID: 3735
		PlanNamedItemKind Kind { get; }

		// Token: 0x06000E98 RID: 3736
		string ToString(ExpressionTable expressionTable);
	}
}
