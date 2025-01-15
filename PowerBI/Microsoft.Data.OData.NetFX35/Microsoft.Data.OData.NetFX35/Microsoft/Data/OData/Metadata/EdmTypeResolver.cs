using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000148 RID: 328
	internal abstract class EdmTypeResolver
	{
		// Token: 0x060008B2 RID: 2226
		internal abstract IEdmEntityType GetElementType(IEdmEntitySet entitySet);

		// Token: 0x060008B3 RID: 2227
		internal abstract IEdmTypeReference GetReturnType(IEdmFunctionImport functionImport);

		// Token: 0x060008B4 RID: 2228
		internal abstract IEdmTypeReference GetReturnType(IEnumerable<IEdmFunctionImport> functionImportGroup);

		// Token: 0x060008B5 RID: 2229
		internal abstract IEdmTypeReference GetParameterType(IEdmFunctionParameter functionParameter);
	}
}
