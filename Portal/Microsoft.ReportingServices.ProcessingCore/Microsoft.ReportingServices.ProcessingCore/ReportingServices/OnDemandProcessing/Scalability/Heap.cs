using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000842 RID: 2114
	internal sealed class Heap<K, V> where K : IComparable<K>
	{
		// Token: 0x06007624 RID: 30244 RVA: 0x001E9A3D File Offset: 0x001E7C3D
		public Heap(int capacity)
			: this(capacity, -1)
		{
		}

		// Token: 0x06007625 RID: 30245 RVA: 0x001E9A47 File Offset: 0x001E7C47
		public Heap(int initialCapacity, int maxCapacity)
		{
			this.m_keys = new Heap<K, V>.HeapEntry[initialCapacity];
			this.m_count = 0;
			this.m_insertIndex = 0;
			this.m_maxCapacity = maxCapacity;
		}

		// Token: 0x06007626 RID: 30246 RVA: 0x001E9A70 File Offset: 0x001E7C70
		public void Insert(K key, V value)
		{
			if (this.m_keys.Length == this.m_count)
			{
				if (this.m_count < this.m_maxCapacity || this.m_maxCapacity == -1)
				{
					int num = (int)((double)this.m_keys.Length * 1.5);
					if (this.m_maxCapacity > 0 && num > this.m_maxCapacity)
					{
						num = this.m_maxCapacity;
					}
					Array.Resize<Heap<K, V>.HeapEntry>(ref this.m_keys, num);
				}
				else
				{
					Global.Tracer.Assert(false, "Invalid Operation: Cannot add to heap at maximum capacity");
				}
			}
			int num2 = this.m_count;
			this.m_count++;
			this.m_keys[num2] = new Heap<K, V>.HeapEntry(key, value, this.m_insertIndex);
			this.m_insertIndex++;
			int num3 = (num2 - 1) / 2;
			while (num2 > 0 && this.LessThan(num3, num2))
			{
				this.Swap(num3, num2);
				num2 = num3;
				num3 = (num2 - 1) / 2;
			}
		}

		// Token: 0x06007627 RID: 30247 RVA: 0x001E9B54 File Offset: 0x001E7D54
		public V ExtractMax()
		{
			V v = this.Peek();
			int num = this.m_count - 1;
			this.m_keys[0] = this.m_keys[num];
			this.m_count--;
			this.Heapify(0);
			if (this.m_maxCapacity > 0 && (double)this.m_count < 0.5 * (double)this.m_keys.Length && this.m_keys.Length > 10)
			{
				int num2 = (int)(0.6 * (double)this.m_keys.Length);
				if (num2 < this.m_count)
				{
					num2 = this.m_count;
				}
				if (num2 < 10)
				{
					num2 = 10;
				}
				Array.Resize<Heap<K, V>.HeapEntry>(ref this.m_keys, num2);
			}
			return v;
		}

		// Token: 0x06007628 RID: 30248 RVA: 0x001E9C07 File Offset: 0x001E7E07
		public V Peek()
		{
			if (this.m_count == 0)
			{
				Global.Tracer.Assert(false, "Cannot Peek from empty heap");
			}
			return this.m_keys[0].Value;
		}

		// Token: 0x06007629 RID: 30249 RVA: 0x001E9C34 File Offset: 0x001E7E34
		private void Heapify(int startIndex)
		{
			int num = 2 * startIndex + 1;
			int num2 = num + 1;
			int num3;
			if (num < this.m_count && this.GreaterThan(num, startIndex))
			{
				num3 = num;
			}
			else
			{
				num3 = startIndex;
			}
			if (num2 < this.m_count && this.GreaterThan(num2, num3))
			{
				num3 = num2;
			}
			if (num3 != startIndex)
			{
				this.Swap(num3, startIndex);
				this.Heapify(num3);
			}
		}

		// Token: 0x0600762A RID: 30250 RVA: 0x001E9C8E File Offset: 0x001E7E8E
		private bool GreaterThan(int index1, int index2)
		{
			return this.m_keys[index1].CompareTo(this.m_keys[index2]) > 0;
		}

		// Token: 0x0600762B RID: 30251 RVA: 0x001E9CB0 File Offset: 0x001E7EB0
		private bool LessThan(int index1, int index2)
		{
			return this.m_keys[index1].CompareTo(this.m_keys[index2]) < 0;
		}

		// Token: 0x0600762C RID: 30252 RVA: 0x001E9CD4 File Offset: 0x001E7ED4
		private void Swap(int index1, int index2)
		{
			Heap<K, V>.HeapEntry heapEntry = this.m_keys[index1];
			this.m_keys[index1] = this.m_keys[index2];
			this.m_keys[index2] = heapEntry;
		}

		// Token: 0x170027AA RID: 10154
		// (get) Token: 0x0600762D RID: 30253 RVA: 0x001E9D13 File Offset: 0x001E7F13
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170027AB RID: 10155
		// (get) Token: 0x0600762E RID: 30254 RVA: 0x001E9D1B File Offset: 0x001E7F1B
		public int Capacity
		{
			get
			{
				return this.m_keys.Length;
			}
		}

		// Token: 0x04003BB4 RID: 15284
		private Heap<K, V>.HeapEntry[] m_keys;

		// Token: 0x04003BB5 RID: 15285
		private int m_count;

		// Token: 0x04003BB6 RID: 15286
		private int m_insertIndex;

		// Token: 0x04003BB7 RID: 15287
		private int m_maxCapacity;

		// Token: 0x02000D04 RID: 3332
		private struct HeapEntry : IComparable<Heap<K, V>.HeapEntry>
		{
			// Token: 0x06008E79 RID: 36473 RVA: 0x00244FF4 File Offset: 0x002431F4
			public HeapEntry(K key, V value, int insertIndex)
			{
				this.m_key = key;
				this.m_value = value;
				this.m_insertIndex = insertIndex;
			}

			// Token: 0x17002BB5 RID: 11189
			// (get) Token: 0x06008E7A RID: 36474 RVA: 0x0024500B File Offset: 0x0024320B
			public K Key
			{
				get
				{
					return this.m_key;
				}
			}

			// Token: 0x17002BB6 RID: 11190
			// (get) Token: 0x06008E7B RID: 36475 RVA: 0x00245013 File Offset: 0x00243213
			public V Value
			{
				get
				{
					return this.m_value;
				}
			}

			// Token: 0x06008E7C RID: 36476 RVA: 0x0024501C File Offset: 0x0024321C
			public int CompareTo(Heap<K, V>.HeapEntry other)
			{
				int num = this.m_key.CompareTo(other.m_key);
				if (num == 0)
				{
					num = this.m_insertIndex - other.m_insertIndex;
				}
				return num;
			}

			// Token: 0x04005019 RID: 20505
			private K m_key;

			// Token: 0x0400501A RID: 20506
			private V m_value;

			// Token: 0x0400501B RID: 20507
			private int m_insertIndex;
		}
	}
}
