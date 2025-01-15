using System;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;

namespace Owin
{
	// Token: 0x02000004 RID: 4
	public static class FileServerExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020DC File Offset: 0x000002DC
		public static IAppBuilder UseFileServer(this IAppBuilder builder)
		{
			return builder.UseFileServer(new FileServerOptions());
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E9 File Offset: 0x000002E9
		public static IAppBuilder UseFileServer(this IAppBuilder builder, bool enableDirectoryBrowsing)
		{
			return builder.UseFileServer(new FileServerOptions
			{
				EnableDirectoryBrowsing = enableDirectoryBrowsing
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020FD File Offset: 0x000002FD
		public static IAppBuilder UseFileServer(this IAppBuilder builder, string requestPath)
		{
			return builder.UseFileServer(new FileServerOptions
			{
				RequestPath = new PathString(requestPath)
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		public static IAppBuilder UseFileServer(this IAppBuilder builder, FileServerOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (options.EnableDefaultFiles)
			{
				builder = builder.UseDefaultFiles(options.DefaultFilesOptions);
			}
			if (options.EnableDirectoryBrowsing)
			{
				builder = builder.UseDirectoryBrowser(options.DirectoryBrowserOptions);
			}
			return builder.UseSendFileFallback().UseStaticFiles(options.StaticFileOptions);
		}
	}
}
