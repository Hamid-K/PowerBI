using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000021 RID: 33
	[DataContract]
	internal sealed class PVPosition
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003E16 File Offset: 0x00002016
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003E1E File Offset: 0x0000201E
		[DataMember]
		public decimal X { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003E27 File Offset: 0x00002027
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003E2F File Offset: 0x0000202F
		[DataMember]
		public decimal Y { get; set; }
	}
}
