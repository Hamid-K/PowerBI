using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E8 RID: 232
	public interface IEdmPropertyValueBinding : IEdmElement
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006AC RID: 1708
		IEdmProperty BoundProperty { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006AD RID: 1709
		IEdmExpression Value { get; }
	}
}
