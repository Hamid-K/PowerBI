using System;
using System.Collections.Generic;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000208 RID: 520
	internal sealed class ODataJsonLightEntityReferenceLinkDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x0003C017 File Offset: 0x0003A217
		internal ODataJsonLightEntityReferenceLinkDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0003C020 File Offset: 0x0003A220
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLinks, propertyAndAnnotationCollector, false, false);
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadEntityReferenceLinksImplementation(propertyAndAnnotationCollector);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLinks;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0003C050 File Offset: 0x0003A250
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLink, propertyAndAnnotationCollector, false, false);
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceLinkImplementation(propertyAndAnnotationCollector);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLink;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0003C080 File Offset: 0x0003A280
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
			this.ReadEntityReferenceLinksAnnotations(odataEntityReferenceLinks, propertyAndAnnotationCollector, true);
			base.JsonReader.ReadStartArray();
			List<ODataEntityReferenceLink> list = new List<ODataEntityReferenceLink>();
			PropertyAndAnnotationCollector propertyAndAnnotationCollector2 = base.JsonLightInputContext.CreatePropertyAndAnnotationCollector();
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = this.ReadSingleEntityReferenceLink(propertyAndAnnotationCollector2, false);
				list.Add(odataEntityReferenceLink);
				propertyAndAnnotationCollector2.Reset();
			}
			base.JsonReader.ReadEndArray();
			this.ReadEntityReferenceLinksAnnotations(odataEntityReferenceLinks, propertyAndAnnotationCollector, false);
			base.JsonReader.ReadEndObject();
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			return odataEntityReferenceLinks;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0003C10B File Offset: 0x0003A30B
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			return this.ReadSingleEntityReferenceLink(propertyAndAnnotationCollector, true);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0003C118 File Offset: 0x0003A318
		private void ReadEntityReferenceLinksAnnotations(ODataEntityReferenceLinks links, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool forLinksStart)
		{
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks);
				};
				bool foundValueProperty = false;
				base.ProcessProperty(propertyAndAnnotationCollector, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParseResult, string propertyName)
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
						object obj = this.ReadCustomInstanceAnnotationValue(propertyAndAnnotationCollector, propertyName);
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

		// Token: 0x060014BB RID: 5307 RVA: 0x0003C1C0 File Offset: 0x0003A3C0
		private void ReadEntityReferenceLinksNextLinkAnnotationValue(ODataEntityReferenceLinks links)
		{
			Uri uri = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
			links.NextPageLink = uri;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0003C1E0 File Offset: 0x0003A3E0
		private void ReadEntityReferenceCountAnnotationValue(ODataEntityReferenceLinks links)
		{
			links.Count = new long?(base.ReadAndValidateAnnotationAsLongForIeee754Compatible("odata.count"));
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0003C1F8 File Offset: 0x0003A3F8
		private ODataEntityReferenceLink ReadSingleEntityReferenceLink(PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool topLevel)
		{
			if (!topLevel)
			{
				if (base.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(base.JsonReader.NodeType));
				}
				base.JsonReader.ReadStartObject();
			}
			ODataEntityReferenceLink[] entityReferenceLink = new ODataEntityReferenceLink[1];
			Func<string, object> func = delegate(string annotationName)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink(annotationName));
			};
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(propertyAndAnnotationCollector, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
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
						if (entityReferenceLink[0] != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink("odata.id"));
						}
						string text = this.JsonReader.ReadStringValue("odata.id");
						if (text == null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull("odata.id"));
						}
						entityReferenceLink[0] = new ODataEntityReferenceLink
						{
							Url = this.ProcessUriFromPayload(text)
						};
						ReaderValidationUtils.ValidateEntityReferenceLink(entityReferenceLink[0]);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						if (entityReferenceLink[0] == null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty("odata.id"));
						}
						ODataAnnotationNames.ValidateIsCustomAnnotationName(propertyName);
						object obj = this.ReadCustomInstanceAnnotationValue(propertyAndAnnotationCollector, propertyName);
						entityReferenceLink[0].InstanceAnnotations.Add(new ODataInstanceAnnotation(propertyName, obj.ToODataValue()));
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntityReferenceLinkDeserializer_ReadSingleEntityReferenceLink));
					}
				});
			}
			if (entityReferenceLink[0] == null)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty("odata.id"));
			}
			base.JsonReader.ReadEndObject();
			return entityReferenceLink[0];
		}
	}
}
