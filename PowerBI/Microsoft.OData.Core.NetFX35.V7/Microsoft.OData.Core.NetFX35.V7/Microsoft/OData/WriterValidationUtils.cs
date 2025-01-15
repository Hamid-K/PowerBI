using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000B6 RID: 182
	internal static class WriterValidationUtils
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x000140F0 File Offset: 0x000122F0
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

		// Token: 0x06000707 RID: 1799 RVA: 0x00014144 File Offset: 0x00012344
		internal static void ValidatePropertyNotNull(ODataProperty property)
		{
			if (property == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertyMustNotBeNull);
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00014154 File Offset: 0x00012354
		internal static void ValidatePropertyName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertiesMustHaveNonEmptyName);
			}
			ValidationUtils.ValidatePropertyName(propertyName);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00014170 File Offset: 0x00012370
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

		// Token: 0x0600070A RID: 1802 RVA: 0x000141AC File Offset: 0x000123AC
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

		// Token: 0x0600070B RID: 1803 RVA: 0x00014208 File Offset: 0x00012408
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

		// Token: 0x0600070C RID: 1804 RVA: 0x0001425D File Offset: 0x0001245D
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

		// Token: 0x0600070D RID: 1805 RVA: 0x00014283 File Offset: 0x00012483
		internal static void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
		{
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_OperationInRequest(operation.Metadata));
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00014299 File Offset: 0x00012499
		internal static void ValidateResourceSetAtEnd(ODataResourceSet resourceSet, bool writingRequest)
		{
			if (resourceSet.NextPageLink != null && writingRequest)
			{
				throw new ODataException(Strings.WriterValidationUtils_NextPageLinkInRequest);
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000142B7 File Offset: 0x000124B7
		internal static void ValidateResourceAtStart(ODataResource resource)
		{
			WriterValidationUtils.ValidateResourceId(resource.Id);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000142B7 File Offset: 0x000124B7
		internal static void ValidateResourceAtEnd(ODataResource resource)
		{
			WriterValidationUtils.ValidateResourceId(resource.Id);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000142C4 File Offset: 0x000124C4
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

		// Token: 0x06000712 RID: 1810 RVA: 0x00014384 File Offset: 0x00012584
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty, bool writingResponse)
		{
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, edmProperty);
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamPropertyInRequest(streamProperty.Name));
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000143A1 File Offset: 0x000125A1
		internal static void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull);
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000143B1 File Offset: 0x000125B1
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000143CC File Offset: 0x000125CC
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
				throw new ODataException(func.Invoke(text));
			}
			return edmNavigationProperty;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001458C File Offset: 0x0001278C
		internal static void ValidateNestedResourceInfoHasCardinality(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (nestedResourceInfo.IsCollection == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection(nestedResourceInfo.Name));
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000145BC File Offset: 0x000127BC
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

		// Token: 0x06000718 RID: 1816 RVA: 0x00014664 File Offset: 0x00012864
		private static void ValidateResourceId(Uri id)
		{
			if (id != null && UriUtils.UriToString(id).Length == 0)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntriesMustHaveNonEmptyId);
			}
		}
	}
}
