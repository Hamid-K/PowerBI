using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System.Collections.Immutable
{
	// Token: 0x02002084 RID: 8324
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[NonVersionable]
	public struct ImmutableArray<[Nullable(2)] T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IList<T>, ICollection<T>, IEquatable<ImmutableArray<T>>, IList, ICollection, IImmutableArray, IStructuralComparable, IStructuralEquatable, IImmutableList<T>
	{
		// Token: 0x17002E42 RID: 11842
		T IList<T>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002E43 RID: 11843
		// (get) Token: 0x06011505 RID: 70917 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E44 RID: 11844
		// (get) Token: 0x06011506 RID: 70918 RVA: 0x003B7EA8 File Offset: 0x003B60A8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection<T>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17002E45 RID: 11845
		// (get) Token: 0x06011507 RID: 70919 RVA: 0x003B7ECC File Offset: 0x003B60CC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int IReadOnlyCollection<T>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17002E46 RID: 11846
		T IReadOnlyList<T>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
		}

		// Token: 0x06011509 RID: 70921 RVA: 0x003B7F13 File Offset: 0x003B6113
		[return: Nullable(new byte[] { 0, 1 })]
		public ReadOnlySpan<T> AsSpan()
		{
			return new ReadOnlySpan<T>(this.array);
		}

		// Token: 0x0601150A RID: 70922 RVA: 0x003B7F20 File Offset: 0x003B6120
		[return: Nullable(new byte[] { 0, 1 })]
		public ReadOnlyMemory<T> AsMemory()
		{
			return new ReadOnlyMemory<T>(this.array);
		}

		// Token: 0x0601150B RID: 70923 RVA: 0x003B7F30 File Offset: 0x003B6130
		public int IndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, 0, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x0601150C RID: 70924 RVA: 0x003B7F5C File Offset: 0x003B615C
		public int IndexOf(T item, int startIndex, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, equalityComparer);
		}

		// Token: 0x0601150D RID: 70925 RVA: 0x003B7F84 File Offset: 0x003B6184
		public int IndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x0601150E RID: 70926 RVA: 0x003B7FAF File Offset: 0x003B61AF
		public int IndexOf(T item, int startIndex, int count)
		{
			return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x0601150F RID: 70927 RVA: 0x003B7FC0 File Offset: 0x003B61C0
		public int IndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (count == 0 && startIndex == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex + count <= immutableArray.Length, "count", null);
			equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.IndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (equalityComparer.Equals(immutableArray.array[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06011510 RID: 70928 RVA: 0x003B806C File Offset: 0x003B626C
		public int LastIndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, immutableArray.Length - 1, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x06011511 RID: 70929 RVA: 0x003B80A8 File Offset: 0x003B62A8
		public int LastIndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0 && startIndex == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x06011512 RID: 70930 RVA: 0x003B80DB File Offset: 0x003B62DB
		public int LastIndexOf(T item, int startIndex, int count)
		{
			return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x06011513 RID: 70931 RVA: 0x003B80EC File Offset: 0x003B62EC
		public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (startIndex == 0 && count == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
			equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.LastIndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i >= startIndex - count + 1; i--)
			{
				if (equalityComparer.Equals(item, immutableArray.array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06011514 RID: 70932 RVA: 0x003B8196 File Offset: 0x003B6396
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06011515 RID: 70933 RVA: 0x003B81A8 File Offset: 0x003B63A8
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Insert(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			T[] array = new T[immutableArray.Length + 1];
			array[index] = item;
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + 1, immutableArray.Length - index);
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06011516 RID: 70934 RVA: 0x003B8244 File Offset: 0x003B6444
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.CreateRange<T>(items);
			}
			int count = ImmutableExtensions.GetCount<T>(ref items);
			if (count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + count];
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + count, immutableArray.Length - index);
			}
			if (!items.TryCopyTo(array, index))
			{
				int num = index;
				foreach (T t in items)
				{
					array[num++] = t;
				}
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06011517 RID: 70935 RVA: 0x003B8344 File Offset: 0x003B6544
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> InsertRange(int index, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.IsEmpty)
			{
				return items;
			}
			if (items.IsEmpty)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + items.Length];
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + items.Length, immutableArray.Length - index);
			}
			Array.Copy(items.array, 0, array, index, items.Length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06011518 RID: 70936 RVA: 0x003B8404 File Offset: 0x003B6604
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Add(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			return immutableArray.Insert(immutableArray.Length, item);
		}

		// Token: 0x06011519 RID: 70937 RVA: 0x003B8438 File Offset: 0x003B6638
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x0601151A RID: 70938 RVA: 0x003B845C File Offset: 0x003B665C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x0601151B RID: 70939 RVA: 0x003B8480 File Offset: 0x003B6680
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> SetItem(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index < immutableArray.Length, "index", null);
			T[] array = new T[immutableArray.Length];
			Array.Copy(immutableArray.array, array, immutableArray.Length);
			array[index] = item;
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0601151C RID: 70940 RVA: 0x003B84E5 File Offset: 0x003B66E5
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x0601151D RID: 70941 RVA: 0x003B84F4 File Offset: 0x003B66F4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Replace(T oldValue, T newValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			int num = immutableArray.IndexOf(oldValue, 0, immutableArray.Length, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return immutableArray.SetItem(num, newValue);
		}

		// Token: 0x0601151E RID: 70942 RVA: 0x003B8537 File Offset: 0x003B6737
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Remove(T item)
		{
			return this.Remove(item, EqualityComparer<T>.Default);
		}

		// Token: 0x0601151F RID: 70943 RVA: 0x003B8548 File Offset: 0x003B6748
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Remove(T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			int num = immutableArray.IndexOf(item, 0, immutableArray.Length, equalityComparer);
			if (num >= 0)
			{
				return immutableArray.RemoveAt(num);
			}
			return immutableArray;
		}

		// Token: 0x06011520 RID: 70944 RVA: 0x003B8583 File Offset: 0x003B6783
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveAt(int index)
		{
			return this.RemoveRange(index, 1);
		}

		// Token: 0x06011521 RID: 70945 RVA: 0x003B8590 File Offset: 0x003B6790
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange(int index, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.Range(length >= 0 && index + length <= immutableArray.Length, "length", null);
			if (length == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - length];
			Array.Copy(immutableArray.array, array, index);
			Array.Copy(immutableArray.array, index + length, array, index, immutableArray.Length - index - length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06011522 RID: 70946 RVA: 0x003B862D File Offset: 0x003B682D
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06011523 RID: 70947 RVA: 0x003B863C File Offset: 0x003B683C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<IEnumerable<T>>(items, "items");
			SortedSet<int> sortedSet = new SortedSet<int>();
			foreach (T t in items)
			{
				int num = immutableArray.IndexOf(t, 0, immutableArray.Length, equalityComparer);
				while (num >= 0 && !sortedSet.Add(num) && num + 1 < immutableArray.Length)
				{
					num = immutableArray.IndexOf(t, num + 1, equalityComparer);
				}
			}
			return immutableArray.RemoveAtRange(sortedSet);
		}

		// Token: 0x06011524 RID: 70948 RVA: 0x003B86E4 File Offset: 0x003B68E4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06011525 RID: 70949 RVA: 0x003B86F4 File Offset: 0x003B68F4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			Requires.NotNull<T[]>(items.array, "items");
			if (items.IsEmpty)
			{
				immutableArray.ThrowNullRefIfNotInitialized();
				return immutableArray;
			}
			if (items.Length == 1)
			{
				return immutableArray.Remove(items[0], equalityComparer);
			}
			return immutableArray.RemoveRange(items.array, equalityComparer);
		}

		// Token: 0x06011526 RID: 70950 RVA: 0x003B8754 File Offset: 0x003B6954
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Predicate<T>>(match, "match");
			if (immutableArray.IsEmpty)
			{
				return immutableArray;
			}
			List<int> list = null;
			for (int i = 0; i < immutableArray.array.Length; i++)
			{
				if (match(immutableArray.array[i]))
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(i);
				}
			}
			if (list == null)
			{
				return immutableArray;
			}
			return immutableArray.RemoveAtRange(list);
		}

		// Token: 0x06011527 RID: 70951 RVA: 0x003B87CB File Offset: 0x003B69CB
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Clear()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x06011528 RID: 70952 RVA: 0x003B87D4 File Offset: 0x003B69D4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort()
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, Comparer<T>.Default);
		}

		// Token: 0x06011529 RID: 70953 RVA: 0x003B87FC File Offset: 0x003B69FC
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(Comparer<T>.Create(comparison));
		}

		// Token: 0x0601152A RID: 70954 RVA: 0x003B8828 File Offset: 0x003B6A28
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, comparer);
		}

		// Token: 0x0601152B RID: 70955 RVA: 0x003B884C File Offset: 0x003B6A4C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0 && index + count <= immutableArray.Length, "count", null);
			if (count > 1)
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				bool flag = false;
				for (int i = index + 1; i < index + count; i++)
				{
					if (comparer.Compare(immutableArray.array[i - 1], immutableArray.array[i]) > 0)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					T[] array = new T[immutableArray.Length];
					Array.Copy(immutableArray.array, array, immutableArray.Length);
					Array.Sort<T>(array, index, count, comparer);
					return new ImmutableArray<T>(array);
				}
			}
			return immutableArray;
		}

		// Token: 0x0601152C RID: 70956 RVA: 0x003B8918 File Offset: 0x003B6B18
		public IEnumerable<TResult> OfType<[Nullable(2)] TResult>()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array == null || immutableArray.array.Length == 0)
			{
				return Enumerable.Empty<TResult>();
			}
			return immutableArray.array.OfType<TResult>();
		}

		// Token: 0x0601152D RID: 70957 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601152E RID: 70958 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601152F RID: 70959 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011530 RID: 70960 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011531 RID: 70961 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011532 RID: 70962 RVA: 0x003B8950 File Offset: 0x003B6B50
		IImmutableList<T> IImmutableList<T>.Clear()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Clear();
		}

		// Token: 0x06011533 RID: 70963 RVA: 0x003B8978 File Offset: 0x003B6B78
		IImmutableList<T> IImmutableList<T>.Add(T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Add(value);
		}

		// Token: 0x06011534 RID: 70964 RVA: 0x003B89A0 File Offset: 0x003B6BA0
		IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.AddRange(items);
		}

		// Token: 0x06011535 RID: 70965 RVA: 0x003B89C8 File Offset: 0x003B6BC8
		IImmutableList<T> IImmutableList<T>.Insert(int index, T element)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Insert(index, element);
		}

		// Token: 0x06011536 RID: 70966 RVA: 0x003B89F4 File Offset: 0x003B6BF4
		IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.InsertRange(index, items);
		}

		// Token: 0x06011537 RID: 70967 RVA: 0x003B8A20 File Offset: 0x003B6C20
		IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Remove(value, equalityComparer);
		}

		// Token: 0x06011538 RID: 70968 RVA: 0x003B8A4C File Offset: 0x003B6C4C
		IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAll(match);
		}

		// Token: 0x06011539 RID: 70969 RVA: 0x003B8A74 File Offset: 0x003B6C74
		IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(items, equalityComparer);
		}

		// Token: 0x0601153A RID: 70970 RVA: 0x003B8AA0 File Offset: 0x003B6CA0
		IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(index, count);
		}

		// Token: 0x0601153B RID: 70971 RVA: 0x003B8ACC File Offset: 0x003B6CCC
		IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAt(index);
		}

		// Token: 0x0601153C RID: 70972 RVA: 0x003B8AF4 File Offset: 0x003B6CF4
		IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.SetItem(index, value);
		}

		// Token: 0x0601153D RID: 70973 RVA: 0x003B8B20 File Offset: 0x003B6D20
		IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x0601153E RID: 70974 RVA: 0x00002C72 File Offset: 0x00000E72
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601153F RID: 70975 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011540 RID: 70976 RVA: 0x003B8B4C File Offset: 0x003B6D4C
		bool IList.Contains(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Contains((T)((object)value));
		}

		// Token: 0x06011541 RID: 70977 RVA: 0x003B8B74 File Offset: 0x003B6D74
		int IList.IndexOf(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.IndexOf((T)((object)value));
		}

		// Token: 0x06011542 RID: 70978 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002E47 RID: 11847
		// (get) Token: 0x06011543 RID: 70979 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E48 RID: 11848
		// (get) Token: 0x06011544 RID: 70980 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E49 RID: 11849
		// (get) Token: 0x06011545 RID: 70981 RVA: 0x003B8B9C File Offset: 0x003B6D9C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17002E4A RID: 11850
		// (get) Token: 0x06011546 RID: 70982 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E4B RID: 11851
		// (get) Token: 0x06011547 RID: 70983 RVA: 0x00002C72 File Offset: 0x00000E72
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06011548 RID: 70984 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011549 RID: 70985 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002E4C RID: 11852
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0601154C RID: 70988 RVA: 0x003B8BE8 File Offset: 0x003B6DE8
		void ICollection.CopyTo(Array array, int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			Array.Copy(immutableArray.array, 0, array, index, immutableArray.Length);
		}

		// Token: 0x0601154D RID: 70989 RVA: 0x003B8C18 File Offset: 0x003B6E18
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return true;
					}
					if (immutableArray.array == null)
					{
						return false;
					}
				}
			}
			IStructuralEquatable structuralEquatable = immutableArray.array;
			return structuralEquatable.Equals(array, comparer);
		}

		// Token: 0x0601154E RID: 70990 RVA: 0x003B8C70 File Offset: 0x003B6E70
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			IStructuralEquatable structuralEquatable = immutableArray.array;
			if (structuralEquatable == null)
			{
				return immutableArray.GetHashCode();
			}
			return structuralEquatable.GetHashCode(comparer);
		}

		// Token: 0x0601154F RID: 70991 RVA: 0x003B8CA4 File Offset: 0x003B6EA4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return 0;
					}
					if ((immutableArray.array == null) ^ (array == null))
					{
						throw new ArgumentException(SR.ArrayInitializedStateNotEqual, "other");
					}
				}
			}
			if (array == null)
			{
				throw new ArgumentException(SR.ArrayLengthsNotEqual, "other");
			}
			IStructuralComparable structuralComparable = immutableArray.array;
			if (structuralComparable == null)
			{
				throw new ArgumentException(SR.ArrayInitializedStateNotEqual, "other");
			}
			return structuralComparable.CompareTo(array, comparer);
		}

		// Token: 0x06011550 RID: 70992 RVA: 0x003B8D38 File Offset: 0x003B6F38
		private ImmutableArray<T> RemoveAtRange(ICollection<int> indicesToRemove)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<ICollection<int>>(indicesToRemove, "indicesToRemove");
			if (indicesToRemove.Count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - indicesToRemove.Count];
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			foreach (int num4 in indicesToRemove)
			{
				int num5 = ((num3 == -1) ? num4 : (num4 - num3 - 1));
				Array.Copy(immutableArray.array, num + num2, array, num, num5);
				num2++;
				num += num5;
				num3 = num4;
			}
			Array.Copy(immutableArray.array, num + num2, array, num, immutableArray.Length - (num + num2));
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06011551 RID: 70993 RVA: 0x003B8E10 File Offset: 0x003B7010
		internal ImmutableArray([Nullable(new byte[] { 2, 1 })] T[] items)
		{
			this.array = items;
		}

		// Token: 0x06011552 RID: 70994 RVA: 0x003B8E19 File Offset: 0x003B7019
		[NonVersionable]
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06011553 RID: 70995 RVA: 0x003B8E23 File Offset: 0x003B7023
		[NonVersionable]
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06011554 RID: 70996 RVA: 0x003B8E30 File Offset: 0x003B7030
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? right)
		{
			return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x06011555 RID: 70997 RVA: 0x003B8E54 File Offset: 0x003B7054
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? right)
		{
			return !left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x17002E4D RID: 11853
		public T this[int index]
		{
			[NonVersionable]
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x06011557 RID: 70999 RVA: 0x003B8E88 File Offset: 0x003B7088
		public readonly ref T ItemRef(int index)
		{
			return ref this.array[index];
		}

		// Token: 0x17002E4E RID: 11854
		// (get) Token: 0x06011558 RID: 71000 RVA: 0x003B8E98 File Offset: 0x003B7098
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this.array.Length == 0;
			}
		}

		// Token: 0x17002E4F RID: 11855
		// (get) Token: 0x06011559 RID: 71001 RVA: 0x003B8EA4 File Offset: 0x003B70A4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Length
		{
			[NonVersionable]
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x17002E50 RID: 11856
		// (get) Token: 0x0601155A RID: 71002 RVA: 0x003B8EAE File Offset: 0x003B70AE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefault
		{
			get
			{
				return this.array == null;
			}
		}

		// Token: 0x17002E51 RID: 11857
		// (get) Token: 0x0601155B RID: 71003 RVA: 0x003B8EBC File Offset: 0x003B70BC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefaultOrEmpty
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				return immutableArray.array == null || immutableArray.array.Length == 0;
			}
		}

		// Token: 0x17002E52 RID: 11858
		// (get) Token: 0x0601155C RID: 71004 RVA: 0x003B8EE4 File Offset: 0x003B70E4
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Array IImmutableArray.Array
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x17002E53 RID: 11859
		// (get) Token: 0x0601155D RID: 71005 RVA: 0x003B8EEC File Offset: 0x003B70EC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				if (!immutableArray.IsDefault)
				{
					return string.Format(CultureInfo.CurrentCulture, "Length = {0}", immutableArray.Length);
				}
				return "Uninitialized";
			}
		}

		// Token: 0x0601155E RID: 71006 RVA: 0x003B8F2C File Offset: 0x003B712C
		public void CopyTo(T[] destination)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, destination, immutableArray.Length);
		}

		// Token: 0x0601155F RID: 71007 RVA: 0x003B8F5C File Offset: 0x003B715C
		public void CopyTo(T[] destination, int destinationIndex)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, destinationIndex, immutableArray.Length);
		}

		// Token: 0x06011560 RID: 71008 RVA: 0x003B8F8C File Offset: 0x003B718C
		public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, sourceIndex, destination, destinationIndex, length);
		}

		// Token: 0x06011561 RID: 71009 RVA: 0x003B8FB8 File Offset: 0x003B71B8
		public ImmutableArray<T>.Builder ToBuilder()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return new ImmutableArray<T>.Builder();
			}
			ImmutableArray<T>.Builder builder = new ImmutableArray<T>.Builder(immutableArray.Length);
			builder.AddRange(immutableArray);
			return builder;
		}

		// Token: 0x06011562 RID: 71010 RVA: 0x003B8FF0 File Offset: 0x003B71F0
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ImmutableArray<T>.Enumerator GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			return new ImmutableArray<T>.Enumerator(immutableArray.array);
		}

		// Token: 0x06011563 RID: 71011 RVA: 0x003B9018 File Offset: 0x003B7218
		public override int GetHashCode()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array != null)
			{
				return immutableArray.array.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06011564 RID: 71012 RVA: 0x003B9044 File Offset: 0x003B7244
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			IImmutableArray immutableArray = obj as IImmutableArray;
			return immutableArray != null && this.array == immutableArray.Array;
		}

		// Token: 0x06011565 RID: 71013 RVA: 0x003B906B File Offset: 0x003B726B
		[NonVersionable]
		public bool Equals([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> other)
		{
			return this.array == other.array;
		}

		// Token: 0x06011566 RID: 71014 RVA: 0x003B907C File Offset: 0x003B727C
		[NullableContext(0)]
		public static ImmutableArray<T> CastUp<[Nullable(2)] TDerived>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items) where TDerived : class, T
		{
			T[] array = items.array;
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06011567 RID: 71015 RVA: 0x003B9096 File Offset: 0x003B7296
		[NullableContext(0)]
		public ImmutableArray<TOther> CastArray<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>((TOther[])this.array);
		}

		// Token: 0x06011568 RID: 71016 RVA: 0x003B90A8 File Offset: 0x003B72A8
		[NullableContext(0)]
		public ImmutableArray<TOther> As<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>(this.array as TOther[]);
		}

		// Token: 0x06011569 RID: 71017 RVA: 0x003B90BC File Offset: 0x003B72BC
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x0601156A RID: 71018 RVA: 0x003B90E4 File Offset: 0x003B72E4
		IEnumerator IEnumerable.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x0601156B RID: 71019 RVA: 0x003B910A File Offset: 0x003B730A
		internal void ThrowNullRefIfNotInitialized()
		{
			int num = this.array.Length;
		}

		// Token: 0x0601156C RID: 71020 RVA: 0x003B9115 File Offset: 0x003B7315
		private void ThrowInvalidOperationIfNotInitialized()
		{
			if (this.IsDefault)
			{
				throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
			}
		}

		// Token: 0x040068D4 RID: 26836
		[Nullable(new byte[] { 0, 1 })]
		public static readonly ImmutableArray<T> Empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x040068D5 RID: 26837
		[Nullable(new byte[] { 2, 1 })]
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		internal T[] array;

		// Token: 0x02002085 RID: 8325
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableArrayBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
		{
			// Token: 0x0601156E RID: 71022 RVA: 0x003B913C File Offset: 0x003B733C
			internal Builder(int capacity)
			{
				Requires.Range(capacity >= 0, "capacity", null);
				this._elements = new T[capacity];
				this._count = 0;
			}

			// Token: 0x0601156F RID: 71023 RVA: 0x003B9169 File Offset: 0x003B7369
			internal Builder()
				: this(8)
			{
			}

			// Token: 0x17002E54 RID: 11860
			// (get) Token: 0x06011570 RID: 71024 RVA: 0x003B9172 File Offset: 0x003B7372
			// (set) Token: 0x06011571 RID: 71025 RVA: 0x003B917C File Offset: 0x003B737C
			public int Capacity
			{
				get
				{
					return this._elements.Length;
				}
				set
				{
					if (value < this._count)
					{
						throw new ArgumentException(SR.CapacityMustBeGreaterThanOrEqualToCount, "value");
					}
					if (value != this._elements.Length)
					{
						if (value > 0)
						{
							T[] array = new T[value];
							if (this._count > 0)
							{
								Array.Copy(this._elements, array, this._count);
							}
							this._elements = array;
							return;
						}
						this._elements = ImmutableArray<T>.Empty.array;
					}
				}
			}

			// Token: 0x17002E55 RID: 11861
			// (get) Token: 0x06011572 RID: 71026 RVA: 0x003B91EB File Offset: 0x003B73EB
			// (set) Token: 0x06011573 RID: 71027 RVA: 0x003B91F4 File Offset: 0x003B73F4
			public int Count
			{
				get
				{
					return this._count;
				}
				set
				{
					Requires.Range(value >= 0, "value", null);
					if (value < this._count)
					{
						if (this._count - value > 64)
						{
							Array.Clear(this._elements, value, this._count - value);
						}
						else
						{
							for (int i = value; i < this.Count; i++)
							{
								this._elements[i] = default(T);
							}
						}
					}
					else if (value > this._count)
					{
						this.EnsureCapacity(value);
					}
					this._count = value;
				}
			}

			// Token: 0x06011574 RID: 71028 RVA: 0x003B927D File Offset: 0x003B747D
			private static void ThrowIndexOutOfRangeException()
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x17002E56 RID: 11862
			public T this[int index]
			{
				get
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					return this._elements[index];
				}
				set
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					this._elements[index] = value;
				}
			}

			// Token: 0x06011577 RID: 71031 RVA: 0x003B92BD File Offset: 0x003B74BD
			public readonly ref T ItemRef(int index)
			{
				if (index >= this.Count)
				{
					ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
				}
				return ref this._elements[index];
			}

			// Token: 0x17002E57 RID: 11863
			// (get) Token: 0x06011578 RID: 71032 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06011579 RID: 71033 RVA: 0x003B92DB File Offset: 0x003B74DB
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<T> ToImmutable()
			{
				return new ImmutableArray<T>(this.ToArray());
			}

			// Token: 0x0601157A RID: 71034 RVA: 0x003B92E8 File Offset: 0x003B74E8
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<T> MoveToImmutable()
			{
				if (this.Capacity != this.Count)
				{
					throw new InvalidOperationException(SR.CapacityMustEqualCountOnMove);
				}
				T[] elements = this._elements;
				this._elements = ImmutableArray<T>.Empty.array;
				this._count = 0;
				return new ImmutableArray<T>(elements);
			}

			// Token: 0x0601157B RID: 71035 RVA: 0x003B9332 File Offset: 0x003B7532
			public void Clear()
			{
				this.Count = 0;
			}

			// Token: 0x0601157C RID: 71036 RVA: 0x003B933C File Offset: 0x003B753C
			public void Insert(int index, T item)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				this.EnsureCapacity(this.Count + 1);
				if (index < this.Count)
				{
					Array.Copy(this._elements, index, this._elements, index + 1, this.Count - index);
				}
				this._count++;
				this._elements[index] = item;
			}

			// Token: 0x0601157D RID: 71037 RVA: 0x003B93B8 File Offset: 0x003B75B8
			public void Add(T item)
			{
				int num = this._count + 1;
				this.EnsureCapacity(num);
				this._elements[this._count] = item;
				this._count = num;
			}

			// Token: 0x0601157E RID: 71038 RVA: 0x003B93F0 File Offset: 0x003B75F0
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				int num;
				if (items.TryGetCount(out num))
				{
					this.EnsureCapacity(this.Count + num);
					if (items.TryCopyTo(this._elements, this._count))
					{
						this._count += num;
						return;
					}
				}
				foreach (T t in items)
				{
					this.Add(t);
				}
			}

			// Token: 0x0601157F RID: 71039 RVA: 0x003B9480 File Offset: 0x003B7680
			public void AddRange(params T[] items)
			{
				Requires.NotNull<T[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x06011580 RID: 71040 RVA: 0x003B94C0 File Offset: 0x003B76C0
			public void AddRange<[Nullable(0)] TDerived>(TDerived[] items) where TDerived : T
			{
				Requires.NotNull<TDerived[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x06011581 RID: 71041 RVA: 0x003B9500 File Offset: 0x003B7700
			public void AddRange(T[] items, int length)
			{
				Requires.NotNull<T[]>(items, "items");
				Requires.Range(length >= 0 && length <= items.Length, "length", null);
				int count = this.Count;
				this.Count += length;
				Array.Copy(items, 0, this._elements, count, length);
			}

			// Token: 0x06011582 RID: 71042 RVA: 0x003B9557 File Offset: 0x003B7757
			public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
			{
				this.AddRange(items, items.Length);
			}

			// Token: 0x06011583 RID: 71043 RVA: 0x003B9567 File Offset: 0x003B7767
			public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, int length)
			{
				Requires.Range(length >= 0, "length", null);
				if (items.array != null)
				{
					this.AddRange(items.array, length);
				}
			}

			// Token: 0x06011584 RID: 71044 RVA: 0x003B9590 File Offset: 0x003B7790
			[NullableContext(0)]
			public void AddRange<TDerived>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items) where TDerived : T
			{
				if (items.array != null)
				{
					this.AddRange<TDerived>(items.array);
				}
			}

			// Token: 0x06011585 RID: 71045 RVA: 0x003B95A6 File Offset: 0x003B77A6
			public void AddRange([Nullable(new byte[] { 1, 0 })] ImmutableArray<T>.Builder items)
			{
				Requires.NotNull<ImmutableArray<T>.Builder>(items, "items");
				this.AddRange(items._elements, items.Count);
			}

			// Token: 0x06011586 RID: 71046 RVA: 0x003B95C5 File Offset: 0x003B77C5
			public void AddRange<[Nullable(0)] TDerived>(ImmutableArray<TDerived>.Builder items) where TDerived : T
			{
				Requires.NotNull<ImmutableArray<TDerived>.Builder>(items, "items");
				this.AddRange<TDerived>(items._elements, items.Count);
			}

			// Token: 0x06011587 RID: 71047 RVA: 0x003B95E4 File Offset: 0x003B77E4
			public bool Remove(T element)
			{
				int num = this.IndexOf(element);
				if (num >= 0)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			// Token: 0x06011588 RID: 71048 RVA: 0x003B9608 File Offset: 0x003B7808
			public void RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this.Count - 1)
				{
					Array.Copy(this._elements, index + 1, this._elements, index, this.Count - index - 1);
				}
				int count = this.Count;
				this.Count = count - 1;
			}

			// Token: 0x06011589 RID: 71049 RVA: 0x003B966A File Offset: 0x003B786A
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x0601158A RID: 71050 RVA: 0x003B967C File Offset: 0x003B787C
			public T[] ToArray()
			{
				if (this.Count == 0)
				{
					return ImmutableArray<T>.Empty.array;
				}
				T[] array = new T[this.Count];
				Array.Copy(this._elements, array, this.Count);
				return array;
			}

			// Token: 0x0601158B RID: 71051 RVA: 0x003B96BC File Offset: 0x003B78BC
			public void CopyTo(T[] array, int index)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0 && index + this.Count <= array.Length, "index", null);
				Array.Copy(this._elements, 0, array, index, this.Count);
			}

			// Token: 0x0601158C RID: 71052 RVA: 0x003B970C File Offset: 0x003B790C
			private void EnsureCapacity(int capacity)
			{
				if (this._elements.Length < capacity)
				{
					int num = Math.Max(this._elements.Length * 2, capacity);
					Array.Resize<T>(ref this._elements, num);
				}
			}

			// Token: 0x0601158D RID: 71053 RVA: 0x003B9741 File Offset: 0x003B7941
			public int IndexOf(T item)
			{
				return this.IndexOf(item, 0, this._count, EqualityComparer<T>.Default);
			}

			// Token: 0x0601158E RID: 71054 RVA: 0x003B9756 File Offset: 0x003B7956
			public int IndexOf(T item, int startIndex)
			{
				return this.IndexOf(item, startIndex, this.Count - startIndex, EqualityComparer<T>.Default);
			}

			// Token: 0x0601158F RID: 71055 RVA: 0x003B976D File Offset: 0x003B796D
			public int IndexOf(T item, int startIndex, int count)
			{
				return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x06011590 RID: 71056 RVA: 0x003B9780 File Offset: 0x003B7980
			public int IndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex + count <= this.Count, "count", null);
				equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.IndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (equalityComparer.Equals(this._elements[i], item))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06011591 RID: 71057 RVA: 0x003B981C File Offset: 0x003B7A1C
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x06011592 RID: 71058 RVA: 0x003B9842 File Offset: 0x003B7A42
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				return this.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x06011593 RID: 71059 RVA: 0x003B987C File Offset: 0x003B7A7C
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x06011594 RID: 71060 RVA: 0x003B988C File Offset: 0x003B7A8C
			public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
				equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.LastIndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i >= startIndex - count + 1; i--)
				{
					if (equalityComparer.Equals(item, this._elements[i]))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06011595 RID: 71061 RVA: 0x003B9928 File Offset: 0x003B7B28
			public void Reverse()
			{
				int i = 0;
				int num = this._count - 1;
				T[] elements = this._elements;
				while (i < num)
				{
					T t = elements[i];
					elements[i] = elements[num];
					elements[num] = t;
					i++;
					num--;
				}
			}

			// Token: 0x06011596 RID: 71062 RVA: 0x003B9973 File Offset: 0x003B7B73
			public void Sort()
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this.Count, Comparer<T>.Default);
				}
			}

			// Token: 0x06011597 RID: 71063 RVA: 0x003B9995 File Offset: 0x003B7B95
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, Comparer<T>.Create(comparison));
				}
			}

			// Token: 0x06011598 RID: 71064 RVA: 0x003B99C3 File Offset: 0x003B7BC3
			public void Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, comparer);
				}
			}

			// Token: 0x06011599 RID: 71065 RVA: 0x003B99E4 File Offset: 0x003B7BE4
			public void Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
				if (count > 1)
				{
					Array.Sort<T>(this._elements, index, count, comparer);
				}
			}

			// Token: 0x0601159A RID: 71066 RVA: 0x003B9A35 File Offset: 0x003B7C35
			public IEnumerator<T> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num + 1)
				{
					yield return this[i];
					num = i;
				}
				yield break;
			}

			// Token: 0x0601159B RID: 71067 RVA: 0x003B9A44 File Offset: 0x003B7C44
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0601159C RID: 71068 RVA: 0x003B9A44 File Offset: 0x003B7C44
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0601159D RID: 71069 RVA: 0x003B9A4C File Offset: 0x003B7C4C
			private void AddRange<TDerived>(TDerived[] items, int length) where TDerived : T
			{
				this.EnsureCapacity(this.Count + length);
				int count = this.Count;
				this.Count += length;
				T[] elements = this._elements;
				for (int i = 0; i < length; i++)
				{
					elements[count + i] = (T)((object)items[i]);
				}
			}

			// Token: 0x040068D6 RID: 26838
			private T[] _elements;

			// Token: 0x040068D7 RID: 26839
			private int _count;
		}

		// Token: 0x02002087 RID: 8327
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x060115A4 RID: 71076 RVA: 0x003B9B42 File Offset: 0x003B7D42
			internal Enumerator(T[] array)
			{
				this._array = array;
				this._index = -1;
			}

			// Token: 0x17002E5A RID: 11866
			// (get) Token: 0x060115A5 RID: 71077 RVA: 0x003B9B52 File Offset: 0x003B7D52
			public T Current
			{
				get
				{
					return this._array[this._index];
				}
			}

			// Token: 0x060115A6 RID: 71078 RVA: 0x003B9B68 File Offset: 0x003B7D68
			public bool MoveNext()
			{
				int num = this._index + 1;
				this._index = num;
				return num < this._array.Length;
			}

			// Token: 0x040068DC RID: 26844
			private readonly T[] _array;

			// Token: 0x040068DD RID: 26845
			private int _index;
		}

		// Token: 0x02002088 RID: 8328
		private class EnumeratorObject : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060115A7 RID: 71079 RVA: 0x003B9B90 File Offset: 0x003B7D90
			private EnumeratorObject(T[] array)
			{
				this._index = -1;
				this._array = array;
			}

			// Token: 0x17002E5B RID: 11867
			// (get) Token: 0x060115A8 RID: 71080 RVA: 0x003B9BA6 File Offset: 0x003B7DA6
			public T Current
			{
				get
				{
					if (this._index < this._array.Length)
					{
						return this._array[this._index];
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17002E5C RID: 11868
			// (get) Token: 0x060115A9 RID: 71081 RVA: 0x003B9BCF File Offset: 0x003B7DCF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060115AA RID: 71082 RVA: 0x003B9BDC File Offset: 0x003B7DDC
			public bool MoveNext()
			{
				int num = this._index + 1;
				int num2 = this._array.Length;
				if (num <= num2)
				{
					this._index = num;
					return num < num2;
				}
				return false;
			}

			// Token: 0x060115AB RID: 71083 RVA: 0x003B9C0C File Offset: 0x003B7E0C
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x060115AC RID: 71084 RVA: 0x0000CC37 File Offset: 0x0000AE37
			public void Dispose()
			{
			}

			// Token: 0x060115AD RID: 71085 RVA: 0x003B9C15 File Offset: 0x003B7E15
			internal static IEnumerator<T> Create(T[] array)
			{
				if (array.Length != 0)
				{
					return new ImmutableArray<T>.EnumeratorObject(array);
				}
				return ImmutableArray<T>.EnumeratorObject.s_EmptyEnumerator;
			}

			// Token: 0x040068DE RID: 26846
			private static readonly IEnumerator<T> s_EmptyEnumerator = new ImmutableArray<T>.EnumeratorObject(ImmutableArray<T>.Empty.array);

			// Token: 0x040068DF RID: 26847
			private readonly T[] _array;

			// Token: 0x040068E0 RID: 26848
			private int _index;
		}
	}
}
