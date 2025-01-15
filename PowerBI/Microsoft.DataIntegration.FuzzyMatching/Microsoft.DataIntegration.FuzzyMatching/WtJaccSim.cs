using System;
using System.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	internal sealed class WtJaccSim
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00021ED0 File Offset: 0x000200D0
		public WtJaccSim()
		{
			this.m_multiRuleAppl1 = new RuleApplEnumerator<TransformationMatch>();
			this.m_multiRuleAppl2 = new RuleApplEnumerator<TransformationMatch>();
			this.m_tranMatchSublist1 = new IndexedSubList<TransformationMatch>();
			this.m_tranMatchSublist2 = new IndexedSubList<TransformationMatch>();
			this.m_PG = new PrepareGraph();
			this.m_tok2NodeMap = new Token2NodeMap();
			this.m_ruleId2nodeId_lhs = new int[16];
			this.m_ruleId2nodeId_rhs = new int[16];
			this.m_minWtHT_rhs = new MinWtHashTable(16);
			this.m_minWtHT_lhs = new MinWtHashTable(16);
			this.m_tmpIntSet = new IntVector(16);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00021F66 File Offset: 0x00020166
		public void ResetLeftRecord(RecordContext recordContext)
		{
			this.ResetLeftRecord(recordContext.TokenSequence, recordContext.TransformationMatchList, true);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00021F7C File Offset: 0x0002017C
		public void ResetLeftRecord(WeightedTokenSequence tokSeq, ArraySegmentBuilder<WeightedTransformationMatch> tranMatchList, bool delay = true)
		{
			this.m_inputTokSeq = tokSeq;
			this.m_inputTMList = tranMatchList;
			this.m_quickIntersectionResultsAreCachedAndValid = false;
			if (delay)
			{
				this.m_delayResetLeftRecordPending = true;
				return;
			}
			this.m_tok2NodeMap.Reset();
			this.m_PG.Reset();
			this.InitializeToken2Node(Side.Left);
			this.CopyMultiRules(tranMatchList, this.m_tranMatchSublist1);
			this.m_delayResetLeftRecordPending = false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00021FDA File Offset: 0x000201DA
		public void ResetRightRecord(RecordContext recordContext, FuzzyComparisonType comparisonType)
		{
			this.ResetRightRecord(recordContext.TokenSequence, recordContext.TransformationMatchList, comparisonType, true);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00021FF0 File Offset: 0x000201F0
		public void ResetRightRecord(WeightedTokenSequence tokSeq, ArraySegmentBuilder<WeightedTransformationMatch> tranMatchList, FuzzyComparisonType comparisonType, bool delay = true)
		{
			this.m_PG.ComparisonType = comparisonType;
			this.m_refTokSeq = tokSeq;
			this.m_refTMList = tranMatchList;
			this.m_quickIntersectionResultsAreCachedAndValid = false;
			if (delay)
			{
				this.m_delayResetRightRecordPending = true;
				return;
			}
			this.m_PG.ResetRhs();
			this.m_minWtHT_lhs.Clear();
			this.m_minWtHT_rhs.Clear();
			this.InitializeToken2Node(Side.Right);
			this.CopyMultiRules(tokSeq.Tokens, tranMatchList, this.m_tranMatchSublist2, this.m_minWtHT_rhs);
			this.PruneInputMultiRules();
			this.SublistMultiRules(this.m_tranMatchSublist1, this.m_minWtHT_lhs);
			this.m_delayResetRightRecordPending = false;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0002208C File Offset: 0x0002028C
		private void QuickIntersection(WeightedTokenSequence t1, WeightedTokenSequence t2, out double intersection, out double union)
		{
			if (this.m_quickIntersectionResultsAreCachedAndValid)
			{
				intersection = this.m_quickIntersection;
				union = this.m_quickUnion;
				return;
			}
			WeightedTokenSequence weightedTokenSequence = ((t1.Count < t2.Count) ? t1 : t2);
			WeightedTokenSequence weightedTokenSequence2 = ((t1.Count < t2.Count) ? t2 : t1);
			BitArray bitArray = new BitArray(weightedTokenSequence2.Count);
			intersection = 0.0;
			union = 0.0;
			for (int i = 0; i < weightedTokenSequence.Count; i++)
			{
				union += (double)weightedTokenSequence.GetWeight(i);
				for (int j = 0; j < weightedTokenSequence2.Count; j++)
				{
					if (weightedTokenSequence.Tokens[i] == weightedTokenSequence2.Tokens[j] && !bitArray[j])
					{
						intersection += (double)weightedTokenSequence.GetWeight(i);
						bitArray[j] = true;
						break;
					}
				}
			}
			for (int k = 0; k < weightedTokenSequence2.Count; k++)
			{
				if (!bitArray[k])
				{
					union += (double)weightedTokenSequence2.GetWeight(k);
				}
			}
			this.m_quickIntersection = intersection;
			this.m_quickUnion = union;
			this.m_quickIntersectionResultsAreCachedAndValid = true;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000221C4 File Offset: 0x000203C4
		public bool CheckSimilarity(double threshold)
		{
			if (this.m_inputTokSeq.Equals(this.m_refTokSeq))
			{
				return true;
			}
			if (this.m_inputTMList.Count != 0 || this.m_refTMList.Count != 0)
			{
				if (this.m_delayResetLeftRecordPending)
				{
					this.ResetLeftRecord(this.m_inputTokSeq, this.m_inputTMList, false);
				}
				if (this.m_delayResetRightRecordPending)
				{
					this.ResetRightRecord(this.m_refTokSeq, this.m_refTMList, this.m_PG.ComparisonType, false);
				}
				this.m_multiRuleAppl1.Reset(this.m_inputTokSeq.Tokens, this.m_tranMatchSublist1);
				while (this.m_multiRuleAppl1.GetNextRuleApplication())
				{
					this.m_multiRuleAppl2.Reset(this.m_refTokSeq.Tokens, this.m_tranMatchSublist2);
					while (this.m_multiRuleAppl2.GetNextRuleApplication())
					{
						this.ConstructSubGraph(Side.Left);
						this.ConstructSubGraph(Side.Right);
						if (this.m_PG.CheckMatching(threshold))
						{
							return true;
						}
					}
				}
				return false;
			}
			double num;
			double num2;
			this.QuickIntersection(this.m_inputTokSeq, this.m_refTokSeq, out num, out num2);
			if (this.m_PG.ComparisonType == FuzzyComparisonType.Jaccard)
			{
				return WtJaccSim.CalculateSimlarity(num, num2) >= threshold;
			}
			if (FuzzyComparisonType.LeftJaccardContainment == this.m_PG.ComparisonType)
			{
				return WtJaccSim.CalculateSimlarity(num, (double)this.m_inputTokSeq.TotalWeight()) >= threshold;
			}
			throw new InvalidOperationException("Unexpected ComparisonType!");
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00022319 File Offset: 0x00020519
		internal static double CalculateSimlarity(double numerator, double denominator)
		{
			if (denominator == 0.0)
			{
				return 1.0;
			}
			return numerator / denominator;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00022334 File Offset: 0x00020534
		public void Similarity(double lowerBound, double upperBound, ComparisonResult result)
		{
			result.TotalLeftWeight = (double)this.m_inputTokSeq.TotalWeight();
			result.TotalRightWeight = (double)this.m_refTokSeq.TotalWeight();
			if (this.m_inputTokSeq.Equals(this.m_refTokSeq))
			{
				result.Similarity = 1.0;
				result.DenominatorWeight = (result.NumeratorWeight = (result.TotalLeftWeight = result.TotalRightWeight));
				return;
			}
			if (this.m_inputTMList.Count != 0 || this.m_refTMList.Count != 0)
			{
				if (this.m_delayResetLeftRecordPending)
				{
					this.ResetLeftRecord(this.m_inputTokSeq, this.m_inputTMList, false);
				}
				if (this.m_delayResetRightRecordPending)
				{
					this.ResetRightRecord(this.m_refTokSeq, this.m_refTMList, this.m_PG.ComparisonType, false);
				}
				double num = lowerBound;
				this.m_multiRuleAppl1.Reset(this.m_inputTokSeq.Tokens, this.m_tranMatchSublist1);
				while (this.m_multiRuleAppl1.GetNextRuleApplication())
				{
					this.m_multiRuleAppl2.Reset(this.m_refTokSeq.Tokens, this.m_tranMatchSublist2);
					while (this.m_multiRuleAppl2.GetNextRuleApplication())
					{
						int num2 = this.m_multiRuleAppl1.NumRulesApplied + this.m_multiRuleAppl2.NumRulesApplied;
						this.ConstructSubGraph(Side.Left);
						this.ConstructSubGraph(Side.Right);
						if (this.m_PG.CheckMatching(num))
						{
							this.m_PG.MaxSimilarity(num, upperBound, result);
							num = result.Similarity;
							this.CopyMultiMatch(this.m_multiRuleAppl1, result.LeftTransformationsApplied);
							this.CopyMultiMatch(this.m_multiRuleAppl2, result.RightTransformationsApplied);
						}
					}
				}
				return;
			}
			double num3;
			double num4;
			this.QuickIntersection(this.m_inputTokSeq, this.m_refTokSeq, out num3, out num4);
			result.TotalLeftWeight = (double)this.m_inputTokSeq.TotalWeight();
			result.TotalRightWeight = (double)this.m_refTokSeq.TotalWeight();
			result.NumeratorWeight = num3;
			if (this.m_PG.ComparisonType == FuzzyComparisonType.Jaccard)
			{
				result.DenominatorWeight = num4;
				result.Similarity = WtJaccSim.CalculateSimlarity(num3, num4);
				return;
			}
			if (FuzzyComparisonType.LeftJaccardContainment == this.m_PG.ComparisonType)
			{
				result.DenominatorWeight = result.TotalLeftWeight;
				result.Similarity = WtJaccSim.CalculateSimlarity(num3, result.TotalLeftWeight);
				return;
			}
			throw new InvalidOperationException("Unexpected ComparisonType!");
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00022570 File Offset: 0x00020770
		private void PruneInputMultiRules()
		{
			for (int i = 0; i < this.m_inputTMList.Count; i++)
			{
				if (!this.m_inputTMList[i].IsUnitRule)
				{
					this.PruneInputMultiRule(this.m_inputTMList[i], i);
				}
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000225BC File Offset: 0x000207BC
		private void PruneInputMultiRule(WeightedTransformationMatch tm, int tranMatchSourceIndex)
		{
			this.m_tmpIntSet.Clear();
			int num = 0;
			if (tm.Transformation.To.Count > 0)
			{
				int num2 = this.m_ruleId2nodeId_lhs[tranMatchSourceIndex];
				for (int i = 0; i < tm.Transformation.To.Count; i++)
				{
					int num3 = tm.Transformation.To[i];
					int num4 = num2 + i;
					if (!this.m_PG.LhsDegree0_OverallGraph(num4))
					{
						this.m_tmpIntSet.Add(num3);
					}
					else
					{
						num += tm.Transformation.GetToWeight(i);
					}
				}
			}
			this.m_minWtHT_lhs.Add(tm.Position, tm.Transformation.From.Count, this.m_tmpIntSet, tranMatchSourceIndex, num);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0002268C File Offset: 0x0002088C
		private void CopyMultiRules(TokenSequence tokSeq, ArraySegmentBuilder<WeightedTransformationMatch> tranMatchList, IndexedSubList<TransformationMatch> tranMatchSublist, MinWtHashTable minwtHT)
		{
			minwtHT.Open();
			tranMatchSublist.BeginListSpecification();
			for (int i = 0; i < tranMatchList.Count; i++)
			{
				WeightedTransformationMatch weightedTransformationMatch = tranMatchList[i];
				if (!weightedTransformationMatch.IsUnitRule && minwtHT.ContainsId(i))
				{
					tranMatchSublist.Add((TransformationMatch)weightedTransformationMatch, i);
				}
			}
			tranMatchSublist.EndListSpecification();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000226E8 File Offset: 0x000208E8
		private void CopyMultiRules(ArraySegmentBuilder<WeightedTransformationMatch> tranMatchList, IndexedSubList<TransformationMatch> tranMatchSublist)
		{
			tranMatchSublist.BeginListSpecification();
			for (int i = 0; i < tranMatchList.Count; i++)
			{
				WeightedTransformationMatch weightedTransformationMatch = tranMatchList[i];
				if (!weightedTransformationMatch.IsUnitRule)
				{
					tranMatchSublist.Add((TransformationMatch)weightedTransformationMatch, i);
				}
			}
			tranMatchSublist.EndListSpecification();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00022730 File Offset: 0x00020930
		private void CopyMultiRules(ArraySegmentBuilder<TransformationMatch> tranMatchList, IndexedSubList<TransformationMatch> tranMatchSublist)
		{
			tranMatchSublist.BeginListSpecification();
			for (int i = 0; i < tranMatchList.Count; i++)
			{
				TransformationMatch transformationMatch = tranMatchList[i];
				if (!transformationMatch.Transformation.IsUnitRule)
				{
					tranMatchSublist.Add(transformationMatch, i);
				}
			}
			tranMatchSublist.EndListSpecification();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00022778 File Offset: 0x00020978
		private void SublistMultiRules(IndexedSubList<TransformationMatch> tranMatchSublist, MinWtHashTable minwtHT)
		{
			minwtHT.Open();
			tranMatchSublist.BeginSubListSpecification();
			while (tranMatchSublist.GetNext())
			{
				if (minwtHT.ContainsId(tranMatchSublist.CurrentTranMatchSourceIndex))
				{
					tranMatchSublist.SetCurrentValid();
				}
				else
				{
					tranMatchSublist.SetCurrentInvalid();
				}
			}
			tranMatchSublist.EndSubListSpecification();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000227B4 File Offset: 0x000209B4
		private void CopyMultiMatch(RuleApplEnumerator<TransformationMatch> multiRuleAppl, ArraySegmentBuilder<TransformationMatch> tranMatchListOut)
		{
			for (int i = 0; i < multiRuleAppl.NumRulesApplied; i++)
			{
				tranMatchListOut.Add(new TransformationMatch(multiRuleAppl.GetAppliedRule(i)));
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000227E4 File Offset: 0x000209E4
		private void ConstructSubGraph(Side side)
		{
			bool flag = side == Side.Left;
			RuleApplEnumerator<TransformationMatch> ruleApplEnumerator = (flag ? this.m_multiRuleAppl1 : this.m_multiRuleAppl2);
			int[] array = (flag ? this.m_ruleId2nodeId_lhs : this.m_ruleId2nodeId_rhs);
			TokenSequence tokenSequence = (flag ? this.m_inputTokSeq.Tokens : this.m_refTokSeq.Tokens);
			this.m_PG.OpenValidMarking(flag);
			int num = 0;
			for (int i = 0; i < ruleApplEnumerator.NumRulesApplied; i++)
			{
				int num2;
				TransformationMatch appliedRule = ruleApplEnumerator.GetAppliedRule(i, out num2);
				for (int j = num; j < appliedRule.Position; j++)
				{
					this.m_PG.SetValid(flag, j);
				}
				if (appliedRule.Transformation.To.Count > 0)
				{
					int num3 = array[num2];
					for (int k = 0; k < appliedRule.Transformation.To.Count; k++)
					{
						this.m_PG.SetValid(flag, num3 + k);
					}
				}
				num = appliedRule.Position + appliedRule.Transformation.From.Count;
			}
			for (int l = num; l < tokenSequence.Count; l++)
			{
				this.m_PG.SetValid(flag, l);
			}
			this.m_PG.CloseValidMarking(flag);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00022928 File Offset: 0x00020B28
		private void InitializeToken2Node(Side side)
		{
			if (side == Side.Left)
			{
				for (int i = 0; i < this.m_inputTokSeq.Count; i++)
				{
					int num = this.m_inputTokSeq.Tokens.Array[this.m_inputTokSeq.Tokens.Offset + i];
					this.m_PG.AddLhsNode(num, i, this.m_inputTokSeq.GetWeight(i));
					this.m_tok2NodeMap.Set(num, i, TransformationMatch.Empty.Position, TransformationMatch.Empty.Transformation);
				}
				for (int j = 0; j < this.m_inputTMList.Count; j++)
				{
					WeightedTransformationMatch weightedTransformationMatch = this.m_inputTMList.Array[j];
					WeightedTransformation transformation = weightedTransformationMatch.Transformation;
					if (weightedTransformationMatch.IsUnitRule)
					{
						int num2 = transformation.To.Array[transformation.To.Offset];
						this.m_tok2NodeMap.Set(num2, weightedTransformationMatch.Position, weightedTransformationMatch.Position, transformation.Transformation);
						this.m_PG.UpdateNodeMinWt(side, weightedTransformationMatch.Position, num2, transformation.GetToWeight(0), weightedTransformationMatch.Position, transformation.Transformation);
					}
				}
			}
			else
			{
				for (int k = 0; k < this.m_refTokSeq.Count; k++)
				{
					int num3 = this.m_refTokSeq.Tokens.Array[this.m_refTokSeq.Tokens.Offset + k];
					int weight = this.m_refTokSeq.GetWeight(k);
					this.m_PG.AddRhsNode(num3, k, weight);
					this.AddEdges(num3, weight, k, TransformationMatch.Empty.Position, TransformationMatch.Empty.Transformation);
				}
				for (int l = 0; l < this.m_refTMList.Count; l++)
				{
					WeightedTransformationMatch weightedTransformationMatch2 = this.m_refTMList.Array[l];
					WeightedTransformation transformation2 = weightedTransformationMatch2.Transformation;
					if (weightedTransformationMatch2.IsUnitRule)
					{
						int num4 = transformation2.To.Array[transformation2.To.Offset];
						int toWeight = transformation2.GetToWeight(0);
						this.m_PG.UpdateNodeMinWt(side, weightedTransformationMatch2.Position, num4, toWeight, weightedTransformationMatch2.Position, transformation2.Transformation);
						this.AddEdges(num4, toWeight, weightedTransformationMatch2.Position, weightedTransformationMatch2.Position, transformation2.Transformation);
					}
				}
			}
			ArraySegmentBuilder<WeightedTransformationMatch> arraySegmentBuilder = ((side == Side.Left) ? this.m_inputTMList : this.m_refTMList);
			for (int m = 0; m < arraySegmentBuilder.Count; m++)
			{
				if (!arraySegmentBuilder[m].IsUnitRule)
				{
					this.ProcessMultiRule(arraySegmentBuilder[m], m, side);
				}
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00022BEC File Offset: 0x00020DEC
		private void ProcessMultiRule(WeightedTransformationMatch tm, int tranMatchSourceIndex, Side side)
		{
			if (side == Side.Right)
			{
				this.m_tmpIntSet.Clear();
			}
			int num = 0;
			for (int i = 0; i < tm.Transformation.To.Count; i++)
			{
				int num2 = tm.Transformation.To[i];
				if (side == Side.Left)
				{
					int toWeight = tm.Transformation.GetToWeight(i);
					int num3 = this.m_PG.AddLhsNode(num2, -1, toWeight);
					this.m_tok2NodeMap.Set(num2, num3, TransformationMatch.Empty.Position, TransformationMatch.Empty.Transformation);
					if (i == 0)
					{
						this.MapRuleId2NodeId(ref this.m_ruleId2nodeId_lhs, tranMatchSourceIndex, num3);
					}
				}
				else
				{
					int toWeight2 = tm.Transformation.GetToWeight(i);
					int num4 = this.m_PG.AddRhsNode(num2, -1, toWeight2);
					if (this.m_tok2NodeMap.ContainsToken(num2))
					{
						this.m_tmpIntSet.Add(num2);
					}
					else
					{
						num += toWeight2;
					}
					if (i == 0)
					{
						this.MapRuleId2NodeId(ref this.m_ruleId2nodeId_rhs, tranMatchSourceIndex, num4);
					}
					this.AddEdges(num2, toWeight2, num4, TransformationMatch.Empty.Position, TransformationMatch.Empty.Transformation);
				}
			}
			if (side == Side.Right)
			{
				this.m_minWtHT_rhs.Add(tm.Position, tm.Transformation.From.Count, this.m_tmpIntSet, tranMatchSourceIndex, num);
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00022D44 File Offset: 0x00020F44
		private void AddEdges(int tok, int wt, int rhsNodeId, int tmPos, Transformation tmTran)
		{
			int[] array;
			TransformationMatch[] array2;
			int num;
			this.m_tok2NodeMap.GetNodes(tok, out array, out array2, out num);
			for (int i = 0; i < num; i++)
			{
				this.m_PG.AddEdge(array[i], rhsNodeId, tok, wt, array2[i].Position, array2[i].Transformation, tmPos, tmTran);
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00022D9C File Offset: 0x00020F9C
		private void MapRuleId2NodeId(ref int[] ruleId2nodeId, int ruleid, int nodeid)
		{
			if (ruleId2nodeId.Length <= ruleid)
			{
				int num = 2 * ruleid;
				Array.Resize<int>(ref ruleId2nodeId, num);
			}
			ruleId2nodeId[ruleid] = nodeid;
		}

		// Token: 0x0400031B RID: 795
		private const double ScoreWidth = 0.01;

		// Token: 0x0400031C RID: 796
		private const int InitialGraphCapacity = 16;

		// Token: 0x0400031D RID: 797
		private const int InitialNodeCount = 2;

		// Token: 0x0400031E RID: 798
		private const int Null = -1;

		// Token: 0x0400031F RID: 799
		private WeightedTokenSequence m_inputTokSeq;

		// Token: 0x04000320 RID: 800
		private WeightedTokenSequence m_refTokSeq;

		// Token: 0x04000321 RID: 801
		private ArraySegmentBuilder<WeightedTransformationMatch> m_inputTMList;

		// Token: 0x04000322 RID: 802
		private ArraySegmentBuilder<WeightedTransformationMatch> m_refTMList;

		// Token: 0x04000323 RID: 803
		private RuleApplEnumerator<TransformationMatch> m_multiRuleAppl1;

		// Token: 0x04000324 RID: 804
		private RuleApplEnumerator<TransformationMatch> m_multiRuleAppl2;

		// Token: 0x04000325 RID: 805
		private IndexedSubList<TransformationMatch> m_tranMatchSublist1;

		// Token: 0x04000326 RID: 806
		private IndexedSubList<TransformationMatch> m_tranMatchSublist2;

		// Token: 0x04000327 RID: 807
		private PrepareGraph m_PG;

		// Token: 0x04000328 RID: 808
		private Token2NodeMap m_tok2NodeMap;

		// Token: 0x04000329 RID: 809
		private int[] m_ruleId2nodeId_lhs;

		// Token: 0x0400032A RID: 810
		private int[] m_ruleId2nodeId_rhs;

		// Token: 0x0400032B RID: 811
		private MinWtHashTable m_minWtHT_rhs;

		// Token: 0x0400032C RID: 812
		private MinWtHashTable m_minWtHT_lhs;

		// Token: 0x0400032D RID: 813
		private IntVector m_tmpIntSet;

		// Token: 0x0400032E RID: 814
		private bool m_delayResetLeftRecordPending;

		// Token: 0x0400032F RID: 815
		private bool m_delayResetRightRecordPending;

		// Token: 0x04000330 RID: 816
		private bool m_quickIntersectionResultsAreCachedAndValid;

		// Token: 0x04000331 RID: 817
		private double m_quickIntersection;

		// Token: 0x04000332 RID: 818
		private double m_quickUnion;
	}
}
