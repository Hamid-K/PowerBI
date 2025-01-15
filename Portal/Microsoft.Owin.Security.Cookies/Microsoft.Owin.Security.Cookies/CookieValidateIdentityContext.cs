using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x0200000F RID: 15
	public class CookieValidateIdentityContext : BaseContext<CookieAuthenticationOptions>
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000028F4 File Offset: 0x00000AF4
		public CookieValidateIdentityContext(IOwinContext context, AuthenticationTicket ticket, CookieAuthenticationOptions options)
			: base(context, options)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this.Identity = ticket.Identity;
			this.Properties = ticket.Properties;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002924 File Offset: 0x00000B24
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000292C File Offset: 0x00000B2C
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002935 File Offset: 0x00000B35
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000293D File Offset: 0x00000B3D
		public AuthenticationProperties Properties { get; private set; }

		// Token: 0x06000067 RID: 103 RVA: 0x00002946 File Offset: 0x00000B46
		public void ReplaceIdentity(IIdentity identity)
		{
			this.Identity = new ClaimsIdentity(identity);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002954 File Offset: 0x00000B54
		public void RejectIdentity()
		{
			this.Identity = null;
		}
	}
}
