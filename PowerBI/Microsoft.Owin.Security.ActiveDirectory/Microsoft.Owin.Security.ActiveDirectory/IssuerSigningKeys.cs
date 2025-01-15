using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Owin.Security.ActiveDirectory
{
	// Token: 0x02000003 RID: 3
	internal class IssuerSigningKeys
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002112 File Offset: 0x00000312
		public string Issuer { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002123 File Offset: 0x00000323
		public IEnumerable<SecurityKey> Keys { get; set; }
	}
}
