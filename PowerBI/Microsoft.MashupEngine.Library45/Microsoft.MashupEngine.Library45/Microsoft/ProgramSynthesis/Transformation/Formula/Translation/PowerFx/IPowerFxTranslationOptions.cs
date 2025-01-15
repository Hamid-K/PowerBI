using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D0 RID: 6352
	public interface IPowerFxTranslationOptions : ITranslationOptions
	{
		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x0600CF6C RID: 53100
		PowerFxOptimizations Optimizations { get; }

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x0600CF6D RID: 53101
		CultureInfo UserInterfaceCulture { get; }
	}
}
