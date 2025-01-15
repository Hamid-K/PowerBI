using System;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000DE RID: 222
	internal static class ValidationUtils
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0000239D File Offset: 0x0000059D
		internal static void ValidateOpenPropertyValue(string propertyName, object value)
		{
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001BDFD File Offset: 0x00019FFD
		internal static void ValidateValueTypeKind(EdmTypeKind typeKind, string typeName)
		{
			if (typeKind != EdmTypeKind.Primitive && typeKind != EdmTypeKind.Enum && typeKind != EdmTypeKind.Collection && typeKind != EdmTypeKind.Untyped)
			{
				throw new ODataException(Strings.ValidationUtils_IncorrectValueTypeKind(typeName, typeKind.ToString()));
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001BE28 File Offset: 0x0001A028
		internal static string ValidateCollectionTypeName(string collectionTypeName)
		{
			string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(collectionTypeName);
			if (collectionItemTypeName == null)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeName(collectionTypeName));
			}
			return collectionItemTypeName;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001BE4C File Offset: 0x0001A04C
		internal static void ValidateEntityTypeIsAssignable(IEdmEntityTypeReference expectedEntityTypeReference, IEdmEntityTypeReference payloadEntityTypeReference)
		{
			if (!expectedEntityTypeReference.EntityDefinition().IsAssignableFrom(payloadEntityTypeReference.EntityDefinition()))
			{
				throw new ODataException(Strings.ValidationUtils_ResourceTypeNotAssignableToExpectedType(payloadEntityTypeReference.FullName(), expectedEntityTypeReference.FullName()));
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001BE78 File Offset: 0x0001A078
		internal static void ValidateComplexTypeIsAssignable(IEdmComplexType expectedComplexType, IEdmComplexType payloadComplexType)
		{
			if (!expectedComplexType.IsAssignableFrom(payloadComplexType))
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadComplexType.FullTypeName(), expectedComplexType.FullTypeName()));
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001BE9C File Offset: 0x0001A09C
		internal static IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null && !typeReference.IsNonEntityCollectionType())
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeReference(typeReference.TypeKind()));
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001BED2 File Offset: 0x0001A0D2
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

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001BF0B File Offset: 0x0001A10B
		internal static void ValidateNullCollectionItem(IEdmTypeReference expectedItemType)
		{
			if (expectedItemType != null && expectedItemType.IsODataPrimitiveTypeKind() && !expectedItemType.IsNullable)
			{
				throw new ODataException(Strings.ValidationUtils_NullCollectionItemForNonNullableType(expectedItemType.FullName()));
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001BF31 File Offset: 0x0001A131
		internal static void ValidateStreamPropertyInfo(IODataStreamReferenceInfo streamInfo, IEdmProperty edmProperty, string propertyName)
		{
			if (edmProperty != null && !edmProperty.Type.IsStream())
			{
				throw new ODataException(Strings.ValidationUtils_MismatchPropertyKindForStreamProperty(propertyName));
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001BF4F File Offset: 0x0001A14F
		internal static void IncreaseAndValidateRecursionDepth(ref int recursionDepth, int maxDepth)
		{
			recursionDepth++;
			if (recursionDepth > maxDepth)
			{
				throw new ODataException(Strings.ValidationUtils_RecursionDepthLimitReached(maxDepth));
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001BF70 File Offset: 0x0001A170
		internal static void ValidateOperationNotNull(ODataOperation operation, bool isAction)
		{
			if (operation == null)
			{
				string text = (isAction ? "ODataResource.Actions" : "ODataResource.Functions");
				throw new ODataException(Strings.ValidationUtils_EnumerableContainsANullItem(text));
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001BF9C File Offset: 0x0001A19C
		internal static void ValidateOperationMetadataNotNull(ODataOperation operation)
		{
			if (operation.Metadata == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(operation.GetType().Name));
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001BFC2 File Offset: 0x0001A1C2
		internal static void ValidateOperationTargetNotNull(ODataOperation operation)
		{
			if (operation.Target == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(operation.GetType().Name));
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		internal static void ValidateMediaResource(ODataResourceBase resource, IEdmEntityType resourceType)
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

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001C028 File Offset: 0x0001A228
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmTypeReference expectedTypeReference)
		{
			Type type = value.GetType();
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(type);
			ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001C04C File Offset: 0x0001A24C
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

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001C09C File Offset: 0x0001A29C
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

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001C124 File Offset: 0x0001A324
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

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001C183 File Offset: 0x0001A383
		internal static void ValidateServiceDocumentElementUrl(string serviceDocumentUrl)
		{
			if (serviceDocumentUrl == null)
			{
				throw new ODataException(Strings.ValidationUtils_ServiceDocumentElementUrlMustNotBeNull);
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001C194 File Offset: 0x0001A394
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
			if ((actualTypeKind == EdmTypeKind.TypeDefinition && expectedTypeKind == EdmTypeKind.Primitive) || (actualTypeKind == EdmTypeKind.Primitive && expectedTypeKind == EdmTypeKind.TypeDefinition) || (actualTypeKind == EdmTypeKind.Primitive && expectedTypeKind == EdmTypeKind.None))
			{
				return;
			}
			throw new ODataException(Strings.ValidationUtils_IncorrectTypeKind(typeName, expectedTypeKind.ToString(), actualTypeKind.ToString()));
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001C231 File Offset: 0x0001A431
		internal static void ValidateBoundaryString(string boundary)
		{
			if (boundary == null || boundary.Length == 0 || boundary.Length > 70)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidBatchBoundaryDelimiterLength(boundary, 70));
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001C25B File Offset: 0x0001A45B
		internal static bool IsValidPropertyName(string propertyName)
		{
			return propertyName.IndexOfAny(ValidationUtils.InvalidCharactersInPropertyNames) < 0;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001C26C File Offset: 0x0001A46C
		internal static void ValidatePropertyName(string propertyName)
		{
			if (!ValidationUtils.IsValidPropertyName(propertyName))
			{
				string text = string.Join(", ", ValidationUtils.InvalidCharactersInPropertyNames.Select((char c) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { c })).ToArray<string>());
				throw new ODataException(Strings.ValidationUtils_PropertiesMustNotContainReservedChars(propertyName, text));
			}
		}

		// Token: 0x040003C5 RID: 965
		internal static readonly char[] InvalidCharactersInPropertyNames = new char[] { ':', '.', '@' };

		// Token: 0x040003C6 RID: 966
		private const int MaxBoundaryLength = 70;
	}
}
