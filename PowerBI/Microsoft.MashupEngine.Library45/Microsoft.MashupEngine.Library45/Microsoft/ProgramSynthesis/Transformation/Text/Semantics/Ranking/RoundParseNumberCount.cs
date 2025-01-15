using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D55 RID: 7509
	internal class RoundParseNumberCount : RankingFeature
	{
		// Token: 0x0600FCA6 RID: 64678 RVA: 0x0035E62E File Offset: 0x0035C82E
		public RoundParseNumberCount(Grammar grammar)
			: base(grammar, "RoundParseNumberCount", -10.2679866373636, true)
		{
		}

		// Token: 0x0600FCA7 RID: 64679 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("RoundNumber", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ParseNumber", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
