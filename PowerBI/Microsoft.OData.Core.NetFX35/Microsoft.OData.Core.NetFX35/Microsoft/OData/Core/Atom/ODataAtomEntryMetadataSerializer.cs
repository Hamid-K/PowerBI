using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000047 RID: 71
	internal sealed class ODataAtomEntryMetadataSerializer : ODataAtomMetadataSerializer
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000ABB1 File Offset: 0x00008DB1
		internal ODataAtomEntryMetadataSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000ABBC File Offset: 0x00008DBC
		private ODataAtomFeedMetadataSerializer SourceMetadataSerializer
		{
			get
			{
				ODataAtomFeedMetadataSerializer odataAtomFeedMetadataSerializer;
				if ((odataAtomFeedMetadataSerializer = this.sourceMetadataSerializer) == null)
				{
					odataAtomFeedMetadataSerializer = (this.sourceMetadataSerializer = new ODataAtomFeedMetadataSerializer(base.AtomOutputContext));
				}
				return odataAtomFeedMetadataSerializer;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "No good way to refactor; logic should be kept together.")]
		internal void WriteEntryMetadata(AtomEntryMetadata entryMetadata, string updatedTime)
		{
			if (entryMetadata == null)
			{
				base.WriteEmptyElement("", "title", "http://www.w3.org/2005/Atom");
				base.WriteElementWithTextContent("", "updated", "http://www.w3.org/2005/Atom", updatedTime);
				base.WriteEmptyAuthor();
				return;
			}
			base.WriteTextConstruct("", "title", "http://www.w3.org/2005/Atom", entryMetadata.Title);
			AtomTextConstruct summary = entryMetadata.Summary;
			if (summary != null)
			{
				base.WriteTextConstruct("", "summary", "http://www.w3.org/2005/Atom", summary);
			}
			string text = ((entryMetadata.Published != null) ? ODataAtomConvert.ToAtomString(entryMetadata.Published.Value) : null);
			if (text != null)
			{
				base.WriteElementWithTextContent("", "published", "http://www.w3.org/2005/Atom", text);
			}
			string text2 = ((entryMetadata.Updated != null) ? ODataAtomConvert.ToAtomString(entryMetadata.Updated.Value) : null);
			text2 = text2 ?? updatedTime;
			base.WriteElementWithTextContent("", "updated", "http://www.w3.org/2005/Atom", text2);
			bool flag = false;
			IEnumerable<AtomPersonMetadata> authors = entryMetadata.Authors;
			if (authors != null)
			{
				foreach (AtomPersonMetadata atomPersonMetadata in authors)
				{
					if (atomPersonMetadata == null)
					{
						throw new ODataException(Strings.ODataAtomWriterMetadataUtils_AuthorMetadataMustNotContainNull);
					}
					base.XmlWriter.WriteStartElement("", "author", "http://www.w3.org/2005/Atom");
					base.WritePersonMetadata(atomPersonMetadata);
					base.XmlWriter.WriteEndElement();
					flag = true;
				}
			}
			if (!flag)
			{
				base.WriteEmptyAuthor();
			}
			IEnumerable<AtomPersonMetadata> contributors = entryMetadata.Contributors;
			if (contributors != null)
			{
				foreach (AtomPersonMetadata atomPersonMetadata2 in contributors)
				{
					if (atomPersonMetadata2 == null)
					{
						throw new ODataException(Strings.ODataAtomWriterMetadataUtils_ContributorMetadataMustNotContainNull);
					}
					base.XmlWriter.WriteStartElement("", "contributor", "http://www.w3.org/2005/Atom");
					base.WritePersonMetadata(atomPersonMetadata2);
					base.XmlWriter.WriteEndElement();
				}
			}
			IEnumerable<AtomLinkMetadata> links = entryMetadata.Links;
			if (links != null)
			{
				foreach (AtomLinkMetadata atomLinkMetadata in links)
				{
					if (atomLinkMetadata == null)
					{
						throw new ODataException(Strings.ODataAtomWriterMetadataUtils_LinkMetadataMustNotContainNull);
					}
					base.WriteAtomLink(atomLinkMetadata, null);
				}
			}
			IEnumerable<AtomCategoryMetadata> categories = entryMetadata.Categories;
			if (categories != null)
			{
				foreach (AtomCategoryMetadata atomCategoryMetadata in categories)
				{
					if (atomCategoryMetadata == null)
					{
						throw new ODataException(Strings.ODataAtomWriterMetadataUtils_CategoryMetadataMustNotContainNull);
					}
					base.WriteCategory(atomCategoryMetadata);
				}
			}
			if (entryMetadata.Rights != null)
			{
				base.WriteTextConstruct("", "rights", "http://www.w3.org/2005/Atom", entryMetadata.Rights);
			}
			AtomFeedMetadata source = entryMetadata.Source;
			if (source != null)
			{
				base.XmlWriter.WriteStartElement("", "source", "http://www.w3.org/2005/Atom");
				bool flag2;
				this.SourceMetadataSerializer.WriteFeedMetadata(source, null, updatedTime, out flag2);
				base.XmlWriter.WriteEndElement();
			}
		}

		// Token: 0x04000170 RID: 368
		private ODataAtomFeedMetadataSerializer sourceMetadataSerializer;
	}
}
