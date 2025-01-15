using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200009B RID: 155
	public interface IOneDimSignatureGenerator : IEnumerable<int>, IEnumerable
	{
		// Token: 0x06000628 RID: 1576
		void Reset(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchingRules);
	}
}
