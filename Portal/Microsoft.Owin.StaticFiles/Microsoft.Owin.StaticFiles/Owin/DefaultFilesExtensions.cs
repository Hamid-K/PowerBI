using System;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;

namespace Owin
{
	// Token: 0x02000002 RID: 2
	public static class DefaultFilesExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static IAppBuilder UseDefaultFiles(this IAppBuilder builder)
		{
			return builder.UseDefaultFiles(new DefaultFilesOptions());
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
		public static IAppBuilder UseDefaultFiles(this IAppBuilder builder, string requestPath)
		{
			return builder.UseDefaultFiles(new DefaultFilesOptions
			{
				RequestPath = new PathString(requestPath)
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002076 File Offset: 0x00000276
		public static IAppBuilder UseDefaultFiles(this IAppBuilder builder, DefaultFilesOptions options)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			return builder.Use(new object[] { options });
		}
	}
}
