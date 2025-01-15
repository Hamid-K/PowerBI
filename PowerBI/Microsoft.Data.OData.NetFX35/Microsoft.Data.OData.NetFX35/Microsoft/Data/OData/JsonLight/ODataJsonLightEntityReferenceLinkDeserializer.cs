using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018B RID: 395
	internal sealed class ODataJsonLightEntityReferenceLinkDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00024518 File Offset: 0x00022718
		internal ODataJsonLightEntityReferenceLinkDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00024524 File Offset: 0x00022724
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLinks, duplicatePropertyNamesChecker, false, false);
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadEntityReferenceLinksImplementation(navigationProperty, duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00024554 File Offset: 0x00022754
		internal ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLink, duplicatePropertyNamesChecker, false, false);
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceLinkImplementation(navigationProperty, duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLink;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00024584 File Offset: 0x00022784
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation(IEdmNavigationProperty navigationProperty, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
			if (base.JsonLightInputContext.ReadingResponse)
			{
				ReaderValidationUtils.ValidateEntityReferenceLinkMetadataUri(base.MetadataUriParseResult, navigationProperty);
			}
			this.ReadEntityReferenceLinksAnnotations(odataEntityReferenceLinks, duplicatePropertyNamesChecker, true);
			base.JsonReader.ReadStartArray();
			List<ODataEntityReferenceLink> list = new List<ODataEntityReferenceLink>();
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker2 = base.JsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = this.ReadSingleEntityReferenceLink(duplicatePropertyNamesChecker2, false);
				list.Add(odataEntityReferenceLink);
				duplicatePropertyNamesChecker2.Clear();
			}
			base.JsonReader.ReadEndArray();
			this.ReadEntityReferenceLinksAnnotations(odataEntityReferenceLinks, duplicatePropertyNamesChecker, false);
			base.JsonReader.ReadEndObject();
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00024628 File Offset: 0x00022828
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation(IEdmNavigationProperty navigationProperty, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (base.JsonLightInputContext.ReadingResponse)
			{
				ReaderValidationUtils.ValidateEntityReferenceLinkMetadataUri(base.MetadataUriParseResult, navigationProperty);
			}
			return this.ReadSingleEntityReferenceLink(duplicatePropertyNamesChecker, true);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00024760 File Offset: 0x00022960
		private void ReadEntityReferenceLinksAnnotations(ODataEntityReferenceLinks links, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool forLinksStart)
		{
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks);
				};
				bool foundValueProperty = false;
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParseResult, string propertyName)
				{
					switch (propertyParseResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						if (string.CompareOrdinal("value", propertyName) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound(propertyName, "value"));
						}
						foundValueProperty = true;
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						if (string.CompareOrdinal("odata.nextLink", propertyName) == 0)
						{
							this.ReadEntityReferenceLinksNextLinkAnnotationValue(links);
							return;
						}
						if (string.CompareOrdinal("odata.count", propertyName) == 0)
						{
							this.ReadEntityReferenceCountAnnotationValue(links);
							return;
						}
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntityReferenceLinkDeserializer_ReadEntityReferenceLinksAnnotations));
					}
				});
				if (foundValueProperty)
				{
					return;
				}
			}
			if (forLinksStart)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound("value"));
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000247F4 File Offset: 0x000229F4
		private void ReadEntityReferenceLinksNextLinkAnnotationValue(ODataEntityReferenceLinks links)
		{
			Uri uri = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
			links.NextPageLink = uri;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00024814 File Offset: 0x00022A14
		private void ReadEntityReferenceCountAnnotationValue(ODataEntityReferenceLinks links)
		{
			links.Count = new long?(base.ReadAndValidateAnnotationStringValueAsLong("odata.count"));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00024950 File Offset: 0x00022B50
		private ODataEntityReferenceLink ReadSingleEntityReferenceLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool topLevel)
		{
			ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClassc CS$<>8__locals1 = new ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClassc();
			CS$<>8__locals1.<>4__this = this;
			if (!topLevel)
			{
				if (base.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(base.JsonReader.NodeType));
				}
				base.JsonReader.ReadStartObject();
			}
			ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClassc CS$<>8__locals2 = CS$<>8__locals1;
			ODataEntityReferenceLink[] array = new ODataEntityReferenceLink[1];
			CS$<>8__locals2.entityReferenceLink = array;
			Func<string, object> func = delegate(string annotationName)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink(annotationName));
			};
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						if (string.CompareOrdinal("url", propertyName) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink(propertyName, "url"));
						}
						if (CS$<>8__locals1.entityReferenceLink[0] != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink("url"));
						}
						string text = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue("url");
						if (text == null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull("url"));
						}
						CS$<>8__locals1.entityReferenceLink[0] = new ODataEntityReferenceLink
						{
							Url = CS$<>8__locals1.<>4__this.ProcessUriFromPayload(text)
						};
						ReaderValidationUtils.ValidateEntityReferenceLink(CS$<>8__locals1.entityReferenceLink[0]);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						CS$<>8__locals1.<>4__this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntityReferenceLinkDeserializer_ReadSingleEntityReferenceLink));
					}
				});
			}
			if (CS$<>8__locals1.entityReferenceLink[0] == null)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty("url"));
			}
			base.JsonReader.ReadEndObject();
			return CS$<>8__locals1.entityReferenceLink[0];
		}
	}
}
