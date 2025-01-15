using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000111 RID: 273
	public interface IEdmDateTimeOffsetValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600077D RID: 1917
		DateTimeOffset Value { get; }
	}
}
