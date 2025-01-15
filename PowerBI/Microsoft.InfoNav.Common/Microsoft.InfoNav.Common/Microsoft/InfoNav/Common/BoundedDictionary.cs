using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200003D RID: 61
	public sealed class BoundedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000832A File Offset: 0x0000652A
		public BoundedDictionary(int capacity)
		{
			this._capacity = capacity;
			this._dictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008344 File Offset: 0x00006544
		public BoundedDictionary(IEqualityComparer<TKey> comparer, int compacity)
		{
			this._capacity = compacity;
			this._dictionary = new Dictionary<TKey, TValue>(comparer);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000835F File Offset: 0x0000655F
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000836C File Offset: 0x0000656C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000836F File Offset: 0x0000656F
		public bool IsFull
		{
			get
			{
				return this.Count >= this._capacity;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00008382 File Offset: 0x00006582
		public ICollection<TKey> Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000838F File Offset: 0x0000658F
		public ICollection<TValue> Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x1700002D RID: 45
		public TValue this[TKey key]
		{
			get
			{
				return this._dictionary[key];
			}
			set
			{
				if (this._dictionary.ContainsKey(key) || !this.IsFull)
				{
					this._dictionary[key] = value;
				}
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000083CF File Offset: 0x000065CF
		public void Add(TKey key, TValue value)
		{
			if (!this.IsFull)
			{
				this._dictionary.Add(key, value);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000083E6 File Offset: 0x000065E6
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (!this.IsFull)
			{
				this._dictionary.Add(item.Key, item.Value);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008409 File Offset: 0x00006609
		public void Clear()
		{
			this._dictionary.Clear();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008416 File Offset: 0x00006616
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this._dictionary.Contains(item);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008424 File Offset: 0x00006624
		public bool ContainsKey(TKey key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008432 File Offset: 0x00006632
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this._dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008441 File Offset: 0x00006641
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000844E File Offset: 0x0000664E
		public bool Remove(TKey key)
		{
			return this._dictionary.Remove(key);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000845C File Offset: 0x0000665C
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return this._dictionary.Remove(item.Key);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008470 File Offset: 0x00006670
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000847F File Offset: 0x0000667F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x04000099 RID: 153
		private readonly int _capacity;

		// Token: 0x0400009A RID: 154
		private readonly IDictionary<TKey, TValue> _dictionary;
	}
}
