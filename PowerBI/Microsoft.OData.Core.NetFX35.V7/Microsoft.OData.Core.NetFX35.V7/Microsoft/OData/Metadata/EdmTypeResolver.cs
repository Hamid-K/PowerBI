using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D5 RID: 469
	internal abstract class EdmTypeResolver
	{
		// Token: 0x06001273 RID: 4723
		internal abstract IEdmEntityType GetElementType(IEdmNavigationSource navigationSource);

		// Token: 0x06001274 RID: 4724
		internal abstract IEdmTypeReference GetReturnType(IEdmOperationImport operationImport);

		// Token: 0x06001275 RID: 4725
		internal abstract IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup);

		// Token: 0x06001276 RID: 4726
		internal abstract IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter);
	}
}
