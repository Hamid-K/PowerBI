using System;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Client.Packaging.SerializationObjectModel
{
	// Token: 0x02000010 RID: 16
	public class SerializedPackageItemMetadata
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002BF5 File Offset: 0x00000DF5
		public SerializedPackageItemMetadata()
		{
			this.Entries = new SerializedMetadataEntry[0];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C09 File Offset: 0x00000E09
		public SerializedPackageItemMetadata(SerializedPackageItemLocation itemLocation, SerializedMetadataEntry[] entries)
		{
			this.ItemLocation = itemLocation;
			this.Entries = entries;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C1F File Offset: 0x00000E1F
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002C27 File Offset: 0x00000E27
		[XmlElement]
		public SerializedPackageItemLocation ItemLocation { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C30 File Offset: 0x00000E30
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002C38 File Offset: 0x00000E38
		[XmlArray("StableEntries")]
		[XmlArrayItem("Entry")]
		public SerializedMetadataEntry[] Entries { get; set; }
	}
}
