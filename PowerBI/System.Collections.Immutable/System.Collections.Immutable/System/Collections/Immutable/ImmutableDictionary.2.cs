using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IImmutableDictionaryInternal<TKey, TValue>, IHashKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00005711 File Offset: 0x00003911
		private ImmutableDictionary(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
			: this(Requires.NotNullPassthrough<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers"))
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			root.Freeze(ImmutableDictionary<TKey, TValue>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005748 File Offset: 0x00003948
		private ImmutableDictionary(ImmutableDictionary<TKey, TValue>.Comparers comparers = null)
		{
			this._comparers = comparers ?? ImmutableDictionary<TKey, TValue>.Comparers.Get(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
			this._root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005775 File Offset: 0x00003975
		public ImmutableDictionary<TKey, TValue> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableDictionary<TKey, TValue>.EmptyWithComparers(this._comparers);
			}
			return this;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000578C File Offset: 0x0000398C
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005794 File Offset: 0x00003994
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000579F File Offset: 0x0000399F
		public IEqualityComparer<TKey> KeyComparer
		{
			get
			{
				return this._comparers.KeyComparer;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000057AC File Offset: 0x000039AC
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._comparers.ValueComparer;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000057BC File Offset: 0x000039BC
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000057DC File Offset: 0x000039DC
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

		// Token: 0x0600019D RID: 413 RVA: 0x000057F9 File Offset: 0x000039F9
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005801 File Offset: 0x00003A01
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005809 File Offset: 0x00003A09
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005811 File Offset: 0x00003A11
		[Nullable(0)]
		private ImmutableDictionary<TKey, TValue>.MutationInput Origin
		{
			get
			{
				return new ImmutableDictionary<TKey, TValue>.MutationInput(this);
			}
		}

		// Token: 0x1700004C RID: 76
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

		// Token: 0x1700004D RID: 77
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000586D File Offset: 0x00003A6D
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005870 File Offset: 0x00003A70
		[return: Nullable(new byte[] { 1, 0, 0 })]
		public ImmutableDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005878 File Offset: 0x00003A78
		public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin).Finalize(this);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000058A7 File Offset: 0x00003AA7
		public ImmutableDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			return this.AddRange(pairs, false);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000058BC File Offset: 0x00003ABC
		public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin).Finalize(this);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000058EC File Offset: 0x00003AEC
		public ImmutableDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue).Finalize(this);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000591C File Offset: 0x00003B1C
		public ImmutableDictionary<TKey, TValue> Remove(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin).Finalize(this);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000594C File Offset: 0x00003B4C
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

		// Token: 0x060001AC RID: 428 RVA: 0x00005A00 File Offset: 0x00003C00
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005A19 File Offset: 0x00003C19
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair)
		{
			return ImmutableDictionary<TKey, TValue>.Contains(pair, this.Origin);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005A27 File Offset: 0x00003C27
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005A41 File Offset: 0x00003C41
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005A5C File Offset: 0x00003C5C
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

		// Token: 0x060001B1 RID: 433 RVA: 0x00005ACA File Offset: 0x00003CCA
		public ImmutableDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._comparers.ValueComparer);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005AE0 File Offset: 0x00003CE0
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

		// Token: 0x060001B3 RID: 435 RVA: 0x00005B44 File Offset: 0x00003D44
		[NullableContext(0)]
		public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, null);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005B52 File Offset: 0x00003D52
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005B5C File Offset: 0x00003D5C
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005B66 File Offset: 0x00003D66
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005B6F File Offset: 0x00003D6F
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005B78 File Offset: 0x00003D78
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005B81 File Offset: 0x00003D81
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005B8A File Offset: 0x00003D8A
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005B91 File Offset: 0x00003D91
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005B98 File Offset: 0x00003D98
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005B9F File Offset: 0x00003D9F
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005BA6 File Offset: 0x00003DA6
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005BB0 File Offset: 0x00003DB0
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005C3C File Offset: 0x00003E3C
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005C3F File Offset: 0x00003E3F
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005C42 File Offset: 0x00003E42
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005C4A File Offset: 0x00003E4A
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005C52 File Offset: 0x00003E52
		[Nullable(new byte[] { 1, 0, 0, 0 })]
		internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
		{
			[return: Nullable(new byte[] { 1, 0, 0, 0 })]
			get
			{
				return this._root;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005C5A File Offset: 0x00003E5A
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005C61 File Offset: 0x00003E61
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005C6F File Offset: 0x00003E6F
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005C81 File Offset: 0x00003E81
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000054 RID: 84
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

		// Token: 0x060001CB RID: 459 RVA: 0x00005CA2 File Offset: 0x00003EA2
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005CAC File Offset: 0x00003EAC
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), new int[] { arrayIndex++ });
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005D68 File Offset: 0x00003F68
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00005D6B File Offset: 0x00003F6B
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005D70 File Offset: 0x00003F70
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005D9D File Offset: 0x00003F9D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005DAA File Offset: 0x00003FAA
		private static ImmutableDictionary<TKey, TValue> EmptyWithComparers(ImmutableDictionary<TKey, TValue>.Comparers comparers)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			if (ImmutableDictionary<TKey, TValue>.Empty._comparers != comparers)
			{
				return new ImmutableDictionary<TKey, TValue>(comparers);
			}
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005DD0 File Offset: 0x00003FD0
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

		// Token: 0x060001D3 RID: 467 RVA: 0x00005E00 File Offset: 0x00004000
		private static bool ContainsKey(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue tvalue;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(key, origin.Comparers, out tvalue);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005E40 File Offset: 0x00004040
		private static bool Contains(KeyValuePair<TKey, TValue> keyValuePair, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue tvalue;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(keyValuePair.Key, origin.Comparers, out tvalue) && origin.ValueComparer.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005EA4 File Offset: 0x000040A4
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

		// Token: 0x060001D6 RID: 470 RVA: 0x00005EEC File Offset: 0x000040EC
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

		// Token: 0x060001D7 RID: 471 RVA: 0x00005F34 File Offset: 0x00004134
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

		// Token: 0x060001D8 RID: 472 RVA: 0x00005FB4 File Offset: 0x000041B4
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

		// Token: 0x060001D9 RID: 473 RVA: 0x00006074 File Offset: 0x00004274
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

		// Token: 0x060001DA RID: 474 RVA: 0x000060DC File Offset: 0x000042DC
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

		// Token: 0x060001DB RID: 475 RVA: 0x00006109 File Offset: 0x00004309
		private static ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableDictionary<TKey, TValue>(root, comparers, count);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000613B File Offset: 0x0000433B
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

		// Token: 0x060001DD RID: 477 RVA: 0x00006170 File Offset: 0x00004370
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

		// Token: 0x0400001D RID: 29
		public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>(null);

		// Token: 0x0400001E RID: 30
		private static readonly Action<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x0400001F RID: 31
		private readonly int _count;

		// Token: 0x04000020 RID: 32
		private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

		// Token: 0x04000021 RID: 33
		private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;

		// Token: 0x02000060 RID: 96
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x06000439 RID: 1081 RVA: 0x0000B09C File Offset: 0x0000929C
			internal Builder(ImmutableDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._count = map._count;
				this._comparers = map._comparers;
				this._immutable = map;
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000B0F0 File Offset: 0x000092F0
			// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000B100 File Offset: 0x00009300
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

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000B16C File Offset: 0x0000936C
			// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000B179 File Offset: 0x00009379
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

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000B1A8 File Offset: 0x000093A8
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000B1B0 File Offset: 0x000093B0
			bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000B1B4 File Offset: 0x000093B4
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

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B1D1 File Offset: 0x000093D1
			ICollection<TKey> IDictionary<TKey, TValue>.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000B1E4 File Offset: 0x000093E4
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

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000B201 File Offset: 0x00009401
			ICollection<TValue> IDictionary<TKey, TValue>.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000B214 File Offset: 0x00009414
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000B217 File Offset: 0x00009417
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000B21A File Offset: 0x0000941A
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000B22D File Offset: 0x0000942D
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000B240 File Offset: 0x00009440
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

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000B262 File Offset: 0x00009462
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x0000B265 File Offset: 0x00009465
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x0000B279 File Offset: 0x00009479
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x0000B287 File Offset: 0x00009487
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x0000B299 File Offset: 0x00009499
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x170000C9 RID: 201
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

			// Token: 0x06000450 RID: 1104 RVA: 0x0000B2D0 File Offset: 0x000094D0
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), new int[] { arrayIndex++ });
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000B38C File Offset: 0x0000958C
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000B394 File Offset: 0x00009594
			[Nullable(0)]
			private ImmutableDictionary<TKey, TValue>.MutationInput Origin
			{
				get
				{
					return new ImmutableDictionary<TKey, TValue>.MutationInput(this.Root, this._comparers);
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000B3A7 File Offset: 0x000095A7
			// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000B3AF File Offset: 0x000095AF
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

			// Token: 0x170000CD RID: 205
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

			// Token: 0x06000457 RID: 1111 RVA: 0x0000B434 File Offset: 0x00009634
			public void AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
				this.Apply(mutationResult);
			}

			// Token: 0x06000458 RID: 1112 RVA: 0x0000B458 File Offset: 0x00009658
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey tkey in keys)
				{
					this.Remove(tkey);
				}
			}

			// Token: 0x06000459 RID: 1113 RVA: 0x0000B4AC File Offset: 0x000096AC
			[NullableContext(0)]
			public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, this);
			}

			// Token: 0x0600045A RID: 1114 RVA: 0x0000B4BC File Offset: 0x000096BC
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x0000B4DC File Offset: 0x000096DC
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

			// Token: 0x0600045C RID: 1116 RVA: 0x0000B502 File Offset: 0x00009702
			public ImmutableDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableDictionary<TKey, TValue>.Wrap(this._root, this._comparers, this._count);
				}
				return this._immutable;
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x0000B530 File Offset: 0x00009730
			public void Add(TKey key, TValue value)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x0600045E RID: 1118 RVA: 0x0000B554 File Offset: 0x00009754
			public bool ContainsKey(TKey key)
			{
				return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x0000B564 File Offset: 0x00009764
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

			// Token: 0x06000460 RID: 1120 RVA: 0x0000B5C8 File Offset: 0x000097C8
			public bool Remove(TKey key)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin);
				return this.Apply(mutationResult);
			}

			// Token: 0x06000461 RID: 1121 RVA: 0x0000B5E9 File Offset: 0x000097E9
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
			}

			// Token: 0x06000462 RID: 1122 RVA: 0x0000B5F8 File Offset: 0x000097F8
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x0000B607 File Offset: 0x00009807
			public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06000464 RID: 1124 RVA: 0x0000B61D File Offset: 0x0000981D
			public void Clear()
			{
				this.Root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
				this._count = 0;
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x0000B631 File Offset: 0x00009831
			public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return ImmutableDictionary<TKey, TValue>.Contains(item, this.Origin);
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x0000B640 File Offset: 0x00009840
			void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x0000B6A0 File Offset: 0x000098A0
			public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x0000B6BA File Offset: 0x000098BA
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x0000B6C7 File Offset: 0x000098C7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x0000B6D4 File Offset: 0x000098D4
			private bool Apply(ImmutableDictionary<TKey, TValue>.MutationResult result)
			{
				this.Root = result.Root;
				this._count += result.CountAdjustment;
				return result.CountAdjustment != 0;
			}

			// Token: 0x04000083 RID: 131
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;

			// Token: 0x04000084 RID: 132
			private ImmutableDictionary<TKey, TValue>.Comparers _comparers;

			// Token: 0x04000085 RID: 133
			private int _count;

			// Token: 0x04000086 RID: 134
			private ImmutableDictionary<TKey, TValue> _immutable;

			// Token: 0x04000087 RID: 135
			private int _version;

			// Token: 0x04000088 RID: 136
			private object _syncRoot;
		}

		// Token: 0x02000061 RID: 97
		[Nullable(0)]
		internal sealed class Comparers : IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket>, IEqualityComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600046B RID: 1131 RVA: 0x0000B701 File Offset: 0x00009901
			internal Comparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				this._keyComparer = keyComparer;
				this._valueComparer = valueComparer;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000B72D File Offset: 0x0000992D
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000B735 File Offset: 0x00009935
			[Nullable(new byte[] { 1, 0, 1, 1 })]
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				[return: Nullable(new byte[] { 1, 0, 1, 1 })]
				get
				{
					return this;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000B738 File Offset: 0x00009938
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000B740 File Offset: 0x00009940
			[Nullable(new byte[] { 1, 0, 0, 0 })]
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketEqualityComparer
			{
				[return: Nullable(new byte[] { 1, 0, 0, 0 })]
				get
				{
					return this;
				}
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x0000B744 File Offset: 0x00009944
			[NullableContext(0)]
			public bool Equals(ImmutableDictionary<TKey, TValue>.HashBucket x, ImmutableDictionary<TKey, TValue>.HashBucket y)
			{
				return x.AdditionalElements == y.AdditionalElements && this.KeyComparer.Equals(x.FirstValue.Key, y.FirstValue.Key) && this.ValueComparer.Equals(x.FirstValue.Value, y.FirstValue.Value);
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x0000B7B8 File Offset: 0x000099B8
			[NullableContext(0)]
			public int GetHashCode(ImmutableDictionary<TKey, TValue>.HashBucket obj)
			{
				return this.KeyComparer.GetHashCode(obj.FirstValue.Key);
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x0000B7DF File Offset: 0x000099DF
			bool IEqualityComparer<KeyValuePair<TKey, TValue>>.Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this._keyComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x0000B7FA File Offset: 0x000099FA
			int IEqualityComparer<KeyValuePair<TKey, TValue>>.GetHashCode(KeyValuePair<TKey, TValue> obj)
			{
				return this._keyComparer.GetHashCode(obj.Key);
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x0000B80E File Offset: 0x00009A0E
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

			// Token: 0x06000475 RID: 1141 RVA: 0x0000B84D File Offset: 0x00009A4D
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

			// Token: 0x04000089 RID: 137
			[Nullable(new byte[] { 1, 0, 0 })]
			internal static readonly ImmutableDictionary<TKey, TValue>.Comparers Default = new ImmutableDictionary<TKey, TValue>.Comparers(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);

			// Token: 0x0400008A RID: 138
			private readonly IEqualityComparer<TKey> _keyComparer;

			// Token: 0x0400008B RID: 139
			private readonly IEqualityComparer<TValue> _valueComparer;
		}

		// Token: 0x02000062 RID: 98
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000477 RID: 1143 RVA: 0x0000B887 File Offset: 0x00009A87
			internal Enumerator([Nullable(new byte[] { 1, 0, 0, 0 })] SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, [Nullable(new byte[] { 2, 0, 0 })] ImmutableDictionary<TKey, TValue>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000B8BA File Offset: 0x00009ABA
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

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000B8D2 File Offset: 0x00009AD2
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600047A RID: 1146 RVA: 0x0000B8E0 File Offset: 0x00009AE0
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

			// Token: 0x0600047B RID: 1147 RVA: 0x0000B93A File Offset: 0x00009B3A
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x0000B97A File Offset: 0x00009B7A
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x0000B992 File Offset: 0x00009B92
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x0400008C RID: 140
			private readonly ImmutableDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x0400008D RID: 141
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x0400008E RID: 142
			private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x0400008F RID: 143
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000063 RID: 99
		[NullableContext(0)]
		internal readonly struct HashBucket : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x0600047E RID: 1150 RVA: 0x0000B9BA File Offset: 0x00009BBA
			private HashBucket(KeyValuePair<TKey, TValue> firstElement, ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = additionalElements ?? ImmutableList<KeyValuePair<TKey, TValue>>.Node.EmptyNode;
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000B9D3 File Offset: 0x00009BD3
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000B9DE File Offset: 0x00009BDE
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

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000B9F4 File Offset: 0x00009BF4
			[Nullable(new byte[] { 1, 0, 1, 1 })]
			internal ImmutableList<KeyValuePair<TKey, TValue>>.Node AdditionalElements
			{
				[return: Nullable(new byte[] { 1, 0, 1, 1 })]
				get
				{
					return this._additionalElements;
				}
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x0000B9FC File Offset: 0x00009BFC
			public ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(this);
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x0000BA09 File Offset: 0x00009C09
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x0000BA16 File Offset: 0x00009C16
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x0000BA23 File Offset: 0x00009C23
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x0000BA2A File Offset: 0x00009C2A
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x0000BA34 File Offset: 0x00009C34
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
						if (!valueComparer.Equals(this._additionalElements[num].Value, value))
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
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x0000BBC4 File Offset: 0x00009DC4
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

			// Token: 0x06000489 RID: 1161 RVA: 0x0000BC88 File Offset: 0x00009E88
			[NullableContext(1)]
			internal bool TryGetValue(TKey key, [Nullable(new byte[] { 1, 0, 0 })] ImmutableDictionary<TKey, TValue>.Comparers comparers, [MaybeNullWhen(false)] out TValue value)
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
				value = this._additionalElements[num].Value;
				return true;
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x0000BD24 File Offset: 0x00009F24
			[NullableContext(1)]
			internal bool TryGetKey(TKey equalKey, [Nullable(new byte[] { 1, 0, 0 })] ImmutableDictionary<TKey, TValue>.Comparers comparers, out TKey actualKey)
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
				actualKey = this._additionalElements[num].Key;
				return true;
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x0000BDC0 File Offset: 0x00009FC0
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x04000090 RID: 144
			private readonly KeyValuePair<TKey, TValue> _firstValue;

			// Token: 0x04000091 RID: 145
			private readonly ImmutableList<KeyValuePair<TKey, TValue>>.Node _additionalElements;

			// Token: 0x02000082 RID: 130
			internal struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
			{
				// Token: 0x06000662 RID: 1634 RVA: 0x000112C3 File Offset: 0x0000F4C3
				internal Enumerator(ImmutableDictionary<TKey, TValue>.HashBucket bucket)
				{
					this._bucket = bucket;
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator);
				}

				// Token: 0x17000154 RID: 340
				// (get) Token: 0x06000663 RID: 1635 RVA: 0x000112DF File Offset: 0x0000F4DF
				[Nullable(1)]
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17000155 RID: 341
				// (get) Token: 0x06000664 RID: 1636 RVA: 0x000112EC File Offset: 0x0000F4EC
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

				// Token: 0x06000665 RID: 1637 RVA: 0x00011330 File Offset: 0x0000F530
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

				// Token: 0x06000666 RID: 1638 RVA: 0x000113D6 File Offset: 0x0000F5D6
				public void Reset()
				{
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x06000667 RID: 1639 RVA: 0x000113EA File Offset: 0x0000F5EA
				public void Dispose()
				{
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x04000119 RID: 281
				private readonly ImmutableDictionary<TKey, TValue>.HashBucket _bucket;

				// Token: 0x0400011A RID: 282
				private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x0400011B RID: 283
				private ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator _additionalEnumerator;

				// Token: 0x02000085 RID: 133
				private enum Position
				{
					// Token: 0x04000125 RID: 293
					BeforeFirst,
					// Token: 0x04000126 RID: 294
					First,
					// Token: 0x04000127 RID: 295
					Additional,
					// Token: 0x04000128 RID: 296
					End
				}
			}
		}

		// Token: 0x02000064 RID: 100
		private readonly struct MutationInput
		{
			// Token: 0x0600048C RID: 1164 RVA: 0x0000BDD5 File Offset: 0x00009FD5
			internal MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers)
			{
				this._root = root;
				this._comparers = comparers;
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x0000BDE5 File Offset: 0x00009FE5
			internal MutationInput(ImmutableDictionary<TKey, TValue> map)
			{
				this._root = map._root;
				this._comparers = map._comparers;
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000BDFF File Offset: 0x00009FFF
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000BE07 File Offset: 0x0000A007
			internal ImmutableDictionary<TKey, TValue>.Comparers Comparers
			{
				get
				{
					return this._comparers;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000BE0F File Offset: 0x0000A00F
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000BE1C File Offset: 0x0000A01C
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				get
				{
					return this._comparers.KeyOnlyComparer;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000BE29 File Offset: 0x0000A029
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000BE36 File Offset: 0x0000A036
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketComparer
			{
				get
				{
					return this._comparers.HashBucketEqualityComparer;
				}
			}

			// Token: 0x04000092 RID: 146
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x04000093 RID: 147
			private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;
		}

		// Token: 0x02000065 RID: 101
		private readonly struct MutationResult
		{
			// Token: 0x06000494 RID: 1172 RVA: 0x0000BE43 File Offset: 0x0000A043
			internal MutationResult(ImmutableDictionary<TKey, TValue>.MutationInput unchangedInput)
			{
				this._root = unchangedInput.Root;
				this._countAdjustment = 0;
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x0000BE59 File Offset: 0x0000A059
			internal MutationResult(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int countAdjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
				this._root = root;
				this._countAdjustment = countAdjustment;
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000BE74 File Offset: 0x0000A074
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000BE7C File Offset: 0x0000A07C
			internal int CountAdjustment
			{
				get
				{
					return this._countAdjustment;
				}
			}

			// Token: 0x06000498 RID: 1176 RVA: 0x0000BE84 File Offset: 0x0000A084
			internal ImmutableDictionary<TKey, TValue> Finalize(ImmutableDictionary<TKey, TValue> priorMap)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(priorMap, "priorMap");
				return priorMap.Wrap(this.Root, priorMap._count + this.CountAdjustment);
			}

			// Token: 0x04000094 RID: 148
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x04000095 RID: 149
			private readonly int _countAdjustment;
		}

		// Token: 0x02000066 RID: 102
		[NullableContext(0)]
		internal enum KeyCollisionBehavior
		{
			// Token: 0x04000097 RID: 151
			SetValue,
			// Token: 0x04000098 RID: 152
			Skip,
			// Token: 0x04000099 RID: 153
			ThrowIfValueDifferent,
			// Token: 0x0400009A RID: 154
			ThrowAlways
		}

		// Token: 0x02000067 RID: 103
		[NullableContext(0)]
		internal enum OperationResult
		{
			// Token: 0x0400009C RID: 156
			AppliedWithoutSizeChange,
			// Token: 0x0400009D RID: 157
			SizeChanged,
			// Token: 0x0400009E RID: 158
			NoChangeRequired
		}
	}
}
