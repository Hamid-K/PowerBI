using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000086 RID: 134
	[DataContract]
	public class DmtsDetails
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00004F55 File Offset: 0x00003155
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x00004F5D File Offset: 0x0000315D
		[DataMember(Name = "gatewayServiceBusDetails", Order = 0)]
		public GatewayServiceBusDetails GatewayServiceBusDetails { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00004F66 File Offset: 0x00003166
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00004F6E File Offset: 0x0000316E
		[DataMember(Name = "dataSourceSystemDetails", Order = 10)]
		public DataSourceSystemDetails DataSourceSystemDetails { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00004F77 File Offset: 0x00003177
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00004F7F File Offset: 0x0000317F
		[DataMember(Name = "gatewayStaticCapabilities", Order = 20)]
		public GatewayStaticCapabilities GatewayStaticCapabilities { get; set; }
	}
}
