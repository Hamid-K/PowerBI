using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200008E RID: 142
	public interface IEdmLabeledExpression : IEdmNamedElement, IEdmExpression, IEdmElement
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600025E RID: 606
		IEdmExpression Expression { get; }
	}
}
