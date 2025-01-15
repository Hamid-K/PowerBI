using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B7 RID: 183
	internal interface IQueryEntityInternal : IPersistable, IQueryEntity, IValidationScope
	{
		// Token: 0x06000A27 RID: 2599
		Expression TryGetSecurityFilterCondition(CompilationContext ctx);
	}
}
