using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C3 RID: 195
	[Serializable]
	internal sealed class Token2NodeMap : IMemoryUsage
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x00020960 File Offset: 0x0001EB60
		public Token2NodeMap()
		{
			this.m_tok2Nodes = new FastIntToIntHash();
			this.m_columnSizes = new int[16];
			this.m_memoryUsage = 64L;
			this.m_nodes = new int[16][];
			this.m_memoryUsage += 8L;
			for (int i = 0; i < 16; i++)
			{
				this.m_nodes[i] = new int[16];
				this.m_memoryUsage += 72L;
			}
			this.m_lineage = new TransformationMatch[16][];
			this.m_memoryUsage += 8L;
			for (int j = 0; j < 16; j++)
			{
				this.m_lineage[j] = new TransformationMatch[16];
				this.m_memoryUsage += 136L;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00020A28 File Offset: 0x0001EC28
		public void Set(int token, int node, int position, Transformation transformation)
		{
			int num;
			if (this.m_tok2Nodes.TryGetValue(token, out num))
			{
				num--;
			}
			else
			{
				if (this.m_nRows == this.m_nodes.Length)
				{
					int num2 = Math.Max(1, (int)(2f * (float)this.m_nodes.Length));
					this.m_memoryUsage += (long)((num2 - this.m_nodes.Length) * 8);
					Array.Resize<int[]>(ref this.m_nodes, num2);
					for (int i = this.m_nRows; i < num2; i++)
					{
						this.m_nodes[i] = new int[16];
						this.m_memoryUsage += 72L;
					}
					Array.Resize<TransformationMatch[]>(ref this.m_lineage, num2);
					this.m_memoryUsage += (long)((num2 - this.m_nodes.Length) * 8);
					for (int j = this.m_nRows; j < num2; j++)
					{
						this.m_lineage[j] = new TransformationMatch[16];
						this.m_memoryUsage += 136L;
					}
				}
				this.m_tok2Nodes.Add(token, this.m_nRows + 1);
				num = this.m_nRows;
				if (num >= this.m_columnSizes.Length)
				{
					Array.Resize<int>(ref this.m_columnSizes, Math.Max(1, (int)(2f * (float)Math.Max(num, this.m_columnSizes.Length))));
				}
				this.m_columnSizes[num] = 0;
				this.m_nRows++;
			}
			int num3 = this.m_columnSizes[num];
			if (num3 > 0 && this.m_nodes[num][num3 - 1] == node)
			{
				return;
			}
			if (num3 == this.m_nodes[num].Length)
			{
				int num4 = Math.Max(1, (int)(2f * (float)this.m_columnSizes[num]));
				Array.Resize<int>(ref this.m_nodes[num], num4);
				Array.Resize<TransformationMatch>(ref this.m_lineage[num], num4);
			}
			this.m_nodes[num][num3] = node;
			this.m_lineage[num][num3].Position = position;
			this.m_lineage[num][num3].Transformation = transformation;
			this.m_columnSizes[num]++;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00020C3D File Offset: 0x0001EE3D
		public void Reset()
		{
			this.m_tok2Nodes.Clear();
			this.m_nRows = 0;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00020C54 File Offset: 0x0001EE54
		public void GetNodes(int token, out int[] nodeList, out TransformationMatch[] tmList, out int listsize)
		{
			int num;
			if (this.m_tok2Nodes.TryGetValue(token, out num))
			{
				num--;
				nodeList = this.m_nodes[num];
				tmList = this.m_lineage[num];
				listsize = this.m_columnSizes[num];
				return;
			}
			nodeList = null;
			tmList = null;
			listsize = 0;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00020C9F File Offset: 0x0001EE9F
		public bool ContainsToken(int tok)
		{
			return this.m_tok2Nodes.ContainsKey(tok);
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00020CAD File Offset: 0x0001EEAD
		public long MemoryUsage
		{
			get
			{
				return this.m_memoryUsage + this.m_tok2Nodes.MemoryUsage;
			}
		}

		// Token: 0x040002E5 RID: 741
		private const int InitialCapacity = 16;

		// Token: 0x040002E6 RID: 742
		private const float GrowthFactor = 2f;

		// Token: 0x040002E7 RID: 743
		private FastIntToIntHash m_tok2Nodes;

		// Token: 0x040002E8 RID: 744
		private int[][] m_nodes;

		// Token: 0x040002E9 RID: 745
		private TransformationMatch[][] m_lineage;

		// Token: 0x040002EA RID: 746
		private int m_nRows;

		// Token: 0x040002EB RID: 747
		private int[] m_columnSizes;

		// Token: 0x040002EC RID: 748
		private long m_memoryUsage;
	}
}
