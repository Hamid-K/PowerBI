using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x020000A8 RID: 168
	public interface IEdmIntegerValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060002DB RID: 731
		long Value { get; }
	}
}
