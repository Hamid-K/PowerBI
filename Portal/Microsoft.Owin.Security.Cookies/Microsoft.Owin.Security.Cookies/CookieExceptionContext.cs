using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x0200000B RID: 11
	public class CookieExceptionContext : BaseContext<CookieAuthenticationOptions>
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000027A5 File Offset: 0x000009A5
		public CookieExceptionContext(IOwinContext context, CookieAuthenticationOptions options, CookieExceptionContext.ExceptionLocation location, Exception exception, AuthenticationTicket ticket)
			: base(context, options)
		{
			this.Location = location;
			this.Exception = exception;
			this.Rethrow = true;
			this.Ticket = ticket;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000027CD File Offset: 0x000009CD
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000027D5 File Offset: 0x000009D5
		public CookieExceptionContext.ExceptionLocation Location { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000027DE File Offset: 0x000009DE
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000027E6 File Offset: 0x000009E6
		public Exception Exception { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000027EF File Offset: 0x000009EF
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000027F7 File Offset: 0x000009F7
		public bool Rethrow { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002808 File Offset: 0x00000A08
		public AuthenticationTicket Ticket { get; set; }

		// Token: 0x02000016 RID: 22
		public enum ExceptionLocation
		{
			// Token: 0x04000059 RID: 89
			AuthenticateAsync,
			// Token: 0x0400005A RID: 90
			ApplyResponseGrant,
			// Token: 0x0400005B RID: 91
			ApplyResponseChallenge
		}
	}
}
