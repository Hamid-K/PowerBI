using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	internal sealed class PrepareNodePtrList
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x000211A1 File Offset: 0x0001F3A1
		public PrepareNodePtrList(int initialCapacity)
		{
			this.m_items = new PrepareNode[initialCapacity];
			this.m_size = 0;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000211BC File Offset: 0x0001F3BC
		public void Add(PrepareNode nd)
		{
			if (this.m_size == this.m_items.Length)
			{
				Array.Resize<PrepareNode>(ref this.m_items, Math.Max(1, (int)(2f * (float)this.m_items.Length)));
			}
			PrepareNode[] items = this.m_items;
			int size = this.m_size;
			this.m_size = size + 1;
			items[size] = nd;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00021213 File Offset: 0x0001F413
		public void Reset()
		{
			this.m_size = 0;
		}

		// Token: 0x17000174 RID: 372
		public PrepareNode this[int i]
		{
			get
			{
				return this.m_items[i];
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00021226 File Offset: 0x0001F426
		public int Length
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x04000300 RID: 768
		private const float GrowthFactor = 2f;

		// Token: 0x04000301 RID: 769
		private PrepareNode[] m_items;

		// Token: 0x04000302 RID: 770
		private int m_size;
	}
}
