using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000119 RID: 281
	public interface IEdmIntegerValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000785 RID: 1925
		long Value { get; }
	}
}
