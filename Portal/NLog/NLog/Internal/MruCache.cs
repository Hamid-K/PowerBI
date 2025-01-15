using System;
using System.Collections.Generic;

namespace NLog.Internal
{
	// Token: 0x02000129 RID: 297
	internal class MruCache<TKey, TValue>
	{
		// Token: 0x06000EF3 RID: 3827 RVA: 0x00025020 File Offset: 0x00023220
		public MruCache(int maxCapacity)
		{
			this._maxCapacity = maxCapacity;
			this._dictionary = new Dictionary<TKey, MruCache<TKey, TValue>.MruCacheItem>(this._maxCapacity);
			this._currentVersion = 1L;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00025048 File Offset: 0x00023248
		public bool TryAddValue(TKey key, TValue value)
		{
			Dictionary<TKey, MruCache<TKey, TValue>.MruCacheItem> dictionary = this._dictionary;
			bool flag2;
			lock (dictionary)
			{
				MruCache<TKey, TValue>.MruCacheItem mruCacheItem;
				if (this._dictionary.TryGetValue(key, out mruCacheItem))
				{
					long currentVersion = this._currentVersion;
					if (mruCacheItem.Version != currentVersion || !EqualityComparer<TValue>.Default.Equals(value, mruCacheItem.Value))
					{
						this._dictionary[key] = new MruCache<TKey, TValue>.MruCacheItem(value, currentVersion, false);
					}
					flag2 = false;
				}
				else
				{
					if (this._dictionary.Count >= this._maxCapacity)
					{
						this._currentVersion += 1L;
						this.PruneCache();
					}
					this._dictionary.Add(key, new MruCache<TKey, TValue>.MruCacheItem(value, this._currentVersion, true));
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00025114 File Offset: 0x00023314
		private void PruneCache()
		{
			long num = this._currentVersion - 2L;
			long num2 = 1L;
			List<TKey> list = new List<TKey>((int)((double)this._dictionary.Count / 2.5));
			for (int i = 0; i < 3; i++)
			{
				long num3 = this._currentVersion - 5L;
				if (i != 0)
				{
					if (i == 1)
					{
						num3 = this._currentVersion - 10L;
					}
				}
				else
				{
					num3 = this._currentVersion - (long)((int)((double)this._maxCapacity / 1.5));
				}
				if (num3 < num2)
				{
					num3 = num2;
				}
				num2 = long.MaxValue;
				foreach (KeyValuePair<TKey, MruCache<TKey, TValue>.MruCacheItem> keyValuePair in this._dictionary)
				{
					long version = keyValuePair.Value.Version;
					if (version <= num3 || (keyValuePair.Value.Virgin && (i != 0 || version < num)))
					{
						list.Add(keyValuePair.Key);
						if ((double)(this._dictionary.Count - list.Count) < (double)this._maxCapacity / 1.5)
						{
							i = 3;
							break;
						}
					}
					else if (version < num2)
					{
						num2 = version;
					}
				}
			}
			foreach (TKey tkey in list)
			{
				this._dictionary.Remove(tkey);
			}
			if (this._dictionary.Count >= this._maxCapacity)
			{
				this._dictionary.Clear();
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000252BC File Offset: 0x000234BC
		public bool TryGetValue(TKey key, out TValue value)
		{
			MruCache<TKey, TValue>.MruCacheItem mruCacheItem;
			try
			{
				if (!this._dictionary.TryGetValue(key, out mruCacheItem))
				{
					value = default(TValue);
					return false;
				}
			}
			catch
			{
				mruCacheItem = default(MruCache<TKey, TValue>.MruCacheItem);
			}
			if (mruCacheItem.Version != this._currentVersion || mruCacheItem.Virgin)
			{
				Dictionary<TKey, MruCache<TKey, TValue>.MruCacheItem> dictionary = this._dictionary;
				lock (dictionary)
				{
					long num = this._currentVersion;
					if (!this._dictionary.TryGetValue(key, out mruCacheItem))
					{
						value = default(TValue);
						return false;
					}
					if (mruCacheItem.Version != num || mruCacheItem.Virgin)
					{
						if (mruCacheItem.Virgin)
						{
							long num2 = this._currentVersion + 1L;
							this._currentVersion = num2;
							num = num2;
						}
						this._dictionary[key] = new MruCache<TKey, TValue>.MruCacheItem(mruCacheItem.Value, num, false);
					}
				}
			}
			value = mruCacheItem.Value;
			return true;
		}

		// Token: 0x04000402 RID: 1026
		private readonly Dictionary<TKey, MruCache<TKey, TValue>.MruCacheItem> _dictionary;

		// Token: 0x04000403 RID: 1027
		private readonly int _maxCapacity;

		// Token: 0x04000404 RID: 1028
		private long _currentVersion;

		// Token: 0x02000268 RID: 616
		private struct MruCacheItem
		{
			// Token: 0x06001616 RID: 5654 RVA: 0x0003A04F File Offset: 0x0003824F
			public MruCacheItem(TValue value, long version, bool virgin)
			{
				this.Value = value;
				this.Version = version;
				this.Virgin = virgin;
			}

			// Token: 0x040006A2 RID: 1698
			public readonly TValue Value;

			// Token: 0x040006A3 RID: 1699
			public readonly long Version;

			// Token: 0x040006A4 RID: 1700
			public readonly bool Virgin;
		}
	}
}
