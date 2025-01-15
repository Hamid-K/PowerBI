using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000084 RID: 132
	public interface IEdmActionImport : IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000459 RID: 1113
		IEdmAction Action { get; }
	}
}
