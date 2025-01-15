using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public sealed class PrefixSignatureGenerator : IOneDimSignatureGenerator, IEnumerable<int>, IEnumerable, ISignatureGeneratorInitialize, IThreshold
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001DF89 File Offset: 0x0001C189
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001DF91 File Offset: 0x0001C191
		public double Threshold { get; set; }

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001DF9C File Offset: 0x0001C19C
		public PrefixSignatureGenerator()
		{
			this.m_signatures = new FastIntHashSet();
			this.m_multiRules = new IndexedSubList<WeightedTransformationMatch>();
			this.m_derivedTokenBuf = new DerivedToken[1];
			this.m_priorityQueue = new Heap<DerivedToken>(new Comparison<DerivedToken>(PrefixSignatureGenerator.CompareDerivedTokens));
			this.m_ruleApplEnumerator = new RuleApplEnumerator<WeightedTransformationMatch>();
			this.m_posInfo = new PrefixSignatureGenerator.PositionInfo[1];
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001DFFF File Offset: 0x0001C1FF
		public PrefixSignatureGenerator(ITokenToClusterMap clustering)
			: this()
		{
			this.Initialize(clustering);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001E00E File Offset: 0x0001C20E
		public void Initialize(ITokenToClusterMap clustering)
		{
			this.m_tokenClusterMap = clustering;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001E018 File Offset: 0x0001C218
		private static int CompareDerivedTokens(DerivedToken n1, DerivedToken n2)
		{
			if (n1.weight == n2.weight)
			{
				if (n1.token < n2.token)
				{
					return 1;
				}
				if (n1.token != n2.token)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (n1.weight <= n2.weight)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001E066 File Offset: 0x0001C266
		public int Indexes
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001E069 File Offset: 0x0001C269
		public void ResetDimension(int signIdx)
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001E06B File Offset: 0x0001C26B
		public void Reset(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			this.m_signatures.Clear();
			this.GeneratePrefixFilterSignatures(tokenSequence, matchingRules);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001E080 File Offset: 0x0001C280
		public IEnumerator<int> GetEnumerator()
		{
			return this.m_signatures.GetEnumerator();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001E08D File Offset: 0x0001C28D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_signatures.GetEnumerator();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001E09C File Offset: 0x0001C29C
		private void GeneratePrefixFilterSignatures(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			this.GenerateGroups(tokenSequence, matchingRules);
			this.CopyMultiRules(matchingRules, this.m_multiRules);
			this.m_ruleApplEnumerator.Reset(tokenSequence.Tokens, this.m_multiRules);
			if (tokenSequence.Count > 0)
			{
				while (this.m_ruleApplEnumerator.GetNextRuleApplication())
				{
					this.GeneratePrefixFilterSignatures(tokenSequence, matchingRules, this.m_ruleApplEnumerator);
				}
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		private void GeneratePrefixFilterSignatures(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules, RuleApplEnumerator<WeightedTransformationMatch> ruleApplication)
		{
			int num = 0;
			int num2 = 0;
			this.m_priorityQueue.Clear();
			for (int i = 0; i < tokenSequence.Count; i++)
			{
				if (!ruleApplication.IsAffected(i))
				{
					num2 += this.m_derivedTokenBuf[this.m_posInfo[i].rhsTokenBufBeginIdx].weight;
					this.m_posInfo[i].numInPrefix = 0;
					for (int j = this.m_posInfo[i].rhsTokenBufBeginIdx; j < this.m_posInfo[i].rhsTokenBufEndIdx; j++)
					{
						this.m_priorityQueue.BulkAdd(this.m_derivedTokenBuf[j]);
					}
				}
			}
			for (int k = 0; k < ruleApplication.NumRulesApplied; k++)
			{
				WeightedTransformationMatch appliedRule = ruleApplication.GetAppliedRule(k);
				Transformation transformation = (Transformation)appliedRule.Transformation;
				for (int l = 0; l < transformation.To.Count; l++)
				{
					int toWeight = appliedRule.Transformation.GetToWeight(l);
					this.m_priorityQueue.BulkAdd(new DerivedToken(transformation.To[l], toWeight, k, false));
					num2 += toWeight;
				}
			}
			this.m_priorityQueue.Heapify();
			while ((double)num * this.Threshold <= (1.0 - this.Threshold) * (double)num2 && this.m_priorityQueue.Count > 0)
			{
				DerivedToken derivedToken = this.m_priorityQueue.Pop();
				int tokenClusterMapping = this.m_tokenClusterMap.GetTokenClusterMapping(derivedToken.token);
				if (!this.m_signatures.Contains(tokenClusterMapping))
				{
					this.m_signatures.Add(tokenClusterMapping);
				}
				if (derivedToken.isUnit)
				{
					int rulePos = derivedToken.rulePos;
					PrefixSignatureGenerator.PositionInfo[] posInfo = this.m_posInfo;
					int num3 = rulePos;
					posInfo[num3].numInPrefix = posInfo[num3].numInPrefix + 1;
					num2 -= derivedToken.weight;
					if (this.m_posInfo[rulePos].numInPrefix == this.m_posInfo[rulePos].rhsTokenBufEndIdx - this.m_posInfo[rulePos].rhsTokenBufBeginIdx)
					{
						num += derivedToken.weight;
					}
					else
					{
						num2 += this.m_derivedTokenBuf[this.m_posInfo[rulePos].numInPrefix + this.m_posInfo[rulePos].rhsTokenBufBeginIdx].weight;
					}
				}
				else
				{
					num += derivedToken.weight;
					num2 -= derivedToken.weight;
				}
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001E378 File Offset: 0x0001C578
		private void GenerateGroups(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchList)
		{
			this.m_numDerivedTokens = 0;
			for (int i = 0; i < tokenSequence.Count; i++)
			{
				this.m_derivedTokenBuf[this.m_numDerivedTokens].token = tokenSequence.Tokens[i];
				this.m_derivedTokenBuf[this.m_numDerivedTokens].weight = tokenSequence.GetWeight(i);
				this.m_derivedTokenBuf[this.m_numDerivedTokens].isUnit = true;
				this.m_derivedTokenBuf[this.m_numDerivedTokens].rulePos = i;
				this.m_numDerivedTokens++;
				if (this.m_numDerivedTokens == this.m_derivedTokenBuf.Length)
				{
					this.GrowRhsTokenBuf();
				}
			}
			for (int j = 0; j < matchList.Count; j++)
			{
				WeightedTransformationMatch weightedTransformationMatch = matchList[j];
				if (weightedTransformationMatch.IsUnitRule)
				{
					this.m_derivedTokenBuf[this.m_numDerivedTokens].token = weightedTransformationMatch.Transformation.To[0];
					this.m_derivedTokenBuf[this.m_numDerivedTokens].weight = weightedTransformationMatch.Transformation.GetToWeight(0);
					this.m_derivedTokenBuf[this.m_numDerivedTokens].isUnit = true;
					this.m_derivedTokenBuf[this.m_numDerivedTokens].rulePos = weightedTransformationMatch.Position;
					this.m_numDerivedTokens++;
					if (this.m_numDerivedTokens == this.m_derivedTokenBuf.Length)
					{
						this.GrowRhsTokenBuf();
					}
				}
			}
			this.SortRhsTokenBuf();
			if (this.m_posInfo.Length < tokenSequence.Count)
			{
				this.GrowPosInfoBuf(tokenSequence.Count);
			}
			int num = 0;
			for (int k = 0; k < tokenSequence.Count; k++)
			{
				this.m_posInfo[k].rhsTokenBufBeginIdx = num;
				while (num < this.m_numDerivedTokens && this.m_derivedTokenBuf[num].rulePos == k)
				{
					num++;
				}
				this.m_posInfo[k].rhsTokenBufEndIdx = num;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001E590 File Offset: 0x0001C790
		private void SortRhsTokenBuf()
		{
			for (int i = 0; i < this.m_numDerivedTokens; i++)
			{
				for (int j = i + 1; j < this.m_numDerivedTokens; j++)
				{
					if (this.m_derivedTokenBuf[j].rulePos < this.m_derivedTokenBuf[i].rulePos)
					{
						DerivedToken derivedToken = this.m_derivedTokenBuf[i];
						this.m_derivedTokenBuf[i] = this.m_derivedTokenBuf[j];
						this.m_derivedTokenBuf[j] = derivedToken;
					}
					else if (this.m_derivedTokenBuf[j].rulePos == this.m_derivedTokenBuf[i].rulePos)
					{
						if (this.m_derivedTokenBuf[j].weight > this.m_derivedTokenBuf[i].weight)
						{
							DerivedToken derivedToken2 = this.m_derivedTokenBuf[i];
							this.m_derivedTokenBuf[i] = this.m_derivedTokenBuf[j];
							this.m_derivedTokenBuf[j] = derivedToken2;
						}
						else if (this.m_derivedTokenBuf[j].weight == this.m_derivedTokenBuf[i].weight && this.m_derivedTokenBuf[j].token < this.m_derivedTokenBuf[i].token)
						{
							DerivedToken derivedToken3 = this.m_derivedTokenBuf[i];
							this.m_derivedTokenBuf[i] = this.m_derivedTokenBuf[j];
							this.m_derivedTokenBuf[j] = derivedToken3;
						}
					}
				}
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001E724 File Offset: 0x0001C924
		private void GrowRhsTokenBuf()
		{
			int num = (int)((double)this.m_derivedTokenBuf.Length * 0.5) + 1 + this.m_derivedTokenBuf.Length;
			Array.Resize<DerivedToken>(ref this.m_derivedTokenBuf, num);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001E760 File Offset: 0x0001C960
		private void GrowPosInfoBuf(int length)
		{
			int num = (int)((double)this.m_posInfo.Length * 0.5) + 1 + this.m_posInfo.Length;
			if (num < length)
			{
				num = length;
			}
			Array.Resize<PrefixSignatureGenerator.PositionInfo>(ref this.m_posInfo, num);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
		private void CopyMultiRules(IList<WeightedTransformationMatch> tranMatchList, IndexedSubList<WeightedTransformationMatch> tranMatchSublist)
		{
			tranMatchSublist.BeginListSpecification();
			for (int i = 0; i < tranMatchList.Count; i++)
			{
				if (!tranMatchList[i].IsUnitRule)
				{
					tranMatchSublist.Add(tranMatchList[i], i);
				}
			}
			tranMatchSublist.EndListSpecification();
		}

		// Token: 0x04000279 RID: 633
		private const int expTokenSequenceSize = 2048;

		// Token: 0x0400027A RID: 634
		private ITokenToClusterMap m_tokenClusterMap;

		// Token: 0x0400027B RID: 635
		private IndexedSubList<WeightedTransformationMatch> m_multiRules;

		// Token: 0x0400027C RID: 636
		private RuleApplEnumerator<WeightedTransformationMatch> m_ruleApplEnumerator;

		// Token: 0x0400027D RID: 637
		private FastIntHashSet m_signatures;

		// Token: 0x0400027E RID: 638
		private DerivedToken[] m_derivedTokenBuf;

		// Token: 0x0400027F RID: 639
		private int m_numDerivedTokens;

		// Token: 0x04000280 RID: 640
		private PrefixSignatureGenerator.PositionInfo[] m_posInfo;

		// Token: 0x04000281 RID: 641
		private Heap<DerivedToken> m_priorityQueue;

		// Token: 0x02000170 RID: 368
		[Serializable]
		private struct PositionInfo
		{
			// Token: 0x040005DD RID: 1501
			public int rhsTokenBufBeginIdx;

			// Token: 0x040005DE RID: 1502
			public int rhsTokenBufEndIdx;

			// Token: 0x040005DF RID: 1503
			public int numInPrefix;
		}
	}
}
