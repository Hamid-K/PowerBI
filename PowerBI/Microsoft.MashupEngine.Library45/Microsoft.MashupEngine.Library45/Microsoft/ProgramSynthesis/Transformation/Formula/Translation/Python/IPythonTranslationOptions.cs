using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001835 RID: 6197
	public interface IPythonTranslationOptions : ITranslationOptions, IRenderingOptions
	{
		// Token: 0x17002242 RID: 8770
		// (get) Token: 0x0600CB2C RID: 52012
		string DefinitionName { get; }

		// Token: 0x17002243 RID: 8771
		// (get) Token: 0x0600CB2D RID: 52013
		ILocalizedStrings LocalizedStrings { get; }

		// Token: 0x17002244 RID: 8772
		// (get) Token: 0x0600CB2E RID: 52014
		int MaximumExamplesInComments { get; }

		// Token: 0x17002245 RID: 8773
		// (get) Token: 0x0600CB2F RID: 52015
		PythonOptimizations PythonOptimizations { get; }

		// Token: 0x17002246 RID: 8774
		// (get) Token: 0x0600CB30 RID: 52016
		bool UseNumpy { get; }
	}
}
