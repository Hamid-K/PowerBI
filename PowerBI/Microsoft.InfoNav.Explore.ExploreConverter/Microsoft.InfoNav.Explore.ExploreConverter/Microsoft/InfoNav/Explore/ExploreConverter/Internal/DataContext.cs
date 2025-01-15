using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000015 RID: 21
	[DataContract]
	internal sealed class DataContext
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000037E8 File Offset: 0x000019E8
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000037F0 File Offset: 0x000019F0
		[DataMember]
		public List<Bucket> Buckets { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000037F9 File Offset: 0x000019F9
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003801 File Offset: 0x00001A01
		[DataMember]
		public Formula Formula { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000380A File Offset: 0x00001A0A
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003812 File Offset: 0x00001A12
		[DataMember]
		public string Type { get; set; }
	}
}
