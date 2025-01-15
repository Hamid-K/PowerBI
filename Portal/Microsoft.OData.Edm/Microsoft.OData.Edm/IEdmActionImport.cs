using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001C RID: 28
	public interface IEdmActionImport : IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A0 RID: 160
		IEdmAction Action { get; }
	}
}
