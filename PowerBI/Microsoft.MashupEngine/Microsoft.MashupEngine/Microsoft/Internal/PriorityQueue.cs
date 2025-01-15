using System;
using System.Collections.Generic;

namespace Microsoft.Internal
{
	// Token: 0x020001C3 RID: 451
	internal class PriorityQueue<T>
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00011074 File Offset: 0x0000F274
		public PriorityQueue(IComparer<T> comparer)
			: this(16, comparer)
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001107F File Offset: 0x0000F27F
		public PriorityQueue(int initialCapacity, IComparer<T> comparer)
		{
			this.items = new T[initialCapacity];
			this.count = 0;
			this.comparer = comparer;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000110A1 File Offset: 0x0000F2A1
		private PriorityQueue(T[] items, IComparer<T> comparer)
		{
			this.items = items;
			this.count = 0;
			this.comparer = comparer;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x000110BE File Offset: 0x0000F2BE
		public T Min
		{
			get
			{
				return this.items[0];
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x000110CC File Offset: 0x0000F2CC
		public bool IsEmpty
		{
			get
			{
				return this.count == 0;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000110D7 File Offset: 0x0000F2D7
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public T Remove()
		{
			T t = this.items[0];
			int num = 0;
			int num2;
			for (;;)
			{
				num2 = num * 2 + 1;
				int num3 = num2 + 1;
				if (num3 >= this.count)
				{
					break;
				}
				T t2 = this.items[num2];
				T t3 = this.items[num3];
				if (this.comparer.Compare(t2, t3) <= 0)
				{
					this.items[num] = t2;
					num = num2;
				}
				else
				{
					this.items[num] = t3;
					num = num3;
				}
			}
			if (num2 < this.count)
			{
				this.items[num] = this.items[num2];
			}
			else
			{
				PriorityQueue<T>.SiftUp(this.items, this.items[this.count - 1], num, this.comparer);
			}
			this.count--;
			this.items[this.count] = default(T);
			return t;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000111D4 File Offset: 0x0000F3D4
		public void Insert(T item)
		{
			if (this.count == this.items.Length)
			{
				T[] array = new T[this.items.Length * 2];
				Array.Copy(this.items, array, this.items.Length);
				this.items = array;
			}
			int num = this.count;
			this.count = num + 1;
			int num2 = num;
			PriorityQueue<T>.SiftUp(this.items, item, num2, this.comparer);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00011244 File Offset: 0x0000F444
		public T[] ToArray()
		{
			T[] array = new T[this.count];
			Array.Copy(this.items, array, array.Length);
			return array;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00011270 File Offset: 0x0000F470
		private static void SiftUp(T[] items, T item, int index, IComparer<T> comparer)
		{
			items[index] = item;
			while (index != 0)
			{
				int num = (index - 1) / 2;
				T t = items[num];
				if (comparer.Compare(t, item) <= 0)
				{
					break;
				}
				items[num] = item;
				items[index] = t;
				index = num;
			}
		}

		// Token: 0x04000505 RID: 1285
		private T[] items;

		// Token: 0x04000506 RID: 1286
		private int count;

		// Token: 0x04000507 RID: 1287
		private IComparer<T> comparer;
	}
}
