using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200007C RID: 124
	[DataContract]
	public sealed class UnifiedGatewayClusterMemberDetails
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00004B88 File Offset: 0x00002D88
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00004B90 File Offset: 0x00002D90
		[DataMember(Name = "gatewayId", Order = 10)]
		public long GatewayId { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00004B99 File Offset: 0x00002D99
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00004BA1 File Offset: 0x00002DA1
		[DataMember(Name = "gatewayObjectId", Order = 20)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00004BAA File Offset: 0x00002DAA
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00004BB2 File Offset: 0x00002DB2
		[DataMember(Name = "gatewayName", Order = 30)]
		public string GatewayName { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00004BBB File Offset: 0x00002DBB
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00004BC3 File Offset: 0x00002DC3
		[DataMember(Name = "gatewayAnnotation", Order = 40)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00004BCC File Offset: 0x00002DCC
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00004BD4 File Offset: 0x00002DD4
		[DataMember(Name = "gatewayStatus", Order = 50)]
		public string GatewayStatus { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00004BDD File Offset: 0x00002DDD
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00004BE5 File Offset: 0x00002DE5
		[DataMember(Name = "isAnchorGateway", Order = 60)]
		public bool IsAnchorGateway { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00004BEE File Offset: 0x00002DEE
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00004BF6 File Offset: 0x00002DF6
		[DataMember(Name = "gatewayClusterStatus", Order = 70)]
		public string GatewayClusterStatus { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00004BFF File Offset: 0x00002DFF
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00004C07 File Offset: 0x00002E07
		[DataMember(Name = "gatewayLoadBalancingSettings", Order = 80)]
		public string GatewayLoadBalancingSettings { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00004C10 File Offset: 0x00002E10
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00004C18 File Offset: 0x00002E18
		[DataMember(Name = "gatewayStaticCapabilities", Order = 90)]
		public GatewayStaticCapabilities GatewayStaticCapabilities { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00004C21 File Offset: 0x00002E21
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00004C29 File Offset: 0x00002E29
		[DataMember(Name = "gatewayPublicKey", Order = 100)]
		public string GatewayPublicKey { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00004C32 File Offset: 0x00002E32
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00004C3A File Offset: 0x00002E3A
		[DataMember(Name = "gatewayVersion", Order = 110)]
		public string GatewayVersion { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00004C43 File Offset: 0x00002E43
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00004C4B File Offset: 0x00002E4B
		[DataMember(Name = "gatewayVersionStatus", Order = 120)]
		public string GatewayVersionStatus { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00004C54 File Offset: 0x00002E54
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00004C5C File Offset: 0x00002E5C
		[DataMember(Name = "expiryDate", Order = 130)]
		public DateTime? ExpiryDate { get; set; }
	}
}
