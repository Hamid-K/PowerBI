using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000069 RID: 105
	public interface IEdmActionImport : IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000197 RID: 407
		IEdmAction Action { get; }
	}
}
