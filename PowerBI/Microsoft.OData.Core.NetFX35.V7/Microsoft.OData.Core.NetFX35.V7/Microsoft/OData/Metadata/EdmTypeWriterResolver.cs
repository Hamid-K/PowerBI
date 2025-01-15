using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D6 RID: 470
	internal sealed class EdmTypeWriterResolver : EdmTypeResolver
	{
		// Token: 0x06001278 RID: 4728 RVA: 0x00034E08 File Offset: 0x00033008
		private EdmTypeWriterResolver()
		{
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00034E10 File Offset: 0x00033010
		internal override IEdmEntityType GetElementType(IEdmNavigationSource navigationSource)
		{
			return navigationSource.EntityType();
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00034E18 File Offset: 0x00033018
		internal override IEdmTypeReference GetReturnType(IEdmOperationImport operationImport)
		{
			if (operationImport != null)
			{
				return operationImport.Operation.ReturnType;
			}
			return null;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00034E2A File Offset: 0x0003302A
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup)
		{
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EdmTypeWriterResolver_GetReturnTypeForOperationImportGroup));
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00034E3D File Offset: 0x0003303D
		internal override IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter)
		{
			if (operationParameter != null)
			{
				return operationParameter.Type;
			}
			return null;
		}

		// Token: 0x0400096F RID: 2415
		internal static EdmTypeWriterResolver Instance = new EdmTypeWriterResolver();
	}
}
