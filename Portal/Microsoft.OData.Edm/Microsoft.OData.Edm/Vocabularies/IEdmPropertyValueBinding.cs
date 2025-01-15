using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E0 RID: 224
	public interface IEdmPropertyValueBinding : IEdmElement
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006CC RID: 1740
		IEdmProperty BoundProperty { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006CD RID: 1741
		IEdmExpression Value { get; }
	}
}
