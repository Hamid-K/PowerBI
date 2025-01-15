using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E5 RID: 229
	public interface IEdmDirectValueAnnotation : IEdmNamedElement, IEdmElement
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006A1 RID: 1697
		string NamespaceUri { get; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006A2 RID: 1698
		object Value { get; }
	}
}
