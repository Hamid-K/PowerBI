using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003E0 RID: 992
	internal class MinHeap<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060022D1 RID: 8913 RVA: 0x0006B27B File Offset: 0x0006947B
		public MinHeap()
		{
			this.m_data = new List<T>();
			this.m_comparer = Comparer<T>.Default;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0006B299 File Offset: 0x00069499
		public MinHeap(int capacity)
			: this(capacity, Comparer<T>.Default)
		{
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0006B2A7 File Offset: 0x000694A7
		public MinHeap(int capacity, IComparer<T> comparer)
		{
			this.m_data = new List<T>(capacity);
			this.m_comparer = comparer;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0006B2C2 File Offset: 0x000694C2
		public bool IsEmpty
		{
			get
			{
				return this.m_data.Count == 0;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0006B2D2 File Offset: 0x000694D2
		public int Count
		{
			get
			{
				return this.m_data.Count;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0006B2DF File Offset: 0x000694DF
		public int Capacity
		{
			get
			{
				return this.m_data.Capacity;
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x0006B2EC File Offset: 0x000694EC
		private void Swap(int i, int j)
		{
			T t = this.m_data[i];
			this.m_data[i] = this.m_data[j];
			this.m_data[j] = t;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0006B32B File Offset: 0x0006952B
		public T Top
		{
			get
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException("heap is empty");
				}
				return this.m_data[0];
			}
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0006B34C File Offset: 0x0006954C
		public void Add(T item)
		{
			int num = this.m_data.Count;
			this.m_data.Add(item);
			int num2;
			while (num > 0 && this.m_comparer.Compare(item, this.m_data[num2 = (num - 1) / 2]) < 0)
			{
				this.Swap(num, num2);
				num = num2;
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0006B3A4 File Offset: 0x000695A4
		public T Extract()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException("heap is empty");
			}
			T t = this.m_data[0];
			T t2 = (this.m_data[0] = this.m_data[this.m_data.Count - 1]);
			this.m_data.RemoveAt(this.m_data.Count - 1);
			int num = 0;
			for (;;)
			{
				int num2 = (num << 1) + 1;
				int num3;
				if (num2 < this.m_data.Count && this.m_comparer.Compare(t2, this.m_data[num2]) > 0)
				{
					num3 = num2;
				}
				else
				{
					num3 = num;
				}
				int num4 = num2 + 1;
				if (num4 < this.m_data.Count && this.m_comparer.Compare(this.m_data[num3], this.m_data[num4]) > 0)
				{
					num3 = num4;
				}
				if (num3 == num)
				{
					break;
				}
				this.Swap(num, num3);
				num = num3;
			}
			return t;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0006B4A1 File Offset: 0x000696A1
		public void Clear()
		{
			this.m_data.Clear();
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0006B4AE File Offset: 0x000696AE
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_data.GetEnumerator();
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0006B4AE File Offset: 0x000696AE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_data.GetEnumerator();
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0006B4C0 File Offset: 0x000696C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			foreach (T t in this.m_data)
			{
				stringBuilder.AppendFormat("{0} ", t);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length--;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040015CF RID: 5583
		private List<T> m_data;

		// Token: 0x040015D0 RID: 5584
		private IComparer<T> m_comparer;
	}
}
