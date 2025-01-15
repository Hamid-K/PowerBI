using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D1 RID: 6353
	[Flags]
	public enum PowerFxOptimizations : byte
	{
		// Token: 0x040050CD RID: 20685
		None = 0,
		// Token: 0x040050CE RID: 20686
		All = 255,
		// Token: 0x040050CF RID: 20687
		Default = 255,
		// Token: 0x040050D0 RID: 20688
		FunctionLocaleArgumentSeparator = 1,
		// Token: 0x040050D1 RID: 20689
		OmitDefaultCulture = 2,
		// Token: 0x040050D2 RID: 20690
		UseWith = 4
	}
}
