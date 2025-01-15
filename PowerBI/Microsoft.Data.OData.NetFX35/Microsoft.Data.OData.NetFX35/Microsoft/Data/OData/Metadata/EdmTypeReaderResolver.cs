using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000149 RID: 329
	internal sealed class EdmTypeReaderResolver : EdmTypeResolver
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x0001BF0D File Offset: 0x0001A10D
		public EdmTypeReaderResolver(IEdmModel model, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			this.model = model;
			this.readerBehavior = readerBehavior;
			this.version = version;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001BF2A File Offset: 0x0001A12A
		internal override IEdmEntityType GetElementType(IEdmEntitySet entitySet)
		{
			if (entitySet != null)
			{
				return (IEdmEntityType)this.ResolveType(entitySet.ElementType);
			}
			return null;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001BF42 File Offset: 0x0001A142
		internal override IEdmTypeReference GetReturnType(IEdmFunctionImport functionImport)
		{
			if (functionImport != null && functionImport.ReturnType != null)
			{
				return this.ResolveTypeReference(functionImport.ReturnType);
			}
			return null;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001BF60 File Offset: 0x0001A160
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			IEdmFunctionImport edmFunctionImport = Enumerable.FirstOrDefault<IEdmFunctionImport>(functionImportGroup);
			return this.GetReturnType(edmFunctionImport);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001BF7B File Offset: 0x0001A17B
		internal override IEdmTypeReference GetParameterType(IEdmFunctionParameter functionParameter)
		{
			if (functionParameter != null)
			{
				return this.ResolveTypeReference(functionParameter.Type);
			}
			return null;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001BF90 File Offset: 0x0001A190
		private IEdmTypeReference ResolveTypeReference(IEdmTypeReference typeReferenceToResolve)
		{
			if (this.readerBehavior.TypeResolver == null)
			{
				return typeReferenceToResolve;
			}
			return this.ResolveType(typeReferenceToResolve.Definition).ToTypeReference(typeReferenceToResolve.IsNullable);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
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
				IEdmType edmType = MetadataUtils.ResolveTypeName(this.model, null, elementType.FullName(), typeResolver, this.version, out edmTypeKind);
				return new EdmCollectionType(edmType.ToTypeReference(elementType.IsNullable));
			}
			return MetadataUtils.ResolveTypeName(this.model, null, typeToResolve.ODataFullName(), typeResolver, this.version, out edmTypeKind);
		}

		// Token: 0x04000354 RID: 852
		private readonly IEdmModel model;

		// Token: 0x04000355 RID: 853
		private readonly ODataReaderBehavior readerBehavior;

		// Token: 0x04000356 RID: 854
		private readonly ODataVersion version;
	}
}
