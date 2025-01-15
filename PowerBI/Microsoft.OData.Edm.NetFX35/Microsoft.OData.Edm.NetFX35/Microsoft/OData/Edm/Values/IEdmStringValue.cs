using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x020000BE RID: 190
	public interface IEdmStringValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000332 RID: 818
		string Value { get; }
	}
}
