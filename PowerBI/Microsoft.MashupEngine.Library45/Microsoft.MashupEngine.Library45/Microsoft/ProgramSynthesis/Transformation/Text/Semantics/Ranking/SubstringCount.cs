using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D42 RID: 7490
	internal class SubstringCount : RankingFeature
	{
		// Token: 0x0600FC75 RID: 64629 RVA: 0x0035E0CA File Offset: 0x0035C2CA
		public SubstringCount(Grammar grammar)
			: base(grammar, "SubstringCount", -4.10965719959982, true)
		{
		}

		// Token: 0x0600FC76 RID: 64630 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("SubStr", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
