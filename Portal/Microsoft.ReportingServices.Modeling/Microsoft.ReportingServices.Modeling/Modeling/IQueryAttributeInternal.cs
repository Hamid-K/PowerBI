using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B8 RID: 184
	internal interface IQueryAttributeInternal : IPersistable, IQueryAttribute, IValidationScope
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A28 RID: 2600
		bool ReplaceWithExpression { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A29 RID: 2601
		IQueryEntity SourceEntity { get; }

		// Token: 0x06000A2A RID: 2602
		bool CheckReference(CompilationContext ctx, string propertyName, bool multipleInScope);
	}
}
