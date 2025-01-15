using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000174 RID: 372
	public class SecurityTokenDescriptor
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00040B84 File Offset: 0x0003ED84
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00040B8C File Offset: 0x0003ED8C
		public string Audience { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00040B95 File Offset: 0x0003ED95
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x00040B9D File Offset: 0x0003ED9D
		public string CompressionAlgorithm { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00040BA6 File Offset: 0x0003EDA6
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x00040BAE File Offset: 0x0003EDAE
		public EncryptingCredentials EncryptingCredentials { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00040BB7 File Offset: 0x0003EDB7
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00040BBF File Offset: 0x0003EDBF
		public DateTime? Expires { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00040BC8 File Offset: 0x0003EDC8
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00040BD0 File Offset: 0x0003EDD0
		public string Issuer { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00040BD9 File Offset: 0x0003EDD9
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00040BE1 File Offset: 0x0003EDE1
		public DateTime? IssuedAt { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00040BEA File Offset: 0x0003EDEA
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00040BF2 File Offset: 0x0003EDF2
		public DateTime? NotBefore { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00040BFB File Offset: 0x0003EDFB
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00040C03 File Offset: 0x0003EE03
		public string TokenType { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00040C0C File Offset: 0x0003EE0C
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00040C14 File Offset: 0x0003EE14
		public IDictionary<string, object> Claims { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00040C1D File Offset: 0x0003EE1D
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00040C25 File Offset: 0x0003EE25
		public IDictionary<string, object> AdditionalHeaderClaims { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00040C2E File Offset: 0x0003EE2E
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00040C36 File Offset: 0x0003EE36
		public IDictionary<string, object> AdditionalInnerHeaderClaims { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00040C3F File Offset: 0x0003EE3F
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00040C47 File Offset: 0x0003EE47
		public SigningCredentials SigningCredentials { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00040C50 File Offset: 0x0003EE50
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00040C58 File Offset: 0x0003EE58
		public ClaimsIdentity Subject { get; set; }
	}
}
