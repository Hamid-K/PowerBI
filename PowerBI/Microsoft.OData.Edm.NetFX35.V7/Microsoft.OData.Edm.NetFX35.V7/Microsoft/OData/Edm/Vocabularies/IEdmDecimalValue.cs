using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012A RID: 298
	public interface IEdmDecimalValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007A0 RID: 1952
		decimal Value { get; }
	}
}
