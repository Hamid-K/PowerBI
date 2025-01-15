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
	// Token: 0x020020A7 RID: 8359
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableList<[Nullable(2)] T> : IImmutableList<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IList<T>, ICollection<T>, IList, ICollection, IOrderedCollection<T>, IImmutableListQueries<T>, IStrongEnumerable<T, ImmutableList<T>.Enumerator>
	{
		// Token: 0x060116F2 RID: 71410 RVA: 0x003BCB57 File Offset: 0x003BAD57
		internal ImmutableList()
		{
			this._root = ImmutableList<T>.Node.EmptyNode;
		}

		// Token: 0x060116F3 RID: 71411 RVA: 0x003BCB6A File Offset: 0x003BAD6A
		private ImmutableList(ImmutableList<T>.Node root)
		{
			Requires.NotNull<ImmutableList<T>.Node>(root, "root");
			root.Freeze();
			this._root = root;
		}

		// Token: 0x060116F4 RID: 71412 RVA: 0x003BCB8A File Offset: 0x003BAD8A
		public ImmutableList<T> Clear()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x060116F5 RID: 71413 RVA: 0x003BCB91 File Offset: 0x003BAD91
		public int BinarySearch(T item)
		{
			return this.BinarySearch(item, null);
		}

		// Token: 0x060116F6 RID: 71414 RVA: 0x003BCB9B File Offset: 0x003BAD9B
		public int BinarySearch(T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x060116F7 RID: 71415 RVA: 0x003BCBAC File Offset: 0x003BADAC
		public int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return this._root.BinarySearch(index, count, item, comparer);
		}

		// Token: 0x17002EA6 RID: 11942
		// (get) Token: 0x060116F8 RID: 71416 RVA: 0x003BCBBE File Offset: 0x003BADBE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x060116F9 RID: 71417 RVA: 0x003BCBCB File Offset: 0x003BADCB
		IImmutableList<T> IImmutableList<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17002EA7 RID: 11943
		// (get) Token: 0x060116FA RID: 71418 RVA: 0x003BCBD3 File Offset: 0x003BADD3
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x17002EA8 RID: 11944
		// (get) Token: 0x060116FB RID: 71419 RVA: 0x00004FAE File Offset: 0x000031AE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002EA9 RID: 11945
		// (get) Token: 0x060116FC RID: 71420 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002EAA RID: 11946
		public unsafe T this[int index]
		{
			get
			{
				return *this._root.ItemRef(index);
			}
		}

		// Token: 0x060116FE RID: 71422 RVA: 0x003BCBF3 File Offset: 0x003BADF3
		public readonly ref T ItemRef(int index)
		{
			return this._root.ItemRef(index);
		}

		// Token: 0x17002EAB RID: 11947
		T IOrderedCollection<T>.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x06011700 RID: 71424 RVA: 0x003BCC0A File Offset: 0x003BAE0A
		[return: Nullable(new byte[] { 1, 0 })]
		public ImmutableList<T>.Builder ToBuilder()
		{
			return new ImmutableList<T>.Builder(this);
		}

		// Token: 0x06011701 RID: 71425 RVA: 0x003BCC14 File Offset: 0x003BAE14
		public ImmutableList<T> Add(T value)
		{
			ImmutableList<T>.Node node = this._root.Add(value);
			return this.Wrap(node);
		}

		// Token: 0x06011702 RID: 71426 RVA: 0x003BCC38 File Offset: 0x003BAE38
		public ImmutableList<T> AddRange(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty)
			{
				return ImmutableList<T>.CreateRange(items);
			}
			ImmutableList<T>.Node node = this._root.AddRange(items);
			return this.Wrap(node);
		}

		// Token: 0x06011703 RID: 71427 RVA: 0x003BCC73 File Offset: 0x003BAE73
		public ImmutableList<T> Insert(int index, T item)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			return this.Wrap(this._root.Insert(index, item));
		}

		// Token: 0x06011704 RID: 71428 RVA: 0x003BCCA8 File Offset: 0x003BAEA8
		public ImmutableList<T> InsertRange(int index, IEnumerable<T> items)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableList<T>.Node node = this._root.InsertRange(index, items);
			return this.Wrap(node);
		}

		// Token: 0x06011705 RID: 71429 RVA: 0x003BCCF3 File Offset: 0x003BAEF3
		public ImmutableList<T> Remove(T value)
		{
			return this.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06011706 RID: 71430 RVA: 0x003BCD04 File Offset: 0x003BAF04
		public ImmutableList<T> Remove(T value, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(value, equalityComparer);
			if (num >= 0)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x06011707 RID: 71431 RVA: 0x003BCD28 File Offset: 0x003BAF28
		public ImmutableList<T> RemoveRange(int index, int count)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
			ImmutableList<T>.Node node = this._root;
			int num = count;
			while (num-- > 0)
			{
				node = node.RemoveAt(index);
			}
			return this.Wrap(node);
		}

		// Token: 0x06011708 RID: 71432 RVA: 0x003BCD95 File Offset: 0x003BAF95
		public ImmutableList<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06011709 RID: 71433 RVA: 0x003BCDA4 File Offset: 0x003BAFA4
		public ImmutableList<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty)
			{
				return this;
			}
			ImmutableList<T>.Node node = this._root;
			foreach (T t in items.GetEnumerableDisposable<T, ImmutableList<T>.Enumerator>())
			{
				int num = node.IndexOf(t, equalityComparer);
				if (num >= 0)
				{
					node = node.RemoveAt(num);
				}
			}
			return this.Wrap(node);
		}

		// Token: 0x0601170A RID: 71434 RVA: 0x003BCE30 File Offset: 0x003BB030
		public ImmutableList<T> RemoveAt(int index)
		{
			Requires.Range(index >= 0 && index < this.Count, "index", null);
			ImmutableList<T>.Node node = this._root.RemoveAt(index);
			return this.Wrap(node);
		}

		// Token: 0x0601170B RID: 71435 RVA: 0x003BCE6C File Offset: 0x003BB06C
		public ImmutableList<T> RemoveAll(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this.Wrap(this._root.RemoveAll(match));
		}

		// Token: 0x0601170C RID: 71436 RVA: 0x003BCE8B File Offset: 0x003BB08B
		public ImmutableList<T> SetItem(int index, T value)
		{
			return this.Wrap(this._root.ReplaceAt(index, value));
		}

		// Token: 0x0601170D RID: 71437 RVA: 0x003BCEA0 File Offset: 0x003BB0A0
		public ImmutableList<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x0601170E RID: 71438 RVA: 0x003BCEB0 File Offset: 0x003BB0B0
		public ImmutableList<T> Replace(T oldValue, T newValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(oldValue, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return this.SetItem(num, newValue);
		}

		// Token: 0x0601170F RID: 71439 RVA: 0x003BCEE2 File Offset: 0x003BB0E2
		public ImmutableList<T> Reverse()
		{
			return this.Wrap(this._root.Reverse());
		}

		// Token: 0x06011710 RID: 71440 RVA: 0x003BCEF5 File Offset: 0x003BB0F5
		public ImmutableList<T> Reverse(int index, int count)
		{
			return this.Wrap(this._root.Reverse(index, count));
		}

		// Token: 0x06011711 RID: 71441 RVA: 0x003BCF0A File Offset: 0x003BB10A
		public ImmutableList<T> Sort()
		{
			return this.Wrap(this._root.Sort());
		}

		// Token: 0x06011712 RID: 71442 RVA: 0x003BCF1D File Offset: 0x003BB11D
		public ImmutableList<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			return this.Wrap(this._root.Sort(comparison));
		}

		// Token: 0x06011713 RID: 71443 RVA: 0x003BCF3C File Offset: 0x003BB13C
		public ImmutableList<T> Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return this.Wrap(this._root.Sort(comparer));
		}

		// Token: 0x06011714 RID: 71444 RVA: 0x003BCF50 File Offset: 0x003BB150
		public ImmutableList<T> Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(this._root.Sort(index, count, comparer));
		}

		// Token: 0x06011715 RID: 71445 RVA: 0x003BCFB0 File Offset: 0x003BB1B0
		public void ForEach(Action<T> action)
		{
			Requires.NotNull<Action<T>>(action, "action");
			foreach (T t in this)
			{
				action(t);
			}
		}

		// Token: 0x06011716 RID: 71446 RVA: 0x003BD00C File Offset: 0x003BB20C
		public void CopyTo(T[] array)
		{
			this._root.CopyTo(array);
		}

		// Token: 0x06011717 RID: 71447 RVA: 0x003BD01A File Offset: 0x003BB21A
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x06011718 RID: 71448 RVA: 0x003BD029 File Offset: 0x003BB229
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this._root.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x06011719 RID: 71449 RVA: 0x003BD03C File Offset: 0x003BB23C
		public ImmutableList<T> GetRange(int index, int count)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
		}

		// Token: 0x0601171A RID: 71450 RVA: 0x003BD094 File Offset: 0x003BB294
		public ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
		{
			Requires.NotNull<Func<T, TOutput>>(converter, "converter");
			return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
		}

		// Token: 0x0601171B RID: 71451 RVA: 0x003BD0B2 File Offset: 0x003BB2B2
		public bool Exists(Predicate<T> match)
		{
			return this._root.Exists(match);
		}

		// Token: 0x0601171C RID: 71452 RVA: 0x003BD0C0 File Offset: 0x003BB2C0
		[return: Nullable(2)]
		public T Find(Predicate<T> match)
		{
			return this._root.Find(match);
		}

		// Token: 0x0601171D RID: 71453 RVA: 0x003BD0CE File Offset: 0x003BB2CE
		public ImmutableList<T> FindAll(Predicate<T> match)
		{
			return this._root.FindAll(match);
		}

		// Token: 0x0601171E RID: 71454 RVA: 0x003BD0DC File Offset: 0x003BB2DC
		public int FindIndex(Predicate<T> match)
		{
			return this._root.FindIndex(match);
		}

		// Token: 0x0601171F RID: 71455 RVA: 0x003BD0EA File Offset: 0x003BB2EA
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, match);
		}

		// Token: 0x06011720 RID: 71456 RVA: 0x003BD0F9 File Offset: 0x003BB2F9
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, count, match);
		}

		// Token: 0x06011721 RID: 71457 RVA: 0x003BD109 File Offset: 0x003BB309
		[return: Nullable(2)]
		public T FindLast(Predicate<T> match)
		{
			return this._root.FindLast(match);
		}

		// Token: 0x06011722 RID: 71458 RVA: 0x003BD117 File Offset: 0x003BB317
		public int FindLastIndex(Predicate<T> match)
		{
			return this._root.FindLastIndex(match);
		}

		// Token: 0x06011723 RID: 71459 RVA: 0x003BD125 File Offset: 0x003BB325
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, match);
		}

		// Token: 0x06011724 RID: 71460 RVA: 0x003BD134 File Offset: 0x003BB334
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, count, match);
		}

		// Token: 0x06011725 RID: 71461 RVA: 0x003BD144 File Offset: 0x003BB344
		public int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return this._root.IndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06011726 RID: 71462 RVA: 0x003BD156 File Offset: 0x003BB356
		public int LastIndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return this._root.LastIndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06011727 RID: 71463 RVA: 0x003BD168 File Offset: 0x003BB368
		public bool TrueForAll(Predicate<T> match)
		{
			return this._root.TrueForAll(match);
		}

		// Token: 0x06011728 RID: 71464 RVA: 0x003BD176 File Offset: 0x003BB376
		public bool Contains(T value)
		{
			return this._root.Contains(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06011729 RID: 71465 RVA: 0x003BD189 File Offset: 0x003BB389
		public int IndexOf(T value)
		{
			return this.IndexOf(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0601172A RID: 71466 RVA: 0x003BD197 File Offset: 0x003BB397
		IImmutableList<T> IImmutableList<T>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x0601172B RID: 71467 RVA: 0x003BD1A0 File Offset: 0x003BB3A0
		IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
		{
			return this.AddRange(items);
		}

		// Token: 0x0601172C RID: 71468 RVA: 0x003BD1A9 File Offset: 0x003BB3A9
		IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
		{
			return this.Insert(index, item);
		}

		// Token: 0x0601172D RID: 71469 RVA: 0x003BD1B3 File Offset: 0x003BB3B3
		IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
		{
			return this.InsertRange(index, items);
		}

		// Token: 0x0601172E RID: 71470 RVA: 0x003BD1BD File Offset: 0x003BB3BD
		IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			return this.Remove(value, equalityComparer);
		}

		// Token: 0x0601172F RID: 71471 RVA: 0x003BD1C7 File Offset: 0x003BB3C7
		IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
		{
			return this.RemoveAll(match);
		}

		// Token: 0x06011730 RID: 71472 RVA: 0x003BD1D0 File Offset: 0x003BB3D0
		IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			return this.RemoveRange(items, equalityComparer);
		}

		// Token: 0x06011731 RID: 71473 RVA: 0x003BD1DA File Offset: 0x003BB3DA
		IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
		{
			return this.RemoveRange(index, count);
		}

		// Token: 0x06011732 RID: 71474 RVA: 0x003BD1E4 File Offset: 0x003BB3E4
		IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
		{
			return this.RemoveAt(index);
		}

		// Token: 0x06011733 RID: 71475 RVA: 0x003BD1ED File Offset: 0x003BB3ED
		IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
		{
			return this.SetItem(index, value);
		}

		// Token: 0x06011734 RID: 71476 RVA: 0x003BD1F7 File Offset: 0x003BB3F7
		IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			return this.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x06011735 RID: 71477 RVA: 0x003BD204 File Offset: 0x003BB404
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06011736 RID: 71478 RVA: 0x003BD231 File Offset: 0x003BB431
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06011737 RID: 71479 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011738 RID: 71480 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002EAC RID: 11948
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

		// Token: 0x0601173B RID: 71483 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601173C RID: 71484 RVA: 0x00002C72 File Offset: 0x00000E72
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002EAD RID: 11949
		// (get) Token: 0x0601173D RID: 71485 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0601173E RID: 71486 RVA: 0x00002C72 File Offset: 0x00000E72
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0601173F RID: 71487 RVA: 0x003BD23E File Offset: 0x003BB43E
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x06011740 RID: 71488 RVA: 0x00002C72 File Offset: 0x00000E72
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011741 RID: 71489 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011742 RID: 71490 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06011743 RID: 71491 RVA: 0x003BD24D File Offset: 0x003BB44D
		bool IList.Contains(object value)
		{
			return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06011744 RID: 71492 RVA: 0x003BD265 File Offset: 0x003BB465
		int IList.IndexOf(object value)
		{
			if (!ImmutableList<T>.IsCompatibleObject(value))
			{
				return -1;
			}
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06011745 RID: 71493 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002EAE RID: 11950
		// (get) Token: 0x06011746 RID: 71494 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002EAF RID: 11951
		// (get) Token: 0x06011747 RID: 71495 RVA: 0x0000A5FD File Offset: 0x000087FD
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06011748 RID: 71496 RVA: 0x00002C72 File Offset: 0x00000E72
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002EB0 RID: 11952
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

		// Token: 0x0601174B RID: 71499 RVA: 0x003BD28B File Offset: 0x003BB48B
		[NullableContext(0)]
		public ImmutableList<T>.Enumerator GetEnumerator()
		{
			return new ImmutableList<T>.Enumerator(this._root, null, -1, -1, false);
		}

		// Token: 0x17002EB1 RID: 11953
		// (get) Token: 0x0601174C RID: 71500 RVA: 0x003BD29C File Offset: 0x003BB49C
		[Nullable(new byte[] { 1, 0 })]
		internal ImmutableList<T>.Node Root
		{
			[return: Nullable(new byte[] { 1, 0 })]
			get
			{
				return this._root;
			}
		}

		// Token: 0x0601174D RID: 71501 RVA: 0x003BD2A4 File Offset: 0x003BB4A4
		private static ImmutableList<T> WrapNode(ImmutableList<T>.Node root)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return ImmutableList<T>.Empty;
		}

		// Token: 0x0601174E RID: 71502 RVA: 0x003BD2BC File Offset: 0x003BB4BC
		private static bool TryCastToImmutableList(IEnumerable<T> sequence, [NotNullWhen(true)] out ImmutableList<T> other)
		{
			other = sequence as ImmutableList<T>;
			if (other != null)
			{
				return true;
			}
			ImmutableList<T>.Builder builder = sequence as ImmutableList<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0601174F RID: 71503 RVA: 0x003BD2EC File Offset: 0x003BB4EC
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x06011750 RID: 71504 RVA: 0x003BD319 File Offset: 0x003BB519
		private ImmutableList<T> Wrap(ImmutableList<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return this.Clear();
		}

		// Token: 0x06011751 RID: 71505 RVA: 0x003BD33C File Offset: 0x003BB53C
		private static ImmutableList<T> CreateRange(IEnumerable<T> items)
		{
			ImmutableList<T> immutableList;
			if (ImmutableList<T>.TryCastToImmutableList(items, out immutableList))
			{
				return immutableList;
			}
			IOrderedCollection<T> orderedCollection = items.AsOrderedCollection<T>();
			if (orderedCollection.Count == 0)
			{
				return ImmutableList<T>.Empty;
			}
			ImmutableList<T>.Node node = ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			return new ImmutableList<T>(node);
		}

		// Token: 0x0400692F RID: 26927
		public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

		// Token: 0x04006930 RID: 26928
		private readonly ImmutableList<T>.Node _root;

		// Token: 0x020020A8 RID: 8360
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableListBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IOrderedCollection<T>, IImmutableListQueries<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
		{
			// Token: 0x06011753 RID: 71507 RVA: 0x003BD38A File Offset: 0x003BB58A
			internal Builder(ImmutableList<T> list)
			{
				Requires.NotNull<ImmutableList<T>>(list, "list");
				this._root = list._root;
				this._immutable = list;
			}

			// Token: 0x17002EB2 RID: 11954
			// (get) Token: 0x06011754 RID: 71508 RVA: 0x003BD3BB File Offset: 0x003BB5BB
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x17002EB3 RID: 11955
			// (get) Token: 0x06011755 RID: 71509 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002EB4 RID: 11956
			// (get) Token: 0x06011756 RID: 71510 RVA: 0x003BD3C8 File Offset: 0x003BB5C8
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17002EB5 RID: 11957
			// (get) Token: 0x06011757 RID: 71511 RVA: 0x003BD3D0 File Offset: 0x003BB5D0
			// (set) Token: 0x06011758 RID: 71512 RVA: 0x003BD3D8 File Offset: 0x003BB5D8
			[Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Root
			{
				[return: Nullable(new byte[] { 1, 0 })]
				get
				{
					return this._root;
				}
				private set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x17002EB6 RID: 11958
			public unsafe T this[int index]
			{
				get
				{
					return *this.Root.ItemRef(index);
				}
				set
				{
					this.Root = this.Root.ReplaceAt(index, value);
				}
			}

			// Token: 0x17002EB7 RID: 11959
			T IOrderedCollection<T>.this[int index]
			{
				get
				{
					return this[index];
				}
			}

			// Token: 0x0601175C RID: 71516 RVA: 0x003BD430 File Offset: 0x003BB630
			public readonly ref T ItemRef(int index)
			{
				return this.Root.ItemRef(index);
			}

			// Token: 0x0601175D RID: 71517 RVA: 0x003BD43E File Offset: 0x003BB63E
			public int IndexOf(T item)
			{
				return this.Root.IndexOf(item, EqualityComparer<T>.Default);
			}

			// Token: 0x0601175E RID: 71518 RVA: 0x003BD451 File Offset: 0x003BB651
			public void Insert(int index, T item)
			{
				this.Root = this.Root.Insert(index, item);
			}

			// Token: 0x0601175F RID: 71519 RVA: 0x003BD466 File Offset: 0x003BB666
			public void RemoveAt(int index)
			{
				this.Root = this.Root.RemoveAt(index);
			}

			// Token: 0x06011760 RID: 71520 RVA: 0x003BD47A File Offset: 0x003BB67A
			public void Add(T item)
			{
				this.Root = this.Root.Add(item);
			}

			// Token: 0x06011761 RID: 71521 RVA: 0x003BD48E File Offset: 0x003BB68E
			public void Clear()
			{
				this.Root = ImmutableList<T>.Node.EmptyNode;
			}

			// Token: 0x06011762 RID: 71522 RVA: 0x003BD49B File Offset: 0x003BB69B
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x06011763 RID: 71523 RVA: 0x003BD4AC File Offset: 0x003BB6AC
			public bool Remove(T item)
			{
				int num = this.IndexOf(item);
				if (num < 0)
				{
					return false;
				}
				this.Root = this.Root.RemoveAt(num);
				return true;
			}

			// Token: 0x06011764 RID: 71524 RVA: 0x003BD4DA File Offset: 0x003BB6DA
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x06011765 RID: 71525 RVA: 0x003BD4E8 File Offset: 0x003BB6E8
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011766 RID: 71526 RVA: 0x003BD4E8 File Offset: 0x003BB6E8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06011767 RID: 71527 RVA: 0x003BD4F8 File Offset: 0x003BB6F8
			public void ForEach(Action<T> action)
			{
				Requires.NotNull<Action<T>>(action, "action");
				foreach (T t in this)
				{
					action(t);
				}
			}

			// Token: 0x06011768 RID: 71528 RVA: 0x003BD554 File Offset: 0x003BB754
			public void CopyTo(T[] array)
			{
				this._root.CopyTo(array);
			}

			// Token: 0x06011769 RID: 71529 RVA: 0x003BD562 File Offset: 0x003BB762
			public void CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x0601176A RID: 71530 RVA: 0x003BD571 File Offset: 0x003BB771
			public void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				this._root.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x0601176B RID: 71531 RVA: 0x003BD584 File Offset: 0x003BB784
			public ImmutableList<T> GetRange(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				return ImmutableList<T>.WrapNode(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
			}

			// Token: 0x0601176C RID: 71532 RVA: 0x003BD5DB File Offset: 0x003BB7DB
			public ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
			{
				Requires.NotNull<Func<T, TOutput>>(converter, "converter");
				return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
			}

			// Token: 0x0601176D RID: 71533 RVA: 0x003BD5F9 File Offset: 0x003BB7F9
			public bool Exists(Predicate<T> match)
			{
				return this._root.Exists(match);
			}

			// Token: 0x0601176E RID: 71534 RVA: 0x003BD607 File Offset: 0x003BB807
			[return: Nullable(2)]
			public T Find(Predicate<T> match)
			{
				return this._root.Find(match);
			}

			// Token: 0x0601176F RID: 71535 RVA: 0x003BD615 File Offset: 0x003BB815
			public ImmutableList<T> FindAll(Predicate<T> match)
			{
				return this._root.FindAll(match);
			}

			// Token: 0x06011770 RID: 71536 RVA: 0x003BD623 File Offset: 0x003BB823
			public int FindIndex(Predicate<T> match)
			{
				return this._root.FindIndex(match);
			}

			// Token: 0x06011771 RID: 71537 RVA: 0x003BD631 File Offset: 0x003BB831
			public int FindIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, match);
			}

			// Token: 0x06011772 RID: 71538 RVA: 0x003BD640 File Offset: 0x003BB840
			public int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, count, match);
			}

			// Token: 0x06011773 RID: 71539 RVA: 0x003BD650 File Offset: 0x003BB850
			[return: Nullable(2)]
			public T FindLast(Predicate<T> match)
			{
				return this._root.FindLast(match);
			}

			// Token: 0x06011774 RID: 71540 RVA: 0x003BD65E File Offset: 0x003BB85E
			public int FindLastIndex(Predicate<T> match)
			{
				return this._root.FindLastIndex(match);
			}

			// Token: 0x06011775 RID: 71541 RVA: 0x003BD66C File Offset: 0x003BB86C
			public int FindLastIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, match);
			}

			// Token: 0x06011776 RID: 71542 RVA: 0x003BD67B File Offset: 0x003BB87B
			public int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, count, match);
			}

			// Token: 0x06011777 RID: 71543 RVA: 0x003BD68B File Offset: 0x003BB88B
			public int IndexOf(T item, int index)
			{
				return this._root.IndexOf(item, index, this.Count - index, EqualityComparer<T>.Default);
			}

			// Token: 0x06011778 RID: 71544 RVA: 0x003BD6A7 File Offset: 0x003BB8A7
			public int IndexOf(T item, int index, int count)
			{
				return this._root.IndexOf(item, index, count, EqualityComparer<T>.Default);
			}

			// Token: 0x06011779 RID: 71545 RVA: 0x003BD6BC File Offset: 0x003BB8BC
			public int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				return this._root.IndexOf(item, index, count, equalityComparer);
			}

			// Token: 0x0601177A RID: 71546 RVA: 0x003BD6CE File Offset: 0x003BB8CE
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x0601177B RID: 71547 RVA: 0x003BD6F9 File Offset: 0x003BB8F9
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x0601177C RID: 71548 RVA: 0x003BD71D File Offset: 0x003BB91D
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this._root.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x0601177D RID: 71549 RVA: 0x003BD732 File Offset: 0x003BB932
			public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				return this._root.LastIndexOf(item, startIndex, count, equalityComparer);
			}

			// Token: 0x0601177E RID: 71550 RVA: 0x003BD744 File Offset: 0x003BB944
			public bool TrueForAll(Predicate<T> match)
			{
				return this._root.TrueForAll(match);
			}

			// Token: 0x0601177F RID: 71551 RVA: 0x003BD752 File Offset: 0x003BB952
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.AddRange(items);
			}

			// Token: 0x06011780 RID: 71552 RVA: 0x003BD771 File Offset: 0x003BB971
			public void InsertRange(int index, IEnumerable<T> items)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.InsertRange(index, items);
			}

			// Token: 0x06011781 RID: 71553 RVA: 0x003BD7B0 File Offset: 0x003BB9B0
			public int RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				int count = this.Count;
				this.Root = this.Root.RemoveAll(match);
				return count - this.Count;
			}

			// Token: 0x06011782 RID: 71554 RVA: 0x003BD7E9 File Offset: 0x003BB9E9
			public void Reverse()
			{
				this.Reverse(0, this.Count);
			}

			// Token: 0x06011783 RID: 71555 RVA: 0x003BD7F8 File Offset: 0x003BB9F8
			public void Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Reverse(index, count);
			}

			// Token: 0x06011784 RID: 71556 RVA: 0x003BD855 File Offset: 0x003BBA55
			public void Sort()
			{
				this.Root = this.Root.Sort();
			}

			// Token: 0x06011785 RID: 71557 RVA: 0x003BD868 File Offset: 0x003BBA68
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				this.Root = this.Root.Sort(comparison);
			}

			// Token: 0x06011786 RID: 71558 RVA: 0x003BD887 File Offset: 0x003BBA87
			public void Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				this.Root = this.Root.Sort(comparer);
			}

			// Token: 0x06011787 RID: 71559 RVA: 0x003BD89C File Offset: 0x003BBA9C
			public void Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Sort(index, count, comparer);
			}

			// Token: 0x06011788 RID: 71560 RVA: 0x003BD8FA File Offset: 0x003BBAFA
			public int BinarySearch(T item)
			{
				return this.BinarySearch(item, null);
			}

			// Token: 0x06011789 RID: 71561 RVA: 0x003BD904 File Offset: 0x003BBB04
			public int BinarySearch(T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				return this.BinarySearch(0, this.Count, item, comparer);
			}

			// Token: 0x0601178A RID: 71562 RVA: 0x003BD915 File Offset: 0x003BBB15
			public int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				return this.Root.BinarySearch(index, count, item, comparer);
			}

			// Token: 0x0601178B RID: 71563 RVA: 0x003BD927 File Offset: 0x003BBB27
			public ImmutableList<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableList<T>.WrapNode(this.Root);
				}
				return this._immutable;
			}

			// Token: 0x0601178C RID: 71564 RVA: 0x003BD948 File Offset: 0x003BBB48
			int IList.Add(object value)
			{
				this.Add((T)((object)value));
				return this.Count - 1;
			}

			// Token: 0x0601178D RID: 71565 RVA: 0x003BD95E File Offset: 0x003BBB5E
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x0601178E RID: 71566 RVA: 0x003BD966 File Offset: 0x003BBB66
			bool IList.Contains(object value)
			{
				return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
			}

			// Token: 0x0601178F RID: 71567 RVA: 0x003BD97E File Offset: 0x003BBB7E
			int IList.IndexOf(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					return this.IndexOf((T)((object)value));
				}
				return -1;
			}

			// Token: 0x06011790 RID: 71568 RVA: 0x003BD996 File Offset: 0x003BBB96
			void IList.Insert(int index, object value)
			{
				this.Insert(index, (T)((object)value));
			}

			// Token: 0x17002EB8 RID: 11960
			// (get) Token: 0x06011791 RID: 71569 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002EB9 RID: 11961
			// (get) Token: 0x06011792 RID: 71570 RVA: 0x0000FA11 File Offset: 0x0000DC11
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06011793 RID: 71571 RVA: 0x003BD9A5 File Offset: 0x003BBBA5
			void IList.Remove(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					this.Remove((T)((object)value));
				}
			}

			// Token: 0x17002EBA RID: 11962
			[Nullable(2)]
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					this[index] = (T)((object)value);
				}
			}

			// Token: 0x06011796 RID: 71574 RVA: 0x003BD9D9 File Offset: 0x003BBBD9
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x17002EBB RID: 11963
			// (get) Token: 0x06011797 RID: 71575 RVA: 0x0000FA11 File Offset: 0x0000DC11
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002EBC RID: 11964
			// (get) Token: 0x06011798 RID: 71576 RVA: 0x003BD9E8 File Offset: 0x003BBBE8
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

			// Token: 0x04006931 RID: 26929
			private ImmutableList<T>.Node _root = ImmutableList<T>.Node.EmptyNode;

			// Token: 0x04006932 RID: 26930
			private ImmutableList<T> _immutable;

			// Token: 0x04006933 RID: 26931
			private int _version;

			// Token: 0x04006934 RID: 26932
			private object _syncRoot;
		}

		// Token: 0x020020A9 RID: 8361
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x06011799 RID: 71577 RVA: 0x003BDA0C File Offset: 0x003BBC0C
			internal Enumerator([Nullable(new byte[] { 1, 0 })] ImmutableList<T>.Node root, [Nullable(new byte[] { 2, 0 })] ImmutableList<T>.Builder builder = null, int startIndex = -1, int count = -1, bool reversed = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(root, "root");
				Requires.Range(startIndex >= -1, "startIndex", null);
				Requires.Range(count >= -1, "count", null);
				Requires.Argument(reversed || count == -1 || ((startIndex == -1) ? 0 : startIndex) + count <= root.Count);
				Requires.Argument(!reversed || count == -1 || ((startIndex == -1) ? (root.Count - 1) : startIndex) - count + 1 >= 0);
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._startIndex = ((startIndex >= 0) ? startIndex : (reversed ? (root.Count - 1) : 0));
				this._count = ((count == -1) ? root.Count : count);
				this._remainingCount = this._count;
				this._reversed = reversed;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (this._count > 0)
				{
					if (!ImmutableList<T>.Enumerator.s_EnumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = ImmutableList<T>.Enumerator.s_EnumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableList<T>.Node>>(root.Height));
					}
					this.ResetStack();
				}
			}

			// Token: 0x17002EBD RID: 11965
			// (get) Token: 0x0601179A RID: 71578 RVA: 0x003BDB61 File Offset: 0x003BBD61
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17002EBE RID: 11966
			// (get) Token: 0x0601179B RID: 71579 RVA: 0x003BDB69 File Offset: 0x003BBD69
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

			// Token: 0x17002EBF RID: 11967
			// (get) Token: 0x0601179C RID: 71580 RVA: 0x003BDB8A File Offset: 0x003BBD8A
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0601179D RID: 71581 RVA: 0x003BDB98 File Offset: 0x003BBD98
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableList<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
					ImmutableList<T>.Enumerator.s_EnumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x0601179E RID: 71582 RVA: 0x003BDBF0 File Offset: 0x003BBDF0
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					if (this._remainingCount > 0 && stack.Count > 0)
					{
						ImmutableList<T>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushNext(this.NextBranch(value));
						this._remainingCount--;
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x0601179F RID: 71583 RVA: 0x003BDC68 File Offset: 0x003BBE68
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._remainingCount = this._count;
				if (this._stack != null)
				{
					this.ResetStack();
				}
			}

			// Token: 0x060117A0 RID: 71584 RVA: 0x003BDCA8 File Offset: 0x003BBEA8
			private void ResetStack()
			{
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
				ImmutableList<T>.Node node = this._root;
				int num = (this._reversed ? (this._root.Count - this._startIndex - 1) : this._startIndex);
				while (!node.IsEmpty && num != this.PreviousBranch(node).Count)
				{
					if (num < this.PreviousBranch(node).Count)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
					else
					{
						num -= this.PreviousBranch(node).Count + 1;
						node = this.NextBranch(node);
					}
				}
				if (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
				}
			}

			// Token: 0x060117A1 RID: 71585 RVA: 0x003BDD5F File Offset: 0x003BBF5F
			private ImmutableList<T>.Node NextBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Right;
				}
				return node.Left;
			}

			// Token: 0x060117A2 RID: 71586 RVA: 0x003BDD76 File Offset: 0x003BBF76
			private ImmutableList<T>.Node PreviousBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Left;
				}
				return node.Right;
			}

			// Token: 0x060117A3 RID: 71587 RVA: 0x003BDD8D File Offset: 0x003BBF8D
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableList<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableList<T>.Enumerator>(this);
				}
			}

			// Token: 0x060117A4 RID: 71588 RVA: 0x003BDDB8 File Offset: 0x003BBFB8
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060117A5 RID: 71589 RVA: 0x003BDDE0 File Offset: 0x003BBFE0
			private void PushNext(ImmutableList<T>.Node node)
			{
				Requires.NotNull<ImmutableList<T>.Node>(node, "node");
				if (!node.IsEmpty)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					while (!node.IsEmpty)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
				}
			}

			// Token: 0x04006935 RID: 26933
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator> s_EnumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator>();

			// Token: 0x04006936 RID: 26934
			private readonly ImmutableList<T>.Builder _builder;

			// Token: 0x04006937 RID: 26935
			private readonly int _poolUserId;

			// Token: 0x04006938 RID: 26936
			private readonly int _startIndex;

			// Token: 0x04006939 RID: 26937
			private readonly int _count;

			// Token: 0x0400693A RID: 26938
			private int _remainingCount;

			// Token: 0x0400693B RID: 26939
			private readonly bool _reversed;

			// Token: 0x0400693C RID: 26940
			private ImmutableList<T>.Node _root;

			// Token: 0x0400693D RID: 26941
			private SecurePooledObject<Stack<RefAsValueType<ImmutableList<T>.Node>>> _stack;

			// Token: 0x0400693E RID: 26942
			private ImmutableList<T>.Node _current;

			// Token: 0x0400693F RID: 26943
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020020AA RID: 8362
		[Nullable(0)]
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060117A7 RID: 71591 RVA: 0x003BDE38 File Offset: 0x003BC038
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060117A8 RID: 71592 RVA: 0x003BDE48 File Offset: 0x003BC048
			private Node(T key, ImmutableList<T>.Node left, ImmutableList<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(left, right);
				this._count = ImmutableList<T>.Node.ParentCount(left, right);
				this._frozen = frozen;
			}

			// Token: 0x17002EC0 RID: 11968
			// (get) Token: 0x060117A9 RID: 71593 RVA: 0x003BDEA8 File Offset: 0x003BC0A8
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17002EC1 RID: 11969
			// (get) Token: 0x060117AA RID: 71594 RVA: 0x003BDEB3 File Offset: 0x003BC0B3
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17002EC2 RID: 11970
			// (get) Token: 0x060117AB RID: 71595 RVA: 0x003BDEBB File Offset: 0x003BC0BB
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableList<T>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002EC3 RID: 11971
			// (get) Token: 0x060117AC RID: 71596 RVA: 0x003BDEBB File Offset: 0x003BC0BB
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002EC4 RID: 11972
			// (get) Token: 0x060117AD RID: 71597 RVA: 0x003BDEC3 File Offset: 0x003BC0C3
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableList<T>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002EC5 RID: 11973
			// (get) Token: 0x060117AE RID: 71598 RVA: 0x003BDEC3 File Offset: 0x003BC0C3
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002EC6 RID: 11974
			// (get) Token: 0x060117AF RID: 71599 RVA: 0x003BDEBB File Offset: 0x003BC0BB
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17002EC7 RID: 11975
			// (get) Token: 0x060117B0 RID: 71600 RVA: 0x003BDEC3 File Offset: 0x003BC0C3
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17002EC8 RID: 11976
			// (get) Token: 0x060117B1 RID: 71601 RVA: 0x003BDECB File Offset: 0x003BC0CB
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17002EC9 RID: 11977
			// (get) Token: 0x060117B2 RID: 71602 RVA: 0x003BDED3 File Offset: 0x003BC0D3
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17002ECA RID: 11978
			// (get) Token: 0x060117B3 RID: 71603 RVA: 0x003BDECB File Offset: 0x003BC0CB
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17002ECB RID: 11979
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

			// Token: 0x060117B5 RID: 71605 RVA: 0x003BDF50 File Offset: 0x003BC150
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

			// Token: 0x060117B6 RID: 71606 RVA: 0x003BDFC2 File Offset: 0x003BC1C2
			[NullableContext(0)]
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return new ImmutableList<T>.Enumerator(this, null, -1, -1, false);
			}

			// Token: 0x060117B7 RID: 71607 RVA: 0x003BDFCE File Offset: 0x003BC1CE
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060117B8 RID: 71608 RVA: 0x003BDFCE File Offset: 0x003BC1CE
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060117B9 RID: 71609 RVA: 0x003BDFDB File Offset: 0x003BC1DB
			[NullableContext(0)]
			internal ImmutableList<T>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0 })] ImmutableList<T>.Builder builder)
			{
				return new ImmutableList<T>.Enumerator(this, builder, -1, -1, false);
			}

			// Token: 0x060117BA RID: 71610 RVA: 0x003BDFE8 File Offset: 0x003BC1E8
			[return: Nullable(new byte[] { 1, 0 })]
			internal static ImmutableList<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableList<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableList<T>.Node node = ImmutableList<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableList<T>.Node node2 = ImmutableList<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableList<T>.Node(items[start + num2], node, node2, true);
			}

			// Token: 0x060117BB RID: 71611 RVA: 0x003BE060 File Offset: 0x003BC260
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Add(T key)
			{
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateLeaf(key);
				}
				ImmutableList<T>.Node node = this._right.Add(key);
				ImmutableList<T>.Node node2 = this.MutateRight(node);
				if (!node2.IsBalanced)
				{
					return node2.BalanceRight();
				}
				return node2;
			}

			// Token: 0x060117BC RID: 71612 RVA: 0x003BE0A4 File Offset: 0x003BC2A4
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Insert(int index, T key)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateLeaf(key);
				}
				if (index <= this._left._count)
				{
					ImmutableList<T>.Node node = this._left.Insert(index, key);
					ImmutableList<T>.Node node2 = this.MutateLeft(node);
					if (!node2.IsBalanced)
					{
						return node2.BalanceLeft();
					}
					return node2;
				}
				else
				{
					ImmutableList<T>.Node node3 = this._right.Insert(index - this._left._count - 1, key);
					ImmutableList<T>.Node node4 = this.MutateRight(node3);
					if (!node4.IsBalanced)
					{
						return node4.BalanceRight();
					}
					return node4;
				}
			}

			// Token: 0x060117BD RID: 71613 RVA: 0x003BE148 File Offset: 0x003BC348
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node AddRange(IEnumerable<T> keys)
			{
				Requires.NotNull<IEnumerable<T>>(keys, "keys");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateRange(keys);
				}
				ImmutableList<T>.Node node = this._right.AddRange(keys);
				ImmutableList<T>.Node node2 = this.MutateRight(node);
				return node2.BalanceMany();
			}

			// Token: 0x060117BE RID: 71614 RVA: 0x003BE18C File Offset: 0x003BC38C
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node InsertRange(int index, IEnumerable<T> keys)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(keys, "keys");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateRange(keys);
				}
				ImmutableList<T>.Node node2;
				if (index <= this._left._count)
				{
					ImmutableList<T>.Node node = this._left.InsertRange(index, keys);
					node2 = this.MutateLeft(node);
				}
				else
				{
					ImmutableList<T>.Node node3 = this._right.InsertRange(index - this._left._count - 1, keys);
					node2 = this.MutateRight(node3);
				}
				return node2.BalanceMany();
			}

			// Token: 0x060117BF RID: 71615 RVA: 0x003BE224 File Offset: 0x003BC424
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node node;
				if (index == this._left._count)
				{
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableList<T>.Node.EmptyNode;
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
						ImmutableList<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						ImmutableList<T>.Node node3 = this._right.RemoveAt(0);
						node = node2.MutateBoth(this._left, node3);
					}
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node node4 = this._left.RemoveAt(index);
					node = this.MutateLeft(node4);
				}
				else
				{
					ImmutableList<T>.Node node5 = this._right.RemoveAt(index - this._left._count - 1);
					node = this.MutateRight(node5);
				}
				if (!node.IsEmpty && !node.IsBalanced)
				{
					return node.Balance();
				}
				return node;
			}

			// Token: 0x060117C0 RID: 71616 RVA: 0x003BE36C File Offset: 0x003BC56C
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				ImmutableList<T>.Node node = this;
				ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(node, null, -1, -1, false);
				try
				{
					int num = 0;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							node = node.RemoveAt(num);
							enumerator.Dispose();
							enumerator = new ImmutableList<T>.Enumerator(node, null, num, -1, false);
						}
						else
						{
							num++;
						}
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				return node;
			}

			// Token: 0x060117C1 RID: 71617 RVA: 0x003BE3EC File Offset: 0x003BC5EC
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node ReplaceAt(int index, T value)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node node;
				if (index == this._left._count)
				{
					node = this.MutateKey(value);
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node node2 = this._left.ReplaceAt(index, value);
					node = this.MutateLeft(node2);
				}
				else
				{
					ImmutableList<T>.Node node3 = this._right.ReplaceAt(index - this._left._count - 1, value);
					node = this.MutateRight(node3);
				}
				return node;
			}

			// Token: 0x060117C2 RID: 71618 RVA: 0x003BE479 File Offset: 0x003BC679
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Reverse()
			{
				return this.Reverse(0, this.Count);
			}

			// Token: 0x060117C3 RID: 71619 RVA: 0x003BE488 File Offset: 0x003BC688
			[return: Nullable(new byte[] { 1, 0 })]
			internal unsafe ImmutableList<T>.Node Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "index", null);
				ImmutableList<T>.Node node = this;
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					T t = *node.ItemRef(i);
					T t2 = *node.ItemRef(num);
					node = node.ReplaceAt(num, t).ReplaceAt(i, t2);
					i++;
					num--;
				}
				return node;
			}

			// Token: 0x060117C4 RID: 71620 RVA: 0x003BE517 File Offset: 0x003BC717
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Sort()
			{
				return this.Sort(Comparer<T>.Default);
			}

			// Token: 0x060117C5 RID: 71621 RVA: 0x003BE524 File Offset: 0x003BC724
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, comparison);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x060117C6 RID: 71622 RVA: 0x003BE568 File Offset: 0x003BC768
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				return this.Sort(0, this.Count, comparer);
			}

			// Token: 0x060117C7 RID: 71623 RVA: 0x003BE578 File Offset: 0x003BC778
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Argument(index + count <= this.Count);
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, index, count, comparer);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x060117C8 RID: 71624 RVA: 0x003BE5EC File Offset: 0x003BC7EC
			internal int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				comparer = comparer ?? Comparer<T>.Default;
				if (this.IsEmpty || count <= 0)
				{
					return ~index;
				}
				int count2 = this._left.Count;
				if (index + count <= count2)
				{
					return this._left.BinarySearch(index, count, item, comparer);
				}
				if (index > count2)
				{
					int num = this._right.BinarySearch(index - count2 - 1, count, item, comparer);
					int num2 = count2 + 1;
					if (num >= 0)
					{
						return num + num2;
					}
					return num - num2;
				}
				else
				{
					int num3 = comparer.Compare(item, this._key);
					if (num3 == 0)
					{
						return count2;
					}
					if (num3 > 0)
					{
						int num4 = count - (count2 - index) - 1;
						int num5 = ((num4 < 0) ? (-1) : this._right.BinarySearch(0, num4, item, comparer));
						int num6 = count2 + 1;
						if (num5 >= 0)
						{
							return num5 + num6;
						}
						return num5 - num6;
					}
					else
					{
						if (index == count2)
						{
							return ~index;
						}
						return this._left.BinarySearch(index, count, item, comparer);
					}
				}
			}

			// Token: 0x060117C9 RID: 71625 RVA: 0x003BE6F2 File Offset: 0x003BC8F2
			internal int IndexOf(T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				return this.IndexOf(item, 0, this.Count, equalityComparer);
			}

			// Token: 0x060117CA RID: 71626 RVA: 0x003BE703 File Offset: 0x003BC903
			internal bool Contains(T item, IEqualityComparer<T> equalityComparer)
			{
				return ImmutableList<T>.Node.Contains(this, item, equalityComparer);
			}

			// Token: 0x060117CB RID: 71627 RVA: 0x003BE710 File Offset: 0x003BC910
			internal int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index++;
					}
				}
				return -1;
			}

			// Token: 0x060117CC RID: 71628 RVA: 0x003BE7D4 File Offset: 0x003BC9D4
			internal int LastIndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && count <= this.Count, "count", null);
				Requires.Argument(index - count + 1 >= 0);
				equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, true))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index--;
					}
				}
				return -1;
			}

			// Token: 0x060117CD RID: 71629 RVA: 0x003BE884 File Offset: 0x003BCA84
			internal void CopyTo(T[] array)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(array.Length >= this.Count, "array", null);
				int num = 0;
				foreach (T t in this)
				{
					array[num++] = t;
				}
			}

			// Token: 0x060117CE RID: 71630 RVA: 0x003BE900 File Offset: 0x003BCB00
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

			// Token: 0x060117CF RID: 71631 RVA: 0x003BE98C File Offset: 0x003BCB8C
			internal void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(arrayIndex + count <= array.Length, "arrayIndex", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						array[arrayIndex++] = t;
					}
				}
			}

			// Token: 0x060117D0 RID: 71632 RVA: 0x003BEA58 File Offset: 0x003BCC58
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

			// Token: 0x060117D1 RID: 71633 RVA: 0x003BEAEC File Offset: 0x003BCCEC
			internal ImmutableList<TOutput>.Node ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
			{
				ImmutableList<TOutput>.Node emptyNode = ImmutableList<TOutput>.Node.EmptyNode;
				if (this.IsEmpty)
				{
					return emptyNode;
				}
				return emptyNode.AddRange(this.Select(converter));
			}

			// Token: 0x060117D2 RID: 71634 RVA: 0x003BEB18 File Offset: 0x003BCD18
			internal bool TrueForAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T t in this)
				{
					if (!match(t))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060117D3 RID: 71635 RVA: 0x003BEB7C File Offset: 0x003BCD7C
			internal bool Exists(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T t in this)
				{
					if (match(t))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060117D4 RID: 71636 RVA: 0x003BEBE0 File Offset: 0x003BCDE0
			[return: Nullable(2)]
			internal T Find(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T t in this)
				{
					if (match(t))
					{
						return t;
					}
				}
				return default(T);
			}

			// Token: 0x060117D5 RID: 71637 RVA: 0x003BEC4C File Offset: 0x003BCE4C
			internal ImmutableList<T> FindAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Empty;
				}
				List<T> list = null;
				foreach (T t in this)
				{
					if (match(t))
					{
						if (list == null)
						{
							list = new List<T>();
						}
						list.Add(t);
					}
				}
				if (list == null)
				{
					return ImmutableList<T>.Empty;
				}
				return ImmutableList.CreateRange<T>(list);
			}

			// Token: 0x060117D6 RID: 71638 RVA: 0x003BECD8 File Offset: 0x003BCED8
			internal int FindIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this.FindIndex(0, this._count, match);
			}

			// Token: 0x060117D7 RID: 71639 RVA: 0x003BECF3 File Offset: 0x003BCEF3
			internal int FindIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0 && startIndex <= this.Count, "startIndex", null);
				return this.FindIndex(startIndex, this.Count - startIndex, match);
			}

			// Token: 0x060117D8 RID: 71640 RVA: 0x003BED30 File Offset: 0x003BCF30
			internal int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(startIndex + count <= this.Count, "count", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, false))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}

			// Token: 0x060117D9 RID: 71641 RVA: 0x003BEDD8 File Offset: 0x003BCFD8
			[return: Nullable(2)]
			internal T FindLast(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, -1, -1, true))
				{
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
				}
				return default(T);
			}

			// Token: 0x060117DA RID: 71642 RVA: 0x003BEE4C File Offset: 0x003BD04C
			internal int FindLastIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (!this.IsEmpty)
				{
					return this.FindLastIndex(this.Count - 1, this.Count, match);
				}
				return -1;
			}

			// Token: 0x060117DB RID: 71643 RVA: 0x003BEE78 File Offset: 0x003BD078
			internal int FindLastIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(startIndex == 0 || startIndex < this.Count, "startIndex", null);
				if (!this.IsEmpty)
				{
					return this.FindLastIndex(startIndex, startIndex + 1, match);
				}
				return -1;
			}

			// Token: 0x060117DC RID: 71644 RVA: 0x003BEED4 File Offset: 0x003BD0D4
			internal int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(startIndex - count + 1 >= 0, "startIndex", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, true))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num--;
					}
				}
				return -1;
			}

			// Token: 0x060117DD RID: 71645 RVA: 0x003BEF80 File Offset: 0x003BD180
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x060117DE RID: 71646 RVA: 0x003BEFA7 File Offset: 0x003BD1A7
			private ImmutableList<T>.Node RotateLeft()
			{
				return this._right.MutateLeft(this.MutateRight(this._right._left));
			}

			// Token: 0x060117DF RID: 71647 RVA: 0x003BEFC5 File Offset: 0x003BD1C5
			private ImmutableList<T>.Node RotateRight()
			{
				return this._left.MutateRight(this.MutateLeft(this._left._right));
			}

			// Token: 0x060117E0 RID: 71648 RVA: 0x003BEFE4 File Offset: 0x003BD1E4
			private ImmutableList<T>.Node DoubleLeft()
			{
				ImmutableList<T>.Node right = this._right;
				ImmutableList<T>.Node left = right._left;
				return left.MutateBoth(this.MutateRight(left._left), right.MutateLeft(left._right));
			}

			// Token: 0x060117E1 RID: 71649 RVA: 0x003BF020 File Offset: 0x003BD220
			private ImmutableList<T>.Node DoubleRight()
			{
				ImmutableList<T>.Node left = this._left;
				ImmutableList<T>.Node right = left._right;
				return right.MutateBoth(left.MutateRight(right._left), this.MutateLeft(right._right));
			}

			// Token: 0x17002ECC RID: 11980
			// (get) Token: 0x060117E2 RID: 71650 RVA: 0x003BF059 File Offset: 0x003BD259
			private int BalanceFactor
			{
				get
				{
					return (int)(this._right._height - this._left._height);
				}
			}

			// Token: 0x17002ECD RID: 11981
			// (get) Token: 0x060117E3 RID: 71651 RVA: 0x003BF072 File Offset: 0x003BD272
			private bool IsRightHeavy
			{
				get
				{
					return this.BalanceFactor >= 2;
				}
			}

			// Token: 0x17002ECE RID: 11982
			// (get) Token: 0x060117E4 RID: 71652 RVA: 0x003BF080 File Offset: 0x003BD280
			private bool IsLeftHeavy
			{
				get
				{
					return this.BalanceFactor <= -2;
				}
			}

			// Token: 0x17002ECF RID: 11983
			// (get) Token: 0x060117E5 RID: 71653 RVA: 0x003BF08F File Offset: 0x003BD28F
			private bool IsBalanced
			{
				get
				{
					return this.BalanceFactor + 1 <= 2;
				}
			}

			// Token: 0x060117E6 RID: 71654 RVA: 0x003BF09F File Offset: 0x003BD29F
			private ImmutableList<T>.Node Balance()
			{
				if (!this.IsLeftHeavy)
				{
					return this.BalanceRight();
				}
				return this.BalanceLeft();
			}

			// Token: 0x060117E7 RID: 71655 RVA: 0x003BF0B6 File Offset: 0x003BD2B6
			private ImmutableList<T>.Node BalanceLeft()
			{
				if (this._left.BalanceFactor <= 0)
				{
					return this.RotateRight();
				}
				return this.DoubleRight();
			}

			// Token: 0x060117E8 RID: 71656 RVA: 0x003BF0D3 File Offset: 0x003BD2D3
			private ImmutableList<T>.Node BalanceRight()
			{
				if (this._right.BalanceFactor >= 0)
				{
					return this.RotateLeft();
				}
				return this.DoubleLeft();
			}

			// Token: 0x060117E9 RID: 71657 RVA: 0x003BF0F0 File Offset: 0x003BD2F0
			private ImmutableList<T>.Node BalanceMany()
			{
				ImmutableList<T>.Node node = this;
				while (!node.IsBalanced)
				{
					if (node.IsRightHeavy)
					{
						node = node.BalanceRight();
						node.MutateLeft(node._left.BalanceMany());
					}
					else
					{
						node = node.BalanceLeft();
						node.MutateRight(node._right.BalanceMany());
					}
				}
				return node;
			}

			// Token: 0x060117EA RID: 71658 RVA: 0x003BF148 File Offset: 0x003BD348
			private ImmutableList<T>.Node MutateBoth(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, left, right, false);
				}
				this._left = left;
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(left, right);
				this._count = ImmutableList<T>.Node.ParentCount(left, right);
				return this;
			}

			// Token: 0x060117EB RID: 71659 RVA: 0x003BF1AC File Offset: 0x003BD3AC
			private ImmutableList<T>.Node MutateLeft(ImmutableList<T>.Node left)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, left, this._right, false);
				}
				this._left = left;
				this._height = ImmutableList<T>.Node.ParentHeight(left, this._right);
				this._count = ImmutableList<T>.Node.ParentCount(left, this._right);
				return this;
			}

			// Token: 0x060117EC RID: 71660 RVA: 0x003BF20C File Offset: 0x003BD40C
			private ImmutableList<T>.Node MutateRight(ImmutableList<T>.Node right)
			{
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, this._left, right, false);
				}
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(this._left, right);
				this._count = ImmutableList<T>.Node.ParentCount(this._left, right);
				return this;
			}

			// Token: 0x060117ED RID: 71661 RVA: 0x003BF26C File Offset: 0x003BD46C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static byte ParentHeight(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return checked(1 + Math.Max(left._height, right._height));
			}

			// Token: 0x060117EE RID: 71662 RVA: 0x003BF282 File Offset: 0x003BD482
			private static int ParentCount(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return 1 + left._count + right._count;
			}

			// Token: 0x060117EF RID: 71663 RVA: 0x003BF293 File Offset: 0x003BD493
			private ImmutableList<T>.Node MutateKey(T key)
			{
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(key, this._left, this._right, false);
				}
				this._key = key;
				return this;
			}

			// Token: 0x060117F0 RID: 71664 RVA: 0x003BF2BC File Offset: 0x003BD4BC
			private static ImmutableList<T>.Node CreateRange(IEnumerable<T> keys)
			{
				ImmutableList<T> immutableList;
				if (ImmutableList<T>.TryCastToImmutableList(keys, out immutableList))
				{
					return immutableList._root;
				}
				IOrderedCollection<T> orderedCollection = keys.AsOrderedCollection<T>();
				return ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x060117F1 RID: 71665 RVA: 0x003BF2EE File Offset: 0x003BD4EE
			private static ImmutableList<T>.Node CreateLeaf(T key)
			{
				return new ImmutableList<T>.Node(key, ImmutableList<T>.Node.EmptyNode, ImmutableList<T>.Node.EmptyNode, false);
			}

			// Token: 0x060117F2 RID: 71666 RVA: 0x003BF301 File Offset: 0x003BD501
			private static bool Contains(ImmutableList<T>.Node node, T value, IEqualityComparer<T> equalityComparer)
			{
				return !node.IsEmpty && (equalityComparer.Equals(value, node._key) || ImmutableList<T>.Node.Contains(node._left, value, equalityComparer) || ImmutableList<T>.Node.Contains(node._right, value, equalityComparer));
			}

			// Token: 0x04006940 RID: 26944
			[Nullable(new byte[] { 1, 0 })]
			internal static readonly ImmutableList<T>.Node EmptyNode = new ImmutableList<T>.Node();

			// Token: 0x04006941 RID: 26945
			private T _key;

			// Token: 0x04006942 RID: 26946
			private bool _frozen;

			// Token: 0x04006943 RID: 26947
			private byte _height;

			// Token: 0x04006944 RID: 26948
			private int _count;

			// Token: 0x04006945 RID: 26949
			private ImmutableList<T>.Node _left;

			// Token: 0x04006946 RID: 26950
			private ImmutableList<T>.Node _right;
		}
	}
}
