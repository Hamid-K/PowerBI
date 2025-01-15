using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001832 RID: 6194
	[Flags]
	public enum PandasOptimizations : byte
	{
		// Token: 0x04004FAC RID: 20396
		None = 0,
		// Token: 0x04004FAD RID: 20397
		All = 255,
		// Token: 0x04004FAE RID: 20398
		Default = 255,
		// Token: 0x04004FAF RID: 20399
		UseSeriesFunctions = 1,
		// Token: 0x04004FB0 RID: 20400
		UseInlineFunctions = 2
	}
}
