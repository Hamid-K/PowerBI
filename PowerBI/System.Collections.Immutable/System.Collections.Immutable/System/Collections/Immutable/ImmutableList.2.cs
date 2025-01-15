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
	// Token: 0x02000035 RID: 53
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableList<[Nullable(2)] T> : IImmutableList<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IList<T>, ICollection<T>, IList, ICollection, IOrderedCollection<T>, IImmutableListQueries<T>, IStrongEnumerable<T, ImmutableList<T>.Enumerator>
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00006D23 File Offset: 0x00004F23
		internal ImmutableList()
		{
			this._root = ImmutableList<T>.Node.EmptyNode;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006D36 File Offset: 0x00004F36
		private ImmutableList(ImmutableList<T>.Node root)
		{
			Requires.NotNull<ImmutableList<T>.Node>(root, "root");
			root.Freeze();
			this._root = root;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006D56 File Offset: 0x00004F56
		public ImmutableList<T> Clear()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006D5D File Offset: 0x00004F5D
		public int BinarySearch(T item)
		{
			return this.BinarySearch(item, null);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006D67 File Offset: 0x00004F67
		public int BinarySearch(T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006D78 File Offset: 0x00004F78
		public int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return this._root.BinarySearch(index, count, item, comparer);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006D8A File Offset: 0x00004F8A
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006D97 File Offset: 0x00004F97
		IImmutableList<T> IImmutableList<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006D9F File Offset: 0x00004F9F
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006DAC File Offset: 0x00004FAC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00006DAF File Offset: 0x00004FAF
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005D RID: 93
		public T this[int index]
		{
			get
			{
				return this._root[index];
			}
		}

		// Token: 0x1700005E RID: 94
		T IOrderedCollection<T>.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006DC9 File Offset: 0x00004FC9
		[return: Nullable(new byte[] { 1, 0 })]
		public ImmutableList<T>.Builder ToBuilder()
		{
			return new ImmutableList<T>.Builder(this);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public ImmutableList<T> Add(T value)
		{
			ImmutableList<T>.Node node = this._root.Add(value);
			return this.Wrap(node);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006DF8 File Offset: 0x00004FF8
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

		// Token: 0x0600022F RID: 559 RVA: 0x00006E33 File Offset: 0x00005033
		public ImmutableList<T> Insert(int index, T item)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			return this.Wrap(this._root.Insert(index, item));
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006E68 File Offset: 0x00005068
		public ImmutableList<T> InsertRange(int index, IEnumerable<T> items)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableList<T>.Node node = this._root.InsertRange(index, items);
			return this.Wrap(node);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006EB3 File Offset: 0x000050B3
		public ImmutableList<T> Remove(T value)
		{
			return this.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006EC4 File Offset: 0x000050C4
		public ImmutableList<T> Remove(T value, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(value, equalityComparer);
			if (num >= 0)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006EE8 File Offset: 0x000050E8
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

		// Token: 0x06000234 RID: 564 RVA: 0x00006F55 File Offset: 0x00005155
		public ImmutableList<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006F64 File Offset: 0x00005164
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

		// Token: 0x06000236 RID: 566 RVA: 0x00006FF0 File Offset: 0x000051F0
		public ImmutableList<T> RemoveAt(int index)
		{
			Requires.Range(index >= 0 && index < this.Count, "index", null);
			ImmutableList<T>.Node node = this._root.RemoveAt(index);
			return this.Wrap(node);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000702C File Offset: 0x0000522C
		public ImmutableList<T> RemoveAll(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this.Wrap(this._root.RemoveAll(match));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000704B File Offset: 0x0000524B
		public ImmutableList<T> SetItem(int index, T value)
		{
			return this.Wrap(this._root.ReplaceAt(index, value));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007060 File Offset: 0x00005260
		public ImmutableList<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007070 File Offset: 0x00005270
		public ImmutableList<T> Replace(T oldValue, T newValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(oldValue, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return this.SetItem(num, newValue);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000070A2 File Offset: 0x000052A2
		public ImmutableList<T> Reverse()
		{
			return this.Wrap(this._root.Reverse());
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000070B5 File Offset: 0x000052B5
		public ImmutableList<T> Reverse(int index, int count)
		{
			return this.Wrap(this._root.Reverse(index, count));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000070CA File Offset: 0x000052CA
		public ImmutableList<T> Sort()
		{
			return this.Wrap(this._root.Sort());
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000070DD File Offset: 0x000052DD
		public ImmutableList<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			return this.Wrap(this._root.Sort(comparison));
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000070FC File Offset: 0x000052FC
		public ImmutableList<T> Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return this.Wrap(this._root.Sort(comparer));
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007110 File Offset: 0x00005310
		public ImmutableList<T> Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(this._root.Sort(index, count, comparer));
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007170 File Offset: 0x00005370
		public void ForEach(Action<T> action)
		{
			Requires.NotNull<Action<T>>(action, "action");
			foreach (T t in this)
			{
				action(t);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000071CC File Offset: 0x000053CC
		public void CopyTo(T[] array)
		{
			this._root.CopyTo(array);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000071DA File Offset: 0x000053DA
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000071E9 File Offset: 0x000053E9
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this._root.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000071FC File Offset: 0x000053FC
		public ImmutableList<T> GetRange(int index, int count)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007254 File Offset: 0x00005454
		public ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
		{
			Requires.NotNull<Func<T, TOutput>>(converter, "converter");
			return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007272 File Offset: 0x00005472
		public bool Exists(Predicate<T> match)
		{
			return this._root.Exists(match);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007280 File Offset: 0x00005480
		[return: Nullable(2)]
		public T Find(Predicate<T> match)
		{
			return this._root.Find(match);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000728E File Offset: 0x0000548E
		public ImmutableList<T> FindAll(Predicate<T> match)
		{
			return this._root.FindAll(match);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000729C File Offset: 0x0000549C
		public int FindIndex(Predicate<T> match)
		{
			return this._root.FindIndex(match);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000072AA File Offset: 0x000054AA
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, match);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000072B9 File Offset: 0x000054B9
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, count, match);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000072C9 File Offset: 0x000054C9
		[return: Nullable(2)]
		public T FindLast(Predicate<T> match)
		{
			return this._root.FindLast(match);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000072D7 File Offset: 0x000054D7
		public int FindLastIndex(Predicate<T> match)
		{
			return this._root.FindLastIndex(match);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000072E5 File Offset: 0x000054E5
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, match);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000072F4 File Offset: 0x000054F4
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, count, match);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007304 File Offset: 0x00005504
		public int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return this._root.IndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007316 File Offset: 0x00005516
		public int LastIndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return this._root.LastIndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007328 File Offset: 0x00005528
		public bool TrueForAll(Predicate<T> match)
		{
			return this._root.TrueForAll(match);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007336 File Offset: 0x00005536
		public bool Contains(T value)
		{
			return this._root.Contains(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007349 File Offset: 0x00005549
		public int IndexOf(T value)
		{
			return this.IndexOf(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007357 File Offset: 0x00005557
		IImmutableList<T> IImmutableList<T>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007360 File Offset: 0x00005560
		IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
		{
			return this.AddRange(items);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007369 File Offset: 0x00005569
		IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
		{
			return this.Insert(index, item);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007373 File Offset: 0x00005573
		IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
		{
			return this.InsertRange(index, items);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000737D File Offset: 0x0000557D
		IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			return this.Remove(value, equalityComparer);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007387 File Offset: 0x00005587
		IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
		{
			return this.RemoveAll(match);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007390 File Offset: 0x00005590
		IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			return this.RemoveRange(items, equalityComparer);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000739A File Offset: 0x0000559A
		IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
		{
			return this.RemoveRange(index, count);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000073A4 File Offset: 0x000055A4
		IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
		{
			return this.RemoveAt(index);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000073AD File Offset: 0x000055AD
		IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
		{
			return this.SetItem(index, value);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000073B7 File Offset: 0x000055B7
		IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			return this.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000073C4 File Offset: 0x000055C4
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000073F1 File Offset: 0x000055F1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000073FE File Offset: 0x000055FE
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007405 File Offset: 0x00005605
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700005F RID: 95
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

		// Token: 0x06000267 RID: 615 RVA: 0x0000741C File Offset: 0x0000561C
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00007423 File Offset: 0x00005623
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000742A File Offset: 0x0000562A
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000742D File Offset: 0x0000562D
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00007434 File Offset: 0x00005634
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007443 File Offset: 0x00005643
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000744A File Offset: 0x0000564A
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007451 File Offset: 0x00005651
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00007458 File Offset: 0x00005658
		bool IList.Contains(object value)
		{
			return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007470 File Offset: 0x00005670
		int IList.IndexOf(object value)
		{
			if (!ImmutableList<T>.IsCompatibleObject(value))
			{
				return -1;
			}
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00007488 File Offset: 0x00005688
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000748F File Offset: 0x0000568F
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007492 File Offset: 0x00005692
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007495 File Offset: 0x00005695
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000063 RID: 99
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

		// Token: 0x06000277 RID: 631 RVA: 0x000074B1 File Offset: 0x000056B1
		[NullableContext(0)]
		public ImmutableList<T>.Enumerator GetEnumerator()
		{
			return new ImmutableList<T>.Enumerator(this._root, null, -1, -1, false);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000074C2 File Offset: 0x000056C2
		[Nullable(new byte[] { 1, 0 })]
		internal ImmutableList<T>.Node Root
		{
			[return: Nullable(new byte[] { 1, 0 })]
			get
			{
				return this._root;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000074CA File Offset: 0x000056CA
		private static ImmutableList<T> WrapNode(ImmutableList<T>.Node root)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return ImmutableList<T>.Empty;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000074E0 File Offset: 0x000056E0
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

		// Token: 0x0600027B RID: 635 RVA: 0x00007510 File Offset: 0x00005710
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000753D File Offset: 0x0000573D
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

		// Token: 0x0600027D RID: 637 RVA: 0x00007560 File Offset: 0x00005760
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

		// Token: 0x04000026 RID: 38
		public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

		// Token: 0x04000027 RID: 39
		private readonly ImmutableList<T>.Node _root;

		// Token: 0x0200006D RID: 109
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableListBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IOrderedCollection<T>, IImmutableListQueries<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
		{
			// Token: 0x060004BA RID: 1210 RVA: 0x0000C3EA File Offset: 0x0000A5EA
			internal Builder(ImmutableList<T> list)
			{
				Requires.NotNull<ImmutableList<T>>(list, "list");
				this._root = list._root;
				this._immutable = list;
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000C41B File Offset: 0x0000A61B
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000C428 File Offset: 0x0000A628
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000C42B File Offset: 0x0000A62B
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000C433 File Offset: 0x0000A633
			// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000C43B File Offset: 0x0000A63B
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

			// Token: 0x170000EB RID: 235
			public T this[int index]
			{
				get
				{
					return this.Root[index];
				}
				set
				{
					this.Root = this.Root.ReplaceAt(index, value);
				}
			}

			// Token: 0x170000EC RID: 236
			T IOrderedCollection<T>.this[int index]
			{
				get
				{
					return this[index];
				}
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x0000C48E File Offset: 0x0000A68E
			public int IndexOf(T item)
			{
				return this.Root.IndexOf(item, EqualityComparer<T>.Default);
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0000C4A1 File Offset: 0x0000A6A1
			public void Insert(int index, T item)
			{
				this.Root = this.Root.Insert(index, item);
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
			public void RemoveAt(int index)
			{
				this.Root = this.Root.RemoveAt(index);
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x0000C4CA File Offset: 0x0000A6CA
			public void Add(T item)
			{
				this.Root = this.Root.Add(item);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0000C4DE File Offset: 0x0000A6DE
			public void Clear()
			{
				this.Root = ImmutableList<T>.Node.EmptyNode;
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0000C4EB File Offset: 0x0000A6EB
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x0000C4FC File Offset: 0x0000A6FC
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

			// Token: 0x060004CA RID: 1226 RVA: 0x0000C52A File Offset: 0x0000A72A
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x0000C538 File Offset: 0x0000A738
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x0000C545 File Offset: 0x0000A745
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x0000C554 File Offset: 0x0000A754
			public void ForEach(Action<T> action)
			{
				Requires.NotNull<Action<T>>(action, "action");
				foreach (T t in this)
				{
					action(t);
				}
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
			public void CopyTo(T[] array)
			{
				this._root.CopyTo(array);
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x0000C5BE File Offset: 0x0000A7BE
			public void CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x0000C5CD File Offset: 0x0000A7CD
			public void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				this._root.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
			public ImmutableList<T> GetRange(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				return ImmutableList<T>.WrapNode(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x0000C637 File Offset: 0x0000A837
			public ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
			{
				Requires.NotNull<Func<T, TOutput>>(converter, "converter");
				return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x0000C655 File Offset: 0x0000A855
			public bool Exists(Predicate<T> match)
			{
				return this._root.Exists(match);
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x0000C663 File Offset: 0x0000A863
			[return: Nullable(2)]
			public T Find(Predicate<T> match)
			{
				return this._root.Find(match);
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x0000C671 File Offset: 0x0000A871
			public ImmutableList<T> FindAll(Predicate<T> match)
			{
				return this._root.FindAll(match);
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0000C67F File Offset: 0x0000A87F
			public int FindIndex(Predicate<T> match)
			{
				return this._root.FindIndex(match);
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x0000C68D File Offset: 0x0000A88D
			public int FindIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, match);
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x0000C69C File Offset: 0x0000A89C
			public int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, count, match);
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x0000C6AC File Offset: 0x0000A8AC
			[return: Nullable(2)]
			public T FindLast(Predicate<T> match)
			{
				return this._root.FindLast(match);
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x0000C6BA File Offset: 0x0000A8BA
			public int FindLastIndex(Predicate<T> match)
			{
				return this._root.FindLastIndex(match);
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
			public int FindLastIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, match);
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x0000C6D7 File Offset: 0x0000A8D7
			public int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, count, match);
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
			public int IndexOf(T item, int index)
			{
				return this._root.IndexOf(item, index, this.Count - index, EqualityComparer<T>.Default);
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x0000C703 File Offset: 0x0000A903
			public int IndexOf(T item, int index, int count)
			{
				return this._root.IndexOf(item, index, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x0000C718 File Offset: 0x0000A918
			public int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				return this._root.IndexOf(item, index, count, equalityComparer);
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x0000C72A File Offset: 0x0000A92A
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x0000C755 File Offset: 0x0000A955
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x0000C779 File Offset: 0x0000A979
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this._root.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x0000C78E File Offset: 0x0000A98E
			public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				return this._root.LastIndexOf(item, startIndex, count, equalityComparer);
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
			public bool TrueForAll(Predicate<T> match)
			{
				return this._root.TrueForAll(match);
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x0000C7AE File Offset: 0x0000A9AE
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.AddRange(items);
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x0000C7CD File Offset: 0x0000A9CD
			public void InsertRange(int index, IEnumerable<T> items)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.InsertRange(index, items);
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x0000C80C File Offset: 0x0000AA0C
			public int RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				int count = this.Count;
				this.Root = this.Root.RemoveAll(match);
				return count - this.Count;
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x0000C845 File Offset: 0x0000AA45
			public void Reverse()
			{
				this.Reverse(0, this.Count);
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x0000C854 File Offset: 0x0000AA54
			public void Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Reverse(index, count);
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
			public void Sort()
			{
				this.Root = this.Root.Sort();
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				this.Root = this.Root.Sort(comparison);
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x0000C8E3 File Offset: 0x0000AAE3
			public void Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				this.Root = this.Root.Sort(comparer);
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
			public void Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Sort(index, count, comparer);
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x0000C956 File Offset: 0x0000AB56
			public int BinarySearch(T item)
			{
				return this.BinarySearch(item, null);
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x0000C960 File Offset: 0x0000AB60
			public int BinarySearch(T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				return this.BinarySearch(0, this.Count, item, comparer);
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x0000C971 File Offset: 0x0000AB71
			public int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				return this.Root.BinarySearch(index, count, item, comparer);
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x0000C983 File Offset: 0x0000AB83
			public ImmutableList<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableList<T>.WrapNode(this.Root);
				}
				return this._immutable;
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
			int IList.Add(object value)
			{
				this.Add((T)((object)value));
				return this.Count - 1;
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x0000C9BA File Offset: 0x0000ABBA
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x0000C9C2 File Offset: 0x0000ABC2
			bool IList.Contains(object value)
			{
				return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x0000C9DA File Offset: 0x0000ABDA
			int IList.IndexOf(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					return this.IndexOf((T)((object)value));
				}
				return -1;
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x0000C9F2 File Offset: 0x0000ABF2
			void IList.Insert(int index, object value)
			{
				this.Insert(index, (T)((object)value));
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000CA01 File Offset: 0x0000AC01
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000CA04 File Offset: 0x0000AC04
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x0000CA07 File Offset: 0x0000AC07
			void IList.Remove(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					this.Remove((T)((object)value));
				}
			}

			// Token: 0x170000EF RID: 239
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

			// Token: 0x060004FC RID: 1276 RVA: 0x0000CA3B File Offset: 0x0000AC3B
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000CA4A File Offset: 0x0000AC4A
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000CA4D File Offset: 0x0000AC4D
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

			// Token: 0x040000AF RID: 175
			private ImmutableList<T>.Node _root = ImmutableList<T>.Node.EmptyNode;

			// Token: 0x040000B0 RID: 176
			private ImmutableList<T> _immutable;

			// Token: 0x040000B1 RID: 177
			private int _version;

			// Token: 0x040000B2 RID: 178
			private object _syncRoot;
		}

		// Token: 0x0200006E RID: 110
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x060004FF RID: 1279 RVA: 0x0000CA70 File Offset: 0x0000AC70
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

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000CBC5 File Offset: 0x0000ADC5
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000CBCD File Offset: 0x0000ADCD
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

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000CBEE File Offset: 0x0000ADEE
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x0000CBFC File Offset: 0x0000ADFC
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

			// Token: 0x06000504 RID: 1284 RVA: 0x0000CC54 File Offset: 0x0000AE54
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

			// Token: 0x06000505 RID: 1285 RVA: 0x0000CCCC File Offset: 0x0000AECC
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

			// Token: 0x06000506 RID: 1286 RVA: 0x0000CD0C File Offset: 0x0000AF0C
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

			// Token: 0x06000507 RID: 1287 RVA: 0x0000CDC3 File Offset: 0x0000AFC3
			private ImmutableList<T>.Node NextBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Right;
				}
				return node.Left;
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x0000CDDA File Offset: 0x0000AFDA
			private ImmutableList<T>.Node PreviousBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Left;
				}
				return node.Right;
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableList<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableList<T>.Enumerator>(this);
				}
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0000CE1C File Offset: 0x0000B01C
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x0000CE44 File Offset: 0x0000B044
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

			// Token: 0x040000B3 RID: 179
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator> s_EnumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator>();

			// Token: 0x040000B4 RID: 180
			private readonly ImmutableList<T>.Builder _builder;

			// Token: 0x040000B5 RID: 181
			private readonly int _poolUserId;

			// Token: 0x040000B6 RID: 182
			private readonly int _startIndex;

			// Token: 0x040000B7 RID: 183
			private readonly int _count;

			// Token: 0x040000B8 RID: 184
			private int _remainingCount;

			// Token: 0x040000B9 RID: 185
			private readonly bool _reversed;

			// Token: 0x040000BA RID: 186
			private ImmutableList<T>.Node _root;

			// Token: 0x040000BB RID: 187
			private SecurePooledObject<Stack<RefAsValueType<ImmutableList<T>.Node>>> _stack;

			// Token: 0x040000BC RID: 188
			private ImmutableList<T>.Node _current;

			// Token: 0x040000BD RID: 189
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x0200006F RID: 111
		[Nullable(0)]
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600050D RID: 1293 RVA: 0x0000CE9C File Offset: 0x0000B09C
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x0000CEAC File Offset: 0x0000B0AC
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

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000CF0C File Offset: 0x0000B10C
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000CF17 File Offset: 0x0000B117
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000CF1F File Offset: 0x0000B11F
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableList<T>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000CF27 File Offset: 0x0000B127
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000CF2F File Offset: 0x0000B12F
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableList<T>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000CF37 File Offset: 0x0000B137
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000CF3F File Offset: 0x0000B13F
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000CF47 File Offset: 0x0000B147
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000CF4F File Offset: 0x0000B14F
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000CF57 File Offset: 0x0000B157
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000CF5F File Offset: 0x0000B15F
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17000100 RID: 256
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

			// Token: 0x0600051B RID: 1307 RVA: 0x0000CFDA File Offset: 0x0000B1DA
			[NullableContext(0)]
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return new ImmutableList<T>.Enumerator(this, null, -1, -1, false);
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x0000CFF3 File Offset: 0x0000B1F3
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x0000D000 File Offset: 0x0000B200
			[NullableContext(0)]
			internal ImmutableList<T>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0 })] ImmutableList<T>.Builder builder)
			{
				return new ImmutableList<T>.Enumerator(this, builder, -1, -1, false);
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x0000D00C File Offset: 0x0000B20C
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

			// Token: 0x06000520 RID: 1312 RVA: 0x0000D084 File Offset: 0x0000B284
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

			// Token: 0x06000521 RID: 1313 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
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

			// Token: 0x06000522 RID: 1314 RVA: 0x0000D16C File Offset: 0x0000B36C
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

			// Token: 0x06000523 RID: 1315 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
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

			// Token: 0x06000524 RID: 1316 RVA: 0x0000D248 File Offset: 0x0000B448
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

			// Token: 0x06000525 RID: 1317 RVA: 0x0000D390 File Offset: 0x0000B590
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

			// Token: 0x06000526 RID: 1318 RVA: 0x0000D410 File Offset: 0x0000B610
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

			// Token: 0x06000527 RID: 1319 RVA: 0x0000D49D File Offset: 0x0000B69D
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Reverse()
			{
				return this.Reverse(0, this.Count);
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x0000D4AC File Offset: 0x0000B6AC
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "index", null);
				ImmutableList<T>.Node node = this;
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					T t = node[i];
					T t2 = node[num];
					node = node.ReplaceAt(num, t).ReplaceAt(i, t2);
					i++;
					num--;
				}
				return node;
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x0000D531 File Offset: 0x0000B731
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Sort()
			{
				return this.Sort(Comparer<T>.Default);
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x0000D540 File Offset: 0x0000B740
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, comparison);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x0000D584 File Offset: 0x0000B784
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableList<T>.Node Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				return this.Sort(0, this.Count, comparer);
			}

			// Token: 0x0600052C RID: 1324 RVA: 0x0000D594 File Offset: 0x0000B794
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

			// Token: 0x0600052D RID: 1325 RVA: 0x0000D608 File Offset: 0x0000B808
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

			// Token: 0x0600052E RID: 1326 RVA: 0x0000D70E File Offset: 0x0000B90E
			internal int IndexOf(T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				return this.IndexOf(item, 0, this.Count, equalityComparer);
			}

			// Token: 0x0600052F RID: 1327 RVA: 0x0000D71F File Offset: 0x0000B91F
			internal bool Contains(T item, IEqualityComparer<T> equalityComparer)
			{
				return ImmutableList<T>.Node.Contains(this, item, equalityComparer);
			}

			// Token: 0x06000530 RID: 1328 RVA: 0x0000D72C File Offset: 0x0000B92C
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

			// Token: 0x06000531 RID: 1329 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
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

			// Token: 0x06000532 RID: 1330 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
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

			// Token: 0x06000533 RID: 1331 RVA: 0x0000D91C File Offset: 0x0000BB1C
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

			// Token: 0x06000534 RID: 1332 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
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

			// Token: 0x06000535 RID: 1333 RVA: 0x0000DA74 File Offset: 0x0000BC74
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

			// Token: 0x06000536 RID: 1334 RVA: 0x0000DB14 File Offset: 0x0000BD14
			internal ImmutableList<TOutput>.Node ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
			{
				ImmutableList<TOutput>.Node emptyNode = ImmutableList<TOutput>.Node.EmptyNode;
				if (this.IsEmpty)
				{
					return emptyNode;
				}
				return emptyNode.AddRange(this.Select(converter));
			}

			// Token: 0x06000537 RID: 1335 RVA: 0x0000DB40 File Offset: 0x0000BD40
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

			// Token: 0x06000538 RID: 1336 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
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

			// Token: 0x06000539 RID: 1337 RVA: 0x0000DC08 File Offset: 0x0000BE08
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

			// Token: 0x0600053A RID: 1338 RVA: 0x0000DC74 File Offset: 0x0000BE74
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

			// Token: 0x0600053B RID: 1339 RVA: 0x0000DD00 File Offset: 0x0000BF00
			internal int FindIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this.FindIndex(0, this._count, match);
			}

			// Token: 0x0600053C RID: 1340 RVA: 0x0000DD1B File Offset: 0x0000BF1B
			internal int FindIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0 && startIndex <= this.Count, "startIndex", null);
				return this.FindIndex(startIndex, this.Count - startIndex, match);
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x0000DD58 File Offset: 0x0000BF58
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

			// Token: 0x0600053E RID: 1342 RVA: 0x0000DE00 File Offset: 0x0000C000
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

			// Token: 0x0600053F RID: 1343 RVA: 0x0000DE74 File Offset: 0x0000C074
			internal int FindLastIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (!this.IsEmpty)
				{
					return this.FindLastIndex(this.Count - 1, this.Count, match);
				}
				return -1;
			}

			// Token: 0x06000540 RID: 1344 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
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

			// Token: 0x06000541 RID: 1345 RVA: 0x0000DEFC File Offset: 0x0000C0FC
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

			// Token: 0x06000542 RID: 1346 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x0000DFCF File Offset: 0x0000C1CF
			private ImmutableList<T>.Node RotateLeft()
			{
				return this._right.MutateLeft(this.MutateRight(this._right._left));
			}

			// Token: 0x06000544 RID: 1348 RVA: 0x0000DFED File Offset: 0x0000C1ED
			private ImmutableList<T>.Node RotateRight()
			{
				return this._left.MutateRight(this.MutateLeft(this._left._right));
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x0000E00C File Offset: 0x0000C20C
			private ImmutableList<T>.Node DoubleLeft()
			{
				ImmutableList<T>.Node right = this._right;
				ImmutableList<T>.Node left = right._left;
				return left.MutateBoth(this.MutateRight(left._left), right.MutateLeft(left._right));
			}

			// Token: 0x06000546 RID: 1350 RVA: 0x0000E048 File Offset: 0x0000C248
			private ImmutableList<T>.Node DoubleRight()
			{
				ImmutableList<T>.Node left = this._left;
				ImmutableList<T>.Node right = left._right;
				return right.MutateBoth(left.MutateRight(right._left), this.MutateLeft(right._right));
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000E081 File Offset: 0x0000C281
			private int BalanceFactor
			{
				get
				{
					return (int)(this._right._height - this._left._height);
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000E09A File Offset: 0x0000C29A
			private bool IsRightHeavy
			{
				get
				{
					return this.BalanceFactor >= 2;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
			private bool IsLeftHeavy
			{
				get
				{
					return this.BalanceFactor <= -2;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000E0B7 File Offset: 0x0000C2B7
			private bool IsBalanced
			{
				get
				{
					return this.BalanceFactor + 1 <= 2;
				}
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x0000E0C7 File Offset: 0x0000C2C7
			private ImmutableList<T>.Node Balance()
			{
				if (!this.IsLeftHeavy)
				{
					return this.BalanceRight();
				}
				return this.BalanceLeft();
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x0000E0DE File Offset: 0x0000C2DE
			private ImmutableList<T>.Node BalanceLeft()
			{
				if (this._left.BalanceFactor <= 0)
				{
					return this.RotateRight();
				}
				return this.DoubleRight();
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x0000E0FB File Offset: 0x0000C2FB
			private ImmutableList<T>.Node BalanceRight()
			{
				if (this._right.BalanceFactor >= 0)
				{
					return this.RotateLeft();
				}
				return this.DoubleLeft();
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x0000E118 File Offset: 0x0000C318
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

			// Token: 0x0600054F RID: 1359 RVA: 0x0000E170 File Offset: 0x0000C370
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

			// Token: 0x06000550 RID: 1360 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
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

			// Token: 0x06000551 RID: 1361 RVA: 0x0000E234 File Offset: 0x0000C434
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

			// Token: 0x06000552 RID: 1362 RVA: 0x0000E294 File Offset: 0x0000C494
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static byte ParentHeight(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return checked(1 + Math.Max(left._height, right._height));
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x0000E2AA File Offset: 0x0000C4AA
			private static int ParentCount(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return 1 + left._count + right._count;
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x0000E2BB File Offset: 0x0000C4BB
			private ImmutableList<T>.Node MutateKey(T key)
			{
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(key, this._left, this._right, false);
				}
				this._key = key;
				return this;
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
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

			// Token: 0x06000556 RID: 1366 RVA: 0x0000E316 File Offset: 0x0000C516
			private static ImmutableList<T>.Node CreateLeaf(T key)
			{
				return new ImmutableList<T>.Node(key, ImmutableList<T>.Node.EmptyNode, ImmutableList<T>.Node.EmptyNode, false);
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x0000E329 File Offset: 0x0000C529
			private static bool Contains(ImmutableList<T>.Node node, T value, IEqualityComparer<T> equalityComparer)
			{
				return !node.IsEmpty && (equalityComparer.Equals(value, node._key) || ImmutableList<T>.Node.Contains(node._left, value, equalityComparer) || ImmutableList<T>.Node.Contains(node._right, value, equalityComparer));
			}

			// Token: 0x040000BE RID: 190
			[Nullable(new byte[] { 1, 0 })]
			internal static readonly ImmutableList<T>.Node EmptyNode = new ImmutableList<T>.Node();

			// Token: 0x040000BF RID: 191
			private T _key;

			// Token: 0x040000C0 RID: 192
			private bool _frozen;

			// Token: 0x040000C1 RID: 193
			private byte _height;

			// Token: 0x040000C2 RID: 194
			private int _count;

			// Token: 0x040000C3 RID: 195
			private ImmutableList<T>.Node _left;

			// Token: 0x040000C4 RID: 196
			private ImmutableList<T>.Node _right;
		}
	}
}
