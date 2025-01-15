using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001675 RID: 5749
	[Flags]
	public enum DateTimeSourceKind : byte
	{
		// Token: 0x04004A31 RID: 18993
		Input = 2,
		// Token: 0x04004A32 RID: 18994
		Parsed = 4,
		// Token: 0x04004A33 RID: 18995
		ParsedPartial = 8,
		// Token: 0x04004A34 RID: 18996
		InputAndParsed = 6,
		// Token: 0x04004A35 RID: 18997
		All = 255
	}
}
