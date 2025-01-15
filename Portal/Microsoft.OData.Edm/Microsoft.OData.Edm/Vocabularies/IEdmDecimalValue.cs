using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000113 RID: 275
	public interface IEdmDecimalValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600077F RID: 1919
		decimal Value { get; }
	}
}
