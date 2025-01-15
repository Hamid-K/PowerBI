using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000041 RID: 65
	internal sealed class ODataAtomEntityReferenceLinkSerializer : ODataAtomSerializer
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00007B72 File Offset: 0x00005D72
		internal ODataAtomEntityReferenceLinkSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.contextUriBuilder = atomOutputContext.CreateContextUriBuilder();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007B87 File Offset: 0x00005D87
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			base.WritePayloadStart();
			this.WriteEntityReferenceLink(entityReferenceLink, true);
			base.WritePayloadEnd();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007BA0 File Offset: 0x00005DA0
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks)
		{
			base.WritePayloadStart();
			base.XmlWriter.WriteStartElement(string.Empty, "feed", "http://www.w3.org/2005/Atom");
			base.XmlWriter.WriteAttributeString("xmlns", "m", null, "http://docs.oasis-open.org/odata/ns/metadata");
			base.WriteContextUriProperty(this.contextUriBuilder.BuildContextUri(ODataPayloadKind.EntityReferenceLinks, null));
			if (entityReferenceLinks.Count != null)
			{
				base.WriteCount(entityReferenceLinks.Count.Value);
			}
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					WriterValidationUtils.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
					this.WriteEntityReferenceLink(odataEntityReferenceLink, false);
				}
			}
			if (entityReferenceLinks.NextPageLink != null)
			{
				string text = base.UriToUrlAttributeValue(entityReferenceLinks.NextPageLink);
				base.XmlWriter.WriteElementString(string.Empty, "next", "http://docs.oasis-open.org/odata/ns/data", text);
			}
			base.XmlWriter.WriteEndElement();
			base.WritePayloadEnd();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007CB8 File Offset: 0x00005EB8
		private void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink, bool isTopLevel)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			if (isTopLevel)
			{
				base.XmlWriter.WriteStartElement("m", "ref", "http://docs.oasis-open.org/odata/ns/metadata");
				base.WriteContextUriProperty(this.contextUriBuilder.BuildContextUri(ODataPayloadKind.EntityReferenceLink, null));
			}
			else
			{
				base.XmlWriter.WriteStartElement("m", "ref", null);
			}
			base.XmlWriter.WriteAttributeString("id", null, base.UriToUrlAttributeValue(entityReferenceLink.Url));
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x0400014B RID: 331
		private readonly ODataContextUriBuilder contextUriBuilder;
	}
}
