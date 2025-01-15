using System;
using System.Security.Claims;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000006 RID: 6
	public class AuthenticationTicket
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002129 File Offset: 0x00000329
		public AuthenticationTicket(ClaimsIdentity identity, AuthenticationProperties properties)
		{
			this.Identity = identity;
			this.Properties = properties ?? new AuthenticationProperties();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002150 File Offset: 0x00000350
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002159 File Offset: 0x00000359
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002161 File Offset: 0x00000361
		public AuthenticationProperties Properties { get; private set; }
	}
}
