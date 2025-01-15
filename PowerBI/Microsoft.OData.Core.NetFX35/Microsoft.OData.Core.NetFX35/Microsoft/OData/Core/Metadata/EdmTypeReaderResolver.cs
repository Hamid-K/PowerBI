using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x0200012E RID: 302
	internal sealed class EdmTypeReaderResolver : EdmTypeResolver
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x0002C0E1 File Offset: 0x0002A2E1
		public EdmTypeReaderResolver(IEdmModel model, ODataReaderBehavior readerBehavior)
		{
			this.model = model;
			this.readerBehavior = readerBehavior;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002C0F8 File Offset: 0x0002A2F8
		internal override IEdmEntityType GetElementType(IEdmNavigationSource navigationSource)
		{
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			if (edmEntityType == null)
			{
				return null;
			}
			return (IEdmEntityType)this.ResolveType(edmEntityType);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002C11D File Offset: 0x0002A31D
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "operationImport.ReturnType is allowed here and the reader code paths should call this method to get to the ReturnType of a operation import.")]
		internal override IEdmTypeReference GetReturnType(IEdmOperationImport operationImport)
		{
			if (operationImport != null && operationImport.Operation.ReturnType != null)
			{
				return this.ResolveTypeReference(operationImport.Operation.ReturnType);
			}
			return null;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002C144 File Offset: 0x0002A344
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmOperationImport> functionImportGroup)
		{
			IEdmOperationImport edmOperationImport = Enumerable.FirstOrDefault<IEdmOperationImport>(functionImportGroup);
			return this.GetReturnType(edmOperationImport);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002C15F File Offset: 0x0002A35F
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "operationParameter.Type is allowed here and the reader code paths should call this method to get to the Type of a operation parameter.")]
		internal override IEdmTypeReference GetParameterType(IEdmOperationParameter operationParameter)
		{
			if (operationParameter != null)
			{
				return this.ResolveTypeReference(operationParameter.Type);
			}
			return null;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002C174 File Offset: 0x0002A374
		private IEdmTypeReference ResolveTypeReference(IEdmTypeReference typeReferenceToResolve)
		{
			if (this.readerBehavior.TypeResolver == null)
			{
				return typeReferenceToResolve;
			}
			return this.ResolveType(typeReferenceToResolve.Definition).ToTypeReference(typeReferenceToResolve.IsNullable);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		private IEdmType ResolveType(IEdmType typeToResolve)
		{
			Func<IEdmType, string, IEdmType> typeResolver = this.readerBehavior.TypeResolver;
			if (typeResolver == null)
			{
				return typeToResolve;
			}
			IEdmCollectionType edmCollectionType = typeToResolve as IEdmCollectionType;
			EdmTypeKind edmTypeKind;
			if (edmCollectionType != null && edmCollectionType.ElementType.IsEntity())
			{
				IEdmTypeReference elementType = edmCollectionType.ElementType;
				IEdmType edmType = MetadataUtils.ResolveTypeName(this.model, null, elementType.FullName(), typeResolver, out edmTypeKind);
				return new EdmCollectionType(edmType.ToTypeReference(elementType.IsNullable));
			}
			return MetadataUtils.ResolveTypeName(this.model, null, typeToResolve.FullTypeName(), typeResolver, out edmTypeKind);
		}

		// Token: 0x040004BA RID: 1210
		private readonly IEdmModel model;

		// Token: 0x040004BB RID: 1211
		private readonly ODataReaderBehavior readerBehavior;
	}
}
