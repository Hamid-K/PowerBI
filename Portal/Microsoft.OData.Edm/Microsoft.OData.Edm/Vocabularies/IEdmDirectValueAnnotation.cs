using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DD RID: 221
	public interface IEdmDirectValueAnnotation : IEdmNamedElement, IEdmElement
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006C1 RID: 1729
		string NamespaceUri { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006C2 RID: 1730
		object Value { get; }
	}
}
