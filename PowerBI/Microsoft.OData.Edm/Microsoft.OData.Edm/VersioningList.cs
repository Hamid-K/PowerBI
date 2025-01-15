using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000070 RID: 112
	internal abstract class VersioningList<TElement> : IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000237 RID: 567
		public abstract int Count { get; }

		// Token: 0x17000115 RID: 277
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

		// Token: 0x06000239 RID: 569 RVA: 0x00005682 File Offset: 0x00003882
		public static VersioningList<TElement> Create()
		{
			return new VersioningList<TElement>.EmptyVersioningList();
		}

		// Token: 0x0600023A RID: 570
		public abstract VersioningList<TElement> Add(TElement value);

		// Token: 0x0600023B RID: 571 RVA: 0x00005689 File Offset: 0x00003889
		public VersioningList<TElement> RemoveAt(int index)
		{
			if ((ulong)index >= (ulong)((long)this.Count))
			{
				throw new IndexOutOfRangeException();
			}
			return this.RemoveIndexedElement(index);
		}

		// Token: 0x0600023C RID: 572
		public abstract IEnumerator<TElement> GetEnumerator();

		// Token: 0x0600023D RID: 573 RVA: 0x000056A3 File Offset: 0x000038A3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600023E RID: 574
		protected abstract TElement IndexedElement(int index);

		// Token: 0x0600023F RID: 575
		protected abstract VersioningList<TElement> RemoveIndexedElement(int index);

		// Token: 0x0200021E RID: 542
		internal sealed class EmptyVersioningList : VersioningList<TElement>
		{
			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x06000E46 RID: 3654 RVA: 0x000026A6 File Offset: 0x000008A6
			public override int Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06000E47 RID: 3655 RVA: 0x00026D56 File Offset: 0x00024F56
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x06000E48 RID: 3656 RVA: 0x00026D5F File Offset: 0x00024F5F
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.EmptyListEnumerator();
			}

			// Token: 0x06000E49 RID: 3657 RVA: 0x00026D66 File Offset: 0x00024F66
			protected override TElement IndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06000E4A RID: 3658 RVA: 0x00026D66 File Offset: 0x00024F66
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0200021F RID: 543
		internal sealed class EmptyListEnumerator : IEnumerator<TElement>, IEnumerator, IDisposable
		{
			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00026D78 File Offset: 0x00024F78
			public TElement Current
			{
				get
				{
					return default(TElement);
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00026D8E File Offset: 0x00024F8E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000E4E RID: 3662 RVA: 0x000068B2 File Offset: 0x00004AB2
			public void Dispose()
			{
			}

			// Token: 0x06000E4F RID: 3663 RVA: 0x000026A6 File Offset: 0x000008A6
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x06000E50 RID: 3664 RVA: 0x000068B2 File Offset: 0x00004AB2
			public void Reset()
			{
			}
		}

		// Token: 0x02000220 RID: 544
		internal sealed class LinkedVersioningList : VersioningList<TElement>
		{
			// Token: 0x06000E52 RID: 3666 RVA: 0x00026D9B File Offset: 0x00024F9B
			public LinkedVersioningList(VersioningList<TElement> preceding, TElement last)
			{
				this.preceding = preceding;
				this.last = last;
			}

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00026DB1 File Offset: 0x00024FB1
			public override int Count
			{
				get
				{
					return this.preceding.Count + 1;
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00026DC0 File Offset: 0x00024FC0
			public VersioningList<TElement> Preceding
			{
				get
				{
					return this.preceding;
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00026DC8 File Offset: 0x00024FC8
			public TElement Last
			{
				get
				{
					return this.last;
				}
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00026DD0 File Offset: 0x00024FD0
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

			// Token: 0x06000E57 RID: 3671 RVA: 0x00026DF7 File Offset: 0x00024FF7
			public override VersioningList<TElement> Add(TElement value)
			{
				if (this.Depth < 5)
				{
					return new VersioningList<TElement>.LinkedVersioningList(this, value);
				}
				return new VersioningList<TElement>.ArrayVersioningList(this, value);
			}

			// Token: 0x06000E58 RID: 3672 RVA: 0x00026E11 File Offset: 0x00025011
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.LinkedListEnumerator(this);
			}

			// Token: 0x06000E59 RID: 3673 RVA: 0x00026E19 File Offset: 0x00025019
			protected override TElement IndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.last;
				}
				return this.preceding.IndexedElement(index);
			}

			// Token: 0x06000E5A RID: 3674 RVA: 0x00026E39 File Offset: 0x00025039
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.preceding;
				}
				return new VersioningList<TElement>.LinkedVersioningList(this.preceding.RemoveIndexedElement(index), this.last);
			}

			// Token: 0x040007DB RID: 2011
			private readonly VersioningList<TElement> preceding;

			// Token: 0x040007DC RID: 2012
			private readonly TElement last;
		}

		// Token: 0x02000221 RID: 545
		internal sealed class LinkedListEnumerator : IEnumerator<TElement>, IEnumerator, IDisposable
		{
			// Token: 0x06000E5B RID: 3675 RVA: 0x00026E64 File Offset: 0x00025064
			public LinkedListEnumerator(VersioningList<TElement>.LinkedVersioningList list)
			{
				this.list = list;
				this.preceding = list.Preceding.GetEnumerator();
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00026E84 File Offset: 0x00025084
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

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00026EA5 File Offset: 0x000250A5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000E5E RID: 3678 RVA: 0x000068B2 File Offset: 0x00004AB2
			public void Dispose()
			{
			}

			// Token: 0x06000E5F RID: 3679 RVA: 0x00026EB2 File Offset: 0x000250B2
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

			// Token: 0x06000E60 RID: 3680 RVA: 0x00026ED3 File Offset: 0x000250D3
			public void Reset()
			{
				this.preceding.Reset();
				this.complete = false;
			}

			// Token: 0x040007DD RID: 2013
			private readonly VersioningList<TElement>.LinkedVersioningList list;

			// Token: 0x040007DE RID: 2014
			private IEnumerator<TElement> preceding;

			// Token: 0x040007DF RID: 2015
			private bool complete;
		}

		// Token: 0x02000222 RID: 546
		internal sealed class ArrayVersioningList : VersioningList<TElement>
		{
			// Token: 0x06000E61 RID: 3681 RVA: 0x00026EE8 File Offset: 0x000250E8
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

			// Token: 0x06000E62 RID: 3682 RVA: 0x00026F64 File Offset: 0x00025164
			private ArrayVersioningList(TElement[] elements)
			{
				this.elements = elements;
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00026F73 File Offset: 0x00025173
			public override int Count
			{
				get
				{
					return this.elements.Length;
				}
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x00026F7D File Offset: 0x0002517D
			public TElement ElementAt(int index)
			{
				return this.elements[index];
			}

			// Token: 0x06000E65 RID: 3685 RVA: 0x00026D56 File Offset: 0x00024F56
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x06000E66 RID: 3686 RVA: 0x00026F8B File Offset: 0x0002518B
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.ArrayListEnumerator(this);
			}

			// Token: 0x06000E67 RID: 3687 RVA: 0x00026F7D File Offset: 0x0002517D
			protected override TElement IndexedElement(int index)
			{
				return this.elements[index];
			}

			// Token: 0x06000E68 RID: 3688 RVA: 0x00026F94 File Offset: 0x00025194
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

			// Token: 0x040007E0 RID: 2016
			private readonly TElement[] elements;
		}

		// Token: 0x02000223 RID: 547
		internal sealed class ArrayListEnumerator : IEnumerator<TElement>, IEnumerator, IDisposable
		{
			// Token: 0x06000E69 RID: 3689 RVA: 0x00026FF8 File Offset: 0x000251F8
			public ArrayListEnumerator(VersioningList<TElement>.ArrayVersioningList array)
			{
				this.array = array;
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00027008 File Offset: 0x00025208
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

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00027045 File Offset: 0x00025245
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000E6C RID: 3692 RVA: 0x000068B2 File Offset: 0x00004AB2
			public void Dispose()
			{
			}

			// Token: 0x06000E6D RID: 3693 RVA: 0x00027054 File Offset: 0x00025254
			public bool MoveNext()
			{
				int count = this.array.Count;
				if (this.index <= count)
				{
					this.index++;
				}
				return this.index <= count;
			}

			// Token: 0x06000E6E RID: 3694 RVA: 0x00027090 File Offset: 0x00025290
			public void Reset()
			{
				this.index = 0;
			}

			// Token: 0x040007E1 RID: 2017
			private readonly VersioningList<TElement>.ArrayVersioningList array;

			// Token: 0x040007E2 RID: 2018
			private int index;
		}
	}
}
