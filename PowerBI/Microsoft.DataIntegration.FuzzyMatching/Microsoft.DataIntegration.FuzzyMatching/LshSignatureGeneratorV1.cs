using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	internal sealed class LshSignatureGeneratorV1 : IMultiDimSignatureGenerator, IOneDimSignatureGenerator, IEnumerable<int>, IEnumerable, ISignatureGeneratorInitialize
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x0001A7EC File Offset: 0x000189EC
		private int CompareRuleMinHash(int i, int j)
		{
			if (this.m_ruleMinHashCache[i].minHashValue >= this.m_ruleMinHashCache[j].minHashValue && (this.m_ruleMinHashCache[i].minHashValue != this.m_ruleMinHashCache[j].minHashValue || this.m_ruleMinHashCache[i].minHashToken >= this.m_ruleMinHashCache[j].minHashToken))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001A850 File Offset: 0x00018A50
		public static int NumHashTables(int nDimensions, double minThreshold, double successProb)
		{
			if (minThreshold == 1.0)
			{
				return 1;
			}
			double num = Math.Log(1.0 - successProb);
			double num2 = Math.Log(1.0 - Math.Pow(minThreshold, (double)nDimensions));
			if (0.0 == num2)
			{
				return 100;
			}
			return Convert.ToInt32(Math.Ceiling(num / num2));
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public static double SuccessProbability(int nDimensions, int nHashTables, double minThreshold)
		{
			return 1.0 - Math.Pow(1.0 - Math.Pow(minThreshold, (double)nDimensions), (double)nHashTables);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001A8D5 File Offset: 0x00018AD5
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0001A8DD File Offset: 0x00018ADD
		public int NumHashtables { get; set; }

		// Token: 0x06000633 RID: 1587 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		public LshSignatureGeneratorV1(int NumHashtables, int numDimensions, int seed, ITokenToClusterMap tokenClusterer)
		{
			NumHashtables = NumHashtables;
			this.m_NumHashtables = NumHashtables;
			this.m_numDimensions = numDimensions;
			this.m_unitRuleGroupList = new UnitRuleGroup[0];
			this.m_globalUnitRuleList = new UnitRule[0];
			this.m_minHashDims = new MinHashDim[NumHashtables * numDimensions];
			this.m_globalRuleMinHashList = new RuleMinHash[0];
			this.m_ruleMinHashPQ = new Heap<int>(new Comparison<int>(this.CompareRuleMinHash));
			this.m_ruleApplEnumerator = new RuleApplEnumerator<WeightedTransformationMatch>();
			this.m_signatures = new IntHashSetEnumerable(10000);
			this.m_multiRulesSublist = new IndexedSubList<WeightedTransformationMatch>();
			this.m_unitRulesSublist = new IndexedSubList<WeightedTransformationMatch>();
			for (int i = 0; i < numDimensions * NumHashtables; i++)
			{
				this.m_minHashDims[i] = new MinHashDim(this.m_globalRuleMinHashList);
			}
			this.InitializeHashFunctions(seed);
			this.MINHASHFN = new double[1000004];
			int num = 1;
			while ((long)num <= 1000003L)
			{
				this.MINHASHFN[num] = LshSignatureGeneratorV1.LogPRIME - Math.Log((double)num);
				num++;
			}
			this.MINHASHFN[0] = LshSignatureGeneratorV1.LogPRIME;
			this.Initialize(tokenClusterer);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001AA0D File Offset: 0x00018C0D
		public void Initialize(ITokenToClusterMap clustering)
		{
			this.m_tokenClusterer = clustering;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001AA18 File Offset: 0x00018C18
		public void Reset(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			this.m_tokenSequence = tokenSequence.Tokens;
			this.m_matchingRules = matchingRules;
			if (tokenSequence.Count == 1 && this.m_UnitLengthOptFlag)
			{
				return;
			}
			this.CopyMultiRules(this.m_matchingRules, this.m_multiRulesSublist);
			this.CopyUnitRules(this.m_matchingRules, this.m_unitRulesSublist);
			this.GenerateUnitRuleGroups(tokenSequence, matchingRules);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001AA78 File Offset: 0x00018C78
		public void Reset(int signIdx)
		{
			if (this.m_tokenSequence.Count == 1 && this.m_UnitLengthOptFlag)
			{
				this.GenerateSignaturesForUnitLength(signIdx);
				return;
			}
			this.m_ruleApplEnumerator.Reset(this.m_tokenSequence, this.m_multiRulesSublist);
			this.m_signatures.Clear();
			if (this.m_tokenSequence.Count > 0)
			{
				while (this.m_ruleApplEnumerator.GetNextRuleApplication())
				{
					this.GenerateMinHashDims(this.m_matchingRules, this.m_ruleApplEnumerator, signIdx);
					this.GenerateSignatures(signIdx);
				}
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001AAFC File Offset: 0x00018CFC
		public IEnumerator<int> GetEnumerator()
		{
			return this.m_signatures.GetEnumerator();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001AB09 File Offset: 0x00018D09
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_signatures.GetEnumerator();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001AB18 File Offset: 0x00018D18
		private void InitializeHashFunctions(int seed)
		{
			int num = this.m_numDimensions * this.m_NumHashtables;
			Random random = new Random(seed);
			this.m_a = new ulong[num];
			this.m_b = new ulong[num];
			for (int i = 0; i < num; i++)
			{
				this.m_a[i] = (ulong)((long)random.Next(1000003));
				this.m_b[i] = (ulong)((long)random.Next(1000003));
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001AB88 File Offset: 0x00018D88
		private void GenerateUnitRuleGroups(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchList)
		{
			if (this.m_unitRuleGroupList.Length < tokenSequence.Count)
			{
				this.GrowUnitRuleGroupList(tokenSequence.Count);
			}
			this.m_globalUnitRuleListSize = 0;
			int i = 0;
			for (int num = this.m_unitRulesSublist.GetFirstMatch(); num != -1; num = this.m_unitRulesSublist.GetSuccessorOverlap(num))
			{
				WeightedTransformationMatch weightedTransformationMatch = this.m_unitRulesSublist[num];
				while (i <= weightedTransformationMatch.Position)
				{
					ListSpan<UnitRule> ruleList = this.m_unitRuleGroupList[i].ruleList;
					ruleList.endPos = (ruleList.beginPos = this.m_globalUnitRuleListSize);
					int num2 = tokenSequence.Tokens[i];
					this.AddUnitRule(ruleList, num2, tokenSequence.GetWeight(i));
					i++;
				}
				this.AddUnitRule(this.m_unitRuleGroupList[weightedTransformationMatch.Position].ruleList, weightedTransformationMatch.Transformation.To[0], weightedTransformationMatch.Transformation.GetToWeight(0));
			}
			while (i < tokenSequence.Count)
			{
				ListSpan<UnitRule> ruleList2 = this.m_unitRuleGroupList[i].ruleList;
				ruleList2.endPos = (ruleList2.beginPos = this.m_globalUnitRuleListSize);
				int num3 = tokenSequence.Tokens[i];
				this.AddUnitRule(ruleList2, num3, tokenSequence.GetWeight(i));
				i++;
			}
			this.m_unitRuleGroupListSize = tokenSequence.Count;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001ACE4 File Offset: 0x00018EE4
		private void GenerateUnitRuleGroup(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchList, int pos)
		{
			ListSpan<UnitRule> ruleList = this.m_unitRuleGroupList[pos].ruleList;
			ruleList.endPos = (ruleList.beginPos = this.m_globalUnitRuleListSize);
			int num = tokenSequence.Tokens[pos];
			this.AddUnitRule(ruleList, num, tokenSequence.GetWeight(pos));
			for (int i = 0; i < matchList.Count; i++)
			{
				WeightedTransformationMatch weightedTransformationMatch = matchList[i];
				if (weightedTransformationMatch.Position == pos && weightedTransformationMatch.IsUnitRule)
				{
					this.AddUnitRule(ruleList, weightedTransformationMatch.Transformation.To[0], matchList[i].Transformation.GetToWeight(0));
				}
			}
			this.m_globalUnitRuleListSize = ruleList.endPos;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001ADA0 File Offset: 0x00018FA0
		private void AddUnitRule(ListSpan<UnitRule> unitRuleList, int token, int weight)
		{
			if (unitRuleList.endPos == this.m_globalUnitRuleList.Length)
			{
				this.GrowUnitRuleList();
			}
			unitRuleList[unitRuleList.Size].Reset(token, weight, this.m_tokenClusterer.GetTokenClusterMapping(token));
			unitRuleList.endPos++;
			this.m_globalUnitRuleListSize++;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001AE00 File Offset: 0x00019000
		private void GenerateMinHashDims(ArraySegmentBuilder<WeightedTransformationMatch> matchingRules, RuleApplEnumerator<WeightedTransformationMatch> ruleApplication, int signIdx)
		{
			this.m_globalRuleMinHashListSize = 0;
			for (int i = signIdx * this.m_numDimensions; i < (signIdx + 1) * this.m_numDimensions; i++)
			{
				this.GenerateMinHashDim(i, matchingRules, ruleApplication);
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001AE39 File Offset: 0x00019039
		private void ResetNewRuleMinHash()
		{
			this.m_nextRuleMinHash = 0;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001AE44 File Offset: 0x00019044
		private RuleMinHash NewRuleMinHash()
		{
			if (this.m_nextRuleMinHash == this.m_ruleMinHashCache.Length)
			{
				Array.Resize<RuleMinHash>(ref this.m_ruleMinHashCache, Math.Max(1, this.m_ruleMinHashCache.Length * 2));
				for (int i = this.m_nextRuleMinHash; i < this.m_ruleMinHashCache.Length; i++)
				{
					this.m_ruleMinHashCache[i] = new RuleMinHash();
				}
			}
			RuleMinHash[] ruleMinHashCache = this.m_ruleMinHashCache;
			int nextRuleMinHash = this.m_nextRuleMinHash;
			this.m_nextRuleMinHash = nextRuleMinHash + 1;
			return ruleMinHashCache[nextRuleMinHash];
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001AEBC File Offset: 0x000190BC
		private void GenerateMinHashDim(int dim, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules, RuleApplEnumerator<WeightedTransformationMatch> ruleApplication)
		{
			this.m_minHashDims[dim].ruleMinHashList.beginPos = this.m_globalRuleMinHashListSize;
			this.m_ruleMinHashPQ.ClearFast();
			this.ResetNewRuleMinHash();
			for (int i = 0; i < this.m_unitRuleGroupListSize; i++)
			{
				if (!ruleApplication.IsAffected(i))
				{
					UnitRuleGroup unitRuleGroup = this.m_unitRuleGroupList[i];
					for (int j = 0; j < unitRuleGroup.ruleList.Size; j++)
					{
						UnitRule unitRule = unitRuleGroup.ruleList[j];
						RuleMinHash ruleMinHash = this.NewRuleMinHash();
						ruleMinHash.minHashValue = this.GetMinHash3(dim, unitRule.tokenCluster, unitRule.weight);
						ruleMinHash.minHashToken = unitRule.token;
						ruleMinHash.isUnitRule = true;
						ruleMinHash.groupId = i;
						ruleMinHash.ruleId = j;
						this.m_ruleMinHashPQ.BulkAdd(this.m_nextRuleMinHash - 1);
					}
				}
			}
			for (int k = 0; k < this.m_ruleApplEnumerator.NumRulesApplied; k++)
			{
				RuleMinHash ruleMinHash2 = this.NewRuleMinHash();
				ruleMinHash2.minHashValue = this.GetMinHash(dim, matchingRules, this.m_ruleApplEnumerator.GetAppliedRule(k), out ruleMinHash2.minHashToken);
				ruleMinHash2.isUnitRule = false;
				ruleMinHash2.groupId = k;
				ruleMinHash2.ruleId = 0;
				this.m_ruleMinHashPQ.BulkAdd(this.m_nextRuleMinHash - 1);
			}
			this.m_ruleMinHashPQ.Heapify();
			for (int l = 0; l < this.m_unitRuleGroupListSize; l++)
			{
				this.m_unitRuleGroupList[l].numNotCovered = this.m_unitRuleGroupList[l].ruleList.Size;
			}
			RuleMinHash ruleMinHash3;
			do
			{
				if (this.m_globalRuleMinHashListSize == this.m_globalRuleMinHashList.Length)
				{
					this.GrowRuleMinHashList();
				}
				ruleMinHash3 = this.m_ruleMinHashCache[this.m_ruleMinHashPQ.Pop()];
				this.m_globalRuleMinHashList[this.m_globalRuleMinHashListSize].Copy(ruleMinHash3);
				this.m_globalRuleMinHashListSize++;
				if (!ruleMinHash3.isUnitRule)
				{
					break;
				}
				this.m_unitRuleGroupList[ruleMinHash3.groupId].numNotCovered--;
			}
			while (this.m_unitRuleGroupList[ruleMinHash3.groupId].numNotCovered != 0);
			this.m_minHashDims[dim].ruleMinHashList.endPos = this.m_globalRuleMinHashListSize;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001B0E4 File Offset: 0x000192E4
		private void GenerateSignatures(int signIdx)
		{
			int num = signIdx * this.m_numDimensions;
			int num2 = num + this.m_numDimensions;
			this.InitUnitRules();
			for (int i = num; i < num2; i++)
			{
				this.m_minHashDims[i].curIdx = -1;
			}
			if (!this.GetNext(num, num, num2))
			{
				return;
			}
			int num3 = this.GenerateSignature(num, num2);
			if (!this.m_signatures.Contains(num3))
			{
				this.m_signatures.Add(num3);
			}
			while (this.GetNext(num2 - 1, num, num2))
			{
				num3 = this.GenerateSignature(num, num2);
				if (!this.m_signatures.Contains(num3))
				{
					this.m_signatures.Add(num3);
				}
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001B184 File Offset: 0x00019384
		private int GenerateSignature(int beginDim, int endDim)
		{
			int num = 0;
			for (int i = beginDim; i < endDim; i++)
			{
				num = Utilities.GetHashCode(num + this.m_tokenClusterer.GetTokenClusterMapping(this.m_minHashDims[i].ruleMinHashList[this.m_minHashDims[i].curIdx].minHashToken));
			}
			return num;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001B1D8 File Offset: 0x000193D8
		private bool GetNext(int curDim, int beginDim, int endDim)
		{
			while (curDim >= beginDim && curDim < endDim)
			{
				MinHashDim minHashDim = this.m_minHashDims[curDim];
				if (minHashDim.curIdx >= 0)
				{
					this.UnApply(minHashDim.ruleMinHashList[minHashDim.curIdx], curDim);
					if (!this.NotApply(minHashDim.ruleMinHashList[minHashDim.curIdx], curDim))
					{
						this.UnNotApply(curDim);
						curDim--;
						continue;
					}
				}
				minHashDim.curIdx++;
				if (this.Apply(minHashDim.ruleMinHashList[minHashDim.curIdx], curDim))
				{
					curDim++;
				}
				else if (!this.NotApply(minHashDim.ruleMinHashList[minHashDim.curIdx], curDim))
				{
					this.UnNotApply(curDim);
					curDim--;
				}
			}
			return curDim == endDim;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001B2A0 File Offset: 0x000194A0
		private int GetMinHash(int dim, int token, int minWeight, int maxWeight, out int minHashUptoMinWeight)
		{
			int num = int.MaxValue;
			for (int i = 0; i < minWeight; i++)
			{
				token = Utilities.GetHashCode(token + i);
				int num2 = (int)((this.m_a[dim] * (ulong)((long)token) + this.m_b[dim]) % 1000003UL);
				if (num2 < num)
				{
					num = num2;
				}
			}
			minHashUptoMinWeight = num;
			for (int j = minWeight; j < maxWeight; j++)
			{
				token = Utilities.GetHashCode(token + j);
				int num3 = (int)((this.m_a[dim] * (ulong)((long)token) + this.m_b[dim]) % 1000003UL);
				if (num3 < num)
				{
					num = num3;
				}
			}
			return num;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001B330 File Offset: 0x00019530
		private int GetMinHash(int dim, int token, int weight)
		{
			int num = int.MaxValue;
			for (int i = 0; i < weight; i++)
			{
				token = Utilities.GetHashCode(token + i);
				int num2 = (int)((this.m_a[dim] * (ulong)((long)token) + this.m_b[dim]) % 1000003UL);
				if (num2 < num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001B380 File Offset: 0x00019580
		private double GetMinHash2(int dim, int token, int weight)
		{
			int tokenClusterMapping = this.m_tokenClusterer.GetTokenClusterMapping(token);
			int num = 1 + (int)((this.m_a[dim] * (ulong)((long)tokenClusterMapping) + this.m_b[dim]) % 1000003UL);
			return this.MINHASHFN[num] / (double)weight;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001B3C4 File Offset: 0x000195C4
		private double GetMinHash3(int dim, int cluster, int weight)
		{
			int num = 1 + (int)((this.m_a[dim] * (ulong)((long)cluster) + this.m_b[dim]) % 1000003UL);
			return this.MINHASHFN[num] / (double)weight;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001B3FC File Offset: 0x000195FC
		private double GetMinHash(int dim, ArraySegmentBuilder<WeightedTransformationMatch> matchList, WeightedTransformationMatch match, out int token)
		{
			double num = double.MaxValue;
			TokenSequence to = match.Transformation.To;
			token = 0;
			for (int i = 0; i < to.Count; i++)
			{
				double minHash = this.GetMinHash2(dim, to[i], match.Transformation.GetToWeight(i));
				if (minHash < num)
				{
					num = minHash;
					token = to[i];
				}
			}
			return num;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001B468 File Offset: 0x00019668
		private double GetMinHash(int dim, WeightedTokenSequence tokenSequence, out int token)
		{
			double num = double.MaxValue;
			token = 0;
			for (int i = 0; i < tokenSequence.Count; i++)
			{
				double minHash = this.GetMinHash2(dim, tokenSequence.Tokens[i], tokenSequence.GetWeight(i));
				if (minHash < num)
				{
					num = minHash;
					token = tokenSequence.Tokens[i];
				}
			}
			return num;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001B4C8 File Offset: 0x000196C8
		private void UnApply(RuleMinHash ruleMinHash, int curDim)
		{
			if (!ruleMinHash.isUnitRule)
			{
				return;
			}
			UnitRuleGroup unitRuleGroup = this.m_unitRuleGroupList[ruleMinHash.groupId];
			UnitRule unitRule = unitRuleGroup.ruleList[ruleMinHash.ruleId];
			if (unitRule.statusDim == curDim && unitRule.status == UnitRuleStatus.APPLIED)
			{
				unitRuleGroup.bApplied = false;
				unitRuleGroup.ruleList[ruleMinHash.ruleId].status = UnitRuleStatus.FREE;
				unitRuleGroup.numFree++;
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001B53C File Offset: 0x0001973C
		private void UnNotApply(int curDim)
		{
			MinHashDim minHashDim = this.m_minHashDims[curDim];
			for (int i = minHashDim.curIdx; i >= 0; i--)
			{
				RuleMinHash ruleMinHash = minHashDim.ruleMinHashList[i];
				if (ruleMinHash.isUnitRule)
				{
					UnitRuleGroup unitRuleGroup = this.m_unitRuleGroupList[ruleMinHash.groupId];
					UnitRule unitRule = unitRuleGroup.ruleList[ruleMinHash.ruleId];
					if (unitRule.statusDim == curDim && unitRule.status == UnitRuleStatus.NOTAPPLIED)
					{
						unitRule.status = UnitRuleStatus.FREE;
						unitRuleGroup.numFree++;
					}
				}
			}
			minHashDim.curIdx = -1;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001B5CC File Offset: 0x000197CC
		private bool NotApply(RuleMinHash ruleMinHash, int curDim)
		{
			if (!ruleMinHash.isUnitRule)
			{
				return false;
			}
			UnitRuleGroup unitRuleGroup = this.m_unitRuleGroupList[ruleMinHash.groupId];
			UnitRule unitRule = unitRuleGroup.ruleList[ruleMinHash.ruleId];
			if (unitRule.status == UnitRuleStatus.APPLIED)
			{
				return false;
			}
			if (unitRule.status == UnitRuleStatus.NOTAPPLIED)
			{
				return true;
			}
			if (unitRuleGroup.bApplied)
			{
				return true;
			}
			if (unitRuleGroup.numFree == 1)
			{
				return false;
			}
			unitRule.status = UnitRuleStatus.NOTAPPLIED;
			unitRule.statusDim = curDim;
			unitRuleGroup.numFree--;
			return true;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001B64C File Offset: 0x0001984C
		private bool Apply(RuleMinHash ruleMinHash, int curDim)
		{
			if (!ruleMinHash.isUnitRule)
			{
				return true;
			}
			UnitRuleGroup unitRuleGroup = this.m_unitRuleGroupList[ruleMinHash.groupId];
			UnitRule unitRule = unitRuleGroup.ruleList[ruleMinHash.ruleId];
			if (unitRule.status == UnitRuleStatus.APPLIED)
			{
				return true;
			}
			if (unitRule.status == UnitRuleStatus.NOTAPPLIED)
			{
				return false;
			}
			if (unitRuleGroup.bApplied)
			{
				return false;
			}
			unitRule.status = UnitRuleStatus.APPLIED;
			unitRule.statusDim = curDim;
			unitRuleGroup.numFree--;
			unitRuleGroup.bApplied = true;
			return true;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001B6C8 File Offset: 0x000198C8
		private void InitUnitRules()
		{
			for (int i = 0; i < this.m_unitRuleGroupListSize; i++)
			{
				UnitRuleGroup unitRuleGroup = this.m_unitRuleGroupList[i];
				unitRuleGroup.bApplied = false;
				unitRuleGroup.numFree = unitRuleGroup.ruleList.Size;
				for (int j = 0; j < unitRuleGroup.ruleList.Size; j++)
				{
					unitRuleGroup.ruleList[j].status = UnitRuleStatus.FREE;
				}
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001B730 File Offset: 0x00019930
		private void GrowUnitRuleGroupList(int requiredSize)
		{
			int num = this.m_unitRuleGroupList.Length;
			int num2 = requiredSize + (int)((float)requiredSize * 0.5f) + 1;
			Array.Resize<UnitRuleGroup>(ref this.m_unitRuleGroupList, num2);
			for (int i = num; i < num2; i++)
			{
				this.m_unitRuleGroupList[i] = new UnitRuleGroup(this.m_globalUnitRuleList);
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001B780 File Offset: 0x00019980
		private void GrowUnitRuleList()
		{
			int num = this.m_globalUnitRuleList.Length;
			int num2 = num + (int)((float)this.m_globalUnitRuleList.Length * 0.5f) + 1;
			Array.Resize<UnitRule>(ref this.m_globalUnitRuleList, num2);
			for (int i = num; i < num2; i++)
			{
				this.m_globalUnitRuleList[i] = new UnitRule();
			}
			for (int j = 0; j < this.m_unitRuleGroupList.Length; j++)
			{
				this.m_unitRuleGroupList[j].ruleList.ResetGlobalList(this.m_globalUnitRuleList);
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001B7FC File Offset: 0x000199FC
		private void GrowRuleMinHashList()
		{
			int num = this.m_globalRuleMinHashList.Length;
			int num2 = num + (int)((float)this.m_globalRuleMinHashList.Length * 0.5f) + 1;
			Array.Resize<RuleMinHash>(ref this.m_globalRuleMinHashList, num2);
			for (int i = num; i < num2; i++)
			{
				this.m_globalRuleMinHashList[i] = new RuleMinHash();
			}
			for (int j = 0; j < this.m_minHashDims.Length; j++)
			{
				this.m_minHashDims[j].ruleMinHashList.ResetGlobalList(this.m_globalRuleMinHashList);
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001B878 File Offset: 0x00019A78
		private void CopyMultiRules(ArraySegmentBuilder<WeightedTransformationMatch> tranMatchList, IndexedSubList<WeightedTransformationMatch> tranMatchSublist)
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

		// Token: 0x06000653 RID: 1619 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		private void CopyUnitRules(ArraySegmentBuilder<WeightedTransformationMatch> tranMatchList, IndexedSubList<WeightedTransformationMatch> tranMatchSublist)
		{
			tranMatchSublist.BeginListSpecification();
			for (int i = 0; i < tranMatchList.Count; i++)
			{
				if (tranMatchList[i].IsUnitRule)
				{
					tranMatchSublist.Add(tranMatchList[i], i);
				}
			}
			tranMatchSublist.EndListSpecification();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001B910 File Offset: 0x00019B10
		private void GenerateSignaturesForUnitLength(int signIdx)
		{
			int num = signIdx * this.m_numDimensions;
			int num2 = num + this.m_numDimensions;
			this.m_signatures.Clear();
			int num3 = 0;
			int num4 = this.m_tokenClusterer.GetTokenClusterMapping(this.m_tokenSequence[0]);
			for (int i = 0; i < this.m_numDimensions; i++)
			{
				num3 = Utilities.GetHashCode(num3 + num4);
			}
			if (!this.m_signatures.Contains(num3))
			{
				this.m_signatures.Add(num3);
			}
			for (int j = 0; j < this.m_matchingRules.Count; j++)
			{
				TokenSequence to = this.m_matchingRules[j].Transformation.To;
				if (to.Count == 1)
				{
					num3 = 0;
					num4 = this.m_tokenClusterer.GetTokenClusterMapping(to[0]);
					for (int k = 0; k < this.m_numDimensions; k++)
					{
						num3 = Utilities.GetHashCode(num3 + num4);
					}
					if (!this.m_signatures.Contains(num3))
					{
						this.m_signatures.Add(num3);
					}
				}
				else if (to.Count > 1)
				{
					num3 = 0;
					for (int l = num; l < num2; l++)
					{
						int num5;
						this.GetMinHash(l, this.m_matchingRules, this.m_matchingRules[j], out num5);
						num3 = Utilities.GetHashCode(num3 + this.m_tokenClusterer.GetTokenClusterMapping(num5));
					}
					if (!this.m_signatures.Contains(num3))
					{
						this.m_signatures.Add(num3);
					}
				}
			}
		}

		// Token: 0x04000206 RID: 518
		private const float GROWTH_FACTOR = 0.5f;

		// Token: 0x04000207 RID: 519
		private const uint PRIME = 1000003U;

		// Token: 0x04000208 RID: 520
		private static readonly double LogPRIME = Math.Log(1000003.0);

		// Token: 0x04000209 RID: 521
		private double[] MINHASHFN;

		// Token: 0x0400020A RID: 522
		private TokenSequence m_tokenSequence;

		// Token: 0x0400020B RID: 523
		private ArraySegmentBuilder<WeightedTransformationMatch> m_matchingRules;

		// Token: 0x0400020C RID: 524
		private IndexedSubList<WeightedTransformationMatch> m_multiRulesSublist;

		// Token: 0x0400020D RID: 525
		private IndexedSubList<WeightedTransformationMatch> m_unitRulesSublist;

		// Token: 0x0400020E RID: 526
		private UnitRuleGroup[] m_unitRuleGroupList;

		// Token: 0x0400020F RID: 527
		private int m_unitRuleGroupListSize;

		// Token: 0x04000210 RID: 528
		private UnitRule[] m_globalUnitRuleList;

		// Token: 0x04000211 RID: 529
		private int m_globalUnitRuleListSize;

		// Token: 0x04000212 RID: 530
		private ITokenToClusterMap m_tokenClusterer;

		// Token: 0x04000213 RID: 531
		private MinHashDim[] m_minHashDims;

		// Token: 0x04000214 RID: 532
		private RuleMinHash[] m_globalRuleMinHashList;

		// Token: 0x04000215 RID: 533
		private int m_globalRuleMinHashListSize;

		// Token: 0x04000216 RID: 534
		private RuleApplEnumerator<WeightedTransformationMatch> m_ruleApplEnumerator;

		// Token: 0x04000217 RID: 535
		private IntHashSetEnumerable m_signatures;

		// Token: 0x04000218 RID: 536
		private Heap<int> m_ruleMinHashPQ;

		// Token: 0x04000219 RID: 537
		private int m_nextRuleMinHash;

		// Token: 0x0400021A RID: 538
		private RuleMinHash[] m_ruleMinHashCache = new RuleMinHash[0];

		// Token: 0x0400021B RID: 539
		private int m_NumHashtables;

		// Token: 0x0400021C RID: 540
		private int m_numDimensions;

		// Token: 0x0400021D RID: 541
		private ulong[] m_a;

		// Token: 0x0400021E RID: 542
		private ulong[] m_b;

		// Token: 0x0400021F RID: 543
		public bool m_UnitLengthOptFlag = true;
	}
}
