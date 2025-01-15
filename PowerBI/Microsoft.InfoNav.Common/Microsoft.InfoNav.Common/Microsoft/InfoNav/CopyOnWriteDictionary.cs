using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000C RID: 12
	[ImmutableObject(false)]
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class CopyOnWriteDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000029D3 File Offset: 0x00000BD3
		internal CopyOnWriteDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this._dictionary = CopyOnWriteDictionary<TKey, TValue>.Unwrap(dictionary);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000029E7 File Offset: 0x00000BE7
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000029F4 File Offset: 0x00000BF4
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000029F7 File Offset: 0x00000BF7
		public ICollection<TKey> Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002A04 File Offset: 0x00000C04
		public ICollection<TValue> Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x1700000A RID: 10
		public TValue this[TKey key]
		{
			get
			{
				return this._dictionary[key];
			}
			set
			{
				this.EnsureCopied();
				this._dictionary[key] = value;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002A34 File Offset: 0x00000C34
		public bool ContainsKey(TKey key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002A42 File Offset: 0x00000C42
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002A51 File Offset: 0x00000C51
		public void Add(TKey key, TValue value)
		{
			this.EnsureCopied();
			this._dictionary.Add(key, value);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002A66 File Offset: 0x00000C66
		public bool Remove(TKey key)
		{
			if (this._copied)
			{
				return this._dictionary.Remove(key);
			}
			if (!this._dictionary.ContainsKey(key))
			{
				return false;
			}
			this.EnsureCopied();
			return this._dictionary.Remove(key);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void Clear()
		{
			if (this._dictionary.Count == 0)
			{
				return;
			}
			if (this._copied)
			{
				this._dictionary.Clear();
				return;
			}
			this._dictionary = new Dictionary<TKey, TValue>(this._dictionary.Comparer);
			this._copied = true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002AEC File Offset: 0x00000CEC
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002AF9 File Offset: 0x00000CF9
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002B0B File Offset: 0x00000D0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002B1D File Offset: 0x00000D1D
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002B33 File Offset: 0x00000D33
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			this.EnsureCopied();
			return ((ICollection<KeyValuePair<TKey, TValue>>)this._dictionary).Remove(item);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002B47 File Offset: 0x00000D47
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this._dictionary).Contains(item);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002B55 File Offset: 0x00000D55
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this._dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002B64 File Offset: 0x00000D64
		private void EnsureCopied()
		{
			if (!this._copied)
			{
				this._dictionary = this.CreateCopy();
				this._copied = true;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002B84 File Offset: 0x00000D84
		private Dictionary<TKey, TValue> CreateCopy()
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(this._dictionary.Count + 4, this._dictionary.Comparer);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this._dictionary)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002C04 File Offset: 0x00000E04
		private static Dictionary<TKey, TValue> Unwrap(IDictionary<TKey, TValue> dictionary)
		{
			CopyOnWriteDictionary<TKey, TValue> copyOnWriteDictionary = dictionary as CopyOnWriteDictionary<TKey, TValue>;
			if (copyOnWriteDictionary != null)
			{
				dictionary = copyOnWriteDictionary._dictionary;
			}
			return (Dictionary<TKey, TValue>)dictionary;
		}

		// Token: 0x04000036 RID: 54
		private Dictionary<TKey, TValue> _dictionary;

		// Token: 0x04000037 RID: 55
		private bool _copied;
	}
}
