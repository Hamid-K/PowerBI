using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000051 RID: 81
	[DataContract]
	public sealed class GatewayReleaseLabel
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00004229 File Offset: 0x00002429
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00004231 File Offset: 0x00002431
		[DataMember(Name = "version")]
		public string Version { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000423A File Offset: 0x0000243A
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00004242 File Offset: 0x00002442
		[DataMember(Name = "label")]
		public string Label { get; set; }
	}
}
