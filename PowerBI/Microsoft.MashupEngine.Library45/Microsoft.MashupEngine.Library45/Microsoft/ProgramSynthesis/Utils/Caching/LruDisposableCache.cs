using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x02000639 RID: 1593
	public class LruDisposableCache<TKey, TValue> : LruCache<TKey, TValue> where TValue : IDisposable
	{
		// Token: 0x060022A5 RID: 8869 RVA: 0x00062188 File Offset: 0x00060388
		public LruDisposableCache(int cacheSize = 4096, IEqualityComparer<TKey> comparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
			: base(cacheSize, comparer, keyCloner, valueCloner)
		{
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00062195 File Offset: 0x00060395
		protected override void OnEviction(TValue evictedValue)
		{
			evictedValue.Dispose();
		}
	}
}
