using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NLog.Internal
{
	// Token: 0x0200014A RID: 330
	internal class ThreadSafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000FDB RID: 4059 RVA: 0x00028C23 File Offset: 0x00026E23
		public ThreadSafeDictionary()
			: this(EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00028C30 File Offset: 0x00026E30
		public ThreadSafeDictionary(IEqualityComparer<TKey> comparer)
		{
			this._lockObject = new object();
			base..ctor();
			this._comparer = comparer;
			this._dict = new Dictionary<TKey, TValue>(this._comparer);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00028C5B File Offset: 0x00026E5B
		public ThreadSafeDictionary(ThreadSafeDictionary<TKey, TValue> source)
		{
			this._lockObject = new object();
			base..ctor();
			this._comparer = source._comparer;
			this._dict = source.GetReadOnlyDict();
			this.GetWritableDict(false);
		}

		// Token: 0x170002FF RID: 767
		public TValue this[TKey key]
		{
			get
			{
				return this.GetReadOnlyDict()[key];
			}
			set
			{
				object lockObject = this._lockObject;
				lock (lockObject)
				{
					this.GetWritableDict(false)[key] = value;
				}
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00028CE4 File Offset: 0x00026EE4
		public ICollection<TKey> Keys
		{
			get
			{
				return this.GetReadOnlyDict().Keys;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00028CF1 File Offset: 0x00026EF1
		public ICollection<TValue> Values
		{
			get
			{
				return this.GetReadOnlyDict().Values;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00028CFE File Offset: 0x00026EFE
		public int Count
		{
			get
			{
				return this.GetReadOnlyDict().Count;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00028D0B File Offset: 0x00026F0B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00028D10 File Offset: 0x00026F10
		public void Add(TKey key, TValue value)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this.GetWritableDict(false).Add(key, value);
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00028D58 File Offset: 0x00026F58
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this.GetWritableDict(false).Add(item.Key, item.Value);
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00028DAC File Offset: 0x00026FAC
		public void Clear()
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this.GetWritableDict(true);
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00028DF0 File Offset: 0x00026FF0
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.GetReadOnlyDict().Contains(item);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00028DFE File Offset: 0x00026FFE
		public bool ContainsKey(TKey key)
		{
			return this.GetReadOnlyDict().ContainsKey(key);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00028E0C File Offset: 0x0002700C
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this.GetReadOnlyDict()).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00028E1C File Offset: 0x0002701C
		public void CopyFrom(IDictionary<TKey, TValue> source)
		{
			if (this != source && source != null && source.Count > 0)
			{
				object lockObject = this._lockObject;
				lock (lockObject)
				{
					IDictionary<TKey, TValue> writableDict = this.GetWritableDict(false);
					foreach (KeyValuePair<TKey, TValue> keyValuePair in source)
					{
						writableDict[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00028EB4 File Offset: 0x000270B4
		public bool Remove(TKey key)
		{
			object lockObject = this._lockObject;
			bool flag2;
			lock (lockObject)
			{
				flag2 = this.GetWritableDict(false).Remove(key);
			}
			return flag2;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00028F00 File Offset: 0x00027100
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			object lockObject = this._lockObject;
			bool flag2;
			lock (lockObject)
			{
				flag2 = this.GetWritableDict(false).Remove(item);
			}
			return flag2;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00028F4C File Offset: 0x0002714C
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.GetReadOnlyDict().TryGetValue(key, out value);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00028F5B File Offset: 0x0002715B
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return this.GetReadOnlyDict().GetEnumerator();
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00028F6D File Offset: 0x0002716D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetReadOnlyDict().GetEnumerator();
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00028F7F File Offset: 0x0002717F
		public ThreadSafeDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ThreadSafeDictionary<TKey, TValue>.Enumerator(this.GetReadOnlyDict().GetEnumerator());
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00028F94 File Offset: 0x00027194
		private Dictionary<TKey, TValue> GetReadOnlyDict()
		{
			Dictionary<TKey, TValue> dictionary = this._dictReadOnly;
			if (dictionary == null)
			{
				object lockObject = this._lockObject;
				lock (lockObject)
				{
					dictionary = (this._dictReadOnly = this._dict);
				}
			}
			return dictionary;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00028FEC File Offset: 0x000271EC
		private IDictionary<TKey, TValue> GetWritableDict(bool clearDictionary = false)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(clearDictionary ? 0 : (this._dict.Count + 1), this._comparer);
			if (!clearDictionary)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this._dict)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			this._dict = dictionary;
			this._dictReadOnly = null;
			return dictionary;
		}

		// Token: 0x0400043F RID: 1087
		private readonly object _lockObject;

		// Token: 0x04000440 RID: 1088
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x04000441 RID: 1089
		private Dictionary<TKey, TValue> _dict;

		// Token: 0x04000442 RID: 1090
		private Dictionary<TKey, TValue> _dictReadOnly;

		// Token: 0x02000288 RID: 648
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x060016B0 RID: 5808 RVA: 0x0003BCAF File Offset: 0x00039EAF
			public Enumerator(Dictionary<TKey, TValue>.Enumerator enumerator)
			{
				this._enumerator = enumerator;
			}

			// Token: 0x17000430 RID: 1072
			// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0003BCB8 File Offset: 0x00039EB8
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x17000431 RID: 1073
			// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0003BCC5 File Offset: 0x00039EC5
			object IEnumerator.Current
			{
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x060016B3 RID: 5811 RVA: 0x0003BCD7 File Offset: 0x00039ED7
			public void Dispose()
			{
				this._enumerator.Dispose();
			}

			// Token: 0x060016B4 RID: 5812 RVA: 0x0003BCE4 File Offset: 0x00039EE4
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x060016B5 RID: 5813 RVA: 0x0003BCF1 File Offset: 0x00039EF1
			void IEnumerator.Reset()
			{
				((IEnumerator)this._enumerator).Reset();
			}

			// Token: 0x0400070C RID: 1804
			private Dictionary<TKey, TValue>.Enumerator _enumerator;
		}
	}
}
