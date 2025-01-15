using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	public class PriorityQueue<T> : Heap<T>
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00021D1D File Offset: 0x0001FF1D
		public PriorityQueue(Comparison<T> comparison)
			: base(comparison)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00021D26 File Offset: 0x0001FF26
		public PriorityQueue(Comparison<T> comparison, int capacity)
			: base(comparison, capacity)
		{
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00021D30 File Offset: 0x0001FF30
		public void Enqueue(T item)
		{
			base.Add(item);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00021D39 File Offset: 0x0001FF39
		public T Dequeue()
		{
			return base.Pop();
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00021D41 File Offset: 0x0001FF41
		public bool IsEmpty
		{
			get
			{
				return base.Count == 0;
			}
		}
	}
}
