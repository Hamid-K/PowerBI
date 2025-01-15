using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x020000A5 RID: 165
	public interface IEdmFloatingValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002CE RID: 718
		double Value { get; }
	}
}
