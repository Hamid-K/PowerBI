using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000009 RID: 9
	public class CookieApplyRedirectContext : BaseContext<CookieAuthenticationOptions>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000025EF File Offset: 0x000007EF
		public CookieApplyRedirectContext(IOwinContext context, CookieAuthenticationOptions options, string redirectUri)
			: base(context, options)
		{
			this.RedirectUri = redirectUri;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002608 File Offset: 0x00000808
		public string RedirectUri { get; set; }
	}
}
