using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000057 RID: 87
	[DataContract]
	public class GatewaySystemDetailsBase
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000042D1 File Offset: 0x000024D1
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000042D9 File Offset: 0x000024D9
		[DataMember(Name = "gatewayId", Order = 0)]
		public long GatewayId { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000042E2 File Offset: 0x000024E2
		// (set) Token: 0x0600029B RID: 667 RVA: 0x000042EA File Offset: 0x000024EA
		[DataMember(Name = "gatewayName", Order = 5)]
		public string GatewayName { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000042F3 File Offset: 0x000024F3
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000042FB File Offset: 0x000024FB
		[DataMember(Name = "gatewaySBDetails", Order = 10)]
		public GatewayServiceBusDetails GatewayServiceBusDetails { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00004304 File Offset: 0x00002504
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000430C File Offset: 0x0000250C
		[DataMember(Name = "gatewaySBDetailsSecondary", Order = 15)]
		public GatewayServiceBusDetails GatewayServiceBusDetailsSecondary { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00004315 File Offset: 0x00002515
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000431D File Offset: 0x0000251D
		[DataMember(Name = "gatewayType", Order = 20)]
		public GatewayType GatewayType { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00004326 File Offset: 0x00002526
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000432E File Offset: 0x0000252E
		[DataMember(Name = "gatewayPublicKey", Order = 30)]
		public string GatewayPublicKey { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00004337 File Offset: 0x00002537
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000433F File Offset: 0x0000253F
		[DataMember(Name = "gatewayAnnotation", Order = 35)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00004348 File Offset: 0x00002548
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00004350 File Offset: 0x00002550
		[DataMember(Name = "gatewayStaticCapabilities", Order = 40)]
		public GatewayStaticCapabilities GatewayStaticCapabilities { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00004359 File Offset: 0x00002559
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00004361 File Offset: 0x00002561
		[DataMember(Name = "gatewayObjectId", Order = 50)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000436A File Offset: 0x0000256A
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00004372 File Offset: 0x00002572
		[DataMember(Name = "gatewayAllowableOptions", Order = 60)]
		public GatewayAllowableOptions? GatewayAllowableOptions { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000437B File Offset: 0x0000257B
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00004383 File Offset: 0x00002583
		[DataMember(Name = "gatewayVersion", Order = 70)]
		public string GatewayVersion { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000438C File Offset: 0x0000258C
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00004394 File Offset: 0x00002594
		[DataMember(Name = "versionUpdateTime", Order = 80)]
		public DateTime? VersionUpdateTime { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000439D File Offset: 0x0000259D
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000043A5 File Offset: 0x000025A5
		[DataMember(Name = "gatewayVirtualNetworkDetails", Order = 90)]
		public GatewayVirtualNetworkDetails GatewayVirtualNetworkDetails { get; set; }
	}
}
