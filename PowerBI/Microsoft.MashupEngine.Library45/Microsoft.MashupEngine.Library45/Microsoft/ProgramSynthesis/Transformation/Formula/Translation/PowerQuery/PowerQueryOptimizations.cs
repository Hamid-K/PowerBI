using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x02001897 RID: 6295
	[Flags]
	public enum PowerQueryOptimizations : byte
	{
		// Token: 0x04005071 RID: 20593
		None = 0,
		// Token: 0x04005072 RID: 20594
		All = 255,
		// Token: 0x04005073 RID: 20595
		Default = 255,
		// Token: 0x04005074 RID: 20596
		OmitDefaultCulture = 1
	}
}
