using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000024 RID: 36
	[DataContract]
	internal sealed class PVResourceEntry
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003E94 File Offset: 0x00002094
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003E9C File Offset: 0x0000209C
		[DataMember]
		public string ResourceId { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003EA5 File Offset: 0x000020A5
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003EAD File Offset: 0x000020AD
		[DataMember]
		public string ImageBytes { get; set; }
	}
}
