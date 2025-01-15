using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001834 RID: 6196
	[Flags]
	public enum PySparkOptimizations : byte
	{
		// Token: 0x04004FB2 RID: 20402
		None = 0,
		// Token: 0x04004FB3 RID: 20403
		All = 255,
		// Token: 0x04004FB4 RID: 20404
		Default = 255,
		// Token: 0x04004FB5 RID: 20405
		UseInlineFunctions = 1
	}
}
