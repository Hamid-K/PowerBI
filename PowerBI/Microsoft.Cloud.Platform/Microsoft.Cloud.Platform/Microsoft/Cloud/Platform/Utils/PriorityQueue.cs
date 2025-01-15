using System;
using System.Diagnostics;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000269 RID: 617
	[DebuggerDisplay("Count = {Count}")]
	public sealed class PriorityQueue<T>
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x00037DD0 File Offset: 0x00035FD0
		public PriorityQueue(Func<T, T, int> comparerFunc)
			: this(32, comparerFunc)
		{
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00037DDB File Offset: 0x00035FDB
		public PriorityQueue(int capacity, Func<T, T, int> comparerFunc)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(capacity, "capacity");
			ExtendedDiagnostics.EnsureNotNull<Func<T, T, int>>(comparerFunc, "comparerFunc");
			this.m_heap = new T[capacity];
			this.m_count = 0;
			this.m_comparerFunc = comparerFunc;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00037E13 File Offset: 0x00036013
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00037E1B File Offset: 0x0003601B
		internal T Top
		{
			get
			{
				ExtendedDiagnostics.EnsureOperation(this.m_count > 0, "m_count > 0");
				return this.m_heap[0];
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00037E3C File Offset: 0x0003603C
		public void Enqueue(T value)
		{
			if (this.m_count == this.m_heap.Length)
			{
				T[] array = new T[this.m_count * 2];
				Array.Copy(this.m_heap, array, this.m_count);
				this.m_heap = array;
			}
			int i;
			int num;
			for (i = this.m_count; i > 0; i = num)
			{
				num = (i - 1) / 2;
				if (this.m_comparerFunc(value, this.m_heap[num]) >= 0)
				{
					break;
				}
				this.m_heap[i] = this.m_heap[num];
			}
			this.m_heap[i] = value;
			this.m_count++;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00037EE4 File Offset: 0x000360E4
		public T Dequeue()
		{
			ExtendedDiagnostics.EnsureOperation(this.m_count != 0, "m_count != 0");
			T t = this.m_heap[0];
			if (this.m_count > 1)
			{
				this.m_heap[0] = this.m_heap[this.m_count - 1];
				this.Heapify();
			}
			this.m_count--;
			return t;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00037F4C File Offset: 0x0003614C
		public T Peek()
		{
			ExtendedDiagnostics.EnsureOperation(this.m_count != 0, "m_count != 0");
			return this.m_heap[0];
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00037F6D File Offset: 0x0003616D
		public bool IsEmpty
		{
			get
			{
				return this.m_count == 0;
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00037F78 File Offset: 0x00036178
		private void Heapify()
		{
			int num = 0;
			for (;;)
			{
				int num2 = num * 2 + 1;
				int num3;
				if (num2 < this.m_count && this.m_comparerFunc(this.m_heap[num2], this.m_heap[num]) < 0)
				{
					num3 = num2;
				}
				else
				{
					num3 = num;
				}
				int num4 = num2 + 1;
				if (num4 < this.m_count && this.m_comparerFunc(this.m_heap[num4], this.m_heap[num3]) < 0)
				{
					num3 = num4;
				}
				if (num3 == num)
				{
					break;
				}
				T t = this.m_heap[num3];
				this.m_heap[num3] = this.m_heap[num];
				this.m_heap[num] = t;
				num = num3;
			}
		}

		// Token: 0x04000612 RID: 1554
		private const int DefaultCapacity = 32;

		// Token: 0x04000613 RID: 1555
		private readonly Func<T, T, int> m_comparerFunc;

		// Token: 0x04000614 RID: 1556
		private T[] m_heap;

		// Token: 0x04000615 RID: 1557
		private int m_count;
	}
}
