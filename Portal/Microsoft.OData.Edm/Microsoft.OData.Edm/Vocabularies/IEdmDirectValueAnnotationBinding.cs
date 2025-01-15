using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DE RID: 222
	public interface IEdmDirectValueAnnotationBinding
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006C3 RID: 1731
		IEdmElement Element { get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006C4 RID: 1732
		string NamespaceUri { get; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006C5 RID: 1733
		string Name { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006C6 RID: 1734
		object Value { get; }
	}
}
