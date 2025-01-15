using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	internal sealed class WtNode
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0001FF2C File Offset: 0x0001E12C
		public WtNode()
		{
			this.m_neighborIds = new int[2];
			this.m_neighbors = new WtNode.NeighborInfo[2];
			this.m_neighborWts = new int[2];
			this.m_markNeighbors = new BitVector(2);
			this.m_componentId = -1;
			this.m_markNeighbors.Clear();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001FF84 File Offset: 0x0001E184
		public void Reset(int maxNeighbors)
		{
			if (maxNeighbors > this.m_neighborIds.Length)
			{
				int num = Math.Max(1, (int)(2f * (float)maxNeighbors));
				Array.Resize<int>(ref this.m_neighborIds, num);
				Array.Resize<WtNode.NeighborInfo>(ref this.m_neighbors, num);
				Array.Resize<int>(ref this.m_neighborWts, num);
				this.m_markNeighbors.ResizeAndClear(num);
			}
			this.m_numNeighbors = 0;
			this.m_markNeighbors.Clear();
			this.m_componentId = -1;
			this.m_nodewt = 0;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		public void ResetNeighborWts(double threshold, int maxwt, WtModifierType wtModifierType)
		{
			for (int i = 0; i < this.m_MirrorEdgeIdx; i++)
			{
				this.m_neighborWts[i] = WeightModifier.round((double)this.m_neighbors[i].wt * (1.0 - threshold) + (double)(2 * maxwt));
			}
			if (this.m_neighbors[this.m_MirrorEdgeIdx].wt > 0)
			{
				this.m_neighborWts[this.m_MirrorEdgeIdx] = WeightModifier.round((double)maxwt - threshold * (double)this.m_neighbors[this.m_MirrorEdgeIdx].wt);
				return;
			}
			this.m_neighborWts[this.m_MirrorEdgeIdx] = 0;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x000200A0 File Offset: 0x0001E2A0
		public int NumNeighbors
		{
			get
			{
				return this.m_numNeighbors;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x000200A8 File Offset: 0x0001E2A8
		public int[] NeighborIds
		{
			get
			{
				return this.m_neighborIds;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000200B0 File Offset: 0x0001E2B0
		public int[] NeighborWts
		{
			get
			{
				return this.m_neighborWts;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000200B8 File Offset: 0x0001E2B8
		public void AddNeighbor(int nid, int ewt)
		{
			if (!this.m_markNeighbors.GetBit(nid))
			{
				this.m_markNeighbors.SetBit(nid, true);
				this.m_neighborIds[this.m_numNeighbors] = nid;
				this.m_neighbors[this.m_numNeighbors].wt = ewt;
				this.m_numNeighbors++;
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00020113 File Offset: 0x0001E313
		public void AddMirrorNeighbor(int nid, int ewt)
		{
			this.m_MirrorEdgeIdx = this.m_numNeighbors;
			this.AddNeighbor(nid, ewt);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00020129 File Offset: 0x0001E329
		public void AddZeroNeighbor(int nid)
		{
			if (!this.m_markNeighbors.GetBit(nid))
			{
				this.m_neighborIds[this.m_numNeighbors] = nid;
				this.m_neighborWts[this.m_numNeighbors] = 0;
				this.m_numNeighbors++;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00020163 File Offset: 0x0001E363
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0002016B File Offset: 0x0001E36B
		public int Wt
		{
			get
			{
				return this.m_nodewt;
			}
			set
			{
				this.m_nodewt = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00020174 File Offset: 0x0001E374
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0002017C File Offset: 0x0001E37C
		public int ComponentId
		{
			get
			{
				return this.m_componentId;
			}
			set
			{
				this.m_componentId = value;
			}
		}

		// Token: 0x040002CC RID: 716
		private const int NullComponent = -1;

		// Token: 0x040002CD RID: 717
		private const float GrowthFactor = 2f;

		// Token: 0x040002CE RID: 718
		private const int InitialNeighbors = 2;

		// Token: 0x040002CF RID: 719
		private const int Null = -1;

		// Token: 0x040002D0 RID: 720
		private int[] m_neighborIds;

		// Token: 0x040002D1 RID: 721
		private int[] m_neighborWts;

		// Token: 0x040002D2 RID: 722
		private WtNode.NeighborInfo[] m_neighbors;

		// Token: 0x040002D3 RID: 723
		private BitVector m_markNeighbors;

		// Token: 0x040002D4 RID: 724
		private int m_numNeighbors;

		// Token: 0x040002D5 RID: 725
		private int m_MirrorEdgeIdx;

		// Token: 0x040002D6 RID: 726
		private int m_componentId;

		// Token: 0x040002D7 RID: 727
		private int m_nodewt;

		// Token: 0x02000173 RID: 371
		[Serializable]
		private struct NeighborInfo
		{
			// Token: 0x040005F4 RID: 1524
			public int wt;
		}
	}
}
