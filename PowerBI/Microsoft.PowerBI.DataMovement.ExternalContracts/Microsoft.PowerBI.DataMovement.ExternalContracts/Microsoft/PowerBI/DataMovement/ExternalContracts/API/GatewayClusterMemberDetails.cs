using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200004F RID: 79
	[DataContract]
	public sealed class GatewayClusterMemberDetails
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00004092 File Offset: 0x00002292
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000409A File Offset: 0x0000229A
		[DataMember(Name = "gatewayId", Order = 10)]
		public long GatewayId { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000040A3 File Offset: 0x000022A3
		// (set) Token: 0x06000257 RID: 599 RVA: 0x000040AB File Offset: 0x000022AB
		[DataMember(Name = "gatewayObjectId", Order = 20)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000040B4 File Offset: 0x000022B4
		// (set) Token: 0x06000259 RID: 601 RVA: 0x000040BC File Offset: 0x000022BC
		[DataMember(Name = "gatewayName", Order = 30)]
		public string GatewayName { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000040C5 File Offset: 0x000022C5
		// (set) Token: 0x0600025B RID: 603 RVA: 0x000040CD File Offset: 0x000022CD
		[DataMember(Name = "gatewayAnnotation", Order = 40)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000040D6 File Offset: 0x000022D6
		// (set) Token: 0x0600025D RID: 605 RVA: 0x000040DE File Offset: 0x000022DE
		[DataMember(Name = "gatewayStatus", Order = 50)]
		public GatewayStatus GatewayStatus { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000040E7 File Offset: 0x000022E7
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000040EF File Offset: 0x000022EF
		[DataMember(Name = "isAnchorGateway", Order = 60)]
		public bool IsAnchorGateway { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000040F8 File Offset: 0x000022F8
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00004100 File Offset: 0x00002300
		[DataMember(Name = "gatewayClusterStatus", Order = 70)]
		public ClusterMemberStatus GatewayClusterStatus { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00004109 File Offset: 0x00002309
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00004111 File Offset: 0x00002311
		[DataMember(Name = "gatewayLoadBalancingSettings", Order = 80)]
		public string GatewayLoadBalancingSettings { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000411A File Offset: 0x0000231A
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00004122 File Offset: 0x00002322
		[DataMember(Name = "gatewayPublicKey", Order = 90)]
		public string GatewayPublicKey { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000412B File Offset: 0x0000232B
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00004133 File Offset: 0x00002333
		[DataMember(Name = "gatewayVersionStatus", Order = 100)]
		public string GatewayVersionStatus { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000413C File Offset: 0x0000233C
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00004144 File Offset: 0x00002344
		[DataMember(Name = "expiryDate", Order = 110)]
		public DateTime? ExpiryDate { get; set; }
	}
}
