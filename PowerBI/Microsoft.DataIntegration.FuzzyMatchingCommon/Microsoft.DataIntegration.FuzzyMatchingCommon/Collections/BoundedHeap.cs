using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public sealed class BoundedHeap<T>
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0001A590 File Offset: 0x00018790
		public BoundedHeap(BoundedHeap<T>.PriorityAccessor accessor, int maxItems)
		{
			this.m_heap = new Heap<T>(delegate(T x, T y)
			{
				double num = accessor(x);
				double num2 = accessor(y);
				if (num == num2)
				{
					return 0;
				}
				if (num <= num2)
				{
					return -1;
				}
				return 1;
			}, maxItems);
			this.MaxItems = maxItems;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001A5CF File Offset: 0x000187CF
		public BoundedHeap(Comparison<T> cmp, int maxItems)
		{
			this.m_heap = new Heap<T>(cmp);
			this.MaxItems = maxItems;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001A5EA File Offset: 0x000187EA
		public void Clear()
		{
			this.m_heap.Clear();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001A5F7 File Offset: 0x000187F7
		public void ClearFast()
		{
			this.m_heap.ClearFast();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001A604 File Offset: 0x00018804
		public T Pop()
		{
			return this.m_heap.Pop();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0001A611 File Offset: 0x00018811
		public int Count
		{
			get
			{
				return this.m_heap.Count;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001A61E File Offset: 0x0001881E
		public T[] ToArray()
		{
			return this.m_heap.ToArray();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001A62B File Offset: 0x0001882B
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0001A633 File Offset: 0x00018833
		public int MaxItems
		{
			get
			{
				return this.m_maxItems;
			}
			set
			{
				while (this.m_heap.Count > value)
				{
					this.m_heap.Pop();
				}
				this.m_maxItems = value;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001A658 File Offset: 0x00018858
		public void Add(T item)
		{
			if (this.m_heap.Count < this.MaxItems)
			{
				this.m_heap.Add(item);
				return;
			}
			if (this.MaxItems > 0 && this.m_heap.Comparison.Invoke(item, this.m_heap.Top) < 0)
			{
				this.m_heap.Pop();
				this.m_heap.Add(item);
			}
		}

		// Token: 0x0400009E RID: 158
		private int m_maxItems;

		// Token: 0x0400009F RID: 159
		private Heap<T> m_heap;

		// Token: 0x020000E7 RID: 231
		// (Invoke) Token: 0x060008EA RID: 2282
		public delegate double PriorityAccessor(T item);

		// Token: 0x020000E8 RID: 232
		// (Invoke) Token: 0x060008EE RID: 2286
		public delegate void OnRemoveHandler(T item);
	}
}
