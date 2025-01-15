using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001836 RID: 6198
	[Flags]
	public enum PythonOptimizations : byte
	{
		// Token: 0x04004FB7 RID: 20407
		None = 0,
		// Token: 0x04004FB8 RID: 20408
		All = 255,
		// Token: 0x04004FB9 RID: 20409
		Default = 251,
		// Token: 0x04004FBA RID: 20410
		ConsolidateCommonExpressions = 1,
		// Token: 0x04004FBB RID: 20411
		UseTernary = 2,
		// Token: 0x04004FBC RID: 20412
		RemoveUnusedParameters = 4,
		// Token: 0x04004FBD RID: 20413
		RemoveTrailingElse = 8,
		// Token: 0x04004FBE RID: 20414
		ConsolidateCommonVariables = 16
	}
}
