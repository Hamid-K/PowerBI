using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012E RID: 302
	public interface IEdmFloatingValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007A4 RID: 1956
		double Value { get; }
	}
}
