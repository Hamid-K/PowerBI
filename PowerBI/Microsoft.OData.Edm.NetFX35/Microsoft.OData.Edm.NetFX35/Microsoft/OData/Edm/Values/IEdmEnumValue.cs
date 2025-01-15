using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x0200016E RID: 366
	public interface IEdmEnumValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000702 RID: 1794
		IEdmPrimitiveValue Value { get; }
	}
}
