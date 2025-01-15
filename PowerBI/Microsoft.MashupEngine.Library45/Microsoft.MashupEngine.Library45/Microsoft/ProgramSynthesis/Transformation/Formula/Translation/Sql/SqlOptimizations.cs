using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001816 RID: 6166
	[Flags]
	public enum SqlOptimizations : byte
	{
		// Token: 0x04004F9B RID: 20379
		None = 0,
		// Token: 0x04004F9C RID: 20380
		All = 255,
		// Token: 0x04004F9D RID: 20381
		Default = 255,
		// Token: 0x04004F9E RID: 20382
		UpperCaseFunctionNames = 1
	}
}
