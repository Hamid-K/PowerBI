using System;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;

namespace Owin
{
	// Token: 0x02000003 RID: 3
	public static class DirectoryBrowserExtensions
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002096 File Offset: 0x00000296
		public static IAppBuilder UseDirectoryBrowser(this IAppBuilder builder)
		{
			return builder.UseDirectoryBrowser(new DirectoryBrowserOptions());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A3 File Offset: 0x000002A3
		public static IAppBuilder UseDirectoryBrowser(this IAppBuilder builder, string requestPath)
		{
			return builder.UseDirectoryBrowser(new DirectoryBrowserOptions
			{
				RequestPath = new PathString(requestPath)
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
		public static IAppBuilder UseDirectoryBrowser(this IAppBuilder builder, DirectoryBrowserOptions options)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			return builder.Use(new object[] { options });
		}
	}
}
