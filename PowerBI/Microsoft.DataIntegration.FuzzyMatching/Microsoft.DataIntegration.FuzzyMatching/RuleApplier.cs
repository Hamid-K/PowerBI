using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public sealed class RuleApplier<T> where T : ITransformationMatch
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x000305AD File Offset: 0x0002E7AD
		public RuleApplier(RuleApplEnumerator<T> ruleApplEnumerator)
		{
			this.m_ruleApplEnumerator = ruleApplEnumerator;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000305D2 File Offset: 0x0002E7D2
		public void Reset(TokenSequence tokenSequence, ArraySegment<T> matchingRules)
		{
			this.m_inputTokenSequence = tokenSequence;
			RuleApplier<T>.CopyAllRules(matchingRules, this.m_matchingRulesSublist);
			this.m_ruleApplEnumerator.Reset(tokenSequence, this.m_matchingRulesSublist);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000305F9 File Offset: 0x0002E7F9
		internal void Reset(TokenSequence tokenSequence, IndexedSubList<T> matchingRules)
		{
			this.m_inputTokenSequence = tokenSequence;
			this.m_ruleApplEnumerator.Reset(tokenSequence, matchingRules);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0003060F File Offset: 0x0002E80F
		public bool GetNextDerivedTokenSequence(out TokenSequence derivedTokenSequence)
		{
			if (!this.m_ruleApplEnumerator.GetNextRuleApplication())
			{
				derivedTokenSequence = default(TokenSequence);
				return false;
			}
			derivedTokenSequence = this.DeriveTokenSequence();
			return true;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00030634 File Offset: 0x0002E834
		private TokenSequence DeriveTokenSequence()
		{
			int i = 0;
			int num = 0;
			this.m_tokenIdSegmentBuilder.Reset();
			for (int j = 0; j < this.m_ruleApplEnumerator.NumRulesApplied; j++)
			{
				T appliedRule = this.m_ruleApplEnumerator.GetAppliedRule(j);
				while (i < appliedRule.Position)
				{
					this.m_tokenIdSegmentBuilder.Add(this.m_inputTokenSequence[i]);
					num++;
					i++;
				}
				int num2 = 0;
				Transformation transformation;
				for (;;)
				{
					int num3 = num2;
					transformation = appliedRule.Transformation;
					if (num3 >= transformation.To.Count)
					{
						break;
					}
					ArraySegmentBuilder<int> tokenIdSegmentBuilder = this.m_tokenIdSegmentBuilder;
					transformation = appliedRule.Transformation;
					tokenIdSegmentBuilder.Add(transformation.To[num2]);
					num++;
					num2++;
				}
				int num4 = i;
				transformation = appliedRule.Transformation;
				i = num4 + transformation.From.Count;
			}
			while (i < this.m_inputTokenSequence.Count)
			{
				this.m_tokenIdSegmentBuilder.Add(this.m_inputTokenSequence[i]);
				num++;
				i++;
			}
			return this.m_tokenIdSegmentBuilder;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00030758 File Offset: 0x0002E958
		private static void CopyAllRules(ArraySegment<T> matchList, IndexedSubList<T> matchingRulesSublist)
		{
			matchingRulesSublist.BeginListSpecification();
			for (int i = 0; i < matchList.Count; i++)
			{
				matchingRulesSublist.Add(matchList.Array[matchList.Offset + i], i);
			}
			matchingRulesSublist.EndListSpecification();
		}

		// Token: 0x04000412 RID: 1042
		private TokenSequence m_inputTokenSequence;

		// Token: 0x04000413 RID: 1043
		private IndexedSubList<T> m_matchingRulesSublist = new IndexedSubList<T>();

		// Token: 0x04000414 RID: 1044
		private RuleApplEnumerator<T> m_ruleApplEnumerator;

		// Token: 0x04000415 RID: 1045
		private ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();
	}
}
