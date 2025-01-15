using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x020018FF RID: 6399
	public interface IPowerAutomateTranslationOptions : ITranslationOptions
	{
		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x0600D0D7 RID: 53463
		PowerAutomateOptimizations Optimizations { get; }

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x0600D0D8 RID: 53464
		CultureInfo UserInterfaceCulture { get; }
	}
}
