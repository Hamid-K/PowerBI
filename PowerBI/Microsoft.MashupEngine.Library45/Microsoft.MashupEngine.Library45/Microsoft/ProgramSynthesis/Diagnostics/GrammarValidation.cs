using System;

namespace Microsoft.ProgramSynthesis.Diagnostics
{
	// Token: 0x020008BB RID: 2235
	[Flags]
	public enum GrammarValidation : uint
	{
		// Token: 0x04001830 RID: 6192
		None = 0U,
		// Token: 0x04001831 RID: 6193
		Core = 1U,
		// Token: 0x04001832 RID: 6194
		Syntax = 2U,
		// Token: 0x04001833 RID: 6195
		Semantics = 4U,
		// Token: 0x04001834 RID: 6196
		DeductiveLearning = 8U,
		// Token: 0x04001835 RID: 6197
		BottomUpLearning = 16U,
		// Token: 0x04001836 RID: 6198
		Features = 32U,
		// Token: 0x04001837 RID: 6199
		Learning = 24U,
		// Token: 0x04001838 RID: 6200
		All = 4294967295U
	}
}
