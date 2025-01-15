using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AC RID: 172
	public interface IEdmOperationImport : IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600049E RID: 1182
		IEdmOperation Operation { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600049F RID: 1183
		IEdmExpression EntitySet { get; }
	}
}
