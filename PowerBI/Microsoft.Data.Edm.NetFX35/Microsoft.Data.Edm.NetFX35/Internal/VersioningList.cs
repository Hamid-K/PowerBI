using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x02000118 RID: 280
	internal abstract class VersioningList<TElement> : IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600054C RID: 1356
		public abstract int Count { get; }

		// Token: 0x17000241 RID: 577
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

		// Token: 0x0600054E RID: 1358 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public static VersioningList<TElement> Create()
		{
			return new VersioningList<TElement>.EmptyVersioningList();
		}

		// Token: 0x0600054F RID: 1359
		public abstract VersioningList<TElement> Add(TElement value);

		// Token: 0x06000550 RID: 1360 RVA: 0x0000D2E3 File Offset: 0x0000B4E3
		public VersioningList<TElement> RemoveAt(int index)
		{
			if ((ulong)index >= (ulong)((long)this.Count))
			{
				throw new IndexOutOfRangeException();
			}
			return this.RemoveIndexedElement(index);
		}

		// Token: 0x06000551 RID: 1361
		public abstract IEnumerator<TElement> GetEnumerator();

		// Token: 0x06000552 RID: 1362 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000553 RID: 1363
		protected abstract TElement IndexedElement(int index);

		// Token: 0x06000554 RID: 1364
		protected abstract VersioningList<TElement> RemoveIndexedElement(int index);

		// Token: 0x02000119 RID: 281
		internal sealed class EmptyVersioningList : VersioningList<TElement>
		{
			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000D30D File Offset: 0x0000B50D
			public override int Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x0000D310 File Offset: 0x0000B510
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x0000D319 File Offset: 0x0000B519
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.EmptyListEnumerator();
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x0000D320 File Offset: 0x0000B520
			protected override TElement IndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x0000D327 File Offset: 0x0000B527
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0200011A RID: 282
		internal sealed class EmptyListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x17000243 RID: 579
			// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000D338 File Offset: 0x0000B538
			public TElement Current
			{
				get
				{
					return default(TElement);
				}
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000D34E File Offset: 0x0000B54E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x0000D35B File Offset: 0x0000B55B
			public void Dispose()
			{
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x0000D35D File Offset: 0x0000B55D
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x0000D360 File Offset: 0x0000B560
			public void Reset()
			{
			}
		}

		// Token: 0x0200011B RID: 283
		internal sealed class LinkedVersioningList : VersioningList<TElement>
		{
			// Token: 0x06000562 RID: 1378 RVA: 0x0000D36A File Offset: 0x0000B56A
			public LinkedVersioningList(VersioningList<TElement> preceding, TElement last)
			{
				this.preceding = preceding;
				this.last = last;
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000D380 File Offset: 0x0000B580
			public override int Count
			{
				get
				{
					return this.preceding.Count + 1;
				}
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000D38F File Offset: 0x0000B58F
			public VersioningList<TElement> Preceding
			{
				get
				{
					return this.preceding;
				}
			}

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000D397 File Offset: 0x0000B597
			public TElement Last
			{
				get
				{
					return this.last;
				}
			}

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
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

			// Token: 0x06000567 RID: 1383 RVA: 0x0000D3C7 File Offset: 0x0000B5C7
			public override VersioningList<TElement> Add(TElement value)
			{
				if (this.Depth < 5)
				{
					return new VersioningList<TElement>.LinkedVersioningList(this, value);
				}
				return new VersioningList<TElement>.ArrayVersioningList(this, value);
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x0000D3E1 File Offset: 0x0000B5E1
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.LinkedListEnumerator(this);
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x0000D3E9 File Offset: 0x0000B5E9
			protected override TElement IndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.last;
				}
				return this.preceding.IndexedElement(index);
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x0000D409 File Offset: 0x0000B609
			protected override VersioningList<TElement> RemoveIndexedElement(int index)
			{
				if (index == this.Count - 1)
				{
					return this.preceding;
				}
				return new VersioningList<TElement>.LinkedVersioningList(this.preceding.RemoveIndexedElement(index), this.last);
			}

			// Token: 0x040001F3 RID: 499
			private readonly VersioningList<TElement> preceding;

			// Token: 0x040001F4 RID: 500
			private readonly TElement last;
		}

		// Token: 0x0200011C RID: 284
		internal sealed class LinkedListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x0600056B RID: 1387 RVA: 0x0000D434 File Offset: 0x0000B634
			public LinkedListEnumerator(VersioningList<TElement>.LinkedVersioningList list)
			{
				this.list = list;
				this.preceding = list.Preceding.GetEnumerator();
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000D454 File Offset: 0x0000B654
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

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000D475 File Offset: 0x0000B675
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x0000D482 File Offset: 0x0000B682
			public void Dispose()
			{
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x0000D484 File Offset: 0x0000B684
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

			// Token: 0x06000570 RID: 1392 RVA: 0x0000D4A5 File Offset: 0x0000B6A5
			public void Reset()
			{
				this.preceding.Reset();
				this.complete = false;
			}

			// Token: 0x040001F5 RID: 501
			private readonly VersioningList<TElement>.LinkedVersioningList list;

			// Token: 0x040001F6 RID: 502
			private IEnumerator<TElement> preceding;

			// Token: 0x040001F7 RID: 503
			private bool complete;
		}

		// Token: 0x0200011D RID: 285
		internal sealed class ArrayVersioningList : VersioningList<TElement>
		{
			// Token: 0x06000571 RID: 1393 RVA: 0x0000D4BC File Offset: 0x0000B6BC
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

			// Token: 0x06000572 RID: 1394 RVA: 0x0000D538 File Offset: 0x0000B738
			private ArrayVersioningList(TElement[] elements)
			{
				this.elements = elements;
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000D547 File Offset: 0x0000B747
			public override int Count
			{
				get
				{
					return this.elements.Length;
				}
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x0000D551 File Offset: 0x0000B751
			public TElement ElementAt(int index)
			{
				return this.elements[index];
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x0000D55F File Offset: 0x0000B75F
			public override VersioningList<TElement> Add(TElement value)
			{
				return new VersioningList<TElement>.LinkedVersioningList(this, value);
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x0000D568 File Offset: 0x0000B768
			public override IEnumerator<TElement> GetEnumerator()
			{
				return new VersioningList<TElement>.ArrayListEnumerator(this);
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x0000D570 File Offset: 0x0000B770
			protected override TElement IndexedElement(int index)
			{
				return this.elements[index];
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x0000D580 File Offset: 0x0000B780
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

			// Token: 0x040001F8 RID: 504
			private readonly TElement[] elements;
		}

		// Token: 0x0200011E RID: 286
		internal sealed class ArrayListEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x06000579 RID: 1401 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
			public ArrayListEnumerator(VersioningList<TElement>.ArrayVersioningList array)
			{
				this.array = array;
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
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

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000D631 File Offset: 0x0000B831
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x0000D63E File Offset: 0x0000B83E
			public void Dispose()
			{
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x0000D640 File Offset: 0x0000B840
			public bool MoveNext()
			{
				int count = this.array.Count;
				if (this.index <= count)
				{
					this.index++;
				}
				return this.index <= count;
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x0000D67C File Offset: 0x0000B87C
			public void Reset()
			{
				this.index = 0;
			}

			// Token: 0x040001F9 RID: 505
			private readonly VersioningList<TElement>.ArrayVersioningList array;

			// Token: 0x040001FA RID: 506
			private int index;
		}
	}
}
