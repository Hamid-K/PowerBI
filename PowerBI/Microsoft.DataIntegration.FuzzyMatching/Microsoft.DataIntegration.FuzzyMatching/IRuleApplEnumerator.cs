using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000103 RID: 259
	internal interface IRuleApplEnumerator<TransformationMatch>
	{
		// Token: 0x06000ABA RID: 2746
		void Reset(TokenSequence tokenSequence, IList<TransformationMatch> transformationMatchList);

		// Token: 0x06000ABB RID: 2747
		bool GetNextRuleApplication();

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000ABC RID: 2748
		int NumRulesApplied { get; }

		// Token: 0x06000ABD RID: 2749
		TransformationMatch GetAppliedRule(int idx);
	}
}
