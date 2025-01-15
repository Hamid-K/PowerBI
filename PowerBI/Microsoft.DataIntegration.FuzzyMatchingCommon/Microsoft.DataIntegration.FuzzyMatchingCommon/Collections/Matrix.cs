using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	internal sealed class Matrix<CellType>
	{
		// Token: 0x170000E7 RID: 231
		public CellType[] this[int i]
		{
			get
			{
				return this.m_cells[i];
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00022647 File Offset: 0x00020847
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0002264F File Offset: 0x0002084F
		public int Height
		{
			get
			{
				return this.m_height;
			}
			private set
			{
				this.m_height = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00022658 File Offset: 0x00020858
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00022660 File Offset: 0x00020860
		public int Width
		{
			get
			{
				return this.m_width;
			}
			private set
			{
				this.m_width = value;
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002266C File Offset: 0x0002086C
		public void Fill(int iStart, int jStart, int iEnd, int jEnd, CellType value)
		{
			for (int i = iStart; i < iEnd; i++)
			{
				for (int j = jStart; j < jEnd; j++)
				{
					this.m_cells[i][j] = value;
				}
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000226A4 File Offset: 0x000208A4
		public bool Resize(int m, int n)
		{
			bool flag = n > this.m_widthAllocated || m > this.m_heightAllocated;
			if (n > this.m_widthAllocated)
			{
				if (m > this.m_heightAllocated)
				{
					Array.Resize<CellType[]>(ref this.m_cells, m);
					this.m_heightAllocated = m;
				}
				for (int i = 0; i < this.m_heightAllocated; i++)
				{
					if (this.m_cells[i] == null)
					{
						this.m_cells[i] = new CellType[n];
					}
					else
					{
						Array.Resize<CellType>(ref this.m_cells[i], n);
					}
				}
				this.m_widthAllocated = n;
			}
			else if (m > this.m_heightAllocated)
			{
				Array.Resize<CellType[]>(ref this.m_cells, m);
				for (int j = this.m_heightAllocated; j < m; j++)
				{
					this.m_cells[j] = new CellType[this.m_widthAllocated];
				}
				this.m_heightAllocated = m;
			}
			this.Height = m;
			this.Width = n;
			return flag;
		}

		// Token: 0x0400012F RID: 303
		private int m_height;

		// Token: 0x04000130 RID: 304
		private int m_width;

		// Token: 0x04000131 RID: 305
		private int m_heightAllocated;

		// Token: 0x04000132 RID: 306
		private int m_widthAllocated;

		// Token: 0x04000133 RID: 307
		private CellType[][] m_cells = new CellType[0][];
	}
}
