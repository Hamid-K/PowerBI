using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	internal sealed class BipartiteGraph
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
		public BipartiteGraph(int capacity)
		{
			this.m_lhsNodes = new Node[capacity];
			this.m_rhsNodes = new Node[capacity];
			for (int i = 0; i < capacity; i++)
			{
				this.m_lhsNodes[i] = new Node(i, NodeType.LHS);
				this.m_rhsNodes[i] = new Node(i, NodeType.RHS);
			}
			this.m_currMatchSize = 0;
			this.m_visitStack = new IndexableStack<Node>();
			this.m_numNodes_lhs = (this.m_numNodes_rhs = 0);
			this.m_neighbors = new BitVector(capacity);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		public void Reset(int l, int r)
		{
			int num = this.m_lhsNodes.Length;
			int num2 = this.m_rhsNodes.Length;
			if (num < l)
			{
				Array.Resize<Node>(ref this.m_lhsNodes, Math.Max(1, (int)(2f * (float)l)));
				for (int i = num; i < this.m_lhsNodes.Length; i++)
				{
					this.m_lhsNodes[i] = new Node(i, NodeType.LHS);
				}
			}
			if (num2 < r)
			{
				Array.Resize<Node>(ref this.m_rhsNodes, Math.Max(1, (int)(2f * (float)r)));
				for (int j = num2; j < this.m_rhsNodes.Length; j++)
				{
					this.m_rhsNodes[j] = new Node(j, NodeType.RHS);
				}
			}
			this.m_numNodes_lhs = l;
			this.m_numNodes_rhs = r;
			for (int k = 0; k < this.m_numNodes_lhs; k++)
			{
				this.m_lhsNodes[k].Reset(r);
			}
			for (int m = 0; m < this.m_numNodes_rhs; m++)
			{
				this.m_rhsNodes[m].Reset(l);
			}
			this.m_visitStack.Clear();
			this.m_currMatchSize = 0;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001FB83 File Offset: 0x0001DD83
		public void ResetEdgesRetainMatch(int lhsNodeId)
		{
			this.m_lhsNodes[lhsNodeId].NeighborIds.Clear();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001FB98 File Offset: 0x0001DD98
		public void ResetEdges()
		{
			for (int i = 0; i < this.m_numNodes_lhs; i++)
			{
				this.m_lhsNodes[i].ResetEdges();
			}
			for (int j = 0; j < this.m_numNodes_rhs; j++)
			{
				this.m_rhsNodes[j].ResetEdges();
			}
			this.m_visitStack.Clear();
			this.m_currMatchSize = 0;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001FBF3 File Offset: 0x0001DDF3
		public void SetPrepareNode(bool lhs, int nodeid, PrepareNode nd)
		{
			if (lhs)
			{
				this.m_lhsNodes[nodeid].PrepareNode = nd;
				return;
			}
			this.m_rhsNodes[nodeid].PrepareNode = nd;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001FC18 File Offset: 0x0001DE18
		public void AddEdge(int lhsNodeId, int rhsNodeId)
		{
			if (this.m_lhsNodes[lhsNodeId].Match == null && this.m_rhsNodes[rhsNodeId].Match == null)
			{
				this.m_lhsNodes[lhsNodeId].Match = this.m_rhsNodes[rhsNodeId];
				this.m_rhsNodes[rhsNodeId].Match = this.m_lhsNodes[lhsNodeId];
				this.m_currMatchSize++;
			}
			this.m_lhsNodes[lhsNodeId].NeighborIds.Add(rhsNodeId);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001FC8E File Offset: 0x0001DE8E
		public int GetMaxMatchingSize()
		{
			this.m_currMatchSize = 0;
			while (this.FindAugmentingPath(this.m_currMatchSize))
			{
				this.UpdateMatching(this.m_currMatchSize);
				this.m_currMatchSize++;
			}
			return this.m_currMatchSize;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001FCC7 File Offset: 0x0001DEC7
		public int UpdateMaxMatching()
		{
			this.ClearVisitNum();
			while (this.FindAugmentingPath(this.m_currMatchSize))
			{
				this.UpdateMatching(this.m_currMatchSize);
				this.m_currMatchSize++;
			}
			return this.m_currMatchSize;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001FCFF File Offset: 0x0001DEFF
		public bool LhsNodeVisited(int id)
		{
			return this.m_lhsNodes[id].LastVisitNum == this.m_currMatchSize;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001FD16 File Offset: 0x0001DF16
		public bool RhsNodeVisited(int id)
		{
			return this.m_rhsNodes[id].LastVisitNum == this.m_currMatchSize;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001FD30 File Offset: 0x0001DF30
		public bool Match(int lhsNodeId, out int matchedRhsNodeId)
		{
			Node match = this.m_lhsNodes[lhsNodeId].Match;
			if (match != null)
			{
				matchedRhsNodeId = match.Id;
				return true;
			}
			matchedRhsNodeId = -1;
			return false;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		private void ClearVisitNum()
		{
			for (int i = 0; i < this.m_numNodes_lhs; i++)
			{
				this.m_lhsNodes[i].LastVisitNum = -1;
			}
			for (int j = 0; j < this.m_numNodes_rhs; j++)
			{
				this.m_rhsNodes[j].LastVisitNum = -1;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001FDA8 File Offset: 0x0001DFA8
		private bool FindAugmentingPath(int iterId)
		{
			bool flag = false;
			for (int i = 0; i < this.m_numNodes_lhs; i++)
			{
				Node node = this.m_lhsNodes[i];
				if (node.Match == null)
				{
					this.m_visitStack.Push(node);
				}
			}
			while (this.m_visitStack.Count > 0)
			{
				Node node = this.m_visitStack.Pop();
				node.LastVisitNum = iterId;
				if (node.Type == NodeType.LHS)
				{
					for (int j = 0; j < node.NeighborIds.Count; j++)
					{
						int num = node.NeighborIds[j];
						Node node2 = this.m_rhsNodes[num];
						if (node2 != node.Match && node2.LastVisitNum != iterId)
						{
							this.m_visitStack.Push(node2);
							node2.Parent = node;
						}
					}
				}
				else if (node.Match == null)
				{
					flag = true;
				}
				else
				{
					Node node2 = node.Match;
					this.m_visitStack.Push(node2);
					node2.Parent = node;
				}
			}
			return flag;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001FE98 File Offset: 0x0001E098
		private void UpdateMatching(int iterId)
		{
			Node node = null;
			for (int i = 0; i < this.m_numNodes_rhs; i++)
			{
				node = this.m_rhsNodes[i];
				if (node.LastVisitNum == iterId && node.Match == null)
				{
					IL_0053:
					while (node != null)
					{
						node.Parent.Match = node;
						node.Match = node.Parent;
						node = node.Parent.Parent;
					}
					return;
				}
			}
			goto IL_0053;
		}

		// Token: 0x040002C1 RID: 705
		private const float GrowthFactor = 2f;

		// Token: 0x040002C2 RID: 706
		private Node[] m_lhsNodes;

		// Token: 0x040002C3 RID: 707
		private Node[] m_rhsNodes;

		// Token: 0x040002C4 RID: 708
		private int m_numNodes_lhs;

		// Token: 0x040002C5 RID: 709
		private int m_numNodes_rhs;

		// Token: 0x040002C6 RID: 710
		private IndexableStack<Node> m_visitStack;

		// Token: 0x040002C7 RID: 711
		private int m_currMatchSize;

		// Token: 0x040002C8 RID: 712
		private BitVector m_neighbors;
	}
}
