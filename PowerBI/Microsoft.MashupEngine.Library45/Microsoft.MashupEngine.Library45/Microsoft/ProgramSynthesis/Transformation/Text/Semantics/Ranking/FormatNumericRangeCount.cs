using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D47 RID: 7495
	internal class FormatNumericRangeCount : RankingFeature
	{
		// Token: 0x0600FC83 RID: 64643 RVA: 0x0035E2CB File Offset: 0x0035C4CB
		public FormatNumericRangeCount(Grammar grammar)
			: base(grammar, "FormatNumericRangeCount", 0.0, true)
		{
		}

		// Token: 0x0600FC84 RID: 64644 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("FormatNumericRange", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
