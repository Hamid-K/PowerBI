using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000048 RID: 72
	[DataContract]
	public sealed class DiscoverUnifiedGatewayFullDetailsResponse
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00003C6E File Offset: 0x00001E6E
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00003C76 File Offset: 0x00001E76
		[DataMember(Name = "gatewayId", Order = 0)]
		public long GatewayId { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00003C7F File Offset: 0x00001E7F
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00003C87 File Offset: 0x00001E87
		[DataMember(Name = "gatewayType", Order = 3)]
		public GatewayType GatewayType { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00003C90 File Offset: 0x00001E90
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00003C98 File Offset: 0x00001E98
		[DataMember(Name = "gatewayObjectId", Order = 5)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00003CA1 File Offset: 0x00001EA1
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00003CA9 File Offset: 0x00001EA9
		[DataMember(Name = "gatewayName", Order = 10)]
		public string GatewayName { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00003CB2 File Offset: 0x00001EB2
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00003CBA File Offset: 0x00001EBA
		[DataMember(Name = "gatewayDescription", Order = 20)]
		public string GatewayDescription { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00003CC3 File Offset: 0x00001EC3
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00003CCB File Offset: 0x00001ECB
		[DataMember(Name = "gatewayAnnotation", Order = 30)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00003CD4 File Offset: 0x00001ED4
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00003CDC File Offset: 0x00001EDC
		[DataMember(Name = "gatewayPublicKey", Order = 40)]
		public string GatewayPublicKey { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00003CE5 File Offset: 0x00001EE5
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00003CED File Offset: 0x00001EED
		[DataMember(Name = "gatewaySBDetails", Order = 60)]
		public GatewayServiceBusDetails GatewayServiceBusDetails { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00003CF6 File Offset: 0x00001EF6
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00003CFE File Offset: 0x00001EFE
		[DataMember(Name = "gatewaySBDetailsSecondary", Order = 65)]
		public GatewayServiceBusDetails GatewayServiceBusDetailsSecondary { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00003D07 File Offset: 0x00001F07
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00003D0F File Offset: 0x00001F0F
		[DataMember(Name = "clusterSystemDetails", Order = 70)]
		public ClusterSystemDetails ClusterSystemDetails { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00003D18 File Offset: 0x00001F18
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00003D20 File Offset: 0x00001F20
		[DataMember(Name = "gatewayStaticCapabilities", Order = 80)]
		public GatewayStaticCapabilities GatewayStaticCapabilities { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00003D29 File Offset: 0x00001F29
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00003D31 File Offset: 0x00001F31
		[DataMember(Name = "gatewayVersion", Order = 90)]
		public string GatewayVersion { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00003D3A File Offset: 0x00001F3A
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00003D42 File Offset: 0x00001F42
		[DataMember(Name = "versionUpdateTime", Order = 100)]
		public DateTime? VersionUpdateTime { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00003D4B File Offset: 0x00001F4B
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00003D53 File Offset: 0x00001F53
		[DataMember(Name = "gatewayVirtualNetworkDetails", Order = 110)]
		public GatewayVirtualNetworkDetails GatewayVirtualNetworkDetails { get; set; }
	}
}
