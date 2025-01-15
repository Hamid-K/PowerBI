using System;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000033 RID: 51
	internal class ODataAtomSerializer : ODataSerializer
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x000061FF File Offset: 0x000043FF
		internal ODataAtomSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.atomOutputContext = atomOutputContext;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000620F File Offset: 0x0000440F
		internal XmlWriter XmlWriter
		{
			get
			{
				return this.atomOutputContext.XmlWriter;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000621C File Offset: 0x0000441C
		protected ODataAtomOutputContext AtomOutputContext
		{
			get
			{
				return this.atomOutputContext;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006224 File Offset: 0x00004424
		internal string UriToUrlAttributeValue(Uri uri)
		{
			return this.UriToUrlAttributeValue(uri, true);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006230 File Offset: 0x00004430
		internal string UriToUrlAttributeValue(Uri uri, bool failOnRelativeUriWithoutBaseUri)
		{
			if (base.UrlResolver != null)
			{
				Uri uri2 = base.UrlResolver.ResolveUrl(base.MessageWriterSettings.PayloadBaseUri, uri);
				if (uri2 != null)
				{
					return UriUtils.UriToString(uri2);
				}
			}
			if (!uri.IsAbsoluteUri)
			{
				if (base.MessageWriterSettings.PayloadBaseUri == null && failOnRelativeUriWithoutBaseUri)
				{
					throw new ODataException(Strings.ODataWriter_RelativeUriUsedWithoutBaseUriSpecified(UriUtils.UriToString(uri)));
				}
				uri = UriUtils.EnsureEscapedRelativeUri(uri);
			}
			return UriUtils.UriToString(uri);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000062AA File Offset: 0x000044AA
		internal void WritePayloadStart()
		{
			this.XmlWriter.WriteStartDocument();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000062B7 File Offset: 0x000044B7
		internal void WritePayloadEnd()
		{
			this.XmlWriter.WriteEndDocument();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000062C4 File Offset: 0x000044C4
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WritePayloadStart();
			ODataAtomWriterUtils.WriteError(this.XmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
			this.WritePayloadEnd();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000062F0 File Offset: 0x000044F0
		internal void WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags flags)
		{
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.Atom) == ODataAtomSerializer.DefaultNamespaceFlags.Atom)
			{
				this.XmlWriter.WriteAttributeString("xmlns", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2005/Atom");
			}
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.OData) == ODataAtomSerializer.DefaultNamespaceFlags.OData)
			{
				this.XmlWriter.WriteAttributeString("d", "http://www.w3.org/2000/xmlns/", "http://docs.oasis-open.org/odata/ns/data");
			}
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata) == ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata)
			{
				this.XmlWriter.WriteAttributeString("m", "http://www.w3.org/2000/xmlns/", "http://docs.oasis-open.org/odata/ns/metadata");
			}
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.GeoRss) == ODataAtomSerializer.DefaultNamespaceFlags.GeoRss)
			{
				this.XmlWriter.WriteAttributeString("georss", "http://www.w3.org/2000/xmlns/", "http://www.georss.org/georss");
			}
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.Gml) == ODataAtomSerializer.DefaultNamespaceFlags.Gml)
			{
				this.XmlWriter.WriteAttributeString("gml", "http://www.w3.org/2000/xmlns/", "http://www.opengis.net/gml");
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000639F File Offset: 0x0000459F
		internal void WriteCount(long count)
		{
			this.XmlWriter.WriteStartElement("m", "count", null);
			this.XmlWriter.WriteValue(count);
			this.XmlWriter.WriteEndElement();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000063D0 File Offset: 0x000045D0
		internal void WriteBaseUriAndDefaultNamespaceAttributes()
		{
			Uri payloadBaseUri = base.MessageWriterSettings.PayloadBaseUri;
			if (payloadBaseUri != null)
			{
				this.XmlWriter.WriteAttributeString("base", "http://www.w3.org/XML/1998/namespace", payloadBaseUri.AbsoluteUri);
			}
			this.WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags.All);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006415 File Offset: 0x00004615
		internal void WriteElementWithTextContent(string prefix, string localName, string ns, string textContent)
		{
			this.XmlWriter.WriteStartElement(prefix, localName, ns);
			if (textContent != null)
			{
				ODataAtomWriterUtils.WriteString(this.XmlWriter, textContent);
			}
			this.XmlWriter.WriteEndElement();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006441 File Offset: 0x00004641
		internal void WriteEmptyElement(string prefix, string localName, string ns)
		{
			this.XmlWriter.WriteStartElement(prefix, localName, ns);
			this.XmlWriter.WriteEndElement();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000645C File Offset: 0x0000465C
		internal void WriteContextUriProperty(Uri contextUri)
		{
			if (contextUri != null)
			{
				this.XmlWriter.WriteAttributeString("context", "http://docs.oasis-open.org/odata/ns/metadata", contextUri.AbsoluteUri);
			}
		}

		// Token: 0x04000123 RID: 291
		private ODataAtomOutputContext atomOutputContext;

		// Token: 0x02000034 RID: 52
		[Flags]
		internal enum DefaultNamespaceFlags
		{
			// Token: 0x04000125 RID: 293
			None = 0,
			// Token: 0x04000126 RID: 294
			OData = 1,
			// Token: 0x04000127 RID: 295
			ODataMetadata = 2,
			// Token: 0x04000128 RID: 296
			Atom = 4,
			// Token: 0x04000129 RID: 297
			GeoRss = 8,
			// Token: 0x0400012A RID: 298
			Gml = 16,
			// Token: 0x0400012B RID: 299
			All = 31
		}
	}
}
