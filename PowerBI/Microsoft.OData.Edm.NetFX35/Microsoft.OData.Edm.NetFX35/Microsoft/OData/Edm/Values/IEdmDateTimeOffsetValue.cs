using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x02000076 RID: 118
	public interface IEdmDateTimeOffsetValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001D1 RID: 465
		DateTimeOffset Value { get; }
	}
}
