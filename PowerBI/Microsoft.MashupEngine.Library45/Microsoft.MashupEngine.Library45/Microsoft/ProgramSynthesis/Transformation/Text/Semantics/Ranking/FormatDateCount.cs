using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D46 RID: 7494
	internal class FormatDateCount : RankingFeature
	{
		// Token: 0x0600FC81 RID: 64641 RVA: 0x0035E2B3 File Offset: 0x0035C4B3
		public FormatDateCount(Grammar grammar)
			: base(grammar, "FormatDateCount", 24.0676607445303, true)
		{
		}

		// Token: 0x0600FC82 RID: 64642 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("FormatPartialDateTime", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
