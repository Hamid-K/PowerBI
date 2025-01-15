using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	internal sealed class UnitRuleGroup
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x0001BAA2 File Offset: 0x00019CA2
		internal UnitRuleGroup(UnitRule[] globalUnitRuleList)
		{
			this.ruleList = new ListSpan<UnitRule>(globalUnitRuleList);
		}

		// Token: 0x04000221 RID: 545
		internal ListSpan<UnitRule> ruleList;

		// Token: 0x04000222 RID: 546
		internal int numNotCovered;

		// Token: 0x04000223 RID: 547
		internal bool bApplied;

		// Token: 0x04000224 RID: 548
		internal int numFree;
	}
}
