using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000E2 RID: 226
	internal static class WriterValidationUtils
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		internal static void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse)
		{
			if (messageWriterSettings.BaseUri != null && !messageWriterSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(UriUtils.UriToString(messageWriterSettings.BaseUri)));
			}
			if (messageWriterSettings.HasJsonPaddingFunction() && !writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001C450 File Offset: 0x0001A650
		internal static void ValidatePropertyNotNull(ODataPropertyInfo property)
		{
			if (property == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertyMustNotBeNull);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001C460 File Offset: 0x0001A660
		internal static void ValidatePropertyName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertiesMustHaveNonEmptyName);
			}
			ValidationUtils.ValidatePropertyName(propertyName);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001C47C File Offset: 0x0001A67C
		internal static IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, bool throwOnUndeclaredProperty)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = owningStructuredType.FindProperty(propertyName);
			if (throwOnUndeclaredProperty && !owningStructuredType.IsOpen && edmProperty == null)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.FullTypeName()));
			}
			return edmProperty;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
		internal static void ValidatePropertyDefined(PropertySerializationInfo propertyInfo, bool throwOnUndeclaredProperty)
		{
			if (propertyInfo.MetadataType.OwningType == null)
			{
				return;
			}
			if (throwOnUndeclaredProperty && propertyInfo.MetadataType.IsUndeclaredProperty && !propertyInfo.MetadataType.IsOpenProperty)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyInfo.PropertyName, propertyInfo.MetadataType.OwningType.FullTypeName()));
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001C514 File Offset: 0x0001A714
		internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmStructuredType owningType, bool throwOnUndeclaredProperty)
		{
			if (owningType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(propertyName, owningType, throwOnUndeclaredProperty);
			if (edmProperty == null)
			{
				return null;
			}
			if (edmProperty.PropertyKind != EdmPropertyKind.Navigation)
			{
				throw new ODataException(Strings.ValidationUtils_NavigationPropertyExpected(propertyName, owningType.FullTypeName(), edmProperty.PropertyKind.ToString()));
			}
			return (IEdmNavigationProperty)edmProperty;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001C569 File Offset: 0x0001A769
		internal static void ValidateNestedResource(IEdmStructuredType resourceType, IEdmStructuredType parentNavigationPropertyType)
		{
			if (parentNavigationPropertyType == null)
			{
				return;
			}
			if (!parentNavigationPropertyType.IsAssignableFrom(resourceType))
			{
				throw new ODataException(Strings.WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType(resourceType.FullTypeName(), parentNavigationPropertyType.FullTypeName()));
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001C58F File Offset: 0x0001A78F
		internal static void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
		{
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_OperationInRequest(operation.Metadata));
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001C5A5 File Offset: 0x0001A7A5
		internal static void ValidateResourceSetAtEnd(ODataResourceSet resourceSet, bool writingRequest)
		{
			if (resourceSet.NextPageLink != null && writingRequest)
			{
				throw new ODataException(Strings.WriterValidationUtils_NextPageLinkInRequest);
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001C5A5 File Offset: 0x0001A7A5
		internal static void ValidateDeltaResourceSetAtEnd(ODataDeltaResourceSet resourceSet, bool writingRequest)
		{
			if (resourceSet.NextPageLink != null && writingRequest)
			{
				throw new ODataException(Strings.WriterValidationUtils_NextPageLinkInRequest);
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001C5C3 File Offset: 0x0001A7C3
		internal static void ValidateResourceAtStart(ODataResourceBase resource)
		{
			WriterValidationUtils.ValidateResourceId(resource.Id);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001C5C3 File Offset: 0x0001A7C3
		internal static void ValidateResourceAtEnd(ODataResourceBase resource)
		{
			WriterValidationUtils.ValidateResourceId(resource.Id);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		internal static void ValidateStreamReferenceValue(ODataStreamReferenceValue streamReference, bool isDefaultStream)
		{
			if (streamReference.ContentType != null && streamReference.ContentType.Length == 0)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamReferenceValueEmptyContentType);
			}
			if (isDefaultStream && streamReference.ReadLink == null && streamReference.ContentType != null)
			{
				throw new ODataException(Strings.WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink);
			}
			if (isDefaultStream && streamReference.ReadLink != null && streamReference.ContentType == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType);
			}
			if (streamReference.EditLink == null && streamReference.ReadLink == null && !isDefaultStream)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink);
			}
			if (streamReference.EditLink == null && streamReference.ETag != null)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001C690 File Offset: 0x0001A890
		internal static void ValidateStreamPropertyInfo(IODataStreamReferenceInfo streamPropertyInfo, IEdmProperty edmProperty, string propertyName, bool writingResponse)
		{
			ValidationUtils.ValidateStreamPropertyInfo(streamPropertyInfo, edmProperty, propertyName);
			if (!writingResponse && ((streamPropertyInfo != null && streamPropertyInfo.EditLink != null) || streamPropertyInfo.ReadLink != null || streamPropertyInfo.ETag != null))
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamPropertyInRequest(propertyName));
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001C6D0 File Offset: 0x0001A8D0
		internal static void ValidatePropertyDerivedTypeConstraint(PropertySerializationInfo propertySerializationInfo)
		{
			if (propertySerializationInfo.MetadataType.IsUndeclaredProperty)
			{
				return;
			}
			PropertyValueTypeInfo valueType = propertySerializationInfo.ValueType;
			if (valueType == null || valueType.TypeReference == null)
			{
				return;
			}
			if (propertySerializationInfo.MetadataType.TypeReference.Definition == valueType.TypeReference.Definition)
			{
				return;
			}
			string fullTypeName = valueType.TypeReference.FullName();
			if (propertySerializationInfo.MetadataType.DerivedTypeConstraints == null || propertySerializationInfo.MetadataType.DerivedTypeConstraints.Any((string d) => d == fullTypeName))
			{
				return;
			}
			throw new ODataException(Strings.WriterValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint(fullTypeName, "property", propertySerializationInfo.PropertyName));
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001C77A File Offset: 0x0001A97A
		internal static void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001C78A File Offset: 0x0001A98A
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull);
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001C7A8 File Offset: 0x0001A9A8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Keeping the validation code for nested resource info multiplicity in one place.")]
		internal static IEdmNavigationProperty ValidateNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmStructuredType declaringStructuredType, ODataPayloadKind? expandedPayloadKind, bool throwOnUndeclaredProperty)
		{
			if (string.IsNullOrEmpty(nestedResourceInfo.Name))
			{
				throw new ODataException(Strings.ValidationUtils_LinkMustSpecifyName);
			}
			bool flag = expandedPayloadKind == ODataPayloadKind.EntityReferenceLink;
			bool flag2 = expandedPayloadKind == ODataPayloadKind.ResourceSet;
			Func<object, string> func = null;
			if (!flag && nestedResourceInfo.IsCollection != null && expandedPayloadKind != null && flag2 != nestedResourceInfo.IsCollection.Value)
			{
				func = ((expandedPayloadKind.Value == ODataPayloadKind.ResourceSet) ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent));
			}
			IEdmNavigationProperty edmNavigationProperty = null;
			if (func == null && declaringStructuredType != null)
			{
				edmNavigationProperty = WriterValidationUtils.ValidateNavigationPropertyDefined(nestedResourceInfo.Name, declaringStructuredType, throwOnUndeclaredProperty);
				if (edmNavigationProperty != null)
				{
					bool flag3 = edmNavigationProperty.Type.TypeKind() == EdmTypeKind.Collection;
					if (nestedResourceInfo.IsCollection != null && flag3 != nestedResourceInfo.IsCollection && (!(nestedResourceInfo.IsCollection == false) || !flag))
					{
						func = (flag3 ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata));
					}
					if (!flag && expandedPayloadKind != null && flag3 != flag2)
					{
						func = (flag3 ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata));
					}
				}
			}
			if (func != null)
			{
				string text = ((nestedResourceInfo.Url == null) ? "null" : UriUtils.UriToString(nestedResourceInfo.Url));
				throw new ODataException(func(text));
			}
			return edmNavigationProperty;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001C968 File Offset: 0x0001AB68
		internal static void ValidateDerivedTypeConstraint(IEdmStructuredType resourceType, IEdmStructuredType metadataType, IEnumerable<string> derivedTypeConstraints, string itemKind, string itemName)
		{
			if (resourceType == null || metadataType == null || derivedTypeConstraints == null || resourceType == metadataType)
			{
				return;
			}
			string fullTypeName = resourceType.FullTypeName();
			if (derivedTypeConstraints.Any((string c) => c == fullTypeName))
			{
				return;
			}
			throw new ODataException(Strings.WriterValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint(fullTypeName, itemKind, itemName));
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		internal static void ValidateNestedResourceInfoHasCardinality(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (nestedResourceInfo.IsCollection == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection(nestedResourceInfo.Name));
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		internal static void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, IEdmModel model)
		{
			if (expectedPropertyTypeReference != null)
			{
				if (expectedPropertyTypeReference.IsNonEntityCollectionType())
				{
					throw new ODataException(Strings.WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue(propertyName));
				}
				if (expectedPropertyTypeReference.IsODataPrimitiveTypeKind() && !expectedPropertyTypeReference.IsNullable)
				{
					throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.FullName()));
				}
				if (expectedPropertyTypeReference.IsODataEnumTypeKind() && !expectedPropertyTypeReference.IsNullable)
				{
					throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.FullName()));
				}
				if (expectedPropertyTypeReference.IsStream())
				{
					throw new ODataException(Strings.WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(propertyName));
				}
				if (expectedPropertyTypeReference.IsODataComplexTypeKind())
				{
					IEdmComplexTypeReference edmComplexTypeReference = expectedPropertyTypeReference.AsComplex();
					if (!edmComplexTypeReference.IsNullable)
					{
						throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.FullName()));
					}
				}
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001CA98 File Offset: 0x0001AC98
		private static void ValidateResourceId(Uri id)
		{
			if (id != null && UriUtils.UriToString(id).Length == 0)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntriesMustHaveNonEmptyId);
			}
		}
	}
}
