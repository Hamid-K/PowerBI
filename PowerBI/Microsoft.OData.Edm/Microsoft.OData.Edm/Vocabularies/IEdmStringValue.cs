using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011D RID: 285
	public interface IEdmStringValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000787 RID: 1927
		string Value { get; }
	}
}
