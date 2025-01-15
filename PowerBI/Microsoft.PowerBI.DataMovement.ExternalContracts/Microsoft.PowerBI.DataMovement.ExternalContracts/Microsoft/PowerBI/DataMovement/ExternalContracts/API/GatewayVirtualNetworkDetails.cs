using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200005B RID: 91
	[DataContract]
	public sealed class GatewayVirtualNetworkDetails
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00004402 File Offset: 0x00002602
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000440A File Offset: 0x0000260A
		[DataMember(Name = "gatewayVirtualNetworkResourceId", Order = 10)]
		public string GatewayVirtualNetworkResourceId { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00004413 File Offset: 0x00002613
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000441B File Offset: 0x0000261B
		[DataMember(Name = "gatewayVirtualNetworkServiceRegion", Order = 20)]
		public string GatewayVirtualNetworkServiceRegion { get; set; }
	}
}
