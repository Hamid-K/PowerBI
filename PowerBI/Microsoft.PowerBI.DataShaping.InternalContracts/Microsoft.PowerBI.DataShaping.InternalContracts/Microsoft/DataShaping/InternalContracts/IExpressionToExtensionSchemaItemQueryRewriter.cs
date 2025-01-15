using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000009 RID: 9
	internal interface IExpressionToExtensionSchemaItemQueryRewriter
	{
		// Token: 0x06000009 RID: 9
		bool TryRewrite(ResolvedQueryDefinition query, IQuerySchemaExtender querySchemaExtender, out ResolvedQueryDefinition newQuery, out IReadOnlyList<EngineMessageBase> errorMessages, out SparklineDataStatistics sparklineStatistics);
	}
}
