using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001833 RID: 6195
	public interface IPySparkTranslationOptions : IPandasTranslationOptions, IPythonTranslationOptions, ITranslationOptions, IRenderingOptions
	{
		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x0600CB29 RID: 52009
		bool UseSqlDataFrame { get; }

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x0600CB2A RID: 52010
		bool ImportPySpark { get; }

		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x0600CB2B RID: 52011
		PySparkOptimizations PySparkOptimizations { get; }
	}
}
