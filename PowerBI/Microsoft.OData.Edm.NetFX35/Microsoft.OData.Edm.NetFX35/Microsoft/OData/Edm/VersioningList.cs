using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000160 RID: 352
	internal abstract class VersioningList<TElement> : IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600068D RID: 1677
		public abstract int Count { get; }

		// Token: 0x170002C7 RID: 711
		public TElement this[int index]
		{
			get
			{
				if ((ulong)index >= (ulong)((long)this.Count))
				{
					throw new IndexOutOfRangeException();
				}
				return this.IndexedElement(index);
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000F888 File Offset: 0x0000DA88
		public static VersioningList<TElement> Create()
		{
			return new VersioningList<TElement>.EmptyVersioningList();
		}

		// Token: 0x06000690 RID: 1680
		public abstract VersioningList<TElement> Add(TElement value);

		// Token: 0x06000691 RID: 1681 RVA: 0x0000F88F File Offset: 0x0000DA8F
		public VersioningList<TElement> RemoveAt(int index)
		{
			if ((ulong)index >= (ulong)((long)this.Count))
			{
				throw new IndexOutOfRangeException();
			}
			return this.RemoveIndexedElement(index);
		}

		// Token: 0x06000692 RID: 1682
		public abstract IEnumerator<TElement> GetEnumerator();

		// Token: 0x06000693 RID: 1683 RVA: 0x0000F8A9 File Offset: 0x0000DAA9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000694 RID: 1684
		protected abstract TElement IndexedElement(int index);

		// Token: 0x06000695 RID: 1685
		protected abstract VersioningList<TElement> RemoveIndexedElement(int index);

		// Token: 0x02000161 RID: 353
		internal sealed class EmptyVersioningList : VersioningList<TElement>
		{
			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000F8B9 File Offset: 0x0000DAB9
			public override int Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x0000F8BC File Offset: 0x0000DABC
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x0000F8C5 File Offset: 0x0000DAC5
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.EmptyListEnumerator();
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0000F8CC File Offset: 0x0000DACC
			protected override TElement IndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x0000F8D3 File Offset: 0x0000DAD3
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x02000162 RID: 354
		internal sealed class EmptyListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
			public TElement Current
			{
				get
				{
					return default(TElement);
				}
			}

			// Token: 0x170002CA RID: 714
			// (get) Token: 0x0600069E RID: 1694 RVA: 0x0000F8FA File Offset: 0x0000DAFA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x0000F907 File Offset: 0x0000DB07
			public void Dispose()
			{
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x0000F909 File Offset: 0x0000DB09
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x0000F90C File Offset: 0x0000DB0C
			public void Reset()
			{
			}
		}

		// Token: 0x02000163 RID: 355
		internal sealed class LinkedVersioningList : VersioningList<TElement>
		{
			// Token: 0x060006A3 RID: 1699 RVA: 0x0000F916 File Offset: 0x0000DB16
			public LinkedVersioningList(VersioningList<TElement> preceding, TElement last)
			{
				this.preceding = preceding;
				this.last = last;
			}

			// Token: 0x170002CB RID: 715
			// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000F92C File Offset: 0x0000DB2C
			public override int Count
			{
				get
				{
					return this.preceding.Count + 1;
				}
			}

			// Token: 0x170002CC RID: 716
			// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000F93B File Offset: 0x0000DB3B
			public VersioningList<TElement> Preceding
			{
				get
				{
					return this.preceding;
				}
			}

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000F943 File Offset: 0x0000DB43
			public TElement Last
			{
				get
				{
					return this.last;
				}
			}

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000F94C File Offset: 0x0000DB4C
			private int Depth
			{
				get
				{
					int num = 0;
					for (VersioningList<TElement>.LinkedVersioningList linkedVersioningList = this; linkedVersioningList != null; linkedVersioningList = linkedVersioningList.Preceding as VersioningList<TElement>.LinkedVersioningList)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0000F973 File Offset: 0x0000DB73
			public override VersioningList<TElement> Add(TElement value)
			{
				if (this.Depth < 5)
				{
					return new VersioningList<TElement>.LinkedVersioningList(this, value);
				}
				return new VersioningList<TElement>.ArrayVersioningList(this, value);
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x0000F98D File Offset: 0x0000DB8D
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.LinkedListEnumerator(this);
			}

			// Token: 0x060006AA RID: 1706 RVA: 0x0000F995 File Offset: 0x0000DB95
			protected override TElement IndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.last;
				}
				return this.preceding.IndexedElement(index);
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x0000F9B5 File Offset: 0x0000DBB5
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.preceding;
				}
				return new VersioningList<TElement>.LinkedVersioningList(this.preceding.RemoveIndexedElement(index), this.last);
			}

			// Token: 0x040002AE RID: 686
			private readonly VersioningList<TElement> preceding;

			// Token: 0x040002AF RID: 687
			private readonly TElement last;
		}

		// Token: 0x02000164 RID: 356
		internal sealed class LinkedListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x060006AC RID: 1708 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
			public LinkedListEnumerator(VersioningList<TElement>.LinkedVersioningList list)
			{
				this.list = list;
				this.preceding = list.Preceding.GetEnumerator();
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x060006AD RID: 1709 RVA: 0x0000FA00 File Offset: 0x0000DC00
			public TElement Current
			{
				get
				{
					if (this.complete)
					{
						return this.list.Last;
					}
					return this.preceding.Current;
				}
			}

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x060006AE RID: 1710 RVA: 0x0000FA21 File Offset: 0x0000DC21
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0000FA2E File Offset: 0x0000DC2E
			public void Dispose()
			{
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x0000FA30 File Offset: 0x0000DC30
			public bool MoveNext()
			{
				if (this.complete)
				{
					return false;
				}
				if (!this.preceding.MoveNext())
				{
					this.complete = true;
				}
				return true;
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x0000FA51 File Offset: 0x0000DC51
			public void Reset()
			{
				this.preceding.Reset();
				this.complete = false;
			}

			// Token: 0x040002B0 RID: 688
			private readonly VersioningList<TElement>.LinkedVersioningList list;

			// Token: 0x040002B1 RID: 689
			private IEnumerator<TElement> preceding;

			// Token: 0x040002B2 RID: 690
			private bool complete;
		}

		// Token: 0x02000165 RID: 357
		internal sealed class ArrayVersioningList : VersioningList<TElement>
		{
			// Token: 0x060006B2 RID: 1714 RVA: 0x0000FA68 File Offset: 0x0000DC68
			public ArrayVersioningList(VersioningList<TElement> preceding, TElement last)
			{
				this.elements = new TElement[preceding.Count + 1];
				int num = 0;
				foreach (TElement telement in preceding)
				{
					this.elements[num++] = telement;
				}
				this.elements[num] = last;
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
			private ArrayVersioningList(TElement[] elements)
			{
				this.elements = elements;
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000FAF3 File Offset: 0x0000DCF3
			public override int Count
			{
				get
				{
					return this.elements.Length;
				}
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x0000FAFD File Offset: 0x0000DCFD
			public TElement ElementAt(int index)
			{
				return this.elements[index];
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x0000FB0B File Offset: 0x0000DD0B
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x0000FB14 File Offset: 0x0000DD14
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.ArrayListEnumerator(this);
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x0000FB1C File Offset: 0x0000DD1C
			protected override TElement IndexedElement(int index)
			{
				return this.elements[index];
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x0000FB2C File Offset: 0x0000DD2C
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				if (this.elements.Length == 1)
				{
					return new VersioningList<TElement>.EmptyVersioningList();
				}
				int num = 0;
				TElement[] array = new TElement[this.elements.Length - 1];
				for (int i = 0; i < this.elements.Length; i++)
				{
					if (i != index)
					{
						array[num++] = this.elements[i];
					}
				}
				return new VersioningList<TElement>.ArrayVersioningList(array);
			}

			// Token: 0x040002B3 RID: 691
			private readonly TElement[] elements;
		}

		// Token: 0x02000166 RID: 358
		internal sealed class ArrayListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x060006BA RID: 1722 RVA: 0x0000FB90 File Offset: 0x0000DD90
			public ArrayListEnumerator(VersioningList<TElement>.ArrayVersioningList array)
			{
				this.array = array;
			}

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
			public TElement Current
			{
				get
				{
					if (this.index <= this.array.Count)
					{
						return this.array.ElementAt(this.index - 1);
					}
					return default(TElement);
				}
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x060006BC RID: 1724 RVA: 0x0000FBDD File Offset: 0x0000DDDD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x0000FBEA File Offset: 0x0000DDEA
			public void Dispose()
			{
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0000FBEC File Offset: 0x0000DDEC
			public bool MoveNext()
			{
				int count = this.array.Count;
				if (this.index <= count)
				{
					this.index++;
				}
				return this.index <= count;
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x0000FC28 File Offset: 0x0000DE28
			public void Reset()
			{
				this.index = 0;
			}

			// Token: 0x040002B4 RID: 692
			private readonly VersioningList<TElement>.ArrayVersioningList array;

			// Token: 0x040002B5 RID: 693
			private int index;
		}
	}
}
