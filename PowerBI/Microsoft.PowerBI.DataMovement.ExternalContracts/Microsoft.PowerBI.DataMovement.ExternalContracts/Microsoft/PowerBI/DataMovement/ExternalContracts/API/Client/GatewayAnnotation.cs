using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API.Client
{
	// Token: 0x0200008D RID: 141
	[DataContract]
	public sealed class GatewayAnnotation
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000051FA File Offset: 0x000033FA
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00005202 File Offset: 0x00003402
		[DataMember(Name = "gatewayContactInformation", Order = 10)]
		public string[] GatewayContactInformation { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000520B File Offset: 0x0000340B
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00005213 File Offset: 0x00003413
		[DataMember(Name = "gatewayVersion", Order = 20)]
		public string GatewayVersion { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000521C File Offset: 0x0000341C
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00005224 File Offset: 0x00003424
		[DataMember(Name = "gatewayWitnessString", Order = 30)]
		public string GatewayWitnessString { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000522D File Offset: 0x0000342D
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00005235 File Offset: 0x00003435
		[DataMember(Name = "gatewayMachine", Order = 40)]
		public string GatewayMachine { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000523E File Offset: 0x0000343E
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00005246 File Offset: 0x00003446
		[DataMember(Name = "gatewaySalt", Order = 50)]
		public string GatewaySalt { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000524F File Offset: 0x0000344F
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00005257 File Offset: 0x00003457
		[DataMember(Name = "gatewayWitnessStringLegacy", Order = 60)]
		public string GatewayWitnessStringLegacy { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00005260 File Offset: 0x00003460
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00005268 File Offset: 0x00003468
		[DataMember(Name = "gatewaySaltLegacy", Order = 70)]
		public string GatewaySaltLegacy { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00005271 File Offset: 0x00003471
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00005279 File Offset: 0x00003479
		[DataMember(Name = "gatewayDepartment", Order = 80)]
		public string GatewayDepartment { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00005282 File Offset: 0x00003482
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000528A File Offset: 0x0000348A
		[DataMember(Name = "gatewayVirtualNetworkSubnetId", Order = 90)]
		public string GatewayVirtualNetworkSubnetId { get; set; }
	}
}
