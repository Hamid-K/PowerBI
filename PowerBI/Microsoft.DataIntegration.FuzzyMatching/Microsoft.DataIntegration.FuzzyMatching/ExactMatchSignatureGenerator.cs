using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public sealed class ExactMatchSignatureGenerator : IOneDimSignatureGenerator, IEnumerable<int>, IEnumerable
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001A4EC File Offset: 0x000186EC
		public int Indexes
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001A4EF File Offset: 0x000186EF
		public void ResetDimension(int signIdx)
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001A4F4 File Offset: 0x000186F4
		public void Reset(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchList)
		{
			this.m_distinctSignatures.Clear();
			if (matchList.Count > 0)
			{
				this.m_allocator.Reset();
				ArraySegment<TransformationMatch> arraySegment = matchList.ToTransformationMatchArraySegment(this.m_allocator);
				MultiRuleApplEnumerator<TransformationMatch> multiRuleApplEnumerator = new MultiRuleApplEnumerator<TransformationMatch>();
				multiRuleApplEnumerator.Reset(tokenSequence.Tokens, arraySegment);
				RuleApplier<TransformationMatch> ruleApplier = new RuleApplier<TransformationMatch>(multiRuleApplEnumerator);
				ruleApplier.Reset(tokenSequence.Tokens, arraySegment);
				WeightedTokenSequence weightedTokenSequence = default(WeightedTokenSequence);
				while (ruleApplier.GetNextDerivedTokenSequence(out weightedTokenSequence.Tokens))
				{
					int hashCode = weightedTokenSequence.Tokens.GetHashCode();
					if (!this.m_distinctSignatures.Contains(hashCode))
					{
						this.m_distinctSignatures.Add(hashCode);
					}
				}
				return;
			}
			this.m_distinctSignatures.Add(tokenSequence.Tokens.GetHashCode());
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001A5BB File Offset: 0x000187BB
		public IEnumerator<int> GetEnumerator()
		{
			return this.m_distinctSignatures.GetEnumerator();
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001A5CD File Offset: 0x000187CD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_distinctSignatures.GetEnumerator();
		}

		// Token: 0x04000200 RID: 512
		private HashSet<int> m_distinctSignatures = new HashSet<int>();

		// Token: 0x04000201 RID: 513
		private BlockedSegmentArray<TransformationMatch> m_allocator = new BlockedSegmentArray<TransformationMatch>();
	}
}
