using System;

namespace Microsoft.ReportingServices.Diagnostics.Caching
{
	// Token: 0x0200007D RID: 125
	internal sealed class CacheProvider
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00002E32 File Offset: 0x00001032
		private CacheProvider()
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010648 File Offset: 0x0000E848
		public static bool TryGetPowerViewSessionStateCache(out ICache cache)
		{
			return CacheProvider.TryGetCache(CacheIdentifier.PowerViewSessionState, out cache);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010651 File Offset: 0x0000E851
		private static bool TryGetCache(CacheIdentifier cacheId, out ICache cache)
		{
			cache = null;
			if (CacheProvider.m_cacheFactory != null)
			{
				cache = CacheProvider.m_cacheFactory.CreateCache(cacheId);
			}
			return cache != null;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001066F File Offset: 0x0000E86F
		internal static void InitCacheFactory(ICacheFactory cacheFactory)
		{
			CacheProvider.m_cacheFactory = cacheFactory;
		}

		// Token: 0x04000371 RID: 881
		private static ICacheFactory m_cacheFactory;
	}
}
