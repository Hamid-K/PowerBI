using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200005C RID: 92
	internal interface IDsqFilterConditionGenerator
	{
		// Token: 0x06000423 RID: 1059
		List<GeneratedFilterCondition> Generate(ResolvedQueryExpression expr, ExpressionContext expressionContext, IReadOnlyList<ResolvedQueryExpression> targets, out FilterUsageKind filterUsageKind);
	}
}
