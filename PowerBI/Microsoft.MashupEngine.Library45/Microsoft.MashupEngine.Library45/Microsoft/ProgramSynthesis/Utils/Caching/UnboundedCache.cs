using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200063A RID: 1594
	public class UnboundedCache<TKey, TValue> : CloneableCache<TKey, TValue, UnboundedCache<TKey, TValue>>
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x000621A4 File Offset: 0x000603A4
		public UnboundedCache()
			: this(null, null, null)
		{
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000621AF File Offset: 0x000603AF
		public UnboundedCache(IEqualityComparer<TKey> equalityComparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
			: base(equalityComparer, keyCloner, valueCloner)
		{
			this._cache = new Dictionary<TKey, TValue>(base.EqualityComparer);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000621CC File Offset: 0x000603CC
		private UnboundedCache(UnboundedCache<TKey, TValue> other, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
			: base(other.EqualityComparer, other.KeyCloner, other.ValueCloner)
		{
			keyCloner = keyCloner.IdentityIfNull<TKey>();
			valueCloner = valueCloner.IdentityIfNull<TValue>();
			this._cache = this._cache.ToDictionary((KeyValuePair<TKey, TValue> kvp) => keyCloner(kvp.Key), (KeyValuePair<TKey, TValue> kvp) => valueCloner(kvp.Value), other.EqualityComparer);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00062256 File Offset: 0x00060456
		public override bool Lookup(TKey key, out TValue value)
		{
			return this._cache.TryGetValue(key, out value);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x00062265 File Offset: 0x00060465
		public override UnboundedCache<TKey, TValue> ShallowClone()
		{
			return new UnboundedCache<TKey, TValue>(this, null, null);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0006226F File Offset: 0x0006046F
		public override UnboundedCache<TKey, TValue> DeepClone()
		{
			return new UnboundedCache<TKey, TValue>(this, base.KeyCloner, base.ValueCloner);
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x00062283 File Offset: 0x00060483
		public override int Count
		{
			get
			{
				return this._cache.Count;
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x00062290 File Offset: 0x00060490
		public override void Clear()
		{
			this._cache.Clear();
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0006229D File Offset: 0x0006049D
		public override IEnumerable<TKey> Keys
		{
			get
			{
				return this._cache.Keys;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000622AA File Offset: 0x000604AA
		public override IEnumerable<TValue> Values
		{
			get
			{
				return this._cache.Values;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000622B7 File Offset: 0x000604B7
		public override IEnumerable<KeyValuePair<TKey, TValue>> Mappings
		{
			get
			{
				return this._cache;
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000622C0 File Offset: 0x000604C0
		public override TValue GetOrAdd(TKey key, Func<TKey, TValue> insertValueFunc)
		{
			TValue tvalue;
			if (this._cache.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			TValue tvalue2 = insertValueFunc(key);
			this.Add(key, tvalue2);
			return tvalue2;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000622F0 File Offset: 0x000604F0
		public override TValue AddOrUpdate(TKey key, Func<TKey, TValue> insertValueFunc, Func<TKey, TValue, TValue> updateValueFunc)
		{
			TValue tvalue;
			if (this._cache.TryGetValue(key, out tvalue))
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

		// Token: 0x060022B4 RID: 8884 RVA: 0x00062334 File Offset: 0x00060534
		public override void Add(TKey key, TValue value)
		{
			if (this._cache.ContainsKey(key))
			{
				throw new InvalidOperationException();
			}
			this._cache[key] = value;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00062357 File Offset: 0x00060557
		public override bool Replace(TKey key, TValue value, out TValue oldValue)
		{
			bool flag = this._cache.TryGetValue(key, out oldValue);
			this._cache[key] = value;
			return flag;
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00062373 File Offset: 0x00060573
		public override bool Remove(TKey key, out TValue removedValue)
		{
			bool flag = this._cache.TryGetValue(key, out removedValue);
			if (flag)
			{
				this._cache.Remove(key);
			}
			return flag;
		}

		// Token: 0x04001074 RID: 4212
		private readonly Dictionary<TKey, TValue> _cache;
	}
}
