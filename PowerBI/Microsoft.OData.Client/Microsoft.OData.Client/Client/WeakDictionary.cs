using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Client
{
	// Token: 0x02000036 RID: 54
	internal class WeakDictionary<TKey, TValue> where TKey : class
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00008198 File Offset: 0x00006398
		public WeakDictionary(IEqualityComparer<TKey> comparer)
			: this(comparer, 1000)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000081A8 File Offset: 0x000063A8
		public WeakDictionary(IEqualityComparer<TKey> comparer, int refreshInterval)
		{
			this.comparer = comparer as WeakKeyComparer<TKey>;
			if (this.comparer == null)
			{
				this.comparer = new WeakKeyComparer<TKey>(comparer);
			}
			this.dictionary = new Dictionary<object, TValue>(this.comparer);
			this.intervalForRefresh = refreshInterval;
			this.countLimitForRefresh = refreshInterval;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000081FA File Offset: 0x000063FA
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00008202 File Offset: 0x00006402
		public Func<object, object> CreateWeakKey { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000820B File Offset: 0x0000640B
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008213 File Offset: 0x00006413
		public List<Func<object, bool>> RemoveCollectedEntriesRules { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000821C File Offset: 0x0000641C
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700005D RID: 93
		public TValue this[TKey key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				this.dictionary[key] = value;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008250 File Offset: 0x00006450
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.countForRefresh >= this.countLimitForRefresh)
			{
				this.RemoveCollectedEntries();
			}
			if (this.CreateWeakKey != null)
			{
				this.dictionary.Add(this.CreateWeakKey(key), value);
			}
			else
			{
				this.dictionary.Add(new WeakKeyReference<TKey>(key, this.comparer), value);
			}
			this.countForRefresh++;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000082D1 File Offset: 0x000064D1
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000082E4 File Offset: 0x000064E4
		public bool Remove(TKey key)
		{
			bool flag = this.dictionary.Remove(key);
			if (flag)
			{
				this.countForRefresh--;
			}
			return flag;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008315 File Offset: 0x00006515
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000832C File Offset: 0x0000652C
		public void RemoveCollectedEntries()
		{
			List<object> list = new List<object>();
			using (Dictionary<object, TValue>.KeyCollection.Enumerator enumerator = this.dictionary.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object key = enumerator.Current;
					WeakKeyReference<TKey> weakKeyReference = key as WeakKeyReference<TKey>;
					if (weakKeyReference != null)
					{
						if (!weakKeyReference.IsAlive)
						{
							list.Add(key);
						}
					}
					else if (this.RemoveCollectedEntriesRules.Any((Func<object, bool> f) => f(key)))
					{
						list.Add(key);
					}
				}
			}
			foreach (object obj in list)
			{
				if (this.dictionary.Remove(obj))
				{
					this.countForRefresh--;
				}
			}
			this.countLimitForRefresh = this.countForRefresh + this.intervalForRefresh;
		}

		// Token: 0x0400008C RID: 140
		private Dictionary<object, TValue> dictionary;

		// Token: 0x0400008D RID: 141
		private WeakKeyComparer<TKey> comparer;

		// Token: 0x0400008E RID: 142
		private int intervalForRefresh;

		// Token: 0x0400008F RID: 143
		private int countLimitForRefresh;

		// Token: 0x04000090 RID: 144
		private int countForRefresh;
	}
}
