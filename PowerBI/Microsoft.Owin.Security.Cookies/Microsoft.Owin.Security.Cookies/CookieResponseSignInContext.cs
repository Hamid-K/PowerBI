using System;
using System.Security.Claims;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x0200000D RID: 13
	public class CookieResponseSignInContext : BaseContext<CookieAuthenticationOptions>
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002865 File Offset: 0x00000A65
		public CookieResponseSignInContext(IOwinContext context, CookieAuthenticationOptions options, string authenticationType, ClaimsIdentity identity, AuthenticationProperties properties, CookieOptions cookieOptions)
			: base(context, options)
		{
			this.AuthenticationType = authenticationType;
			this.Identity = identity;
			this.Properties = properties;
			this.CookieOptions = cookieOptions;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000288E File Offset: 0x00000A8E
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002896 File Offset: 0x00000A96
		public string AuthenticationType { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000289F File Offset: 0x00000A9F
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000028A7 File Offset: 0x00000AA7
		public ClaimsIdentity Identity { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000028B8 File Offset: 0x00000AB8
		public AuthenticationProperties Properties { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000028C1 File Offset: 0x00000AC1
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000028C9 File Offset: 0x00000AC9
		public CookieOptions CookieOptions { get; set; }
	}
}
