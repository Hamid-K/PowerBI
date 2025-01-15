using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200004C RID: 76
	internal sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000311 RID: 785 RVA: 0x000091D3 File Offset: 0x000073D3
		internal ReadOnlyDictionary(IDictionary<TKey, TValue> underlyingDictionary)
		{
			this.m_underlyingDictionary = underlyingDictionary;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000091E2 File Offset: 0x000073E2
		public int Count
		{
			get
			{
				return this.m_underlyingDictionary.Count;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000091EF File Offset: 0x000073EF
		public ICollection<TKey> Keys
		{
			get
			{
				return this.m_underlyingDictionary.Keys;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000091FC File Offset: 0x000073FC
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000091FF File Offset: 0x000073FF
		public ICollection<TValue> Values
		{
			get
			{
				return this.m_underlyingDictionary.Values;
			}
		}

		// Token: 0x1700007C RID: 124
		public TValue this[TKey key]
		{
			get
			{
				return this.m_underlyingDictionary[key];
			}
			set
			{
				throw new NotSupportedException("this[TKey]");
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00009226 File Offset: 0x00007426
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.m_underlyingDictionary.Contains(item);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00009234 File Offset: 0x00007434
		public bool ContainsKey(TKey key)
		{
			return this.m_underlyingDictionary.ContainsKey(key);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00009242 File Offset: 0x00007442
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.m_underlyingDictionary.GetEnumerator();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000924F File Offset: 0x0000744F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_underlyingDictionary.GetEnumerator();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000925C File Offset: 0x0000745C
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.m_underlyingDictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000926B File Offset: 0x0000746B
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Add(KeyValuePair<TKey, TValue>)");
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00009277 File Offset: 0x00007477
		public void Add(TKey key, TValue value)
		{
			throw new NotSupportedException("Add(TKey, TValue)");
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00009283 File Offset: 0x00007483
		public void Clear()
		{
			throw new NotSupportedException("Clear()");
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000928F File Offset: 0x0000748F
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			throw new NotSupportedException("CopyTo()");
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000929B File Offset: 0x0000749B
		public bool Remove(TKey key)
		{
			throw new NotSupportedException("Remove(TKey)");
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000092A7 File Offset: 0x000074A7
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Remove()");
		}

		// Token: 0x040000DA RID: 218
		private readonly IDictionary<TKey, TValue> m_underlyingDictionary;
	}
}
