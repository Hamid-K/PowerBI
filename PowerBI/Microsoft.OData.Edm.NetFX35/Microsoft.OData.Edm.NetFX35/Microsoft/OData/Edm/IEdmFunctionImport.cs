using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007E RID: 126
	public interface IEdmFunctionImport : IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000200 RID: 512
		bool IncludeInServiceDocument { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000201 RID: 513
		IEdmFunction Function { get; }
	}
}
