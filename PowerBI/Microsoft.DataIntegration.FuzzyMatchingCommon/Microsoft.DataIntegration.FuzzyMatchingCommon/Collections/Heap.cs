using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	public class Heap<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00021747 File Offset: 0x0001F947
		public Heap(Comparison<T> comparison)
		{
			this.m_items = new T[2];
			this.m_comparison = comparison;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00021762 File Offset: 0x0001F962
		public Heap(Comparison<T> comparison, int capacity)
		{
			this.m_items = new T[capacity];
			this.m_comparison = comparison;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0002177D File Offset: 0x0001F97D
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x00021787 File Offset: 0x0001F987
		public int Capacity
		{
			get
			{
				return this.m_items.Length;
			}
			set
			{
				this.Grow(value);
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00021790 File Offset: 0x0001F990
		private void Grow(int capacity)
		{
			if (this.m_items.Length < capacity)
			{
				Array.Resize<T>(ref this.m_items, capacity);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000217A9 File Offset: 0x0001F9A9
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000217B1 File Offset: 0x0001F9B1
		public Comparison<T> Comparison
		{
			get
			{
				return this.m_comparison;
			}
			set
			{
				this.m_comparison = value;
				this.Heapify();
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000217C0 File Offset: 0x0001F9C0
		private static int Parent(int i)
		{
			return (i - 1) / 2;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000217C7 File Offset: 0x0001F9C7
		private static int Left(int i)
		{
			return 2 * i + 1;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000217CE File Offset: 0x0001F9CE
		private static int Right(int i)
		{
			return 2 * i + 2;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000217D8 File Offset: 0x0001F9D8
		public T Pop()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Pop called on an empty Heap.");
			}
			T t = this.m_items[0];
			T[] items = this.m_items;
			int num = 0;
			T[] items2 = this.m_items;
			int num2 = this.m_itemCount - 1;
			this.m_itemCount = num2;
			items[num] = items2[num2];
			this.m_items[this.m_itemCount] = default(T);
			this.Heapify(0);
			return t;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0002184D File Offset: 0x0001FA4D
		public T Top
		{
			get
			{
				if (this.m_itemCount == 0)
				{
					throw new InvalidOperationException("Top called on an empty Heap.");
				}
				return this.m_items[0];
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00021870 File Offset: 0x0001FA70
		public void BulkAdd(T item)
		{
			if (this.m_itemCount == this.m_items.Length)
			{
				this.Grow(this.m_items.Length * 2);
			}
			T[] items = this.m_items;
			int itemCount = this.m_itemCount;
			this.m_itemCount = itemCount + 1;
			items[itemCount] = item;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000218BC File Offset: 0x0001FABC
		public void Add(T item)
		{
			if (this.m_itemCount == this.m_items.Length)
			{
				this.Grow(this.m_items.Length * 2);
			}
			T[] items = this.m_items;
			int itemCount = this.m_itemCount;
			this.m_itemCount = itemCount + 1;
			items[itemCount] = item;
			int num = this.m_itemCount - 1;
			while (num > 0 && this.m_comparison.Invoke(item, this.m_items[Heap<T>.Parent(num)]) > 0)
			{
				this.m_items[num] = this.m_items[Heap<T>.Parent(num)];
				num = Heap<T>.Parent(num);
			}
			this.m_items[num] = item;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00021968 File Offset: 0x0001FB68
		public void Add(T[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				this.BulkAdd(items[i]);
			}
			if (items.Length != 0)
			{
				this.Heapify();
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0002199C File Offset: 0x0001FB9C
		public void Add(T[] items, int m)
		{
			for (int i = 0; i < m; i++)
			{
				this.BulkAdd(items[i]);
			}
			if (m > 0)
			{
				this.Heapify();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000219CC File Offset: 0x0001FBCC
		public void Add(IEnumerable<T> items)
		{
			bool flag = false;
			foreach (T t in items)
			{
				this.BulkAdd(t);
				flag = true;
			}
			if (flag)
			{
				this.Heapify();
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00021A24 File Offset: 0x0001FC24
		public bool Remove(T item)
		{
			int num = this.FindIndex(item);
			if (num >= 0)
			{
				if (this.m_itemCount > 1)
				{
					this.m_items[num] = this.m_items[this.m_itemCount - 1];
				}
				T[] items = this.m_items;
				int num2 = this.m_itemCount - 1;
				this.m_itemCount = num2;
				items[num2] = default(T);
				this.Heapify(num);
			}
			return num >= 0;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00021A98 File Offset: 0x0001FC98
		private void SwapItems(int i, int j)
		{
			T t = this.m_items[i];
			this.m_items[i] = this.m_items[j];
			this.m_items[j] = t;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00021AD8 File Offset: 0x0001FCD8
		public void Heapify()
		{
			for (int i = this.m_itemCount / 2 - 1; i >= 0; i--)
			{
				this.Heapify(i);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00021B04 File Offset: 0x0001FD04
		public void Heapify(int i)
		{
			for (;;)
			{
				int num = 2 * i + 1;
				int num2 = num + 1;
				int num3 = i;
				if (num < this.m_itemCount && this.m_comparison.Invoke(this.m_items[num], this.m_items[i]) > 0)
				{
					num3 = num;
				}
				if (num2 < this.m_itemCount && this.m_comparison.Invoke(this.m_items[num2], this.m_items[num3]) > 0)
				{
					num3 = num2;
				}
				if (num3 == i)
				{
					break;
				}
				this.SwapItems(i, num3);
				i = num3;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00021B90 File Offset: 0x0001FD90
		public int Count
		{
			get
			{
				return this.m_itemCount;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00021B98 File Offset: 0x0001FD98
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00021B9C File Offset: 0x0001FD9C
		public void Clear()
		{
			while (this.m_itemCount > 0)
			{
				T[] items = this.m_items;
				int num = this.m_itemCount - 1;
				this.m_itemCount = num;
				items[num] = default(T);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00021BD9 File Offset: 0x0001FDD9
		public void ClearFast()
		{
			this.m_itemCount = 0;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		protected int FindIndex(T item, int startIndex)
		{
			for (int i = startIndex; i < this.Count; i = Heap<T>.Right(Heap<T>.Parent(i)))
			{
				int num = this.m_comparison.Invoke(item, this.m_items[i]);
				if (num == 0)
				{
					return i;
				}
				if (num < 0)
				{
					i = Heap<T>.Left(i);
					if (i < this.Count)
					{
						continue;
					}
					i = Heap<T>.Right(Heap<T>.Parent(i));
				}
				while ((i & 1) == 0)
				{
					i = Heap<T>.Parent(i);
					if (i == startIndex)
					{
						i = -1;
						break;
					}
				}
			}
			return -1;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00021C61 File Offset: 0x0001FE61
		protected int FindIndex1(T item)
		{
			return this.FindIndex(item, 0);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00021C6C File Offset: 0x0001FE6C
		protected int FindIndex(T item)
		{
			for (int i = 0; i < this.m_itemCount; i++)
			{
				if (this.m_comparison.Invoke(item, this.m_items[i]) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00021CA7 File Offset: 0x0001FEA7
		public bool Contains(T item)
		{
			return this.FindIndex(item, 0) != -1;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00021CB7 File Offset: 0x0001FEB7
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.ConstrainedCopy(this.m_items, 0, array, arrayIndex, this.m_itemCount);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00021CCD File Offset: 0x0001FECD
		public T[] ToArray()
		{
			return Enumerable.ToArray<T>(this.m_items.SubArray(0, this.m_itemCount).OrderBy((T x, T y) => this.Comparison.Invoke(x, y)));
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00021CF7 File Offset: 0x0001FEF7
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_itemCount; i = num + 1)
			{
				yield return this.m_items[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00021D06 File Offset: 0x0001FF06
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000119 RID: 281
		private const int InitialCapacity = 2;

		// Token: 0x0400011A RID: 282
		private const int GrowthFactor = 2;

		// Token: 0x0400011B RID: 283
		private T[] m_items;

		// Token: 0x0400011C RID: 284
		private int m_itemCount;

		// Token: 0x0400011D RID: 285
		private Comparison<T> m_comparison;
	}
}
