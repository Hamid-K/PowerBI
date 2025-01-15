using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200012E RID: 302
	internal static class RepublishingCache
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x0002D824 File Offset: 0x0002BA24
		public static void MarkPathAsChecked(CatalogItemPath itemPath)
		{
			if (ItemPathBase.IsNullOrEmpty(itemPath))
			{
				return;
			}
			Dictionary<string, string> cachedPaths = RepublishingCache.m_cachedPaths;
			lock (cachedPaths)
			{
				if (RepublishingCache.m_cachedPaths.Count >= 131072)
				{
					RepublishingCache.m_cachedPaths.Clear();
				}
				RepublishingCache.m_cachedPaths[itemPath.Value] = itemPath.Value;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002D898 File Offset: 0x0002BA98
		public static bool HasPathBeenChecked(CatalogItemPath itemPath)
		{
			if (ItemPathBase.IsNullOrEmpty(itemPath))
			{
				return false;
			}
			Dictionary<string, string> cachedPaths = RepublishingCache.m_cachedPaths;
			bool flag2;
			lock (cachedPaths)
			{
				flag2 = RepublishingCache.m_cachedPaths.ContainsKey(itemPath.Value);
			}
			return flag2;
		}

		// Token: 0x040004EF RID: 1263
		private static readonly Dictionary<string, string> m_cachedPaths = new Dictionary<string, string>(StringComparer.Create(Localization.CatalogCulture, true));

		// Token: 0x040004F0 RID: 1264
		private const int MaxCacheEntries = 131072;
	}
}
