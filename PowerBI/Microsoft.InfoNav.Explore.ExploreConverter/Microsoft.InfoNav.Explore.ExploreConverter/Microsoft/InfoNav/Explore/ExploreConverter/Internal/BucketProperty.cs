using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000012 RID: 18
	[DataContract]
	internal sealed class BucketProperty
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003737 File Offset: 0x00001937
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000373F File Offset: 0x0000193F
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003748 File Offset: 0x00001948
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003750 File Offset: 0x00001950
		[DataMember]
		public bool Value { get; set; }
	}
}
