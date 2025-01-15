using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200026F RID: 623
	internal interface ICacheItemFactory
	{
		// Token: 0x060014D5 RID: 5333
		AOMCacheItem GetCacheItem(Key key);

		// Token: 0x060014D6 RID: 5334
		AOMCacheItem GetCacheItem(IOMRegion region, Key key);

		// Token: 0x060014D7 RID: 5335
		AOMCacheItem GetCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags);

		// Token: 0x060014D8 RID: 5336
		AOMCacheItem GetCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags, ObjectType oType);

		// Token: 0x060014D9 RID: 5337
		AOMCacheItem GetCacheItem(AOMCacheItem item);

		// Token: 0x060014DA RID: 5338
		AOMCacheItem GetCacheItem(OMCacheItem item, OMRegion region);
	}
}
