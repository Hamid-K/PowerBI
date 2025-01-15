using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000DB RID: 219
	internal sealed class ODataJsonLightEntityReferenceLinkDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		internal ODataJsonLightEntityReferenceLinkDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLinks, duplicatePropertyNamesChecker, false, false);
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadEntityReferenceLinksImplementation(duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLink, duplicatePropertyNamesChecker, false, false);
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceLinkImplementation(duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLink;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001BF1C File Offset: 0x0001A11C
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
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

		// Token: 0x0600081B RID: 2075 RVA: 0x0001BFA7 File Offset: 0x0001A1A7
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			return this.ReadSingleEntityReferenceLink(duplicatePropertyNamesChecker, true);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001C0F8 File Offset: 0x0001A2F8
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
					{
						ODataAnnotationNames.ValidateIsCustomAnnotationName(propertyName);
						object obj = this.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, propertyName);
						links.InstanceAnnotations.Add(new ODataInstanceAnnotation(propertyName, obj.ToODataValue()));
						return;
					}
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

		// Token: 0x0600081D RID: 2077 RVA: 0x0001C198 File Offset: 0x0001A398
		private void ReadEntityReferenceLinksNextLinkAnnotationValue(ODataEntityReferenceLinks links)
		{
			Uri uri = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
			links.NextPageLink = uri;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		private void ReadEntityReferenceCountAnnotationValue(ODataEntityReferenceLinks links)
		{
			links.Count = new long?(base.ReadAndValidateAnnotationAsLongForIeee754Compatible("odata.count"));
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001C328 File Offset: 0x0001A528
		private ODataEntityReferenceLink ReadSingleEntityReferenceLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool topLevel)
		{
			ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClassc CS$<>8__locals1 = new ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClassc();
			CS$<>8__locals1.duplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
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
				base.ProcessProperty(CS$<>8__locals1.duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
					{
						if (string.CompareOrdinal("odata.id", propertyName) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink(propertyName, "odata.id"));
						}
						if (CS$<>8__locals1.entityReferenceLink[0] != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink("odata.id"));
						}
						string text = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue("odata.id");
						if (text == null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull("odata.id"));
						}
						CS$<>8__locals1.entityReferenceLink[0] = new ODataEntityReferenceLink
						{
							Url = CS$<>8__locals1.<>4__this.ProcessUriFromPayload(text)
						};
						ReaderValidationUtils.ValidateEntityReferenceLink(CS$<>8__locals1.entityReferenceLink[0]);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						if (CS$<>8__locals1.entityReferenceLink[0] == null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty("odata.id"));
						}
						ODataAnnotationNames.ValidateIsCustomAnnotationName(propertyName);
						object obj = CS$<>8__locals1.<>4__this.ReadCustomInstanceAnnotationValue(CS$<>8__locals1.duplicatePropertyNamesChecker, propertyName);
						CS$<>8__locals1.entityReferenceLink[0].InstanceAnnotations.Add(new ODataInstanceAnnotation(propertyName, obj.ToODataValue()));
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntityReferenceLinkDeserializer_ReadSingleEntityReferenceLink));
					}
				});
			}
			if (CS$<>8__locals1.entityReferenceLink[0] == null)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty("odata.id"));
			}
			base.JsonReader.ReadEndObject();
			return CS$<>8__locals1.entityReferenceLink[0];
		}
	}
}
