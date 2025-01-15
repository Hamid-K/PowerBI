using System;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000022 RID: 34
	public class AuthenticationResponseGrant
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00004903 File Offset: 0x00002B03
		public AuthenticationResponseGrant(ClaimsIdentity identity, AuthenticationProperties properties)
		{
			this.Principal = new ClaimsPrincipal(identity);
			this.Identity = identity;
			this.Properties = properties;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004925 File Offset: 0x00002B25
		public AuthenticationResponseGrant(ClaimsPrincipal principal, AuthenticationProperties properties)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			this.Principal = principal;
			this.Identity = principal.Identities.FirstOrDefault<ClaimsIdentity>();
			this.Properties = properties;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000495A File Offset: 0x00002B5A
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00004962 File Offset: 0x00002B62
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000496B File Offset: 0x00002B6B
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00004973 File Offset: 0x00002B73
		public ClaimsPrincipal Principal { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000497C File Offset: 0x00002B7C
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00004984 File Offset: 0x00002B84
		public AuthenticationProperties Properties { get; private set; }
	}
}
