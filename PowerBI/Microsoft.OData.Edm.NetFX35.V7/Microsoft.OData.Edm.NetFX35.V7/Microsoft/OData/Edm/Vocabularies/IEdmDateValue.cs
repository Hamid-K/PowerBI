using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000129 RID: 297
	public interface IEdmDateValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600079F RID: 1951
		Date Value { get; }
	}
}
