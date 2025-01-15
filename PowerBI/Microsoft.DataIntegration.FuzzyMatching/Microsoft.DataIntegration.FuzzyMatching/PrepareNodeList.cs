using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	internal sealed class PrepareNodeList
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x00021230 File Offset: 0x0001F430
		public PrepareNodeList(int initialCapacity)
		{
			this.m_items = new PrepareNode[initialCapacity];
			for (int i = 0; i < initialCapacity; i++)
			{
				this.m_items[i] = new PrepareNode();
			}
			this.m_size = 0;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00021270 File Offset: 0x0001F470
		public void Add(int tok, int mintok, int minwt)
		{
			if (this.m_size == this.m_items.Length)
			{
				int num = this.m_items.Length;
				int num2 = Math.Max(1, (int)(2f * (float)num));
				Array.Resize<PrepareNode>(ref this.m_items, num2);
				for (int i = num; i < this.m_items.Length; i++)
				{
					this.m_items[i] = new PrepareNode();
				}
			}
			else
			{
				this.m_items[this.m_size].Reset();
			}
			PrepareNode prepareNode = this.m_items[this.m_size];
			prepareNode.Token = tok;
			prepareNode.Id = this.m_size;
			prepareNode.MinTok = mintok;
			prepareNode.MinWt = minwt;
			prepareNode.Valid = false;
			this.m_size++;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00021328 File Offset: 0x0001F528
		public void Reset()
		{
			this.ResetEdges();
			this.m_size = 0;
		}

		// Token: 0x17000176 RID: 374
		public PrepareNode this[int i]
		{
			get
			{
				return this.m_items[i];
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00021341 File Offset: 0x0001F541
		public int Length
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0002134C File Offset: 0x0001F54C
		public void ResetEdges()
		{
			for (int i = 0; i < this.m_size; i++)
			{
				this.m_items[i].ResetEdges();
			}
		}

		// Token: 0x04000303 RID: 771
		private const float GrowthFactor = 2f;

		// Token: 0x04000304 RID: 772
		private PrepareNode[] m_items;

		// Token: 0x04000305 RID: 773
		private int m_size;
	}
}
