using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000FC RID: 252
	internal sealed class EdmTypeReaderResolver : EdmTypeResolver
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00024E02 File Offset: 0x00023002
		public EdmTypeReaderResolver(IEdmModel model, Func<IEdmType, string, IEdmType> clientCustomTypeResolver)
		{
			this.model = model;
			this.clientCustomTypeResolver = clientCustomTypeResolver;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00024E18 File Offset: 0x00023018
		internal override IEdmEntityType GetElementType(IEdmNavigationSource navigationSource)
		{
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			if (edmEntityType == null)
			{
				return null;
			}
			return (IEdmEntityType)this.ResolveType(edmEntityType);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00024E3D File Offset: 0x0002303D
		internal override IEdmTypeReference GetReturnType(IEdmOperationImport operationImport)
		{
			if (operationImport != null && operationImport.Operation.ReturnType != null)
			{
				return this.ResolveTypeReference(operationImport.Operation.ReturnType);
			}
			return null;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00024E64 File Offset: 0x00023064
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup)
		{
			IEdmOperationImport edmOperationImport = functionImportGroup.FirstOrDefault<IEdmOperationImport>();
			return this.GetReturnType(edmOperationImport);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00024E7F File Offset: 0x0002307F
		internal override IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter)
		{
			if (operationParameter != null)
			{
				return this.ResolveTypeReference(operationParameter.Type);
			}
			return null;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00024E92 File Offset: 0x00023092
		private IEdmTypeReference ResolveTypeReference(IEdmTypeReference typeReferenceToResolve)
		{
			if (this.clientCustomTypeResolver == null)
			{
				return typeReferenceToResolve;
			}
			return this.ResolveType(typeReferenceToResolve.Definition).ToTypeReference(typeReferenceToResolve.IsNullable);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00024EB8 File Offset: 0x000230B8
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

		// Token: 0x04000761 RID: 1889
		private readonly IEdmModel model;

		// Token: 0x04000762 RID: 1890
		private readonly Func<IEdmType, string, IEdmType> clientCustomTypeResolver;
	}
}
