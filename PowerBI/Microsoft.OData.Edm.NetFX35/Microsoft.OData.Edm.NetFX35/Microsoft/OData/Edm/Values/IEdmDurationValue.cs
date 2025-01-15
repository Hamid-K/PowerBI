using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x020000C4 RID: 196
	public interface IEdmDurationValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000349 RID: 841
		TimeSpan Value { get; }
	}
}
