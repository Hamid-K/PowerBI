using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000011 RID: 17
	[DataContract]
	internal sealed class Bucket
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000036FC File Offset: 0x000018FC
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00003704 File Offset: 0x00001904
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000370D File Offset: 0x0000190D
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003715 File Offset: 0x00001915
		[DataMember]
		public List<BucketItem> BucketItems { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000371E File Offset: 0x0000191E
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003726 File Offset: 0x00001926
		[DataMember]
		public List<BucketProperty> Properties { get; set; }
	}
}
