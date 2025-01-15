using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001FA RID: 506
	internal sealed class ODataAtomEntityReferenceLinkDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x00034A98 File Offset: 0x00032C98
		internal ODataAtomEntityReferenceLinkDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.ODataLinksElementName = nameTable.Add("links");
			this.ODataCountElementName = nameTable.Add("count");
			this.ODataNextElementName = nameTable.Add("next");
			this.ODataUriElementName = nameTable.Add("uri");
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00034AFC File Offset: 0x00032CFC
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			base.ReadPayloadStart();
			if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace) || !base.XmlReader.LocalNameEquals(this.ODataLinksElementName))
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksStartElement(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadLinksElement();
			base.ReadPayloadEnd();
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00034B6C File Offset: 0x00032D6C
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			base.ReadPayloadStart();
			if ((!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace) && !base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace)) || !base.XmlReader.LocalNameEquals(this.ODataUriElementName))
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinkStartElement(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadUriElement();
			base.ReadPayloadEnd();
			return odataEntityReferenceLink;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00034BF1 File Offset: 0x00032DF1
		private static void VerifyEntityReferenceLinksElementNotFound(ref ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask elementsFoundBitField, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask elementFoundBitMask, string elementNamespace, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_MultipleEntityReferenceLinksElementsWithSameName(elementNamespace, elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00034C10 File Offset: 0x00032E10
		private ODataEntityReferenceLinks ReadLinksElement()
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
							goto IL_016F;
						}
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && base.XmlReader.LocalNameEquals(this.ODataCountElementName) && base.Version >= ODataVersion.V2)
					{
						ODataAtomEntityReferenceLinkDeserializer.VerifyEntityReferenceLinksElementNotFound(ref duplicateEntityReferenceLinksElementBitMask, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.Count, base.XmlReader.ODataMetadataNamespace, "count");
						long num = (long)AtomValueUtils.ReadPrimitiveValue(base.XmlReader, EdmCoreModel.Instance.GetInt64(false));
						odataEntityReferenceLinks.Count = new long?(num);
						base.XmlReader.Read();
					}
					else
					{
						if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
						{
							goto IL_016F;
						}
						if (base.XmlReader.LocalNameEquals(this.ODataUriElementName))
						{
							ODataEntityReferenceLink odataEntityReferenceLink = this.ReadUriElement();
							list.Add(odataEntityReferenceLink);
						}
						else
						{
							if (!base.XmlReader.LocalNameEquals(this.ODataNextElementName) || base.Version < ODataVersion.V2)
							{
								goto IL_016F;
							}
							ODataAtomEntityReferenceLinkDeserializer.VerifyEntityReferenceLinksElementNotFound(ref duplicateEntityReferenceLinksElementBitMask, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.NextLink, base.XmlReader.ODataNamespace, "next");
							Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
							string text = base.XmlReader.ReadElementValue();
							odataEntityReferenceLinks.NextPageLink = base.ProcessUriFromPayload(text, xmlBaseUri);
						}
					}
					IL_017A:
					if (base.XmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_016F:
					base.XmlReader.Skip();
					goto IL_017A;
				}
			}
			base.XmlReader.Read();
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00034DC4 File Offset: 0x00032FC4
		private ODataEntityReferenceLink ReadUriElement()
		{
			ODataEntityReferenceLink odataEntityReferenceLink = new ODataEntityReferenceLink();
			Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
			string text = base.XmlReader.ReadElementValue();
			Uri uri = base.ProcessUriFromPayload(text, xmlBaseUri);
			odataEntityReferenceLink.Url = uri;
			ReaderValidationUtils.ValidateEntityReferenceLink(odataEntityReferenceLink);
			return odataEntityReferenceLink;
		}

		// Token: 0x04000573 RID: 1395
		private readonly string ODataLinksElementName;

		// Token: 0x04000574 RID: 1396
		private readonly string ODataCountElementName;

		// Token: 0x04000575 RID: 1397
		private readonly string ODataNextElementName;

		// Token: 0x04000576 RID: 1398
		private readonly string ODataUriElementName;

		// Token: 0x020001FB RID: 507
		[Flags]
		private enum DuplicateEntityReferenceLinksElementBitMask
		{
			// Token: 0x04000578 RID: 1400
			None = 0,
			// Token: 0x04000579 RID: 1401
			Count = 1,
			// Token: 0x0400057A RID: 1402
			NextLink = 2
		}
	}
}
