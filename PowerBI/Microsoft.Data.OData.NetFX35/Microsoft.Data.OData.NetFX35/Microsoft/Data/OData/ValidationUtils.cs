using System;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000256 RID: 598
	internal static class ValidationUtils
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x0004613C File Offset: 0x0004433C
		internal static void ValidateOpenPropertyValue(string propertyName, object value, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			bool flag = !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
			if (flag && value is ODataCollectionValue)
			{
				throw new ODataException(Strings.ValidationUtils_OpenCollectionProperty(propertyName));
			}
			if (value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ValidationUtils_OpenStreamProperty(propertyName));
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004618B File Offset: 0x0004438B
		internal static void ValidateValueTypeKind(EdmTypeKind typeKind, string typeName)
		{
			if (typeKind != EdmTypeKind.Primitive && typeKind != EdmTypeKind.Complex && typeKind != EdmTypeKind.Collection)
			{
				throw new ODataException(Strings.ValidationUtils_IncorrectValueTypeKind(typeName, typeKind.ToString()));
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000461B0 File Offset: 0x000443B0
		internal static string ValidateCollectionTypeName(string collectionTypeName)
		{
			string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(collectionTypeName);
			if (collectionItemTypeName == null)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeName(collectionTypeName));
			}
			return collectionItemTypeName;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000461D4 File Offset: 0x000443D4
		internal static void ValidateEntityTypeIsAssignable(IEdmEntityTypeReference expectedEntityTypeReference, IEdmEntityTypeReference payloadEntityTypeReference)
		{
			if (!expectedEntityTypeReference.EntityDefinition().IsAssignableFrom(payloadEntityTypeReference.EntityDefinition()))
			{
				throw new ODataException(Strings.ValidationUtils_EntryTypeNotAssignableToExpectedType(payloadEntityTypeReference.ODataFullName(), expectedEntityTypeReference.ODataFullName()));
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00046200 File Offset: 0x00044400
		internal static IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null && !typeReference.IsNonEntityCollectionType())
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeReference(typeReference.TypeKind()));
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00046236 File Offset: 0x00044436
		internal static void ValidateCollectionItem(object item, bool isStreamable)
		{
			if (!isStreamable && item == null)
			{
				throw new ODataException(Strings.ValidationUtils_NonStreamingCollectionElementsMustNotBeNull);
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

		// Token: 0x0600128C RID: 4748 RVA: 0x0004626F File Offset: 0x0004446F
		internal static void ValidateNullCollectionItem(IEdmTypeReference expectedItemType, ODataWriterBehavior writerBehavior)
		{
			if (expectedItemType != null && expectedItemType.IsODataPrimitiveTypeKind() && !expectedItemType.IsNullable && !writerBehavior.AllowNullValuesForNonNullablePrimitiveTypes)
			{
				throw new ODataException(Strings.ValidationUtils_NullCollectionItemForNonNullableType(expectedItemType.ODataFullName()));
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0004629D File Offset: 0x0004449D
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty)
		{
			if (edmProperty != null && !edmProperty.Type.IsStream())
			{
				throw new ODataException(Strings.ValidationUtils_MismatchPropertyKindForStreamProperty(streamProperty.Name));
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000462C0 File Offset: 0x000444C0
		internal static void ValidateAssociationLinkNotNull(ODataAssociationLink associationLink)
		{
			if (associationLink == null)
			{
				throw new ODataException(Strings.ValidationUtils_EnumerableContainsANullItem("ODataEntry.AssociationLinks"));
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000462D5 File Offset: 0x000444D5
		internal static void ValidateAssociationLinkName(string associationLinkName)
		{
			if (string.IsNullOrEmpty(associationLinkName))
			{
				throw new ODataException(Strings.ValidationUtils_AssociationLinkMustSpecifyName);
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000462EA File Offset: 0x000444EA
		internal static void ValidateAssociationLink(ODataAssociationLink associationLink)
		{
			ValidationUtils.ValidateAssociationLinkName(associationLink.Name);
			if (associationLink.Url == null)
			{
				throw new ODataException(Strings.ValidationUtils_AssociationLinkMustSpecifyUrl);
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00046310 File Offset: 0x00044510
		internal static void IncreaseAndValidateRecursionDepth(ref int recursionDepth, int maxDepth)
		{
			recursionDepth++;
			if (recursionDepth > maxDepth)
			{
				throw new ODataException(Strings.ValidationUtils_RecursionDepthLimitReached(maxDepth));
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00046330 File Offset: 0x00044530
		internal static void ValidateOperationNotNull(ODataOperation operation, bool isAction)
		{
			if (operation == null)
			{
				string text = (isAction ? "ODataEntry.Actions" : "ODataEntry.Functions");
				throw new ODataException(Strings.ValidationUtils_EnumerableContainsANullItem(text));
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004635C File Offset: 0x0004455C
		internal static void ValidateOperationMetadataNotNull(ODataOperation operation)
		{
			if (operation.Metadata == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(operation.GetType().Name));
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00046382 File Offset: 0x00044582
		internal static void ValidateOperationTargetNotNull(ODataOperation operation)
		{
			if (operation.Target == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(operation.GetType().Name));
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000463A8 File Offset: 0x000445A8
		internal static void ValidateEntryMetadataResource(ODataEntry entry, IEdmEntityType entityType, IEdmModel model, bool validateMediaResource)
		{
			if (entityType != null && validateMediaResource)
			{
				bool flag = model.HasDefaultStream(entityType);
				if (entry.MediaResource == null)
				{
					if (flag)
					{
						throw new ODataException(Strings.ValidationUtils_EntryWithoutMediaResourceAndMLEType(entityType.ODataFullName()));
					}
				}
				else if (!flag)
				{
					throw new ODataException(Strings.ValidationUtils_EntryWithMediaResourceAndNonMLEType(entityType.ODataFullName()));
				}
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000463F4 File Offset: 0x000445F4
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmTypeReference expectedTypeReference)
		{
			Type type = value.GetType();
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(type);
			ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00046417 File Offset: 0x00044617
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference)
		{
			if (valuePrimitiveTypeReference == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
			}
			if (!expectedTypeReference.IsODataPrimitiveTypeKind())
			{
				throw new ODataException(Strings.ValidationUtils_NonPrimitiveTypeForPrimitiveValue(expectedTypeReference.ODataFullName()));
			}
			ValidationUtils.ValidateMetadataPrimitiveType(expectedTypeReference, valuePrimitiveTypeReference);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00046454 File Offset: 0x00044654
		internal static void ValidateMetadataPrimitiveType(IEdmTypeReference expectedTypeReference, IEdmTypeReference typeReferenceFromValue)
		{
			IEdmPrimitiveType edmPrimitiveType = (IEdmPrimitiveType)expectedTypeReference.Definition;
			IEdmPrimitiveType edmPrimitiveType2 = (IEdmPrimitiveType)typeReferenceFromValue.Definition;
			bool flag = expectedTypeReference.IsNullable == typeReferenceFromValue.IsNullable || (expectedTypeReference.IsNullable && !typeReferenceFromValue.IsNullable) || !typeReferenceFromValue.IsODataValueType();
			bool flag2 = edmPrimitiveType.IsAssignableFrom(edmPrimitiveType2);
			if (!flag || !flag2)
			{
				throw new ODataException(Strings.ValidationUtils_IncompatiblePrimitiveItemType(typeReferenceFromValue.ODataFullName(), typeReferenceFromValue.IsNullable, expectedTypeReference.ODataFullName(), expectedTypeReference.IsNullable));
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000464DF File Offset: 0x000446DF
		internal static void ValidateResourceCollectionInfo(ODataResourceCollectionInfo collectionInfo)
		{
			if (collectionInfo == null)
			{
				throw new ODataException(Strings.ValidationUtils_WorkspaceCollectionsMustNotContainNullItem);
			}
			if (collectionInfo.Url == null)
			{
				throw new ODataException(Strings.ValidationUtils_ResourceCollectionMustSpecifyUrl);
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00046508 File Offset: 0x00044708
		internal static void ValidateResourceCollectionInfoUrl(string collectionInfoUrl)
		{
			if (collectionInfoUrl == null)
			{
				throw new ODataException(Strings.ValidationUtils_ResourceCollectionUrlMustNotBeNull);
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00046518 File Offset: 0x00044718
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
			throw new ODataException(Strings.ValidationUtils_IncorrectTypeKind(typeName, expectedTypeKind.ToString(), actualTypeKind.ToString()));
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004656F File Offset: 0x0004476F
		internal static void ValidateBoundaryString(string boundary)
		{
			if (boundary == null || boundary.Length == 0 || boundary.Length > 70)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidBatchBoundaryDelimiterLength(boundary, 70));
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004659C File Offset: 0x0004479C
		internal static bool ShouldValidateComplexPropertyNullValue(IEdmModel model)
		{
			Version edmVersion = model.GetEdmVersion();
			Version dataServiceVersion = model.GetDataServiceVersion();
			return !(edmVersion != null) || !(dataServiceVersion != null) || !(edmVersion < ODataVersion.V3.ToDataServiceVersion());
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000465DA File Offset: 0x000447DA
		internal static bool IsValidPropertyName(string propertyName)
		{
			return propertyName.IndexOfAny(ValidationUtils.InvalidCharactersInPropertyNames) < 0;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004661C File Offset: 0x0004481C
		internal static void ValidatePropertyName(string propertyName)
		{
			if (!ValidationUtils.IsValidPropertyName(propertyName))
			{
				string text = string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<char, string>(ValidationUtils.InvalidCharactersInPropertyNames, (char c) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { c }))));
				throw new ODataException(Strings.ValidationUtils_PropertiesMustNotContainReservedChars(propertyName, text));
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00046678 File Offset: 0x00044878
		internal static int ValidateTotalEntityPropertyMappingCount(ODataEntityPropertyMappingCache baseCache, ODataEntityPropertyMappingCollection mappings, int maxMappingCount)
		{
			int num = ((baseCache == null) ? 0 : baseCache.TotalMappingCount);
			int num2 = ((mappings == null) ? 0 : mappings.Count);
			int num3 = num + num2;
			if (num3 > maxMappingCount)
			{
				throw new ODataException(Strings.ValidationUtils_MaxNumberOfEntityPropertyMappingsExceeded(num3, maxMappingCount));
			}
			return num3;
		}

		// Token: 0x040006FA RID: 1786
		private const int MaxBoundaryLength = 70;

		// Token: 0x040006FB RID: 1787
		internal static readonly char[] InvalidCharactersInPropertyNames = new char[] { ':', '.', '@' };
	}
}
