using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200062C RID: 1580
	public class ConcurrentLruCache<TKey, TValue> : LruCache<TKey, TValue>
	{
		// Token: 0x0600224D RID: 8781 RVA: 0x00061698 File Offset: 0x0005F898
		public ConcurrentLruCache(int cacheSize = 4096, IEqualityComparer<TKey> comparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
			: base(cacheSize, comparer ?? EqualityComparer<TKey>.Default, keyCloner, valueCloner)
		{
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000616B0 File Offset: 0x0005F8B0
		public override bool Remove(TKey key, out TValue removedValue)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Remove(key, out removedValue);
			}
			return flag2;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000616F0 File Offset: 0x0005F8F0
		public override bool Lookup(TKey key, out TValue value)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Lookup(key, out value);
			}
			return flag2;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00061730 File Offset: 0x0005F930
		public override LruCache<TKey, TValue> ShallowClone()
		{
			LruCache<TKey, TValue> lruCache;
			lock (this)
			{
				lruCache = base.ShallowClone();
			}
			return lruCache;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00061770 File Offset: 0x0005F970
		public override LruCache<TKey, TValue> DeepClone()
		{
			LruCache<TKey, TValue> lruCache;
			lock (this)
			{
				lruCache = base.DeepClone();
			}
			return lruCache;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000617B0 File Offset: 0x0005F9B0
		public override TValue GetOrAdd(TKey key, Func<TKey, TValue> insertValueFunc)
		{
			TValue orAdd;
			lock (this)
			{
				orAdd = base.GetOrAdd(key, insertValueFunc);
			}
			return orAdd;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000617F0 File Offset: 0x0005F9F0
		public override TValue AddOrUpdate(TKey key, Func<TKey, TValue> insertValueFunc, Func<TKey, TValue, TValue> updateValueFunc)
		{
			TValue tvalue;
			lock (this)
			{
				tvalue = base.AddOrUpdate(key, insertValueFunc, updateValueFunc);
			}
			return tvalue;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00061830 File Offset: 0x0005FA30
		public override void Add(TKey key, TValue value)
		{
			TValue tvalue;
			this.Replace(key, value, out tvalue);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00061848 File Offset: 0x0005FA48
		public override bool Replace(TKey key, TValue value, out TValue oldValue)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Replace(key, value, out oldValue);
			}
			return flag2;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00061888 File Offset: 0x0005FA88
		public override void Clear()
		{
			lock (this)
			{
				base.Clear();
			}
		}
	}
}
