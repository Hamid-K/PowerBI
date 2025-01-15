using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableSortedDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, ISortKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x000079FD File Offset: 0x00005BFD
		internal ImmutableSortedDictionary([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer = null, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer = null)
		{
			this._keyComparer = keyComparer ?? Comparer<TKey>.Default;
			this._valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			this._root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007A30 File Offset: 0x00005C30
		private ImmutableSortedDictionary(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
			Requires.Range(count >= 0, "count", null);
			Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			root.Freeze();
			this._root = root;
			this._count = count;
			this._keyComparer = keyComparer;
			this._valueComparer = valueComparer;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007A9A File Offset: 0x00005C9A
		public ImmutableSortedDictionary<TKey, TValue> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(this._keyComparer, this._valueComparer);
			}
			return this;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00007AC1 File Offset: 0x00005CC1
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._valueComparer;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00007AC9 File Offset: 0x00005CC9
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00007AD6 File Offset: 0x00005CD6
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00007ADE File Offset: 0x00005CDE
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this._root.Keys;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00007AEB File Offset: 0x00005CEB
		public IEnumerable<TValue> Values
		{
			get
			{
				return this._root.Values;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00007AF8 File Offset: 0x00005CF8
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00007B00 File Offset: 0x00005D00
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00007B08 File Offset: 0x00005D08
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00007B10 File Offset: 0x00005D10
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00007B13 File Offset: 0x00005D13
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00007B1B File Offset: 0x00005D1B
		[Nullable(new byte[] { 1, 0, 0 })]
		internal ImmutableSortedDictionary<TKey, TValue>.Node Root
		{
			[return: Nullable(new byte[] { 1, 0, 0 })]
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000073 RID: 115
		public TValue this[TKey key]
		{
			get
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
			}
		}

		// Token: 0x17000074 RID: 116
		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get
			{
				return this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00007B75 File Offset: 0x00005D75
		[return: Nullable(new byte[] { 1, 0, 0 })]
		public ImmutableSortedDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableSortedDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00007B80 File Offset: 0x00005D80
		public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
			return this.Wrap(node, this._count + 1);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00007BC4 File Offset: 0x00005DC4
		public ImmutableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			bool flag2;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
			return this.Wrap(node, flag ? this._count : (this._count + 1));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00007C14 File Offset: 0x00005E14
		public ImmutableSortedDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, true, false);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007C2A File Offset: 0x00005E2A
		public ImmutableSortedDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, false, false);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00007C40 File Offset: 0x00005E40
		public ImmutableSortedDictionary<TKey, TValue> Remove(TKey value)
		{
			Requires.NotNullAllowStructs<TKey>(value, "value");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.Remove(value, this._keyComparer, out flag);
			return this.Wrap(node, this._count - 1);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00007C7C File Offset: 0x00005E7C
		public ImmutableSortedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (TKey tkey in keys)
			{
				bool flag;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = node.Remove(tkey, this._keyComparer, out flag);
				if (flag)
				{
					node = node2;
					num--;
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007CFC File Offset: 0x00005EFC
		public ImmutableSortedDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = Comparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (keyComparer != this._keyComparer)
			{
				ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode, 0, keyComparer, valueComparer);
				return immutableSortedDictionary.AddRange(this, false, true);
			}
			if (valueComparer == this._valueComparer)
			{
				return this;
			}
			return new ImmutableSortedDictionary<TKey, TValue>(this._root, this._count, this._keyComparer, valueComparer);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007D63 File Offset: 0x00005F63
		public ImmutableSortedDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._valueComparer);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00007D72 File Offset: 0x00005F72
		public bool ContainsValue(TValue value)
		{
			return this._root.ContainsValue(value, this._valueComparer);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007D86 File Offset: 0x00005F86
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00007D90 File Offset: 0x00005F90
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007D9A File Offset: 0x00005F9A
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00007DA3 File Offset: 0x00005FA3
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007DAC File Offset: 0x00005FAC
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00007DB5 File Offset: 0x00005FB5
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007DBE File Offset: 0x00005FBE
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ContainsKey(key, this._keyComparer);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007DDD File Offset: 0x00005FDD
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair)
		{
			return this._root.Contains(pair, this._keyComparer, this._valueComparer);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007DF7 File Offset: 0x00005FF7
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.TryGetValue(key, this._keyComparer, out value);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007E17 File Offset: 0x00006017
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return this._root.TryGetKey(equalKey, this._keyComparer, out actualKey);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007E37 File Offset: 0x00006037
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007E3E File Offset: 0x0000603E
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00007E45 File Offset: 0x00006045
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007E4C File Offset: 0x0000604C
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00007E53 File Offset: 0x00006053
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00007E5C File Offset: 0x0000605C
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00007EE8 File Offset: 0x000060E8
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00007EEB File Offset: 0x000060EB
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00007EEE File Offset: 0x000060EE
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00007EF6 File Offset: 0x000060F6
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00007EFE File Offset: 0x000060FE
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00007F05 File Offset: 0x00006105
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00007F13 File Offset: 0x00006113
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007F25 File Offset: 0x00006125
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000079 RID: 121
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			get
			{
				return this[(TKey)((object)key)];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00007F46 File Offset: 0x00006146
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00007F4D File Offset: 0x0000614D
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index, this.Count);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00007F62 File Offset: 0x00006162
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00007F65 File Offset: 0x00006165
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00007F68 File Offset: 0x00006168
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00007F95 File Offset: 0x00006195
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00007FA2 File Offset: 0x000061A2
		[NullableContext(0)]
		public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00007FAF File Offset: 0x000061AF
		private static ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, count, keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007FD0 File Offset: 0x000061D0
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, [NotNullWhen(true)] out ImmutableSortedDictionary<TKey, TValue> other)
		{
			other = sequence as ImmutableSortedDictionary<TKey, TValue>;
			if (other != null)
			{
				return true;
			}
			ImmutableSortedDictionary<TKey, TValue>.Builder builder = sequence as ImmutableSortedDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008000 File Offset: 0x00006200
		private ImmutableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision, bool avoidToSortedMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			if (this.IsEmpty && !avoidToSortedMap)
			{
				return this.FillFromEmpty(items, overwriteOnCollision);
			}
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				bool flag = false;
				bool flag2;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = (overwriteOnCollision ? node.SetItem(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag, out flag2) : node.Add(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag2));
				if (flag2)
				{
					node = node2;
					if (!flag)
					{
						num++;
					}
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000080D8 File Offset: 0x000062D8
		private ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int adjustedCountIfDifferentRoot)
		{
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, adjustedCountIfDifferentRoot, this._keyComparer, this._valueComparer);
			}
			return this.Clear();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008108 File Offset: 0x00006308
		private ImmutableSortedDictionary<TKey, TValue> FillFromEmpty(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary;
			if (ImmutableSortedDictionary<TKey, TValue>.TryCastToImmutableMap(items, out immutableSortedDictionary))
			{
				return immutableSortedDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			IDictionary<TKey, TValue> dictionary = items as IDictionary<TKey, TValue>;
			SortedDictionary<TKey, TValue> sortedDictionary;
			if (dictionary != null)
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(dictionary, this.KeyComparer);
			}
			else
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(this.KeyComparer);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					TValue tvalue;
					if (overwriteOnCollision)
					{
						sortedDictionary[keyValuePair.Key] = keyValuePair.Value;
					}
					else if (sortedDictionary.TryGetValue(keyValuePair.Key, out tvalue))
					{
						if (!this._valueComparer.Equals(tvalue, keyValuePair.Value))
						{
							throw new ArgumentException(SR.Format(SR.DuplicateKey, keyValuePair.Key));
						}
					}
					else
					{
						sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			if (sortedDictionary.Count == 0)
			{
				return this;
			}
			ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromSortedDictionary(sortedDictionary);
			return new ImmutableSortedDictionary<TKey, TValue>(node, sortedDictionary.Count, this.KeyComparer, this.ValueComparer);
		}

		// Token: 0x0400002E RID: 46
		public static readonly ImmutableSortedDictionary<TKey, TValue> Empty = new ImmutableSortedDictionary<TKey, TValue>(null, null);

		// Token: 0x0400002F RID: 47
		private readonly ImmutableSortedDictionary<TKey, TValue>.Node _root;

		// Token: 0x04000030 RID: 48
		private readonly int _count;

		// Token: 0x04000031 RID: 49
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x04000032 RID: 50
		private readonly IEqualityComparer<TValue> _valueComparer;

		// Token: 0x02000073 RID: 115
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x06000565 RID: 1381 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
			internal Builder(ImmutableSortedDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._keyComparer = map.KeyComparer;
				this._valueComparer = map.ValueComparer;
				this._count = map.Count;
				this._immutable = map;
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000E64E File Offset: 0x0000C84E
			ICollection<TKey> IDictionary<TKey, TValue>.Keys
			{
				get
				{
					return this.Root.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000E666 File Offset: 0x0000C866
			public IEnumerable<TKey> Keys
			{
				get
				{
					return this.Root.Keys;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000E673 File Offset: 0x0000C873
			ICollection<TValue> IDictionary<TKey, TValue>.Values
			{
				get
				{
					return this.Root.Values.ToArray(this.Count);
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000E68B File Offset: 0x0000C88B
			public IEnumerable<TValue> Values
			{
				get
				{
					return this.Root.Values;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000E698 File Offset: 0x0000C898
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
			bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000E6A3 File Offset: 0x0000C8A3
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000E6AB File Offset: 0x0000C8AB
			// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000E6B3 File Offset: 0x0000C8B3
			[Nullable(new byte[] { 1, 0, 0 })]
			private ImmutableSortedDictionary<TKey, TValue>.Node Root
			{
				get
				{
					return this._root;
				}
				set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x17000110 RID: 272
			public TValue this[TKey key]
			{
				get
				{
					TValue tvalue;
					if (this.TryGetValue(key, out tvalue))
					{
						return tvalue;
					}
					throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
				}
				set
				{
					bool flag;
					bool flag2;
					this.Root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
					if (flag2 && !flag)
					{
						this._count++;
					}
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000E758 File Offset: 0x0000C958
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000E75B File Offset: 0x0000C95B
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000E75E File Offset: 0x0000C95E
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000E771 File Offset: 0x0000C971
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000E784 File Offset: 0x0000C984
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
					return this._syncRoot;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000E7A6 File Offset: 0x0000C9A6
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000E7A9 File Offset: 0x0000C9A9
			// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
			public IComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
				set
				{
					Requires.NotNull<IComparer<TKey>>(value, "value");
					if (value != this._keyComparer)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
						int num = 0;
						foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
						{
							bool flag;
							node = node.Add(keyValuePair.Key, keyValuePair.Value, value, this._valueComparer, out flag);
							if (flag)
							{
								num++;
							}
						}
						this._keyComparer = value;
						this.Root = node;
						this._count = num;
					}
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000E854 File Offset: 0x0000CA54
			// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000E85C File Offset: 0x0000CA5C
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this._valueComparer)
					{
						this._valueComparer = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x0000E880 File Offset: 0x0000CA80
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x0000E894 File Offset: 0x0000CA94
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x0000E8A2 File Offset: 0x0000CAA2
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x17000119 RID: 281
			[Nullable(2)]
			object IDictionary.this[object key]
			{
				get
				{
					return this[(TKey)((object)key)];
				}
				set
				{
					this[(TKey)((object)key)] = (TValue)((object)value);
				}
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x0000E8EA File Offset: 0x0000CAEA
			void ICollection.CopyTo(Array array, int index)
			{
				this.Root.CopyTo(array, index, this.Count);
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x0000E900 File Offset: 0x0000CB00
			public void Add(TKey key, TValue value)
			{
				bool flag;
				this.Root = this.Root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
				if (flag)
				{
					this._count++;
				}
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x0000E93F File Offset: 0x0000CB3F
			public bool ContainsKey(TKey key)
			{
				return this.Root.ContainsKey(key, this._keyComparer);
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x0000E954 File Offset: 0x0000CB54
			public bool Remove(TKey key)
			{
				bool flag;
				this.Root = this.Root.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					this._count--;
				}
				return flag;
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x0000E98D File Offset: 0x0000CB8D
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return this.Root.TryGetValue(key, this._keyComparer, out value);
			}

			// Token: 0x06000586 RID: 1414 RVA: 0x0000E9A2 File Offset: 0x0000CBA2
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				return this.Root.TryGetKey(equalKey, this._keyComparer, out actualKey);
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x0000E9C2 File Offset: 0x0000CBC2
			public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
			public void Clear()
			{
				this.Root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				this._count = 0;
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x0000E9EC File Offset: 0x0000CBEC
			public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Root.Contains(item, this._keyComparer, this._valueComparer);
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x0000EA06 File Offset: 0x0000CC06
			void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex, this.Count);
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x0000EA1B File Offset: 0x0000CC1B
			public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x0000EA35 File Offset: 0x0000CC35
			[return: Nullable(new byte[] { 0, 1, 1 })]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x0600058D RID: 1421 RVA: 0x0000EA43 File Offset: 0x0000CC43
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x0000EA50 File Offset: 0x0000CC50
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x0000EA5D File Offset: 0x0000CC5D
			public bool ContainsValue(TValue value)
			{
				return this._root.ContainsValue(value, this._valueComparer);
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x0000EA74 File Offset: 0x0000CC74
			public void AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					this.Add(keyValuePair);
				}
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey tkey in keys)
				{
					this.Remove(tkey);
				}
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0000EB1C File Offset: 0x0000CD1C
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0000EB3C File Offset: 0x0000CD3C
			public TValue GetValueOrDefault(TKey key, TValue defaultValue)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				return defaultValue;
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x0000EB62 File Offset: 0x0000CD62
			public ImmutableSortedDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedDictionary<TKey, TValue>.Wrap(this.Root, this._count, this._keyComparer, this._valueComparer);
				}
				return this._immutable;
			}

			// Token: 0x040000CE RID: 206
			private ImmutableSortedDictionary<TKey, TValue>.Node _root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;

			// Token: 0x040000CF RID: 207
			private IComparer<TKey> _keyComparer = Comparer<TKey>.Default;

			// Token: 0x040000D0 RID: 208
			private IEqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;

			// Token: 0x040000D1 RID: 209
			private int _count;

			// Token: 0x040000D2 RID: 210
			private ImmutableSortedDictionary<TKey, TValue> _immutable;

			// Token: 0x040000D3 RID: 211
			private int _version;

			// Token: 0x040000D4 RID: 212
			private object _syncRoot;
		}

		// Token: 0x02000074 RID: 116
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x0000EB98 File Offset: 0x0000CD98
			internal Enumerator([Nullable(new byte[] { 1, 0, 0 })] ImmutableSortedDictionary<TKey, TValue>.Node root, [Nullable(new byte[] { 2, 0, 0 })] ImmutableSortedDictionary<TKey, TValue>.Builder builder = null)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000EC3B File Offset: 0x0000CE3B
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000EC5C File Offset: 0x0000CE5C
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000EC64 File Offset: 0x0000CE64
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x0000EC74 File Offset: 0x0000CE74
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x0000ECCC File Offset: 0x0000CECC
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x0000ED2C File Offset: 0x0000CF2C
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._current = null;
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x0000ED89 File Offset: 0x0000CF89
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this);
				}
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x0000EDDC File Offset: 0x0000CFDC
			private void PushLeft(ImmutableSortedDictionary<TKey, TValue>.Node node)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>(node));
					node = node.Left;
				}
			}

			// Token: 0x040000D5 RID: 213
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>();

			// Token: 0x040000D6 RID: 214
			private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x040000D7 RID: 215
			private readonly int _poolUserId;

			// Token: 0x040000D8 RID: 216
			private ImmutableSortedDictionary<TKey, TValue>.Node _root;

			// Token: 0x040000D9 RID: 217
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>> _stack;

			// Token: 0x040000DA RID: 218
			private ImmutableSortedDictionary<TKey, TValue>.Node _current;

			// Token: 0x040000DB RID: 219
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000075 RID: 117
		[Nullable(0)]
		[DebuggerDisplay("{_key} = {_value}")]
		internal sealed class Node : IBinaryTree<KeyValuePair<TKey, TValue>>, IBinaryTree, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x060005A0 RID: 1440 RVA: 0x0000EE2B File Offset: 0x0000D02B
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x0000EE3C File Offset: 0x0000D03C
			private Node(TKey key, TValue value, ImmutableSortedDictionary<TKey, TValue>.Node left, ImmutableSortedDictionary<TKey, TValue>.Node right, bool frozen = false)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(right, "right");
				this._key = key;
				this._value = value;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._frozen = frozen;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000EEB1 File Offset: 0x0000D0B1
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000EEBC File Offset: 0x0000D0BC
			[Nullable(new byte[] { 2, 0, 1, 1 })]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
			[Nullable(new byte[] { 2, 0, 1, 1 })]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000EECC File Offset: 0x0000D0CC
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000EED4 File Offset: 0x0000D0D4
			[Nullable(new byte[] { 2, 0, 0 })]
			public ImmutableSortedDictionary<TKey, TValue>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000EEDC File Offset: 0x0000D0DC
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
			[Nullable(new byte[] { 2, 0, 0 })]
			public ImmutableSortedDictionary<TKey, TValue>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000EEEC File Offset: 0x0000D0EC
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Value
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000EF07 File Offset: 0x0000D107
			int IBinaryTree.Count
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000EF0E File Offset: 0x0000D10E
			internal IEnumerable<TKey> Keys
			{
				get
				{
					return this.Select((KeyValuePair<TKey, TValue> p) => p.Key);
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000EF35 File Offset: 0x0000D135
			internal IEnumerable<TValue> Values
			{
				get
				{
					return this.Select((KeyValuePair<TKey, TValue> p) => p.Value);
				}
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x0000EF5C File Offset: 0x0000D15C
			[NullableContext(0)]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, null);
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0000EF65 File Offset: 0x0000D165
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0000EF72 File Offset: 0x0000D172
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x0000EF7F File Offset: 0x0000D17F
			[NullableContext(0)]
			internal ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0, 0 })] ImmutableSortedDictionary<TKey, TValue>.Builder builder)
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, builder);
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0000EF88 File Offset: 0x0000D188
			internal void CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x0000F010 File Offset: 0x0000D210
			internal void CopyTo(Array array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), new int[] { arrayIndex++ });
				}
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromSortedDictionary(SortedDictionary<TKey, TValue> dictionary)
			{
				Requires.NotNull<SortedDictionary<TKey, TValue>>(dictionary, "dictionary");
				IOrderedCollection<KeyValuePair<TKey, TValue>> orderedCollection = dictionary.AsOrderedCollection<KeyValuePair<TKey, TValue>>();
				return ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Add(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				bool flag;
				return this.SetOrAdd(key, value, keyComparer, valueComparer, false, out flag, out mutated);
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0000F12F File Offset: 0x0000D32F
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node SetItem(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				return this.SetOrAdd(key, value, keyComparer, valueComparer, true, out replacedExistingValue, out mutated);
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0000F163 File Offset: 0x0000D363
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Remove(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return this.RemoveRecursive(key, keyComparer, out mutated);
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0000F184 File Offset: 0x0000D384
			internal bool TryGetValue(TKey key, IComparer<TKey> keyComparer, [MaybeNullWhen(false)] out TValue value)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				value = node._value;
				return true;
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
			internal bool TryGetKey(TKey equalKey, IComparer<TKey> keyComparer, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(equalKey, keyComparer);
				if (node.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				actualKey = node._key;
				return true;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0000F21A File Offset: 0x0000D41A
			internal bool ContainsKey(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return !this.Search(key, keyComparer).IsEmpty;
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0000F244 File Offset: 0x0000D444
			internal bool ContainsValue(TValue value, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (valueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
			internal bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNullAllowStructs<TKey>(pair.Key, "Key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(pair.Key, keyComparer);
				return !node.IsEmpty && valueComparer.Equals(node._value, pair.Value);
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0000F310 File Offset: 0x0000D510
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0000F338 File Offset: 0x0000D538
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0000F37C File Offset: 0x0000D57C
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = tree.Mutate(null, ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree._right));
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(node);
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0000F400 File Offset: 0x0000D600
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = tree.Mutate(ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree._left), null);
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(node);
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0000F440 File Offset: 0x0000D640
			private static int Balance(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0000F464 File Offset: 0x0000D664
			private static bool IsRightHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) >= 2;
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0000F47D File Offset: 0x0000D67D
			private static bool IsLeftHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) <= -2;
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0000F498 File Offset: 0x0000D698
			private static ImmutableSortedDictionary<TKey, TValue>.Node MakeBalanced(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (ImmutableSortedDictionary<TKey, TValue>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x0000F4FC File Offset: 0x0000D6FC
			private static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromList(IOrderedCollection<KeyValuePair<TKey, TValue>> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<KeyValuePair<TKey, TValue>>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				KeyValuePair<TKey, TValue> keyValuePair = items[start + num2];
				return new ImmutableSortedDictionary<TKey, TValue>.Node(keyValuePair.Key, keyValuePair.Value, node, node2, true);
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x0000F584 File Offset: 0x0000D784
			private ImmutableSortedDictionary<TKey, TValue>.Node SetOrAdd(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
			{
				replacedExistingValue = false;
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this, this, false);
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node2 = this._right.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node2);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node3 = this._left.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(node3, null);
					}
				}
				else
				{
					if (valueComparer.Equals(this._value, value))
					{
						mutated = false;
						return this;
					}
					if (!overwriteExistingValue)
					{
						throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
					}
					mutated = true;
					replacedExistingValue = true;
					node = new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this._left, this._right, false);
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x0000F674 File Offset: 0x0000D874
			private ImmutableSortedDictionary<TKey, TValue>.Node RemoveRecursive(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
					}
					else if (this._right.IsEmpty && !this._left.IsEmpty)
					{
						node = this._left;
					}
					else if (!this._right.IsEmpty && this._left.IsEmpty)
					{
						node = this._right;
					}
					else
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedDictionary<TKey, TValue>.Node node3 = this._right.Remove(node2._key, keyComparer, out flag);
						node = node2.Mutate(this._left, node3);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node4 = this._left.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(node4, null);
					}
				}
				else
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node5 = this._right.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node5);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
			private ImmutableSortedDictionary<TKey, TValue>.Node Mutate(ImmutableSortedDictionary<TKey, TValue>.Node left = null, ImmutableSortedDictionary<TKey, TValue>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedDictionary<TKey, TValue>.Node(this._key, this._value, left ?? this._left, right ?? this._right, false);
				}
				if (left != null)
				{
					this._left = left;
				}
				if (right != null)
				{
					this._right = right;
				}
				this._height = checked(1 + Math.Max(this._left._height, this._right._height));
				return this;
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0000F828 File Offset: 0x0000DA28
			private ImmutableSortedDictionary<TKey, TValue>.Node Search(TKey key, IComparer<TKey> keyComparer)
			{
				if (this.IsEmpty)
				{
					return this;
				}
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, keyComparer);
				}
				return this._left.Search(key, keyComparer);
			}

			// Token: 0x040000DC RID: 220
			[Nullable(new byte[] { 1, 0, 0 })]
			internal static readonly ImmutableSortedDictionary<TKey, TValue>.Node EmptyNode = new ImmutableSortedDictionary<TKey, TValue>.Node();

			// Token: 0x040000DD RID: 221
			private readonly TKey _key;

			// Token: 0x040000DE RID: 222
			private readonly TValue _value;

			// Token: 0x040000DF RID: 223
			private bool _frozen;

			// Token: 0x040000E0 RID: 224
			private byte _height;

			// Token: 0x040000E1 RID: 225
			private ImmutableSortedDictionary<TKey, TValue>.Node _left;

			// Token: 0x040000E2 RID: 226
			private ImmutableSortedDictionary<TKey, TValue>.Node _right;
		}
	}
}
