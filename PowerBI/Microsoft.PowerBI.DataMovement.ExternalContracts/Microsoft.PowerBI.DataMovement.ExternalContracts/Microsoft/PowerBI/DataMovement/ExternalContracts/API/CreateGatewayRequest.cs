using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200002F RID: 47
	[DataContract]
	public sealed class CreateGatewayRequest
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002F10 File Offset: 0x00001110
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00002F18 File Offset: 0x00001118
		[MaxLength(200)]
		[DataMember(Name = "gatewayName", Order = 10)]
		public string GatewayName { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002F21 File Offset: 0x00001121
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002F29 File Offset: 0x00001129
		[DataMember(Name = "gatewayDescription", Order = 20)]
		public string GatewayDescription { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002F32 File Offset: 0x00001132
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002F3A File Offset: 0x0000113A
		[MaxLength(4000)]
		[DataMember(Name = "gatewayAnnotation", Order = 30)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002F43 File Offset: 0x00001143
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002F4B File Offset: 0x0000114B
		[Required]
		[DataMember(Name = "gatewayPublicKey", Order = 40)]
		public string GatewayPublicKey { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002F54 File Offset: 0x00001154
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002F5C File Offset: 0x0000115C
		[DataMember(Name = "gatewayVersion", Order = 50)]
		public string GatewayVersion { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002F65 File Offset: 0x00001165
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002F6D File Offset: 0x0000116D
		[DataMember(Name = "gatewaySBDetails", Order = 60)]
		public GatewayServiceBusDetails GatewayServiceBusDetails { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002F76 File Offset: 0x00001176
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002F7E File Offset: 0x0000117E
		[DataMember(Name = "gatewaySBDetailsSecondary", Order = 70)]
		public GatewayServiceBusDetails GatewayServiceBusDetailsSecondary { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002F87 File Offset: 0x00001187
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002F8F File Offset: 0x0000118F
		[DataMember(Name = "createSecondaryRelay", Order = 80)]
		public bool CreateSecondaryRelay { get; set; }
	}
}
