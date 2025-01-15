using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Client.Packaging.SerializationObjectModel
{
	// Token: 0x02000012 RID: 18
	[XmlRoot("LocalPackageMetadataFile")]
	public class SerializedPackageMetadata : XmlRoot
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002C41 File Offset: 0x00000E41
		public SerializedPackageMetadata()
		{
			this.items = new List<SerializedPackageItemMetadata>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C54 File Offset: 0x00000E54
		public SerializedPackageMetadata(IEnumerable<SerializedPackageItemMetadata> metadataItems)
		{
			this.items = new List<SerializedPackageItemMetadata>(metadataItems);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002C68 File Offset: 0x00000E68
		[XmlArray("Items")]
		[XmlArrayItem("Item")]
		public List<SerializedPackageItemMetadata> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x04000053 RID: 83
		private List<SerializedPackageItemMetadata> items;
	}
}
