using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	internal sealed class Node
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001F8CA File Offset: 0x0001DACA
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001F8D2 File Offset: 0x0001DAD2
		public int Id { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001F8DB File Offset: 0x0001DADB
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001F8E3 File Offset: 0x0001DAE3
		public IntVector NeighborIds { get; private set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001F8EC File Offset: 0x0001DAEC
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
		public NodeType Type { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001F8FD File Offset: 0x0001DAFD
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001F905 File Offset: 0x0001DB05
		public int LastVisitNum { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001F90E File Offset: 0x0001DB0E
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001F918 File Offset: 0x0001DB18
		public Node Match
		{
			get
			{
				return this.m_matchNode;
			}
			set
			{
				this.m_matchNode = value;
				if (this.PrepareNode != null)
				{
					if (this.m_matchNode != null && this.m_matchNode.PrepareNode != null)
					{
						this.PrepareNode.MatchId = this.m_matchNode.PrepareNode.Id;
						return;
					}
					this.PrepareNode.MatchId = -1;
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001F971 File Offset: 0x0001DB71
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001F979 File Offset: 0x0001DB79
		public Node Parent { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001F982 File Offset: 0x0001DB82
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001F98A File Offset: 0x0001DB8A
		public PrepareNode PrepareNode { get; set; }

		// Token: 0x06000727 RID: 1831 RVA: 0x0001F993 File Offset: 0x0001DB93
		public Node(int id, NodeType type)
		{
			this.NeighborIds = new IntVector(32);
			this.LastVisitNum = -1;
			this.Type = type;
			this.Id = id;
			this.PrepareNode = null;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001F9C4 File Offset: 0x0001DBC4
		public void Reset(int maxNeighbors)
		{
			this.PrepareNode = null;
			this.ResetEdges();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001F9D3 File Offset: 0x0001DBD3
		public void ResetEdges()
		{
			this.NeighborIds.Clear();
			this.LastVisitNum = -1;
			this.Match = null;
			this.Parent = null;
		}

		// Token: 0x040002B8 RID: 696
		private const int InitialNeighbors = 32;

		// Token: 0x040002B9 RID: 697
		private const int Null = -1;

		// Token: 0x040002BA RID: 698
		private Node m_matchNode;
	}
}
