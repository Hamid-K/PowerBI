using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C2 RID: 194
	[Serializable]
	internal sealed class UnionFind
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x00020820 File Offset: 0x0001EA20
		public UnionFind()
		{
			this.m_nodes = new UnionFind.UnionFindNode[32];
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00020835 File Offset: 0x0001EA35
		public void MakeSet(int x)
		{
			this.m_nodes[x].parent = -1;
			this.m_nodes[x].rank = 0;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0002085B File Offset: 0x0001EA5B
		public void Reset(int nelements)
		{
			if (nelements > this.m_nodes.Length)
			{
				Array.Resize<UnionFind.UnionFindNode>(ref this.m_nodes, 2 * nelements);
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00020878 File Offset: 0x0001EA78
		public void Union(int x, int y)
		{
			if (x == y)
			{
				return;
			}
			int num = this.Find(x);
			int num2 = this.Find(y);
			if (num == num2)
			{
				return;
			}
			int rank = this.m_nodes[num].rank;
			int rank2 = this.m_nodes[num2].rank;
			if (rank > rank2)
			{
				this.m_nodes[num2].parent = num;
				return;
			}
			if (rank < rank2)
			{
				this.m_nodes[num].parent = num2;
				return;
			}
			this.m_nodes[num2].parent = num;
			this.m_nodes[num].rank = rank + 1;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00020918 File Offset: 0x0001EB18
		public int Find(int x)
		{
			UnionFind.UnionFindNode unionFindNode = this.m_nodes[x];
			if (unionFindNode.parent == -1)
			{
				return x;
			}
			int num = this.Find(unionFindNode.parent);
			this.m_nodes[x].parent = num;
			return num;
		}

		// Token: 0x040002E2 RID: 738
		private const int InitialCapacity = 32;

		// Token: 0x040002E3 RID: 739
		private const int NullCode = -1;

		// Token: 0x040002E4 RID: 740
		private UnionFind.UnionFindNode[] m_nodes;

		// Token: 0x02000174 RID: 372
		[Serializable]
		private struct UnionFindNode
		{
			// Token: 0x040005F5 RID: 1525
			public int rank;

			// Token: 0x040005F6 RID: 1526
			public int parent;
		}
	}
}
