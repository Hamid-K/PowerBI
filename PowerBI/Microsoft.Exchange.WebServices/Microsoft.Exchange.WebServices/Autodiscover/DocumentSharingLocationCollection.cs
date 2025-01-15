using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200001E RID: 30
	public sealed class DocumentSharingLocationCollection
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00006F1C File Offset: 0x00005F1C
		internal DocumentSharingLocationCollection()
		{
			this.Entries = new List<DocumentSharingLocation>();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006F30 File Offset: 0x00005F30
		internal static DocumentSharingLocationCollection LoadFromXml(EwsXmlReader reader)
		{
			DocumentSharingLocationCollection documentSharingLocationCollection = new DocumentSharingLocationCollection();
			do
			{
				reader.Read();
				if (reader.NodeType == 1 && reader.LocalName == "DocumentSharingLocation")
				{
					DocumentSharingLocation documentSharingLocation = DocumentSharingLocation.LoadFromXml(reader);
					documentSharingLocationCollection.Entries.Add(documentSharingLocation);
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "DocumentSharingLocations"));
			return documentSharingLocationCollection;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006F87 File Offset: 0x00005F87
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00006F8F File Offset: 0x00005F8F
		public List<DocumentSharingLocation> Entries { get; private set; }
	}
}
