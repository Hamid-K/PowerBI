using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000026 RID: 38
	[DataContract]
	internal sealed class PVTextStyle
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003EE8 File Offset: 0x000020E8
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003EF0 File Offset: 0x000020F0
		[DataMember]
		public string FontFamily { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003EF9 File Offset: 0x000020F9
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003F01 File Offset: 0x00002101
		[DataMember]
		public string FontSize { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003F0A File Offset: 0x0000210A
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003F12 File Offset: 0x00002112
		[DataMember]
		public string FontStyle { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003F1B File Offset: 0x0000211B
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003F23 File Offset: 0x00002123
		[DataMember]
		public string FontWeight { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003F2C File Offset: 0x0000212C
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003F34 File Offset: 0x00002134
		[DataMember]
		public string TextDecoration { get; set; }
	}
}
