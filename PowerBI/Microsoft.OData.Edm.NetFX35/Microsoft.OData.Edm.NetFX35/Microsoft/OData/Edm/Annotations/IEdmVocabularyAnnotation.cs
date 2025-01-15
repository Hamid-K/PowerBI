using System;

namespace Microsoft.OData.Edm.Annotations
{
	// Token: 0x020000C7 RID: 199
	public interface IEdmVocabularyAnnotation : IEdmElement
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000356 RID: 854
		string Qualifier { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000357 RID: 855
		IEdmTerm Term { get; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000358 RID: 856
		IEdmVocabularyAnnotatable Target { get; }
	}
}
