using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000068 RID: 104
	[ImmutableObject(true)]
	[DebuggerDisplay("Count = {Count}")]
	internal abstract class ReadOnlyHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0000A280 File Offset: 0x00008480
		protected ReadOnlyHashTable(int capacity)
		{
			this._entries = new ReadOnlyHashTable<TKey, TValue>.Entry[capacity];
			this._buckets = new int[capacity];
			for (int i = 0; i < capacity; i++)
			{
				this._buckets[i] = -1;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000A2C0 File Offset: 0x000084C0
		internal int Count
		{
			get
			{
				return this._entries.Length;
			}
		}

		// Token: 0x060003D2 RID: 978
		protected abstract bool LookupEquals(TKey lookup, TKey entry);

		// Token: 0x060003D3 RID: 979
		protected abstract int GetHashCode(TKey item);

		// Token: 0x060003D4 RID: 980 RVA: 0x0000A2CC File Offset: 0x000084CC
		internal static ReadOnlyHashTable<TKey, TValue> Create(IList<KeyValuePair<TKey, TValue>> values, IEqualityComparer<TKey> comparer = null)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			IWiderLookupComparer<TKey> widerLookupComparer = comparer as IWiderLookupComparer<TKey>;
			if (widerLookupComparer != null)
			{
				return new ReadOnlyHashTable<TKey, TValue>.ReadOnlyWiderHashTable(values, widerLookupComparer);
			}
			return new ReadOnlyHashTable<TKey, TValue>.ReadOnlyGenericHashTable(values, comparer);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000A2FC File Offset: 0x000084FC
		internal bool TryGetValues(TKey lookupKey, out IList<TValue> values)
		{
			values = null;
			int num = this.GetHashCode(lookupKey) & int.MaxValue;
			for (int i = this._buckets[num % this._buckets.Length]; i >= 0; i = this._entries[i].Next)
			{
				ReadOnlyHashTable<TKey, TValue>.Entry entry = this._entries[i];
				if (entry.HashCode == num && this.LookupEquals(lookupKey, entry.Key))
				{
					if (values == null)
					{
						values = new List<TValue>();
					}
					values.Add(entry.Value);
				}
			}
			return values != null;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000A388 File Offset: 0x00008588
		protected void Populate(IList<KeyValuePair<TKey, TValue>> values)
		{
			int count = values.Count;
			for (int i = 0; i < count; i++)
			{
				this.Set(i, values[i].Key, values[i].Value);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000A3D0 File Offset: 0x000085D0
		private void Set(int index, TKey key, TValue value)
		{
			int num = this.GetHashCode(key) & int.MaxValue;
			int num2 = num % this._buckets.Length;
			this._entries[index].HashCode = num;
			this._entries[index].Key = key;
			this._entries[index].Value = value;
			this._entries[index].Next = this._buckets[num2];
			this._buckets[num2] = index;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000A44E File Offset: 0x0000864E
		internal ReadOnlyHashTable<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ReadOnlyHashTable<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000A456 File Offset: 0x00008656
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new ReadOnlyHashTable<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000A463 File Offset: 0x00008663
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ReadOnlyHashTable<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x040000D5 RID: 213
		private const int HashMask = 2147483647;

		// Token: 0x040000D6 RID: 214
		private readonly ReadOnlyHashTable<TKey, TValue>.Entry[] _entries;

		// Token: 0x040000D7 RID: 215
		private readonly int[] _buckets;

		// Token: 0x020000C2 RID: 194
		private struct Entry
		{
			// Token: 0x04000200 RID: 512
			internal int HashCode;

			// Token: 0x04000201 RID: 513
			internal int Next;

			// Token: 0x04000202 RID: 514
			internal TKey Key;

			// Token: 0x04000203 RID: 515
			internal TValue Value;
		}

		// Token: 0x020000C3 RID: 195
		internal struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x060005E5 RID: 1509 RVA: 0x0000F7DA File Offset: 0x0000D9DA
			internal Enumerator(ReadOnlyHashTable<TKey, TValue> parent)
			{
				this._parent = parent;
				this._index = 0;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000F7F6 File Offset: 0x0000D9F6
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000F7FE File Offset: 0x0000D9FE
			object IEnumerator.Current
			{
				get
				{
					return Util.ToKeyValuePair<TKey, TValue>(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x0000F820 File Offset: 0x0000DA20
			public void Dispose()
			{
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x0000F824 File Offset: 0x0000DA24
			public bool MoveNext()
			{
				if (this._index < this._parent.Count)
				{
					ReadOnlyHashTable<TKey, TValue>.Entry entry = this._parent._entries[this._index];
					this._current = Util.ToKeyValuePair<TKey, TValue>(entry.Key, entry.Value);
					this._index++;
					return true;
				}
				this._index = int.MaxValue;
				this._current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x0000F89A File Offset: 0x0000DA9A
			void IEnumerator.Reset()
			{
				this._index = 0;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x04000204 RID: 516
			private ReadOnlyHashTable<TKey, TValue> _parent;

			// Token: 0x04000205 RID: 517
			private int _index;

			// Token: 0x04000206 RID: 518
			private KeyValuePair<TKey, TValue> _current;
		}

		// Token: 0x020000C4 RID: 196
		[ImmutableObject(true)]
		private sealed class ReadOnlyGenericHashTable : ReadOnlyHashTable<TKey, TValue>
		{
			// Token: 0x060005EB RID: 1515 RVA: 0x0000F8AF File Offset: 0x0000DAAF
			internal ReadOnlyGenericHashTable(IList<KeyValuePair<TKey, TValue>> values, IEqualityComparer<TKey> comparer)
				: base(values.Count)
			{
				this._comparer = comparer;
				base.Populate(values);
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x0000F8CB File Offset: 0x0000DACB
			protected override bool LookupEquals(TKey lookup, TKey entry)
			{
				return this._comparer.Equals(lookup, entry);
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x0000F8DA File Offset: 0x0000DADA
			protected override int GetHashCode(TKey item)
			{
				return this._comparer.GetHashCode(item);
			}

			// Token: 0x04000207 RID: 519
			private readonly IEqualityComparer<TKey> _comparer;
		}

		// Token: 0x020000C5 RID: 197
		[ImmutableObject(true)]
		private sealed class ReadOnlyWiderHashTable : ReadOnlyHashTable<TKey, TValue>
		{
			// Token: 0x060005EE RID: 1518 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
			internal ReadOnlyWiderHashTable(IList<KeyValuePair<TKey, TValue>> values, IWiderLookupComparer<TKey> comparer)
				: base(values.Count)
			{
				this._comparer = comparer;
				base.Populate(values);
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x0000F904 File Offset: 0x0000DB04
			protected override bool LookupEquals(TKey lookup, TKey entry)
			{
				return this._comparer.LookupEquals(lookup, entry);
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x0000F913 File Offset: 0x0000DB13
			protected override int GetHashCode(TKey item)
			{
				return this._comparer.GetHashCode(item);
			}

			// Token: 0x04000208 RID: 520
			private readonly IWiderLookupComparer<TKey> _comparer;
		}
	}
}
