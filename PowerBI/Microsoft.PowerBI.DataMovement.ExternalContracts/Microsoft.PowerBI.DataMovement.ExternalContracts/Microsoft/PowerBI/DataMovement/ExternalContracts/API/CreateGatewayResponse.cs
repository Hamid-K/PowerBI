using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000030 RID: 48
	[DataContract]
	public sealed class CreateGatewayResponse
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002FA0 File Offset: 0x000011A0
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00002FA8 File Offset: 0x000011A8
		[DataMember(Name = "gatewayId", Order = 0)]
		public long GatewayId { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002FB1 File Offset: 0x000011B1
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00002FB9 File Offset: 0x000011B9
		[DataMember(Name = "gatewayObjectId", Order = 5)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002FC2 File Offset: 0x000011C2
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002FCA File Offset: 0x000011CA
		[DataMember(Name = "gatewayName", Order = 6)]
		public string GatewayName { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002FD3 File Offset: 0x000011D3
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002FDB File Offset: 0x000011DB
		[DataMember(Name = "gatewayType", Order = 7)]
		public string GatewayType { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002FE4 File Offset: 0x000011E4
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00002FEC File Offset: 0x000011EC
		[DataMember(Name = "gatewaySBDetails", Order = 10)]
		public GatewayServiceBusDetails GatewayServiceBusDetails { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002FF5 File Offset: 0x000011F5
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002FFD File Offset: 0x000011FD
		[DataMember(Name = "gatewaySBDetailsSecondary", Order = 12)]
		public GatewayServiceBusDetails GatewayServiceBusDetailsSecondary { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003006 File Offset: 0x00001206
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000300E File Offset: 0x0000120E
		[DataMember(Name = "deprecatedServiceBusNamespace", Order = 15)]
		public string DeprecatedServiceBusNamespace { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003017 File Offset: 0x00001217
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000301F File Offset: 0x0000121F
		[DataMember(Name = "deprecatedServiceBusEndpoint", Order = 20)]
		public string DeprecatedServiceBusEndpoint { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003028 File Offset: 0x00001228
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00003030 File Offset: 0x00001230
		[DataMember(Name = "deprecatedServiceBusNamespaceSecondary", Order = 30)]
		public string DeprecatedServiceBusNamespaceSecondary { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003039 File Offset: 0x00001239
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00003041 File Offset: 0x00001241
		[DataMember(Name = "deprecatedServiceBusEndpointSecondary", Order = 40)]
		public string DeprecatedServiceBusEndpointSecondary { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000304A File Offset: 0x0000124A
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00003052 File Offset: 0x00001252
		[DataMember(Name = "integrationRuntimeAuthenticationKey", Order = 50)]
		public string IntegrationRuntimeAuthenticationKey { get; set; }
	}
}
