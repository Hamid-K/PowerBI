using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A0 RID: 160
	internal class PriorityQueue<T>
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000F57C File Offset: 0x0000D77C
		public long Push(T item, long cost)
		{
			int num = this.m_heapObjects.Count;
			this.m_heapObjects.Add(item);
			this.m_heapCost.Add(cost);
			int num2;
			while (num > 0 && this.m_heapCost[num2 = PriorityQueue<T>.Parent(num)] < cost)
			{
				this.Swap(num, num2);
				num = num2;
			}
			this.m_totalCost += cost;
			return this.m_totalCost;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		public T Pop(out long cost)
		{
			T t = this.m_heapObjects[0];
			cost = this.m_heapCost[0];
			int num = this.m_heapObjects.Count - 1;
			this.Swap(0, num);
			num--;
			int num2 = 0;
			for (;;)
			{
				int num3 = PriorityQueue<T>.LeftChild(num2);
				if (PriorityQueue<T>.LeftChild(num2) > num)
				{
					break;
				}
				int num4 = PriorityQueue<T>.RightChild(num2);
				if (num4 <= num && this.m_heapCost[num3] < this.m_heapCost[num4])
				{
					num3 = num4;
				}
				if (this.m_heapCost[num2] >= this.m_heapCost[num4])
				{
					break;
				}
				this.Swap(num2, num3);
				num2 = num3;
			}
			this.m_heapObjects.RemoveAt(this.m_heapObjects.Count - 1);
			this.m_heapCost.RemoveAt(this.m_heapCost.Count - 1);
			this.m_totalCost -= cost;
			return t;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000F6CE File Offset: 0x0000D8CE
		public long TotalCost
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_totalCost;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000F6D6 File Offset: 0x0000D8D6
		public int Count
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_heapObjects.Count;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000F6E3 File Offset: 0x0000D8E3
		public IEnumerable<T> GetPoppingIterator()
		{
			while (this.Count > 0)
			{
				long num;
				yield return this.Pop(out num);
			}
			yield break;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000F6F3 File Offset: 0x0000D8F3
		private static int Parent(int index)
		{
			return (index - 1) / 2;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000F6FA File Offset: 0x0000D8FA
		private static int LeftChild(int index)
		{
			return 2 * index + 1;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000F701 File Offset: 0x0000D901
		private static int RightChild(int index)
		{
			return 2 * index + 2;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000F708 File Offset: 0x0000D908
		private void Swap(int x, int y)
		{
			T t = this.m_heapObjects[y];
			long num = this.m_heapCost[y];
			this.m_heapObjects[y] = this.m_heapObjects[x];
			this.m_heapCost[y] = this.m_heapCost[x];
			this.m_heapObjects[x] = t;
			this.m_heapCost[x] = num;
		}

		// Token: 0x040002E1 RID: 737
		private long m_totalCost;

		// Token: 0x040002E2 RID: 738
		private readonly List<T> m_heapObjects = new List<T>();

		// Token: 0x040002E3 RID: 739
		private readonly List<long> m_heapCost = new List<long>();
	}
}
