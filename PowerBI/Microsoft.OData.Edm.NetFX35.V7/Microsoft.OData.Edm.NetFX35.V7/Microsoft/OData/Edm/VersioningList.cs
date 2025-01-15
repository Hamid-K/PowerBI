using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000D RID: 13
	internal abstract class VersioningList<TElement> : IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46
		public abstract int Count { get; }

		// Token: 0x17000004 RID: 4
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

		// Token: 0x06000030 RID: 48 RVA: 0x00002BDE File Offset: 0x00000DDE
		public static VersioningList<TElement> Create()
		{
			return new VersioningList<TElement>.EmptyVersioningList();
		}

		// Token: 0x06000031 RID: 49
		public abstract VersioningList<TElement> Add(TElement value);

		// Token: 0x06000032 RID: 50 RVA: 0x00002BE5 File Offset: 0x00000DE5
		public VersioningList<TElement> RemoveAt(int index)
		{
			if ((ulong)index >= (ulong)((long)this.Count))
			{
				throw new IndexOutOfRangeException();
			}
			return this.RemoveIndexedElement(index);
		}

		// Token: 0x06000033 RID: 51
		public abstract IEnumerator<TElement> GetEnumerator();

		// Token: 0x06000034 RID: 52 RVA: 0x00002BFF File Offset: 0x00000DFF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000035 RID: 53
		protected abstract TElement IndexedElement(int index);

		// Token: 0x06000036 RID: 54
		protected abstract VersioningList<TElement> RemoveIndexedElement(int index);

		// Token: 0x02000208 RID: 520
		internal sealed class EmptyVersioningList : VersioningList<TElement>
		{
			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00008EC3 File Offset: 0x000070C3
			public override int Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06000D76 RID: 3446 RVA: 0x00024B06 File Offset: 0x00022D06
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x06000D77 RID: 3447 RVA: 0x00024B0F File Offset: 0x00022D0F
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.EmptyListEnumerator();
			}

			// Token: 0x06000D78 RID: 3448 RVA: 0x00024B16 File Offset: 0x00022D16
			protected override TElement IndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x00024B16 File Offset: 0x00022D16
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x02000209 RID: 521
		internal sealed class EmptyListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00024B28 File Offset: 0x00022D28
			public TElement Current
			{
				get
				{
					return default(TElement);
				}
			}

			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00024B3E File Offset: 0x00022D3E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x00003C4E File Offset: 0x00001E4E
			public void Dispose()
			{
			}

			// Token: 0x06000D7E RID: 3454 RVA: 0x00008EC3 File Offset: 0x000070C3
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x06000D7F RID: 3455 RVA: 0x00003C4E File Offset: 0x00001E4E
			public void Reset()
			{
			}
		}

		// Token: 0x0200020A RID: 522
		internal sealed class LinkedVersioningList : VersioningList<TElement>
		{
			// Token: 0x06000D81 RID: 3457 RVA: 0x00024B4B File Offset: 0x00022D4B
			public LinkedVersioningList(VersioningList<TElement> preceding, TElement last)
			{
				this.preceding = preceding;
				this.last = last;
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00024B61 File Offset: 0x00022D61
			public override int Count
			{
				get
				{
					return this.preceding.Count + 1;
				}
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00024B70 File Offset: 0x00022D70
			public VersioningList<TElement> Preceding
			{
				get
				{
					return this.preceding;
				}
			}

			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00024B78 File Offset: 0x00022D78
			public TElement Last
			{
				get
				{
					return this.last;
				}
			}

			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00024B80 File Offset: 0x00022D80
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

			// Token: 0x06000D86 RID: 3462 RVA: 0x00024BA7 File Offset: 0x00022DA7
			public override VersioningList<TElement> Add(TElement value)
			{
				if (this.Depth < 5)
				{
					return new VersioningList<TElement>.LinkedVersioningList(this, value);
				}
				return new VersioningList<TElement>.ArrayVersioningList(this, value);
			}

			// Token: 0x06000D87 RID: 3463 RVA: 0x00024BC1 File Offset: 0x00022DC1
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.LinkedListEnumerator(this);
			}

			// Token: 0x06000D88 RID: 3464 RVA: 0x00024BC9 File Offset: 0x00022DC9
			protected override TElement IndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.last;
				}
				return this.preceding.IndexedElement(index);
			}

			// Token: 0x06000D89 RID: 3465 RVA: 0x00024BE9 File Offset: 0x00022DE9
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.preceding;
				}
				return new VersioningList<TElement>.LinkedVersioningList(this.preceding.RemoveIndexedElement(index), this.last);
			}

			// Token: 0x04000754 RID: 1876
			private readonly VersioningList<TElement> preceding;

			// Token: 0x04000755 RID: 1877
			private readonly TElement last;
		}

		// Token: 0x0200020B RID: 523
		internal sealed class LinkedListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x06000D8A RID: 3466 RVA: 0x00024C14 File Offset: 0x00022E14
			public LinkedListEnumerator(VersioningList<TElement>.LinkedVersioningList list)
			{
				this.list = list;
				this.preceding = list.Preceding.GetEnumerator();
			}

			// Token: 0x17000488 RID: 1160
			// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00024C34 File Offset: 0x00022E34
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

			// Token: 0x17000489 RID: 1161
			// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00024C55 File Offset: 0x00022E55
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D8D RID: 3469 RVA: 0x00003C4E File Offset: 0x00001E4E
			public void Dispose()
			{
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x00024C62 File Offset: 0x00022E62
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

			// Token: 0x06000D8F RID: 3471 RVA: 0x00024C83 File Offset: 0x00022E83
			public void Reset()
			{
				this.preceding.Reset();
				this.complete = false;
			}

			// Token: 0x04000756 RID: 1878
			private readonly VersioningList<TElement>.LinkedVersioningList list;

			// Token: 0x04000757 RID: 1879
			private IEnumerator<TElement> preceding;

			// Token: 0x04000758 RID: 1880
			private bool complete;
		}

		// Token: 0x0200020C RID: 524
		internal sealed class ArrayVersioningList : VersioningList<TElement>
		{
			// Token: 0x06000D90 RID: 3472 RVA: 0x00024C98 File Offset: 0x00022E98
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

			// Token: 0x06000D91 RID: 3473 RVA: 0x00024D14 File Offset: 0x00022F14
			private ArrayVersioningList(TElement[] elements)
			{
				this.elements = elements;
			}

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00024D23 File Offset: 0x00022F23
			public override int Count
			{
				get
				{
					return this.elements.Length;
				}
			}

			// Token: 0x06000D93 RID: 3475 RVA: 0x00024D2D File Offset: 0x00022F2D
			public TElement ElementAt(int index)
			{
				return this.elements[index];
			}

			// Token: 0x06000D94 RID: 3476 RVA: 0x00024B06 File Offset: 0x00022D06
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x06000D95 RID: 3477 RVA: 0x00024D3B File Offset: 0x00022F3B
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.ArrayListEnumerator(this);
			}

			// Token: 0x06000D96 RID: 3478 RVA: 0x00024D2D File Offset: 0x00022F2D
			protected override TElement IndexedElement(int index)
			{
				return this.elements[index];
			}

			// Token: 0x06000D97 RID: 3479 RVA: 0x00024D44 File Offset: 0x00022F44
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

			// Token: 0x04000759 RID: 1881
			private readonly TElement[] elements;
		}

		// Token: 0x0200020D RID: 525
		internal sealed class ArrayListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x06000D98 RID: 3480 RVA: 0x00024DA8 File Offset: 0x00022FA8
			public ArrayListEnumerator(VersioningList<TElement>.ArrayVersioningList array)
			{
				this.array = array;
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00024DB8 File Offset: 0x00022FB8
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

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00024DF5 File Offset: 0x00022FF5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x00003C4E File Offset: 0x00001E4E
			public void Dispose()
			{
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x00024E04 File Offset: 0x00023004
			public bool MoveNext()
			{
				int count = this.array.Count;
				if (this.index <= count)
				{
					this.index++;
				}
				return this.index <= count;
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x00024E40 File Offset: 0x00023040
			public void Reset()
			{
				this.index = 0;
			}

			// Token: 0x0400075A RID: 1882
			private readonly VersioningList<TElement>.ArrayVersioningList array;

			// Token: 0x0400075B RID: 1883
			private int index;
		}
	}
}
