using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x02001896 RID: 6294
	public interface IPowerQueryTranslationOptions : ITranslationOptions
	{
		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x0600CDDA RID: 52698
		PowerQueryOptimizations Optimizations { get; }

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x0600CDDB RID: 52699
		CultureInfo UserInterfaceCulture { get; }

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x0600CDDC RID: 52700
		OutputType OutputType { get; }
	}
}
