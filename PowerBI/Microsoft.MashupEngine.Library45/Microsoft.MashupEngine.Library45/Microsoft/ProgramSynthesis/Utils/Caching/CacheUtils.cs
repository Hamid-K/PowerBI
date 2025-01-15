using System;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x02000629 RID: 1577
	public static class CacheUtils
	{
		// Token: 0x0600222A RID: 8746 RVA: 0x00061360 File Offset: 0x0005F560
		public static TValue LookupOrCompute<TKey, TValue, TCache>(this ICloneableCache<TKey, TValue, TCache> cache, TKey key, Func<TKey, TValue> compute) where TCache : ICloneableCache<TKey, TValue, TCache>
		{
			TValue tvalue;
			if (!cache.Lookup(key, out tvalue))
			{
				tvalue = compute(key);
				cache.Add(key, tvalue);
			}
			return tvalue;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0006138C File Offset: 0x0005F58C
		public static TValue LookupOrCreateValue<TKey, TValue, TCache>(this ICloneableCache<TKey, TValue, TCache> cache, TKey key) where TValue : new() where TCache : ICloneableCache<TKey, TValue, TCache>
		{
			TValue tvalue;
			if (!cache.Lookup(key, out tvalue))
			{
				tvalue = new TValue();
				cache.Add(key, tvalue);
			}
			return tvalue;
		}
	}
}
