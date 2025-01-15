using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000023 RID: 35
	[DataContract]
	internal sealed class PVPropertyValue
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003E6A File Offset: 0x0000206A
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003E72 File Offset: 0x00002072
		[DataMember]
		public string Direction { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003E7B File Offset: 0x0000207B
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003E83 File Offset: 0x00002083
		[DataMember]
		public Formula Formula { get; set; }
	}
}
