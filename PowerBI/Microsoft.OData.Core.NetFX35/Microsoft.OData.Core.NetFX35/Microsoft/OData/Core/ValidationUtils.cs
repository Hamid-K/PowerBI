using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200029F RID: 671
	internal static class ValidationUtils
	{
		// Token: 0x06001704 RID: 5892 RVA: 0x0004EEA7 File Offset: 0x0004D0A7
		internal static void ValidateOpenPropertyValue(string propertyName, object value)
		{
			if (value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ValidationUtils_OpenStreamProperty(propertyName));
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0004EEBD File Offset: 0x0004D0BD
		internal static void ValidateValueTypeKind(EdmTypeKind typeKind, string typeName)
		{
			if (typeKind != EdmTypeKind.Primitive && typeKind != EdmTypeKind.Enum && typeKind != EdmTypeKind.Complex && typeKind != EdmTypeKind.Collection)
			{
				throw new ODataException(Strings.ValidationUtils_IncorrectValueTypeKind(typeName, typeKind.ToString()));
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0004EEE8 File Offset: 0x0004D0E8
		internal static string ValidateCollectionTypeName(string collectionTypeName)
		{
			string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(collectionTypeName);
			if (collectionItemTypeName == null)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeName(collectionTypeName));
			}
			return collectionItemTypeName;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0004EF0C File Offset: 0x0004D10C
		internal static void ValidateEntityTypeIsAssignable(IEdmEntityTypeReference expectedEntityTypeReference, IEdmEntityTypeReference payloadEntityTypeReference)
		{
			if (!expectedEntityTypeReference.EntityDefinition().IsAssignableFrom(payloadEntityTypeReference.EntityDefinition()))
			{
				throw new ODataException(Strings.ValidationUtils_EntryTypeNotAssignableToExpectedType(payloadEntityTypeReference.FullName(), expectedEntityTypeReference.FullName()));
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0004EF38 File Offset: 0x0004D138
		internal static void ValidateComplexTypeIsAssignable(IEdmComplexType expectedComplexType, IEdmComplexType payloadComplexType)
		{
			if (!expectedComplexType.IsAssignableFrom(payloadComplexType))
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadComplexType.FullTypeName(), expectedComplexType.FullTypeName()));
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0004EF5C File Offset: 0x0004D15C
		internal static IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null && !typeReference.IsNonEntityCollectionType())
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeReference(typeReference.TypeKind()));
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0004EF92 File Offset: 0x0004D192
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

		// Token: 0x0600170B RID: 5899 RVA: 0x0004EFCB File Offset: 0x0004D1CB
		internal static void ValidateNullCollectionItem(IEdmTypeReference expectedItemType, ODataWriterBehavior writerBehavior)
		{
			if (expectedItemType != null && expectedItemType.IsODataPrimitiveTypeKind() && !expectedItemType.IsNullable && !writerBehavior.AllowNullValuesForNonNullablePrimitiveTypes)
			{
				throw new ODataException(Strings.ValidationUtils_NullCollectionItemForNonNullableType(expectedItemType.FullName()));
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0004EFF9 File Offset: 0x0004D1F9
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty)
		{
			if (edmProperty != null && !edmProperty.Type.IsStream())
			{
				throw new ODataException(Strings.ValidationUtils_MismatchPropertyKindForStreamProperty(streamProperty.Name));
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0004F01C File Offset: 0x0004D21C
		internal static void IncreaseAndValidateRecursionDepth(ref int recursionDepth, int maxDepth)
		{
			recursionDepth++;
			if (recursionDepth > maxDepth)
			{
				throw new ODataException(Strings.ValidationUtils_RecursionDepthLimitReached(maxDepth));
			}
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0004F03C File Offset: 0x0004D23C
		internal static void ValidateOperationNotNull(ODataOperation operation, bool isAction)
		{
			if (operation == null)
			{
				string text = (isAction ? "ODataEntry.Actions" : "ODataEntry.Functions");
				throw new ODataException(Strings.ValidationUtils_EnumerableContainsANullItem(text));
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0004F068 File Offset: 0x0004D268
		internal static void ValidateOperationMetadataNotNull(ODataOperation operation)
		{
			if (operation.Metadata == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(operation.GetType().Name));
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0004F08E File Offset: 0x0004D28E
		internal static void ValidateOperationTargetNotNull(ODataOperation operation)
		{
			if (operation.Target == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(operation.GetType().Name));
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0004F0B4 File Offset: 0x0004D2B4
		internal static void ValidateEntryMetadataResource(ODataEntry entry, IEdmEntityType entityType, IEdmModel model, bool validateMediaResource)
		{
			if (entityType != null && validateMediaResource)
			{
				if (entry.MediaResource == null)
				{
					if (entityType.HasStream)
					{
						throw new ODataException(Strings.ValidationUtils_EntryWithoutMediaResourceAndMLEType(entityType.FullTypeName()));
					}
				}
				else if (!entityType.HasStream)
				{
					throw new ODataException(Strings.ValidationUtils_EntryWithMediaResourceAndNonMLEType(entityType.FullTypeName()));
				}
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0004F104 File Offset: 0x0004D304
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmTypeReference expectedTypeReference)
		{
			Type type = value.GetType();
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(type);
			ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0004F128 File Offset: 0x0004D328
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

		// Token: 0x06001714 RID: 5908 RVA: 0x0004F178 File Offset: 0x0004D378
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

		// Token: 0x06001715 RID: 5909 RVA: 0x0004F200 File Offset: 0x0004D400
		[SuppressMessage("DataWeb.Usage", "AC0010", Justification = "Usage of ToString is safe in this context")]
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

		// Token: 0x06001716 RID: 5910 RVA: 0x0004F25F File Offset: 0x0004D45F
		internal static void ValidateServiceDocumentElementUrl(string serviceDocumentUrl)
		{
			if (serviceDocumentUrl == null)
			{
				throw new ODataException(Strings.ValidationUtils_ServiceDocumentElementUrlMustNotBeNull);
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0004F270 File Offset: 0x0004D470
		internal static void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, string typeName)
		{
			if (actualTypeKind == expectedTypeKind)
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

		// Token: 0x06001718 RID: 5912 RVA: 0x0004F2D8 File Offset: 0x0004D4D8
		internal static void ValidateBoundaryString(string boundary)
		{
			if (boundary == null || boundary.Length == 0 || boundary.Length > 70)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidBatchBoundaryDelimiterLength(boundary, 70));
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0004F302 File Offset: 0x0004D502
		internal static bool ShouldValidateComplexPropertyNullValue(IEdmModel model)
		{
			return true;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0004F305 File Offset: 0x0004D505
		internal static bool IsValidPropertyName(string propertyName)
		{
			return propertyName.IndexOfAny(ValidationUtils.InvalidCharactersInPropertyNames) < 0;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0004F348 File Offset: 0x0004D548
		internal static void ValidatePropertyName(string propertyName)
		{
			if (!ValidationUtils.IsValidPropertyName(propertyName))
			{
				string text = string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<char, string>(ValidationUtils.InvalidCharactersInPropertyNames, (char c) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { c }))));
				throw new ODataException(Strings.ValidationUtils_PropertiesMustNotContainReservedChars(propertyName, text));
			}
		}

		// Token: 0x04000A08 RID: 2568
		private const int MaxBoundaryLength = 70;

		// Token: 0x04000A09 RID: 2569
		internal static readonly char[] InvalidCharactersInPropertyNames = new char[] { ':', '.', '@' };
	}
}
