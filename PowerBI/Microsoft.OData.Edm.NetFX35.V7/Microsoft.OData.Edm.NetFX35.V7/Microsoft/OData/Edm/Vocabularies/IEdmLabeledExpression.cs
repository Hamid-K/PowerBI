using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010B RID: 267
	public interface IEdmLabeledExpression : IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600073B RID: 1851
		IEdmExpression Expression { get; }
	}
}
