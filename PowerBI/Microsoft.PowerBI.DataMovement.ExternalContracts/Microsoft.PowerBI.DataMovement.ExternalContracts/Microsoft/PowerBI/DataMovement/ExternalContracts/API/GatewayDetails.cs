using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000050 RID: 80
	[DataContract]
	public sealed class GatewayDetails
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00004155 File Offset: 0x00002355
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000415D File Offset: 0x0000235D
		[DataMember(Name = "gatewayId", Order = 0)]
		public long GatewayId { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00004166 File Offset: 0x00002366
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000416E File Offset: 0x0000236E
		[DataMember(Name = "gatewayObjectId", Order = 1)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00004177 File Offset: 0x00002377
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000417F File Offset: 0x0000237F
		[DataMember(Name = "gatewayName", Order = 10)]
		public string GatewayName { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00004188 File Offset: 0x00002388
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00004190 File Offset: 0x00002390
		[DataMember(Name = "gatewayDescription", Order = 20)]
		public string GatewayDescription { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00004199 File Offset: 0x00002399
		// (set) Token: 0x06000274 RID: 628 RVA: 0x000041A1 File Offset: 0x000023A1
		[DataMember(Name = "gatewayAnnotation", Order = 30)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000041AA File Offset: 0x000023AA
		// (set) Token: 0x06000276 RID: 630 RVA: 0x000041B2 File Offset: 0x000023B2
		[DataMember(Name = "gatewayPublicKey", Order = 40)]
		public string GatewayPublicKey { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000041BB File Offset: 0x000023BB
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000041C3 File Offset: 0x000023C3
		[DataMember(Name = "gatewayStatus", Order = 50)]
		public GatewayStatus GatewayStatus { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000041CC File Offset: 0x000023CC
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000041D4 File Offset: 0x000023D4
		[DataMember(Name = "gatewayType", Order = 60)]
		public GatewayType GatewayType { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000041DD File Offset: 0x000023DD
		// (set) Token: 0x0600027C RID: 636 RVA: 0x000041E5 File Offset: 0x000023E5
		[DataMember(Name = "allowCloudDatasourceRefreshThroughGateway", Order = 70)]
		public bool AllowCloudDatasourceRefreshThroughGateway { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000041EE File Offset: 0x000023EE
		// (set) Token: 0x0600027E RID: 638 RVA: 0x000041F6 File Offset: 0x000023F6
		[DataMember(Name = "allowableOptions", Order = 80)]
		public GatewayAllowableOptions AllowableOptions { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000041FF File Offset: 0x000023FF
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00004207 File Offset: 0x00002407
		[DataMember(Name = "versionStatus", Order = 90)]
		public string VersionStatus { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00004210 File Offset: 0x00002410
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00004218 File Offset: 0x00002418
		[DataMember(Name = "expiryDate", Order = 100)]
		public DateTime? ExpiryDate { get; set; }
	}
}
