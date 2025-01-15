using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000094 RID: 148
	public interface IEdmEntitySet : IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600046B RID: 1131
		bool IncludeInServiceDocument { get; }
	}
}
