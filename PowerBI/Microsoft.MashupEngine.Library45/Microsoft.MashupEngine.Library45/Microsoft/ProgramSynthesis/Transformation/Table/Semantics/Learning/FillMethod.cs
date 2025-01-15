using System;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B0F RID: 6927
	public enum FillMethod
	{
		// Token: 0x04005664 RID: 22116
		None,
		// Token: 0x04005665 RID: 22117
		Mode,
		// Token: 0x04005666 RID: 22118
		Mean,
		// Token: 0x04005667 RID: 22119
		RoundedMean,
		// Token: 0x04005668 RID: 22120
		Max,
		// Token: 0x04005669 RID: 22121
		Min,
		// Token: 0x0400566A RID: 22122
		FixedValueZero
	}
}
