using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000117 RID: 279
	public interface IEdmEntityContainer : IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000588 RID: 1416
		IEnumerable<IEdmEntityContainerElement> Elements { get; }

		// Token: 0x06000589 RID: 1417
		IEdmEntitySet FindEntitySet(string setName);

		// Token: 0x0600058A RID: 1418
		IEdmSingleton FindSingleton(string singletonName);

		// Token: 0x0600058B RID: 1419
		IEnumerable<IEdmOperationImport> FindOperationImports(string operationName);
	}
}
