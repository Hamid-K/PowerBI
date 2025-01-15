using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000104 RID: 260
	public interface IEdmLabeledExpression : IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000768 RID: 1896
		IEdmExpression Expression { get; }
	}
}
