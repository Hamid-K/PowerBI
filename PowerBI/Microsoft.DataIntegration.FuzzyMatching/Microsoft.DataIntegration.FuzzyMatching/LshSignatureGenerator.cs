using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public sealed class LshSignatureGenerator : IMultiDimSignatureGenerator, IOneDimSignatureGenerator, IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator, IDeserializationCallback, ISignatureGeneratorInitialize
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001BB87 File Offset: 0x00019D87
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001BB8F File Offset: 0x00019D8F
		public int Seed
		{
			get
			{
				return this.m_seed;
			}
			set
			{
				this.m_seed = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001BB98 File Offset: 0x00019D98
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		public int NumHashtables
		{
			get
			{
				return this.m_NumHashtables;
			}
			set
			{
				this.Initialize(value, this.DimensionsPerSignature);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001BBAF File Offset: 0x00019DAF
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001BBB7 File Offset: 0x00019DB7
		public int DimensionsPerSignature
		{
			get
			{
				return this.m_NumDimensionsPerSignature;
			}
			set
			{
				this.Initialize(this.NumHashtables, value);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001BBC6 File Offset: 0x00019DC6
		public LshSignatureGenerator()
		{
			this.Seed = 4;
			this.NumHashtables = 6;
			this.DimensionsPerSignature = 4;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001BBE3 File Offset: 0x00019DE3
		internal LshSignatureGenerator(int numHashtables, int dimensionsPerSignature, int seed, ITokenToClusterMap tokenClusterer)
		{
			this.Seed = seed;
			this.NumHashtables = numHashtables;
			this.DimensionsPerSignature = dimensionsPerSignature;
			this.Initialize(tokenClusterer);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001BC08 File Offset: 0x00019E08
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.Initialize(this.NumHashtables, this.DimensionsPerSignature);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001BC1C File Offset: 0x00019E1C
		public void Initialize(ITokenToClusterMap clustering)
		{
			this.m_tokenClusterer = clustering;
			this.Initialize(this.NumHashtables, this.DimensionsPerSignature);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001BC38 File Offset: 0x00019E38
		private static MinHashGenerator GetMinHashGenerator(int dimensions, int seed)
		{
			foreach (MinHashGenerator minHashGenerator in LshSignatureGenerator.s_minHashGenerators)
			{
				if (minHashGenerator.Dimensions == dimensions && minHashGenerator.Seed == seed)
				{
					return minHashGenerator;
				}
			}
			List<MinHashGenerator> list = LshSignatureGenerator.s_minHashGenerators;
			MinHashGenerator minHashGenerator4;
			lock (list)
			{
				foreach (MinHashGenerator minHashGenerator2 in LshSignatureGenerator.s_minHashGenerators)
				{
					if (minHashGenerator2.Dimensions == dimensions && minHashGenerator2.Seed == seed)
					{
						return minHashGenerator2;
					}
				}
				MinHashGenerator minHashGenerator3 = new MinHashGenerator(dimensions, seed);
				LshSignatureGenerator.s_minHashGenerators.Add(minHashGenerator3);
				minHashGenerator4 = minHashGenerator3;
			}
			return minHashGenerator4;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001BD2C File Offset: 0x00019F2C
		private void Initialize(int numHashtables, int dimensionsPerSignature)
		{
			this.m_NumHashtables = numHashtables;
			this.m_NumDimensionsPerSignature = dimensionsPerSignature;
			this.m_numDimensions = dimensionsPerSignature * numHashtables;
			this.m_minHashGenerator = LshSignatureGenerator.GetMinHashGenerator(this.m_numDimensions, this.m_seed);
			this.m_ruleMinHash = new double[0][];
			this.m_ruleMinHashRep = new int[0][];
			this.m_ruleIdToPos = new int[0];
			this.m_ruleGroupPool = new RuleGroupPool(this.m_numDimensions);
			this.m_multiRules = new IndexedSubList<TransformationMatch>();
			this.m_multiRuleToGroup = new RuleGroup[0];
			this.m_tokenWtScratchBuf = new int[0];
			this.m_clusterIdScratchBuf = new int[0];
			this.m_minHashDimAggr = new GroupMinHashDimAggr[this.m_numDimensions];
			this.m_ruleConstraints = new RuleApplConstraints();
			this.m_signatures = new IntHashSet[this.m_NumHashtables];
			for (int i = 0; i < this.m_NumHashtables; i++)
			{
				this.m_signatures[i] = new IntHashSetEnumerable(1000);
			}
			this.m_ruleApplEnumerator = new RuleApplEnumerator<TransformationMatch>();
			this.m_numGeneratedSignatures = new int[this.m_NumHashtables];
			this.m_signatureBuf = new int[this.m_NumHashtables][];
			this.bPosRuleFree = new bool[0];
			for (int j = 0; j < this.m_NumHashtables; j++)
			{
				this.m_signatureBuf[j] = new int[2];
			}
			for (int k = 0; k < this.m_numDimensions; k++)
			{
				this.m_minHashDimAggr[k] = new GroupMinHashDimAggr(k, this.m_ruleConstraints);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001BE98 File Offset: 0x0001A098
		public void Reset(int signIdx)
		{
			this.m_curIdx = signIdx;
			this.m_curSignIdx = 0;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		public void Reset(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			if (tokenSequence.Count == 1)
			{
				this.GenerateSignaturesForUnitLength(tokenSequence, matchingRules);
				return;
			}
			for (int i = 0; i < this.m_NumHashtables; i++)
			{
				this.m_numGeneratedSignatures[i] = 0;
				this.m_signatures[i].Clear();
			}
			if (tokenSequence.Count == 0)
			{
				return;
			}
			this.PrepareForSignatureGeneration(tokenSequence, matchingRules);
			this.m_ruleApplEnumerator.Reset(tokenSequence.Tokens, this.m_multiRules);
			this.m_ruleConstraints.Reset(tokenSequence.Count, matchingRules.Count + tokenSequence.Count);
			if (this.m_minHashDimAggr[0].RequiresCapacityIncrease(tokenSequence.Count, matchingRules.Count + tokenSequence.Count))
			{
				for (int j = 0; j < this.m_numDimensions; j++)
				{
					this.m_minHashDimAggr[j].IncreaseCapacity(tokenSequence.Count, matchingRules.Count + tokenSequence.Count);
				}
			}
			while (this.m_ruleApplEnumerator.GetNextRuleApplication())
			{
				this.InitMinHashDimAggrs(tokenSequence, this.m_ruleApplEnumerator, matchingRules);
				for (int k = 0; k < this.m_NumHashtables; k++)
				{
					this.GenerateSignatures(k);
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public IEnumerator<int> GetEnumerator()
		{
			this.m_curSignIdx = -1;
			return this;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001BFCE File Offset: 0x0001A1CE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001BFD6 File Offset: 0x0001A1D6
		public int Current
		{
			get
			{
				return this.m_signatureBuf[this.m_curIdx][this.m_curSignIdx];
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		public bool MoveNext()
		{
			int num = this.m_numGeneratedSignatures[this.m_curIdx];
			int num2 = this.m_curSignIdx + 1;
			this.m_curSignIdx = num2;
			return num > num2;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001C029 File Offset: 0x0001A229
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001C030 File Offset: 0x0001A230
		public void Dispose()
		{
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001C034 File Offset: 0x0001A234
		private void PrepareForSignatureGeneration(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			this.EnsureCapacities(tokenSequence, matchingRules);
			for (int i = 0; i < tokenSequence.Count; i++)
			{
				this.bPosRuleFree[i] = true;
			}
			this.m_ruleGroupPool.BeginUpdate(tokenSequence.Count);
			if (matchingRules.Count > 0)
			{
				this.ProcessRules(tokenSequence, matchingRules);
				this.ProcessBaseTokens(tokenSequence, matchingRules);
			}
			else
			{
				this.ProcessBaseTokensNoRules(tokenSequence);
			}
			this.m_ruleGroupPool.EndUpdate();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		private void EnsureCapacities(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			if (this.m_ruleMinHash.Length < matchingRules.Count + tokenSequence.Count)
			{
				int num = this.m_ruleMinHash.Length;
				int num2 = (int)((double)(matchingRules.Count + tokenSequence.Count) * 1.5);
				Array.Resize<double[]>(ref this.m_ruleMinHash, num2);
				Array.Resize<int[]>(ref this.m_ruleMinHashRep, num2);
				for (int i = num; i < num2; i++)
				{
					this.m_ruleMinHash[i] = new double[this.m_numDimensions];
					this.m_ruleMinHashRep[i] = new int[this.m_numDimensions];
				}
				Array.Resize<int>(ref this.m_ruleIdToPos, num2);
			}
			if (this.m_multiRuleToGroup.Length < matchingRules.Count)
			{
				Array.Resize<RuleGroup>(ref this.m_multiRuleToGroup, (int)((double)matchingRules.Count * 1.5));
			}
			if (this.m_clusterIdScratchBuf.Length < tokenSequence.Count)
			{
				Array.Resize<int>(ref this.m_clusterIdScratchBuf, (int)((double)tokenSequence.Count * 1.5));
				Array.Resize<int>(ref this.m_tokenWtScratchBuf, (int)((double)tokenSequence.Count * 1.5));
			}
			if (this.bPosRuleFree.Length < tokenSequence.Count)
			{
				Array.Resize<bool>(ref this.bPosRuleFree, (int)((double)tokenSequence.Count * 1.5));
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		private void ProcessBaseTokens(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			int num = matchingRules.Count;
			int num2 = 0;
			this.m_firstRuleFreePos = -1;
			int i = 0;
			while (i < tokenSequence.Count)
			{
				if (this.bPosRuleFree[i])
				{
					if (this.m_firstRuleFreePos == -1)
					{
						this.m_firstRuleFreePos = i;
					}
					this.m_clusterIdScratchBuf[num2] = this.m_tokenClusterer.GetTokenClusterMapping(tokenSequence.Tokens[i]);
					this.m_tokenWtScratchBuf[num2++] = tokenSequence.GetWeight(i);
				}
				else
				{
					int tokenClusterMapping = this.m_tokenClusterer.GetTokenClusterMapping(tokenSequence.Tokens[i]);
					int weight = tokenSequence.GetWeight(i);
					RuleGroup ruleGroup = this.m_ruleGroupPool[i];
					this.m_minHashGenerator.GetMinHash(tokenClusterMapping, weight, this.m_ruleMinHash[num]);
					for (int j = 0; j < this.m_numDimensions; j++)
					{
						this.m_ruleMinHashRep[num][j] = tokenClusterMapping;
						ruleGroup[j].AddMinHash(num, tokenClusterMapping, this.m_ruleMinHash[num][j]);
					}
					ruleGroup.EndUpdate();
					this.m_ruleIdToPos[num] = i;
				}
				i++;
				num++;
			}
			if (this.m_firstRuleFreePos != -1)
			{
				RuleGroup ruleGroup2 = this.m_ruleGroupPool[this.m_firstRuleFreePos];
				ruleGroup2.BeginUpdate();
				num = this.m_firstRuleFreePos + matchingRules.Count;
				this.m_minHashGenerator.GetMinHash(this.m_clusterIdScratchBuf, this.m_tokenWtScratchBuf, num2, this.m_ruleMinHash[num], this.m_ruleMinHashRep[num]);
				for (int k = 0; k < this.m_numDimensions; k++)
				{
					ruleGroup2[k].AddMinHash(num, this.m_ruleMinHashRep[num][k], this.m_ruleMinHash[num][k]);
				}
				this.m_ruleIdToPos[num] = this.m_firstRuleFreePos;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001C3B0 File Offset: 0x0001A5B0
		private void ProcessBaseTokensNoRules(WeightedTokenSequence tokenSequence)
		{
			this.m_firstRuleFreePos = 0;
			for (int i = 0; i < tokenSequence.Count; i++)
			{
				this.m_clusterIdScratchBuf[i] = this.m_tokenClusterer.GetTokenClusterMapping(tokenSequence.Tokens[i]);
				this.m_tokenWtScratchBuf[i] = tokenSequence.GetWeight(i);
			}
			RuleGroup ruleGroup = this.m_ruleGroupPool[0];
			ruleGroup.BeginUpdate();
			this.m_minHashGenerator.GetMinHash(this.m_clusterIdScratchBuf, this.m_tokenWtScratchBuf, tokenSequence.Count, this.m_ruleMinHash[0], this.m_ruleMinHashRep[0]);
			for (int j = 0; j < this.m_numDimensions; j++)
			{
				ruleGroup[j].AddMinHash(0, this.m_ruleMinHashRep[0][j], this.m_ruleMinHash[0][j]);
			}
			ruleGroup.EndUpdate();
			this.m_ruleIdToPos[0] = 0;
			this.m_multiRules.BeginListSpecification();
			this.m_multiRules.EndListSpecification();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001C4A0 File Offset: 0x0001A6A0
		private void ProcessRules(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			this.m_multiRules.BeginListSpecification();
			for (int i = 0; i < matchingRules.Count; i++)
			{
				Transformation transformation = (Transformation)matchingRules[i].Transformation;
				this.m_ruleIdToPos[i] = matchingRules[i].Position;
				RuleGroup ruleGroup;
				if (transformation.From.Count == 1)
				{
					ruleGroup = this.m_ruleGroupPool[matchingRules[i].Position];
					if (this.bPosRuleFree[matchingRules[i].Position])
					{
						this.bPosRuleFree[matchingRules[i].Position] = false;
						ruleGroup.BeginUpdate();
					}
				}
				else
				{
					this.m_multiRules.Add((TransformationMatch)matchingRules[i], i);
					ruleGroup = (this.m_multiRuleToGroup[i] = this.m_ruleGroupPool.GetNewRuleGroup());
				}
				TokenSequence to = transformation.To;
				if (to.Count == 1)
				{
					int tokenClusterMapping = this.m_tokenClusterer.GetTokenClusterMapping(to[0]);
					WeightedTransformationMatch weightedTransformationMatch = matchingRules[i];
					int toWeight = weightedTransformationMatch.Transformation.GetToWeight(0);
					this.m_minHashGenerator.GetMinHash(tokenClusterMapping, toWeight, this.m_ruleMinHash[i]);
					for (int j = 0; j < this.m_numDimensions; j++)
					{
						this.m_ruleMinHashRep[i][j] = tokenClusterMapping;
						ruleGroup[j].AddMinHash(i, tokenClusterMapping, this.m_ruleMinHash[i][j]);
					}
				}
				else
				{
					if (this.m_tokenWtScratchBuf.Length < to.Count)
					{
						Array.Resize<int>(ref this.m_tokenWtScratchBuf, (int)((double)to.Count * 1.5));
						Array.Resize<int>(ref this.m_clusterIdScratchBuf, (int)((double)to.Count * 1.5));
					}
					for (int k = 0; k < to.Count; k++)
					{
						int[] tokenWtScratchBuf = this.m_tokenWtScratchBuf;
						int num = k;
						WeightedTransformationMatch weightedTransformationMatch = matchingRules[i];
						tokenWtScratchBuf[num] = weightedTransformationMatch.Transformation.GetToWeight(k);
						this.m_clusterIdScratchBuf[k] = this.m_tokenClusterer.GetTokenClusterMapping(to[k]);
					}
					this.m_minHashGenerator.GetMinHash(this.m_clusterIdScratchBuf, this.m_tokenWtScratchBuf, to.Count, this.m_ruleMinHash[i], this.m_ruleMinHashRep[i]);
					for (int l = 0; l < this.m_numDimensions; l++)
					{
						ruleGroup[l].AddMinHash(i, this.m_ruleMinHashRep[i][l], this.m_ruleMinHash[i][l]);
					}
				}
			}
			this.m_multiRules.EndListSpecification();
			int m = 0;
			for (int num2 = this.m_multiRules.GetFirstMatch(); num2 != -1; num2 = this.m_multiRules.GetSuccessorOverlap(num2))
			{
				int position = this.m_multiRules[num2].Position;
				int num3 = position + this.m_multiRules[num2].Transformation.From.Count;
				if (m < position)
				{
					m = position;
				}
				while (m < num3)
				{
					if (this.bPosRuleFree[m])
					{
						this.bPosRuleFree[m] = false;
						this.m_ruleGroupPool[m].BeginUpdate();
					}
					m++;
				}
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		private void InitMinHashDimAggrs(WeightedTokenSequence tokenSequence, RuleApplEnumerator<TransformationMatch> ruleApplication, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			int i = 0;
			for (int j = 0; j < this.m_numDimensions; j++)
			{
				this.m_minHashDimAggr[j].BeginUpdate(this.m_ruleMinHash, this.m_ruleMinHashRep, this.m_ruleIdToPos);
			}
			if (this.m_firstRuleFreePos != -1)
			{
				RuleGroup ruleGroup = this.m_ruleGroupPool[this.m_firstRuleFreePos];
				this.m_ruleConstraints.FreeRulesAtPos[this.m_firstRuleFreePos] = 1;
				for (int k = 0; k < this.m_numDimensions; k++)
				{
					this.m_minHashDimAggr[k].AddGroupMinHashDim(this.m_firstRuleFreePos, ruleGroup[k]);
				}
			}
			for (int l = 0; l < ruleApplication.NumRulesApplied; l++)
			{
				int num;
				TransformationMatch appliedRule = ruleApplication.GetAppliedRule(l, out num);
				while (i < appliedRule.Position)
				{
					if (!this.bPosRuleFree[i])
					{
						RuleGroup ruleGroup2 = this.m_ruleGroupPool[i];
						this.m_ruleConstraints.FreeRulesAtPos[i] = ruleGroup2[0].NumEntries;
						for (int m = 0; m < this.m_numDimensions; m++)
						{
							this.m_minHashDimAggr[m].AddGroupMinHashDim(i, ruleGroup2[m]);
						}
					}
					i++;
				}
				this.m_ruleConstraints.FreeRulesAtPos[i] = 1;
				for (int n = 0; n < this.m_numDimensions; n++)
				{
					this.m_minHashDimAggr[n].AddGroupMinHashDim(i, this.m_multiRuleToGroup[num][n]);
				}
				i += appliedRule.Transformation.From.Count;
			}
			while (i < tokenSequence.Count)
			{
				if (!this.bPosRuleFree[i])
				{
					RuleGroup ruleGroup3 = this.m_ruleGroupPool[i];
					this.m_ruleConstraints.FreeRulesAtPos[i] = ruleGroup3[0].NumEntries;
					for (int num2 = 0; num2 < this.m_numDimensions; num2++)
					{
						this.m_minHashDimAggr[num2].AddGroupMinHashDim(i, ruleGroup3[num2]);
					}
				}
				i++;
			}
			for (int num3 = 0; num3 < this.m_numDimensions; num3++)
			{
				this.m_minHashDimAggr[num3].EndUpdate();
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		private void GenerateSignatures(int signIdx)
		{
			int num = signIdx * this.m_NumDimensionsPerSignature;
			int num2 = num + this.m_NumDimensionsPerSignature;
			IntHashSet intHashSet = this.m_signatures[signIdx];
			int[] array = this.m_signatureBuf[signIdx];
			int num3 = this.m_numGeneratedSignatures[signIdx];
			int num4;
			this.GetFirstSignature(num, num2, out num4);
			if (!intHashSet.Contains(num4))
			{
				intHashSet.Add(num4);
				if (num3 == array.Length)
				{
					Array.Resize<int>(ref array, (int)((double)num3 * 1.5));
					this.m_signatureBuf[signIdx] = array;
				}
				array[num3++] = num4;
			}
			while (this.GetNextSignature(num, num2, out num4))
			{
				if (!intHashSet.Contains(num4))
				{
					intHashSet.Add(num4);
					if (num3 == array.Length)
					{
						Array.Resize<int>(ref array, (int)((double)num3 * 1.5));
						this.m_signatureBuf[signIdx] = array;
					}
					array[num3++] = num4;
				}
			}
			this.m_numGeneratedSignatures[signIdx] = num3;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
		private void GenerateSignaturesForUnitLength(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matches)
		{
			this.EnsureCapacities(tokenSequence, matches);
			int num = 0;
			int num2 = this.m_tokenClusterer.GetTokenClusterMapping(tokenSequence.Tokens[0]);
			for (int i = 0; i < this.m_NumDimensionsPerSignature; i++)
			{
				num = Utilities.GetHashCode(num + num2);
			}
			for (int j = 0; j < this.m_NumHashtables; j++)
			{
				this.m_signatures[j].Clear();
				this.m_signatureBuf[j][0] = num;
				this.m_numGeneratedSignatures[j] = 1;
				this.m_signatures[j].Add(num);
			}
			for (int k = 0; k < matches.Count; k++)
			{
				WeightedTransformationMatch weightedTransformationMatch = matches[k];
				TokenSequence to = weightedTransformationMatch.Transformation.To;
				if (to.Count == 1)
				{
					num = 0;
					num2 = this.m_tokenClusterer.GetTokenClusterMapping(to[0]);
					for (int l = 0; l < this.m_NumDimensionsPerSignature; l++)
					{
						num = Utilities.GetHashCode(num + num2);
					}
					for (int m = 0; m < this.m_NumHashtables; m++)
					{
						if (!this.m_signatures[m].Contains(num))
						{
							this.m_signatures[m].Add(num);
							if (this.m_numGeneratedSignatures[m] == this.m_signatureBuf[m].Length)
							{
								Array.Resize<int>(ref this.m_signatureBuf[m], (int)(1.5 * (double)this.m_numGeneratedSignatures[m]));
							}
							int[] array = this.m_signatureBuf[m];
							int[] numGeneratedSignatures = this.m_numGeneratedSignatures;
							int num3 = m;
							int num4 = numGeneratedSignatures[num3];
							numGeneratedSignatures[num3] = num4 + 1;
							array[num4] = num;
						}
					}
				}
				else
				{
					if (this.m_clusterIdScratchBuf.Length < to.Count)
					{
						Array.Resize<int>(ref this.m_clusterIdScratchBuf, (int)((double)to.Count * 1.5));
						Array.Resize<int>(ref this.m_tokenWtScratchBuf, (int)((double)to.Count * 1.5));
					}
					for (int n = 0; n < to.Count; n++)
					{
						this.m_clusterIdScratchBuf[n] = this.m_tokenClusterer.GetTokenClusterMapping(to[n]);
						int[] tokenWtScratchBuf = this.m_tokenWtScratchBuf;
						int num5 = n;
						weightedTransformationMatch = matches[k];
						tokenWtScratchBuf[num5] = weightedTransformationMatch.Transformation.GetToWeight(n);
					}
					double[] array2 = this.m_ruleMinHash[k];
					int[] array3 = this.m_ruleMinHashRep[k];
					this.m_minHashGenerator.GetMinHash(this.m_clusterIdScratchBuf, this.m_tokenWtScratchBuf, to.Count, array2, array3);
					num = 0;
					int num6 = this.m_NumDimensionsPerSignature;
					int m = 0;
					for (int num7 = 0; num7 < this.m_numDimensions; num7++)
					{
						if (num7 == num6)
						{
							if (!this.m_signatures[m].Contains(num))
							{
								this.m_signatures[m].Add(num);
								if (this.m_numGeneratedSignatures[m] == this.m_signatureBuf[m].Length)
								{
									Array.Resize<int>(ref this.m_signatureBuf[m], (int)(1.5 * (double)this.m_numGeneratedSignatures[m]));
								}
								int[] array4 = this.m_signatureBuf[m];
								int[] numGeneratedSignatures2 = this.m_numGeneratedSignatures;
								int num8 = m;
								int num4 = numGeneratedSignatures2[num8];
								numGeneratedSignatures2[num8] = num4 + 1;
								array4[num4] = num;
							}
							num = 0;
							num6 += this.m_NumDimensionsPerSignature;
							m++;
						}
						num = Utilities.GetHashCode(num + array3[num7]);
					}
					if (!this.m_signatures[m].Contains(num))
					{
						this.m_signatures[m].Add(num);
						if (this.m_numGeneratedSignatures[m] == this.m_signatureBuf[m].Length)
						{
							Array.Resize<int>(ref this.m_signatureBuf[m], (int)(1.5 * (double)this.m_numGeneratedSignatures[m]));
						}
						int[] array5 = this.m_signatureBuf[m];
						int[] numGeneratedSignatures3 = this.m_numGeneratedSignatures;
						int num9 = m;
						int num4 = numGeneratedSignatures3[num9];
						numGeneratedSignatures3[num9] = num4 + 1;
						array5[num4] = num;
					}
				}
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001CEB4 File Offset: 0x0001B0B4
		private bool GetFirstSignature(int beginDim, int endDim, out int signature)
		{
			int num = beginDim;
			while (num >= beginDim && num < endDim)
			{
				if (this.m_minHashDimAggr[num].GetNext())
				{
					num++;
				}
				else
				{
					this.m_minHashDimAggr[num].Reset();
					num--;
				}
			}
			if (num == endDim)
			{
				signature = this.GetCurrentSignature(beginDim, endDim);
				return true;
			}
			signature = 0;
			return false;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001CF08 File Offset: 0x0001B108
		private bool GetNextSignature(int beginDim, int endDim, out int signature)
		{
			int num = endDim - 1;
			while (num >= beginDim && num < endDim)
			{
				if (this.m_minHashDimAggr[num].GetNext())
				{
					num++;
				}
				else
				{
					this.m_minHashDimAggr[num].Reset();
					num--;
				}
			}
			if (num == endDim)
			{
				signature = this.GetCurrentSignature(beginDim, endDim);
				return true;
			}
			signature = 0;
			return false;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001CF60 File Offset: 0x0001B160
		private int GetCurrentSignature(int beginDim, int endDim)
		{
			int num = 0;
			for (int i = beginDim; i < endDim; i++)
			{
				num = Utilities.GetHashCode(num + this.m_minHashDimAggr[i].GetCurrentRep());
			}
			return num;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001CF94 File Offset: 0x0001B194
		public static int NumHashTables(int nDimensions, double minThreshold, double successProb)
		{
			double num = Math.Pow(minThreshold, (double)nDimensions);
			double num2 = Math.Log(1.0 - num);
			double num3 = Math.Log(1.0 - successProb);
			if (num2 == 0.0)
			{
				return -1;
			}
			return Convert.ToInt32(Math.Ceiling(num3 / num2));
		}

		// Token: 0x04000238 RID: 568
		private int m_NumHashtables;

		// Token: 0x04000239 RID: 569
		private int m_NumDimensionsPerSignature;

		// Token: 0x0400023A RID: 570
		private int m_numDimensions;

		// Token: 0x0400023B RID: 571
		private int m_seed;

		// Token: 0x0400023C RID: 572
		private ITokenToClusterMap m_tokenClusterer;

		// Token: 0x0400023D RID: 573
		[NonSerialized]
		private MinHashGenerator m_minHashGenerator;

		// Token: 0x0400023E RID: 574
		[NonSerialized]
		private double[][] m_ruleMinHash;

		// Token: 0x0400023F RID: 575
		[NonSerialized]
		private int[][] m_ruleMinHashRep;

		// Token: 0x04000240 RID: 576
		[NonSerialized]
		private int[] m_ruleIdToPos;

		// Token: 0x04000241 RID: 577
		[NonSerialized]
		private RuleGroupPool m_ruleGroupPool;

		// Token: 0x04000242 RID: 578
		[NonSerialized]
		private RuleGroup[] m_multiRuleToGroup;

		// Token: 0x04000243 RID: 579
		[NonSerialized]
		private bool[] bPosRuleFree;

		// Token: 0x04000244 RID: 580
		private int m_firstRuleFreePos;

		// Token: 0x04000245 RID: 581
		[NonSerialized]
		private IndexedSubList<TransformationMatch> m_multiRules;

		// Token: 0x04000246 RID: 582
		[NonSerialized]
		private RuleApplEnumerator<TransformationMatch> m_ruleApplEnumerator;

		// Token: 0x04000247 RID: 583
		[NonSerialized]
		private GroupMinHashDimAggr[] m_minHashDimAggr;

		// Token: 0x04000248 RID: 584
		[NonSerialized]
		private RuleApplConstraints m_ruleConstraints;

		// Token: 0x04000249 RID: 585
		[NonSerialized]
		private IntHashSet[] m_signatures;

		// Token: 0x0400024A RID: 586
		[NonSerialized]
		private int[][] m_signatureBuf;

		// Token: 0x0400024B RID: 587
		[NonSerialized]
		private int[] m_numGeneratedSignatures;

		// Token: 0x0400024C RID: 588
		private int m_curIdx;

		// Token: 0x0400024D RID: 589
		private int m_curSignIdx;

		// Token: 0x0400024E RID: 590
		[NonSerialized]
		private int[] m_tokenWtScratchBuf;

		// Token: 0x0400024F RID: 591
		[NonSerialized]
		private int[] m_clusterIdScratchBuf;

		// Token: 0x04000250 RID: 592
		private static List<MinHashGenerator> s_minHashGenerators = new List<MinHashGenerator>();
	}
}
