using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001674 RID: 5748
	[Flags]
	public enum NumberSourceKind : byte
	{
		// Token: 0x04004A2C RID: 18988
		Input = 2,
		// Token: 0x04004A2D RID: 18989
		Parsed = 4,
		// Token: 0x04004A2E RID: 18990
		InputAndParsed = 6,
		// Token: 0x04004A2F RID: 18991
		All = 255
	}
}
