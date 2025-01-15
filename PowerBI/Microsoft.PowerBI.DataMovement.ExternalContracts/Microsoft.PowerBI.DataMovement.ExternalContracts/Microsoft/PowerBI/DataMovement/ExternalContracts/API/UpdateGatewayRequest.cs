using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000083 RID: 131
	[DataContract]
	public sealed class UpdateGatewayRequest
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00004E79 File Offset: 0x00003079
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00004E81 File Offset: 0x00003081
		[Required]
		[MaxLength(200)]
		[DataMember(Name = "gatewayName", Order = 10)]
		public string GatewayName { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00004E8A File Offset: 0x0000308A
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00004E92 File Offset: 0x00003092
		[DataMember(Name = "gatewayDescription", Order = 20)]
		public string GatewayDescription { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00004E9B File Offset: 0x0000309B
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00004EA3 File Offset: 0x000030A3
		[MaxLength(4000)]
		[DataMember(Name = "gatewayAnnotation", Order = 30)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00004EAC File Offset: 0x000030AC
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00004EB4 File Offset: 0x000030B4
		[DataMember(Name = "allowCloudDatasourceRefreshThroughGateway", Order = 40, IsRequired = false)]
		public bool AllowCloudDatasourceRefreshThroughGateway { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00004EBD File Offset: 0x000030BD
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00004EC5 File Offset: 0x000030C5
		[DataMember(Name = "allowableOptions", Order = 50, IsRequired = false)]
		public GatewayAllowableOptions AllowableOptions { get; set; }
	}
}
