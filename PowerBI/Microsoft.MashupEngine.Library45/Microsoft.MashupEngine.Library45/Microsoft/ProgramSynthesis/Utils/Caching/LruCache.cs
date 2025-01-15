using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x02000635 RID: 1589
	public class LruCache<TKey, TValue> : CloneableCache<TKey, TValue, LruCache<TKey, TValue>>
	{
		// Token: 0x0600228B RID: 8843 RVA: 0x00061D28 File Offset: 0x0005FF28
		public LruCache(int cacheSize = 4096, IEqualityComparer<TKey> comparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
			: base(comparer ?? EqualityComparer<TKey>.Default, keyCloner, valueCloner)
		{
			this._cache = new Dictionary<TKey, LinkedListNode<LruCache<TKey, TValue>.CacheItem>>(comparer ?? base.EqualityComparer);
			this._lruList = new LinkedList<LruCache<TKey, TValue>.CacheItem>();
			this._cacheSize = cacheSize;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x00061D68 File Offset: 0x0005FF68
		private LruCache(LruCache<TKey, TValue> other, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
			: base(other.EqualityComparer, other.KeyCloner, other.ValueCloner)
		{
			keyCloner = keyCloner.IdentityIfNull<TKey>();
			valueCloner = valueCloner.IdentityIfNull<TValue>();
			this._lruList = new LinkedList<LruCache<TKey, TValue>.CacheItem>(other._lruList.Select((LruCache<TKey, TValue>.CacheItem ci) => new LruCache<TKey, TValue>.CacheItem(keyCloner(ci.Key), valueCloner(ci.Value))));
			this._cache = this._lruList.Nodes<LruCache<TKey, TValue>.CacheItem>().ToDictionary((LinkedListNode<LruCache<TKey, TValue>.CacheItem> node) => node.Value.Key, (LinkedListNode<LruCache<TKey, TValue>.CacheItem> node) => node, base.EqualityComparer);
			this._cacheSize = other._cacheSize;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x00061E4C File Offset: 0x0006004C
		public override bool Remove(TKey key, out TValue removedValue)
		{
			LinkedListNode<LruCache<TKey, TValue>.CacheItem> linkedListNode;
			if (this._cache.TryGetValue(key, out linkedListNode))
			{
				removedValue = linkedListNode.Value.Value;
				this._cache.Remove(key);
				this._lruList.Remove(linkedListNode);
				return true;
			}
			removedValue = default(TValue);
			return false;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00061EA0 File Offset: 0x000600A0
		public override bool Lookup(TKey key, out TValue value)
		{
			LinkedListNode<LruCache<TKey, TValue>.CacheItem> linkedListNode;
			if (this._cache.TryGetValue(key, out linkedListNode))
			{
				this._lruList.Remove(linkedListNode);
				this._lruList.AddFirst(linkedListNode);
				value = linkedListNode.Value.Value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00061EF0 File Offset: 0x000600F0
		public override LruCache<TKey, TValue> ShallowClone()
		{
			return new LruCache<TKey, TValue>(this, null, null);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00061EFA File Offset: 0x000600FA
		public override LruCache<TKey, TValue> DeepClone()
		{
			return new LruCache<TKey, TValue>(this, base.KeyCloner, base.ValueCloner);
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x00061F0E File Offset: 0x0006010E
		public override IEnumerable<TKey> Keys
		{
			get
			{
				return this._cache.Keys;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x00061F1B File Offset: 0x0006011B
		public override IEnumerable<TValue> Values
		{
			get
			{
				return this._lruList.Select((LruCache<TKey, TValue>.CacheItem ci) => ci.Value);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x00061F47 File Offset: 0x00060147
		public override IEnumerable<KeyValuePair<TKey, TValue>> Mappings
		{
			get
			{
				return this._cache.Select((KeyValuePair<TKey, LinkedListNode<LruCache<TKey, TValue>.CacheItem>> kvp) => new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value.Value.Value));
			}
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00061F74 File Offset: 0x00060174
		public override TValue GetOrAdd(TKey key, Func<TKey, TValue> insertValueFunc)
		{
			TValue tvalue;
			if (this.Lookup(key, out tvalue))
			{
				return tvalue;
			}
			TValue tvalue2 = insertValueFunc(key);
			this.Add(key, tvalue2);
			return tvalue2;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00061FA0 File Offset: 0x000601A0
		public override TValue AddOrUpdate(TKey key, Func<TKey, TValue> insertValueFunc, Func<TKey, TValue, TValue> updateValueFunc)
		{
			TValue tvalue;
			if (this.Lookup(key, out tvalue))
			{
				TValue tvalue2 = updateValueFunc(key, tvalue);
				TValue tvalue3;
				this.Replace(key, tvalue2, out tvalue3);
				return tvalue2;
			}
			TValue tvalue4 = insertValueFunc(key);
			this.Add(key, tvalue4);
			return tvalue4;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0000CC37 File Offset: 0x0000AE37
		protected virtual void OnEviction(TValue evictedValue)
		{
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00061FE0 File Offset: 0x000601E0
		private void EvictIfNeeded()
		{
			while (this._cache.Count >= this._cacheSize)
			{
				LinkedListNode<LruCache<TKey, TValue>.CacheItem> last = this._lruList.Last;
				this._lruList.RemoveLast();
				this._cache.Remove(last.Value.Key);
				this.OnEviction(last.Value.Value);
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00062044 File Offset: 0x00060244
		public override void Add(TKey key, TValue value)
		{
			TValue tvalue;
			this.Replace(key, value, out tvalue);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0006205C File Offset: 0x0006025C
		public override bool Replace(TKey key, TValue value, out TValue oldValue)
		{
			LinkedListNode<LruCache<TKey, TValue>.CacheItem> linkedListNode;
			if (this._cache.TryGetValue(key, out linkedListNode))
			{
				oldValue = linkedListNode.Value.Value;
				linkedListNode.Value.Value = value;
				this._lruList.Remove(linkedListNode);
				this._lruList.AddFirst(linkedListNode);
				return true;
			}
			this.EvictIfNeeded();
			LinkedListNode<LruCache<TKey, TValue>.CacheItem> linkedListNode2 = new LinkedListNode<LruCache<TKey, TValue>.CacheItem>(new LruCache<TKey, TValue>.CacheItem(key, value));
			this._cache[key] = linkedListNode2;
			this._lruList.AddFirst(linkedListNode2);
			oldValue = default(TValue);
			return false;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x000620E4 File Offset: 0x000602E4
		public override int Count
		{
			get
			{
				return this._cache.Count;
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000620F1 File Offset: 0x000602F1
		public override void Clear()
		{
			this._cache.Clear();
			this._lruList.Clear();
		}

		// Token: 0x04001067 RID: 4199
		public const int DefaultCacheSize = 4096;

		// Token: 0x04001068 RID: 4200
		private readonly Dictionary<TKey, LinkedListNode<LruCache<TKey, TValue>.CacheItem>> _cache;

		// Token: 0x04001069 RID: 4201
		private readonly LinkedList<LruCache<TKey, TValue>.CacheItem> _lruList;

		// Token: 0x0400106A RID: 4202
		private readonly int _cacheSize;

		// Token: 0x02000636 RID: 1590
		private class CacheItem
		{
			// Token: 0x0600229C RID: 8860 RVA: 0x00062109 File Offset: 0x00060309
			public CacheItem(TKey key, TValue value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x0400106B RID: 4203
			public readonly TKey Key;

			// Token: 0x0400106C RID: 4204
			public TValue Value;
		}
	}
}
