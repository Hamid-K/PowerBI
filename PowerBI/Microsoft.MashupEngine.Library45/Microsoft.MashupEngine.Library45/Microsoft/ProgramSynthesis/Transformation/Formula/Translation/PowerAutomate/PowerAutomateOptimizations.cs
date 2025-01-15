using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001900 RID: 6400
	[Flags]
	public enum PowerAutomateOptimizations : byte
	{
		// Token: 0x0400510E RID: 20750
		None = 0,
		// Token: 0x0400510F RID: 20751
		All = 255,
		// Token: 0x04005110 RID: 20752
		Default = 255,
		// Token: 0x04005111 RID: 20753
		CamelCaseFunctionNames = 1,
		// Token: 0x04005112 RID: 20754
		FlattenConcat = 2
	}
}
