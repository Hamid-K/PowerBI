using System;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AFF RID: 6911
	[Flags]
	public enum MissingValueType
	{
		// Token: 0x04005641 RID: 22081
		NAValue = 1,
		// Token: 0x04005642 RID: 22082
		EmptyString = 2,
		// Token: 0x04005643 RID: 22083
		WhiteSpace = 4,
		// Token: 0x04005644 RID: 22084
		NanString = 8,
		// Token: 0x04005645 RID: 22085
		All = -1,
		// Token: 0x04005646 RID: 22086
		None = 0
	}
}
