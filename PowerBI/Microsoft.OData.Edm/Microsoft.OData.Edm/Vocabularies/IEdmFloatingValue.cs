using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000117 RID: 279
	public interface IEdmFloatingValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000783 RID: 1923
		double Value { get; }
	}
}
