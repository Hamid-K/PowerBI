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
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableSortedSet<[Nullable(2)] T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISortKeyCollection<T>, IReadOnlyList<T>, IList<T>, ICollection<T>, ISet<T>, IList, ICollection, IStrongEnumerable<T, ImmutableSortedSet<T>.Enumerator>
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000836C File Offset: 0x0000656C
		internal ImmutableSortedSet([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer = null)
		{
			this._root = ImmutableSortedSet<T>.Node.EmptyNode;
			this._comparer = comparer ?? Comparer<T>.Default;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000838F File Offset: 0x0000658F
		private ImmutableSortedSet(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			root.Freeze();
			this._root = root;
			this._comparer = comparer;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000083C1 File Offset: 0x000065C1
		public ImmutableSortedSet<T> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedSet<T>.Empty.WithComparer(this._comparer);
			}
			return this;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000083E2 File Offset: 0x000065E2
		[Nullable(2)]
		public T Max
		{
			[NullableContext(2)]
			get
			{
				return this._root.Max;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000083EF File Offset: 0x000065EF
		[Nullable(2)]
		public T Min
		{
			[NullableContext(2)]
			get
			{
				return this._root.Min;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000083FC File Offset: 0x000065FC
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00008409 File Offset: 0x00006609
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00008416 File Offset: 0x00006616
		public IComparer<T> KeyComparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000841E File Offset: 0x0000661E
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000083 RID: 131
		public T this[int index]
		{
			get
			{
				return this._root[index];
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00008434 File Offset: 0x00006634
		[return: Nullable(new byte[] { 1, 0 })]
		public ImmutableSortedSet<T>.Builder ToBuilder()
		{
			return new ImmutableSortedSet<T>.Builder(this);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000843C File Offset: 0x0000663C
		public ImmutableSortedSet<T> Add(T value)
		{
			bool flag;
			return this.Wrap(this._root.Add(value, this._comparer, out flag));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008464 File Offset: 0x00006664
		public ImmutableSortedSet<T> Remove(T value)
		{
			bool flag;
			return this.Wrap(this._root.Remove(value, this._comparer, out flag));
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000848C File Offset: 0x0000668C
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
			if (node.IsEmpty)
			{
				actualValue = equalValue;
				return false;
			}
			actualValue = node.Key;
			return true;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000084CC File Offset: 0x000066CC
		public ImmutableSortedSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = this.Clear();
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(t))
				{
					immutableSortedSet = immutableSortedSet.Add(t);
				}
			}
			return immutableSortedSet;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008540 File Offset: 0x00006740
		public ImmutableSortedSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Remove(t, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000085B8 File Offset: 0x000067B8
		public ImmutableSortedSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = ImmutableSortedSet.CreateRange<T>(this._comparer, other);
			ImmutableSortedSet<T> immutableSortedSet2 = this.Clear();
			foreach (T t in this)
			{
				if (!immutableSortedSet.Contains(t))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(t);
				}
			}
			foreach (T t2 in immutableSortedSet)
			{
				if (!this.Contains(t2))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(t2);
				}
			}
			return immutableSortedSet2;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000867C File Offset: 0x0000687C
		public ImmutableSortedSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet;
			if (ImmutableSortedSet<T>.TryCastToImmutableSortedSet(other, out immutableSortedSet) && immutableSortedSet.KeyComparer == this.KeyComparer)
			{
				if (immutableSortedSet.IsEmpty)
				{
					return this;
				}
				if (this.IsEmpty)
				{
					return immutableSortedSet;
				}
				if (immutableSortedSet.Count > this.Count)
				{
					return immutableSortedSet.Union(this);
				}
			}
			int num;
			if (this.IsEmpty || (other.TryGetCount(out num) && (float)(this.Count + num) * 0.15f > (float)this.Count))
			{
				return this.LeafToRootRefill(other);
			}
			return this.UnionIncremental(other);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00008710 File Offset: 0x00006910
		public ImmutableSortedSet<T> WithComparer([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			if (comparer == this._comparer)
			{
				return this;
			}
			ImmutableSortedSet<T> immutableSortedSet = new ImmutableSortedSet<T>(ImmutableSortedSet<T>.Node.EmptyNode, comparer);
			return immutableSortedSet.Union(this);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008748 File Offset: 0x00006948
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this == other)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count != sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			foreach (T t in sortedSet)
			{
				if (!this.Contains(t))
				{
					return false;
				}
				num++;
			}
			return num == this.Count;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000087E0 File Offset: 0x000069E0
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return other.Any<T>();
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count >= sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (T t in sortedSet)
			{
				if (this.Contains(t))
				{
					num++;
				}
				else
				{
					flag = true;
				}
				if (num == this.Count && flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000888C File Offset: 0x00006A8C
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				num++;
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return this.Count > num;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00008910 File Offset: 0x00006B10
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			int num = 0;
			foreach (T t in sortedSet)
			{
				if (this.Contains(t))
				{
					num++;
				}
			}
			return num == this.Count;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008994 File Offset: 0x00006B94
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00008A00 File Offset: 0x00006C00
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008A74 File Offset: 0x00006C74
		public IEnumerable<T> Reverse()
		{
			return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00008A81 File Offset: 0x00006C81
		public int IndexOf(T item)
		{
			return this._root.IndexOf(item, this._comparer);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00008A95 File Offset: 0x00006C95
		public bool Contains(T value)
		{
			return this._root.Contains(value, this._comparer);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008AA9 File Offset: 0x00006CA9
		IImmutableSet<T> IImmutableSet<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00008AB1 File Offset: 0x00006CB1
		IImmutableSet<T> IImmutableSet<T>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008ABA File Offset: 0x00006CBA
		IImmutableSet<T> IImmutableSet<T>.Remove(T value)
		{
			return this.Remove(value);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008AC3 File Offset: 0x00006CC3
		IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00008ACC File Offset: 0x00006CCC
		IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008AD5 File Offset: 0x00006CD5
		IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008ADE File Offset: 0x00006CDE
		IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00008AE7 File Offset: 0x00006CE7
		bool ISet<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00008AEE File Offset: 0x00006CEE
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00008AF5 File Offset: 0x00006CF5
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008AFC File Offset: 0x00006CFC
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008B03 File Offset: 0x00006D03
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00008B0A File Offset: 0x00006D0A
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008B0D File Offset: 0x00006D0D
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00008B1C File Offset: 0x00006D1C
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008B23 File Offset: 0x00006D23
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00008B2A File Offset: 0x00006D2A
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000085 RID: 133
		T IList<T>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008B41 File Offset: 0x00006D41
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008B48 File Offset: 0x00006D48
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00008B4F File Offset: 0x00006D4F
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00008B52 File Offset: 0x00006D52
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00008B55 File Offset: 0x00006D55
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00008B58 File Offset: 0x00006D58
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008B5B File Offset: 0x00006D5B
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00008B62 File Offset: 0x00006D62
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008B69 File Offset: 0x00006D69
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008B77 File Offset: 0x00006D77
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008B85 File Offset: 0x00006D85
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008B8C File Offset: 0x00006D8C
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008B93 File Offset: 0x00006D93
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700008A RID: 138
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008BAF File Offset: 0x00006DAF
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008BC0 File Offset: 0x00006DC0
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00008BED File Offset: 0x00006DED
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008BFA File Offset: 0x00006DFA
		[NullableContext(0)]
		public ImmutableSortedSet<T>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008C08 File Offset: 0x00006E08
		private static bool TryCastToImmutableSortedSet(IEnumerable<T> sequence, [NotNullWhen(true)] out ImmutableSortedSet<T> other)
		{
			other = sequence as ImmutableSortedSet<T>;
			if (other != null)
			{
				return true;
			}
			ImmutableSortedSet<T>.Builder builder = sequence as ImmutableSortedSet<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008C38 File Offset: 0x00006E38
		private static ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, comparer);
			}
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008C58 File Offset: 0x00006E58
		private ImmutableSortedSet<T> UnionIncremental(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T t in items.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Add(t, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008CD0 File Offset: 0x00006ED0
		private ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, this._comparer);
			}
			return this.Clear();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008CF8 File Offset: 0x00006EF8
		private ImmutableSortedSet<T> LeafToRootRefill(IEnumerable<T> addedItems)
		{
			Requires.NotNull<IEnumerable<T>>(addedItems, "addedItems");
			List<T> list;
			if (this.IsEmpty)
			{
				int num;
				if (addedItems.TryGetCount(out num) && num == 0)
				{
					return this;
				}
				list = new List<T>(addedItems);
				if (list.Count == 0)
				{
					return this;
				}
			}
			else
			{
				list = new List<T>(this);
				list.AddRange(addedItems);
			}
			IComparer<T> keyComparer = this.KeyComparer;
			list.Sort(keyComparer);
			int num2 = 1;
			for (int i = 1; i < list.Count; i++)
			{
				if (keyComparer.Compare(list[i], list[i - 1]) != 0)
				{
					list[num2++] = list[i];
				}
			}
			list.RemoveRange(num2, list.Count - num2);
			ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.NodeTreeFromList(list.AsOrderedCollection<T>(), 0, list.Count);
			return this.Wrap(node);
		}

		// Token: 0x04000035 RID: 53
		private const float RefillOverIncrementalThreshold = 0.15f;

		// Token: 0x04000036 RID: 54
		public static readonly ImmutableSortedSet<T> Empty = new ImmutableSortedSet<T>(null);

		// Token: 0x04000037 RID: 55
		private readonly ImmutableSortedSet<T>.Node _root;

		// Token: 0x04000038 RID: 56
		private readonly IComparer<T> _comparer;

		// Token: 0x02000076 RID: 118
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedSetBuilderDebuggerProxy<>))]
		public sealed class Builder : ISortKeyCollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, ICollection<T>, ICollection
		{
			// Token: 0x060005CC RID: 1484 RVA: 0x0000F880 File Offset: 0x0000DA80
			internal Builder(ImmutableSortedSet<T> set)
			{
				Requires.NotNull<ImmutableSortedSet<T>>(set, "set");
				this._root = set._root;
				this._comparer = set.KeyComparer;
				this._immutable = set;
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000F8D3 File Offset: 0x0000DAD3
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700012B RID: 299
			public T this[int index]
			{
				get
				{
					return this._root[index];
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000F8F1 File Offset: 0x0000DAF1
			[Nullable(2)]
			public T Max
			{
				[NullableContext(2)]
				get
				{
					return this._root.Max;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000F8FE File Offset: 0x0000DAFE
			[Nullable(2)]
			public T Min
			{
				[NullableContext(2)]
				get
				{
					return this._root.Min;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000F90B File Offset: 0x0000DB0B
			// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0000F914 File Offset: 0x0000DB14
			public IComparer<T> KeyComparer
			{
				get
				{
					return this._comparer;
				}
				set
				{
					Requires.NotNull<IComparer<T>>(value, "value");
					if (value != this._comparer)
					{
						ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
						foreach (T t in this)
						{
							bool flag;
							node = node.Add(t, value, out flag);
						}
						this._immutable = null;
						this._comparer = value;
						this.Root = node;
					}
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000F998 File Offset: 0x0000DB98
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
			// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
			[Nullable(new byte[] { 1, 0 })]
			private ImmutableSortedSet<T>.Node Root
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

			// Token: 0x060005D7 RID: 1495 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
			public bool Add(T item)
			{
				bool flag;
				this.Root = this.Root.Add(item, this._comparer, out flag);
				return flag;
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
			public void ExceptWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T t in other)
				{
					bool flag;
					this.Root = this.Root.Remove(t, this._comparer, out flag);
				}
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x0000FA60 File Offset: 0x0000DC60
			public void IntersectWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
				foreach (T t in other)
				{
					if (this.Contains(t))
					{
						bool flag;
						node = node.Add(t, this._comparer, out flag);
					}
				}
				this.Root = node;
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSubsetOf(other);
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x0000FAE2 File Offset: 0x0000DCE2
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSupersetOf(other);
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSubsetOf(other);
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x0000FAFE File Offset: 0x0000DCFE
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSupersetOf(other);
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x0000FB0C File Offset: 0x0000DD0C
			public bool Overlaps(IEnumerable<T> other)
			{
				return this.ToImmutable().Overlaps(other);
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x0000FB1A File Offset: 0x0000DD1A
			public bool SetEquals(IEnumerable<T> other)
			{
				return this.ToImmutable().SetEquals(other);
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0000FB28 File Offset: 0x0000DD28
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				this.Root = this.ToImmutable().SymmetricExcept(other)._root;
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0000FB44 File Offset: 0x0000DD44
			public void UnionWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T t in other)
				{
					bool flag;
					this.Root = this.Root.Add(t, this._comparer, out flag);
				}
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x0000FBAC File Offset: 0x0000DDAC
			void ICollection<T>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x0000FBB6 File Offset: 0x0000DDB6
			public void Clear()
			{
				this.Root = ImmutableSortedSet<T>.Node.EmptyNode;
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x0000FBC3 File Offset: 0x0000DDC3
			public bool Contains(T item)
			{
				return this.Root.Contains(item, this._comparer);
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x0000FBD7 File Offset: 0x0000DDD7
			void ICollection<T>.CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
			public bool Remove(T item)
			{
				bool flag;
				this.Root = this.Root.Remove(item, this._comparer, out flag);
				return flag;
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x0000FC10 File Offset: 0x0000DE10
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x0000FC1E File Offset: 0x0000DE1E
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.Root.GetEnumerator();
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x0000FC30 File Offset: 0x0000DE30
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x0000FC3D File Offset: 0x0000DE3D
			public IEnumerable<T> Reverse()
			{
				return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x0000FC4A File Offset: 0x0000DE4A
			public ImmutableSortedSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedSet<T>.Wrap(this.Root, this._comparer);
				}
				return this._immutable;
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x0000FC74 File Offset: 0x0000DE74
			public bool TryGetValue(T equalValue, out T actualValue)
			{
				ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
				if (!node.IsEmpty)
				{
					actualValue = node.Key;
					return true;
				}
				actualValue = equalValue;
				return false;
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000FCC1 File Offset: 0x0000DEC1
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
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

			// Token: 0x040000E3 RID: 227
			private ImmutableSortedSet<T>.Node _root = ImmutableSortedSet<T>.Node.EmptyNode;

			// Token: 0x040000E4 RID: 228
			private IComparer<T> _comparer = Comparer<T>.Default;

			// Token: 0x040000E5 RID: 229
			private ImmutableSortedSet<T> _immutable;

			// Token: 0x040000E6 RID: 230
			private int _version;

			// Token: 0x040000E7 RID: 231
			private object _syncRoot;
		}

		// Token: 0x02000077 RID: 119
		private class ReverseEnumerable : IEnumerable<T>, IEnumerable
		{
			// Token: 0x060005F0 RID: 1520 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
			internal ReverseEnumerable(ImmutableSortedSet<T>.Node root)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x0000FD00 File Offset: 0x0000DF00
			public IEnumerator<T> GetEnumerator()
			{
				return this._root.Reverse();
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x0000FD0D File Offset: 0x0000DF0D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000E8 RID: 232
			private readonly ImmutableSortedSet<T>.Node _root;
		}

		// Token: 0x02000078 RID: 120
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x060005F3 RID: 1523 RVA: 0x0000FD18 File Offset: 0x0000DF18
			internal Enumerator([Nullable(new byte[] { 1, 0 })] ImmutableSortedSet<T>.Node root, [Nullable(new byte[] { 2, 0 })] ImmutableSortedSet<T>.Builder builder = null, bool reverse = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._reverse = reverse;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
				{
					this._stack = ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>(root.Height));
				}
				this.PushNext(this._root);
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000FDB5 File Offset: 0x0000DFB5
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000FDBD File Offset: 0x0000DFBD
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
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

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000FDDE File Offset: 0x0000DFDE
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x0000FDEC File Offset: 0x0000DFEC
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedSet<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
					ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
					this._stack = null;
				}
			}

			// Token: 0x060005F8 RID: 1528 RVA: 0x0000FE44 File Offset: 0x0000E044
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				if (stack.Count > 0)
				{
					ImmutableSortedSet<T>.Node value = stack.Pop().Value;
					this._current = value;
					this.PushNext(this._reverse ? value.Left : value.Right);
					return true;
				}
				this._current = null;
				return false;
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x0000FEAC File Offset: 0x0000E0AC
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
				this.PushNext(this._root);
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x0000FF01 File Offset: 0x0000E101
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedSet<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedSet<T>.Enumerator>(this);
				}
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x0000FF2C File Offset: 0x0000E12C
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x0000FF54 File Offset: 0x0000E154
			private void PushNext(ImmutableSortedSet<T>.Node node)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedSet<T>.Node>(node));
					node = (this._reverse ? node.Right : node.Left);
				}
			}

			// Token: 0x040000E9 RID: 233
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>();

			// Token: 0x040000EA RID: 234
			private readonly ImmutableSortedSet<T>.Builder _builder;

			// Token: 0x040000EB RID: 235
			private readonly int _poolUserId;

			// Token: 0x040000EC RID: 236
			private readonly bool _reverse;

			// Token: 0x040000ED RID: 237
			private ImmutableSortedSet<T>.Node _root;

			// Token: 0x040000EE RID: 238
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>> _stack;

			// Token: 0x040000EF RID: 239
			private ImmutableSortedSet<T>.Node _current;

			// Token: 0x040000F0 RID: 240
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000079 RID: 121
		[Nullable(0)]
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x0000FFB3 File Offset: 0x0000E1B3
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
			private Node(T key, ImmutableSortedSet<T>.Node left, ImmutableSortedSet<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedSet<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._count = 1 + left._count + right._count;
				this._frozen = frozen;
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000600 RID: 1536 RVA: 0x00010039 File Offset: 0x0000E239
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x00010044 File Offset: 0x0000E244
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001004C File Offset: 0x0000E24C
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableSortedSet<T>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000603 RID: 1539 RVA: 0x00010054 File Offset: 0x0000E254
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001005C File Offset: 0x0000E25C
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableSortedSet<T>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000605 RID: 1541 RVA: 0x00010064 File Offset: 0x0000E264
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001006C File Offset: 0x0000E26C
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000607 RID: 1543 RVA: 0x00010074 File Offset: 0x0000E274
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001007C File Offset: 0x0000E27C
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000609 RID: 1545 RVA: 0x00010084 File Offset: 0x0000E284
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001008C File Offset: 0x0000E28C
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x0600060B RID: 1547 RVA: 0x00010094 File Offset: 0x0000E294
			[Nullable(2)]
			internal T Max
			{
				[NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._right.IsEmpty)
					{
						node = node._right;
					}
					return node._key;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x0600060C RID: 1548 RVA: 0x000100D4 File Offset: 0x0000E2D4
			[Nullable(2)]
			internal T Min
			{
				[NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._left.IsEmpty)
					{
						node = node._left;
					}
					return node._key;
				}
			}

			// Token: 0x17000143 RID: 323
			internal T this[int index]
			{
				get
				{
					Requires.Range(index >= 0 && index < this.Count, "index", null);
					if (index < this._left._count)
					{
						return this._left[index];
					}
					if (index > this._left._count)
					{
						return this._right[index - this._left._count - 1];
					}
					return this._key;
				}
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x00010186 File Offset: 0x0000E386
			[NullableContext(0)]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, false);
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x00010190 File Offset: 0x0000E390
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0001019D File Offset: 0x0000E39D
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x000101AA File Offset: 0x0000E3AA
			[NullableContext(0)]
			internal ImmutableSortedSet<T>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0 })] ImmutableSortedSet<T>.Builder builder)
			{
				return new ImmutableSortedSet<T>.Enumerator(this, builder, false);
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x000101B4 File Offset: 0x0000E3B4
			internal void CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x00010240 File Offset: 0x0000E440
			internal void CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array.SetValue(t, new int[] { arrayIndex++ });
				}
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x000102E0 File Offset: 0x0000E4E0
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableSortedSet<T>.Node Add(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedSet<T>.Node(key, this, this, false);
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedSet<T>.Node node2 = this._right.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node2);
					}
				}
				else
				{
					if (num >= 0)
					{
						mutated = false;
						return this;
					}
					ImmutableSortedSet<T>.Node node3 = this._left.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(node3, null);
					}
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedSet<T>.Node.MakeBalanced(node);
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x00010374 File Offset: 0x0000E574
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableSortedSet<T>.Node Remove(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedSet<T>.Node.EmptyNode;
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
						ImmutableSortedSet<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedSet<T>.Node node3 = this._right.Remove(node2._key, comparer, out flag);
						node = node2.Mutate(this._left, node3);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedSet<T>.Node node4 = this._left.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(node4, null);
					}
				}
				else
				{
					ImmutableSortedSet<T>.Node node5 = this._right.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node5);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedSet<T>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x000104B9 File Offset: 0x0000E6B9
			internal bool Contains(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				return !this.Search(key, comparer).IsEmpty;
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x000104D6 File Offset: 0x0000E6D6
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00010500 File Offset: 0x0000E700
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableSortedSet<T>.Node Search(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return this;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, comparer);
				}
				return this._left.Search(key, comparer);
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x00010554 File Offset: 0x0000E754
			internal int IndexOf(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return -1;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this._left.Count;
				}
				if (num > 0)
				{
					int num2 = this._right.IndexOf(key, comparer);
					bool flag = num2 < 0;
					if (flag)
					{
						num2 = ~num2;
					}
					num2 = this._left.Count + 1 + num2;
					if (flag)
					{
						num2 = ~num2;
					}
					return num2;
				}
				return this._left.IndexOf(key, comparer);
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x000105D5 File Offset: 0x0000E7D5
			internal IEnumerator<T> Reverse()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, true);
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x000105E4 File Offset: 0x0000E7E4
			private static ImmutableSortedSet<T>.Node RotateLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x00010628 File Offset: 0x0000E828
			private static ImmutableSortedSet<T>.Node RotateRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x0001066C File Offset: 0x0000E86C
			private static ImmutableSortedSet<T>.Node DoubleLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node node = tree.Mutate(null, ImmutableSortedSet<T>.Node.RotateRight(tree._right));
				return ImmutableSortedSet<T>.Node.RotateLeft(node);
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x000106AC File Offset: 0x0000E8AC
			private static ImmutableSortedSet<T>.Node DoubleRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node node = tree.Mutate(ImmutableSortedSet<T>.Node.RotateLeft(tree._left), null);
				return ImmutableSortedSet<T>.Node.RotateRight(node);
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x000106EC File Offset: 0x0000E8EC
			private static int Balance(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x00010710 File Offset: 0x0000E910
			private static bool IsRightHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) >= 2;
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x00010729 File Offset: 0x0000E929
			private static bool IsLeftHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) <= -2;
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x00010744 File Offset: 0x0000E944
			private static ImmutableSortedSet<T>.Node MakeBalanced(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (ImmutableSortedSet<T>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedSet<T>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateLeft(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedSet<T>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedSet<T>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateRight(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x000107A8 File Offset: 0x0000E9A8
			[return: Nullable(new byte[] { 1, 0 })]
			internal static ImmutableSortedSet<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				if (length == 0)
				{
					return ImmutableSortedSet<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedSet<T>.Node node2 = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableSortedSet<T>.Node(items[start + num2], node, node2, true);
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x000107FC File Offset: 0x0000E9FC
			private ImmutableSortedSet<T>.Node Mutate(ImmutableSortedSet<T>.Node left = null, ImmutableSortedSet<T>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedSet<T>.Node(this._key, left ?? this._left, right ?? this._right, false);
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
				this._count = 1 + this._left._count + this._right._count;
				return this;
			}

			// Token: 0x040000F1 RID: 241
			[Nullable(new byte[] { 1, 0 })]
			internal static readonly ImmutableSortedSet<T>.Node EmptyNode = new ImmutableSortedSet<T>.Node();

			// Token: 0x040000F2 RID: 242
			private readonly T _key;

			// Token: 0x040000F3 RID: 243
			private bool _frozen;

			// Token: 0x040000F4 RID: 244
			private byte _height;

			// Token: 0x040000F5 RID: 245
			private int _count;

			// Token: 0x040000F6 RID: 246
			private ImmutableSortedSet<T>.Node _left;

			// Token: 0x040000F7 RID: 247
			private ImmutableSortedSet<T>.Node _right;
		}
	}
}
