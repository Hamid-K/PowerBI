using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x0200208E RID: 8334
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IImmutableDictionaryInternal<TKey, TValue>, IHashKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x060115CE RID: 71118 RVA: 0x003B9ED4 File Offset: 0x003B80D4
		private ImmutableDictionary(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
			: this(Requires.NotNullPassthrough<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers"))
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			root.Freeze(ImmutableDictionary<TKey, TValue>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
		}

		// Token: 0x060115CF RID: 71119 RVA: 0x003B9F0B File Offset: 0x003B810B
		private ImmutableDictionary(ImmutableDictionary<TKey, TValue>.Comparers comparers = null)
		{
			this._comparers = comparers ?? ImmutableDictionary<TKey, TValue>.Comparers.Get(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
			this._root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
		}

		// Token: 0x060115D0 RID: 71120 RVA: 0x003B9F38 File Offset: 0x003B8138
		public ImmutableDictionary<TKey, TValue> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableDictionary<TKey, TValue>.EmptyWithComparers(this._comparers);
			}
			return this;
		}

		// Token: 0x17002E5E RID: 11870
		// (get) Token: 0x060115D1 RID: 71121 RVA: 0x003B9F4F File Offset: 0x003B814F
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17002E5F RID: 11871
		// (get) Token: 0x060115D2 RID: 71122 RVA: 0x003B9F57 File Offset: 0x003B8157
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17002E60 RID: 11872
		// (get) Token: 0x060115D3 RID: 71123 RVA: 0x003B9F62 File Offset: 0x003B8162
		public IEqualityComparer<TKey> KeyComparer
		{
			get
			{
				return this._comparers.KeyComparer;
			}
		}

		// Token: 0x17002E61 RID: 11873
		// (get) Token: 0x060115D4 RID: 71124 RVA: 0x003B9F6F File Offset: 0x003B816F
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._comparers.ValueComparer;
			}
		}

		// Token: 0x17002E62 RID: 11874
		// (get) Token: 0x060115D5 RID: 71125 RVA: 0x003B9F7C File Offset: 0x003B817C
		public IEnumerable<TKey> Keys
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Key;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17002E63 RID: 11875
		// (get) Token: 0x060115D6 RID: 71126 RVA: 0x003B9F9C File Offset: 0x003B819C
		public IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Value;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060115D7 RID: 71127 RVA: 0x003B9FB9 File Offset: 0x003B81B9
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17002E64 RID: 11876
		// (get) Token: 0x060115D8 RID: 71128 RVA: 0x003B9FC1 File Offset: 0x003B81C1
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17002E65 RID: 11877
		// (get) Token: 0x060115D9 RID: 71129 RVA: 0x003B9FC9 File Offset: 0x003B81C9
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17002E66 RID: 11878
		// (get) Token: 0x060115DA RID: 71130 RVA: 0x003B9FD1 File Offset: 0x003B81D1
		[Nullable(0)]
		private ImmutableDictionary<TKey, TValue>.MutationInput Origin
		{
			get
			{
				return new ImmutableDictionary<TKey, TValue>.MutationInput(this);
			}
		}

		// Token: 0x17002E67 RID: 11879
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

		// Token: 0x17002E68 RID: 11880
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

		// Token: 0x17002E69 RID: 11881
		// (get) Token: 0x060115DE RID: 71134 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060115DF RID: 71135 RVA: 0x003BA026 File Offset: 0x003B8226
		[return: Nullable(new byte[] { 1, 0, 0 })]
		public ImmutableDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x060115E0 RID: 71136 RVA: 0x003BA030 File Offset: 0x003B8230
		public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin).Finalize(this);
		}

		// Token: 0x060115E1 RID: 71137 RVA: 0x003BA05F File Offset: 0x003B825F
		public ImmutableDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			return this.AddRange(pairs, false);
		}

		// Token: 0x060115E2 RID: 71138 RVA: 0x003BA074 File Offset: 0x003B8274
		public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin).Finalize(this);
		}

		// Token: 0x060115E3 RID: 71139 RVA: 0x003BA0A4 File Offset: 0x003B82A4
		public ImmutableDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue).Finalize(this);
		}

		// Token: 0x060115E4 RID: 71140 RVA: 0x003BA0D4 File Offset: 0x003B82D4
		public ImmutableDictionary<TKey, TValue> Remove(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin).Finalize(this);
		}

		// Token: 0x060115E5 RID: 71141 RVA: 0x003BA104 File Offset: 0x003B8304
		public ImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			int num = this._count;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = this._root;
			foreach (TKey tkey in keys)
			{
				int hashCode = this.KeyComparer.GetHashCode(tkey);
				ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(hashCode, out hashBucket))
				{
					ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
					ImmutableDictionary<TKey, TValue>.HashBucket hashBucket2 = hashBucket.Remove(tkey, this._comparers.KeyOnlyComparer, out operationResult);
					sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, hashBucket2, this._comparers.HashBucketEqualityComparer);
					if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
					{
						num--;
					}
				}
			}
			return this.Wrap(sortedInt32KeyNode, num);
		}

		// Token: 0x060115E6 RID: 71142 RVA: 0x003BA1B8 File Offset: 0x003B83B8
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
		}

		// Token: 0x060115E7 RID: 71143 RVA: 0x003BA1D1 File Offset: 0x003B83D1
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair)
		{
			return ImmutableDictionary<TKey, TValue>.Contains(pair, this.Origin);
		}

		// Token: 0x060115E8 RID: 71144 RVA: 0x003BA1DF File Offset: 0x003B83DF
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
		}

		// Token: 0x060115E9 RID: 71145 RVA: 0x003BA1F9 File Offset: 0x003B83F9
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
		}

		// Token: 0x060115EA RID: 71146 RVA: 0x003BA214 File Offset: 0x003B8414
		public ImmutableDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = EqualityComparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (this.KeyComparer != keyComparer)
			{
				ImmutableDictionary<TKey, TValue>.Comparers comparers = ImmutableDictionary<TKey, TValue>.Comparers.Get(keyComparer, valueComparer);
				ImmutableDictionary<TKey, TValue> immutableDictionary = new ImmutableDictionary<TKey, TValue>(comparers);
				return immutableDictionary.AddRange(this, true);
			}
			if (this.ValueComparer == valueComparer)
			{
				return this;
			}
			ImmutableDictionary<TKey, TValue>.Comparers comparers2 = this._comparers.WithValueComparer(valueComparer);
			return new ImmutableDictionary<TKey, TValue>(this._root, comparers2, this._count);
		}

		// Token: 0x060115EB RID: 71147 RVA: 0x003BA282 File Offset: 0x003B8482
		public ImmutableDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._comparers.ValueComparer);
		}

		// Token: 0x060115EC RID: 71148 RVA: 0x003BA298 File Offset: 0x003B8498
		public bool ContainsValue(TValue value)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (this.ValueComparer.Equals(value, keyValuePair.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060115ED RID: 71149 RVA: 0x003BA2FC File Offset: 0x003B84FC
		[NullableContext(0)]
		public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, null);
		}

		// Token: 0x060115EE RID: 71150 RVA: 0x003BA30A File Offset: 0x003B850A
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x060115EF RID: 71151 RVA: 0x003BA314 File Offset: 0x003B8514
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x060115F0 RID: 71152 RVA: 0x003BA31E File Offset: 0x003B851E
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x060115F1 RID: 71153 RVA: 0x003BA327 File Offset: 0x003B8527
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x060115F2 RID: 71154 RVA: 0x003BA330 File Offset: 0x003B8530
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x060115F3 RID: 71155 RVA: 0x003BA339 File Offset: 0x003B8539
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x060115F4 RID: 71156 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060115F5 RID: 71157 RVA: 0x00002C72 File Offset: 0x00000E72
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060115F6 RID: 71158 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060115F7 RID: 71159 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060115F8 RID: 71160 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060115F9 RID: 71161 RVA: 0x003BA344 File Offset: 0x003B8544
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

		// Token: 0x17002E6A RID: 11882
		// (get) Token: 0x060115FA RID: 71162 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E6B RID: 11883
		// (get) Token: 0x060115FB RID: 71163 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E6C RID: 11884
		// (get) Token: 0x060115FC RID: 71164 RVA: 0x003B9FC1 File Offset: 0x003B81C1
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17002E6D RID: 11885
		// (get) Token: 0x060115FD RID: 71165 RVA: 0x003B9FC9 File Offset: 0x003B81C9
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17002E6E RID: 11886
		// (get) Token: 0x060115FE RID: 71166 RVA: 0x003BA3D0 File Offset: 0x003B85D0
		[Nullable(new byte[] { 1, 0, 0, 0 })]
		internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
		{
			[return: Nullable(new byte[] { 1, 0, 0, 0 })]
			get
			{
				return this._root;
			}
		}

		// Token: 0x060115FF RID: 71167 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011600 RID: 71168 RVA: 0x003BA3D8 File Offset: 0x003B85D8
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06011601 RID: 71169 RVA: 0x003BA3E6 File Offset: 0x003B85E6
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x06011602 RID: 71170 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002E6F RID: 11887
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

		// Token: 0x06011605 RID: 71173 RVA: 0x00002C72 File Offset: 0x00000E72
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011606 RID: 71174 RVA: 0x003BA40C File Offset: 0x003B860C
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
			}
		}

		// Token: 0x17002E70 RID: 11888
		// (get) Token: 0x06011607 RID: 71175 RVA: 0x00004FAE File Offset: 0x000031AE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002E71 RID: 11889
		// (get) Token: 0x06011608 RID: 71176 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06011609 RID: 71177 RVA: 0x003BA4BC File Offset: 0x003B86BC
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x0601160A RID: 71178 RVA: 0x003BA4E9 File Offset: 0x003B86E9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0601160B RID: 71179 RVA: 0x003BA4F6 File Offset: 0x003B86F6
		private static ImmutableDictionary<TKey, TValue> EmptyWithComparers(ImmutableDictionary<TKey, TValue>.Comparers comparers)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			if (ImmutableDictionary<TKey, TValue>.Empty._comparers != comparers)
			{
				return new ImmutableDictionary<TKey, TValue>(comparers);
			}
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x0601160C RID: 71180 RVA: 0x003BA51C File Offset: 0x003B871C
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, [NotNullWhen(true)] out ImmutableDictionary<TKey, TValue> other)
		{
			other = sequence as ImmutableDictionary<TKey, TValue>;
			if (other != null)
			{
				return true;
			}
			ImmutableDictionary<TKey, TValue>.Builder builder = sequence as ImmutableDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0601160D RID: 71181 RVA: 0x003BA54C File Offset: 0x003B874C
		private static bool ContainsKey(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue tvalue;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(key, origin.Comparers, out tvalue);
		}

		// Token: 0x0601160E RID: 71182 RVA: 0x003BA58C File Offset: 0x003B878C
		private static bool Contains(KeyValuePair<TKey, TValue> keyValuePair, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue tvalue;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(keyValuePair.Key, origin.Comparers, out tvalue) && origin.ValueComparer.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x0601160F RID: 71183 RVA: 0x003BA5F0 File Offset: 0x003B87F0
		private static bool TryGetValue(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin, [MaybeNullWhen(false)] out TValue value)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetValue(key, origin.Comparers, out value);
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06011610 RID: 71184 RVA: 0x003BA638 File Offset: 0x003B8838
		private static bool TryGetKey(TKey equalKey, ImmutableDictionary<TKey, TValue>.MutationInput origin, out TKey actualKey)
		{
			int hashCode = origin.KeyComparer.GetHashCode(equalKey);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetKey(equalKey, origin.Comparers, out actualKey);
			}
			actualKey = equalKey;
			return false;
		}

		// Token: 0x06011611 RID: 71185 RVA: 0x003BA680 File Offset: 0x003B8880
		private static ImmutableDictionary<TKey, TValue>.MutationResult Add(TKey key, TValue value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket = origin.Root.GetValueOrDefault(hashCode).Add(key, value, origin.KeyOnlyComparer, origin.ValueComparer, behavior, out operationResult);
			if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired)
			{
				return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
			}
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, hashBucket, origin.HashBucketComparer);
			return new ImmutableDictionary<TKey, TValue>.MutationResult(sortedInt32KeyNode, (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? 1 : 0);
		}

		// Token: 0x06011612 RID: 71186 RVA: 0x003BA700 File Offset: 0x003B8900
		private static ImmutableDictionary<TKey, TValue>.MutationResult AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, ImmutableDictionary<TKey, TValue>.MutationInput origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior collisionBehavior = ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			int num = 0;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				ImmutableDictionary<TKey, TValue>.HashBucket hashBucket = sortedInt32KeyNode.GetValueOrDefault(hashCode).Add(keyValuePair.Key, keyValuePair.Value, origin.KeyOnlyComparer, origin.ValueComparer, collisionBehavior, out operationResult);
				sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, hashBucket, origin.HashBucketComparer);
				if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
				{
					num++;
				}
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(sortedInt32KeyNode, num);
		}

		// Token: 0x06011613 RID: 71187 RVA: 0x003BA7C0 File Offset: 0x003B89C0
		private static ImmutableDictionary<TKey, TValue>.MutationResult Remove(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, hashBucket.Remove(key, origin.KeyOnlyComparer, out operationResult), origin.HashBucketComparer);
				return new ImmutableDictionary<TKey, TValue>.MutationResult(sortedInt32KeyNode, (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? (-1) : 0);
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
		}

		// Token: 0x06011614 RID: 71188 RVA: 0x003BA828 File Offset: 0x003B8A28
		private static SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int hashCode, ImmutableDictionary<TKey, TValue>.HashBucket newBucket, IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> hashBucketComparer)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, hashBucketComparer, out flag2, out flag);
		}

		// Token: 0x06011615 RID: 71189 RVA: 0x003BA855 File Offset: 0x003B8A55
		private static ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableDictionary<TKey, TValue>(root, comparers, count);
		}

		// Token: 0x06011616 RID: 71190 RVA: 0x003BA887 File Offset: 0x003B8A87
		private ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == null)
			{
				return this.Clear();
			}
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableDictionary<TKey, TValue>(root, this._comparers, adjustedCountIfDifferentRoot);
			}
			return this.Clear();
		}

		// Token: 0x06011617 RID: 71191 RVA: 0x003BA8BC File Offset: 0x003B8ABC
		private ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs, bool avoidToHashMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			ImmutableDictionary<TKey, TValue> immutableDictionary;
			if (this.IsEmpty && !avoidToHashMap && ImmutableDictionary<TKey, TValue>.TryCastToImmutableMap(pairs, out immutableDictionary))
			{
				return immutableDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.AddRange(pairs, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent).Finalize(this);
		}

		// Token: 0x040068E8 RID: 26856
		public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>(null);

		// Token: 0x040068E9 RID: 26857
		private static readonly Action<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x040068EA RID: 26858
		private readonly int _count;

		// Token: 0x040068EB RID: 26859
		private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

		// Token: 0x040068EC RID: 26860
		private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;

		// Token: 0x0200208F RID: 8335
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x06011619 RID: 71193 RVA: 0x003BA934 File Offset: 0x003B8B34
			internal Builder(ImmutableDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._count = map._count;
				this._comparers = map._comparers;
				this._immutable = map;
			}

			// Token: 0x17002E72 RID: 11890
			// (get) Token: 0x0601161A RID: 71194 RVA: 0x003BA988 File Offset: 0x003B8B88
			// (set) Token: 0x0601161B RID: 71195 RVA: 0x003BA998 File Offset: 0x003B8B98
			public IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TKey>>(value, "value");
					if (value != this.KeyComparer)
					{
						ImmutableDictionary<TKey, TValue>.Comparers comparers = ImmutableDictionary<TKey, TValue>.Comparers.Get(value, this.ValueComparer);
						ImmutableDictionary<TKey, TValue>.MutationInput mutationInput = new ImmutableDictionary<TKey, TValue>.MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode, comparers);
						ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.AddRange(this, mutationInput, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
						this._immutable = null;
						this._comparers = comparers;
						this._count = mutationResult.CountAdjustment;
						this.Root = mutationResult.Root;
					}
				}
			}

			// Token: 0x17002E73 RID: 11891
			// (get) Token: 0x0601161C RID: 71196 RVA: 0x003BAA04 File Offset: 0x003B8C04
			// (set) Token: 0x0601161D RID: 71197 RVA: 0x003BAA11 File Offset: 0x003B8C11
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this.ValueComparer)
					{
						this._comparers = this._comparers.WithValueComparer(value);
						this._immutable = null;
					}
				}
			}

			// Token: 0x17002E74 RID: 11892
			// (get) Token: 0x0601161E RID: 71198 RVA: 0x003BAA40 File Offset: 0x003B8C40
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17002E75 RID: 11893
			// (get) Token: 0x0601161F RID: 71199 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002E76 RID: 11894
			// (get) Token: 0x06011620 RID: 71200 RVA: 0x003BAA48 File Offset: 0x003B8C48
			public IEnumerable<TKey> Keys
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Key;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x17002E77 RID: 11895
			// (get) Token: 0x06011621 RID: 71201 RVA: 0x003BAA65 File Offset: 0x003B8C65
			ICollection<TKey> IDictionary<TKey, TValue>.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17002E78 RID: 11896
			// (get) Token: 0x06011622 RID: 71202 RVA: 0x003BAA78 File Offset: 0x003B8C78
			public IEnumerable<TValue> Values
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Value;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x17002E79 RID: 11897
			// (get) Token: 0x06011623 RID: 71203 RVA: 0x003BAA95 File Offset: 0x003B8C95
			ICollection<TValue> IDictionary<TKey, TValue>.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17002E7A RID: 11898
			// (get) Token: 0x06011624 RID: 71204 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002E7B RID: 11899
			// (get) Token: 0x06011625 RID: 71205 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002E7C RID: 11900
			// (get) Token: 0x06011626 RID: 71206 RVA: 0x003BAA65 File Offset: 0x003B8C65
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17002E7D RID: 11901
			// (get) Token: 0x06011627 RID: 71207 RVA: 0x003BAA95 File Offset: 0x003B8C95
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17002E7E RID: 11902
			// (get) Token: 0x06011628 RID: 71208 RVA: 0x003BAAA8 File Offset: 0x003B8CA8
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

			// Token: 0x17002E7F RID: 11903
			// (get) Token: 0x06011629 RID: 71209 RVA: 0x0000FA11 File Offset: 0x0000DC11
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0601162A RID: 71210 RVA: 0x003BAACA File Offset: 0x003B8CCA
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x0601162B RID: 71211 RVA: 0x003BAADE File Offset: 0x003B8CDE
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x0601162C RID: 71212 RVA: 0x003BAAEC File Offset: 0x003B8CEC
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x0601162D RID: 71213 RVA: 0x003BAAFE File Offset: 0x003B8CFE
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x17002E80 RID: 11904
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

			// Token: 0x06011630 RID: 71216 RVA: 0x003BAB34 File Offset: 0x003B8D34
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
				}
			}

			// Token: 0x17002E81 RID: 11905
			// (get) Token: 0x06011631 RID: 71217 RVA: 0x003BABE4 File Offset: 0x003B8DE4
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17002E82 RID: 11906
			// (get) Token: 0x06011632 RID: 71218 RVA: 0x003BABEC File Offset: 0x003B8DEC
			[Nullable(0)]
			private ImmutableDictionary<TKey, TValue>.MutationInput Origin
			{
				get
				{
					return new ImmutableDictionary<TKey, TValue>.MutationInput(this.Root, this._comparers);
				}
			}

			// Token: 0x17002E83 RID: 11907
			// (get) Token: 0x06011633 RID: 71219 RVA: 0x003BABFF File Offset: 0x003B8DFF
			// (set) Token: 0x06011634 RID: 71220 RVA: 0x003BAC07 File Offset: 0x003B8E07
			[Nullable(new byte[] { 1, 0, 0, 0 })]
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
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

			// Token: 0x17002E84 RID: 11908
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
					ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin);
					this.Apply(mutationResult);
				}
			}

			// Token: 0x06011637 RID: 71223 RVA: 0x003BAC8C File Offset: 0x003B8E8C
			public void AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
				this.Apply(mutationResult);
			}

			// Token: 0x06011638 RID: 71224 RVA: 0x003BACB0 File Offset: 0x003B8EB0
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey tkey in keys)
				{
					this.Remove(tkey);
				}
			}

			// Token: 0x06011639 RID: 71225 RVA: 0x003BAD04 File Offset: 0x003B8F04
			[NullableContext(0)]
			public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, this);
			}

			// Token: 0x0601163A RID: 71226 RVA: 0x003BAD14 File Offset: 0x003B8F14
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x0601163B RID: 71227 RVA: 0x003BAD34 File Offset: 0x003B8F34
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

			// Token: 0x0601163C RID: 71228 RVA: 0x003BAD5A File Offset: 0x003B8F5A
			public ImmutableDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableDictionary<TKey, TValue>.Wrap(this._root, this._comparers, this._count);
				}
				return this._immutable;
			}

			// Token: 0x0601163D RID: 71229 RVA: 0x003BAD88 File Offset: 0x003B8F88
			public void Add(TKey key, TValue value)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x0601163E RID: 71230 RVA: 0x003BADAC File Offset: 0x003B8FAC
			public bool ContainsKey(TKey key)
			{
				return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
			}

			// Token: 0x0601163F RID: 71231 RVA: 0x003BADBC File Offset: 0x003B8FBC
			public bool ContainsValue(TValue value)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (this.ValueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06011640 RID: 71232 RVA: 0x003BAE20 File Offset: 0x003B9020
			public bool Remove(TKey key)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin);
				return this.Apply(mutationResult);
			}

			// Token: 0x06011641 RID: 71233 RVA: 0x003BAE41 File Offset: 0x003B9041
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
			}

			// Token: 0x06011642 RID: 71234 RVA: 0x003BAE50 File Offset: 0x003B9050
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
			}

			// Token: 0x06011643 RID: 71235 RVA: 0x003BAE5F File Offset: 0x003B905F
			public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06011644 RID: 71236 RVA: 0x003BAE75 File Offset: 0x003B9075
			public void Clear()
			{
				this.Root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
				this._count = 0;
			}

			// Token: 0x06011645 RID: 71237 RVA: 0x003BAE89 File Offset: 0x003B9089
			public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return ImmutableDictionary<TKey, TValue>.Contains(item, this.Origin);
			}

			// Token: 0x06011646 RID: 71238 RVA: 0x003BAE98 File Offset: 0x003B9098
			void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x06011647 RID: 71239 RVA: 0x003BAEF8 File Offset: 0x003B90F8
			public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x06011648 RID: 71240 RVA: 0x003BAF12 File Offset: 0x003B9112
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011649 RID: 71241 RVA: 0x003BAF12 File Offset: 0x003B9112
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0601164A RID: 71242 RVA: 0x003BAF1F File Offset: 0x003B911F
			private bool Apply(ImmutableDictionary<TKey, TValue>.MutationResult result)
			{
				this.Root = result.Root;
				this._count += result.CountAdjustment;
				return result.CountAdjustment != 0;
			}

			// Token: 0x040068ED RID: 26861
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;

			// Token: 0x040068EE RID: 26862
			private ImmutableDictionary<TKey, TValue>.Comparers _comparers;

			// Token: 0x040068EF RID: 26863
			private int _count;

			// Token: 0x040068F0 RID: 26864
			private ImmutableDictionary<TKey, TValue> _immutable;

			// Token: 0x040068F1 RID: 26865
			private int _version;

			// Token: 0x040068F2 RID: 26866
			private object _syncRoot;
		}

		// Token: 0x02002092 RID: 8338
		[Nullable(0)]
		internal sealed class Comparers : IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket>, IEqualityComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0601165D RID: 71261 RVA: 0x003BB243 File Offset: 0x003B9443
			internal Comparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				this._keyComparer = keyComparer;
				this._valueComparer = valueComparer;
			}

			// Token: 0x17002E89 RID: 11913
			// (get) Token: 0x0601165E RID: 71262 RVA: 0x003BB26F File Offset: 0x003B946F
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
			}

			// Token: 0x17002E8A RID: 11914
			// (get) Token: 0x0601165F RID: 71263 RVA: 0x00004FAE File Offset: 0x000031AE
			[Nullable(new byte[] { 1, 0, 1, 1 })]
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				[return: Nullable(new byte[] { 1, 0, 1, 1 })]
				get
				{
					return this;
				}
			}

			// Token: 0x17002E8B RID: 11915
			// (get) Token: 0x06011660 RID: 71264 RVA: 0x003BB277 File Offset: 0x003B9477
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
			}

			// Token: 0x17002E8C RID: 11916
			// (get) Token: 0x06011661 RID: 71265 RVA: 0x00004FAE File Offset: 0x000031AE
			[Nullable(new byte[] { 1, 0, 0, 0 })]
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketEqualityComparer
			{
				[return: Nullable(new byte[] { 1, 0, 0, 0 })]
				get
				{
					return this;
				}
			}

			// Token: 0x06011662 RID: 71266 RVA: 0x003BB280 File Offset: 0x003B9480
			[NullableContext(0)]
			public bool Equals(ImmutableDictionary<TKey, TValue>.HashBucket x, ImmutableDictionary<TKey, TValue>.HashBucket y)
			{
				return x.AdditionalElements == y.AdditionalElements && this.KeyComparer.Equals(x.FirstValue.Key, y.FirstValue.Key) && this.ValueComparer.Equals(x.FirstValue.Value, y.FirstValue.Value);
			}

			// Token: 0x06011663 RID: 71267 RVA: 0x003BB2F4 File Offset: 0x003B94F4
			[NullableContext(0)]
			public int GetHashCode(ImmutableDictionary<TKey, TValue>.HashBucket obj)
			{
				return this.KeyComparer.GetHashCode(obj.FirstValue.Key);
			}

			// Token: 0x06011664 RID: 71268 RVA: 0x003BB31B File Offset: 0x003B951B
			bool IEqualityComparer<KeyValuePair<TKey, TValue>>.Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this._keyComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x06011665 RID: 71269 RVA: 0x003BB336 File Offset: 0x003B9536
			int IEqualityComparer<KeyValuePair<TKey, TValue>>.GetHashCode(KeyValuePair<TKey, TValue> obj)
			{
				return this._keyComparer.GetHashCode(obj.Key);
			}

			// Token: 0x06011666 RID: 71270 RVA: 0x003BB34A File Offset: 0x003B954A
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal static ImmutableDictionary<TKey, TValue>.Comparers Get(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (keyComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.KeyComparer || valueComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.ValueComparer)
				{
					return new ImmutableDictionary<TKey, TValue>.Comparers(keyComparer, valueComparer);
				}
				return ImmutableDictionary<TKey, TValue>.Comparers.Default;
			}

			// Token: 0x06011667 RID: 71271 RVA: 0x003BB389 File Offset: 0x003B9589
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableDictionary<TKey, TValue>.Comparers WithValueComparer(IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (this._valueComparer != valueComparer)
				{
					return ImmutableDictionary<TKey, TValue>.Comparers.Get(this.KeyComparer, valueComparer);
				}
				return this;
			}

			// Token: 0x040068FD RID: 26877
			[Nullable(new byte[] { 1, 0, 0 })]
			internal static readonly ImmutableDictionary<TKey, TValue>.Comparers Default = new ImmutableDictionary<TKey, TValue>.Comparers(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);

			// Token: 0x040068FE RID: 26878
			private readonly IEqualityComparer<TKey> _keyComparer;

			// Token: 0x040068FF RID: 26879
			private readonly IEqualityComparer<TValue> _valueComparer;
		}

		// Token: 0x02002093 RID: 8339
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06011669 RID: 71273 RVA: 0x003BB3C3 File Offset: 0x003B95C3
			internal Enumerator([Nullable(new byte[] { 1, 0, 0, 0 })] SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, [Nullable(new byte[] { 2, 0, 0 })] ImmutableDictionary<TKey, TValue>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
			}

			// Token: 0x17002E8D RID: 11917
			// (get) Token: 0x0601166A RID: 71274 RVA: 0x003BB3F6 File Offset: 0x003B95F6
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x17002E8E RID: 11918
			// (get) Token: 0x0601166B RID: 71275 RVA: 0x003BB40E File Offset: 0x003B960E
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0601166C RID: 71276 RVA: 0x003BB41C File Offset: 0x003B961C
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x0601166D RID: 71277 RVA: 0x003BB476 File Offset: 0x003B9676
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
			}

			// Token: 0x0601166E RID: 71278 RVA: 0x003BB4B6 File Offset: 0x003B96B6
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x0601166F RID: 71279 RVA: 0x003BB4CE File Offset: 0x003B96CE
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x04006900 RID: 26880
			private readonly ImmutableDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x04006901 RID: 26881
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x04006902 RID: 26882
			private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x04006903 RID: 26883
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02002094 RID: 8340
		[NullableContext(0)]
		internal readonly struct HashBucket : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x06011670 RID: 71280 RVA: 0x003BB4F6 File Offset: 0x003B96F6
			private HashBucket(KeyValuePair<TKey, TValue> firstElement, ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = additionalElements ?? ImmutableList<KeyValuePair<TKey, TValue>>.Node.EmptyNode;
			}

			// Token: 0x17002E8F RID: 11919
			// (get) Token: 0x06011671 RID: 71281 RVA: 0x003BB50F File Offset: 0x003B970F
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x17002E90 RID: 11920
			// (get) Token: 0x06011672 RID: 71282 RVA: 0x003BB51A File Offset: 0x003B971A
			[Nullable(new byte[] { 0, 1, 1 })]
			internal KeyValuePair<TKey, TValue> FirstValue
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					if (this.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._firstValue;
				}
			}

			// Token: 0x17002E91 RID: 11921
			// (get) Token: 0x06011673 RID: 71283 RVA: 0x003BB530 File Offset: 0x003B9730
			[Nullable(new byte[] { 1, 0, 1, 1 })]
			internal ImmutableList<KeyValuePair<TKey, TValue>>.Node AdditionalElements
			{
				[return: Nullable(new byte[] { 1, 0, 1, 1 })]
				get
				{
					return this._additionalElements;
				}
			}

			// Token: 0x06011674 RID: 71284 RVA: 0x003BB538 File Offset: 0x003B9738
			public ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(this);
			}

			// Token: 0x06011675 RID: 71285 RVA: 0x003BB545 File Offset: 0x003B9745
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011676 RID: 71286 RVA: 0x003BB545 File Offset: 0x003B9745
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011677 RID: 71287 RVA: 0x00002C72 File Offset: 0x00000E72
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06011678 RID: 71288 RVA: 0x00002C72 File Offset: 0x00000E72
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06011679 RID: 71289 RVA: 0x003BB554 File Offset: 0x003B9754
			internal ImmutableDictionary<TKey, TValue>.HashBucket Add([Nullable(1)] TKey key, [Nullable(1)] TValue value, [Nullable(new byte[] { 1, 0, 1, 1 })] IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, [Nullable(1)] IEqualityComparer<TValue> valueComparer, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, null);
				}
				if (keyOnlyComparer.Equals(keyValuePair, this._firstValue))
				{
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, this._additionalElements);
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
						if (!valueComparer.Equals(this._firstValue.Value, value))
						{
							throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
					default:
						throw new InvalidOperationException();
					}
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.Add(keyValuePair));
					}
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.ReplaceAt(num, keyValuePair));
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
					{
						readonly ref KeyValuePair<TKey, TValue> ptr = ref this._additionalElements.ItemRef(num);
						KeyValuePair<TKey, TValue> keyValuePair2 = ptr;
						if (!valueComparer.Equals(keyValuePair2.Value, value))
						{
							throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					}
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
					default:
						throw new InvalidOperationException();
					}
				}
			}

			// Token: 0x0601167A RID: 71290 RVA: 0x003BB6F0 File Offset: 0x003B98F0
			internal ImmutableDictionary<TKey, TValue>.HashBucket Remove([Nullable(1)] TKey key, [Nullable(new byte[] { 1, 0, 1, 1 })] IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
					return this;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
				if (keyOnlyComparer.Equals(this._firstValue, keyValuePair))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return default(ImmutableDictionary<TKey, TValue>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x0601167B RID: 71291 RVA: 0x003BB7B4 File Offset: 0x003B99B4
			[NullableContext(1)]
			internal unsafe bool TryGetValue(TKey key, [Nullable(new byte[] { 1, 0, 0 })] ImmutableDictionary<TKey, TValue>.Comparers comparers, [MaybeNullWhen(false)] out TValue value)
			{
				if (this.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				if (comparers.KeyComparer.Equals(this._firstValue.Key, key))
				{
					value = this._firstValue.Value;
					return true;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
				int num = this._additionalElements.IndexOf(keyValuePair, comparers.KeyOnlyComparer);
				if (num < 0)
				{
					value = default(TValue);
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair2 = *this._additionalElements.ItemRef(num);
				value = keyValuePair2.Value;
				return true;
			}

			// Token: 0x0601167C RID: 71292 RVA: 0x003BB858 File Offset: 0x003B9A58
			[NullableContext(1)]
			internal unsafe bool TryGetKey(TKey equalKey, [Nullable(new byte[] { 1, 0, 0 })] ImmutableDictionary<TKey, TValue>.Comparers comparers, out TKey actualKey)
			{
				if (this.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				if (comparers.KeyComparer.Equals(this._firstValue.Key, equalKey))
				{
					actualKey = this._firstValue.Key;
					return true;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(equalKey, default(TValue));
				int num = this._additionalElements.IndexOf(keyValuePair, comparers.KeyOnlyComparer);
				if (num < 0)
				{
					actualKey = equalKey;
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair2 = *this._additionalElements.ItemRef(num);
				actualKey = keyValuePair2.Key;
				return true;
			}

			// Token: 0x0601167D RID: 71293 RVA: 0x003BB8F9 File Offset: 0x003B9AF9
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x04006904 RID: 26884
			private readonly KeyValuePair<TKey, TValue> _firstValue;

			// Token: 0x04006905 RID: 26885
			private readonly ImmutableList<KeyValuePair<TKey, TValue>>.Node _additionalElements;

			// Token: 0x02002095 RID: 8341
			internal struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
			{
				// Token: 0x0601167E RID: 71294 RVA: 0x003BB90E File Offset: 0x003B9B0E
				internal Enumerator(ImmutableDictionary<TKey, TValue>.HashBucket bucket)
				{
					this._bucket = bucket;
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator);
				}

				// Token: 0x17002E92 RID: 11922
				// (get) Token: 0x0601167F RID: 71295 RVA: 0x003BB92A File Offset: 0x003B9B2A
				[Nullable(1)]
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17002E93 RID: 11923
				// (get) Token: 0x06011680 RID: 71296 RVA: 0x003BB938 File Offset: 0x003B9B38
				[Nullable(new byte[] { 0, 1, 1 })]
				public KeyValuePair<TKey, TValue> Current
				{
					[return: Nullable(new byte[] { 0, 1, 1 })]
					get
					{
						ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						KeyValuePair<TKey, TValue> keyValuePair;
						if (currentPosition != ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First)
						{
							if (currentPosition != ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional)
							{
								throw new InvalidOperationException();
							}
							keyValuePair = this._additionalEnumerator.Current;
						}
						else
						{
							keyValuePair = this._bucket._firstValue;
						}
						return keyValuePair;
					}
				}

				// Token: 0x06011681 RID: 71297 RVA: 0x003BB97C File Offset: 0x003B9B7C
				public bool MoveNext()
				{
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x06011682 RID: 71298 RVA: 0x003BBA22 File Offset: 0x003B9C22
				public void Reset()
				{
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x06011683 RID: 71299 RVA: 0x003BBA36 File Offset: 0x003B9C36
				public void Dispose()
				{
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x04006906 RID: 26886
				private readonly ImmutableDictionary<TKey, TValue>.HashBucket _bucket;

				// Token: 0x04006907 RID: 26887
				private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x04006908 RID: 26888
				private ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator _additionalEnumerator;

				// Token: 0x02002096 RID: 8342
				private enum Position
				{
					// Token: 0x0400690A RID: 26890
					BeforeFirst,
					// Token: 0x0400690B RID: 26891
					First,
					// Token: 0x0400690C RID: 26892
					Additional,
					// Token: 0x0400690D RID: 26893
					End
				}
			}
		}

		// Token: 0x02002097 RID: 8343
		private readonly struct MutationInput
		{
			// Token: 0x06011684 RID: 71300 RVA: 0x003BBA43 File Offset: 0x003B9C43
			internal MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers)
			{
				this._root = root;
				this._comparers = comparers;
			}

			// Token: 0x06011685 RID: 71301 RVA: 0x003BBA53 File Offset: 0x003B9C53
			internal MutationInput(ImmutableDictionary<TKey, TValue> map)
			{
				this._root = map._root;
				this._comparers = map._comparers;
			}

			// Token: 0x17002E94 RID: 11924
			// (get) Token: 0x06011686 RID: 71302 RVA: 0x003BBA6D File Offset: 0x003B9C6D
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17002E95 RID: 11925
			// (get) Token: 0x06011687 RID: 71303 RVA: 0x003BBA75 File Offset: 0x003B9C75
			internal ImmutableDictionary<TKey, TValue>.Comparers Comparers
			{
				get
				{
					return this._comparers;
				}
			}

			// Token: 0x17002E96 RID: 11926
			// (get) Token: 0x06011688 RID: 71304 RVA: 0x003BBA7D File Offset: 0x003B9C7D
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
			}

			// Token: 0x17002E97 RID: 11927
			// (get) Token: 0x06011689 RID: 71305 RVA: 0x003BBA8A File Offset: 0x003B9C8A
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				get
				{
					return this._comparers.KeyOnlyComparer;
				}
			}

			// Token: 0x17002E98 RID: 11928
			// (get) Token: 0x0601168A RID: 71306 RVA: 0x003BBA97 File Offset: 0x003B9C97
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
			}

			// Token: 0x17002E99 RID: 11929
			// (get) Token: 0x0601168B RID: 71307 RVA: 0x003BBAA4 File Offset: 0x003B9CA4
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketComparer
			{
				get
				{
					return this._comparers.HashBucketEqualityComparer;
				}
			}

			// Token: 0x0400690E RID: 26894
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x0400690F RID: 26895
			private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;
		}

		// Token: 0x02002098 RID: 8344
		private readonly struct MutationResult
		{
			// Token: 0x0601168C RID: 71308 RVA: 0x003BBAB1 File Offset: 0x003B9CB1
			internal MutationResult(ImmutableDictionary<TKey, TValue>.MutationInput unchangedInput)
			{
				this._root = unchangedInput.Root;
				this._countAdjustment = 0;
			}

			// Token: 0x0601168D RID: 71309 RVA: 0x003BBAC7 File Offset: 0x003B9CC7
			internal MutationResult(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int countAdjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
				this._root = root;
				this._countAdjustment = countAdjustment;
			}

			// Token: 0x17002E9A RID: 11930
			// (get) Token: 0x0601168E RID: 71310 RVA: 0x003BBAE2 File Offset: 0x003B9CE2
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17002E9B RID: 11931
			// (get) Token: 0x0601168F RID: 71311 RVA: 0x003BBAEA File Offset: 0x003B9CEA
			internal int CountAdjustment
			{
				get
				{
					return this._countAdjustment;
				}
			}

			// Token: 0x06011690 RID: 71312 RVA: 0x003BBAF2 File Offset: 0x003B9CF2
			internal ImmutableDictionary<TKey, TValue> Finalize(ImmutableDictionary<TKey, TValue> priorMap)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(priorMap, "priorMap");
				return priorMap.Wrap(this.Root, priorMap._count + this.CountAdjustment);
			}

			// Token: 0x04006910 RID: 26896
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x04006911 RID: 26897
			private readonly int _countAdjustment;
		}

		// Token: 0x02002099 RID: 8345
		[NullableContext(0)]
		internal enum KeyCollisionBehavior
		{
			// Token: 0x04006913 RID: 26899
			SetValue,
			// Token: 0x04006914 RID: 26900
			Skip,
			// Token: 0x04006915 RID: 26901
			ThrowIfValueDifferent,
			// Token: 0x04006916 RID: 26902
			ThrowAlways
		}

		// Token: 0x0200209A RID: 8346
		[NullableContext(0)]
		internal enum OperationResult
		{
			// Token: 0x04006918 RID: 26904
			AppliedWithoutSizeChange,
			// Token: 0x04006919 RID: 26905
			SizeChanged,
			// Token: 0x0400691A RID: 26906
			NoChangeRequired
		}
	}
}
