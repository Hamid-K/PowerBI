using System;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A3 RID: 163
	[DebuggerDisplay("minHashValue={minHashValue} minHashToken={minHashToken}")]
	[Serializable]
	internal sealed class RuleMinHash
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001BAD5 File Offset: 0x00019CD5
		internal void Copy(RuleMinHash ruleMinHash)
		{
			this.minHashValue = ruleMinHash.minHashValue;
			this.minHashToken = ruleMinHash.minHashToken;
			this.isUnitRule = ruleMinHash.isUnitRule;
			this.groupId = ruleMinHash.groupId;
			this.ruleId = ruleMinHash.ruleId;
		}

		// Token: 0x0400022E RID: 558
		internal double minHashValue;

		// Token: 0x0400022F RID: 559
		internal int minHashToken;

		// Token: 0x04000230 RID: 560
		internal bool isUnitRule;

		// Token: 0x04000231 RID: 561
		internal int groupId;

		// Token: 0x04000232 RID: 562
		internal int ruleId;
	}
}
