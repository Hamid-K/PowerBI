using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001815 RID: 6165
	public interface ISqlTranslationOptions : ITranslationOptions
	{
		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x0600CA87 RID: 51847
		SqlOptimizations Optimizations { get; }
	}
}
