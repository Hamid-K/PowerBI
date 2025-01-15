using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C19 RID: 7193
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x0600B37F RID: 45951 RVA: 0x00247D01 File Offset: 0x00245F01
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = dictionary;
		}

		// Token: 0x17002CF3 RID: 11507
		public TValue this[TKey key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002CF4 RID: 11508
		// (get) Token: 0x0600B382 RID: 45954 RVA: 0x00247D1E File Offset: 0x00245F1E
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17002CF5 RID: 11509
		// (get) Token: 0x0600B383 RID: 45955 RVA: 0x00002139 File Offset: 0x00000339
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002CF6 RID: 11510
		// (get) Token: 0x0600B384 RID: 45956 RVA: 0x00247D2B File Offset: 0x00245F2B
		public ICollection<TKey> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x17002CF7 RID: 11511
		// (get) Token: 0x0600B385 RID: 45957 RVA: 0x00247D38 File Offset: 0x00245F38
		public ICollection<TValue> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x0600B386 RID: 45958 RVA: 0x000033E7 File Offset: 0x000015E7
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B387 RID: 45959 RVA: 0x000033E7 File Offset: 0x000015E7
		public void Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B388 RID: 45960 RVA: 0x000033E7 File Offset: 0x000015E7
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B389 RID: 45961 RVA: 0x00247D45 File Offset: 0x00245F45
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.dictionary.Contains(item);
		}

		// Token: 0x0600B38A RID: 45962 RVA: 0x00247D53 File Offset: 0x00245F53
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x0600B38B RID: 45963 RVA: 0x00247D61 File Offset: 0x00245F61
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600B38C RID: 45964 RVA: 0x00247D70 File Offset: 0x00245F70
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x0600B38D RID: 45965 RVA: 0x000033E7 File Offset: 0x000015E7
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B38E RID: 45966 RVA: 0x000033E7 File Offset: 0x000015E7
		public bool Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B38F RID: 45967 RVA: 0x00247D7D File Offset: 0x00245F7D
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600B390 RID: 45968 RVA: 0x00247D70 File Offset: 0x00245F70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x04005B8F RID: 23439
		private readonly IDictionary<TKey, TValue> dictionary;
	}
}
