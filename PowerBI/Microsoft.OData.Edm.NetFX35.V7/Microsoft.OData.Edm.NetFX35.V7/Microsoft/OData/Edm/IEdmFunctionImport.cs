using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009F RID: 159
	public interface IEdmFunctionImport : IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000476 RID: 1142
		bool IncludeInServiceDocument { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000477 RID: 1143
		IEdmFunction Function { get; }
	}
}
