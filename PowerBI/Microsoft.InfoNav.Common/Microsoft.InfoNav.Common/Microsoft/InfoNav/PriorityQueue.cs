using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.InfoNav
{
	// Token: 0x02000021 RID: 33
	[DebuggerDisplay("Count = {Count}")]
	public sealed class PriorityQueue<T>
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00005CAE File Offset: 0x00003EAE
		internal PriorityQueue(IComparer<T> comparer)
			: this(32, comparer)
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005CB9 File Offset: 0x00003EB9
		internal PriorityQueue(int capacity, IComparer<T> comparer)
		{
			this._heap = new T[(capacity <= 0) ? 32 : capacity];
			this._count = 0;
			this._comparer = comparer;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00005CE3 File Offset: 0x00003EE3
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005CEB File Offset: 0x00003EEB
		internal T Top
		{
			get
			{
				return this._heap[0];
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005CFC File Offset: 0x00003EFC
		internal void Push(T value)
		{
			if (this._count == this._heap.Length)
			{
				T[] array = new T[this._count * 2];
				Array.Copy(this._heap, array, this._count);
				this._heap = array;
			}
			int i;
			int num;
			for (i = this._count; i > 0; i = num)
			{
				num = (i - 1) / 2;
				if (this._comparer.Compare(value, this._heap[num]) >= 0)
				{
					break;
				}
				this._heap[i] = this._heap[num];
			}
			this._heap[i] = value;
			this._count++;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005DA4 File Offset: 0x00003FA4
		internal T Pop()
		{
			T t = this._heap[0];
			if (this._count > 1)
			{
				this._heap[0] = this._heap[this._count - 1];
				this.Heapify();
			}
			this._count--;
			return t;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005DF9 File Offset: 0x00003FF9
		internal void ForceReorderTop()
		{
			this.Heapify();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005E01 File Offset: 0x00004001
		internal void Clear()
		{
			this._count = 0;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005E0C File Offset: 0x0000400C
		private void Heapify()
		{
			int num = 0;
			for (;;)
			{
				int num2 = num * 2 + 1;
				int num3;
				if (num2 < this._count && this._comparer.Compare(this._heap[num2], this._heap[num]) < 0)
				{
					num3 = num2;
				}
				else
				{
					num3 = num;
				}
				int num4 = num2 + 1;
				if (num4 < this._count && this._comparer.Compare(this._heap[num4], this._heap[num3]) < 0)
				{
					num3 = num4;
				}
				if (num3 == num)
				{
					break;
				}
				T t = this._heap[num3];
				this._heap[num3] = this._heap[num];
				this._heap[num] = t;
				num = num3;
			}
		}

		// Token: 0x0400004E RID: 78
		private const int DefaultCapacity = 32;

		// Token: 0x0400004F RID: 79
		private readonly IComparer<T> _comparer;

		// Token: 0x04000050 RID: 80
		private T[] _heap;

		// Token: 0x04000051 RID: 81
		private int _count;
	}
}
