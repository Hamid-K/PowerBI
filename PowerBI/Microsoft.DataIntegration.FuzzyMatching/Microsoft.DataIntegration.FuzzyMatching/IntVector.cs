using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	internal sealed class IntVector
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00020CC1 File Offset: 0x0001EEC1
		public IntVector()
		{
			this.m_items = new int[2];
			this.m_size = 0;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00020CDC File Offset: 0x0001EEDC
		public IntVector(int initialcapacity)
		{
			this.m_items = new int[initialcapacity];
			this.m_size = 0;
		}

		// Token: 0x17000171 RID: 369
		public int this[int index]
		{
			get
			{
				return this.m_items[index];
			}
			set
			{
				if (index >= this.m_items.Length)
				{
					Array.Resize<int>(ref this.m_items, Math.Max(1, (int)(2f * (float)index)));
				}
				this.m_items[index] = value;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00020D34 File Offset: 0x0001EF34
		public void Add(int val)
		{
			if (this.m_size == this.m_items.Length)
			{
				Array.Resize<int>(ref this.m_items, Math.Max(1, (int)(2f * (float)this.m_size)));
			}
			int[] items = this.m_items;
			int size = this.m_size;
			this.m_size = size + 1;
			items[size] = val;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00020D89 File Offset: 0x0001EF89
		public void Clear()
		{
			Array.Clear(this.m_items, 0, this.m_size);
			this.m_size = 0;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public int Count
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x040002ED RID: 749
		private const int InitialCapacity = 2;

		// Token: 0x040002EE RID: 750
		private const float GrowthFactor = 2f;

		// Token: 0x040002EF RID: 751
		public int[] m_items;

		// Token: 0x040002F0 RID: 752
		private int m_size;
	}
}
