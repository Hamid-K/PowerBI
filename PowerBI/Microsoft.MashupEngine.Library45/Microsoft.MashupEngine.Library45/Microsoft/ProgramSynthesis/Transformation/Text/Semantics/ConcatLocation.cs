using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CD2 RID: 7378
	[Flags]
	public enum ConcatLocation
	{
		// Token: 0x04005CB5 RID: 23733
		Nowhere = 1,
		// Token: 0x04005CB6 RID: 23734
		Anywhere = 2,
		// Token: 0x04005CB7 RID: 23735
		AtTokenBoundaries = 4,
		// Token: 0x04005CB8 RID: 23736
		AtTokenBoundariesExceptInputs = 8
	}
}
