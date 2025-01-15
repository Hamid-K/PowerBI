using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000013 RID: 19
	[DataContract]
	internal sealed class BucketItem
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003761 File Offset: 0x00001961
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003769 File Offset: 0x00001969
		[DataMember]
		public Formula Formula { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003772 File Offset: 0x00001972
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000377A File Offset: 0x0000197A
		[DataMember]
		public bool? ShowItemsWithNoData { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003783 File Offset: 0x00001983
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000378B File Offset: 0x0000198B
		[DataMember]
		public bool? IsDrilledItem { get; set; }
	}
}
