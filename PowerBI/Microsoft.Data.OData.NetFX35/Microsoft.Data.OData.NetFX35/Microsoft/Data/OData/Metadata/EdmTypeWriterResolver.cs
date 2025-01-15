using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x0200014A RID: 330
	internal sealed class EdmTypeWriterResolver : EdmTypeResolver
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x0001C04F File Offset: 0x0001A24F
		private EdmTypeWriterResolver()
		{
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001C057 File Offset: 0x0001A257
		internal override IEdmEntityType GetElementType(IEdmEntitySet entitySet)
		{
			if (entitySet != null)
			{
				return entitySet.ElementType;
			}
			return null;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C064 File Offset: 0x0001A264
		internal override IEdmTypeReference GetReturnType(IEdmFunctionImport functionImport)
		{
			if (functionImport != null)
			{
				return functionImport.ReturnType;
			}
			return null;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C071 File Offset: 0x0001A271
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EdmTypeWriterResolver_GetReturnTypeForFunctionImportGroup));
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C084 File Offset: 0x0001A284
		internal override IEdmTypeReference GetParameterType(IEdmFunctionParameter functionParameter)
		{
			if (functionParameter != null)
			{
				return functionParameter.Type;
			}
			return null;
		}

		// Token: 0x04000357 RID: 855
		internal static EdmTypeWriterResolver Instance = new EdmTypeWriterResolver();
	}
}
