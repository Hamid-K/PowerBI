using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000134 RID: 308
	public interface IEdmStringValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007A8 RID: 1960
		string Value { get; }
	}
}
