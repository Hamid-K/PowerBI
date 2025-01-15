using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E6 RID: 230
	public interface IEdmDirectValueAnnotationBinding
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006A3 RID: 1699
		IEdmElement Element { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006A4 RID: 1700
		string NamespaceUri { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006A5 RID: 1701
		string Name { get; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006A6 RID: 1702
		object Value { get; }
	}
}
