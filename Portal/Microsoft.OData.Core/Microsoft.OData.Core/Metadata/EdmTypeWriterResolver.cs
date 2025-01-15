using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000FE RID: 254
	internal sealed class EdmTypeWriterResolver : EdmTypeResolver
	{
		// Token: 0x06000EE4 RID: 3812 RVA: 0x00024F34 File Offset: 0x00023134
		private EdmTypeWriterResolver()
		{
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00024F3C File Offset: 0x0002313C
		internal override IEdmEntityType GetElementType(IEdmNavigationSource navigationSource)
		{
			return navigationSource.EntityType();
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00024F44 File Offset: 0x00023144
		internal override IEdmTypeReference GetReturnType(IEdmOperationImport operationImport)
		{
			if (operationImport != null)
			{
				return operationImport.Operation.ReturnType;
			}
			return null;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00024F56 File Offset: 0x00023156
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup)
		{
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EdmTypeWriterResolver_GetReturnTypeForOperationImportGroup));
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00024F69 File Offset: 0x00023169
		internal override IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter)
		{
			if (operationParameter != null)
			{
				return operationParameter.Type;
			}
			return null;
		}

		// Token: 0x04000763 RID: 1891
		internal static EdmTypeWriterResolver Instance = new EdmTypeWriterResolver();
	}
}
