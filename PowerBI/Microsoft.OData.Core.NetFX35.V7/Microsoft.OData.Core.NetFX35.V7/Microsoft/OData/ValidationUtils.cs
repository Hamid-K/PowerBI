using System;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000B4 RID: 180
	internal static class ValidationUtils
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x00013BB1 File Offset: 0x00011DB1
		internal static void ValidateOpenPropertyValue(string propertyName, object value)
		{
			if (value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ValidationUtils_OpenStreamProperty(propertyName));
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00013BC7 File Offset: 0x00011DC7
		internal static void ValidateValueTypeKind(EdmTypeKind typeKind, string typeName)
		{
			if (typeKind != EdmTypeKind.Primitive && typeKind != EdmTypeKind.Enum && typeKind != EdmTypeKind.Collection && typeKind != EdmTypeKind.Untyped)
			{
				throw new ODataException(Strings.ValidationUtils_IncorrectValueTypeKind(typeName, typeKind.ToString()));
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00013BF4 File Offset: 0x00011DF4
		internal static string ValidateCollectionTypeName(string collectionTypeName)
		{
			string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(collectionTypeName);
			if (collectionItemTypeName == null)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeName(collectionTypeName));
			}
			return collectionItemTypeName;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00013C18 File Offset: 0x00011E18
		internal static void ValidateEntityTypeIsAssignable(IEdmEntityTypeReference expectedEntityTypeReference, IEdmEntityTypeReference payloadEntityTypeReference)
		{
			if (!expectedEntityTypeReference.EntityDefinition().IsAssignableFrom(payloadEntityTypeReference.EntityDefinition()))
			{
				throw new ODataException(Strings.ValidationUtils_ResourceTypeNotAssignableToExpectedType(payloadEntityTypeReference.FullName(), expectedEntityTypeReference.FullName()));
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00013C44 File Offset: 0x00011E44
		internal static void ValidateComplexTypeIsAssignable(IEdmComplexType expectedComplexType, IEdmComplexType payloadComplexType)
		{
			if (!expectedComplexType.IsAssignableFrom(payloadComplexType))
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadComplexType.FullTypeName(), expectedComplexType.FullTypeName()));
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00013C68 File Offset: 0x00011E68
		internal static IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null && !typeReference.IsNonEntityCollectionType())
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeReference(typeReference.TypeKind()));
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00013C9E File Offset: 0x00011E9E
		internal static void ValidateCollectionItem(object item, bool isNullable)
		{
			if (!isNullable && item == null)
			{
				throw new ODataException(Strings.ValidationUtils_NonNullableCollectionElementsMustNotBeNull);
			}
			if (item is ODataCollectionValue)
			{
				throw new ODataException(Strings.ValidationUtils_NestedCollectionsAreNotSupported);
			}
			if (item is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ValidationUtils_StreamReferenceValuesNotSupportedInCollections);
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00013CD7 File Offset: 0x00011ED7
		internal static void ValidateNullCollectionItem(IEdmTypeReference expectedItemType)
		{
			if (expectedItemType != null && expectedItemType.IsODataPrimitiveTypeKind() && !expectedItemType.IsNullable)
			{
				throw new ODataException(Strings.ValidationUtils_NullCollectionItemForNonNullableType(expectedItemType.FullName()));
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00013CFD File Offset: 0x00011EFD
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty)
		{
			if (edmProperty != null && !edmProperty.Type.IsStream())
			{
				throw new ODataException(Strings.ValidationUtils_MismatchPropertyKindForStreamProperty(streamProperty.Name));
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00013D20 File Offset: 0x00011F20
		internal static void IncreaseAndValidateRecursionDepth(ref int recursionDepth, int maxDepth)
		{
			recursionDepth++;
			if (recursionDepth > maxDepth)
			{
				throw new ODataException(Strings.ValidationUtils_RecursionDepthLimitReached(maxDepth));
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00013D40 File Offset: 0x00011F40
		internal static void ValidateOperationNotNull(ODataOperation operation, bool isAction)
		{
			if (operation == null)
			{
				string text = (isAction ? "ODataResource.Actions" : "ODataResource.Functions");
				throw new ODataException(Strings.ValidationUtils_EnumerableContainsANullItem(text));
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00013D6C File Offset: 0x00011F6C
		internal static void ValidateOperationMetadataNotNull(ODataOperation operation)
		{
			if (operation.Metadata == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(operation.GetType().Name));
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00013D92 File Offset: 0x00011F92
		internal static void ValidateOperationTargetNotNull(ODataOperation operation)
		{
			if (operation.Target == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(operation.GetType().Name));
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00013DB8 File Offset: 0x00011FB8
		internal static void ValidateMediaResource(ODataResource resource, IEdmEntityType resourceType)
		{
			if (resourceType != null)
			{
				if (resource.MediaResource == null)
				{
					if (resourceType.HasStream)
					{
						throw new ODataException(Strings.ValidationUtils_ResourceWithoutMediaResourceAndMLEType(resourceType.FullTypeName()));
					}
				}
				else if (!resourceType.HasStream)
				{
					throw new ODataException(Strings.ValidationUtils_ResourceWithMediaResourceAndNonMLEType(resourceType.FullTypeName()));
				}
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00013DF8 File Offset: 0x00011FF8
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmTypeReference expectedTypeReference)
		{
			Type type = value.GetType();
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(type);
			ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00013E1C File Offset: 0x0001201C
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference)
		{
			if (valuePrimitiveTypeReference == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
			}
			if (!expectedTypeReference.IsODataPrimitiveTypeKind() && !expectedTypeReference.IsODataTypeDefinitionTypeKind())
			{
				throw new ODataException(Strings.ValidationUtils_NonPrimitiveTypeForPrimitiveValue(expectedTypeReference.FullName()));
			}
			ValidationUtils.ValidateMetadataPrimitiveType(expectedTypeReference, valuePrimitiveTypeReference);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00013E6C File Offset: 0x0001206C
		internal static void ValidateMetadataPrimitiveType(IEdmTypeReference expectedTypeReference, IEdmTypeReference typeReferenceFromValue)
		{
			IEdmType definition = expectedTypeReference.Definition;
			IEdmPrimitiveType edmPrimitiveType = (IEdmPrimitiveType)typeReferenceFromValue.Definition;
			bool flag = expectedTypeReference.IsNullable == typeReferenceFromValue.IsNullable || (expectedTypeReference.IsNullable && !typeReferenceFromValue.IsNullable) || !typeReferenceFromValue.IsODataValueType();
			bool flag2 = definition.IsAssignableFrom(edmPrimitiveType);
			if (!flag || !flag2)
			{
				throw new ODataException(Strings.ValidationUtils_IncompatiblePrimitiveItemType(typeReferenceFromValue.FullName(), typeReferenceFromValue.IsNullable, expectedTypeReference.FullName(), expectedTypeReference.IsNullable));
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00013EF4 File Offset: 0x000120F4
		internal static void ValidateServiceDocumentElement(ODataServiceDocumentElement serviceDocumentElement, ODataFormat format)
		{
			if (serviceDocumentElement == null)
			{
				throw new ODataException(Strings.ValidationUtils_WorkspaceResourceMustNotContainNullItem);
			}
			if (serviceDocumentElement.Url == null)
			{
				throw new ODataException(Strings.ValidationUtils_ResourceMustSpecifyUrl);
			}
			if (format == ODataFormat.Json && string.IsNullOrEmpty(serviceDocumentElement.Name))
			{
				throw new ODataException(Strings.ValidationUtils_ResourceMustSpecifyName(serviceDocumentElement.Url.ToString()));
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00013F53 File Offset: 0x00012153
		internal static void ValidateServiceDocumentElementUrl(string serviceDocumentUrl)
		{
			if (serviceDocumentUrl == null)
			{
				throw new ODataException(Strings.ValidationUtils_ServiceDocumentElementUrlMustNotBeNull);
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00013F64 File Offset: 0x00012164
		internal static void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, bool? expectStructuredType, string typeName)
		{
			if (expectStructuredType != null && expectStructuredType.Value && (expectedTypeKind.IsStructured() || expectedTypeKind == EdmTypeKind.None) && actualTypeKind.IsStructured())
			{
				return;
			}
			if (expectedTypeKind == actualTypeKind)
			{
				return;
			}
			if (typeName == null)
			{
				throw new ODataException(Strings.ValidationUtils_IncorrectTypeKindNoTypeName(actualTypeKind.ToString(), expectedTypeKind.ToString()));
			}
			if ((actualTypeKind == EdmTypeKind.TypeDefinition && expectedTypeKind == EdmTypeKind.Primitive) || (actualTypeKind == EdmTypeKind.Primitive && expectedTypeKind == EdmTypeKind.TypeDefinition))
			{
				return;
			}
			throw new ODataException(Strings.ValidationUtils_IncorrectTypeKind(typeName, expectedTypeKind.ToString(), actualTypeKind.ToString()));
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00013FFA File Offset: 0x000121FA
		internal static void ValidateBoundaryString(string boundary)
		{
			if (boundary == null || boundary.Length == 0 || boundary.Length > 70)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidBatchBoundaryDelimiterLength(boundary, 70));
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00014024 File Offset: 0x00012224
		internal static bool IsValidPropertyName(string propertyName)
		{
			return propertyName.IndexOfAny(ValidationUtils.InvalidCharactersInPropertyNames) < 0;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00014034 File Offset: 0x00012234
		internal static void ValidatePropertyName(string propertyName)
		{
			if (!ValidationUtils.IsValidPropertyName(propertyName))
			{
				string text = string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<char, string>(ValidationUtils.InvalidCharactersInPropertyNames, (char c) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { c }))));
				throw new ODataException(Strings.ValidationUtils_PropertiesMustNotContainReservedChars(propertyName, text));
			}
		}

		// Token: 0x040002F9 RID: 761
		internal static readonly char[] InvalidCharactersInPropertyNames = new char[] { ':', '.', '@' };

		// Token: 0x040002FA RID: 762
		private const int MaxBoundaryLength = 70;
	}
}
