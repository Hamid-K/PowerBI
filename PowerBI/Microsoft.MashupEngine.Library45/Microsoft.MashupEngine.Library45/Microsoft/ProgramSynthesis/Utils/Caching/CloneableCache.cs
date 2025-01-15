using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200062A RID: 1578
	public abstract class CloneableCache<TKey, TValue, TCache> : ICloneableCache<TKey, TValue, TCache>, ICloneableCache, ICachefulObject, ICachefulObject<TCache> where TCache : CloneableCache<TKey, TValue, TCache>
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x000613B3 File Offset: 0x0005F5B3
		protected CloneableCache(IEqualityComparer<TKey> equalityComparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
		{
			this.EqualityComparer = equalityComparer ?? EqualityComparer<TKey>.Default;
			this.KeyCloner = keyCloner.IdentityIfNull<TKey>();
			this.ValueCloner = valueCloner.IdentityIfNull<TValue>();
		}

		// Token: 0x170005E8 RID: 1512
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (this.Lookup(key, out tvalue))
				{
					return tvalue;
				}
				throw new KeyNotFoundException(FormattableString.Invariant(FormattableStringFactory.Create("Key: {0}", new object[] { key })));
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600222E RID: 8750
		public abstract IEnumerable<TKey> Keys { get; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600222F RID: 8751
		public abstract IEnumerable<TValue> Values { get; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002230 RID: 8752
		public abstract IEnumerable<KeyValuePair<TKey, TValue>> Mappings { get; }

		// Token: 0x06002231 RID: 8753
		public abstract TValue GetOrAdd(TKey key, Func<TKey, TValue> insertValueFunc);

		// Token: 0x06002232 RID: 8754
		public abstract TValue AddOrUpdate(TKey key, Func<TKey, TValue> insertValueFunc, Func<TKey, TValue, TValue> updateValueFunc);

		// Token: 0x06002233 RID: 8755
		public abstract void Add(TKey key, TValue value);

		// Token: 0x06002234 RID: 8756
		public abstract bool Replace(TKey key, TValue value, out TValue oldValue);

		// Token: 0x06002235 RID: 8757
		public abstract bool Remove(TKey key, out TValue removedValue);

		// Token: 0x06002236 RID: 8758
		public abstract bool Lookup(TKey key, out TValue value);

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00061421 File Offset: 0x0005F621
		public Func<TKey, TKey> KeyCloner { get; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x00061429 File Offset: 0x0005F629
		public Func<TValue, TValue> ValueCloner { get; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x00061431 File Offset: 0x0005F631
		public IEqualityComparer<TKey> EqualityComparer { get; }

		// Token: 0x0600223A RID: 8762
		public abstract TCache ShallowClone();

		// Token: 0x0600223B RID: 8763 RVA: 0x00061439 File Offset: 0x0005F639
		ICloneableCache ICloneableCache.ShallowClone()
		{
			return this.ShallowClone();
		}

		// Token: 0x0600223C RID: 8764
		public abstract TCache DeepClone();

		// Token: 0x0600223D RID: 8765 RVA: 0x00061446 File Offset: 0x0005F646
		ICloneableCache ICloneableCache.DeepClone()
		{
			return this.DeepClone();
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600223E RID: 8766
		public abstract int Count { get; }

		// Token: 0x0600223F RID: 8767
		public abstract void Clear();

		// Token: 0x06002240 RID: 8768 RVA: 0x00061453 File Offset: 0x0005F653
		public TCache CloneWithCurrentCacheState()
		{
			return this.DeepClone();
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0006145B File Offset: 0x0005F65B
		public void ClearCaches()
		{
			this.Clear();
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00061463 File Offset: 0x0005F663
		ICachefulObject ICachefulObject.CloneWithCurrentCacheState()
		{
			return this.CloneWithCurrentCacheState();
		}
	}
}
