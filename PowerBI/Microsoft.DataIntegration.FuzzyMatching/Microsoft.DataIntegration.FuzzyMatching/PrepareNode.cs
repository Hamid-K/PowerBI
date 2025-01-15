using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	internal sealed class PrepareNode
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00020DAC File Offset: 0x0001EFAC
		public bool Degree0
		{
			get
			{
				return this.m_neighbors.Count == 0;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00020DBC File Offset: 0x0001EFBC
		public PrepareNode()
		{
			this.m_neighborInfo = new PrepareNode.NeighborInfo[2];
			this.m_neighbors = new IntVector(2);
			this.m_neighborMark = new BitVector(2);
			this.m_neighborIdx = 0;
			this.MatchId = -1;
			this.MinTM.Reset();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00020E0C File Offset: 0x0001F00C
		public void Reset()
		{
			this.ResetEdges();
			this.MinTM.Reset();
			this.Id = (this.NewId = (this.Token = (this.MinTok = (this.MinWt = -1))));
			this.Valid = false;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00020E5C File Offset: 0x0001F05C
		public void ResetEdges()
		{
			this.m_neighbors.Clear();
			this.m_neighborMark.Clear();
			this.m_neighborIdx = 0;
			this.MatchId = -1;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00020E82 File Offset: 0x0001F082
		public void BeginNeighbors()
		{
			this.m_neighborIdx = 0;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00020E8C File Offset: 0x0001F08C
		public bool GetNextValidNeighbor(out PrepareNode nd, out int tok, out int tokwt)
		{
			while (this.m_neighborIdx < this.m_neighbors.Count && !this.m_neighborInfo[this.m_neighbors[this.m_neighborIdx]].nd.Valid)
			{
				this.m_neighborIdx++;
			}
			if (this.m_neighbors.Count == this.m_neighborIdx)
			{
				nd = null;
				tok = (tokwt = -1);
				return false;
			}
			nd = this.m_neighborInfo[this.m_neighbors[this.m_neighborIdx]].nd;
			tok = this.m_neighborInfo[this.m_neighbors[this.m_neighborIdx]].tok;
			tokwt = this.m_neighborInfo[this.m_neighbors[this.m_neighborIdx]].wt;
			this.m_neighborIdx++;
			return true;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00020F7C File Offset: 0x0001F17C
		public void GetNeighborInfo(int nodeid, out int wt, out TransformationMatch lhsTM, out TransformationMatch rhsTM)
		{
			wt = this.m_neighborInfo[nodeid].wt;
			lhsTM = this.m_neighborInfo[nodeid].tmLhs;
			rhsTM = this.m_neighborInfo[nodeid].tmRhs;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00020FCC File Offset: 0x0001F1CC
		public void AddNeighbor(PrepareNode nd, int tok, int ewt, int lhsTMPos, Transformation lhsTMTran, int rhsTMPos, Transformation rhsTMTran)
		{
			int id = nd.Id;
			if (id >= this.m_neighborInfo.Length)
			{
				int num = Math.Max(1, (int)(2f * (float)id));
				Array.Resize<PrepareNode.NeighborInfo>(ref this.m_neighborInfo, num);
				this.m_neighborMark.Resize(num);
			}
			if (!this.m_neighborMark.GetBit(id))
			{
				this.m_neighbors.Add(id);
				this.m_neighborMark.SetBit(id, true);
				this.m_neighborInfo[id].nd = nd;
				this.m_neighborInfo[id].wt = ewt;
				this.m_neighborInfo[id].tok = tok;
				this.m_neighborInfo[id].tmLhs.Position = lhsTMPos;
				this.m_neighborInfo[id].tmLhs.Transformation = lhsTMTran;
				this.m_neighborInfo[id].tmRhs.Position = rhsTMPos;
				this.m_neighborInfo[id].tmRhs.Transformation = rhsTMTran;
				return;
			}
			if (this.m_neighborInfo[id].wt < ewt || (this.m_neighborInfo[id].wt == ewt && this.Token == tok))
			{
				this.m_neighborInfo[id].nd = nd;
				this.m_neighborInfo[id].wt = ewt;
				this.m_neighborInfo[id].tok = tok;
				this.m_neighborInfo[id].tmLhs.Position = lhsTMPos;
				this.m_neighborInfo[id].tmLhs.Transformation = lhsTMTran;
				this.m_neighborInfo[id].tmRhs.Position = rhsTMPos;
				this.m_neighborInfo[id].tmRhs.Transformation = rhsTMTran;
			}
		}

		// Token: 0x040002F1 RID: 753
		private const int Null = -1;

		// Token: 0x040002F2 RID: 754
		private const float GrowthFactor = 2f;

		// Token: 0x040002F3 RID: 755
		private const int InitialCapacity = 2;

		// Token: 0x040002F4 RID: 756
		private PrepareNode.NeighborInfo[] m_neighborInfo;

		// Token: 0x040002F5 RID: 757
		private IntVector m_neighbors;

		// Token: 0x040002F6 RID: 758
		private BitVector m_neighborMark;

		// Token: 0x040002F7 RID: 759
		private int m_neighborIdx;

		// Token: 0x040002F8 RID: 760
		public int Id;

		// Token: 0x040002F9 RID: 761
		public int NewId;

		// Token: 0x040002FA RID: 762
		public int Token;

		// Token: 0x040002FB RID: 763
		public int MatchId;

		// Token: 0x040002FC RID: 764
		public int MinTok;

		// Token: 0x040002FD RID: 765
		public TransformationMatch MinTM;

		// Token: 0x040002FE RID: 766
		public int MinWt;

		// Token: 0x040002FF RID: 767
		public bool Valid;

		// Token: 0x02000175 RID: 373
		[Serializable]
		private struct NeighborInfo
		{
			// Token: 0x040005F7 RID: 1527
			public PrepareNode nd;

			// Token: 0x040005F8 RID: 1528
			public TransformationMatch tmLhs;

			// Token: 0x040005F9 RID: 1529
			public TransformationMatch tmRhs;

			// Token: 0x040005FA RID: 1530
			public int wt;

			// Token: 0x040005FB RID: 1531
			public int tok;
		}
	}
}
