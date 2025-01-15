using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Json
{
	// Token: 0x02000187 RID: 391
	[Flags]
	public enum JsonErrors
	{
		// Token: 0x04000439 RID: 1081
		None = 0,
		// Token: 0x0400043A RID: 1082
		Truncated = 1,
		// Token: 0x0400043B RID: 1083
		TrailingCommas = 2,
		// Token: 0x0400043C RID: 1084
		MismatchedBrackets = 4,
		// Token: 0x0400043D RID: 1085
		ExtraCharacters = 8,
		// Token: 0x0400043E RID: 1086
		HasComment = 16,
		// Token: 0x0400043F RID: 1087
		EmptyQuote = 32,
		// Token: 0x04000440 RID: 1088
		All = -1
	}
}
