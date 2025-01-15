using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200006E RID: 110
	internal interface IDsqTargetResolver
	{
		// Token: 0x060004BB RID: 1211
		MatchStatus TryTranslateToDsqScopeForFilter(IReadOnlyList<ResolvedQueryExpression> targets, DsqFilterType filterType, out Identifier targetScope);

		// Token: 0x060004BC RID: 1212
		bool TryTranslateToDsqScopeForVisualAxis(IReadOnlyList<ResolvedQueryExpression> targets, out Identifier scopeId);

		// Token: 0x060004BD RID: 1213
		bool TryResolveContextFilter(IReadOnlyList<ResolvedQueryExpression> targets, out DataShapeFilterResolutionResult result);
	}
}
