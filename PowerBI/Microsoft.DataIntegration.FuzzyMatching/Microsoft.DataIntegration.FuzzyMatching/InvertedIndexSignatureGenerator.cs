using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public sealed class InvertedIndexSignatureGenerator : IOneDimSignatureGenerator, IEnumerable<int>, IEnumerable, ISignatureGeneratorInitialize
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0001A636 File Offset: 0x00018836
		public InvertedIndexSignatureGenerator()
		{
			this.m_allTokens = new FastIntHashSet();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001A649 File Offset: 0x00018849
		public InvertedIndexSignatureGenerator(ITokenToClusterMap clustering)
			: this()
		{
			this.Initialize(clustering);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001A658 File Offset: 0x00018858
		public void Initialize(ITokenToClusterMap clustering)
		{
			this.m_tokenClusterMap = clustering;
			this.UseTokenClustering = clustering != null;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001A66B File Offset: 0x0001886B
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0001A673 File Offset: 0x00018873
		public bool UseTokenClustering { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001A67C File Offset: 0x0001887C
		public int Indexes
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001A67F File Offset: 0x0001887F
		public void ResetDimension(int signIdx)
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001A681 File Offset: 0x00018881
		public void Reset(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules)
		{
			this.m_allTokens.Clear();
			this.GenerateSignaturesFromOriginalTokens(tokenSequence.Tokens);
			this.GenerateSiganturesFromRuleRhs(matchingRules);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001A6A1 File Offset: 0x000188A1
		public IEnumerator<int> GetEnumerator()
		{
			return this.m_allTokens.GetEnumerator();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001A6AE File Offset: 0x000188AE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_allTokens.GetEnumerator();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001A6BC File Offset: 0x000188BC
		public void GenerateSignaturesFromOriginalTokens(TokenSequence tokenSequence)
		{
			if (this.UseTokenClustering)
			{
				for (int i = 0; i < tokenSequence.Count; i++)
				{
					this.m_allTokens.Add(this.m_tokenClusterMap.GetTokenClusterMapping(tokenSequence[i]));
				}
				return;
			}
			for (int j = 0; j < tokenSequence.Count; j++)
			{
				this.m_allTokens.Add(tokenSequence[j]);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001A728 File Offset: 0x00018928
		public void GenerateSiganturesFromRuleRhs(IList<WeightedTransformationMatch> matchingRules)
		{
			if (this.UseTokenClustering)
			{
				for (int i = 0; i < matchingRules.Count; i++)
				{
					WeightedTransformationMatch weightedTransformationMatch = matchingRules[i];
					TokenSequence to = weightedTransformationMatch.Transformation.To;
					for (int j = 0; j < to.Count; j++)
					{
						this.m_allTokens.Add(this.m_tokenClusterMap.GetTokenClusterMapping(to[j]));
					}
				}
				return;
			}
			for (int k = 0; k < matchingRules.Count; k++)
			{
				WeightedTransformationMatch weightedTransformationMatch = matchingRules[k];
				TokenSequence to2 = weightedTransformationMatch.Transformation.To;
				for (int l = 0; l < to2.Count; l++)
				{
					this.m_allTokens.Add(to2[l]);
				}
			}
		}

		// Token: 0x04000203 RID: 515
		private FastIntHashSet m_allTokens;

		// Token: 0x04000204 RID: 516
		private ITokenToClusterMap m_tokenClusterMap;
	}
}
