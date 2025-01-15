using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000088 RID: 136
	[DataContract]
	public class OnPremAccessDiscoveryRequest
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00004F90 File Offset: 0x00003190
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00004F98 File Offset: 0x00003198
		[Required]
		[DataMember(Name = "datasource", Order = 0)]
		public string Datasource { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00004FA1 File Offset: 0x000031A1
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00004FA9 File Offset: 0x000031A9
		[Required]
		[DataMember(Name = "initialCatalog", Order = 20)]
		public string InitialCatalog { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00004FB2 File Offset: 0x000031B2
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00004FBA File Offset: 0x000031BA
		[Required]
		[DataMember(Name = "provider", Order = 30)]
		public string Provider { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00004FC3 File Offset: 0x000031C3
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00004FCB File Offset: 0x000031CB
		[Required]
		[DataMember(Name = "gatewayStaticCapabilities", Order = 40)]
		public GatewayStaticCapabilities GatewayStaticCapabilities { get; set; }
	}
}
