using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D8 RID: 5848
	[Flags]
	public enum MatchName : ulong
	{
		// Token: 0x04004BE1 RID: 19425
		None = 0UL,
		// Token: 0x04004BE2 RID: 19426
		DigitBlock = 1UL,
		// Token: 0x04004BE3 RID: 19427
		LastWord = 2UL,
		// Token: 0x04004BE4 RID: 19428
		LowercaseWord = 4UL,
		// Token: 0x04004BE5 RID: 19429
		DelimiterWithWhitespace = 8UL,
		// Token: 0x04004BE6 RID: 19430
		LowerCharSpaceOrEnd = 16UL,
		// Token: 0x04004BE7 RID: 19431
		UpperLowerChar = 32UL,
		// Token: 0x04004BE8 RID: 19432
		WhitespaceUpperLowerChar = 64UL,
		// Token: 0x04004BE9 RID: 19433
		Whitespace = 128UL,
		// Token: 0x04004BEA RID: 19434
		MultipleLowerChar = 256UL,
		// Token: 0x04004BEB RID: 19435
		UpperChar = 512UL,
		// Token: 0x04004BEC RID: 19436
		Unassigned = 1073741824UL,
		// Token: 0x04004BED RID: 19437
		Digit = 2147483648UL,
		// Token: 0x04004BEE RID: 19438
		All = 18446744073709551615UL
	}
}
