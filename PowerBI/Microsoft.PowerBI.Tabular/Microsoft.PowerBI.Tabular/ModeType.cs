using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000018 RID: 24
	public enum ModeType
	{
		// Token: 0x04000063 RID: 99
		Import,
		// Token: 0x04000064 RID: 100
		DirectQuery,
		// Token: 0x04000065 RID: 101
		Default,
		// Token: 0x04000066 RID: 102
		[CompatibilityRequirement(Pbi = "1200")]
		Push,
		// Token: 0x04000067 RID: 103
		[CompatibilityRequirement("1455")]
		Dual,
		// Token: 0x04000068 RID: 104
		[CompatibilityRequirement("1604")]
		DirectLake
	}
}
