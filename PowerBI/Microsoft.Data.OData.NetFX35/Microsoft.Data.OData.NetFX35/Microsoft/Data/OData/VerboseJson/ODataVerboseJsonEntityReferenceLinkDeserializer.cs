using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000234 RID: 564
	internal sealed class ODataVerboseJsonEntityReferenceLinkDeserializer : ODataVerboseJsonDeserializer
	{
		// Token: 0x06001110 RID: 4368 RVA: 0x0003FE7C File Offset: 0x0003E07C
		internal ODataVerboseJsonEntityReferenceLinkDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003FE88 File Offset: 0x0003E088
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			bool flag = base.Version >= ODataVersion.V2 && base.ReadingResponse;
			ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask entityReferenceLinksWrapperPropertyBitMask = ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.None;
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
			base.ReadPayloadStart(false);
			if (flag)
			{
				base.JsonReader.ReadStartObject();
				if (!this.ReadEntityReferenceLinkProperties(odataEntityReferenceLinks, ref entityReferenceLinksWrapperPropertyBitMask))
				{
					throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksResultsPropertyNotFound);
				}
			}
			base.JsonReader.ReadStartArray();
			List<ODataEntityReferenceLink> list = new List<ODataEntityReferenceLink>();
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = this.ReadSingleEntityReferenceLink();
				list.Add(odataEntityReferenceLink);
			}
			base.JsonReader.ReadEndArray();
			if (flag)
			{
				this.ReadEntityReferenceLinkProperties(odataEntityReferenceLinks, ref entityReferenceLinksWrapperPropertyBitMask);
				base.JsonReader.ReadEndObject();
			}
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0003FF48 File Offset: 0x0003E148
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			base.ReadPayloadStart(false);
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadSingleEntityReferenceLink();
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLink;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003FF6C File Offset: 0x0003E16C
		private bool ReadEntityReferenceLinkProperties(ODataEntityReferenceLinks entityReferenceLinks, ref ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask propertiesFoundBitField)
		{
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				if ((text2 = text) != null)
				{
					if (text2 == "results")
					{
						ODataVerboseJsonReaderUtils.VerifyEntityReferenceLinksWrapperPropertyNotFound(ref propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.Results, "results");
						return true;
					}
					if (text2 == "__count")
					{
						ODataVerboseJsonReaderUtils.VerifyEntityReferenceLinksWrapperPropertyNotFound(ref propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.Count, "__count");
						object obj = base.JsonReader.ReadPrimitiveValue();
						long? num = (long?)ODataVerboseJsonReaderUtils.ConvertValue(obj, EdmCoreModel.Instance.GetInt64(true), base.MessageReaderSettings, base.Version, true, text);
						ODataVerboseJsonReaderUtils.ValidateCountPropertyInEntityReferenceLinks(num);
						entityReferenceLinks.Count = num;
						continue;
					}
					if (text2 == "__next")
					{
						ODataVerboseJsonReaderUtils.VerifyEntityReferenceLinksWrapperPropertyNotFound(ref propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.NextPageLink, "__next");
						string text3 = base.JsonReader.ReadStringValue("__next");
						ODataVerboseJsonReaderUtils.ValidateEntityReferenceLinksStringProperty(text3, "__next");
						entityReferenceLinks.NextPageLink = base.ProcessUriFromPayload(text3);
						continue;
					}
				}
				base.JsonReader.SkipValue();
			}
			return false;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00040070 File Offset: 0x0003E270
		private ODataEntityReferenceLink ReadSingleEntityReferenceLink()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartObject();
			ODataEntityReferenceLink odataEntityReferenceLink = new ODataEntityReferenceLink();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("uri", text) == 0)
				{
					if (odataEntityReferenceLink.Url != null)
					{
						throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink);
					}
					string text2 = base.JsonReader.ReadStringValue("uri");
					if (text2 == null)
					{
						throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkUriCannotBeNull);
					}
					odataEntityReferenceLink.Url = base.ProcessUriFromPayload(text2);
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			ReaderValidationUtils.ValidateEntityReferenceLink(odataEntityReferenceLink);
			base.JsonReader.ReadEndObject();
			return odataEntityReferenceLink;
		}
	}
}
