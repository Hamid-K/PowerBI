using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000022 RID: 34
	[DataContract]
	internal sealed class PVProperty
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003E40 File Offset: 0x00002040
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003E48 File Offset: 0x00002048
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003E51 File Offset: 0x00002051
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003E59 File Offset: 0x00002059
		[DataMember]
		public CustomPVProperties Value { get; set; }
	}
}
