using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200004C RID: 76
	internal sealed class ODataAtomFeedMetadataSerializer : ODataAtomMetadataSerializer
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000B908 File Offset: 0x00009B08
		internal ODataAtomFeedMetadataSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B914 File Offset: 0x00009B14
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Does not make sense to break up writing the various parts of feed metadata. Very linear code; complexity not high.")]
		internal void WriteFeedMetadata(AtomFeedMetadata feedMetadata, ODataFeed feed, string updatedTime, out bool authorWritten)
		{
			Uri uri = ((feed == null) ? feedMetadata.SourceId : feed.Id);
			base.WriteElementWithTextContent("", "id", "http://www.w3.org/2005/Atom", (uri == null) ? null : UriUtils.UriToString(uri));
			base.WriteTextConstruct("", "title", "http://www.w3.org/2005/Atom", feedMetadata.Title);
			if (feedMetadata.Subtitle != null)
			{
				base.WriteTextConstruct("", "subtitle", "http://www.w3.org/2005/Atom", feedMetadata.Subtitle);
			}
			string text = ((feedMetadata.Updated != null) ? ODataAtomConvert.ToAtomString(feedMetadata.Updated.Value) : updatedTime);
			base.WriteElementWithTextContent("", "updated", "http://www.w3.org/2005/Atom", text);
			AtomLinkMetadata selfLink = feedMetadata.SelfLink;
			if (selfLink != null)
			{
				AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(selfLink, "self", null, null, null);
				base.WriteAtomLink(atomLinkMetadata, null);
			}
			IEnumerable<AtomLinkMetadata> links = feedMetadata.Links;
			if (links != null)
			{
				foreach (AtomLinkMetadata atomLinkMetadata2 in links)
				{
					if (atomLinkMetadata2.Relation != "http://docs.oasis-open.org/odata/ns/delta")
					{
						base.WriteAtomLink(atomLinkMetadata2, null);
					}
				}
			}
			IEnumerable<AtomCategoryMetadata> categories = feedMetadata.Categories;
			if (categories != null)
			{
				foreach (AtomCategoryMetadata atomCategoryMetadata in categories)
				{
					base.WriteCategory(atomCategoryMetadata);
				}
			}
			Uri logo = feedMetadata.Logo;
			if (logo != null)
			{
				base.WriteElementWithTextContent("", "logo", "http://www.w3.org/2005/Atom", base.UriToUrlAttributeValue(logo));
			}
			if (feedMetadata.Rights != null)
			{
				base.WriteTextConstruct("", "rights", "http://www.w3.org/2005/Atom", feedMetadata.Rights);
			}
			IEnumerable<AtomPersonMetadata> contributors = feedMetadata.Contributors;
			if (contributors != null)
			{
				foreach (AtomPersonMetadata atomPersonMetadata in contributors)
				{
					base.XmlWriter.WriteStartElement("", "contributor", "http://www.w3.org/2005/Atom");
					base.WritePersonMetadata(atomPersonMetadata);
					base.XmlWriter.WriteEndElement();
				}
			}
			AtomGeneratorMetadata generator = feedMetadata.Generator;
			if (generator != null)
			{
				base.XmlWriter.WriteStartElement("", "generator", "http://www.w3.org/2005/Atom");
				if (generator.Uri != null)
				{
					base.XmlWriter.WriteAttributeString("uri", base.UriToUrlAttributeValue(generator.Uri));
				}
				if (!string.IsNullOrEmpty(generator.Version))
				{
					base.XmlWriter.WriteAttributeString("version", generator.Version);
				}
				ODataAtomWriterUtils.WriteString(base.XmlWriter, generator.Name);
				base.XmlWriter.WriteEndElement();
			}
			Uri icon = feedMetadata.Icon;
			if (icon != null)
			{
				base.WriteElementWithTextContent("", "icon", "http://www.w3.org/2005/Atom", base.UriToUrlAttributeValue(icon));
			}
			IEnumerable<AtomPersonMetadata> authors = feedMetadata.Authors;
			authorWritten = false;
			if (authors != null)
			{
				foreach (AtomPersonMetadata atomPersonMetadata2 in authors)
				{
					authorWritten = true;
					base.XmlWriter.WriteStartElement("", "author", "http://www.w3.org/2005/Atom");
					base.WritePersonMetadata(atomPersonMetadata2);
					base.XmlWriter.WriteEndElement();
				}
			}
		}
	}
}
