using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x0200000E RID: 14
	public class CookieResponseSignOutContext : BaseContext<CookieAuthenticationOptions>
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000028D2 File Offset: 0x00000AD2
		public CookieResponseSignOutContext(IOwinContext context, CookieAuthenticationOptions options, CookieOptions cookieOptions)
			: base(context, options)
		{
			this.CookieOptions = cookieOptions;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000028E3 File Offset: 0x00000AE3
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000028EB File Offset: 0x00000AEB
		public CookieOptions CookieOptions { get; set; }
	}
}
