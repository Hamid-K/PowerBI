using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008F RID: 143
	public interface IEdmEntityContainer : IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000464 RID: 1124
		IEnumerable<IEdmEntityContainerElement> Elements { get; }

		// Token: 0x06000465 RID: 1125
		IEdmEntitySet FindEntitySet(string setName);

		// Token: 0x06000466 RID: 1126
		IEdmSingleton FindSingleton(string singletonName);

		// Token: 0x06000467 RID: 1127
		IEnumerable<IEdmOperationImport> FindOperationImports(string operationName);
	}
}
