using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200007B RID: 123
	[DataContract]
	public sealed class UnifiedGatewayClusterDetails
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00004A2C File Offset: 0x00002C2C
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00004A34 File Offset: 0x00002C34
		[DataMember(Name = "gatewayId", Order = 0)]
		public long GatewayId { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00004A3D File Offset: 0x00002C3D
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00004A45 File Offset: 0x00002C45
		[DataMember(Name = "objectId", Order = 10)]
		public Guid ObjectId { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00004A4E File Offset: 0x00002C4E
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00004A56 File Offset: 0x00002C56
		[DataMember(Name = "name", Order = 20)]
		public string Name { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00004A5F File Offset: 0x00002C5F
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00004A67 File Offset: 0x00002C67
		[DataMember(Name = "description", Order = 30)]
		public string Description { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00004A70 File Offset: 0x00002C70
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00004A78 File Offset: 0x00002C78
		[DataMember(Name = "publicKey", Order = 40)]
		public string PublicKey { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00004A81 File Offset: 0x00002C81
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00004A89 File Offset: 0x00002C89
		[DataMember(Name = "keyword", Order = 50)]
		public string Keyword { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00004A92 File Offset: 0x00002C92
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00004A9A File Offset: 0x00002C9A
		[DataMember(Name = "metadata", Order = 60)]
		public string Metadata { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00004AA3 File Offset: 0x00002CA3
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00004AAB File Offset: 0x00002CAB
		[DataMember(Name = "permission", Order = 70, EmitDefaultValue = false)]
		public UnifiedGatewayPrincipalEntryResponse Permission { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00004AB4 File Offset: 0x00002CB4
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00004ABC File Offset: 0x00002CBC
		[DataMember(Name = "tenantPermission", Order = 80, EmitDefaultValue = false)]
		public UnifiedGatewayPermission TenantPermission { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00004AC5 File Offset: 0x00002CC5
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00004ACD File Offset: 0x00002CCD
		[DataMember(Name = "gateways", Order = 90, EmitDefaultValue = false)]
		public IList<UnifiedGatewayClusterMemberDetails> Gateways { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00004AD6 File Offset: 0x00002CD6
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00004ADE File Offset: 0x00002CDE
		[DataMember(Name = "dataSources", Order = 95, EmitDefaultValue = false)]
		public IList<DataSourceNoCredentials> DataSources { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00004AE7 File Offset: 0x00002CE7
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00004AEF File Offset: 0x00002CEF
		[DataMember(Name = "loadBalancingSettings", Order = 100)]
		public string LoadBalancingSettings { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00004AF8 File Offset: 0x00002CF8
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00004B00 File Offset: 0x00002D00
		[DataMember(Name = "annotation", Order = 110)]
		public string Annotation { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00004B09 File Offset: 0x00002D09
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00004B11 File Offset: 0x00002D11
		[DataMember(Name = "versionStatus", Order = 120)]
		public string VersionStatus { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00004B1A File Offset: 0x00002D1A
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00004B22 File Offset: 0x00002D22
		[DataMember(Name = "expiryDate", Order = 130)]
		public DateTime? ExpiryDate { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00004B2B File Offset: 0x00002D2B
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00004B33 File Offset: 0x00002D33
		[DataMember(Name = "type", Order = 140)]
		public string Type { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00004B3C File Offset: 0x00002D3C
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00004B44 File Offset: 0x00002D44
		[DataMember(Name = "options", Order = 150)]
		public IDictionary<string, bool> AllowableOptions { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00004B4D File Offset: 0x00002D4D
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00004B55 File Offset: 0x00002D55
		[DataMember(Name = "users", Order = 160, EmitDefaultValue = false)]
		public IEnumerable<UnifiedGatewayPrincipalEntryResponse> Users { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00004B5E File Offset: 0x00002D5E
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00004B66 File Offset: 0x00002D66
		[DataMember(Name = "staticCapabilities", Order = 170)]
		public GatewayStaticCapabilities StaticCapabilities { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00004B6F File Offset: 0x00002D6F
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00004B77 File Offset: 0x00002D77
		[DataMember(Name = "userDatasourceLimitStatus", Order = 180)]
		public UserDatasourceLimitStatus UserDatasourceLimitStatus { get; set; }
	}
}
