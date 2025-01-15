using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D3E RID: 7486
	internal class ConstantStringCount : RankingFeature
	{
		// Token: 0x0600FC6B RID: 64619 RVA: 0x0035E025 File Offset: 0x0035C225
		public ConstantStringCount(Grammar grammar)
			: base(grammar, "ConstantStringCount", -19.1802649639123, true)
		{
		}

		// Token: 0x0600FC6C RID: 64620 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("ConstStr", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
