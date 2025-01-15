using System;
using System.Xml.Serialization;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200000D RID: 13
	[XmlRoot("SerializeableCookie")]
	[Serializable]
	public struct SerializeableCookie
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000374E File Offset: 0x0000194E
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003756 File Offset: 0x00001956
		public string Name { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000375F File Offset: 0x0000195F
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003767 File Offset: 0x00001967
		public string Value { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003770 File Offset: 0x00001970
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003778 File Offset: 0x00001978
		public string Path { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003781 File Offset: 0x00001981
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003789 File Offset: 0x00001989
		public string Domain { get; set; }
	}
}
