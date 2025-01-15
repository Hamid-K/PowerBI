using System;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;

namespace Owin
{
	// Token: 0x02000005 RID: 5
	public static class StaticFileExtensions
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002170 File Offset: 0x00000370
		public static IAppBuilder UseStaticFiles(this IAppBuilder builder)
		{
			return builder.UseStaticFiles(new StaticFileOptions());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000217D File Offset: 0x0000037D
		public static IAppBuilder UseStaticFiles(this IAppBuilder builder, string requestPath)
		{
			return builder.UseStaticFiles(new StaticFileOptions
			{
				RequestPath = new PathString(requestPath)
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002196 File Offset: 0x00000396
		public static IAppBuilder UseStaticFiles(this IAppBuilder builder, StaticFileOptions options)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			return builder.Use(new object[] { options });
		}
	}
}
