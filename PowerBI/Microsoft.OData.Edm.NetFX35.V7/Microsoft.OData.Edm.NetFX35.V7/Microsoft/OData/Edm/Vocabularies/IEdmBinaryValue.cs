using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000125 RID: 293
	public interface IEdmBinaryValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600079B RID: 1947
		byte[] Value { get; }
	}
}
