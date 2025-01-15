using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010E RID: 270
	public interface IEdmBinaryValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600077A RID: 1914
		byte[] Value { get; }
	}
}
