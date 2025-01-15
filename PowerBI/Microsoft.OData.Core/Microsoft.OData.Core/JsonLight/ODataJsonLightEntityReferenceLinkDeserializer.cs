using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000241 RID: 577
	internal sealed class ODataJsonLightEntityReferenceLinkDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06001914 RID: 6420 RVA: 0x00048443 File Offset: 0x00046643
		internal ODataJsonLightEntityReferenceLinkDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0004844C File Offset: 0x0004664C
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLinks, propertyAndAnnotationCollector, false, false);
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadEntityReferenceLinksImplementation(propertyAndAnnotationCollector);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0004847C File Offset: 0x0004667C
		internal Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			return base.ReadPayloadStartAsync(ODataPayloadKind.EntityReferenceLinks, propertyAndAnnotationCollector, false, false).FollowOnSuccessWith(delegate(Task t)
			{
				ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadEntityReferenceLinksImplementation(propertyAndAnnotationCollector);
				this.ReadPayloadEnd(false);
				return odataEntityReferenceLinks;
			});
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000484C4 File Offset: 0x000466C4
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLink, propertyAndAnnotationCollector, false, false);
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceLinkImplementation(propertyAndAnnotationCollector);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLink;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000484F4 File Offset: 0x000466F4
		internal Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			return base.ReadPayloadStartAsync(ODataPayloadKind.EntityReferenceLink, propertyAndAnnotationCollector, false, false).FollowOnSuccessWith(delegate(Task t)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceLinkImplementation(propertyAndAnnotationCollector);
				this.ReadPayloadEnd(false);
				return odataEntityReferenceLink;
			});
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0004853C File Offset: 0x0004673C
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

		// Token: 0x0600191A RID: 6426 RVA: 0x000485C7 File Offset: 0x000467C7
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			return this.ReadSingleEntityReferenceLink(propertyAndAnnotationCollector, true);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000485D4 File Offset: 0x000467D4
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
					if (this.JsonReader.NodeType == JsonNodeType.Property)
					{
						this.JsonReader.Read();
					}
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

		// Token: 0x0600191C RID: 6428 RVA: 0x0004867C File Offset: 0x0004687C
		private void ReadEntityReferenceLinksNextLinkAnnotationValue(ODataEntityReferenceLinks links)
		{
			Uri uri = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
			links.NextPageLink = uri;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0004869C File Offset: 0x0004689C
		private void ReadEntityReferenceCountAnnotationValue(ODataEntityReferenceLinks links)
		{
			links.Count = new long?(base.ReadAndValidateAnnotationAsLongForIeee754Compatible("odata.count"));
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000486B4 File Offset: 0x000468B4
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
					if (this.JsonReader.NodeType == JsonNodeType.Property)
					{
						this.JsonReader.Read();
					}
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
