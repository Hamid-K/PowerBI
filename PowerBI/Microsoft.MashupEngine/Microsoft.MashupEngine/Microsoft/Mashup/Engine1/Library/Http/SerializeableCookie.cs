using System;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A4B RID: 2635
	[XmlRoot("SerializeableCookie")]
	[Serializable]
	public struct SerializeableCookie
	{
		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x000F55B0 File Offset: 0x000F37B0
		// (set) Token: 0x06004945 RID: 18757 RVA: 0x000F55B8 File Offset: 0x000F37B8
		public string Name { get; set; }

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x000F55C1 File Offset: 0x000F37C1
		// (set) Token: 0x06004947 RID: 18759 RVA: 0x000F55C9 File Offset: 0x000F37C9
		public string Value { get; set; }

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x000F55D2 File Offset: 0x000F37D2
		// (set) Token: 0x06004949 RID: 18761 RVA: 0x000F55DA File Offset: 0x000F37DA
		public string Path { get; set; }

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x000F55E3 File Offset: 0x000F37E3
		// (set) Token: 0x0600494B RID: 18763 RVA: 0x000F55EB File Offset: 0x000F37EB
		public string Domain { get; set; }
	}
}
