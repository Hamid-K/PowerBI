using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200003F RID: 63
	internal sealed class ODataAtomEntityReferenceLinkDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00007808 File Offset: 0x00005A08
		internal ODataAtomEntityReferenceLinkDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.ODataFeedElementName = nameTable.Add("feed");
			this.ODataCountElementName = nameTable.Add("count");
			this.ODataNextElementName = nameTable.Add("next");
			this.ODataRefElementName = nameTable.Add("ref");
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000786C File Offset: 0x00005A6C
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			base.ReadPayloadStart();
			if (!base.XmlReader.NamespaceEquals(base.XmlReader.NamespaceURI) || !base.XmlReader.LocalNameEquals(this.ODataFeedElementName))
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksStartElement(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadFeedElement();
			base.ReadPayloadEnd();
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000078DC File Offset: 0x00005ADC
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			base.ReadPayloadStart();
			if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) || !base.XmlReader.LocalNameEquals(this.ODataRefElementName))
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinkStartElement(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceId();
			base.ReadPayloadEnd();
			return odataEntityReferenceLink;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007949 File Offset: 0x00005B49
		private static void VerifyEntityReferenceLinksElementNotFound(ref ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask elementsFoundBitField, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask elementFoundBitMask, string elementNamespace, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_MultipleEntityReferenceLinksElementsWithSameName(elementNamespace, elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007968 File Offset: 0x00005B68
		private ODataEntityReferenceLinks ReadFeedElement()
		{
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
			List<ODataEntityReferenceLink> list = new List<ODataEntityReferenceLink>();
			ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask duplicateEntityReferenceLinksElementBitMask = ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.None;
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							goto IL_0172;
						}
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && base.XmlReader.LocalNameEquals(this.ODataCountElementName))
					{
						ODataAtomEntityReferenceLinkDeserializer.VerifyEntityReferenceLinksElementNotFound(ref duplicateEntityReferenceLinksElementBitMask, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.Count, base.XmlReader.ODataMetadataNamespace, "count");
						long num = (long)AtomValueUtils.ReadPrimitiveValue(base.XmlReader, EdmCoreModel.Instance.GetInt64(false));
						odataEntityReferenceLinks.Count = new long?(num);
						base.XmlReader.Read();
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && base.XmlReader.LocalNameEquals(this.ODataRefElementName))
					{
						ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceId();
						list.Add(odataEntityReferenceLink);
					}
					else
					{
						if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace) || !base.XmlReader.LocalNameEquals(this.ODataNextElementName))
						{
							goto IL_0172;
						}
						ODataAtomEntityReferenceLinkDeserializer.VerifyEntityReferenceLinksElementNotFound(ref duplicateEntityReferenceLinksElementBitMask, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.NextLink, base.XmlReader.ODataNamespace, "next");
						Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
						string text = base.XmlReader.ReadElementValue();
						odataEntityReferenceLinks.NextPageLink = base.ProcessUriFromPayload(text, xmlBaseUri);
					}
					IL_017D:
					if (base.XmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_0172:
					base.XmlReader.Skip();
					goto IL_017D;
				}
			}
			base.XmlReader.Read();
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			return odataEntityReferenceLinks;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007B20 File Offset: 0x00005D20
		private ODataEntityReferenceLink ReadEntityReferenceId()
		{
			ODataEntityReferenceLink odataEntityReferenceLink = new ODataEntityReferenceLink();
			string attribute = base.XmlReader.GetAttribute("id");
			Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
			Uri uri = base.ProcessUriFromPayload(attribute, xmlBaseUri);
			odataEntityReferenceLink.Url = uri;
			base.XmlReader.Skip();
			ReaderValidationUtils.ValidateEntityReferenceLink(odataEntityReferenceLink);
			return odataEntityReferenceLink;
		}

		// Token: 0x04000143 RID: 323
		private readonly string ODataFeedElementName;

		// Token: 0x04000144 RID: 324
		private readonly string ODataCountElementName;

		// Token: 0x04000145 RID: 325
		private readonly string ODataNextElementName;

		// Token: 0x04000146 RID: 326
		private readonly string ODataRefElementName;

		// Token: 0x02000040 RID: 64
		[Flags]
		private enum DuplicateEntityReferenceLinksElementBitMask
		{
			// Token: 0x04000148 RID: 328
			None = 0,
			// Token: 0x04000149 RID: 329
			Count = 1,
			// Token: 0x0400014A RID: 330
			NextLink = 2
		}
	}
}
