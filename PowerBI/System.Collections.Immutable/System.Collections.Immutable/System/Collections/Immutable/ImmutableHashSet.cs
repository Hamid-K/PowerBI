using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableHashSet<[Nullable(2)] T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IHashKeyCollection<T>, ICollection<T>, ISet<T>, ICollection, IStrongEnumerable<T, ImmutableHashSet<T>.Enumerator>
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00002EFA File Offset: 0x000010FA
		internal ImmutableHashSet(IEqualityComparer<T> equalityComparer)
			: this(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, equalityComparer, 0)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002F0C File Offset: 0x0000110C
		private ImmutableHashSet(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			root.Freeze(ImmutableHashSet<T>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
			this._equalityComparer = equalityComparer;
			this._hashBucketEqualityComparer = ImmutableHashSet<T>.GetHashBucketEqualityComparer(equalityComparer);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002F61 File Offset: 0x00001161
		public ImmutableHashSet<T> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableHashSet<T>.Empty.WithComparer(this._equalityComparer);
			}
			return this;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002F7D File Offset: 0x0000117D
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002F85 File Offset: 0x00001185
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002F90 File Offset: 0x00001190
		public IEqualityComparer<T> KeyComparer
		{
			get
			{
				return this._equalityComparer;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002F98 File Offset: 0x00001198
		IImmutableSet<T> IImmutableSet<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002FA0 File Offset: 0x000011A0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002FA3 File Offset: 0x000011A3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002FA6 File Offset: 0x000011A6
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002FAE File Offset: 0x000011AE
		[Nullable(0)]
		private ImmutableHashSet<T>.MutationInput Origin
		{
			get
			{
				return new ImmutableHashSet<T>.MutationInput(this);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002FB6 File Offset: 0x000011B6
		[return: Nullable(new byte[] { 1, 0 })]
		public ImmutableHashSet<T>.Builder ToBuilder()
		{
			return new ImmutableHashSet<T>.Builder(this);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002FC0 File Offset: 0x000011C0
		public ImmutableHashSet<T> Add(T item)
		{
			return ImmutableHashSet<T>.Add(item, this.Origin).Finalize(this);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002FE4 File Offset: 0x000011E4
		public ImmutableHashSet<T> Remove(T item)
		{
			return ImmutableHashSet<T>.Remove(item, this.Origin).Finalize(this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003008 File Offset: 0x00001208
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			int num = ((equalValue != null) ? this._equalityComparer.GetHashCode(equalValue) : 0);
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (this._root.TryGetValue(num, out hashBucket))
			{
				return hashBucket.TryExchange(equalValue, this._equalityComparer, out actualValue);
			}
			actualValue = equalValue;
			return false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003055 File Offset: 0x00001255
		public ImmutableHashSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this.Union(other, false);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000306C File Offset: 0x0000126C
		public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Intersect(other, this.Origin).Finalize(this);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000309C File Offset: 0x0000129C
		public ImmutableHashSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root).Finalize(this);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000030D8 File Offset: 0x000012D8
		public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.SymmetricExcept(other, this.Origin).Finalize(this);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003105 File Offset: 0x00001305
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003124 File Offset: 0x00001324
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000313D File Offset: 0x0000133D
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003156 File Offset: 0x00001356
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000316F File Offset: 0x0000136F
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003188 File Offset: 0x00001388
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Overlaps(other, this.Origin);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000031A1 File Offset: 0x000013A1
		IImmutableSet<T> IImmutableSet<T>.Add(T item)
		{
			return this.Add(item);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000031AA File Offset: 0x000013AA
		IImmutableSet<T> IImmutableSet<T>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000031B3 File Offset: 0x000013B3
		IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000031BC File Offset: 0x000013BC
		IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000031C5 File Offset: 0x000013C5
		IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000031CE File Offset: 0x000013CE
		IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000031D7 File Offset: 0x000013D7
		public bool Contains(T item)
		{
			return ImmutableHashSet<T>.Contains(item, this.Origin);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000031E8 File Offset: 0x000013E8
		public ImmutableHashSet<T> WithComparer([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				equalityComparer = EqualityComparer<T>.Default;
			}
			if (equalityComparer == this._equalityComparer)
			{
				return this;
			}
			ImmutableHashSet<T> immutableHashSet = new ImmutableHashSet<T>(equalityComparer);
			return immutableHashSet.Union(this, true);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000321B File Offset: 0x0000141B
		bool ISet<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003222 File Offset: 0x00001422
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003229 File Offset: 0x00001429
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003230 File Offset: 0x00001430
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003237 File Offset: 0x00001437
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000323E File Offset: 0x0000143E
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003244 File Offset: 0x00001444
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000032D0 File Offset: 0x000014D0
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000032D7 File Offset: 0x000014D7
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000032DE File Offset: 0x000014DE
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000032E8 File Offset: 0x000014E8
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, new int[] { arrayIndex++ });
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003388 File Offset: 0x00001588
		[NullableContext(0)]
		public ImmutableHashSet<T>.Enumerator GetEnumerator()
		{
			return new ImmutableHashSet<T>.Enumerator(this._root, null);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003398 File Offset: 0x00001598
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000033C5 File Offset: 0x000015C5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000033D4 File Offset: 0x000015D4
		private static bool IsSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (!ImmutableHashSet<T>.Contains(t, origin))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003440 File Offset: 0x00001640
		private static ImmutableHashSet<T>.MutationResult Add(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int num = ((item != null) ? origin.EqualityComparer.GetHashCode(item) : 0);
			ImmutableHashSet<T>.OperationResult operationResult;
			ImmutableHashSet<T>.HashBucket hashBucket = origin.Root.GetValueOrDefault(num).Add(item, origin.EqualityComparer, out operationResult);
			if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
			{
				return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
			}
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(origin.Root, num, origin.HashBucketEqualityComparer, hashBucket);
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, 1, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000034BC File Offset: 0x000016BC
		private static ImmutableHashSet<T>.MutationResult Remove(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			ImmutableHashSet<T>.OperationResult operationResult = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
			int num = ((item != null) ? origin.EqualityComparer.GetHashCode(item) : 0);
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = origin.Root;
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(num, out hashBucket))
			{
				ImmutableHashSet<T>.HashBucket hashBucket2 = hashBucket.Remove(item, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
				{
					return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
				}
				sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(origin.Root, num, origin.HashBucketEqualityComparer, hashBucket2);
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged) ? (-1) : 0, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003548 File Offset: 0x00001748
		private static bool Contains(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int num = ((item != null) ? origin.EqualityComparer.GetHashCode(item) : 0);
			ImmutableHashSet<T>.HashBucket hashBucket;
			return origin.Root.TryGetValue(num, out hashBucket) && hashBucket.Contains(item, origin.EqualityComparer);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003590 File Offset: 0x00001790
		private static ImmutableHashSet<T>.MutationResult Union(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int num2 = ((t != null) ? origin.EqualityComparer.GetHashCode(t) : 0);
				ImmutableHashSet<T>.OperationResult operationResult;
				ImmutableHashSet<T>.HashBucket hashBucket = sortedInt32KeyNode.GetValueOrDefault(num2).Add(t, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
				{
					sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, num2, origin.HashBucketEqualityComparer, hashBucket);
					num++;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003654 File Offset: 0x00001854
		private static bool Overlaps(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (ImmutableHashSet<T>.Contains(t, origin))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000036D0 File Offset: 0x000018D0
		private static bool SetEquals(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count != hashSet.Count)
			{
				return false;
			}
			foreach (T t in hashSet)
			{
				if (!ImmutableHashSet<T>.Contains(t, origin))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003754 File Offset: 0x00001954
		private static SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int hashCode, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, ImmutableHashSet<T>.HashBucket newBucket)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, hashBucketEqualityComparer, out flag2, out flag);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003784 File Offset: 0x00001984
		private static ImmutableHashSet<T>.MutationResult Intersect(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			int num = 0;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (ImmutableHashSet<T>.Contains(t, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(t, new ImmutableHashSet<T>.MutationInput(sortedInt32KeyNode, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					sortedInt32KeyNode = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000382C File Offset: 0x00001A2C
		private static ImmutableHashSet<T>.MutationResult Except(IEnumerable<T> other, IEqualityComparer<T> equalityComparer, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int num2 = ((t != null) ? equalityComparer.GetHashCode(t) : 0);
				ImmutableHashSet<T>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(num2, out hashBucket))
				{
					ImmutableHashSet<T>.OperationResult operationResult;
					ImmutableHashSet<T>.HashBucket hashBucket2 = hashBucket.Remove(t, equalityComparer, out operationResult);
					if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
					{
						num--;
						sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, num2, hashBucketEqualityComparer, hashBucket2);
					}
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000038F0 File Offset: 0x00001AF0
		private static ImmutableHashSet<T>.MutationResult SymmetricExcept(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableHashSet<T> immutableHashSet = ImmutableHashSet.CreateRange<T>(origin.EqualityComparer, other);
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			foreach (T t in new ImmutableHashSet<T>.NodeEnumerable(origin.Root))
			{
				if (!immutableHashSet.Contains(t))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(t, new ImmutableHashSet<T>.MutationInput(sortedInt32KeyNode, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					sortedInt32KeyNode = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			foreach (T t2 in immutableHashSet)
			{
				if (!ImmutableHashSet<T>.Contains(t2, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult2 = ImmutableHashSet<T>.Add(t2, new ImmutableHashSet<T>.MutationInput(sortedInt32KeyNode, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					sortedInt32KeyNode = mutationResult2.Root;
					num += mutationResult2.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003A1C File Offset: 0x00001C1C
		private static bool IsProperSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return other.Any<T>();
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count >= hashSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (T t in hashSet)
			{
				if (ImmutableHashSet<T>.Contains(t, origin))
				{
					num++;
				}
				else
				{
					flag = true;
				}
				if (num == origin.Count && flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003AD0 File Offset: 0x00001CD0
		private static bool IsProperSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				num++;
				if (!ImmutableHashSet<T>.Contains(t, origin))
				{
					return false;
				}
			}
			return origin.Count > num;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003B5C File Offset: 0x00001D5C
		private static bool IsSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return true;
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			int num = 0;
			foreach (T t in hashSet)
			{
				if (ImmutableHashSet<T>.Contains(t, origin))
				{
					num++;
				}
			}
			return num == origin.Count;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private static ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableHashSet<T>(root, equalityComparer, count);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003C1A File Offset: 0x00001E1A
		private static IEqualityComparer<ImmutableHashSet<T>.HashBucket> GetHashBucketEqualityComparer(IEqualityComparer<T> valueComparer)
		{
			if (!ImmutableExtensions.IsValueType<T>())
			{
				return ImmutableHashSet<T>.HashBucketByRefEqualityComparer.DefaultInstance;
			}
			if (valueComparer == EqualityComparer<T>.Default)
			{
				return ImmutableHashSet<T>.HashBucketByValueEqualityComparer.DefaultInstance;
			}
			return new ImmutableHashSet<T>.HashBucketByValueEqualityComparer(valueComparer);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003C3D File Offset: 0x00001E3D
		private ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == this._root)
			{
				return this;
			}
			return new ImmutableHashSet<T>(root, this._equalityComparer, adjustedCountIfDifferentRoot);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003C58 File Offset: 0x00001E58
		private ImmutableHashSet<T> Union(IEnumerable<T> items, bool avoidWithComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty && !avoidWithComparer)
			{
				ImmutableHashSet<T> immutableHashSet = items as ImmutableHashSet<T>;
				if (immutableHashSet != null)
				{
					return immutableHashSet.WithComparer(this.KeyComparer);
				}
			}
			return ImmutableHashSet<T>.Union(items, this.Origin).Finalize(this);
		}

		// Token: 0x04000013 RID: 19
		public static readonly ImmutableHashSet<T> Empty = new ImmutableHashSet<T>(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, EqualityComparer<T>.Default, 0);

		// Token: 0x04000014 RID: 20
		private static readonly Action<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableHashSet<T>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x04000015 RID: 21
		private readonly IEqualityComparer<T> _equalityComparer;

		// Token: 0x04000016 RID: 22
		private readonly int _count;

		// Token: 0x04000017 RID: 23
		private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

		// Token: 0x04000018 RID: 24
		private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

		// Token: 0x0200004F RID: 79
		private class HashBucketByValueEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060003A9 RID: 937 RVA: 0x00009C4B File Offset: 0x00007E4B
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByValueEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00009C52 File Offset: 0x00007E52
			internal HashBucketByValueEqualityComparer(IEqualityComparer<T> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(valueComparer, "valueComparer");
				this._valueComparer = valueComparer;
			}

			// Token: 0x060003AB RID: 939 RVA: 0x00009C6C File Offset: 0x00007E6C
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByValue(y, this._valueComparer);
			}

			// Token: 0x060003AC RID: 940 RVA: 0x00009C7C File Offset: 0x00007E7C
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000058 RID: 88
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByValueEqualityComparer(EqualityComparer<T>.Default);

			// Token: 0x04000059 RID: 89
			private readonly IEqualityComparer<T> _valueComparer;
		}

		// Token: 0x02000050 RID: 80
		private class HashBucketByRefEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060003AE RID: 942 RVA: 0x00009C94 File Offset: 0x00007E94
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByRefEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x060003AF RID: 943 RVA: 0x00009C9B File Offset: 0x00007E9B
			private HashBucketByRefEqualityComparer()
			{
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00009CA3 File Offset: 0x00007EA3
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByRef(y);
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00009CAD File Offset: 0x00007EAD
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400005A RID: 90
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByRefEqualityComparer();
		}

		// Token: 0x02000051 RID: 81
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		public sealed class Builder : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, ICollection<T>
		{
			// Token: 0x060003B3 RID: 947 RVA: 0x00009CC0 File Offset: 0x00007EC0
			internal Builder(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._count = set._count;
				this._equalityComparer = set._equalityComparer;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
				this._immutable = set;
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x00009D20 File Offset: 0x00007F20
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060003B5 RID: 949 RVA: 0x00009D28 File Offset: 0x00007F28
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x00009D2B File Offset: 0x00007F2B
			// (set) Token: 0x060003B7 RID: 951 RVA: 0x00009D34 File Offset: 0x00007F34
			public IEqualityComparer<T> KeyComparer
			{
				get
				{
					return this._equalityComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<T>>(value, "value");
					if (value != this._equalityComparer)
					{
						ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Union(this, new ImmutableHashSet<T>.MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, value, this._hashBucketEqualityComparer, 0));
						this._immutable = null;
						this._equalityComparer = value;
						this.Root = mutationResult.Root;
						this._count = mutationResult.Count;
					}
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x00009D96 File Offset: 0x00007F96
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x00009D9E File Offset: 0x00007F9E
			[Nullable(0)]
			private ImmutableHashSet<T>.MutationInput Origin
			{
				get
				{
					return new ImmutableHashSet<T>.MutationInput(this.Root, this._equalityComparer, this._hashBucketEqualityComparer, this._count);
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060003BA RID: 954 RVA: 0x00009DBD File Offset: 0x00007FBD
			// (set) Token: 0x060003BB RID: 955 RVA: 0x00009DC5 File Offset: 0x00007FC5
			[Nullable(new byte[] { 1, 0, 0 })]
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
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

			// Token: 0x060003BC RID: 956 RVA: 0x00009DEC File Offset: 0x00007FEC
			[NullableContext(0)]
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, this);
			}

			// Token: 0x060003BD RID: 957 RVA: 0x00009DFA File Offset: 0x00007FFA
			public ImmutableHashSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableHashSet<T>.Wrap(this._root, this._equalityComparer, this._count);
				}
				return this._immutable;
			}

			// Token: 0x060003BE RID: 958 RVA: 0x00009E28 File Offset: 0x00008028
			public bool TryGetValue(T equalValue, out T actualValue)
			{
				int num = ((equalValue != null) ? this._equalityComparer.GetHashCode(equalValue) : 0);
				ImmutableHashSet<T>.HashBucket hashBucket;
				if (this._root.TryGetValue(num, out hashBucket))
				{
					return hashBucket.TryExchange(equalValue, this._equalityComparer, out actualValue);
				}
				actualValue = equalValue;
				return false;
			}

			// Token: 0x060003BF RID: 959 RVA: 0x00009E78 File Offset: 0x00008078
			public bool Add(T item)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, this.Origin);
				this.Apply(mutationResult);
				return mutationResult.Count != 0;
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x00009EA4 File Offset: 0x000080A4
			public bool Remove(T item)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Remove(item, this.Origin);
				this.Apply(mutationResult);
				return mutationResult.Count != 0;
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x00009ECF File Offset: 0x000080CF
			public bool Contains(T item)
			{
				return ImmutableHashSet<T>.Contains(item, this.Origin);
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x00009EDD File Offset: 0x000080DD
			public void Clear()
			{
				this._count = 0;
				this.Root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x00009EF4 File Offset: 0x000080F4
			public void ExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root);
				this.Apply(mutationResult);
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x00009F24 File Offset: 0x00008124
			public void IntersectWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Intersect(other, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x00009F45 File Offset: 0x00008145
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00009F53 File Offset: 0x00008153
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x00009F61 File Offset: 0x00008161
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x00009F6F File Offset: 0x0000816F
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x00009F7D File Offset: 0x0000817D
			public bool Overlaps(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.Overlaps(other, this.Origin);
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00009F8B File Offset: 0x0000818B
			public bool SetEquals(IEnumerable<T> other)
			{
				return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
			}

			// Token: 0x060003CB RID: 971 RVA: 0x00009FA0 File Offset: 0x000081A0
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.SymmetricExcept(other, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00009FC4 File Offset: 0x000081C4
			public void UnionWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Union(other, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x060003CD RID: 973 RVA: 0x00009FE5 File Offset: 0x000081E5
			void ICollection<T>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060003CE RID: 974 RVA: 0x00009FF0 File Offset: 0x000081F0
			void ICollection<T>.CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x060003CF RID: 975 RVA: 0x0000A07C File Offset: 0x0000827C
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0000A089 File Offset: 0x00008289
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x0000A096 File Offset: 0x00008296
			private void Apply(ImmutableHashSet<T>.MutationResult result)
			{
				this.Root = result.Root;
				if (result.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					this._count += result.Count;
					return;
				}
				this._count = result.Count;
			}

			// Token: 0x0400005B RID: 91
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;

			// Token: 0x0400005C RID: 92
			private IEqualityComparer<T> _equalityComparer;

			// Token: 0x0400005D RID: 93
			private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

			// Token: 0x0400005E RID: 94
			private int _count;

			// Token: 0x0400005F RID: 95
			private ImmutableHashSet<T> _immutable;

			// Token: 0x04000060 RID: 96
			private int _version;
		}

		// Token: 0x02000052 RID: 82
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, IStrongEnumerator<T>
		{
			// Token: 0x060003D2 RID: 978 RVA: 0x0000A0D0 File Offset: 0x000082D0
			internal Enumerator([Nullable(new byte[] { 1, 0, 0 })] SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, [Nullable(new byte[] { 2, 0 })] ImmutableHashSet<T>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000A103 File Offset: 0x00008303
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000A11B File Offset: 0x0000831B
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x0000A128 File Offset: 0x00008328
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableHashSet<T>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableHashSet<T>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x0000A182 File Offset: 0x00008382
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x0000A1C2 File Offset: 0x000083C2
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x0000A1DA File Offset: 0x000083DA
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x04000061 RID: 97
			private readonly ImmutableHashSet<T>.Builder _builder;

			// Token: 0x04000062 RID: 98
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x04000063 RID: 99
			private ImmutableHashSet<T>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x04000064 RID: 100
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000053 RID: 83
		[NullableContext(0)]
		internal enum OperationResult
		{
			// Token: 0x04000066 RID: 102
			SizeChanged,
			// Token: 0x04000067 RID: 103
			NoChangeRequired
		}

		// Token: 0x02000054 RID: 84
		[NullableContext(0)]
		internal readonly struct HashBucket
		{
			// Token: 0x060003D9 RID: 985 RVA: 0x0000A202 File Offset: 0x00008402
			private HashBucket(T firstElement, ImmutableList<T>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = additionalElements ?? ImmutableList<T>.Node.EmptyNode;
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060003DA RID: 986 RVA: 0x0000A21B File Offset: 0x0000841B
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x060003DB RID: 987 RVA: 0x0000A226 File Offset: 0x00008426
			public ImmutableHashSet<T>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.HashBucket.Enumerator(this);
			}

			// Token: 0x060003DC RID: 988 RVA: 0x0000A233 File Offset: 0x00008433
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060003DD RID: 989 RVA: 0x0000A23A File Offset: 0x0000843A
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060003DE RID: 990 RVA: 0x0000A241 File Offset: 0x00008441
			internal bool EqualsByRef(ImmutableHashSet<T>.HashBucket other)
			{
				return this._firstValue == other._firstValue && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060003DF RID: 991 RVA: 0x0000A26B File Offset: 0x0000846B
			internal bool EqualsByValue(ImmutableHashSet<T>.HashBucket other, [Nullable(1)] IEqualityComparer<T> valueComparer)
			{
				return valueComparer.Equals(this._firstValue, other._firstValue) && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x0000A294 File Offset: 0x00008494
			internal ImmutableHashSet<T>.HashBucket Add([Nullable(1)] T value, [Nullable(1)] IEqualityComparer<T> valueComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(value, null);
				}
				if (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				result = ImmutableHashSet<T>.OperationResult.SizeChanged;
				return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.Add(value));
			}

			// Token: 0x060003E1 RID: 993 RVA: 0x0000A2F7 File Offset: 0x000084F7
			[NullableContext(1)]
			internal bool Contains(T value, IEqualityComparer<T> valueComparer)
			{
				return !this.IsEmpty && (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0);
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x0000A328 File Offset: 0x00008528
			[NullableContext(1)]
			internal bool TryExchange(T value, IEqualityComparer<T> valueComparer, out T existingValue)
			{
				if (!this.IsEmpty)
				{
					if (valueComparer.Equals(value, this._firstValue))
					{
						existingValue = this._firstValue;
						return true;
					}
					int num = this._additionalElements.IndexOf(value, valueComparer);
					if (num >= 0)
					{
						existingValue = this._additionalElements[num];
						return true;
					}
				}
				existingValue = value;
				return false;
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x0000A388 File Offset: 0x00008588
			internal ImmutableHashSet<T>.HashBucket Remove([Nullable(1)] T value, [Nullable(1)] IEqualityComparer<T> equalityComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				if (equalityComparer.Equals(this._firstValue, value))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableHashSet<T>.OperationResult.SizeChanged;
						return default(ImmutableHashSet<T>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(value, equalityComparer);
					if (num < 0)
					{
						result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x060003E4 RID: 996 RVA: 0x0000A437 File Offset: 0x00008637
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x04000068 RID: 104
			private readonly T _firstValue;

			// Token: 0x04000069 RID: 105
			private readonly ImmutableList<T>.Node _additionalElements;

			// Token: 0x0200007E RID: 126
			internal struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
			{
				// Token: 0x06000643 RID: 1603 RVA: 0x00010DB3 File Offset: 0x0000EFB3
				internal Enumerator(ImmutableHashSet<T>.HashBucket bucket)
				{
					this._disposed = false;
					this._bucket = bucket;
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<T>.Enumerator);
				}

				// Token: 0x1700014C RID: 332
				// (get) Token: 0x06000644 RID: 1604 RVA: 0x00010DD6 File Offset: 0x0000EFD6
				[Nullable(2)]
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x1700014D RID: 333
				// (get) Token: 0x06000645 RID: 1605 RVA: 0x00010DE4 File Offset: 0x0000EFE4
				[Nullable(1)]
				public T Current
				{
					[NullableContext(1)]
					get
					{
						this.ThrowIfDisposed();
						ImmutableHashSet<T>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						T t;
						if (currentPosition != ImmutableHashSet<T>.HashBucket.Enumerator.Position.First)
						{
							if (currentPosition != ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional)
							{
								throw new InvalidOperationException();
							}
							t = this._additionalEnumerator.Current;
						}
						else
						{
							t = this._bucket._firstValue;
						}
						return t;
					}
				}

				// Token: 0x06000646 RID: 1606 RVA: 0x00010E2C File Offset: 0x0000F02C
				public bool MoveNext()
				{
					this.ThrowIfDisposed();
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<T>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x06000647 RID: 1607 RVA: 0x00010ED8 File Offset: 0x0000F0D8
				public void Reset()
				{
					this.ThrowIfDisposed();
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x06000648 RID: 1608 RVA: 0x00010EF2 File Offset: 0x0000F0F2
				public void Dispose()
				{
					this._disposed = true;
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x06000649 RID: 1609 RVA: 0x00010F06 File Offset: 0x0000F106
				private void ThrowIfDisposed()
				{
					if (this._disposed)
					{
						Requires.FailObjectDisposed<ImmutableHashSet<T>.HashBucket.Enumerator>(this);
					}
				}

				// Token: 0x04000107 RID: 263
				private readonly ImmutableHashSet<T>.HashBucket _bucket;

				// Token: 0x04000108 RID: 264
				private bool _disposed;

				// Token: 0x04000109 RID: 265
				private ImmutableHashSet<T>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x0400010A RID: 266
				private ImmutableList<T>.Enumerator _additionalEnumerator;

				// Token: 0x02000084 RID: 132
				private enum Position
				{
					// Token: 0x04000120 RID: 288
					BeforeFirst,
					// Token: 0x04000121 RID: 289
					First,
					// Token: 0x04000122 RID: 290
					Additional,
					// Token: 0x04000123 RID: 291
					End
				}
			}
		}

		// Token: 0x02000055 RID: 85
		private readonly struct MutationInput
		{
			// Token: 0x060003E5 RID: 997 RVA: 0x0000A44C File Offset: 0x0000864C
			internal MutationInput(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._equalityComparer = set._equalityComparer;
				this._count = set._count;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x0000A48C File Offset: 0x0000868C
			internal MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, int count)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				Requires.Range(count >= 0, "count", null);
				Requires.NotNull<IEqualityComparer<ImmutableHashSet<T>.HashBucket>>(hashBucketEqualityComparer, "hashBucketEqualityComparer");
				this._root = root;
				this._equalityComparer = equalityComparer;
				this._count = count;
				this._hashBucketEqualityComparer = hashBucketEqualityComparer;
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000A4EA File Offset: 0x000086EA
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000A4F2 File Offset: 0x000086F2
			internal IEqualityComparer<T> EqualityComparer
			{
				get
				{
					return this._equalityComparer;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000A4FA File Offset: 0x000086FA
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000A502 File Offset: 0x00008702
			internal IEqualityComparer<ImmutableHashSet<T>.HashBucket> HashBucketEqualityComparer
			{
				get
				{
					return this._hashBucketEqualityComparer;
				}
			}

			// Token: 0x0400006A RID: 106
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x0400006B RID: 107
			private readonly IEqualityComparer<T> _equalityComparer;

			// Token: 0x0400006C RID: 108
			private readonly int _count;

			// Token: 0x0400006D RID: 109
			private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;
		}

		// Token: 0x02000056 RID: 86
		private enum CountType
		{
			// Token: 0x0400006F RID: 111
			Adjustment,
			// Token: 0x04000070 RID: 112
			FinalValue
		}

		// Token: 0x02000057 RID: 87
		private readonly struct MutationResult
		{
			// Token: 0x060003EB RID: 1003 RVA: 0x0000A50A File Offset: 0x0000870A
			internal MutationResult(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int count, ImmutableHashSet<T>.CountType countType = ImmutableHashSet<T>.CountType.Adjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
				this._count = count;
				this._countType = countType;
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000A52C File Offset: 0x0000872C
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000A534 File Offset: 0x00008734
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000A53C File Offset: 0x0000873C
			internal ImmutableHashSet<T>.CountType CountType
			{
				get
				{
					return this._countType;
				}
			}

			// Token: 0x060003EF RID: 1007 RVA: 0x0000A544 File Offset: 0x00008744
			internal ImmutableHashSet<T> Finalize(ImmutableHashSet<T> priorSet)
			{
				Requires.NotNull<ImmutableHashSet<T>>(priorSet, "priorSet");
				int num = this.Count;
				if (this.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					num += priorSet._count;
				}
				return priorSet.Wrap(this.Root, num);
			}

			// Token: 0x04000071 RID: 113
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x04000072 RID: 114
			private readonly int _count;

			// Token: 0x04000073 RID: 115
			private readonly ImmutableHashSet<T>.CountType _countType;
		}

		// Token: 0x02000058 RID: 88
		private readonly struct NodeEnumerable : IEnumerable<T>, IEnumerable
		{
			// Token: 0x060003F0 RID: 1008 RVA: 0x0000A581 File Offset: 0x00008781
			internal NodeEnumerable(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x0000A595 File Offset: 0x00008795
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, null);
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x0000A5A3 File Offset: 0x000087A3
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x0000A5B0 File Offset: 0x000087B0
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000074 RID: 116
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;
		}
	}
}
