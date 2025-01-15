using System;
using System.Globalization;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x0200000D RID: 13
	internal static class Helpers
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000272F File Offset: 0x0000092F
		internal static bool IsGetOrHeadMethod(string method)
		{
			return Helpers.IsGetMethod(method) || Helpers.IsHeadMethod(method);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002741 File Offset: 0x00000941
		internal static bool IsGetMethod(string method)
		{
			return string.Equals("GET", method, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000274F File Offset: 0x0000094F
		internal static bool IsHeadMethod(string method)
		{
			return string.Equals("HEAD", method, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000275D File Offset: 0x0000095D
		internal static bool PathEndsInSlash(PathString path)
		{
			return path.Value.EndsWith("/", StringComparison.Ordinal);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002774 File Offset: 0x00000974
		internal static bool TryMatchPath(IOwinContext context, PathString matchUrl, bool forDirectory, out PathString subpath)
		{
			PathString path = context.Request.Path;
			if (forDirectory && !Helpers.PathEndsInSlash(path))
			{
				path += new PathString("/");
			}
			return path.StartsWithSegments(matchUrl, out subpath);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027B7 File Offset: 0x000009B7
		internal static bool TryParseHttpDate(string dateString, out DateTime parsedDate)
		{
			return DateTime.TryParseExact(dateString, "r", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
		}
	}
}
