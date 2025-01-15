using System;
using Microsoft.Owin.Cors;

namespace Owin
{
	// Token: 0x02000006 RID: 6
	public static class CorsExtensions
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000023B1 File Offset: 0x000005B1
		public static IAppBuilder UseCors(this IAppBuilder app, CorsOptions options)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return app.Use(typeof(CorsMiddleware), new object[] { options });
		}
	}
}
