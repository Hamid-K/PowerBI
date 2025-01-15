using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002072 RID: 8306
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableHashSet<[Nullable(2)] T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IHashKeyCollection<T>, ICollection<T>, ISet<T>, ICollection, IStrongEnumerable<T, ImmutableHashSet<T>.Enumerator>
	{
		// Token: 0x06011451 RID: 70737 RVA: 0x003B615A File Offset: 0x003B435A
		internal ImmutableHashSet(IEqualityComparer<T> equalityComparer)
			: this(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, equalityComparer, 0)
		{
		}

		// Token: 0x06011452 RID: 70738 RVA: 0x003B616C File Offset: 0x003B436C
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

		// Token: 0x06011453 RID: 70739 RVA: 0x003B61C1 File Offset: 0x003B43C1
		public ImmutableHashSet<T> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableHashSet<T>.Empty.WithComparer(this._equalityComparer);
			}
			return this;
		}

		// Token: 0x17002E23 RID: 11811
		// (get) Token: 0x06011454 RID: 70740 RVA: 0x003B61DD File Offset: 0x003B43DD
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17002E24 RID: 11812
		// (get) Token: 0x06011455 RID: 70741 RVA: 0x003B61E5 File Offset: 0x003B43E5
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17002E25 RID: 11813
		// (get) Token: 0x06011456 RID: 70742 RVA: 0x003B61F0 File Offset: 0x003B43F0
		public IEqualityComparer<T> KeyComparer
		{
			get
			{
				return this._equalityComparer;
			}
		}

		// Token: 0x06011457 RID: 70743 RVA: 0x003B61F8 File Offset: 0x003B43F8
		IImmutableSet<T> IImmutableSet<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17002E26 RID: 11814
		// (get) Token: 0x06011458 RID: 70744 RVA: 0x00004FAE File Offset: 0x000031AE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002E27 RID: 11815
		// (get) Token: 0x06011459 RID: 70745 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E28 RID: 11816
		// (get) Token: 0x0601145A RID: 70746 RVA: 0x003B6200 File Offset: 0x003B4400
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17002E29 RID: 11817
		// (get) Token: 0x0601145B RID: 70747 RVA: 0x003B6208 File Offset: 0x003B4408
		[Nullable(0)]
		private ImmutableHashSet<T>.MutationInput Origin
		{
			get
			{
				return new ImmutableHashSet<T>.MutationInput(this);
			}
		}

		// Token: 0x0601145C RID: 70748 RVA: 0x003B6210 File Offset: 0x003B4410
		[return: Nullable(new byte[] { 1, 0 })]
		public ImmutableHashSet<T>.Builder ToBuilder()
		{
			return new ImmutableHashSet<T>.Builder(this);
		}

		// Token: 0x0601145D RID: 70749 RVA: 0x003B6218 File Offset: 0x003B4418
		public ImmutableHashSet<T> Add(T item)
		{
			return ImmutableHashSet<T>.Add(item, this.Origin).Finalize(this);
		}

		// Token: 0x0601145E RID: 70750 RVA: 0x003B623C File Offset: 0x003B443C
		public ImmutableHashSet<T> Remove(T item)
		{
			return ImmutableHashSet<T>.Remove(item, this.Origin).Finalize(this);
		}

		// Token: 0x0601145F RID: 70751 RVA: 0x003B6260 File Offset: 0x003B4460
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

		// Token: 0x06011460 RID: 70752 RVA: 0x003B62AD File Offset: 0x003B44AD
		public ImmutableHashSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this.Union(other, false);
		}

		// Token: 0x06011461 RID: 70753 RVA: 0x003B62C4 File Offset: 0x003B44C4
		public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Intersect(other, this.Origin).Finalize(this);
		}

		// Token: 0x06011462 RID: 70754 RVA: 0x003B62F4 File Offset: 0x003B44F4
		public ImmutableHashSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root).Finalize(this);
		}

		// Token: 0x06011463 RID: 70755 RVA: 0x003B6330 File Offset: 0x003B4530
		public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.SymmetricExcept(other, this.Origin).Finalize(this);
		}

		// Token: 0x06011464 RID: 70756 RVA: 0x003B635D File Offset: 0x003B455D
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
		}

		// Token: 0x06011465 RID: 70757 RVA: 0x003B637C File Offset: 0x003B457C
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
		}

		// Token: 0x06011466 RID: 70758 RVA: 0x003B6395 File Offset: 0x003B4595
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
		}

		// Token: 0x06011467 RID: 70759 RVA: 0x003B63AE File Offset: 0x003B45AE
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
		}

		// Token: 0x06011468 RID: 70760 RVA: 0x003B63C7 File Offset: 0x003B45C7
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
		}

		// Token: 0x06011469 RID: 70761 RVA: 0x003B63E0 File Offset: 0x003B45E0
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Overlaps(other, this.Origin);
		}

		// Token: 0x0601146A RID: 70762 RVA: 0x003B63F9 File Offset: 0x003B45F9
		IImmutableSet<T> IImmutableSet<T>.Add(T item)
		{
			return this.Add(item);
		}

		// Token: 0x0601146B RID: 70763 RVA: 0x003B6402 File Offset: 0x003B4602
		IImmutableSet<T> IImmutableSet<T>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x0601146C RID: 70764 RVA: 0x003B640B File Offset: 0x003B460B
		IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x0601146D RID: 70765 RVA: 0x003B6414 File Offset: 0x003B4614
		IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x0601146E RID: 70766 RVA: 0x003B641D File Offset: 0x003B461D
		IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x0601146F RID: 70767 RVA: 0x003B6426 File Offset: 0x003B4626
		IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x06011470 RID: 70768 RVA: 0x003B642F File Offset: 0x003B462F
		public bool Contains(T item)
		{
			return ImmutableHashSet<T>.Contains(item, this.Origin);
		}

		// Token: 0x06011471 RID: 70769 RVA: 0x003B6440 File Offset: 0x003B4640
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

		// Token: 0x06011472 RID: 70770 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ISet<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011473 RID: 70771 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011474 RID: 70772 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011475 RID: 70773 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011476 RID: 70774 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002E2A RID: 11818
		// (get) Token: 0x06011477 RID: 70775 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06011478 RID: 70776 RVA: 0x003B6474 File Offset: 0x003B4674
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

		// Token: 0x06011479 RID: 70777 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601147A RID: 70778 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601147B RID: 70779 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601147C RID: 70780 RVA: 0x003B6500 File Offset: 0x003B4700
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, arrayIndex++);
			}
		}

		// Token: 0x0601147D RID: 70781 RVA: 0x003B6594 File Offset: 0x003B4794
		[NullableContext(0)]
		public ImmutableHashSet<T>.Enumerator GetEnumerator()
		{
			return new ImmutableHashSet<T>.Enumerator(this._root, null);
		}

		// Token: 0x0601147E RID: 70782 RVA: 0x003B65A4 File Offset: 0x003B47A4
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x0601147F RID: 70783 RVA: 0x003B65D1 File Offset: 0x003B47D1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06011480 RID: 70784 RVA: 0x003B65E0 File Offset: 0x003B47E0
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

		// Token: 0x06011481 RID: 70785 RVA: 0x003B664C File Offset: 0x003B484C
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

		// Token: 0x06011482 RID: 70786 RVA: 0x003B66C8 File Offset: 0x003B48C8
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

		// Token: 0x06011483 RID: 70787 RVA: 0x003B6754 File Offset: 0x003B4954
		private static bool Contains(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int num = ((item != null) ? origin.EqualityComparer.GetHashCode(item) : 0);
			ImmutableHashSet<T>.HashBucket hashBucket;
			return origin.Root.TryGetValue(num, out hashBucket) && hashBucket.Contains(item, origin.EqualityComparer);
		}

		// Token: 0x06011484 RID: 70788 RVA: 0x003B679C File Offset: 0x003B499C
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

		// Token: 0x06011485 RID: 70789 RVA: 0x003B6860 File Offset: 0x003B4A60
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

		// Token: 0x06011486 RID: 70790 RVA: 0x003B68DC File Offset: 0x003B4ADC
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

		// Token: 0x06011487 RID: 70791 RVA: 0x003B6960 File Offset: 0x003B4B60
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

		// Token: 0x06011488 RID: 70792 RVA: 0x003B6990 File Offset: 0x003B4B90
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

		// Token: 0x06011489 RID: 70793 RVA: 0x003B6A38 File Offset: 0x003B4C38
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

		// Token: 0x0601148A RID: 70794 RVA: 0x003B6AFC File Offset: 0x003B4CFC
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

		// Token: 0x0601148B RID: 70795 RVA: 0x003B6C28 File Offset: 0x003B4E28
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

		// Token: 0x0601148C RID: 70796 RVA: 0x003B6CDC File Offset: 0x003B4EDC
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

		// Token: 0x0601148D RID: 70797 RVA: 0x003B6D68 File Offset: 0x003B4F68
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

		// Token: 0x0601148E RID: 70798 RVA: 0x003B6DF4 File Offset: 0x003B4FF4
		private static ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableHashSet<T>(root, equalityComparer, count);
		}

		// Token: 0x0601148F RID: 70799 RVA: 0x003B6E26 File Offset: 0x003B5026
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

		// Token: 0x06011490 RID: 70800 RVA: 0x003B6E49 File Offset: 0x003B5049
		private ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == this._root)
			{
				return this;
			}
			return new ImmutableHashSet<T>(root, this._equalityComparer, adjustedCountIfDifferentRoot);
		}

		// Token: 0x06011491 RID: 70801 RVA: 0x003B6E64 File Offset: 0x003B5064
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

		// Token: 0x040068A6 RID: 26790
		public static readonly ImmutableHashSet<T> Empty = new ImmutableHashSet<T>(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, EqualityComparer<T>.Default, 0);

		// Token: 0x040068A7 RID: 26791
		private static readonly Action<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableHashSet<T>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x040068A8 RID: 26792
		private readonly IEqualityComparer<T> _equalityComparer;

		// Token: 0x040068A9 RID: 26793
		private readonly int _count;

		// Token: 0x040068AA RID: 26794
		private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

		// Token: 0x040068AB RID: 26795
		private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

		// Token: 0x02002073 RID: 8307
		private class HashBucketByValueEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x17002E2B RID: 11819
			// (get) Token: 0x06011493 RID: 70803 RVA: 0x003B6EDF File Offset: 0x003B50DF
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByValueEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x06011494 RID: 70804 RVA: 0x003B6EE6 File Offset: 0x003B50E6
			internal HashBucketByValueEqualityComparer(IEqualityComparer<T> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(valueComparer, "valueComparer");
				this._valueComparer = valueComparer;
			}

			// Token: 0x06011495 RID: 70805 RVA: 0x003B6F00 File Offset: 0x003B5100
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByValue(y, this._valueComparer);
			}

			// Token: 0x06011496 RID: 70806 RVA: 0x00002C72 File Offset: 0x00000E72
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040068AC RID: 26796
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByValueEqualityComparer(EqualityComparer<T>.Default);

			// Token: 0x040068AD RID: 26797
			private readonly IEqualityComparer<T> _valueComparer;
		}

		// Token: 0x02002074 RID: 8308
		private class HashBucketByRefEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x17002E2C RID: 11820
			// (get) Token: 0x06011498 RID: 70808 RVA: 0x003B6F21 File Offset: 0x003B5121
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByRefEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x06011499 RID: 70809 RVA: 0x00002130 File Offset: 0x00000330
			private HashBucketByRefEqualityComparer()
			{
			}

			// Token: 0x0601149A RID: 70810 RVA: 0x003B6F28 File Offset: 0x003B5128
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByRef(y);
			}

			// Token: 0x0601149B RID: 70811 RVA: 0x00002C72 File Offset: 0x00000E72
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040068AE RID: 26798
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByRefEqualityComparer();
		}

		// Token: 0x02002075 RID: 8309
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		public sealed class Builder : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, ICollection<T>
		{
			// Token: 0x0601149D RID: 70813 RVA: 0x003B6F40 File Offset: 0x003B5140
			internal Builder(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._count = set._count;
				this._equalityComparer = set._equalityComparer;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
				this._immutable = set;
			}

			// Token: 0x17002E2D RID: 11821
			// (get) Token: 0x0601149E RID: 70814 RVA: 0x003B6FA0 File Offset: 0x003B51A0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17002E2E RID: 11822
			// (get) Token: 0x0601149F RID: 70815 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002E2F RID: 11823
			// (get) Token: 0x060114A0 RID: 70816 RVA: 0x003B6FA8 File Offset: 0x003B51A8
			// (set) Token: 0x060114A1 RID: 70817 RVA: 0x003B6FB0 File Offset: 0x003B51B0
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

			// Token: 0x17002E30 RID: 11824
			// (get) Token: 0x060114A2 RID: 70818 RVA: 0x003B7012 File Offset: 0x003B5212
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17002E31 RID: 11825
			// (get) Token: 0x060114A3 RID: 70819 RVA: 0x003B701A File Offset: 0x003B521A
			[Nullable(0)]
			private ImmutableHashSet<T>.MutationInput Origin
			{
				get
				{
					return new ImmutableHashSet<T>.MutationInput(this.Root, this._equalityComparer, this._hashBucketEqualityComparer, this._count);
				}
			}

			// Token: 0x17002E32 RID: 11826
			// (get) Token: 0x060114A4 RID: 70820 RVA: 0x003B7039 File Offset: 0x003B5239
			// (set) Token: 0x060114A5 RID: 70821 RVA: 0x003B7041 File Offset: 0x003B5241
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

			// Token: 0x060114A6 RID: 70822 RVA: 0x003B7068 File Offset: 0x003B5268
			[NullableContext(0)]
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, this);
			}

			// Token: 0x060114A7 RID: 70823 RVA: 0x003B7076 File Offset: 0x003B5276
			public ImmutableHashSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableHashSet<T>.Wrap(this._root, this._equalityComparer, this._count);
				}
				return this._immutable;
			}

			// Token: 0x060114A8 RID: 70824 RVA: 0x003B70A4 File Offset: 0x003B52A4
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

			// Token: 0x060114A9 RID: 70825 RVA: 0x003B70F4 File Offset: 0x003B52F4
			public bool Add(T item)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, this.Origin);
				this.Apply(mutationResult);
				return mutationResult.Count != 0;
			}

			// Token: 0x060114AA RID: 70826 RVA: 0x003B7120 File Offset: 0x003B5320
			public bool Remove(T item)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Remove(item, this.Origin);
				this.Apply(mutationResult);
				return mutationResult.Count != 0;
			}

			// Token: 0x060114AB RID: 70827 RVA: 0x003B714B File Offset: 0x003B534B
			public bool Contains(T item)
			{
				return ImmutableHashSet<T>.Contains(item, this.Origin);
			}

			// Token: 0x060114AC RID: 70828 RVA: 0x003B7159 File Offset: 0x003B5359
			public void Clear()
			{
				this._count = 0;
				this.Root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			}

			// Token: 0x060114AD RID: 70829 RVA: 0x003B7170 File Offset: 0x003B5370
			public void ExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root);
				this.Apply(mutationResult);
			}

			// Token: 0x060114AE RID: 70830 RVA: 0x003B71A0 File Offset: 0x003B53A0
			public void IntersectWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Intersect(other, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x060114AF RID: 70831 RVA: 0x003B71C1 File Offset: 0x003B53C1
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
			}

			// Token: 0x060114B0 RID: 70832 RVA: 0x003B71CF File Offset: 0x003B53CF
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
			}

			// Token: 0x060114B1 RID: 70833 RVA: 0x003B71DD File Offset: 0x003B53DD
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
			}

			// Token: 0x060114B2 RID: 70834 RVA: 0x003B71EB File Offset: 0x003B53EB
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
			}

			// Token: 0x060114B3 RID: 70835 RVA: 0x003B71F9 File Offset: 0x003B53F9
			public bool Overlaps(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.Overlaps(other, this.Origin);
			}

			// Token: 0x060114B4 RID: 70836 RVA: 0x003B7207 File Offset: 0x003B5407
			public bool SetEquals(IEnumerable<T> other)
			{
				return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
			}

			// Token: 0x060114B5 RID: 70837 RVA: 0x003B721C File Offset: 0x003B541C
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.SymmetricExcept(other, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x060114B6 RID: 70838 RVA: 0x003B7240 File Offset: 0x003B5440
			public void UnionWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Union(other, this.Origin);
				this.Apply(mutationResult);
			}

			// Token: 0x060114B7 RID: 70839 RVA: 0x003B7261 File Offset: 0x003B5461
			void ICollection<T>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060114B8 RID: 70840 RVA: 0x003B726C File Offset: 0x003B546C
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

			// Token: 0x060114B9 RID: 70841 RVA: 0x003B72F8 File Offset: 0x003B54F8
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060114BA RID: 70842 RVA: 0x003B72F8 File Offset: 0x003B54F8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060114BB RID: 70843 RVA: 0x003B7305 File Offset: 0x003B5505
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

			// Token: 0x040068AF RID: 26799
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;

			// Token: 0x040068B0 RID: 26800
			private IEqualityComparer<T> _equalityComparer;

			// Token: 0x040068B1 RID: 26801
			private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

			// Token: 0x040068B2 RID: 26802
			private int _count;

			// Token: 0x040068B3 RID: 26803
			private ImmutableHashSet<T> _immutable;

			// Token: 0x040068B4 RID: 26804
			private int _version;
		}

		// Token: 0x02002076 RID: 8310
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, IStrongEnumerator<T>
		{
			// Token: 0x060114BC RID: 70844 RVA: 0x003B733F File Offset: 0x003B553F
			internal Enumerator([Nullable(new byte[] { 1, 0, 0 })] SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, [Nullable(new byte[] { 2, 0 })] ImmutableHashSet<T>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
			}

			// Token: 0x17002E33 RID: 11827
			// (get) Token: 0x060114BD RID: 70845 RVA: 0x003B7372 File Offset: 0x003B5572
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

			// Token: 0x17002E34 RID: 11828
			// (get) Token: 0x060114BE RID: 70846 RVA: 0x003B738A File Offset: 0x003B558A
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060114BF RID: 70847 RVA: 0x003B7398 File Offset: 0x003B5598
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

			// Token: 0x060114C0 RID: 70848 RVA: 0x003B73F2 File Offset: 0x003B55F2
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
			}

			// Token: 0x060114C1 RID: 70849 RVA: 0x003B7432 File Offset: 0x003B5632
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x060114C2 RID: 70850 RVA: 0x003B744A File Offset: 0x003B564A
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x040068B5 RID: 26805
			private readonly ImmutableHashSet<T>.Builder _builder;

			// Token: 0x040068B6 RID: 26806
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x040068B7 RID: 26807
			private ImmutableHashSet<T>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x040068B8 RID: 26808
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02002077 RID: 8311
		[NullableContext(0)]
		internal enum OperationResult
		{
			// Token: 0x040068BA RID: 26810
			SizeChanged,
			// Token: 0x040068BB RID: 26811
			NoChangeRequired
		}

		// Token: 0x02002078 RID: 8312
		[NullableContext(0)]
		internal readonly struct HashBucket
		{
			// Token: 0x060114C3 RID: 70851 RVA: 0x003B7472 File Offset: 0x003B5672
			private HashBucket(T firstElement, ImmutableList<T>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = additionalElements ?? ImmutableList<T>.Node.EmptyNode;
			}

			// Token: 0x17002E35 RID: 11829
			// (get) Token: 0x060114C4 RID: 70852 RVA: 0x003B748B File Offset: 0x003B568B
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x060114C5 RID: 70853 RVA: 0x003B7496 File Offset: 0x003B5696
			public ImmutableHashSet<T>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.HashBucket.Enumerator(this);
			}

			// Token: 0x060114C6 RID: 70854 RVA: 0x00002C72 File Offset: 0x00000E72
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060114C7 RID: 70855 RVA: 0x00002C72 File Offset: 0x00000E72
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060114C8 RID: 70856 RVA: 0x003B74A3 File Offset: 0x003B56A3
			internal bool EqualsByRef(ImmutableHashSet<T>.HashBucket other)
			{
				return this._firstValue == other._firstValue && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060114C9 RID: 70857 RVA: 0x003B74CD File Offset: 0x003B56CD
			internal bool EqualsByValue(ImmutableHashSet<T>.HashBucket other, [Nullable(1)] IEqualityComparer<T> valueComparer)
			{
				return valueComparer.Equals(this._firstValue, other._firstValue) && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060114CA RID: 70858 RVA: 0x003B74F4 File Offset: 0x003B56F4
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

			// Token: 0x060114CB RID: 70859 RVA: 0x003B7557 File Offset: 0x003B5757
			[NullableContext(1)]
			internal bool Contains(T value, IEqualityComparer<T> valueComparer)
			{
				return !this.IsEmpty && (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0);
			}

			// Token: 0x060114CC RID: 70860 RVA: 0x003B7588 File Offset: 0x003B5788
			[NullableContext(1)]
			internal unsafe bool TryExchange(T value, IEqualityComparer<T> valueComparer, out T existingValue)
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
						existingValue = *this._additionalElements.ItemRef(num);
						return true;
					}
				}
				existingValue = value;
				return false;
			}

			// Token: 0x060114CD RID: 70861 RVA: 0x003B75F0 File Offset: 0x003B57F0
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

			// Token: 0x060114CE RID: 70862 RVA: 0x003B769F File Offset: 0x003B589F
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x040068BC RID: 26812
			private readonly T _firstValue;

			// Token: 0x040068BD RID: 26813
			private readonly ImmutableList<T>.Node _additionalElements;

			// Token: 0x02002079 RID: 8313
			internal struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
			{
				// Token: 0x060114CF RID: 70863 RVA: 0x003B76B4 File Offset: 0x003B58B4
				internal Enumerator(ImmutableHashSet<T>.HashBucket bucket)
				{
					this._disposed = false;
					this._bucket = bucket;
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<T>.Enumerator);
				}

				// Token: 0x17002E36 RID: 11830
				// (get) Token: 0x060114D0 RID: 70864 RVA: 0x003B76D7 File Offset: 0x003B58D7
				[Nullable(2)]
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17002E37 RID: 11831
				// (get) Token: 0x060114D1 RID: 70865 RVA: 0x003B76E4 File Offset: 0x003B58E4
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

				// Token: 0x060114D2 RID: 70866 RVA: 0x003B772C File Offset: 0x003B592C
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

				// Token: 0x060114D3 RID: 70867 RVA: 0x003B77D8 File Offset: 0x003B59D8
				public void Reset()
				{
					this.ThrowIfDisposed();
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x060114D4 RID: 70868 RVA: 0x003B77F2 File Offset: 0x003B59F2
				public void Dispose()
				{
					this._disposed = true;
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x060114D5 RID: 70869 RVA: 0x003B7806 File Offset: 0x003B5A06
				private void ThrowIfDisposed()
				{
					if (this._disposed)
					{
						Requires.FailObjectDisposed<ImmutableHashSet<T>.HashBucket.Enumerator>(this);
					}
				}

				// Token: 0x040068BE RID: 26814
				private readonly ImmutableHashSet<T>.HashBucket _bucket;

				// Token: 0x040068BF RID: 26815
				private bool _disposed;

				// Token: 0x040068C0 RID: 26816
				private ImmutableHashSet<T>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x040068C1 RID: 26817
				private ImmutableList<T>.Enumerator _additionalEnumerator;

				// Token: 0x0200207A RID: 8314
				private enum Position
				{
					// Token: 0x040068C3 RID: 26819
					BeforeFirst,
					// Token: 0x040068C4 RID: 26820
					First,
					// Token: 0x040068C5 RID: 26821
					Additional,
					// Token: 0x040068C6 RID: 26822
					End
				}
			}
		}

		// Token: 0x0200207B RID: 8315
		private readonly struct MutationInput
		{
			// Token: 0x060114D6 RID: 70870 RVA: 0x003B781B File Offset: 0x003B5A1B
			internal MutationInput(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._equalityComparer = set._equalityComparer;
				this._count = set._count;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
			}

			// Token: 0x060114D7 RID: 70871 RVA: 0x003B7858 File Offset: 0x003B5A58
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

			// Token: 0x17002E38 RID: 11832
			// (get) Token: 0x060114D8 RID: 70872 RVA: 0x003B78B6 File Offset: 0x003B5AB6
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17002E39 RID: 11833
			// (get) Token: 0x060114D9 RID: 70873 RVA: 0x003B78BE File Offset: 0x003B5ABE
			internal IEqualityComparer<T> EqualityComparer
			{
				get
				{
					return this._equalityComparer;
				}
			}

			// Token: 0x17002E3A RID: 11834
			// (get) Token: 0x060114DA RID: 70874 RVA: 0x003B78C6 File Offset: 0x003B5AC6
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17002E3B RID: 11835
			// (get) Token: 0x060114DB RID: 70875 RVA: 0x003B78CE File Offset: 0x003B5ACE
			internal IEqualityComparer<ImmutableHashSet<T>.HashBucket> HashBucketEqualityComparer
			{
				get
				{
					return this._hashBucketEqualityComparer;
				}
			}

			// Token: 0x040068C7 RID: 26823
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x040068C8 RID: 26824
			private readonly IEqualityComparer<T> _equalityComparer;

			// Token: 0x040068C9 RID: 26825
			private readonly int _count;

			// Token: 0x040068CA RID: 26826
			private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;
		}

		// Token: 0x0200207C RID: 8316
		private enum CountType
		{
			// Token: 0x040068CC RID: 26828
			Adjustment,
			// Token: 0x040068CD RID: 26829
			FinalValue
		}

		// Token: 0x0200207D RID: 8317
		private readonly struct MutationResult
		{
			// Token: 0x060114DC RID: 70876 RVA: 0x003B78D6 File Offset: 0x003B5AD6
			internal MutationResult(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int count, ImmutableHashSet<T>.CountType countType = ImmutableHashSet<T>.CountType.Adjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
				this._count = count;
				this._countType = countType;
			}

			// Token: 0x17002E3C RID: 11836
			// (get) Token: 0x060114DD RID: 70877 RVA: 0x003B78F8 File Offset: 0x003B5AF8
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17002E3D RID: 11837
			// (get) Token: 0x060114DE RID: 70878 RVA: 0x003B7900 File Offset: 0x003B5B00
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17002E3E RID: 11838
			// (get) Token: 0x060114DF RID: 70879 RVA: 0x003B7908 File Offset: 0x003B5B08
			internal ImmutableHashSet<T>.CountType CountType
			{
				get
				{
					return this._countType;
				}
			}

			// Token: 0x060114E0 RID: 70880 RVA: 0x003B7910 File Offset: 0x003B5B10
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

			// Token: 0x040068CE RID: 26830
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x040068CF RID: 26831
			private readonly int _count;

			// Token: 0x040068D0 RID: 26832
			private readonly ImmutableHashSet<T>.CountType _countType;
		}

		// Token: 0x0200207E RID: 8318
		private readonly struct NodeEnumerable : IEnumerable<T>, IEnumerable
		{
			// Token: 0x060114E1 RID: 70881 RVA: 0x003B794D File Offset: 0x003B5B4D
			internal NodeEnumerable(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
			}

			// Token: 0x060114E2 RID: 70882 RVA: 0x003B7961 File Offset: 0x003B5B61
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, null);
			}

			// Token: 0x060114E3 RID: 70883 RVA: 0x003B796F File Offset: 0x003B5B6F
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060114E4 RID: 70884 RVA: 0x003B796F File Offset: 0x003B5B6F
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040068D1 RID: 26833
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;
		}
	}
}
