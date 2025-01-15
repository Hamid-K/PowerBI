using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000093 RID: 147
	public interface IEdmEntityContainer : IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003BB RID: 955
		IEnumerable<IEdmEntityContainerElement> Elements { get; }

		// Token: 0x060003BC RID: 956
		IEdmEntitySet FindEntitySet(string setName);

		// Token: 0x060003BD RID: 957
		IEdmSingleton FindSingleton(string singletonName);

		// Token: 0x060003BE RID: 958
		IEnumerable<IEdmOperationImport> FindOperationImports(string operationName);
	}
}
