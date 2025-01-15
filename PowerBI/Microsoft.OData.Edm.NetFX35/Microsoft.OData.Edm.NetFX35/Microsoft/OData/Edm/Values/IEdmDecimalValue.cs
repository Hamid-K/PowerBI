using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x020000A2 RID: 162
	public interface IEdmDecimalValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002C1 RID: 705
		decimal Value { get; }
	}
}
