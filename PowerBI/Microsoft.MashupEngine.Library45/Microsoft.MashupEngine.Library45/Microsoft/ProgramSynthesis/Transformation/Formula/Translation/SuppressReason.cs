using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001814 RID: 6164
	public enum SuppressReason : uint
	{
		// Token: 0x04004F93 RID: 20371
		Unknown = 1U,
		// Token: 0x04004F94 RID: 20372
		ConstantOutput,
		// Token: 0x04004F95 RID: 20373
		WholeColumnOutput,
		// Token: 0x04004F96 RID: 20374
		LowPrecision,
		// Token: 0x04004F97 RID: 20375
		LowAcceptance,
		// Token: 0x04004F98 RID: 20376
		NoTranslation,
		// Token: 0x04004F99 RID: 20377
		InconsistentOutput
	}
}
