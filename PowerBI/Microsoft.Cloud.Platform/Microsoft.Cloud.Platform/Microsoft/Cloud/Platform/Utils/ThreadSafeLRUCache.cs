using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A8 RID: 424
	public sealed class ThreadSafeLRUCache<TKey, TValue>
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x00025B84 File Offset: 0x00023D84
		public ThreadSafeLRUCache(int maxSize)
		{
			this.m_cache = new LRUCache<TKey, TValue>(maxSize);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00025BA4 File Offset: 0x00023DA4
		public bool TryGet(TKey key, out TValue value)
		{
			object syncRoot = this.m_syncRoot;
			bool flag2;
			lock (syncRoot)
			{
				flag2 = this.m_cache.TryGet(key, out value);
			}
			return flag2;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00025BF0 File Offset: 0x00023DF0
		public bool TryUpdateValue(TKey key, Func<TValue, TValue> valueUpdater, out TValue updatedValue)
		{
			object syncRoot = this.m_syncRoot;
			bool flag2;
			lock (syncRoot)
			{
				flag2 = this.m_cache.TryUpdateValue(key, valueUpdater, out updatedValue);
			}
			return flag2;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00025C3C File Offset: 0x00023E3C
		public TValue Set(TKey key, TValue value)
		{
			object syncRoot = this.m_syncRoot;
			TValue tvalue;
			lock (syncRoot)
			{
				tvalue = this.m_cache.Set(key, value);
			}
			return tvalue;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00025C88 File Offset: 0x00023E88
		public bool TryRemove(TKey key, out TValue removedValue)
		{
			object syncRoot = this.m_syncRoot;
			bool flag2;
			lock (syncRoot)
			{
				flag2 = this.m_cache.TryRemove(key, out removedValue);
			}
			return flag2;
		}

		// Token: 0x0400044D RID: 1101
		private readonly object m_syncRoot = new object();

		// Token: 0x0400044E RID: 1102
		private LRUCache<TKey, TValue> m_cache;
	}
}
