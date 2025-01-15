using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x02000130 RID: 304
	internal static class MetadataUtils
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0002C288 File Offset: 0x0002A488
		internal static IEnumerable<IEdmDirectValueAnnotation> GetODataAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			IEnumerable<IEdmDirectValueAnnotation> enumerable = model.DirectValueAnnotations(annotatable);
			if (enumerable == null)
			{
				return null;
			}
			return Enumerable.Where<IEdmDirectValueAnnotation>(enumerable, (IEdmDirectValueAnnotation a) => a.NamespaceUri == "http://docs.oasis-open.org/odata/ns/metadata");
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002C2C8 File Offset: 0x0002A4C8
		internal static IEdmTypeReference GetEdmType(this ODataAnnotatable annotatable)
		{
			if (annotatable == null)
			{
				return null;
			}
			ODataTypeAnnotation annotation = annotatable.GetAnnotation<ODataTypeAnnotation>();
			if (annotation != null)
			{
				return annotation.Type;
			}
			return null;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002C2EC File Offset: 0x0002A4EC
		internal static IEdmType ResolveTypeNameForWrite(IEdmModel model, string typeName)
		{
			EdmTypeKind edmTypeKind;
			return MetadataUtils.ResolveTypeName(model, null, typeName, null, out edmTypeKind);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002C304 File Offset: 0x0002A504
		internal static IEdmType ResolveTypeNameForRead(IEdmModel model, IEdmType expectedType, string typeName, ODataReaderBehavior readerBehavior, out EdmTypeKind typeKind)
		{
			Func<IEdmType, string, IEdmType> func = ((readerBehavior == null) ? null : readerBehavior.TypeResolver);
			return MetadataUtils.ResolveTypeName(model, expectedType, typeName, func, out typeKind);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002C32C File Offset: 0x0002A52C
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "IEdmModel.FindType is allowed here and all other places should call this method to get to the type.")]
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

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002C3C4 File Offset: 0x0002A5C4
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "ExceptionUtils.IsCatchableExceptionType is being used correctly")]
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

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002C484 File Offset: 0x0002A684
		internal static IEdmTypeReference LookupTypeOfValueTerm(string qualifiedTermName, IEdmModel model)
		{
			IEdmTypeReference edmTypeReference = null;
			IEdmValueTerm edmValueTerm = model.FindValueTerm(qualifiedTermName);
			if (edmValueTerm != null)
			{
				edmTypeReference = edmValueTerm.Type;
			}
			return edmTypeReference;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002C4A6 File Offset: 0x0002A6A6
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
