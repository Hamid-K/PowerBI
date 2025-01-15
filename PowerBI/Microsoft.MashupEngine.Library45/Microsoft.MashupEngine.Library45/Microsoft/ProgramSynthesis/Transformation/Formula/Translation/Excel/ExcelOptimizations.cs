using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001939 RID: 6457
	[Flags]
	public enum ExcelOptimizations : byte
	{
		// Token: 0x04005154 RID: 20820
		None = 0,
		// Token: 0x04005155 RID: 20821
		All = 255,
		// Token: 0x04005156 RID: 20822
		Default = 247,
		// Token: 0x04005157 RID: 20823
		UpperCaseFunctionNames = 1,
		// Token: 0x04005158 RID: 20824
		FunctionLocaleArgumentSeparator = 2,
		// Token: 0x04005159 RID: 20825
		OmitDefaultCulture = 4,
		// Token: 0x0400515A RID: 20826
		UseLet = 8
	}
}
