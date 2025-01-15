using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D56 RID: 7510
	internal class HasConcatenations : BooleanRankingFeature
	{
		// Token: 0x0600FCA8 RID: 64680 RVA: 0x0035E646 File Offset: 0x0035C846
		public HasConcatenations(Grammar grammar)
			: base(grammar, "HasConcatenations", -137.654338456922, true)
		{
		}

		// Token: 0x0600FCA9 RID: 64681 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("Concat", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
