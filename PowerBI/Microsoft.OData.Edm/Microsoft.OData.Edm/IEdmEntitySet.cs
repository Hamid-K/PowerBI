using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000096 RID: 150
	public interface IEdmEntitySet : IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003C1 RID: 961
		bool IncludeInServiceDocument { get; }
	}
}
