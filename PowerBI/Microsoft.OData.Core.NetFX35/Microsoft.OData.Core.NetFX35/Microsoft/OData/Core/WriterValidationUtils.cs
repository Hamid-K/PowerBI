using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020002A1 RID: 673
	internal static class WriterValidationUtils
	{
		// Token: 0x06001720 RID: 5920 RVA: 0x0004F440 File Offset: 0x0004D640
		internal static void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse)
		{
			if (messageWriterSettings.PayloadBaseUri != null && !messageWriterSettings.PayloadBaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(UriUtils.UriToString(messageWriterSettings.PayloadBaseUri)));
			}
			if (messageWriterSettings.HasJsonPaddingFunction() && !writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage);
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0004F494 File Offset: 0x0004D694
		internal static void ValidatePropertyNotNull(ODataProperty property)
		{
			if (property == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertyMustNotBeNull);
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0004F4A4 File Offset: 0x0004D6A4
		internal static void ValidatePropertyName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertiesMustHaveNonEmptyName);
			}
			ValidationUtils.ValidatePropertyName(propertyName);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0004F4C0 File Offset: 0x0004D6C0
		internal static IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, bool throwOnMissingProperty = true)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = owningStructuredType.FindProperty(propertyName);
			if (throwOnMissingProperty && !owningStructuredType.IsOpen && edmProperty == null)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.FullTypeName()));
			}
			return edmProperty;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0004F4FC File Offset: 0x0004D6FC
		internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType)
		{
			if (owningEntityType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(propertyName, owningEntityType, true);
			if (edmProperty == null)
			{
				throw new ODataException(Strings.ValidationUtils_OpenNavigationProperty(propertyName, owningEntityType.FullTypeName()));
			}
			if (edmProperty.PropertyKind != EdmPropertyKind.Navigation)
			{
				throw new ODataException(Strings.ValidationUtils_NavigationPropertyExpected(propertyName, owningEntityType.FullTypeName(), edmProperty.PropertyKind.ToString()));
			}
			return (IEdmNavigationProperty)edmProperty;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0004F55D File Offset: 0x0004D75D
		internal static void ValidateEntryInExpandedLink(IEdmEntityType entryEntityType, IEdmEntityType parentNavigationPropertyType)
		{
			if (parentNavigationPropertyType == null)
			{
				return;
			}
			if (!parentNavigationPropertyType.IsAssignableFrom(entryEntityType))
			{
				throw new ODataException(Strings.WriterValidationUtils_EntryTypeInExpandedLinkNotCompatibleWithNavigationPropertyType(entryEntityType.FullTypeName(), parentNavigationPropertyType.FullTypeName()));
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0004F583 File Offset: 0x0004D783
		internal static void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
		{
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_OperationInRequest(operation.Metadata));
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0004F599 File Offset: 0x0004D799
		internal static void ValidateFeedAtEnd(ODataFeed feed, bool writingRequest)
		{
			if (feed.NextPageLink != null && writingRequest)
			{
				throw new ODataException(Strings.WriterValidationUtils_NextPageLinkInRequest);
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0004F5B7 File Offset: 0x0004D7B7
		internal static void ValidateEntryAtStart(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryId(entry.Id);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0004F5C4 File Offset: 0x0004D7C4
		internal static void ValidateEntryAtEnd(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryId(entry.Id);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0004F5D4 File Offset: 0x0004D7D4
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

		// Token: 0x0600172B RID: 5931 RVA: 0x0004F694 File Offset: 0x0004D894
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty, bool writingResponse)
		{
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, edmProperty);
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamPropertyInRequest(streamProperty.Name));
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0004F6B1 File Offset: 0x0004D8B1
		internal static void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull);
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0004F6C1 File Offset: 0x0004D8C1
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull);
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0004F6DC File Offset: 0x0004D8DC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Keeping the validation code for navigation link multiplicity in one place.")]
		internal static IEdmNavigationProperty ValidateNavigationLink(ODataNavigationLink navigationLink, IEdmEntityType declaringEntityType, ODataPayloadKind? expandedPayloadKind)
		{
			if (string.IsNullOrEmpty(navigationLink.Name))
			{
				throw new ODataException(Strings.ValidationUtils_LinkMustSpecifyName);
			}
			bool flag = expandedPayloadKind == ODataPayloadKind.EntityReferenceLink;
			bool flag2 = expandedPayloadKind == ODataPayloadKind.Feed;
			Func<object, string> func = null;
			if (!flag && navigationLink.IsCollection != null && expandedPayloadKind != null && flag2 != navigationLink.IsCollection.Value)
			{
				func = ((expandedPayloadKind.Value == ODataPayloadKind.Feed) ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedContent) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryContent));
			}
			IEdmNavigationProperty edmNavigationProperty = null;
			if (func == null && declaringEntityType != null)
			{
				edmNavigationProperty = WriterValidationUtils.ValidateNavigationPropertyDefined(navigationLink.Name, declaringEntityType);
				bool flag3 = edmNavigationProperty.Type.TypeKind() == EdmTypeKind.Collection;
				if (navigationLink.IsCollection != null && flag3 != navigationLink.IsCollection && (!(navigationLink.IsCollection == false) || !flag))
				{
					func = (flag3 ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedMetadata) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryMetadata));
				}
				if (!flag && expandedPayloadKind != null && flag3 != flag2)
				{
					func = (flag3 ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkWithEntryPayloadAndFeedMetadata) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkWithFeedPayloadAndEntryMetadata));
				}
			}
			if (func != null)
			{
				string text = ((navigationLink.Url == null) ? "null" : UriUtils.UriToString(navigationLink.Url));
				throw new ODataException(func.Invoke(text));
			}
			return edmNavigationProperty;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0004F88A File Offset: 0x0004DA8A
		internal static void ValidateNavigationLinkUrlPresent(ODataNavigationLink navigationLink)
		{
			if (navigationLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NavigationLinkMustSpecifyUrl(navigationLink.Name));
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0004F8AC File Offset: 0x0004DAAC
		internal static void ValidateNavigationLinkHasCardinality(ODataNavigationLink navigationLink)
		{
			if (navigationLink.IsCollection == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NavigationLinkMustSpecifyIsCollection(navigationLink.Name));
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0004F8DC File Offset: 0x0004DADC
		internal static void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, ODataWriterBehavior writerBehavior, IEdmModel model)
		{
			if (expectedPropertyTypeReference != null)
			{
				if (expectedPropertyTypeReference.IsNonEntityCollectionType())
				{
					throw new ODataException(Strings.WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue(propertyName));
				}
				if (expectedPropertyTypeReference.IsODataPrimitiveTypeKind())
				{
					if (!expectedPropertyTypeReference.IsNullable && !writerBehavior.AllowNullValuesForNonNullablePrimitiveTypes)
					{
						throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.FullName()));
					}
				}
				else
				{
					if (expectedPropertyTypeReference.IsODataEnumTypeKind() && !expectedPropertyTypeReference.IsNullable)
					{
						throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.FullName()));
					}
					if (expectedPropertyTypeReference.IsStream())
					{
						throw new ODataException(Strings.WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(propertyName));
					}
					if (expectedPropertyTypeReference.IsODataComplexTypeKind() && ValidationUtils.ShouldValidateComplexPropertyNullValue(model))
					{
						IEdmComplexTypeReference edmComplexTypeReference = expectedPropertyTypeReference.AsComplex();
						if (!edmComplexTypeReference.IsNullable)
						{
							throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.FullName()));
						}
					}
				}
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0004F997 File Offset: 0x0004DB97
		private static void ValidateEntryId(Uri id)
		{
			if (id != null && UriUtils.UriToString(id).Length == 0)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntriesMustHaveNonEmptyId);
			}
		}
	}
}
