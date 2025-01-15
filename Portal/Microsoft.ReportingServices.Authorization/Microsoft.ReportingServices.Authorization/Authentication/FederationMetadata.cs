using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace Microsoft.ReportingServices.Authentication
{
	// Token: 0x02000007 RID: 7
	internal class FederationMetadata
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000210D File Offset: 0x0000030D
		public List<X509SecurityToken> SigningTokens { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002116 File Offset: 0x00000316
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000211E File Offset: 0x0000031E
		public string ValidIssuer { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002127 File Offset: 0x00000327
		public bool IsValid()
		{
			return this.SigningTokens != null && this.SigningTokens.Count > 0 && !string.IsNullOrWhiteSpace(this.ValidIssuer);
		}
	}
}
