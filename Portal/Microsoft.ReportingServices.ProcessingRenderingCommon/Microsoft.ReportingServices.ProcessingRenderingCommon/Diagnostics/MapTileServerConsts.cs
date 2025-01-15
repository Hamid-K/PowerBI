using System;
using System.Net.Cache;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000056 RID: 86
	internal static class MapTileServerConsts
	{
		// Token: 0x06000252 RID: 594 RVA: 0x000094B4 File Offset: 0x000076B4
		internal static RequestCacheLevel ConvertFromMapTileCacheLevel(MapTileCacheLevel cacheLevel)
		{
			switch (cacheLevel)
			{
			case MapTileCacheLevel.BypassCache:
				return RequestCacheLevel.BypassCache;
			case MapTileCacheLevel.CacheOnly:
				return RequestCacheLevel.CacheOnly;
			case MapTileCacheLevel.CacheIfAvailable:
				return RequestCacheLevel.CacheIfAvailable;
			case MapTileCacheLevel.Revalidate:
				return RequestCacheLevel.Revalidate;
			case MapTileCacheLevel.Reload:
				return RequestCacheLevel.Reload;
			case MapTileCacheLevel.NoCacheNoStore:
				return RequestCacheLevel.NoCacheNoStore;
			default:
				return RequestCacheLevel.Default;
			}
		}

		// Token: 0x04000134 RID: 308
		internal const string MaxConnections = "MaxConnections";

		// Token: 0x04000135 RID: 309
		internal const string Timeout = "Timeout";

		// Token: 0x04000136 RID: 310
		internal const string AppID = "AppID";

		// Token: 0x04000137 RID: 311
		internal const string CacheLevel = "CacheLevel";

		// Token: 0x04000138 RID: 312
		internal const int MaxConnectionsDefault = 2;

		// Token: 0x04000139 RID: 313
		internal const int TimeoutDefault = 10;

		// Token: 0x0400013A RID: 314
		internal const string AppIDDefault = "(Default)";

		// Token: 0x0400013B RID: 315
		internal const MapTileCacheLevel CacheLevelDefault = MapTileCacheLevel.Default;

		// Token: 0x0400013C RID: 316
		internal const int MaxConnectionsMinValue = 1;

		// Token: 0x0400013D RID: 317
		internal const int MaxConnectionsMaxValue = 2147483647;

		// Token: 0x0400013E RID: 318
		internal const int TimeoutMinValue = 1;

		// Token: 0x0400013F RID: 319
		internal const int TimeoutMaxValue = 2147483647;
	}
}
