using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001C1 RID: 449
	internal sealed class ODataAtomServiceDocumentSerializer : ODataAtomSerializer
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x0002EBD0 File Offset: 0x0002CDD0
		internal ODataAtomServiceDocumentSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.atomServiceDocumentMetadataSerializer = new ODataAtomServiceDocumentMetadataSerializer(atomOutputContext);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		internal void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			IEnumerable<ODataResourceCollectionInfo> collections = defaultWorkspace.Collections;
			base.WritePayloadStart();
			base.XmlWriter.WriteStartElement(string.Empty, "service", "http://www.w3.org/2007/app");
			if (base.MessageWriterSettings.BaseUri != null)
			{
				base.XmlWriter.WriteAttributeString("base", "http://www.w3.org/XML/1998/namespace", base.MessageWriterSettings.BaseUri.AbsoluteUri);
			}
			base.XmlWriter.WriteAttributeString("xmlns", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2007/app");
			base.XmlWriter.WriteAttributeString("atom", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2005/Atom");
			base.XmlWriter.WriteStartElement(string.Empty, "workspace", "http://www.w3.org/2007/app");
			this.atomServiceDocumentMetadataSerializer.WriteWorkspaceMetadata(defaultWorkspace);
			if (collections != null)
			{
				foreach (ODataResourceCollectionInfo odataResourceCollectionInfo in collections)
				{
					ValidationUtils.ValidateResourceCollectionInfo(odataResourceCollectionInfo);
					base.XmlWriter.WriteStartElement(string.Empty, "collection", "http://www.w3.org/2007/app");
					base.XmlWriter.WriteAttributeString("href", base.UriToUrlAttributeValue(odataResourceCollectionInfo.Url));
					this.atomServiceDocumentMetadataSerializer.WriteResourceCollectionMetadata(odataResourceCollectionInfo);
					base.XmlWriter.WriteEndElement();
				}
			}
			base.XmlWriter.WriteEndElement();
			base.XmlWriter.WriteEndElement();
			base.WritePayloadEnd();
		}

		// Token: 0x040004AC RID: 1196
		private readonly ODataAtomServiceDocumentMetadataSerializer atomServiceDocumentMetadataSerializer;
	}
}
