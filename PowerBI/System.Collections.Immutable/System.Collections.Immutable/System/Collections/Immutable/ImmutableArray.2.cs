using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System.Collections.Immutable
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[NonVersionable]
	public struct ImmutableArray<[Nullable(2)] T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IList<T>, ICollection<T>, IEquatable<ImmutableArray<T>>, IList, ICollection, IImmutableArray, IStructuralComparable, IStructuralEquatable, IImmutableList<T>
	{
		// Token: 0x17000030 RID: 48
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000041DE File Offset: 0x000023DE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000041E4 File Offset: 0x000023E4
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004208 File Offset: 0x00002408
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

		// Token: 0x17000034 RID: 52
		T IReadOnlyList<T>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004250 File Offset: 0x00002450
		public int IndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, 0, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000427C File Offset: 0x0000247C
		public int IndexOf(T item, int startIndex, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, equalityComparer);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000042A4 File Offset: 0x000024A4
		public int IndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000042CF File Offset: 0x000024CF
		public int IndexOf(T item, int startIndex, int count)
		{
			return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000042E0 File Offset: 0x000024E0
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

		// Token: 0x06000120 RID: 288 RVA: 0x0000438C File Offset: 0x0000258C
		public int LastIndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, immutableArray.Length - 1, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000043C8 File Offset: 0x000025C8
		public int LastIndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0 && startIndex == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000043FB File Offset: 0x000025FB
		public int LastIndexOf(T item, int startIndex, int count)
		{
			return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000440C File Offset: 0x0000260C
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

		// Token: 0x06000124 RID: 292 RVA: 0x000044B6 File Offset: 0x000026B6
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000044C8 File Offset: 0x000026C8
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

		// Token: 0x06000126 RID: 294 RVA: 0x00004564 File Offset: 0x00002764
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

		// Token: 0x06000127 RID: 295 RVA: 0x00004664 File Offset: 0x00002864
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

		// Token: 0x06000128 RID: 296 RVA: 0x00004724 File Offset: 0x00002924
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

		// Token: 0x06000129 RID: 297 RVA: 0x00004758 File Offset: 0x00002958
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000477C File Offset: 0x0000297C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000047A0 File Offset: 0x000029A0
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

		// Token: 0x0600012C RID: 300 RVA: 0x00004805 File Offset: 0x00002A05
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004814 File Offset: 0x00002A14
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

		// Token: 0x0600012E RID: 302 RVA: 0x00004857 File Offset: 0x00002A57
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Remove(T item)
		{
			return this.Remove(item, EqualityComparer<T>.Default);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004868 File Offset: 0x00002A68
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

		// Token: 0x06000130 RID: 304 RVA: 0x000048A3 File Offset: 0x00002AA3
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveAt(int index)
		{
			return this.RemoveRange(index, 1);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000048B0 File Offset: 0x00002AB0
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

		// Token: 0x06000132 RID: 306 RVA: 0x0000494D File Offset: 0x00002B4D
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000495C File Offset: 0x00002B5C
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

		// Token: 0x06000134 RID: 308 RVA: 0x00004A04 File Offset: 0x00002C04
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004A14 File Offset: 0x00002C14
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

		// Token: 0x06000136 RID: 310 RVA: 0x00004A74 File Offset: 0x00002C74
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

		// Token: 0x06000137 RID: 311 RVA: 0x00004AEB File Offset: 0x00002CEB
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Clear()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004AF4 File Offset: 0x00002CF4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort()
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, Comparer<T>.Default);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004B1C File Offset: 0x00002D1C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(Comparer<T>.Create(comparison));
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004B48 File Offset: 0x00002D48
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, comparer);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004B6C File Offset: 0x00002D6C
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

		// Token: 0x0600013C RID: 316 RVA: 0x00004C38 File Offset: 0x00002E38
		public IEnumerable<TResult> OfType<[Nullable(2)] TResult>()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array == null || immutableArray.array.Length == 0)
			{
				return Enumerable.Empty<TResult>();
			}
			return immutableArray.array.OfType<TResult>();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004C6E File Offset: 0x00002E6E
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004C75 File Offset: 0x00002E75
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004C7C File Offset: 0x00002E7C
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004C83 File Offset: 0x00002E83
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004C8A File Offset: 0x00002E8A
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004C94 File Offset: 0x00002E94
		IImmutableList<T> IImmutableList<T>.Clear()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Clear();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004CBC File Offset: 0x00002EBC
		IImmutableList<T> IImmutableList<T>.Add(T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Add(value);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004CE4 File Offset: 0x00002EE4
		IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.AddRange(items);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004D0C File Offset: 0x00002F0C
		IImmutableList<T> IImmutableList<T>.Insert(int index, T element)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Insert(index, element);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004D38 File Offset: 0x00002F38
		IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.InsertRange(index, items);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004D64 File Offset: 0x00002F64
		IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Remove(value, equalityComparer);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004D90 File Offset: 0x00002F90
		IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAll(match);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004DB8 File Offset: 0x00002FB8
		IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(items, equalityComparer);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004DE4 File Offset: 0x00002FE4
		IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(index, count);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004E10 File Offset: 0x00003010
		IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAt(index);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004E38 File Offset: 0x00003038
		IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.SetItem(index, value);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004E64 File Offset: 0x00003064
		IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004E8E File Offset: 0x0000308E
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E95 File Offset: 0x00003095
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004E9C File Offset: 0x0000309C
		bool IList.Contains(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Contains((T)((object)value));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004EC4 File Offset: 0x000030C4
		int IList.IndexOf(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.IndexOf((T)((object)value));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004EEC File Offset: 0x000030EC
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004EF3 File Offset: 0x000030F3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004EF6 File Offset: 0x000030F6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004EFC File Offset: 0x000030FC
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004F1E File Offset: 0x0000311E
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004F21 File Offset: 0x00003121
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004F28 File Offset: 0x00003128
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004F2F File Offset: 0x0000312F
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700003A RID: 58
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

		// Token: 0x0600015C RID: 348 RVA: 0x00004F68 File Offset: 0x00003168
		void ICollection.CopyTo(Array array, int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			Array.Copy(immutableArray.array, 0, array, index, immutableArray.Length);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004F98 File Offset: 0x00003198
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

		// Token: 0x0600015E RID: 350 RVA: 0x00004FF0 File Offset: 0x000031F0
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

		// Token: 0x0600015F RID: 351 RVA: 0x00005024 File Offset: 0x00003224
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

		// Token: 0x06000160 RID: 352 RVA: 0x000050B8 File Offset: 0x000032B8
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

		// Token: 0x06000161 RID: 353 RVA: 0x00005190 File Offset: 0x00003390
		internal ImmutableArray([Nullable(new byte[] { 2, 1 })] T[] items)
		{
			this.array = items;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005199 File Offset: 0x00003399
		[NonVersionable]
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000051A3 File Offset: 0x000033A3
		[NonVersionable]
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000051B0 File Offset: 0x000033B0
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? right)
		{
			return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000051D4 File Offset: 0x000033D4
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? right)
		{
			return !left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x1700003B RID: 59
		public T this[int index]
		{
			[NonVersionable]
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005208 File Offset: 0x00003408
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this.array.Length == 0;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005214 File Offset: 0x00003414
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Length
		{
			[NonVersionable]
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000521E File Offset: 0x0000341E
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefault
		{
			get
			{
				return this.array == null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000522C File Offset: 0x0000342C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefaultOrEmpty
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				return immutableArray.array == null || immutableArray.array.Length == 0;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005254 File Offset: 0x00003454
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Array IImmutableArray.Array
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000525C File Offset: 0x0000345C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				if (!immutableArray.IsDefault)
				{
					return string.Format(CultureInfo.CurrentCulture, "Length = {0}", new object[] { immutableArray.Length });
				}
				return "Uninitialized";
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000052A4 File Offset: 0x000034A4
		public void CopyTo(T[] destination)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, destination, immutableArray.Length);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000052D4 File Offset: 0x000034D4
		public void CopyTo(T[] destination, int destinationIndex)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, destinationIndex, immutableArray.Length);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005304 File Offset: 0x00003504
		public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, sourceIndex, destination, destinationIndex, length);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005330 File Offset: 0x00003530
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

		// Token: 0x06000171 RID: 369 RVA: 0x00005368 File Offset: 0x00003568
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ImmutableArray<T>.Enumerator GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			return new ImmutableArray<T>.Enumerator(immutableArray.array);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005390 File Offset: 0x00003590
		public override int GetHashCode()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array != null)
			{
				return immutableArray.array.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000053BC File Offset: 0x000035BC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			IImmutableArray immutableArray = obj as IImmutableArray;
			return immutableArray != null && this.array == immutableArray.Array;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000053E3 File Offset: 0x000035E3
		[NonVersionable]
		public bool Equals([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> other)
		{
			return this.array == other.array;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000053F4 File Offset: 0x000035F4
		[NullableContext(0)]
		public static ImmutableArray<T> CastUp<[Nullable(2)] TDerived>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items) where TDerived : class, T
		{
			T[] array = items.array;
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000540E File Offset: 0x0000360E
		[NullableContext(0)]
		public ImmutableArray<TOther> CastArray<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>((TOther[])this.array);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005420 File Offset: 0x00003620
		[NullableContext(0)]
		public ImmutableArray<TOther> As<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>(this.array as TOther[]);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005434 File Offset: 0x00003634
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000545C File Offset: 0x0000365C
		IEnumerator IEnumerable.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005482 File Offset: 0x00003682
		internal void ThrowNullRefIfNotInitialized()
		{
			int num = this.array.Length;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000548D File Offset: 0x0000368D
		private void ThrowInvalidOperationIfNotInitialized()
		{
			if (this.IsDefault)
			{
				throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
			}
		}

		// Token: 0x0400001A RID: 26
		[Nullable(new byte[] { 0, 1 })]
		public static readonly ImmutableArray<T> Empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x0400001B RID: 27
		[Nullable(new byte[] { 2, 1 })]
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		internal T[] array;

		// Token: 0x0200005A RID: 90
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableArrayBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
		{
			// Token: 0x060003F7 RID: 1015 RVA: 0x0000A5F0 File Offset: 0x000087F0
			internal Builder(int capacity)
			{
				Requires.Range(capacity >= 0, "capacity", null);
				this._elements = new T[capacity];
				this._count = 0;
			}

			// Token: 0x060003F8 RID: 1016 RVA: 0x0000A61D File Offset: 0x0000881D
			internal Builder()
				: this(8)
			{
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000A626 File Offset: 0x00008826
			// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000A630 File Offset: 0x00008830
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

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000A69F File Offset: 0x0000889F
			// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000A6A8 File Offset: 0x000088A8
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

			// Token: 0x060003FD RID: 1021 RVA: 0x0000A731 File Offset: 0x00008931
			private static void ThrowIndexOutOfRangeException()
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x170000B6 RID: 182
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

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000A771 File Offset: 0x00008971
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x0000A774 File Offset: 0x00008974
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<T> ToImmutable()
			{
				return new ImmutableArray<T>(this.ToArray());
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x0000A784 File Offset: 0x00008984
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

			// Token: 0x06000403 RID: 1027 RVA: 0x0000A7CE File Offset: 0x000089CE
			public void Clear()
			{
				this.Count = 0;
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x0000A7D8 File Offset: 0x000089D8
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

			// Token: 0x06000405 RID: 1029 RVA: 0x0000A854 File Offset: 0x00008A54
			public void Add(T item)
			{
				int num = this._count + 1;
				this.EnsureCapacity(num);
				this._elements[this._count] = item;
				this._count = num;
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x0000A88C File Offset: 0x00008A8C
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

			// Token: 0x06000407 RID: 1031 RVA: 0x0000A91C File Offset: 0x00008B1C
			public void AddRange(params T[] items)
			{
				Requires.NotNull<T[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x0000A95C File Offset: 0x00008B5C
			public void AddRange<[Nullable(0)] TDerived>(TDerived[] items) where TDerived : T
			{
				Requires.NotNull<TDerived[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x0000A99C File Offset: 0x00008B9C
			public void AddRange(T[] items, int length)
			{
				Requires.NotNull<T[]>(items, "items");
				Requires.Range(length >= 0 && length <= items.Length, "length", null);
				int count = this.Count;
				this.Count += length;
				Array.Copy(items, 0, this._elements, count, length);
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0000A9F3 File Offset: 0x00008BF3
			public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
			{
				this.AddRange(items, items.Length);
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x0000AA03 File Offset: 0x00008C03
			public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, int length)
			{
				Requires.Range(length >= 0, "length", null);
				if (items.array != null)
				{
					this.AddRange(items.array, length);
				}
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000AA2C File Offset: 0x00008C2C
			[NullableContext(0)]
			public void AddRange<TDerived>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items) where TDerived : T
			{
				if (items.array != null)
				{
					this.AddRange<TDerived>(items.array);
				}
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0000AA42 File Offset: 0x00008C42
			public void AddRange([Nullable(new byte[] { 1, 0 })] ImmutableArray<T>.Builder items)
			{
				Requires.NotNull<ImmutableArray<T>.Builder>(items, "items");
				this.AddRange(items._elements, items.Count);
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000AA61 File Offset: 0x00008C61
			public void AddRange<[Nullable(0)] TDerived>(ImmutableArray<TDerived>.Builder items) where TDerived : T
			{
				Requires.NotNull<ImmutableArray<TDerived>.Builder>(items, "items");
				this.AddRange<TDerived>(items._elements, items.Count);
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x0000AA80 File Offset: 0x00008C80
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

			// Token: 0x06000410 RID: 1040 RVA: 0x0000AAA4 File Offset: 0x00008CA4
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

			// Token: 0x06000411 RID: 1041 RVA: 0x0000AB06 File Offset: 0x00008D06
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x0000AB18 File Offset: 0x00008D18
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

			// Token: 0x06000413 RID: 1043 RVA: 0x0000AB58 File Offset: 0x00008D58
			public void CopyTo(T[] array, int index)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0 && index + this.Count <= array.Length, "index", null);
				Array.Copy(this._elements, 0, array, index, this.Count);
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000ABA8 File Offset: 0x00008DA8
			private void EnsureCapacity(int capacity)
			{
				if (this._elements.Length < capacity)
				{
					int num = Math.Max(this._elements.Length * 2, capacity);
					Array.Resize<T>(ref this._elements, num);
				}
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x0000ABDD File Offset: 0x00008DDD
			public int IndexOf(T item)
			{
				return this.IndexOf(item, 0, this._count, EqualityComparer<T>.Default);
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000ABF2 File Offset: 0x00008DF2
			public int IndexOf(T item, int startIndex)
			{
				return this.IndexOf(item, startIndex, this.Count - startIndex, EqualityComparer<T>.Default);
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x0000AC09 File Offset: 0x00008E09
			public int IndexOf(T item, int startIndex, int count)
			{
				return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x0000AC1C File Offset: 0x00008E1C
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

			// Token: 0x06000419 RID: 1049 RVA: 0x0000ACB8 File Offset: 0x00008EB8
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x0000ACDE File Offset: 0x00008EDE
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				return this.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x0000AD18 File Offset: 0x00008F18
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x0000AD28 File Offset: 0x00008F28
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

			// Token: 0x0600041D RID: 1053 RVA: 0x0000ADC4 File Offset: 0x00008FC4
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

			// Token: 0x0600041E RID: 1054 RVA: 0x0000AE0F File Offset: 0x0000900F
			public void Sort()
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this.Count, Comparer<T>.Default);
				}
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000AE31 File Offset: 0x00009031
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, Comparer<T>.Create(comparison));
				}
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x0000AE5F File Offset: 0x0000905F
			public void Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, comparer);
				}
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000AE80 File Offset: 0x00009080
			public void Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
				if (count > 1)
				{
					Array.Sort<T>(this._elements, index, count, comparer);
				}
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x0000AED1 File Offset: 0x000090D1
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

			// Token: 0x06000423 RID: 1059 RVA: 0x0000AEE0 File Offset: 0x000090E0
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000AEE8 File Offset: 0x000090E8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x0000AEF0 File Offset: 0x000090F0
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

			// Token: 0x04000076 RID: 118
			private T[] _elements;

			// Token: 0x04000077 RID: 119
			private int _count;
		}

		// Token: 0x0200005B RID: 91
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x06000426 RID: 1062 RVA: 0x0000AF4D File Offset: 0x0000914D
			internal Enumerator(T[] array)
			{
				this._array = array;
				this._index = -1;
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000AF5D File Offset: 0x0000915D
			public T Current
			{
				get
				{
					return this._array[this._index];
				}
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x0000AF70 File Offset: 0x00009170
			public bool MoveNext()
			{
				int num = this._index + 1;
				this._index = num;
				return num < this._array.Length;
			}

			// Token: 0x04000078 RID: 120
			private readonly T[] _array;

			// Token: 0x04000079 RID: 121
			private int _index;
		}

		// Token: 0x0200005C RID: 92
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000429 RID: 1065 RVA: 0x0000AF98 File Offset: 0x00009198
			private EnumeratorObject(T[] array)
			{
				this._index = -1;
				this._array = array;
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000AFAE File Offset: 0x000091AE
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

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000AFD7 File Offset: 0x000091D7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0000AFE4 File Offset: 0x000091E4
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

			// Token: 0x0600042D RID: 1069 RVA: 0x0000B014 File Offset: 0x00009214
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x0000B01D File Offset: 0x0000921D
			public void Dispose()
			{
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0000B01F File Offset: 0x0000921F
			internal static IEnumerator<T> Create(T[] array)
			{
				if (array.Length != 0)
				{
					return new ImmutableArray<T>.EnumeratorObject(array);
				}
				return ImmutableArray<T>.EnumeratorObject.s_EmptyEnumerator;
			}

			// Token: 0x0400007A RID: 122
			private static readonly IEnumerator<T> s_EmptyEnumerator = new ImmutableArray<T>.EnumeratorObject(ImmutableArray<T>.Empty.array);

			// Token: 0x0400007B RID: 123
			private readonly T[] _array;

			// Token: 0x0400007C RID: 124
			private int _index;
		}
	}
}
