using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200063C RID: 1596
	public abstract class UnboundedMultiValueCache<TKey, TValue> : CloneableCache<TKey, IReadOnlyCollection<TValue>, UnboundedMultiValueCache<TKey, TValue>>
	{
		// Token: 0x060022BA RID: 8890 RVA: 0x000623BA File Offset: 0x000605BA
		protected UnboundedMultiValueCache(IEqualityComparer<TKey> equalityComparer, Func<TKey, TKey> keyCloner, Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<TValue>> valueCloner)
			: base(equalityComparer, keyCloner, valueCloner)
		{
		}

		// Token: 0x060022BB RID: 8891
		public abstract void Add(TKey key, TValue value);

		// Token: 0x060022BC RID: 8892
		public abstract IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnlyDictionary();

		// Token: 0x060022BD RID: 8893 RVA: 0x000623C5 File Offset: 0x000605C5
		public static UnboundedMultiValueCache<TKey, TValue> Create(IEqualityComparer<TKey> equalityComparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
		{
			return new UnboundedMultiValueCache<TKey, TValue>.UnboundedMultiValueCacheImpl<List<TValue>>(equalityComparer, keyCloner, valueCloner);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000623CF File Offset: 0x000605CF
		public static UnboundedMultiValueCache<TKey, TValue> Create<TValueCollection>(IEqualityComparer<TKey> equalityComparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null) where TValueCollection : ICollection<TValue>, new()
		{
			return new UnboundedMultiValueCache<TKey, TValue>.UnboundedMultiValueCacheImpl<TValueCollection>(equalityComparer, keyCloner, valueCloner);
		}

		// Token: 0x0200063D RID: 1597
		private class UnboundedMultiValueCacheImpl<TValueCollection> : UnboundedMultiValueCache<TKey, TValue> where TValueCollection : ICollection<TValue>, new()
		{
			// Token: 0x060022BF RID: 8895 RVA: 0x000623DC File Offset: 0x000605DC
			public UnboundedMultiValueCacheImpl(IEqualityComparer<TKey> equalityComparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
				: base(equalityComparer, keyCloner, delegate(IReadOnlyCollection<TValue> lst)
				{
					Func<TValue, TValue> actualValueCloner = valueCloner.IdentityIfNull<TValue>();
					return lst.Select((TValue v) => actualValueCloner(v)).ToList<TValue>();
				})
			{
				this._cache = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(base.EqualityComparer);
			}

			// Token: 0x060022C0 RID: 8896 RVA: 0x0006241C File Offset: 0x0006061C
			private UnboundedMultiValueCacheImpl(UnboundedMultiValueCache<TKey, TValue>.UnboundedMultiValueCacheImpl<TValueCollection> other, Func<TKey, TKey> keyCloner = null, Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<TValue>> valueCloner = null)
				: base(other.EqualityComparer, other.KeyCloner, other.ValueCloner)
			{
				this._cache = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(base.EqualityComparer);
				keyCloner = keyCloner.IdentityIfNull<TKey>();
				valueCloner = valueCloner.IdentityIfNull<IReadOnlyCollection<TValue>>();
				foreach (KeyValuePair<TKey, IReadOnlyCollection<TValue>> keyValuePair in other._cache)
				{
					this._cache.AddRange(keyCloner(keyValuePair.Key), valueCloner(keyValuePair.Value));
				}
			}

			// Token: 0x060022C1 RID: 8897 RVA: 0x000624C0 File Offset: 0x000606C0
			public override bool Lookup(TKey key, out IReadOnlyCollection<TValue> value)
			{
				return this._cache.TryGetValue(key, out value);
			}

			// Token: 0x060022C2 RID: 8898 RVA: 0x000624CF File Offset: 0x000606CF
			public override UnboundedMultiValueCache<TKey, TValue> ShallowClone()
			{
				return new UnboundedMultiValueCache<TKey, TValue>.UnboundedMultiValueCacheImpl<TValueCollection>(this, null, null);
			}

			// Token: 0x060022C3 RID: 8899 RVA: 0x000624D9 File Offset: 0x000606D9
			public override UnboundedMultiValueCache<TKey, TValue> DeepClone()
			{
				return new UnboundedMultiValueCache<TKey, TValue>.UnboundedMultiValueCacheImpl<TValueCollection>(this, base.KeyCloner, base.ValueCloner);
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x060022C4 RID: 8900 RVA: 0x000624ED File Offset: 0x000606ED
			public override int Count
			{
				get
				{
					return this._cache.Count;
				}
			}

			// Token: 0x060022C5 RID: 8901 RVA: 0x000624FA File Offset: 0x000606FA
			public override void Clear()
			{
				this._cache.Clear();
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x060022C6 RID: 8902 RVA: 0x00062507 File Offset: 0x00060707
			public override IEnumerable<TKey> Keys
			{
				get
				{
					return this._cache.Keys;
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00062514 File Offset: 0x00060714
			public override IEnumerable<IReadOnlyCollection<TValue>> Values
			{
				get
				{
					return this._cache.Values;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x060022C8 RID: 8904 RVA: 0x00062521 File Offset: 0x00060721
			public override IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> Mappings
			{
				get
				{
					return this._cache;
				}
			}

			// Token: 0x060022C9 RID: 8905 RVA: 0x0006252C File Offset: 0x0006072C
			public override IReadOnlyCollection<TValue> GetOrAdd(TKey key, Func<TKey, IReadOnlyCollection<TValue>> insertValueFunc)
			{
				IReadOnlyCollection<TValue> readOnlyCollection;
				if (this._cache.TryGetValue(key, out readOnlyCollection))
				{
					return readOnlyCollection;
				}
				IReadOnlyCollection<TValue> readOnlyCollection2 = insertValueFunc(key);
				this.Add(key, readOnlyCollection2);
				return readOnlyCollection2;
			}

			// Token: 0x060022CA RID: 8906 RVA: 0x0006255C File Offset: 0x0006075C
			public override IReadOnlyCollection<TValue> AddOrUpdate(TKey key, Func<TKey, IReadOnlyCollection<TValue>> insertValueFunc, Func<TKey, IReadOnlyCollection<TValue>, IReadOnlyCollection<TValue>> updateValueFunc)
			{
				IReadOnlyCollection<TValue> readOnlyCollection;
				if (this._cache.TryGetValue(key, out readOnlyCollection))
				{
					IReadOnlyCollection<TValue> readOnlyCollection2 = updateValueFunc(key, readOnlyCollection);
					IReadOnlyCollection<TValue> readOnlyCollection3;
					this.Replace(key, readOnlyCollection2, out readOnlyCollection3);
					return readOnlyCollection2;
				}
				IReadOnlyCollection<TValue> readOnlyCollection4 = insertValueFunc(key);
				this.Add(key, readOnlyCollection4);
				return readOnlyCollection4;
			}

			// Token: 0x060022CB RID: 8907 RVA: 0x000625A0 File Offset: 0x000607A0
			public override void Add(TKey key, IReadOnlyCollection<TValue> value)
			{
				this._cache.AddRange(key, value);
			}

			// Token: 0x060022CC RID: 8908 RVA: 0x000625AF File Offset: 0x000607AF
			public override void Add(TKey key, TValue value)
			{
				this._cache.Add(key, value);
			}

			// Token: 0x060022CD RID: 8909 RVA: 0x000625BE File Offset: 0x000607BE
			public override bool Replace(TKey key, IReadOnlyCollection<TValue> value, out IReadOnlyCollection<TValue> oldValue)
			{
				bool flag = this._cache.TryGetValue(key, out oldValue);
				if (flag)
				{
					this._cache.Remove(key);
				}
				this._cache.AddRange(key, value);
				return flag;
			}

			// Token: 0x060022CE RID: 8910 RVA: 0x000625EA File Offset: 0x000607EA
			public override bool Remove(TKey key, out IReadOnlyCollection<TValue> removedValue)
			{
				bool flag = this._cache.TryGetValue(key, out removedValue);
				if (flag)
				{
					this._cache.Remove(key);
				}
				return flag;
			}

			// Token: 0x060022CF RID: 8911 RVA: 0x00062521 File Offset: 0x00060721
			public override IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnlyDictionary()
			{
				return this._cache;
			}

			// Token: 0x04001077 RID: 4215
			private readonly MultiValueDictionary<TKey, TValue> _cache;
		}
	}
}
