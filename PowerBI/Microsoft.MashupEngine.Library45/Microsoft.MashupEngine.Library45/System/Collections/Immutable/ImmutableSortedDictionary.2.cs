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
	// Token: 0x020020B2 RID: 8370
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableSortedDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, ISortKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x06011828 RID: 71720 RVA: 0x003BFA18 File Offset: 0x003BDC18
		internal ImmutableSortedDictionary([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer = null, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer = null)
		{
			this._keyComparer = keyComparer ?? Comparer<TKey>.Default;
			this._valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			this._root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
		}

		// Token: 0x06011829 RID: 71721 RVA: 0x003BFA4C File Offset: 0x003BDC4C
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

		// Token: 0x0601182A RID: 71722 RVA: 0x003BFAB6 File Offset: 0x003BDCB6
		public ImmutableSortedDictionary<TKey, TValue> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(this._keyComparer, this._valueComparer);
			}
			return this;
		}

		// Token: 0x17002ED7 RID: 11991
		// (get) Token: 0x0601182B RID: 71723 RVA: 0x003BFADD File Offset: 0x003BDCDD
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._valueComparer;
			}
		}

		// Token: 0x17002ED8 RID: 11992
		// (get) Token: 0x0601182C RID: 71724 RVA: 0x003BFAE5 File Offset: 0x003BDCE5
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x17002ED9 RID: 11993
		// (get) Token: 0x0601182D RID: 71725 RVA: 0x003BFAF2 File Offset: 0x003BDCF2
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17002EDA RID: 11994
		// (get) Token: 0x0601182E RID: 71726 RVA: 0x003BFAFA File Offset: 0x003BDCFA
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this._root.Keys;
			}
		}

		// Token: 0x17002EDB RID: 11995
		// (get) Token: 0x0601182F RID: 71727 RVA: 0x003BFB07 File Offset: 0x003BDD07
		public IEnumerable<TValue> Values
		{
			get
			{
				return this._root.Values;
			}
		}

		// Token: 0x06011830 RID: 71728 RVA: 0x003BFB14 File Offset: 0x003BDD14
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17002EDC RID: 11996
		// (get) Token: 0x06011831 RID: 71729 RVA: 0x003B9FC1 File Offset: 0x003B81C1
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17002EDD RID: 11997
		// (get) Token: 0x06011832 RID: 71730 RVA: 0x003B9FC9 File Offset: 0x003B81C9
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17002EDE RID: 11998
		// (get) Token: 0x06011833 RID: 71731 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002EDF RID: 11999
		// (get) Token: 0x06011834 RID: 71732 RVA: 0x003BFB1C File Offset: 0x003BDD1C
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x17002EE0 RID: 12000
		// (get) Token: 0x06011835 RID: 71733 RVA: 0x003BFB24 File Offset: 0x003BDD24
		[Nullable(new byte[] { 1, 0, 0 })]
		internal ImmutableSortedDictionary<TKey, TValue>.Node Root
		{
			[return: Nullable(new byte[] { 1, 0, 0 })]
			get
			{
				return this._root;
			}
		}

		// Token: 0x17002EE1 RID: 12001
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

		// Token: 0x06011837 RID: 71735 RVA: 0x003BFB6D File Offset: 0x003BDD6D
		public readonly ref TValue ValueRef(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ValueRef(key, this._keyComparer);
		}

		// Token: 0x17002EE2 RID: 12002
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

		// Token: 0x0601183A RID: 71738 RVA: 0x003BFB95 File Offset: 0x003BDD95
		[return: Nullable(new byte[] { 1, 0, 0 })]
		public ImmutableSortedDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableSortedDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x0601183B RID: 71739 RVA: 0x003BFBA0 File Offset: 0x003BDDA0
		public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
			return this.Wrap(node, this._count + 1);
		}

		// Token: 0x0601183C RID: 71740 RVA: 0x003BFBE4 File Offset: 0x003BDDE4
		public ImmutableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			bool flag2;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
			return this.Wrap(node, flag ? this._count : (this._count + 1));
		}

		// Token: 0x0601183D RID: 71741 RVA: 0x003BFC34 File Offset: 0x003BDE34
		public ImmutableSortedDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, true, false);
		}

		// Token: 0x0601183E RID: 71742 RVA: 0x003BFC4A File Offset: 0x003BDE4A
		public ImmutableSortedDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, false, false);
		}

		// Token: 0x0601183F RID: 71743 RVA: 0x003BFC60 File Offset: 0x003BDE60
		public ImmutableSortedDictionary<TKey, TValue> Remove(TKey value)
		{
			Requires.NotNullAllowStructs<TKey>(value, "value");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.Remove(value, this._keyComparer, out flag);
			return this.Wrap(node, this._count - 1);
		}

		// Token: 0x06011840 RID: 71744 RVA: 0x003BFC9C File Offset: 0x003BDE9C
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

		// Token: 0x06011841 RID: 71745 RVA: 0x003BFD1C File Offset: 0x003BDF1C
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

		// Token: 0x06011842 RID: 71746 RVA: 0x003BFD83 File Offset: 0x003BDF83
		public ImmutableSortedDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._valueComparer);
		}

		// Token: 0x06011843 RID: 71747 RVA: 0x003BFD92 File Offset: 0x003BDF92
		public bool ContainsValue(TValue value)
		{
			return this._root.ContainsValue(value, this._valueComparer);
		}

		// Token: 0x06011844 RID: 71748 RVA: 0x003BFDA6 File Offset: 0x003BDFA6
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x06011845 RID: 71749 RVA: 0x003BFDB0 File Offset: 0x003BDFB0
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x06011846 RID: 71750 RVA: 0x003BFDBA File Offset: 0x003BDFBA
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x06011847 RID: 71751 RVA: 0x003BFDC3 File Offset: 0x003BDFC3
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x06011848 RID: 71752 RVA: 0x003BFDCC File Offset: 0x003BDFCC
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x06011849 RID: 71753 RVA: 0x003BFDD5 File Offset: 0x003BDFD5
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x0601184A RID: 71754 RVA: 0x003BFDDE File Offset: 0x003BDFDE
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ContainsKey(key, this._keyComparer);
		}

		// Token: 0x0601184B RID: 71755 RVA: 0x003BFDFD File Offset: 0x003BDFFD
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair)
		{
			return this._root.Contains(pair, this._keyComparer, this._valueComparer);
		}

		// Token: 0x0601184C RID: 71756 RVA: 0x003BFE17 File Offset: 0x003BE017
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.TryGetValue(key, this._keyComparer, out value);
		}

		// Token: 0x0601184D RID: 71757 RVA: 0x003BFE37 File Offset: 0x003BE037
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return this._root.TryGetKey(equalKey, this._keyComparer, out actualKey);
		}

		// Token: 0x0601184E RID: 71758 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601184F RID: 71759 RVA: 0x00002C72 File Offset: 0x00000E72
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011850 RID: 71760 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011851 RID: 71761 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011852 RID: 71762 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011853 RID: 71763 RVA: 0x003BFE58 File Offset: 0x003BE058
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

		// Token: 0x17002EE3 RID: 12003
		// (get) Token: 0x06011854 RID: 71764 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002EE4 RID: 12004
		// (get) Token: 0x06011855 RID: 71765 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002EE5 RID: 12005
		// (get) Token: 0x06011856 RID: 71766 RVA: 0x003B9FC1 File Offset: 0x003B81C1
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17002EE6 RID: 12006
		// (get) Token: 0x06011857 RID: 71767 RVA: 0x003B9FC9 File Offset: 0x003B81C9
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x06011858 RID: 71768 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011859 RID: 71769 RVA: 0x003BFEE4 File Offset: 0x003BE0E4
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x0601185A RID: 71770 RVA: 0x003BFEF2 File Offset: 0x003BE0F2
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x0601185B RID: 71771 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002EE7 RID: 12007
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

		// Token: 0x0601185E RID: 71774 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601185F RID: 71775 RVA: 0x003BFF17 File Offset: 0x003BE117
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index, this.Count);
		}

		// Token: 0x17002EE8 RID: 12008
		// (get) Token: 0x06011860 RID: 71776 RVA: 0x00004FAE File Offset: 0x000031AE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002EE9 RID: 12009
		// (get) Token: 0x06011861 RID: 71777 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06011862 RID: 71778 RVA: 0x003BFF2C File Offset: 0x003BE12C
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x06011863 RID: 71779 RVA: 0x003BFF59 File Offset: 0x003BE159
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06011864 RID: 71780 RVA: 0x003BFF66 File Offset: 0x003BE166
		[NullableContext(0)]
		public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x06011865 RID: 71781 RVA: 0x003BFF73 File Offset: 0x003BE173
		private static ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, count, keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x06011866 RID: 71782 RVA: 0x003BFF94 File Offset: 0x003BE194
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

		// Token: 0x06011867 RID: 71783 RVA: 0x003BFFC4 File Offset: 0x003BE1C4
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

		// Token: 0x06011868 RID: 71784 RVA: 0x003C009C File Offset: 0x003BE29C
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

		// Token: 0x06011869 RID: 71785 RVA: 0x003C00CC File Offset: 0x003BE2CC
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

		// Token: 0x04006956 RID: 26966
		public static readonly ImmutableSortedDictionary<TKey, TValue> Empty = new ImmutableSortedDictionary<TKey, TValue>(null, null);

		// Token: 0x04006957 RID: 26967
		private readonly ImmutableSortedDictionary<TKey, TValue>.Node _root;

		// Token: 0x04006958 RID: 26968
		private readonly int _count;

		// Token: 0x04006959 RID: 26969
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x0400695A RID: 26970
		private readonly IEqualityComparer<TValue> _valueComparer;

		// Token: 0x020020B3 RID: 8371
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x0601186B RID: 71787 RVA: 0x003C0210 File Offset: 0x003BE410
			internal Builder(ImmutableSortedDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._keyComparer = map.KeyComparer;
				this._valueComparer = map.ValueComparer;
				this._count = map.Count;
				this._immutable = map;
			}

			// Token: 0x17002EEA RID: 12010
			// (get) Token: 0x0601186C RID: 71788 RVA: 0x003C0286 File Offset: 0x003BE486
			ICollection<TKey> IDictionary<TKey, TValue>.Keys
			{
				get
				{
					return this.Root.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17002EEB RID: 12011
			// (get) Token: 0x0601186D RID: 71789 RVA: 0x003C029E File Offset: 0x003BE49E
			public IEnumerable<TKey> Keys
			{
				get
				{
					return this.Root.Keys;
				}
			}

			// Token: 0x17002EEC RID: 12012
			// (get) Token: 0x0601186E RID: 71790 RVA: 0x003C02AB File Offset: 0x003BE4AB
			ICollection<TValue> IDictionary<TKey, TValue>.Values
			{
				get
				{
					return this.Root.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17002EED RID: 12013
			// (get) Token: 0x0601186F RID: 71791 RVA: 0x003C02C3 File Offset: 0x003BE4C3
			public IEnumerable<TValue> Values
			{
				get
				{
					return this.Root.Values;
				}
			}

			// Token: 0x17002EEE RID: 12014
			// (get) Token: 0x06011870 RID: 71792 RVA: 0x003C02D0 File Offset: 0x003BE4D0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17002EEF RID: 12015
			// (get) Token: 0x06011871 RID: 71793 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002EF0 RID: 12016
			// (get) Token: 0x06011872 RID: 71794 RVA: 0x003C02D8 File Offset: 0x003BE4D8
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17002EF1 RID: 12017
			// (get) Token: 0x06011873 RID: 71795 RVA: 0x003C02E0 File Offset: 0x003BE4E0
			// (set) Token: 0x06011874 RID: 71796 RVA: 0x003C02E8 File Offset: 0x003BE4E8
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

			// Token: 0x17002EF2 RID: 12018
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

			// Token: 0x06011877 RID: 71799 RVA: 0x003C038C File Offset: 0x003BE58C
			public readonly ref TValue ValueRef(TKey key)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				return this._root.ValueRef(key, this._keyComparer);
			}

			// Token: 0x17002EF3 RID: 12019
			// (get) Token: 0x06011878 RID: 71800 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002EF4 RID: 12020
			// (get) Token: 0x06011879 RID: 71801 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002EF5 RID: 12021
			// (get) Token: 0x0601187A RID: 71802 RVA: 0x003C03AB File Offset: 0x003BE5AB
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17002EF6 RID: 12022
			// (get) Token: 0x0601187B RID: 71803 RVA: 0x003C03BE File Offset: 0x003BE5BE
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17002EF7 RID: 12023
			// (get) Token: 0x0601187C RID: 71804 RVA: 0x003C03D1 File Offset: 0x003BE5D1
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

			// Token: 0x17002EF8 RID: 12024
			// (get) Token: 0x0601187D RID: 71805 RVA: 0x0000FA11 File Offset: 0x0000DC11
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002EF9 RID: 12025
			// (get) Token: 0x0601187E RID: 71806 RVA: 0x003C03F3 File Offset: 0x003BE5F3
			// (set) Token: 0x0601187F RID: 71807 RVA: 0x003C03FC File Offset: 0x003BE5FC
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

			// Token: 0x17002EFA RID: 12026
			// (get) Token: 0x06011880 RID: 71808 RVA: 0x003C049C File Offset: 0x003BE69C
			// (set) Token: 0x06011881 RID: 71809 RVA: 0x003C04A4 File Offset: 0x003BE6A4
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

			// Token: 0x06011882 RID: 71810 RVA: 0x003C04C8 File Offset: 0x003BE6C8
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x06011883 RID: 71811 RVA: 0x003C04DC File Offset: 0x003BE6DC
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x06011884 RID: 71812 RVA: 0x003C04EA File Offset: 0x003BE6EA
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x06011885 RID: 71813 RVA: 0x003C04FC File Offset: 0x003BE6FC
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x17002EFB RID: 12027
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

			// Token: 0x06011888 RID: 71816 RVA: 0x003C0532 File Offset: 0x003BE732
			void ICollection.CopyTo(Array array, int index)
			{
				this.Root.CopyTo(array, index, this.Count);
			}

			// Token: 0x06011889 RID: 71817 RVA: 0x003C0548 File Offset: 0x003BE748
			public void Add(TKey key, TValue value)
			{
				bool flag;
				this.Root = this.Root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
				if (flag)
				{
					this._count++;
				}
			}

			// Token: 0x0601188A RID: 71818 RVA: 0x003C0587 File Offset: 0x003BE787
			public bool ContainsKey(TKey key)
			{
				return this.Root.ContainsKey(key, this._keyComparer);
			}

			// Token: 0x0601188B RID: 71819 RVA: 0x003C059C File Offset: 0x003BE79C
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

			// Token: 0x0601188C RID: 71820 RVA: 0x003C05D5 File Offset: 0x003BE7D5
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return this.Root.TryGetValue(key, this._keyComparer, out value);
			}

			// Token: 0x0601188D RID: 71821 RVA: 0x003C05EA File Offset: 0x003BE7EA
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				return this.Root.TryGetKey(equalKey, this._keyComparer, out actualKey);
			}

			// Token: 0x0601188E RID: 71822 RVA: 0x003C060A File Offset: 0x003BE80A
			public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x0601188F RID: 71823 RVA: 0x003C0620 File Offset: 0x003BE820
			public void Clear()
			{
				this.Root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				this._count = 0;
			}

			// Token: 0x06011890 RID: 71824 RVA: 0x003C0634 File Offset: 0x003BE834
			public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Root.Contains(item, this._keyComparer, this._valueComparer);
			}

			// Token: 0x06011891 RID: 71825 RVA: 0x003C064E File Offset: 0x003BE84E
			void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex, this.Count);
			}

			// Token: 0x06011892 RID: 71826 RVA: 0x003C0663 File Offset: 0x003BE863
			public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x06011893 RID: 71827 RVA: 0x003C067D File Offset: 0x003BE87D
			[return: Nullable(new byte[] { 0, 1, 1 })]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x06011894 RID: 71828 RVA: 0x003C068B File Offset: 0x003BE88B
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011895 RID: 71829 RVA: 0x003C068B File Offset: 0x003BE88B
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011896 RID: 71830 RVA: 0x003C0698 File Offset: 0x003BE898
			public bool ContainsValue(TValue value)
			{
				return this._root.ContainsValue(value, this._valueComparer);
			}

			// Token: 0x06011897 RID: 71831 RVA: 0x003C06AC File Offset: 0x003BE8AC
			public void AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					this.Add(keyValuePair);
				}
			}

			// Token: 0x06011898 RID: 71832 RVA: 0x003C0700 File Offset: 0x003BE900
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey tkey in keys)
				{
					this.Remove(tkey);
				}
			}

			// Token: 0x06011899 RID: 71833 RVA: 0x003C0754 File Offset: 0x003BE954
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x0601189A RID: 71834 RVA: 0x003C0774 File Offset: 0x003BE974
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

			// Token: 0x0601189B RID: 71835 RVA: 0x003C079A File Offset: 0x003BE99A
			public ImmutableSortedDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedDictionary<TKey, TValue>.Wrap(this.Root, this._count, this._keyComparer, this._valueComparer);
				}
				return this._immutable;
			}

			// Token: 0x0400695B RID: 26971
			private ImmutableSortedDictionary<TKey, TValue>.Node _root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;

			// Token: 0x0400695C RID: 26972
			private IComparer<TKey> _keyComparer = Comparer<TKey>.Default;

			// Token: 0x0400695D RID: 26973
			private IEqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;

			// Token: 0x0400695E RID: 26974
			private int _count;

			// Token: 0x0400695F RID: 26975
			private ImmutableSortedDictionary<TKey, TValue> _immutable;

			// Token: 0x04006960 RID: 26976
			private int _version;

			// Token: 0x04006961 RID: 26977
			private object _syncRoot;
		}

		// Token: 0x020020B4 RID: 8372
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, ISecurePooledObjectUser
		{
			// Token: 0x0601189C RID: 71836 RVA: 0x003C07D0 File Offset: 0x003BE9D0
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

			// Token: 0x17002EFC RID: 12028
			// (get) Token: 0x0601189D RID: 71837 RVA: 0x003C0873 File Offset: 0x003BEA73
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

			// Token: 0x17002EFD RID: 12029
			// (get) Token: 0x0601189E RID: 71838 RVA: 0x003C0894 File Offset: 0x003BEA94
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17002EFE RID: 12030
			// (get) Token: 0x0601189F RID: 71839 RVA: 0x003C089C File Offset: 0x003BEA9C
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060118A0 RID: 71840 RVA: 0x003C08AC File Offset: 0x003BEAAC
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

			// Token: 0x060118A1 RID: 71841 RVA: 0x003C0904 File Offset: 0x003BEB04
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

			// Token: 0x060118A2 RID: 71842 RVA: 0x003C0964 File Offset: 0x003BEB64
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

			// Token: 0x060118A3 RID: 71843 RVA: 0x003C09C1 File Offset: 0x003BEBC1
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this);
				}
			}

			// Token: 0x060118A4 RID: 71844 RVA: 0x003C09EC File Offset: 0x003BEBEC
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060118A5 RID: 71845 RVA: 0x003C0A14 File Offset: 0x003BEC14
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

			// Token: 0x04006962 RID: 26978
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>();

			// Token: 0x04006963 RID: 26979
			private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x04006964 RID: 26980
			private readonly int _poolUserId;

			// Token: 0x04006965 RID: 26981
			private ImmutableSortedDictionary<TKey, TValue>.Node _root;

			// Token: 0x04006966 RID: 26982
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>> _stack;

			// Token: 0x04006967 RID: 26983
			private ImmutableSortedDictionary<TKey, TValue>.Node _current;

			// Token: 0x04006968 RID: 26984
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020020B5 RID: 8373
		[Nullable(0)]
		[DebuggerDisplay("{_key} = {_value}")]
		internal sealed class Node : IBinaryTree<KeyValuePair<TKey, TValue>>, IBinaryTree, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x060118A7 RID: 71847 RVA: 0x003C0A63 File Offset: 0x003BEC63
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060118A8 RID: 71848 RVA: 0x003C0A74 File Offset: 0x003BEC74
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

			// Token: 0x17002EFF RID: 12031
			// (get) Token: 0x060118A9 RID: 71849 RVA: 0x003C0AE9 File Offset: 0x003BECE9
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17002F00 RID: 12032
			// (get) Token: 0x060118AA RID: 71850 RVA: 0x003C0AF4 File Offset: 0x003BECF4
			[Nullable(new byte[] { 2, 0, 1, 1 })]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002F01 RID: 12033
			// (get) Token: 0x060118AB RID: 71851 RVA: 0x003C0AFC File Offset: 0x003BECFC
			[Nullable(new byte[] { 2, 0, 1, 1 })]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002F02 RID: 12034
			// (get) Token: 0x060118AC RID: 71852 RVA: 0x003C0B04 File Offset: 0x003BED04
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17002F03 RID: 12035
			// (get) Token: 0x060118AD RID: 71853 RVA: 0x003C0AF4 File Offset: 0x003BECF4
			[Nullable(new byte[] { 2, 0, 0 })]
			public ImmutableSortedDictionary<TKey, TValue>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002F04 RID: 12036
			// (get) Token: 0x060118AE RID: 71854 RVA: 0x003C0AF4 File Offset: 0x003BECF4
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002F05 RID: 12037
			// (get) Token: 0x060118AF RID: 71855 RVA: 0x003C0AFC File Offset: 0x003BECFC
			[Nullable(new byte[] { 2, 0, 0 })]
			public ImmutableSortedDictionary<TKey, TValue>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002F06 RID: 12038
			// (get) Token: 0x060118B0 RID: 71856 RVA: 0x003C0AFC File Offset: 0x003BECFC
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002F07 RID: 12039
			// (get) Token: 0x060118B1 RID: 71857 RVA: 0x003C0B0C File Offset: 0x003BED0C
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Value
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17002F08 RID: 12040
			// (get) Token: 0x060118B2 RID: 71858 RVA: 0x00002C72 File Offset: 0x00000E72
			int IBinaryTree.Count
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17002F09 RID: 12041
			// (get) Token: 0x060118B3 RID: 71859 RVA: 0x003C0B1F File Offset: 0x003BED1F
			internal IEnumerable<TKey> Keys
			{
				get
				{
					return this.Select((KeyValuePair<TKey, TValue> p) => p.Key);
				}
			}

			// Token: 0x17002F0A RID: 12042
			// (get) Token: 0x060118B4 RID: 71860 RVA: 0x003C0B46 File Offset: 0x003BED46
			internal IEnumerable<TValue> Values
			{
				get
				{
					return this.Select((KeyValuePair<TKey, TValue> p) => p.Value);
				}
			}

			// Token: 0x060118B5 RID: 71861 RVA: 0x003C0B6D File Offset: 0x003BED6D
			[NullableContext(0)]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, null);
			}

			// Token: 0x060118B6 RID: 71862 RVA: 0x003C0B76 File Offset: 0x003BED76
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060118B7 RID: 71863 RVA: 0x003C0B76 File Offset: 0x003BED76
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060118B8 RID: 71864 RVA: 0x003C0B83 File Offset: 0x003BED83
			[NullableContext(0)]
			internal ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0, 0 })] ImmutableSortedDictionary<TKey, TValue>.Builder builder)
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, builder);
			}

			// Token: 0x060118B9 RID: 71865 RVA: 0x003C0B8C File Offset: 0x003BED8C
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

			// Token: 0x060118BA RID: 71866 RVA: 0x003C0C14 File Offset: 0x003BEE14
			internal void CopyTo(Array array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
				}
			}

			// Token: 0x060118BB RID: 71867 RVA: 0x003C0CC0 File Offset: 0x003BEEC0
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromSortedDictionary(SortedDictionary<TKey, TValue> dictionary)
			{
				Requires.NotNull<SortedDictionary<TKey, TValue>>(dictionary, "dictionary");
				IOrderedCollection<KeyValuePair<TKey, TValue>> orderedCollection = dictionary.AsOrderedCollection<KeyValuePair<TKey, TValue>>();
				return ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x060118BC RID: 71868 RVA: 0x003C0CEC File Offset: 0x003BEEEC
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Add(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				bool flag;
				return this.SetOrAdd(key, value, keyComparer, valueComparer, false, out flag, out mutated);
			}

			// Token: 0x060118BD RID: 71869 RVA: 0x003C0D2B File Offset: 0x003BEF2B
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node SetItem(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				return this.SetOrAdd(key, value, keyComparer, valueComparer, true, out replacedExistingValue, out mutated);
			}

			// Token: 0x060118BE RID: 71870 RVA: 0x003C0D5F File Offset: 0x003BEF5F
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Remove(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return this.RemoveRecursive(key, keyComparer, out mutated);
			}

			// Token: 0x060118BF RID: 71871 RVA: 0x003C0D80 File Offset: 0x003BEF80
			internal readonly ref TValue ValueRef(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
				}
				return ref node._value;
			}

			// Token: 0x060118C0 RID: 71872 RVA: 0x003C0DD8 File Offset: 0x003BEFD8
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

			// Token: 0x060118C1 RID: 71873 RVA: 0x003C0E24 File Offset: 0x003BF024
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

			// Token: 0x060118C2 RID: 71874 RVA: 0x003C0E6E File Offset: 0x003BF06E
			internal bool ContainsKey(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return !this.Search(key, keyComparer).IsEmpty;
			}

			// Token: 0x060118C3 RID: 71875 RVA: 0x003C0E98 File Offset: 0x003BF098
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

			// Token: 0x060118C4 RID: 71876 RVA: 0x003C0F04 File Offset: 0x003BF104
			internal bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNullAllowStructs<TKey>(pair.Key, "Key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(pair.Key, keyComparer);
				return !node.IsEmpty && valueComparer.Equals(node._value, pair.Value);
			}

			// Token: 0x060118C5 RID: 71877 RVA: 0x003C0F64 File Offset: 0x003BF164
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x060118C6 RID: 71878 RVA: 0x003C0F8C File Offset: 0x003BF18C
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

			// Token: 0x060118C7 RID: 71879 RVA: 0x003C0FD0 File Offset: 0x003BF1D0
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

			// Token: 0x060118C8 RID: 71880 RVA: 0x003C1014 File Offset: 0x003BF214
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

			// Token: 0x060118C9 RID: 71881 RVA: 0x003C1054 File Offset: 0x003BF254
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

			// Token: 0x060118CA RID: 71882 RVA: 0x003C1094 File Offset: 0x003BF294
			private static int Balance(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x060118CB RID: 71883 RVA: 0x003C10B8 File Offset: 0x003BF2B8
			private static bool IsRightHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) >= 2;
			}

			// Token: 0x060118CC RID: 71884 RVA: 0x003C10D1 File Offset: 0x003BF2D1
			private static bool IsLeftHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) <= -2;
			}

			// Token: 0x060118CD RID: 71885 RVA: 0x003C10EC File Offset: 0x003BF2EC
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

			// Token: 0x060118CE RID: 71886 RVA: 0x003C1150 File Offset: 0x003BF350
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

			// Token: 0x060118CF RID: 71887 RVA: 0x003C11D8 File Offset: 0x003BF3D8
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

			// Token: 0x060118D0 RID: 71888 RVA: 0x003C12C8 File Offset: 0x003BF4C8
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

			// Token: 0x060118D1 RID: 71889 RVA: 0x003C1404 File Offset: 0x003BF604
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

			// Token: 0x060118D2 RID: 71890 RVA: 0x003C147C File Offset: 0x003BF67C
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

			// Token: 0x04006969 RID: 26985
			[Nullable(new byte[] { 1, 0, 0 })]
			internal static readonly ImmutableSortedDictionary<TKey, TValue>.Node EmptyNode = new ImmutableSortedDictionary<TKey, TValue>.Node();

			// Token: 0x0400696A RID: 26986
			private readonly TKey _key;

			// Token: 0x0400696B RID: 26987
			private readonly TValue _value;

			// Token: 0x0400696C RID: 26988
			private bool _frozen;

			// Token: 0x0400696D RID: 26989
			private byte _height;

			// Token: 0x0400696E RID: 26990
			private ImmutableSortedDictionary<TKey, TValue>.Node _left;

			// Token: 0x0400696F RID: 26991
			private ImmutableSortedDictionary<TKey, TValue>.Node _right;
		}
	}
}
