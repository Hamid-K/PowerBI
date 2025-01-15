using System;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000005 RID: 5
	public class CookieAuthenticationMiddleware : AuthenticationMiddleware<CookieAuthenticationOptions>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002374 File Offset: 0x00000574
		public CookieAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, CookieAuthenticationOptions options)
			: base(next, options)
		{
			if (base.Options.Provider == null)
			{
				base.Options.Provider = new CookieAuthenticationProvider();
			}
			if (string.IsNullOrEmpty(base.Options.CookieName))
			{
				base.Options.CookieName = ".AspNet." + base.Options.AuthenticationType;
			}
			this._logger = app.CreateLogger<CookieAuthenticationMiddleware>();
			if (base.Options.TicketDataFormat == null)
			{
				IDataProtector dataProtector = app.CreateDataProtector(new string[]
				{
					typeof(CookieAuthenticationMiddleware).FullName,
					base.Options.AuthenticationType,
					"v1"
				});
				base.Options.TicketDataFormat = new TicketDataFormat(dataProtector);
			}
			if (base.Options.CookieManager == null)
			{
				base.Options.CookieManager = new ChunkingCookieManager();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002454 File Offset: 0x00000654
		protected override AuthenticationHandler<CookieAuthenticationOptions> CreateHandler()
		{
			return new CookieAuthenticationHandler(this._logger);
		}

		// Token: 0x04000011 RID: 17
		private readonly ILogger _logger;
	}
}
