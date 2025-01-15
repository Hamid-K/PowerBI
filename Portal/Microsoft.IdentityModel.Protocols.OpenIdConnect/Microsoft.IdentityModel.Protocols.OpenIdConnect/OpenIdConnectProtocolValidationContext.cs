using System;
using System.IdentityModel.Tokens.Jwt;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x0200000F RID: 15
	public class OpenIdConnectProtocolValidationContext
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003118 File Offset: 0x00001318
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003120 File Offset: 0x00001320
		public string ClientId { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003129 File Offset: 0x00001329
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003131 File Offset: 0x00001331
		public string Nonce { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000313A File Offset: 0x0000133A
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003142 File Offset: 0x00001342
		public OpenIdConnectMessage ProtocolMessage { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000314B File Offset: 0x0000134B
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003153 File Offset: 0x00001353
		public string State { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00003164 File Offset: 0x00001364
		public string UserInfoEndpointResponse { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000316D File Offset: 0x0000136D
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00003175 File Offset: 0x00001375
		public JwtSecurityToken ValidatedIdToken { get; set; }
	}
}
