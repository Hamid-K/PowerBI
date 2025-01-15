using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000046 RID: 70
	internal abstract class ODataAtomMetadataSerializer : ODataAtomSerializer
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000A899 File Offset: 0x00008A99
		internal ODataAtomMetadataSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		internal void WriteTextConstruct(string prefix, string localName, string ns, AtomTextConstruct textConstruct)
		{
			base.XmlWriter.WriteStartElement(prefix, localName, ns);
			if (textConstruct != null)
			{
				AtomTextConstructKind kind = textConstruct.Kind;
				base.XmlWriter.WriteAttributeString("type", AtomValueUtils.ToString(textConstruct.Kind));
				string text = textConstruct.Text;
				if (text == null)
				{
					text = string.Empty;
				}
				if (kind == AtomTextConstructKind.Xhtml)
				{
					ODataAtomWriterUtils.WriteRaw(base.XmlWriter, text);
				}
				else
				{
					ODataAtomWriterUtils.WriteString(base.XmlWriter, text);
				}
			}
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A921 File Offset: 0x00008B21
		internal void WriteCategory(AtomCategoryMetadata category)
		{
			this.WriteCategory("", category.Term, category.Scheme, category.Label);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A940 File Offset: 0x00008B40
		internal void WriteCategory(string atomPrefix, string term, string scheme, string label)
		{
			base.XmlWriter.WriteStartElement(atomPrefix, "category", "http://www.w3.org/2005/Atom");
			if (term == null)
			{
				throw new ODataException(Strings.ODataAtomWriterMetadataUtils_CategoryMustSpecifyTerm);
			}
			base.XmlWriter.WriteAttributeString("term", ODataAtomWriterUtils.PrefixTypeName(term));
			if (scheme != null)
			{
				base.XmlWriter.WriteAttributeString("scheme", scheme);
			}
			if (label != null)
			{
				base.XmlWriter.WriteAttributeString("label", label);
			}
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A9BC File Offset: 0x00008BBC
		internal void WriteEmptyAuthor()
		{
			base.XmlWriter.WriteStartElement("", "author", "http://www.w3.org/2005/Atom");
			base.WriteEmptyElement("", "name", "http://www.w3.org/2005/Atom");
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		internal void WritePersonMetadata(AtomPersonMetadata personMetadata)
		{
			base.WriteElementWithTextContent("", "name", "http://www.w3.org/2005/Atom", personMetadata.Name);
			string text = personMetadata.UriFromEpm;
			if (text == null)
			{
				Uri uri = personMetadata.Uri;
				if (uri != null)
				{
					text = base.UriToUrlAttributeValue(uri);
				}
			}
			if (text != null)
			{
				base.WriteElementWithTextContent("", "uri", "http://www.w3.org/2005/Atom", text);
			}
			string email = personMetadata.Email;
			if (email != null)
			{
				base.WriteElementWithTextContent("", "email", "http://www.w3.org/2005/Atom", email);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000AA7B File Offset: 0x00008C7B
		internal void WriteAtomLink(AtomLinkMetadata linkMetadata, string etag)
		{
			base.XmlWriter.WriteStartElement("", "link", "http://www.w3.org/2005/Atom");
			this.WriteAtomLinkAttributes(linkMetadata, etag);
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AAAC File Offset: 0x00008CAC
		internal void WriteAtomLinkAttributes(AtomLinkMetadata linkMetadata, string etag)
		{
			string text = ((linkMetadata.Href == null) ? null : base.UriToUrlAttributeValue(linkMetadata.Href));
			this.WriteAtomLinkMetadataAttributes(linkMetadata.Relation, text, linkMetadata.HrefLang, linkMetadata.Title, linkMetadata.MediaType, linkMetadata.Length);
			if (etag != null)
			{
				ODataAtomWriterUtils.WriteETag(base.XmlWriter, etag);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AB0C File Offset: 0x00008D0C
		private void WriteAtomLinkMetadataAttributes(string relation, string href, string hrefLang, string title, string mediaType, int? length)
		{
			if (relation != null)
			{
				base.XmlWriter.WriteAttributeString("rel", relation);
			}
			if (mediaType != null)
			{
				base.XmlWriter.WriteAttributeString("type", mediaType);
			}
			if (title != null)
			{
				base.XmlWriter.WriteAttributeString("title", title);
			}
			if (href == null)
			{
				throw new ODataException(Strings.ODataAtomWriterMetadataUtils_LinkMustSpecifyHref);
			}
			base.XmlWriter.WriteAttributeString("href", href);
			if (hrefLang != null)
			{
				base.XmlWriter.WriteAttributeString("hreflang", hrefLang);
			}
			if (length != null)
			{
				base.XmlWriter.WriteAttributeString("length", length.Value.ToString());
			}
		}
	}
}
