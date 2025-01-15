using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	internal class MultiRuleApplEnumerator<T> : RuleApplEnumerator<T> where T : ITransformationMatch
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0002F05A File Offset: 0x0002D25A
		public MultiRuleApplEnumerator()
		{
			this.m_multiRules = new IndexedSubList<T>();
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002F070 File Offset: 0x0002D270
		public void Reset(TokenSequence tokenSequence, ArraySegment<T> matchList)
		{
			this.m_multiRules.BeginListSpecification();
			for (int i = 0; i < matchList.Count; i++)
			{
				T t = matchList.Array[matchList.Offset + i];
				if (!t.Transformation.IsUnitRule)
				{
					this.m_multiRules.Add(t, i);
				}
			}
			this.m_multiRules.EndListSpecification();
			base.Reset(tokenSequence, this.m_multiRules);
		}

		// Token: 0x040003F7 RID: 1015
		private IndexedSubList<T> m_multiRules;
	}
}
