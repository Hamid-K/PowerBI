using System;
using System.Security.Claims;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x0200000C RID: 12
	public class CookieResponseSignedInContext : BaseContext<CookieAuthenticationOptions>
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002811 File Offset: 0x00000A11
		public CookieResponseSignedInContext(IOwinContext context, CookieAuthenticationOptions options, string authenticationType, ClaimsIdentity identity, AuthenticationProperties properties)
			: base(context, options)
		{
			this.AuthenticationType = authenticationType;
			this.Identity = identity;
			this.Properties = properties;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002832 File Offset: 0x00000A32
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000283A File Offset: 0x00000A3A
		public string AuthenticationType { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002843 File Offset: 0x00000A43
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000284B File Offset: 0x00000A4B
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000285C File Offset: 0x00000A5C
		public AuthenticationProperties Properties { get; private set; }
	}
}
