using System;
using System.Collections;
using System.Collections.Generic;

namespace NLog.Internal
{
	// Token: 0x02000112 RID: 274
	internal class DictionaryAdapter<TKey, TValue> : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x0002434F File Offset: 0x0002254F
		public DictionaryAdapter(IDictionary<TKey, TValue> implementation)
		{
			this._implementation = implementation;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0002435E File Offset: 0x0002255E
		public ICollection Values
		{
			get
			{
				return new List<TValue>(this._implementation.Values);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00024370 File Offset: 0x00022570
		public int Count
		{
			get
			{
				return this._implementation.Count;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0002437D File Offset: 0x0002257D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00024380 File Offset: 0x00022580
		public object SyncRoot
		{
			get
			{
				return this._implementation;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00024388 File Offset: 0x00022588
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002438B File Offset: 0x0002258B
		public bool IsReadOnly
		{
			get
			{
				return this._implementation.IsReadOnly;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00024398 File Offset: 0x00022598
		public ICollection Keys
		{
			get
			{
				return new List<TKey>(this._implementation.Keys);
			}
		}

		// Token: 0x170002CE RID: 718
		public object this[object key]
		{
			get
			{
				TValue tvalue;
				if (this._implementation.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				this._implementation[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000243EF File Offset: 0x000225EF
		public void Add(object key, object value)
		{
			this._implementation.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00024408 File Offset: 0x00022608
		public void Clear()
		{
			this._implementation.Clear();
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00024415 File Offset: 0x00022615
		public bool Contains(object key)
		{
			return this._implementation.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00024428 File Offset: 0x00022628
		public IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryAdapter<TKey, TValue>.MyEnumerator(this._implementation.GetEnumerator());
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002443A File Offset: 0x0002263A
		public void Remove(object key)
		{
			this._implementation.Remove((TKey)((object)key));
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002444E File Offset: 0x0002264E
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00024455 File Offset: 0x00022655
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003E9 RID: 1001
		private readonly IDictionary<TKey, TValue> _implementation;

		// Token: 0x02000266 RID: 614
		private class MyEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06001609 RID: 5641 RVA: 0x00039F10 File Offset: 0x00038110
			public MyEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> wrapped)
			{
				this._wrapped = wrapped;
			}

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x0600160A RID: 5642 RVA: 0x00039F20 File Offset: 0x00038120
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._wrapped.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this._wrapped.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x0600160B RID: 5643 RVA: 0x00039F64 File Offset: 0x00038164
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._wrapped.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x0600160C RID: 5644 RVA: 0x00039F8C File Offset: 0x0003818C
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._wrapped.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x0600160D RID: 5645 RVA: 0x00039FB1 File Offset: 0x000381B1
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x0600160E RID: 5646 RVA: 0x00039FBE File Offset: 0x000381BE
			public bool MoveNext()
			{
				return this._wrapped.MoveNext();
			}

			// Token: 0x0600160F RID: 5647 RVA: 0x00039FCB File Offset: 0x000381CB
			public void Reset()
			{
				this._wrapped.Reset();
			}

			// Token: 0x040006A0 RID: 1696
			private readonly IEnumerator<KeyValuePair<TKey, TValue>> _wrapped;
		}
	}
}
