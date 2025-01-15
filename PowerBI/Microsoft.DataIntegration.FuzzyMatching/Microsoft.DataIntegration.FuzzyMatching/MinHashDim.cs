using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	internal sealed class MinHashDim
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0001BB1B File Offset: 0x00019D1B
		internal MinHashDim(RuleMinHash[] globalList)
		{
			this.ruleMinHashList = new ListSpan<RuleMinHash>(globalList);
		}

		// Token: 0x04000233 RID: 563
		internal ListSpan<RuleMinHash> ruleMinHashList;

		// Token: 0x04000234 RID: 564
		internal int curIdx;
	}
}
