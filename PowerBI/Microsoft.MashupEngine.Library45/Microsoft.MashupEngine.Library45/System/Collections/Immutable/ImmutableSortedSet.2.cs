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
	// Token: 0x020020B9 RID: 8377
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableSortedSet<[Nullable(2)] T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISortKeyCollection<T>, IReadOnlyList<T>, IList<T>, ICollection<T>, ISet<T>, IList, ICollection, IStrongEnumerable<T, ImmutableSortedSet<T>.Enumerator>
	{
		// Token: 0x060118E7 RID: 71911 RVA: 0x003C15DC File Offset: 0x003BF7DC
		internal ImmutableSortedSet([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer = null)
		{
			this._root = ImmutableSortedSet<T>.Node.EmptyNode;
			this._comparer = comparer ?? Comparer<T>.Default;
		}

		// Token: 0x060118E8 RID: 71912 RVA: 0x003C15FF File Offset: 0x003BF7FF
		private ImmutableSortedSet(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			root.Freeze();
			this._root = root;
			this._comparer = comparer;
		}

		// Token: 0x060118E9 RID: 71913 RVA: 0x003C1631 File Offset: 0x003BF831
		public ImmutableSortedSet<T> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedSet<T>.Empty.WithComparer(this._comparer);
			}
			return this;
		}

		// Token: 0x17002F0C RID: 12044
		// (get) Token: 0x060118EA RID: 71914 RVA: 0x003C1652 File Offset: 0x003BF852
		[Nullable(2)]
		public T Max
		{
			[NullableContext(2)]
			get
			{
				return this._root.Max;
			}
		}

		// Token: 0x17002F0D RID: 12045
		// (get) Token: 0x060118EB RID: 71915 RVA: 0x003C165F File Offset: 0x003BF85F
		[Nullable(2)]
		public T Min
		{
			[NullableContext(2)]
			get
			{
				return this._root.Min;
			}
		}

		// Token: 0x17002F0E RID: 12046
		// (get) Token: 0x060118EC RID: 71916 RVA: 0x003C166C File Offset: 0x003BF86C
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x17002F0F RID: 12047
		// (get) Token: 0x060118ED RID: 71917 RVA: 0x003C1679 File Offset: 0x003BF879
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x17002F10 RID: 12048
		// (get) Token: 0x060118EE RID: 71918 RVA: 0x003C1686 File Offset: 0x003BF886
		public IComparer<T> KeyComparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17002F11 RID: 12049
		// (get) Token: 0x060118EF RID: 71919 RVA: 0x003C168E File Offset: 0x003BF88E
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17002F12 RID: 12050
		public unsafe T this[int index]
		{
			get
			{
				return *this._root.ItemRef(index);
			}
		}

		// Token: 0x060118F1 RID: 71921 RVA: 0x003C16A9 File Offset: 0x003BF8A9
		public readonly ref T ItemRef(int index)
		{
			return this._root.ItemRef(index);
		}

		// Token: 0x060118F2 RID: 71922 RVA: 0x003C16B7 File Offset: 0x003BF8B7
		[return: Nullable(new byte[] { 1, 0 })]
		public ImmutableSortedSet<T>.Builder ToBuilder()
		{
			return new ImmutableSortedSet<T>.Builder(this);
		}

		// Token: 0x060118F3 RID: 71923 RVA: 0x003C16C0 File Offset: 0x003BF8C0
		public ImmutableSortedSet<T> Add(T value)
		{
			bool flag;
			return this.Wrap(this._root.Add(value, this._comparer, out flag));
		}

		// Token: 0x060118F4 RID: 71924 RVA: 0x003C16E8 File Offset: 0x003BF8E8
		public ImmutableSortedSet<T> Remove(T value)
		{
			bool flag;
			return this.Wrap(this._root.Remove(value, this._comparer, out flag));
		}

		// Token: 0x060118F5 RID: 71925 RVA: 0x003C1710 File Offset: 0x003BF910
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

		// Token: 0x060118F6 RID: 71926 RVA: 0x003C1750 File Offset: 0x003BF950
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

		// Token: 0x060118F7 RID: 71927 RVA: 0x003C17C4 File Offset: 0x003BF9C4
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

		// Token: 0x060118F8 RID: 71928 RVA: 0x003C183C File Offset: 0x003BFA3C
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

		// Token: 0x060118F9 RID: 71929 RVA: 0x003C1900 File Offset: 0x003BFB00
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

		// Token: 0x060118FA RID: 71930 RVA: 0x003C1994 File Offset: 0x003BFB94
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

		// Token: 0x060118FB RID: 71931 RVA: 0x003C19CC File Offset: 0x003BFBCC
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

		// Token: 0x060118FC RID: 71932 RVA: 0x003C1A64 File Offset: 0x003BFC64
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

		// Token: 0x060118FD RID: 71933 RVA: 0x003C1B10 File Offset: 0x003BFD10
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

		// Token: 0x060118FE RID: 71934 RVA: 0x003C1B94 File Offset: 0x003BFD94
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

		// Token: 0x060118FF RID: 71935 RVA: 0x003C1C18 File Offset: 0x003BFE18
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

		// Token: 0x06011900 RID: 71936 RVA: 0x003C1C84 File Offset: 0x003BFE84
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

		// Token: 0x06011901 RID: 71937 RVA: 0x003C1CF8 File Offset: 0x003BFEF8
		public IEnumerable<T> Reverse()
		{
			return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
		}

		// Token: 0x06011902 RID: 71938 RVA: 0x003C1D05 File Offset: 0x003BFF05
		public int IndexOf(T item)
		{
			return this._root.IndexOf(item, this._comparer);
		}

		// Token: 0x06011903 RID: 71939 RVA: 0x003C1D19 File Offset: 0x003BFF19
		public bool Contains(T value)
		{
			return this._root.Contains(value, this._comparer);
		}

		// Token: 0x06011904 RID: 71940 RVA: 0x003C1D2D File Offset: 0x003BFF2D
		IImmutableSet<T> IImmutableSet<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x06011905 RID: 71941 RVA: 0x003C1D35 File Offset: 0x003BFF35
		IImmutableSet<T> IImmutableSet<T>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x06011906 RID: 71942 RVA: 0x003C1D3E File Offset: 0x003BFF3E
		IImmutableSet<T> IImmutableSet<T>.Remove(T value)
		{
			return this.Remove(value);
		}

		// Token: 0x06011907 RID: 71943 RVA: 0x003C1D47 File Offset: 0x003BFF47
		IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x06011908 RID: 71944 RVA: 0x003C1D50 File Offset: 0x003BFF50
		IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x06011909 RID: 71945 RVA: 0x003C1D59 File Offset: 0x003BFF59
		IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x0601190A RID: 71946 RVA: 0x003C1D62 File Offset: 0x003BFF62
		IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x0601190B RID: 71947 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ISet<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601190C RID: 71948 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601190D RID: 71949 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601190E RID: 71950 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601190F RID: 71951 RVA: 0x00002C72 File Offset: 0x00000E72
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002F13 RID: 12051
		// (get) Token: 0x06011910 RID: 71952 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06011911 RID: 71953 RVA: 0x003C1D6B File Offset: 0x003BFF6B
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x06011912 RID: 71954 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011913 RID: 71955 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011914 RID: 71956 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002F14 RID: 12052
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

		// Token: 0x06011917 RID: 71959 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011918 RID: 71960 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002F15 RID: 12053
		// (get) Token: 0x06011919 RID: 71961 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002F16 RID: 12054
		// (get) Token: 0x0601191A RID: 71962 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002F17 RID: 12055
		// (get) Token: 0x0601191B RID: 71963 RVA: 0x00004FAE File Offset: 0x000031AE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002F18 RID: 12056
		// (get) Token: 0x0601191C RID: 71964 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0601191D RID: 71965 RVA: 0x00002C72 File Offset: 0x00000E72
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601191E RID: 71966 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601191F RID: 71967 RVA: 0x003C1D83 File Offset: 0x003BFF83
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x06011920 RID: 71968 RVA: 0x003C1D91 File Offset: 0x003BFF91
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06011921 RID: 71969 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011922 RID: 71970 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011923 RID: 71971 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002F19 RID: 12057
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

		// Token: 0x06011926 RID: 71974 RVA: 0x003C1DAD File Offset: 0x003BFFAD
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index);
		}

		// Token: 0x06011927 RID: 71975 RVA: 0x003C1DBC File Offset: 0x003BFFBC
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06011928 RID: 71976 RVA: 0x003C1DE9 File Offset: 0x003BFFE9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06011929 RID: 71977 RVA: 0x003C1DF6 File Offset: 0x003BFFF6
		[NullableContext(0)]
		public ImmutableSortedSet<T>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x0601192A RID: 71978 RVA: 0x003C1E04 File Offset: 0x003C0004
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

		// Token: 0x0601192B RID: 71979 RVA: 0x003C1E34 File Offset: 0x003C0034
		private static ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, comparer);
			}
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x0601192C RID: 71980 RVA: 0x003C1E54 File Offset: 0x003C0054
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

		// Token: 0x0601192D RID: 71981 RVA: 0x003C1ECC File Offset: 0x003C00CC
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

		// Token: 0x0601192E RID: 71982 RVA: 0x003C1EF4 File Offset: 0x003C00F4
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

		// Token: 0x04006975 RID: 26997
		private const float RefillOverIncrementalThreshold = 0.15f;

		// Token: 0x04006976 RID: 26998
		public static readonly ImmutableSortedSet<T> Empty = new ImmutableSortedSet<T>(null);

		// Token: 0x04006977 RID: 26999
		private readonly ImmutableSortedSet<T>.Node _root;

		// Token: 0x04006978 RID: 27000
		private readonly IComparer<T> _comparer;

		// Token: 0x020020BA RID: 8378
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedSetBuilderDebuggerProxy<>))]
		public sealed class Builder : ISortKeyCollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, ICollection<T>, ICollection
		{
			// Token: 0x06011930 RID: 71984 RVA: 0x003C1FCC File Offset: 0x003C01CC
			internal Builder(ImmutableSortedSet<T> set)
			{
				Requires.NotNull<ImmutableSortedSet<T>>(set, "set");
				this._root = set._root;
				this._comparer = set.KeyComparer;
				this._immutable = set;
			}

			// Token: 0x17002F1A RID: 12058
			// (get) Token: 0x06011931 RID: 71985 RVA: 0x003C201F File Offset: 0x003C021F
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x17002F1B RID: 12059
			// (get) Token: 0x06011932 RID: 71986 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002F1C RID: 12060
			public unsafe T this[int index]
			{
				get
				{
					return *this._root.ItemRef(index);
				}
			}

			// Token: 0x06011934 RID: 71988 RVA: 0x003C203F File Offset: 0x003C023F
			public readonly ref T ItemRef(int index)
			{
				return this._root.ItemRef(index);
			}

			// Token: 0x17002F1D RID: 12061
			// (get) Token: 0x06011935 RID: 71989 RVA: 0x003C204D File Offset: 0x003C024D
			[Nullable(2)]
			public T Max
			{
				[NullableContext(2)]
				get
				{
					return this._root.Max;
				}
			}

			// Token: 0x17002F1E RID: 12062
			// (get) Token: 0x06011936 RID: 71990 RVA: 0x003C205A File Offset: 0x003C025A
			[Nullable(2)]
			public T Min
			{
				[NullableContext(2)]
				get
				{
					return this._root.Min;
				}
			}

			// Token: 0x17002F1F RID: 12063
			// (get) Token: 0x06011937 RID: 71991 RVA: 0x003C2067 File Offset: 0x003C0267
			// (set) Token: 0x06011938 RID: 71992 RVA: 0x003C2070 File Offset: 0x003C0270
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

			// Token: 0x17002F20 RID: 12064
			// (get) Token: 0x06011939 RID: 71993 RVA: 0x003C20F4 File Offset: 0x003C02F4
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17002F21 RID: 12065
			// (get) Token: 0x0601193A RID: 71994 RVA: 0x003C20FC File Offset: 0x003C02FC
			// (set) Token: 0x0601193B RID: 71995 RVA: 0x003C2104 File Offset: 0x003C0304
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

			// Token: 0x0601193C RID: 71996 RVA: 0x003C212C File Offset: 0x003C032C
			public bool Add(T item)
			{
				bool flag;
				this.Root = this.Root.Add(item, this._comparer, out flag);
				return flag;
			}

			// Token: 0x0601193D RID: 71997 RVA: 0x003C2154 File Offset: 0x003C0354
			public void ExceptWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T t in other)
				{
					bool flag;
					this.Root = this.Root.Remove(t, this._comparer, out flag);
				}
			}

			// Token: 0x0601193E RID: 71998 RVA: 0x003C21BC File Offset: 0x003C03BC
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

			// Token: 0x0601193F RID: 71999 RVA: 0x003C2230 File Offset: 0x003C0430
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSubsetOf(other);
			}

			// Token: 0x06011940 RID: 72000 RVA: 0x003C223E File Offset: 0x003C043E
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSupersetOf(other);
			}

			// Token: 0x06011941 RID: 72001 RVA: 0x003C224C File Offset: 0x003C044C
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSubsetOf(other);
			}

			// Token: 0x06011942 RID: 72002 RVA: 0x003C225A File Offset: 0x003C045A
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSupersetOf(other);
			}

			// Token: 0x06011943 RID: 72003 RVA: 0x003C2268 File Offset: 0x003C0468
			public bool Overlaps(IEnumerable<T> other)
			{
				return this.ToImmutable().Overlaps(other);
			}

			// Token: 0x06011944 RID: 72004 RVA: 0x003C2276 File Offset: 0x003C0476
			public bool SetEquals(IEnumerable<T> other)
			{
				return this.ToImmutable().SetEquals(other);
			}

			// Token: 0x06011945 RID: 72005 RVA: 0x003C2284 File Offset: 0x003C0484
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				this.Root = this.ToImmutable().SymmetricExcept(other)._root;
			}

			// Token: 0x06011946 RID: 72006 RVA: 0x003C22A0 File Offset: 0x003C04A0
			public void UnionWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T t in other)
				{
					bool flag;
					this.Root = this.Root.Add(t, this._comparer, out flag);
				}
			}

			// Token: 0x06011947 RID: 72007 RVA: 0x003C2308 File Offset: 0x003C0508
			void ICollection<T>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x06011948 RID: 72008 RVA: 0x003C2312 File Offset: 0x003C0512
			public void Clear()
			{
				this.Root = ImmutableSortedSet<T>.Node.EmptyNode;
			}

			// Token: 0x06011949 RID: 72009 RVA: 0x003C231F File Offset: 0x003C051F
			public bool Contains(T item)
			{
				return this.Root.Contains(item, this._comparer);
			}

			// Token: 0x0601194A RID: 72010 RVA: 0x003C2333 File Offset: 0x003C0533
			void ICollection<T>.CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x0601194B RID: 72011 RVA: 0x003C2344 File Offset: 0x003C0544
			public bool Remove(T item)
			{
				bool flag;
				this.Root = this.Root.Remove(item, this._comparer, out flag);
				return flag;
			}

			// Token: 0x0601194C RID: 72012 RVA: 0x003C236C File Offset: 0x003C056C
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x0601194D RID: 72013 RVA: 0x003C237A File Offset: 0x003C057A
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.Root.GetEnumerator();
			}

			// Token: 0x0601194E RID: 72014 RVA: 0x003C238C File Offset: 0x003C058C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0601194F RID: 72015 RVA: 0x003C2399 File Offset: 0x003C0599
			public IEnumerable<T> Reverse()
			{
				return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
			}

			// Token: 0x06011950 RID: 72016 RVA: 0x003C23A6 File Offset: 0x003C05A6
			public ImmutableSortedSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedSet<T>.Wrap(this.Root, this._comparer);
				}
				return this._immutable;
			}

			// Token: 0x06011951 RID: 72017 RVA: 0x003C23D0 File Offset: 0x003C05D0
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

			// Token: 0x06011952 RID: 72018 RVA: 0x003C240E File Offset: 0x003C060E
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x17002F22 RID: 12066
			// (get) Token: 0x06011953 RID: 72019 RVA: 0x0000FA11 File Offset: 0x0000DC11
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002F23 RID: 12067
			// (get) Token: 0x06011954 RID: 72020 RVA: 0x003C241D File Offset: 0x003C061D
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

			// Token: 0x04006979 RID: 27001
			private ImmutableSortedSet<T>.Node _root = ImmutableSortedSet<T>.Node.EmptyNode;

			// Token: 0x0400697A RID: 27002
			private IComparer<T> _comparer = Comparer<T>.Default;

			// Token: 0x0400697B RID: 27003
			private ImmutableSortedSet<T> _immutable;

			// Token: 0x0400697C RID: 27004
			private int _version;

			// Token: 0x0400697D RID: 27005
			private object _syncRoot;
		}

		// Token: 0x020020BB RID: 8379
		private class ReverseEnumerable : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06011955 RID: 72021 RVA: 0x003C243F File Offset: 0x003C063F
			internal ReverseEnumerable(ImmutableSortedSet<T>.Node root)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
			}

			// Token: 0x06011956 RID: 72022 RVA: 0x003C2459 File Offset: 0x003C0659
			public IEnumerator<T> GetEnumerator()
			{
				return this._root.Reverse();
			}

			// Token: 0x06011957 RID: 72023 RVA: 0x003C2466 File Offset: 0x003C0666
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400697E RID: 27006
			private readonly ImmutableSortedSet<T>.Node _root;
		}

		// Token: 0x020020BC RID: 8380
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x06011958 RID: 72024 RVA: 0x003C2470 File Offset: 0x003C0670
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

			// Token: 0x17002F24 RID: 12068
			// (get) Token: 0x06011959 RID: 72025 RVA: 0x003C250D File Offset: 0x003C070D
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17002F25 RID: 12069
			// (get) Token: 0x0601195A RID: 72026 RVA: 0x003C2515 File Offset: 0x003C0715
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

			// Token: 0x17002F26 RID: 12070
			// (get) Token: 0x0601195B RID: 72027 RVA: 0x003C2536 File Offset: 0x003C0736
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0601195C RID: 72028 RVA: 0x003C2544 File Offset: 0x003C0744
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

			// Token: 0x0601195D RID: 72029 RVA: 0x003C259C File Offset: 0x003C079C
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

			// Token: 0x0601195E RID: 72030 RVA: 0x003C2604 File Offset: 0x003C0804
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
				this.PushNext(this._root);
			}

			// Token: 0x0601195F RID: 72031 RVA: 0x003C2659 File Offset: 0x003C0859
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedSet<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedSet<T>.Enumerator>(this);
				}
			}

			// Token: 0x06011960 RID: 72032 RVA: 0x003C2684 File Offset: 0x003C0884
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x06011961 RID: 72033 RVA: 0x003C26AC File Offset: 0x003C08AC
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

			// Token: 0x0400697F RID: 27007
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>();

			// Token: 0x04006980 RID: 27008
			private readonly ImmutableSortedSet<T>.Builder _builder;

			// Token: 0x04006981 RID: 27009
			private readonly int _poolUserId;

			// Token: 0x04006982 RID: 27010
			private readonly bool _reverse;

			// Token: 0x04006983 RID: 27011
			private ImmutableSortedSet<T>.Node _root;

			// Token: 0x04006984 RID: 27012
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>> _stack;

			// Token: 0x04006985 RID: 27013
			private ImmutableSortedSet<T>.Node _current;

			// Token: 0x04006986 RID: 27014
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020020BD RID: 8381
		[Nullable(0)]
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<T>, IEnumerable
		{
			// Token: 0x06011963 RID: 72035 RVA: 0x003C270B File Offset: 0x003C090B
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x06011964 RID: 72036 RVA: 0x003C271C File Offset: 0x003C091C
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

			// Token: 0x17002F27 RID: 12071
			// (get) Token: 0x06011965 RID: 72037 RVA: 0x003C2791 File Offset: 0x003C0991
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17002F28 RID: 12072
			// (get) Token: 0x06011966 RID: 72038 RVA: 0x003C279C File Offset: 0x003C099C
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17002F29 RID: 12073
			// (get) Token: 0x06011967 RID: 72039 RVA: 0x003C27A4 File Offset: 0x003C09A4
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableSortedSet<T>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002F2A RID: 12074
			// (get) Token: 0x06011968 RID: 72040 RVA: 0x003C27A4 File Offset: 0x003C09A4
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002F2B RID: 12075
			// (get) Token: 0x06011969 RID: 72041 RVA: 0x003C27AC File Offset: 0x003C09AC
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableSortedSet<T>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002F2C RID: 12076
			// (get) Token: 0x0601196A RID: 72042 RVA: 0x003C27AC File Offset: 0x003C09AC
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002F2D RID: 12077
			// (get) Token: 0x0601196B RID: 72043 RVA: 0x003C27A4 File Offset: 0x003C09A4
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002F2E RID: 12078
			// (get) Token: 0x0601196C RID: 72044 RVA: 0x003C27AC File Offset: 0x003C09AC
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002F2F RID: 12079
			// (get) Token: 0x0601196D RID: 72045 RVA: 0x003C27B4 File Offset: 0x003C09B4
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17002F30 RID: 12080
			// (get) Token: 0x0601196E RID: 72046 RVA: 0x003C27BC File Offset: 0x003C09BC
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17002F31 RID: 12081
			// (get) Token: 0x0601196F RID: 72047 RVA: 0x003C27B4 File Offset: 0x003C09B4
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17002F32 RID: 12082
			// (get) Token: 0x06011970 RID: 72048 RVA: 0x003C27C4 File Offset: 0x003C09C4
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

			// Token: 0x17002F33 RID: 12083
			// (get) Token: 0x06011971 RID: 72049 RVA: 0x003C2804 File Offset: 0x003C0A04
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

			// Token: 0x17002F34 RID: 12084
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

			// Token: 0x06011973 RID: 72051 RVA: 0x003C28B8 File Offset: 0x003C0AB8
			internal readonly ref T ItemRef(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this._left._count)
				{
					return this._left.ItemRef(index);
				}
				if (index > this._left._count)
				{
					return this._right.ItemRef(index - this._left._count - 1);
				}
				return ref this._key;
			}

			// Token: 0x06011974 RID: 72052 RVA: 0x003C292A File Offset: 0x003C0B2A
			[NullableContext(0)]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, false);
			}

			// Token: 0x06011975 RID: 72053 RVA: 0x003C2934 File Offset: 0x003C0B34
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011976 RID: 72054 RVA: 0x003C2934 File Offset: 0x003C0B34
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011977 RID: 72055 RVA: 0x003C2941 File Offset: 0x003C0B41
			[NullableContext(0)]
			internal ImmutableSortedSet<T>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0 })] ImmutableSortedSet<T>.Builder builder)
			{
				return new ImmutableSortedSet<T>.Enumerator(this, builder, false);
			}

			// Token: 0x06011978 RID: 72056 RVA: 0x003C294C File Offset: 0x003C0B4C
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

			// Token: 0x06011979 RID: 72057 RVA: 0x003C29D8 File Offset: 0x003C0BD8
			internal void CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array.SetValue(t, arrayIndex++);
				}
			}

			// Token: 0x0601197A RID: 72058 RVA: 0x003C2A6C File Offset: 0x003C0C6C
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

			// Token: 0x0601197B RID: 72059 RVA: 0x003C2B00 File Offset: 0x003C0D00
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

			// Token: 0x0601197C RID: 72060 RVA: 0x003C2C45 File Offset: 0x003C0E45
			internal bool Contains(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				return !this.Search(key, comparer).IsEmpty;
			}

			// Token: 0x0601197D RID: 72061 RVA: 0x003C2C62 File Offset: 0x003C0E62
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x0601197E RID: 72062 RVA: 0x003C2C8C File Offset: 0x003C0E8C
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

			// Token: 0x0601197F RID: 72063 RVA: 0x003C2CE0 File Offset: 0x003C0EE0
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

			// Token: 0x06011980 RID: 72064 RVA: 0x003C2D61 File Offset: 0x003C0F61
			internal IEnumerator<T> Reverse()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, true);
			}

			// Token: 0x06011981 RID: 72065 RVA: 0x003C2D70 File Offset: 0x003C0F70
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

			// Token: 0x06011982 RID: 72066 RVA: 0x003C2DB4 File Offset: 0x003C0FB4
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

			// Token: 0x06011983 RID: 72067 RVA: 0x003C2DF8 File Offset: 0x003C0FF8
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

			// Token: 0x06011984 RID: 72068 RVA: 0x003C2E38 File Offset: 0x003C1038
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

			// Token: 0x06011985 RID: 72069 RVA: 0x003C2E78 File Offset: 0x003C1078
			private static int Balance(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x06011986 RID: 72070 RVA: 0x003C2E9C File Offset: 0x003C109C
			private static bool IsRightHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) >= 2;
			}

			// Token: 0x06011987 RID: 72071 RVA: 0x003C2EB5 File Offset: 0x003C10B5
			private static bool IsLeftHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) <= -2;
			}

			// Token: 0x06011988 RID: 72072 RVA: 0x003C2ED0 File Offset: 0x003C10D0
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

			// Token: 0x06011989 RID: 72073 RVA: 0x003C2F34 File Offset: 0x003C1134
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

			// Token: 0x0601198A RID: 72074 RVA: 0x003C2F88 File Offset: 0x003C1188
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

			// Token: 0x04006987 RID: 27015
			[Nullable(new byte[] { 1, 0 })]
			internal static readonly ImmutableSortedSet<T>.Node EmptyNode = new ImmutableSortedSet<T>.Node();

			// Token: 0x04006988 RID: 27016
			private readonly T _key;

			// Token: 0x04006989 RID: 27017
			private bool _frozen;

			// Token: 0x0400698A RID: 27018
			private byte _height;

			// Token: 0x0400698B RID: 27019
			private int _count;

			// Token: 0x0400698C RID: 27020
			private ImmutableSortedSet<T>.Node _left;

			// Token: 0x0400698D RID: 27021
			private ImmutableSortedSet<T>.Node _right;
		}
	}
}
