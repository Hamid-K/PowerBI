using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x0200012D RID: 301
	internal abstract class EdmTypeResolver
	{
		// Token: 0x06000B88 RID: 2952
		internal abstract IEdmEntityType GetElementType(IEdmNavigationSource navigationSource);

		// Token: 0x06000B89 RID: 2953
		internal abstract IEdmTypeReference GetReturnType(IEdmOperationImport operationImport);

		// Token: 0x06000B8A RID: 2954
		internal abstract IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup);

		// Token: 0x06000B8B RID: 2955
		internal abstract IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter);
	}
}
