using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000098 RID: 152
	public interface IEdmOperationImport : IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003C2 RID: 962
		IEdmOperation Operation { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003C3 RID: 963
		IEdmExpression EntitySet { get; }
	}
}
