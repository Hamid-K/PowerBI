using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001F RID: 31
	public interface IEdmFunctionImport : IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A3 RID: 163
		bool IncludeInServiceDocument { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A4 RID: 164
		IEdmFunction Function { get; }
	}
}
