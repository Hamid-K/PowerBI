using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012F RID: 303
	public interface IEdmGuidValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007A5 RID: 1957
		Guid Value { get; }
	}
}
