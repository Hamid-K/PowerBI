using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D48 RID: 7496
	internal class FormatNumberCount : RankingFeature
	{
		// Token: 0x0600FC85 RID: 64645 RVA: 0x0035E2E3 File Offset: 0x0035C4E3
		public FormatNumberCount(Grammar grammar)
			: base(grammar, "FormatNumberCount", 2.98532331406264, true)
		{
		}

		// Token: 0x0600FC86 RID: 64646 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("FormatNumber", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
