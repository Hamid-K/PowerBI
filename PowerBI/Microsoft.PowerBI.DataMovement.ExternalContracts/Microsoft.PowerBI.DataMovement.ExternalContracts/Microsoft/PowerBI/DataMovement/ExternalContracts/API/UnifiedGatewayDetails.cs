using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200007E RID: 126
	[DataContract]
	public sealed class UnifiedGatewayDetails
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00004CA8 File Offset: 0x00002EA8
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00004CB0 File Offset: 0x00002EB0
		[DataMember(Name = "gatewayId", Order = 0)]
		public long GatewayId { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00004CB9 File Offset: 0x00002EB9
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00004CC1 File Offset: 0x00002EC1
		[DataMember(Name = "gatewayObjectId", Order = 5)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00004CCA File Offset: 0x00002ECA
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00004CD2 File Offset: 0x00002ED2
		[DataMember(Name = "gatewayName", Order = 10)]
		public string GatewayName { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00004CDB File Offset: 0x00002EDB
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00004CE3 File Offset: 0x00002EE3
		[DataMember(Name = "gatewayDescription", Order = 20)]
		public string GatewayDescription { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00004CEC File Offset: 0x00002EEC
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00004CF4 File Offset: 0x00002EF4
		[DataMember(Name = "gatewayAnnotation", Order = 30)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00004CFD File Offset: 0x00002EFD
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00004D05 File Offset: 0x00002F05
		[DataMember(Name = "gatewayPublicKey", Order = 40)]
		public string GatewayPublicKey { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00004D0E File Offset: 0x00002F0E
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00004D16 File Offset: 0x00002F16
		[DataMember(Name = "gatewayStatus", Order = 50)]
		public string GatewayStatus { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00004D1F File Offset: 0x00002F1F
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00004D27 File Offset: 0x00002F27
		[DataMember(Name = "gatewayKeyword", Order = 53)]
		public string GatewayKeyword { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00004D30 File Offset: 0x00002F30
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00004D38 File Offset: 0x00002F38
		[DataMember(Name = "gatewayMetadata", Order = 57)]
		public string GatewayMetadata { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00004D41 File Offset: 0x00002F41
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00004D49 File Offset: 0x00002F49
		[DataMember(Name = "Permission", Order = 60, EmitDefaultValue = false)]
		public UnifiedGatewayPrincipalEntryResponse Permission { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00004D52 File Offset: 0x00002F52
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00004D5A File Offset: 0x00002F5A
		[DataMember(Name = "tenantPermission", Order = 70, EmitDefaultValue = false)]
		public UnifiedGatewayPermission TenantPermission { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00004D63 File Offset: 0x00002F63
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00004D6B File Offset: 0x00002F6B
		[DataMember(Name = "type", Order = 140)]
		public string Type { get; set; }
	}
}
