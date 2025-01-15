using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001983 RID: 6531
	[Flags]
	public enum CSharpOptimizations : byte
	{
		// Token: 0x040051E2 RID: 20962
		None = 0,
		// Token: 0x040051E3 RID: 20963
		All = 255,
		// Token: 0x040051E4 RID: 20964
		Default = 255,
		// Token: 0x040051E5 RID: 20965
		ConsolidateCommonExpressions = 1,
		// Token: 0x040051E6 RID: 20966
		UseTernary = 2,
		// Token: 0x040051E7 RID: 20967
		RemoveTrailingElse = 4
	}
}
