using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000FF RID: 255
	internal static class MetadataUtils
	{
		// Token: 0x06000EEA RID: 3818 RVA: 0x00024F84 File Offset: 0x00023184
		internal static IEnumerable<IEdmDirectValueAnnotation> GetODataAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			IEnumerable<IEdmDirectValueAnnotation> enumerable = model.DirectValueAnnotations(annotatable);
			if (enumerable == null)
			{
				return null;
			}
			return enumerable.Where((IEdmDirectValueAnnotation a) => a.NamespaceUri == "http://docs.oasis-open.org/odata/ns/metadata");
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00024FC4 File Offset: 0x000231C4
		internal static IEdmType ResolveTypeNameForWrite(IEdmModel model, string typeName)
		{
			EdmTypeKind edmTypeKind;
			return MetadataUtils.ResolveTypeName(model, null, typeName, null, out edmTypeKind);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00024FDC File Offset: 0x000231DC
		internal static IEdmType ResolveTypeNameForRead(IEdmModel model, IEdmType expectedType, string typeName, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, out EdmTypeKind typeKind)
		{
			return MetadataUtils.ResolveTypeName(model, expectedType, typeName, clientCustomTypeResolver, out typeKind);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00024FEC File Offset: 0x000231EC
		internal static IEdmType ResolveTypeName(IEdmModel model, IEdmType expectedType, string typeName, Func<IEdmType, string, IEdmType> customTypeResolver, out EdmTypeKind typeKind)
		{
			IEdmType edmType = null;
			string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(typeName);
			if (collectionItemTypeName == null)
			{
				if (customTypeResolver != null && model.IsUserModel())
				{
					edmType = customTypeResolver(expectedType, typeName);
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

		// Token: 0x06000EEE RID: 3822 RVA: 0x00025084 File Offset: 0x00023284
		internal static IList<IEdmOperation> CalculateBindableOperationsForType(IEdmType bindingType, IEdmModel model, EdmTypeResolver edmTypeResolver)
		{
			IEnumerable<IEdmOperation> enumerable = null;
			try
			{
				enumerable = model.FindBoundOperations(bindingType);
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw new ODataException(Strings.MetadataUtils_CalculateBindableOperationsForType(bindingType.FullTypeName()), ex);
			}
			List<IEdmOperation> list = new List<IEdmOperation>();
			foreach (IEdmOperation edmOperation in enumerable)
			{
				if (!edmOperation.IsBound)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(edmOperation.Name));
				}
				if (edmOperation.Parameters.FirstOrDefault<IEdmOperationParameter>() == null)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(edmOperation.Name));
				}
				IEdmOperationParameter edmOperationParameter = edmOperation.Parameters.FirstOrDefault<IEdmOperationParameter>();
				IEdmType definition = edmTypeResolver.GetParameterType(edmOperationParameter).Definition;
				if (definition.IsAssignableFrom(bindingType))
				{
					list.Add(edmOperation);
				}
			}
			return list;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002516C File Offset: 0x0002336C
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

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002518E File Offset: 0x0002338E
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
