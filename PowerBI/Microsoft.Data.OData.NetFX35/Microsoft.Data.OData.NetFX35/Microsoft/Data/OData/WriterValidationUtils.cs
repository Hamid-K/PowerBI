using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000249 RID: 585
	internal static class WriterValidationUtils
	{
		// Token: 0x060011C7 RID: 4551 RVA: 0x0004361C File Offset: 0x0004181C
		internal static void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse)
		{
			if (messageWriterSettings.BaseUri != null && !messageWriterSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(UriUtilsCommon.UriToString(messageWriterSettings.BaseUri)));
			}
			if (messageWriterSettings.HasJsonPaddingFunction() && !writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00043670 File Offset: 0x00041870
		internal static void ValidatePropertyNotNull(ODataProperty property)
		{
			if (property == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertyMustNotBeNull);
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00043680 File Offset: 0x00041880
		internal static void ValidatePropertyName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertiesMustHaveNonEmptyName);
			}
			ValidationUtils.ValidatePropertyName(propertyName);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004369C File Offset: 0x0004189C
		internal static IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			bool flag = !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
			IEdmProperty edmProperty = owningStructuredType.FindProperty(propertyName);
			if (flag && !owningStructuredType.IsOpen && edmProperty == null)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
			}
			return edmProperty;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000436F0 File Offset: 0x000418F0
		internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (owningEntityType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(propertyName, owningEntityType, undeclaredPropertyBehaviorKinds);
			if (edmProperty == null)
			{
				bool flag = !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
				throw new ODataException(Strings.ValidationUtils_OpenNavigationProperty(propertyName, owningEntityType.ODataFullName()));
			}
			if (edmProperty.PropertyKind != EdmPropertyKind.Navigation)
			{
				throw new ODataException(Strings.ValidationUtils_NavigationPropertyExpected(propertyName, owningEntityType.ODataFullName(), edmProperty.PropertyKind.ToString()));
			}
			return (IEdmNavigationProperty)edmProperty;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004376A File Offset: 0x0004196A
		internal static void ValidateEntryInExpandedLink(IEdmEntityType entryEntityType, IEdmEntityType parentNavigationPropertyType)
		{
			if (parentNavigationPropertyType == null)
			{
				return;
			}
			if (!parentNavigationPropertyType.IsAssignableFrom(entryEntityType))
			{
				throw new ODataException(Strings.WriterValidationUtils_EntryTypeInExpandedLinkNotCompatibleWithNavigationPropertyType(entryEntityType.ODataFullName(), parentNavigationPropertyType.ODataFullName()));
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00043790 File Offset: 0x00041990
		internal static void ValidateAssociationLink(ODataAssociationLink associationLink, ODataVersion version, bool writingResponse)
		{
			ODataVersionChecker.CheckAssociationLinks(version);
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_AssociationLinkInRequest(associationLink.Name));
			}
			ValidationUtils.ValidateAssociationLink(associationLink);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000437B2 File Offset: 0x000419B2
		internal static void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
		{
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_OperationInRequest(operation.Metadata));
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000437C8 File Offset: 0x000419C8
		internal static void ValidateFeedAtEnd(ODataFeed feed, bool writingRequest, ODataVersion version)
		{
			if (feed.NextPageLink != null)
			{
				if (writingRequest)
				{
					throw new ODataException(Strings.WriterValidationUtils_NextPageLinkInRequest);
				}
				ODataVersionChecker.CheckNextLink(version);
			}
			if (feed.DeltaLink != null)
			{
				ODataVersionChecker.CheckDeltaLink(version);
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00043800 File Offset: 0x00041A00
		internal static void ValidateEntryAtStart(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryId(entry.Id);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004380D File Offset: 0x00041A0D
		internal static void ValidateEntryAtEnd(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryId(entry.Id);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004381C File Offset: 0x00041A1C
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

		// Token: 0x060011D3 RID: 4563 RVA: 0x000438DC File Offset: 0x00041ADC
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty, ODataVersion version, bool writingResponse)
		{
			ODataVersionChecker.CheckStreamReferenceProperty(version);
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, edmProperty);
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamPropertyInRequest(streamProperty.Name));
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000438FF File Offset: 0x00041AFF
		internal static void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull);
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004390F File Offset: 0x00041B0F
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull);
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004392C File Offset: 0x00041B2C
		internal static IEdmNavigationProperty ValidateNavigationLink(ODataNavigationLink navigationLink, IEdmEntityType declaringEntityType, ODataPayloadKind? expandedPayloadKind, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
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
				edmNavigationProperty = WriterValidationUtils.ValidateNavigationPropertyDefined(navigationLink.Name, declaringEntityType, undeclaredPropertyBehaviorKinds);
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
				string text = ((navigationLink.Url == null) ? "null" : UriUtilsCommon.UriToString(navigationLink.Url));
				throw new ODataException(func.Invoke(text));
			}
			return edmNavigationProperty;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00043ADB File Offset: 0x00041CDB
		internal static void ValidateNavigationLinkUrlPresent(ODataNavigationLink navigationLink)
		{
			if (navigationLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NavigationLinkMustSpecifyUrl(navigationLink.Name));
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00043AFC File Offset: 0x00041CFC
		internal static void ValidateNavigationLinkHasCardinality(ODataNavigationLink navigationLink)
		{
			if (navigationLink.IsCollection == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NavigationLinkMustSpecifyIsCollection(navigationLink.Name));
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00043B2C File Offset: 0x00041D2C
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
						throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.ODataFullName()));
					}
				}
				else
				{
					if (expectedPropertyTypeReference.IsStream())
					{
						throw new ODataException(Strings.WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(propertyName));
					}
					if (expectedPropertyTypeReference.IsODataComplexTypeKind() && ValidationUtils.ShouldValidateComplexPropertyNullValue(model))
					{
						IEdmComplexTypeReference edmComplexTypeReference = expectedPropertyTypeReference.AsComplex();
						if (!edmComplexTypeReference.IsNullable)
						{
							throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.ODataFullName()));
						}
					}
				}
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00043BC2 File Offset: 0x00041DC2
		private static void ValidateEntryId(string id)
		{
			if (id != null && id.Length == 0)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntriesMustHaveNonEmptyId);
			}
		}
	}
}
