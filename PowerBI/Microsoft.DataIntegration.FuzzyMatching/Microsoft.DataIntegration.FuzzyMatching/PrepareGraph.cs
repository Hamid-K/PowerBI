using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	internal sealed class PrepareGraph
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x00021378 File Offset: 0x0001F578
		public PrepareGraph()
		{
			this.m_lhsNodes = new PrepareNodeList(16);
			this.m_rhsNodes = new PrepareNodeList(16);
			this.m_lhsValidNodes = new PrepareNodePtrList(16);
			this.m_rhsValidNodes = new PrepareNodePtrList(16);
			this.m_lhsPrunedNodes = new PrepareNodePtrList(16);
			this.m_rhsPrunedNodes = new PrepareNodePtrList(16);
			this.m_G = new WtBipartiteGraph(16);
			this.m_maxWt = (this.m_maxWtLhs = 1);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000213F6 File Offset: 0x0001F5F6
		public void Reset()
		{
			this.m_lhsNodes.Reset();
			this.m_lhsValidNodes.Reset();
			this.m_rhsNodes.Reset();
			this.m_rhsValidNodes.Reset();
			this.m_maxWt = 1;
			this.m_maxWtLhs = 1;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00021432 File Offset: 0x0001F632
		public int AddLhsNode(int tok, int pos, int tokwt)
		{
			this.m_lhsNodes.Add(tok, tok, tokwt);
			if (tokwt + 1 > this.m_maxWt)
			{
				this.m_maxWt = tokwt + 1;
				this.m_maxWtLhs = this.m_maxWt;
			}
			return this.m_lhsNodes.Length - 1;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00021470 File Offset: 0x0001F670
		public void UpdateNodeMinWt(Side side, int nodeid, int tok, int wt, int position, Transformation transformation)
		{
			if (side == Side.Left)
			{
				if (wt < this.m_lhsNodes[nodeid].MinWt)
				{
					this.m_lhsNodes[nodeid].MinWt = wt;
					this.m_lhsNodes[nodeid].MinTok = tok;
					this.m_lhsNodes[nodeid].MinTM.Position = position;
					this.m_lhsNodes[nodeid].MinTM.Transformation = transformation;
					return;
				}
			}
			else if (wt < this.m_rhsNodes[nodeid].MinWt)
			{
				this.m_rhsNodes[nodeid].MinWt = wt;
				this.m_rhsNodes[nodeid].MinTok = tok;
				this.m_rhsNodes[nodeid].MinTM.Position = position;
				this.m_rhsNodes[nodeid].MinTM.Transformation = transformation;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00021558 File Offset: 0x0001F758
		public void ResetRhs()
		{
			this.m_rhsNodes.Reset();
			this.m_rhsValidNodes.Reset();
			this.m_lhsNodes.ResetEdges();
			this.m_maxWt = this.m_maxWtLhs;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00021587 File Offset: 0x0001F787
		public int AddRhsNode(int tok, int pos, int tokwt)
		{
			this.m_rhsNodes.Add(tok, tok, tokwt);
			if (tokwt + 1 > this.m_maxWt)
			{
				this.m_maxWt = tokwt + 1;
			}
			return this.m_rhsNodes.Length - 1;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000215B8 File Offset: 0x0001F7B8
		public void AddEdge(int lhsid, int rhsid, int tok, int wt, int lhsTMPos, Transformation lhsTMTran, int rhsTMPos, Transformation rhsTMTran)
		{
			this.m_lhsNodes[lhsid].AddNeighbor(this.m_rhsNodes[rhsid], tok, wt, lhsTMPos, lhsTMTran, rhsTMPos, rhsTMTran);
			this.m_rhsNodes[rhsid].AddNeighbor(this.m_lhsNodes[lhsid], tok, wt, rhsTMPos, rhsTMTran, lhsTMPos, lhsTMTran);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00021618 File Offset: 0x0001F818
		public void OpenValidMarking(bool lhs)
		{
			if (lhs)
			{
				for (int i = 0; i < this.m_lhsValidNodes.Length; i++)
				{
					this.m_lhsValidNodes[i].Valid = false;
					this.m_lhsValidNodes[i].MatchId = -1;
				}
				this.m_lhsValidNodes.Reset();
				this.m_lhsPrunedNodes.Reset();
				return;
			}
			for (int j = 0; j < this.m_rhsValidNodes.Length; j++)
			{
				this.m_rhsValidNodes[j].Valid = false;
				this.m_rhsValidNodes[j].MatchId = -1;
			}
			this.m_rhsValidNodes.Reset();
			this.m_rhsPrunedNodes.Reset();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000216CC File Offset: 0x0001F8CC
		public void SetValid(bool lhs, int nodeid)
		{
			if (lhs)
			{
				this.m_lhsNodes[nodeid].Valid = true;
				this.m_lhsValidNodes.Add(this.m_lhsNodes[nodeid]);
				return;
			}
			this.m_rhsNodes[nodeid].Valid = true;
			this.m_rhsValidNodes.Add(this.m_rhsNodes[nodeid]);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002172F File Offset: 0x0001F92F
		public void CloseValidMarking(bool lhs)
		{
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00021731 File Offset: 0x0001F931
		public bool LhsDegree0_OverallGraph(int lhsid)
		{
			return this.m_lhsNodes[lhsid].Degree0;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00021744 File Offset: 0x0001F944
		public bool CheckMatching(double threshold)
		{
			this.PruneValidSubgraph();
			this.BuildPrunedGraph();
			if (this.m_lhsPrunedNodes.Length > 0 || this.m_rhsPrunedNodes.Length > 0)
			{
				this.m_G.FindMaxMatching(threshold, this.m_maxWt);
			}
			return this.ComputeScore() >= threshold;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00021798 File Offset: 0x0001F998
		public void MaxSimilarity(double lowerBound, double upperBound, ComparisonResult result)
		{
			if (this.m_G.NumNodes == 0)
			{
				this.ScoreWithLineage(result, true);
				return;
			}
			double num = lowerBound;
			double num2 = upperBound;
			this.ScoreWithLineage(result, true);
			double num3 = result.Similarity;
			if (num3 > num)
			{
				num = num3;
			}
			while (num2 - num > 0.01)
			{
				double num4 = (num2 + num) / 2.0;
				this.m_G.UpdateMaxMatching(num4, this.m_maxWt);
				this.ScoreWithLineage(result, true);
				num3 = result.Similarity;
				if (num3 >= num4)
				{
					num = num3;
				}
				else
				{
					num2 = num4;
				}
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00021820 File Offset: 0x0001FA20
		private double ComputeScore()
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_lhsValidNodes.Length; i++)
			{
				PrepareNode prepareNode = this.m_lhsValidNodes[i];
				if (prepareNode.MatchId == -1)
				{
					num2 += prepareNode.MinWt;
				}
				else
				{
					int num3;
					TransformationMatch transformationMatch;
					TransformationMatch transformationMatch2;
					prepareNode.GetNeighborInfo(prepareNode.MatchId, out num3, out transformationMatch, out transformationMatch2);
					num += num3;
				}
			}
			if (this.ComparisonType == FuzzyComparisonType.Jaccard)
			{
				for (int j = 0; j < this.m_rhsValidNodes.Length; j++)
				{
					PrepareNode prepareNode2 = this.m_rhsValidNodes[j];
					if (prepareNode2.MatchId == -1)
					{
						num2 += prepareNode2.MinWt;
					}
				}
			}
			if (num + num2 == 0)
			{
				return 1.0;
			}
			return (double)num / (double)(num + num2);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000218DC File Offset: 0x0001FADC
		private void ScoreWithLineage(ComparisonResult result, bool includeLineage = true)
		{
			ArraySegmentBuilder<TransformationMatch> leftTransformationsApplied = result.LeftTransformationsApplied;
			ArraySegmentBuilder<TransformationMatch> rightTransformationsApplied = result.RightTransformationsApplied;
			leftTransformationsApplied.Reset();
			rightTransformationsApplied.Reset();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_lhsValidNodes.Length; i++)
			{
				PrepareNode prepareNode = this.m_lhsValidNodes[i];
				if (prepareNode.MatchId == -1)
				{
					num2 += prepareNode.MinWt;
					if (includeLineage && !prepareNode.MinTM.Transformation.IsNull)
					{
						leftTransformationsApplied.Add(new TransformationMatch(prepareNode.MinTM));
					}
				}
				else
				{
					int num3;
					TransformationMatch transformationMatch;
					TransformationMatch transformationMatch2;
					prepareNode.GetNeighborInfo(prepareNode.MatchId, out num3, out transformationMatch, out transformationMatch2);
					num += num3;
					if (includeLineage)
					{
						if (!transformationMatch.Transformation.IsNull)
						{
							leftTransformationsApplied.Add(new TransformationMatch(transformationMatch));
						}
						if (!transformationMatch2.Transformation.IsNull)
						{
							rightTransformationsApplied.Add(new TransformationMatch(transformationMatch2));
						}
					}
				}
			}
			if (this.ComparisonType == FuzzyComparisonType.Jaccard)
			{
				for (int j = 0; j < this.m_rhsValidNodes.Length; j++)
				{
					PrepareNode prepareNode2 = this.m_rhsValidNodes[j];
					if (prepareNode2.MatchId == -1)
					{
						num2 += prepareNode2.MinWt;
						if (includeLineage && !prepareNode2.MinTM.Transformation.IsNull)
						{
							rightTransformationsApplied.Add(new TransformationMatch(prepareNode2.MinTM));
						}
					}
				}
			}
			result.NumeratorWeight = (double)num;
			result.DenominatorWeight = (double)(num + num2);
			if (num + num2 == 0)
			{
				result.Similarity = 1.0;
				return;
			}
			result.Similarity = (double)num / (double)(num + num2);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00021A68 File Offset: 0x0001FC68
		private void BuildPrunedGraph()
		{
			if (this.m_lhsPrunedNodes.Length + this.m_rhsPrunedNodes.Length == 0)
			{
				this.m_G.Reset(0, this.ComparisonType);
				return;
			}
			this.m_G.Reset(this.m_lhsPrunedNodes.Length + this.m_rhsPrunedNodes.Length, this.ComparisonType);
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < this.m_lhsPrunedNodes.Length; i++)
			{
				PrepareNode prepareNode = this.m_lhsPrunedNodes[i];
				this.m_G.SetPrepareNode(flag, prepareNode.NewId, prepareNode);
			}
			for (int j = 0; j < this.m_rhsPrunedNodes.Length; j++)
			{
				PrepareNode prepareNode2 = this.m_rhsPrunedNodes[j];
				this.m_G.SetPrepareNode(flag2, prepareNode2.NewId, prepareNode2);
			}
			for (int k = 0; k < this.m_lhsPrunedNodes.Length; k++)
			{
				PrepareNode prepareNode3 = this.m_lhsPrunedNodes[k];
				prepareNode3.BeginNeighbors();
				PrepareNode prepareNode4;
				int num;
				int num2;
				while (prepareNode3.GetNextValidNeighbor(out prepareNode4, out num, out num2))
				{
					this.m_G.AddEdge(prepareNode3.NewId, prepareNode4.NewId, num2);
				}
				this.m_G.AddMirrorEdge(prepareNode3.NewId, prepareNode3.NewId + this.m_rhsPrunedNodes.Length, prepareNode3.MinWt);
			}
			for (int l = 0; l < this.m_rhsPrunedNodes.Length; l++)
			{
				PrepareNode prepareNode5 = this.m_rhsPrunedNodes[l];
				if (this.ComparisonType == FuzzyComparisonType.Jaccard)
				{
					this.m_G.AddMirrorEdge(prepareNode5.NewId + this.m_lhsPrunedNodes.Length, prepareNode5.NewId, prepareNode5.MinWt);
				}
				else if (this.ComparisonType == FuzzyComparisonType.LeftJaccardContainment)
				{
					this.m_G.AddMirrorEdge(prepareNode5.NewId + this.m_lhsPrunedNodes.Length, prepareNode5.NewId, 0);
				}
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00021C60 File Offset: 0x0001FE60
		private void PruneValidSubgraph()
		{
			for (int i = 0; i < this.m_lhsValidNodes.Length; i++)
			{
				PrepareNode prepareNode = this.m_lhsValidNodes[i];
				if (!this.PrunableValidSubgraph(prepareNode))
				{
					prepareNode.NewId = this.m_lhsPrunedNodes.Length;
					this.m_lhsPrunedNodes.Add(prepareNode);
				}
			}
			for (int j = 0; j < this.m_rhsValidNodes.Length; j++)
			{
				PrepareNode prepareNode2 = this.m_rhsValidNodes[j];
				if (!this.PrunableValidSubgraph(prepareNode2))
				{
					prepareNode2.NewId = this.m_rhsPrunedNodes.Length;
					this.m_rhsPrunedNodes.Add(prepareNode2);
				}
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00021D00 File Offset: 0x0001FF00
		private bool PrunableValidSubgraph(PrepareNode nd)
		{
			nd.BeginNeighbors();
			PrepareNode prepareNode;
			int num;
			int num2;
			if (!nd.GetNextValidNeighbor(out prepareNode, out num, out num2))
			{
				return true;
			}
			PrepareNode prepareNode2;
			int num3;
			int num4;
			if (nd.GetNextValidNeighbor(out prepareNode2, out num3, out num4))
			{
				return false;
			}
			prepareNode.BeginNeighbors();
			prepareNode.GetNextValidNeighbor(out prepareNode2, out num3, out num4);
			if (prepareNode.GetNextValidNeighbor(out prepareNode2, out num3, out num4))
			{
				return false;
			}
			nd.MatchId = prepareNode.Id;
			prepareNode.MatchId = nd.Id;
			return true;
		}

		// Token: 0x04000306 RID: 774
		private int m_maxWt;

		// Token: 0x04000307 RID: 775
		private int m_maxWtLhs;

		// Token: 0x04000308 RID: 776
		private const int delta = 1;

		// Token: 0x04000309 RID: 777
		private const int Null = -1;

		// Token: 0x0400030A RID: 778
		private const int InitialCapacity = 16;

		// Token: 0x0400030B RID: 779
		private const double ScoreWidth = 0.01;

		// Token: 0x0400030C RID: 780
		private PrepareNodeList m_lhsNodes;

		// Token: 0x0400030D RID: 781
		private PrepareNodeList m_rhsNodes;

		// Token: 0x0400030E RID: 782
		private PrepareNodePtrList m_lhsValidNodes;

		// Token: 0x0400030F RID: 783
		private PrepareNodePtrList m_rhsValidNodes;

		// Token: 0x04000310 RID: 784
		private PrepareNodePtrList m_lhsPrunedNodes;

		// Token: 0x04000311 RID: 785
		private PrepareNodePtrList m_rhsPrunedNodes;

		// Token: 0x04000312 RID: 786
		private WtBipartiteGraph m_G;

		// Token: 0x04000313 RID: 787
		public FuzzyComparisonType ComparisonType;
	}
}
