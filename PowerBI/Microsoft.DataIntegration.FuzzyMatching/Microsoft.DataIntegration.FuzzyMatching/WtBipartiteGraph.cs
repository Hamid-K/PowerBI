using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	internal sealed class WtBipartiteGraph
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00020188 File Offset: 0x0001E388
		public WtBipartiteGraph(int capacity)
		{
			this.m_lhsNodes = new WtNode[capacity];
			this.m_rhsNodes = new WtNode[capacity];
			this.m_mindelta = new int[capacity];
			for (int i = 0; i < capacity; i++)
			{
				this.m_mindelta[i] = -1;
				this.m_lhsNodes[i] = new WtNode();
				this.m_rhsNodes[i] = new WtNode();
			}
			this.m_numNodes = 0;
			this.m_shadowG = new BipartiteGraph(capacity);
			this.m_uf = new UnionFind();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0002020C File Offset: 0x0001E40C
		public void Reset(int n, FuzzyComparisonType comparisonType)
		{
			if (n > this.m_lhsNodes.Length)
			{
				int num = Math.Max(1, (int)(2f * (float)n));
				int num2 = this.m_lhsNodes.Length;
				Array.Resize<WtNode>(ref this.m_lhsNodes, num);
				Array.Resize<WtNode>(ref this.m_rhsNodes, num);
				Array.Resize<int>(ref this.m_mindelta, (int)(2f * (float)n));
				for (int i = num2; i < num; i++)
				{
					this.m_lhsNodes[i] = new WtNode();
					this.m_rhsNodes[i] = new WtNode();
				}
			}
			this.m_numNodes = n;
			for (int j = 0; j < this.m_numNodes; j++)
			{
				this.m_lhsNodes[j].Reset(n);
				this.m_rhsNodes[j].Reset(n);
				this.m_mindelta[j] = -1;
			}
			this.m_shadowG.Reset(n, n);
			this.m_uf.Reset(2 * n);
			if (comparisonType == FuzzyComparisonType.Jaccard)
			{
				this.m_wtModifierType = WtModifierType.Jaccard;
				return;
			}
			if (comparisonType == FuzzyComparisonType.LeftJaccardContainment)
			{
				this.m_wtModifierType = WtModifierType.LeftJaccardContainment;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000202FB File Offset: 0x0001E4FB
		public void SetPrepareNode(bool lhs, int nodeid, PrepareNode nd)
		{
			this.m_shadowG.SetPrepareNode(lhs, nodeid, nd);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0002030B File Offset: 0x0001E50B
		public void AddEdge(int lhsNodeId, int rhsNodeId, int ewt)
		{
			this.m_lhsNodes[lhsNodeId].AddNeighbor(rhsNodeId, ewt);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002031C File Offset: 0x0001E51C
		public void AddMirrorEdge(int lhsNodeId, int rhsNodeId, int ewt)
		{
			this.m_lhsNodes[lhsNodeId].AddMirrorNeighbor(rhsNodeId, ewt);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0002032D File Offset: 0x0001E52D
		public int NumNodes
		{
			get
			{
				return this.m_numNodes;
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00020335 File Offset: 0x0001E535
		public int FindMaxMatching(double threshold, int maxwt)
		{
			this.ConnectedComponents();
			this.CompleteEdges();
			return this.UpdateMaxMatching(threshold, maxwt);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0002034B File Offset: 0x0001E54B
		public int UpdateMaxMatching(double threshold, int maxwt)
		{
			this.ModifyEdgeWts(threshold, maxwt);
			return this.UpdateMaxMatching();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002035C File Offset: 0x0001E55C
		private int UpdateMaxMatching()
		{
			this.InitNodeWts();
			this.InitShadowGEdges();
			bool flag = this.m_shadowG.UpdateMaxMatching() == this.m_numNodes;
			while (!flag)
			{
				this.Delta();
				this.UpdateNodeWeights();
				this.InitShadowGEdges();
				flag = this.m_shadowG.UpdateMaxMatching() == this.m_numNodes;
			}
			return this.ComputeMatchWt();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000203BC File Offset: 0x0001E5BC
		private void ModifyEdgeWts(double threshold, int maxwt)
		{
			for (int i = 0; i < this.m_numNodes; i++)
			{
				this.m_lhsNodes[i].ResetNeighborWts(threshold, maxwt, this.m_wtModifierType);
			}
			this.m_shadowG.ResetEdges();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000203FC File Offset: 0x0001E5FC
		private void CompleteEdges()
		{
			for (int i = 0; i < this.m_numNodes; i++)
			{
				for (int j = 0; j < this.m_numNodes; j++)
				{
					if (this.m_rhsNodes[j].ComponentId == this.m_lhsNodes[i].ComponentId)
					{
						this.m_lhsNodes[i].AddZeroNeighbor(j);
					}
				}
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00020458 File Offset: 0x0001E658
		private void InitNodeWts()
		{
			for (int i = 0; i < this.m_numNodes; i++)
			{
				this.m_rhsNodes[i].Wt = 0;
			}
			for (int j = 0; j < this.m_numNodes; j++)
			{
				WtNode wtNode = this.m_lhsNodes[j];
				int numNeighbors = wtNode.NumNeighbors;
				int[] neighborWts = wtNode.NeighborWts;
				int num = 0;
				for (int k = 0; k < numNeighbors; k++)
				{
					if (neighborWts[k] > num)
					{
						num = neighborWts[k];
					}
				}
				this.m_lhsNodes[j].Wt = num;
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000204DC File Offset: 0x0001E6DC
		private void InitShadowGEdges()
		{
			for (int i = 0; i < this.m_numNodes; i++)
			{
				WtNode wtNode = this.m_lhsNodes[i];
				this.m_shadowG.ResetEdgesRetainMatch(i);
				int numNeighbors = wtNode.NumNeighbors;
				int[] neighborIds = wtNode.NeighborIds;
				int[] neighborWts = wtNode.NeighborWts;
				for (int j = 0; j < numNeighbors; j++)
				{
					int num = neighborIds[j];
					int num2 = neighborWts[j];
					if (wtNode.Wt + this.m_rhsNodes[num].Wt == num2)
					{
						this.m_shadowG.AddEdge(i, num);
					}
				}
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0002056C File Offset: 0x0001E76C
		private int ComputeMatchWt()
		{
			int num = 0;
			for (int i = 0; i < this.m_numNodes; i++)
			{
				num += this.m_lhsNodes[i].Wt;
			}
			for (int j = 0; j < this.m_numNodes; j++)
			{
				num += this.m_rhsNodes[j].Wt;
			}
			return num;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000205C0 File Offset: 0x0001E7C0
		private void Delta()
		{
			for (int i = 0; i < this.m_numNodes; i++)
			{
				this.m_mindelta[i] = -1;
			}
			for (int j = 0; j < this.m_numNodes; j++)
			{
				if (this.m_shadowG.LhsNodeVisited(j))
				{
					WtNode wtNode = this.m_lhsNodes[j];
					int componentId = wtNode.ComponentId;
					int numNeighbors = wtNode.NumNeighbors;
					int[] neighborIds = wtNode.NeighborIds;
					int[] neighborWts = wtNode.NeighborWts;
					for (int k = 0; k < numNeighbors; k++)
					{
						int num = neighborIds[k];
						if (!this.m_shadowG.RhsNodeVisited(num))
						{
							int num2 = neighborWts[k];
							int num3 = wtNode.Wt + this.m_rhsNodes[num].Wt - num2;
							if (this.m_mindelta[componentId] > num3 || this.m_mindelta[componentId] == -1)
							{
								this.m_mindelta[componentId] = num3;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000206A0 File Offset: 0x0001E8A0
		private void UpdateNodeWeights()
		{
			for (int i = 0; i < this.m_numNodes; i++)
			{
				if (this.m_shadowG.LhsNodeVisited(i))
				{
					this.m_lhsNodes[i].Wt = this.m_lhsNodes[i].Wt - this.m_mindelta[this.m_lhsNodes[i].ComponentId];
				}
				if (this.m_shadowG.RhsNodeVisited(i))
				{
					this.m_rhsNodes[i].Wt = this.m_rhsNodes[i].Wt + this.m_mindelta[this.m_rhsNodes[i].ComponentId];
				}
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002073C File Offset: 0x0001E93C
		private void ConnectedComponents()
		{
			for (int i = 0; i < this.m_numNodes; i++)
			{
				this.m_uf.MakeSet(i);
				this.m_uf.MakeSet(i + this.m_numNodes);
			}
			for (int j = 0; j < this.m_numNodes; j++)
			{
				WtNode wtNode = this.m_lhsNodes[j];
				int numNeighbors = wtNode.NumNeighbors;
				int[] neighborIds = wtNode.NeighborIds;
				for (int k = 0; k < numNeighbors; k++)
				{
					this.m_uf.Union(j, neighborIds[k] + this.m_numNodes);
				}
			}
			for (int l = 0; l < this.m_numNodes; l++)
			{
				int num = this.m_uf.Find(l);
				this.m_lhsNodes[l].ComponentId = num;
				num = this.m_uf.Find(l + this.m_numNodes);
				this.m_rhsNodes[l].ComponentId = num;
			}
		}

		// Token: 0x040002D8 RID: 728
		private const float GrowthFactor = 2f;

		// Token: 0x040002D9 RID: 729
		private const int NullComponent = -1;

		// Token: 0x040002DA RID: 730
		private const int Null = -1;

		// Token: 0x040002DB RID: 731
		private WtNode[] m_lhsNodes;

		// Token: 0x040002DC RID: 732
		private WtNode[] m_rhsNodes;

		// Token: 0x040002DD RID: 733
		private int m_numNodes;

		// Token: 0x040002DE RID: 734
		private BipartiteGraph m_shadowG;

		// Token: 0x040002DF RID: 735
		private UnionFind m_uf;

		// Token: 0x040002E0 RID: 736
		private int[] m_mindelta;

		// Token: 0x040002E1 RID: 737
		private WtModifierType m_wtModifierType;
	}
}
