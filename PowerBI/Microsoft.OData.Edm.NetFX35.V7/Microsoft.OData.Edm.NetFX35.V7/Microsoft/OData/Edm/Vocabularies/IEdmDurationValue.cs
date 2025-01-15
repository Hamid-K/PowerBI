using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012C RID: 300
	public interface IEdmDurationValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007A2 RID: 1954
		TimeSpan Value { get; }
	}
}
