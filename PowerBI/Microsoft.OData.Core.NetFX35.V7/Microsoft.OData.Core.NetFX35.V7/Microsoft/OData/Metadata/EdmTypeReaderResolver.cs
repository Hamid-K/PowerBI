using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D4 RID: 468
	internal sealed class EdmTypeReaderResolver : EdmTypeResolver
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x00034CD6 File Offset: 0x00032ED6
		public EdmTypeReaderResolver(IEdmModel model, Func<IEdmType, string, IEdmType> clientCustomTypeResolver)
		{
			this.model = model;
			this.clientCustomTypeResolver = clientCustomTypeResolver;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00034CEC File Offset: 0x00032EEC
		internal override IEdmEntityType GetElementType(IEdmNavigationSource navigationSource)
		{
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			if (edmEntityType == null)
			{
				return null;
			}
			return (IEdmEntityType)this.ResolveType(edmEntityType);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00034D11 File Offset: 0x00032F11
		internal override IEdmTypeReference GetReturnType(IEdmOperationImport operationImport)
		{
			if (operationImport != null && operationImport.Operation.ReturnType != null)
			{
				return this.ResolveTypeReference(operationImport.Operation.ReturnType);
			}
			return null;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00034D38 File Offset: 0x00032F38
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup)
		{
			IEdmOperationImport edmOperationImport = Enumerable.FirstOrDefault<IEdmOperationImport>(functionImportGroup);
			return this.GetReturnType(edmOperationImport);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00034D53 File Offset: 0x00032F53
		internal override IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter)
		{
			if (operationParameter != null)
			{
				return this.ResolveTypeReference(operationParameter.Type);
			}
			return null;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00034D66 File Offset: 0x00032F66
		private IEdmTypeReference ResolveTypeReference(IEdmTypeReference typeReferenceToResolve)
		{
			if (this.clientCustomTypeResolver == null)
			{
				return typeReferenceToResolve;
			}
			return this.ResolveType(typeReferenceToResolve.Definition).ToTypeReference(typeReferenceToResolve.IsNullable);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00034D8C File Offset: 0x00032F8C
		private IEdmType ResolveType(IEdmType typeToResolve)
		{
			if (this.clientCustomTypeResolver == null)
			{
				return typeToResolve;
			}
			IEdmCollectionType edmCollectionType = typeToResolve as IEdmCollectionType;
			EdmTypeKind edmTypeKind;
			if (edmCollectionType != null && edmCollectionType.ElementType.IsEntity())
			{
				IEdmTypeReference elementType = edmCollectionType.ElementType;
				IEdmType edmType = MetadataUtils.ResolveTypeName(this.model, null, elementType.FullName(), this.clientCustomTypeResolver, out edmTypeKind);
				return new EdmCollectionType(edmType.ToTypeReference(elementType.IsNullable));
			}
			return MetadataUtils.ResolveTypeName(this.model, null, typeToResolve.FullTypeName(), this.clientCustomTypeResolver, out edmTypeKind);
		}

		// Token: 0x0400096D RID: 2413
		private readonly IEdmModel model;

		// Token: 0x0400096E RID: 2414
		private readonly Func<IEdmType, string, IEdmType> clientCustomTypeResolver;
	}
}
