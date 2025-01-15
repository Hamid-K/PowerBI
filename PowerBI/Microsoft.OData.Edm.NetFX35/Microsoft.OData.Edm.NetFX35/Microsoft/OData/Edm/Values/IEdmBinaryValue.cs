using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x02000087 RID: 135
	public interface IEdmBinaryValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000234 RID: 564
		byte[] Value { get; }
	}
}
