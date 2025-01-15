using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x0200012F RID: 303
	internal sealed class EdmTypeWriterResolver : EdmTypeResolver
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0002C227 File Offset: 0x0002A427
		private EdmTypeWriterResolver()
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002C22F File Offset: 0x0002A42F
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "IEntitySetBase.ElementType is allowed here and the writer code paths should call this method to get to the ElementType of a set.")]
		internal override IEdmEntityType GetElementType(IEdmNavigationSource navigationSource)
		{
			return navigationSource.EntityType();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002C237 File Offset: 0x0002A437
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "operationImport.ReturnType is allowed here and the writer code paths should call this method to get to the ReturnType of a operation import.")]
		internal override IEdmTypeReference GetReturnType(IEdmOperationImport operationImport)
		{
			if (operationImport != null)
			{
				return operationImport.Operation.ReturnType;
			}
			return null;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002C249 File Offset: 0x0002A449
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup)
		{
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EdmTypeWriterResolver_GetReturnTypeForOperationImportGroup));
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002C25C File Offset: 0x0002A45C
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "operationParameter.Type is allowed here and the writer code paths should call this method to get to the Type of a function parameter.")]
		internal override IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter)
		{
			if (operationParameter != null)
			{
				return operationParameter.Type;
			}
			return null;
		}

		// Token: 0x040004BC RID: 1212
		internal static EdmTypeWriterResolver Instance = new EdmTypeWriterResolver();
	}
}
