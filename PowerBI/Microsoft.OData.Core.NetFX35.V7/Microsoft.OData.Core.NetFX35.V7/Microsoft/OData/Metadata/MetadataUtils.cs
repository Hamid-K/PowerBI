using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D7 RID: 471
	internal static class MetadataUtils
	{
		// Token: 0x0600127E RID: 4734 RVA: 0x00034E58 File Offset: 0x00033058
		internal static IEnumerable<IEdmDirectValueAnnotation> GetODataAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			IEnumerable<IEdmDirectValueAnnotation> enumerable = model.DirectValueAnnotations(annotatable);
			if (enumerable == null)
			{
				return null;
			}
			return Enumerable.Where<IEdmDirectValueAnnotation>(enumerable, (IEdmDirectValueAnnotation a) => a.NamespaceUri == "http://docs.oasis-open.org/odata/ns/metadata");
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00034E98 File Offset: 0x00033098
		internal static IEdmType ResolveTypeNameForWrite(IEdmModel model, string typeName)
		{
			EdmTypeKind edmTypeKind;
			return MetadataUtils.ResolveTypeName(model, null, typeName, null, out edmTypeKind);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00034EB0 File Offset: 0x000330B0
		internal static IEdmType ResolveTypeNameForRead(IEdmModel model, IEdmType expectedType, string typeName, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, out EdmTypeKind typeKind)
		{
			return MetadataUtils.ResolveTypeName(model, expectedType, typeName, clientCustomTypeResolver, out typeKind);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00034EC0 File Offset: 0x000330C0
		internal static IEdmType ResolveTypeName(IEdmModel model, IEdmType expectedType, string typeName, Func<IEdmType, string, IEdmType> customTypeResolver, out EdmTypeKind typeKind)
		{
			IEdmType edmType = null;
			string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(typeName);
			if (collectionItemTypeName == null)
			{
				if (customTypeResolver != null && model.IsUserModel())
				{
					edmType = customTypeResolver.Invoke(expectedType, typeName);
					if (edmType == null)
					{
						throw new ODataException(Strings.MetadataUtils_ResolveTypeName(typeName));
					}
				}
				else
				{
					edmType = model.FindType(typeName);
				}
				typeKind = ((edmType == null) ? EdmTypeKind.None : edmType.TypeKind);
			}
			else
			{
				typeKind = EdmTypeKind.Collection;
				IEdmType edmType2 = null;
				if (customTypeResolver != null && expectedType != null && expectedType.TypeKind == EdmTypeKind.Collection)
				{
					edmType2 = ((IEdmCollectionType)expectedType).ElementType.Definition;
				}
				EdmTypeKind edmTypeKind;
				IEdmType edmType3 = MetadataUtils.ResolveTypeName(model, edmType2, collectionItemTypeName, customTypeResolver, out edmTypeKind);
				if (edmType3 != null)
				{
					edmType = EdmLibraryExtensions.GetCollectionType(edmType3);
				}
			}
			return edmType;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00034F58 File Offset: 0x00033158
		internal static IEdmOperation[] CalculateBindableOperationsForType(IEdmType bindingType, IEdmModel model, EdmTypeResolver edmTypeResolver)
		{
			List<IEdmOperation> list = null;
			try
			{
				list = Enumerable.ToList<IEdmOperation>(model.FindBoundOperations(bindingType));
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw new ODataException(Strings.MetadataUtils_CalculateBindableOperationsForType(bindingType.FullTypeName()), ex);
			}
			List<IEdmOperation> list2 = new List<IEdmOperation>();
			foreach (IEdmOperation edmOperation in list.EnsureOperationsBoundWithBindingParameter())
			{
				IEdmOperationParameter edmOperationParameter = Enumerable.FirstOrDefault<IEdmOperationParameter>(edmOperation.Parameters);
				IEdmType definition = edmTypeResolver.GetParameterType(edmOperationParameter).Definition;
				if (definition.IsAssignableFrom(bindingType))
				{
					list2.Add(edmOperation);
				}
			}
			return list2.ToArray();
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00035014 File Offset: 0x00033214
		internal static IEdmTypeReference LookupTypeOfTerm(string qualifiedTermName, IEdmModel model)
		{
			IEdmTypeReference edmTypeReference = null;
			IEdmTerm edmTerm = model.FindTerm(qualifiedTermName);
			if (edmTerm != null)
			{
				edmTypeReference = edmTerm.Type;
			}
			return edmTypeReference;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00035036 File Offset: 0x00033236
		internal static IEdmTypeReference GetNullablePayloadTypeReference(IEdmType payloadType)
		{
			if (payloadType != null)
			{
				return payloadType.ToTypeReference(true);
			}
			return null;
		}
	}
}
