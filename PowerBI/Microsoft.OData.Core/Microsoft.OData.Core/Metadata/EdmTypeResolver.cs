using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000FD RID: 253
	internal abstract class EdmTypeResolver
	{
		// Token: 0x06000EDF RID: 3807
		internal abstract IEdmEntityType GetElementType(IEdmNavigationSource navigationSource);

		// Token: 0x06000EE0 RID: 3808
		internal abstract IEdmTypeReference GetReturnType(IEdmOperationImport operationImport);

		// Token: 0x06000EE1 RID: 3809
		internal abstract IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup);

		// Token: 0x06000EE2 RID: 3810
		internal abstract IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter);
	}
}
