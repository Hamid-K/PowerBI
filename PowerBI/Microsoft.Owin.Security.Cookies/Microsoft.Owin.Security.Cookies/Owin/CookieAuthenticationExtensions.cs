using System;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Cookies;

namespace Owin
{
	// Token: 0x02000002 RID: 2
	public static class CookieAuthenticationExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static IAppBuilder UseCookieAuthentication(this IAppBuilder app, CookieAuthenticationOptions options)
		{
			return app.UseCookieAuthentication(options, PipelineStage.Authenticate);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public static IAppBuilder UseCookieAuthentication(this IAppBuilder app, CookieAuthenticationOptions options, PipelineStage stage)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			app.Use(typeof(CookieAuthenticationMiddleware), new object[] { app, options });
			app.UseStageMarker(stage);
			return app;
		}
	}
}
