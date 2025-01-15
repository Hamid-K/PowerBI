using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000037 RID: 55
	internal abstract class ResolvedStructureReferenceExpressionNode : ExpressionNode
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000273 RID: 627
		public abstract IIdentifiable Target { get; }
	}
}
